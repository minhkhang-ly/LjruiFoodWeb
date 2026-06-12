using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Data;
using LjruiFoodWeb.Models;
using LjruiFoodWeb.ViewModels;

namespace LjruiFoodWeb.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly LjruiFoodWeb.Services.IEmailService _emailService;
    private const string CartSessionKey = "ljrui_gio_hang";

    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public HomeController(ApplicationDbContext context, LjruiFoodWeb.Services.IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Menu(string? danhMuc = "tatca", string? sapXep = "mac-dinh")
    {
        return RedirectToAction("List", "Product", new { area = "" });
    }

    public IActionResult ChiTietMon(string ma)
    {
        var sp = _context.SanPhams.FirstOrDefault(p => p.Slug == ma);
        if (sp is null)
            return RedirectToAction(nameof(Menu));

        return View(sp);
    }

    public IActionResult TimKiem(string? q)
    {
        var tuKhoa = q?.Trim() ?? "";
        List<SanPham> ketQua;

        if (string.IsNullOrEmpty(tuKhoa))
        {
            ketQua = _context.SanPhams.ToList();
        }
        else
        {
            ketQua = _context.SanPhams
                .Where(p => p.Ten.Contains(tuKhoa) ||
                           (p.MoTa != null && p.MoTa.Contains(tuKhoa)) ||
                           (p.MoTaDayDu != null && p.MoTaDayDu.Contains(tuKhoa)) ||
                           (p.ThanhPhan != null && p.ThanhPhan.Contains(tuKhoa)) ||
                           (p.NguonGoc != null && p.NguonGoc.Contains(tuKhoa)))
                .ToList();
        }

        return View(new TimKiemViewModel { TuKhoa = tuKhoa, KetQua = ketQua });
    }

    public IActionResult ThemVaoGio(string ma)
    {
        if (string.IsNullOrWhiteSpace(ma))
            return RedirectToAction(nameof(GioHang));

        var sp = _context.SanPhams.FirstOrDefault(p => p.Slug == ma);
        if (sp is null)
            return RedirectToAction(nameof(Menu));

        var cart = DocGioHang();
        var line = cart.FirstOrDefault(x => x.Ma == sp.Slug);
        if (line is null)
            cart.Add(new GioHangItemViewModel { Ma = sp.Slug, Ten = sp.Ten, DonGia = sp.GiaHienTai, SoLuong = 1 });
        else
            line.SoLuong++;

        LuuGioHang(cart);
        TempData["ThongBaoGio"] = $"Đã thêm «{sp.Ten}» vào giỏ hàng.";
        return RedirectToAction(nameof(GioHang));
    }

    public IActionResult XoaKhoiGio(string ma)
    {
        var cart = DocGioHang();
        cart.RemoveAll(x => string.Equals(x.Ma, ma, StringComparison.OrdinalIgnoreCase));
        LuuGioHang(cart);
        TempData["ThongBaoGio"] = "Đã xóa món khỏi giỏ hàng.";
        return RedirectToAction(nameof(GioHang));
    }

    public IActionResult GioHang()
    {
        return View(DocGioHang());
    }

    public IActionResult CapNhatSoLuong(string ma, int soLuong)
    {
        var cart = DocGioHang();
        var item = cart.FirstOrDefault(x => string.Equals(x.Ma, ma, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            if (soLuong <= 0)
                cart.Remove(item);
            else
                item.SoLuong = soLuong;
        }
        LuuGioHang(cart);
        return RedirectToAction(nameof(GioHang));
    }

    [Authorize]
    public IActionResult DatHang()
    {
        var cart = DocGioHang();
        if (cart.Count == 0)
            return RedirectToAction(nameof(GioHang));

        var userId = GetUserId();
        if (userId == null)
            return RedirectToAction("DangNhap", "Account");

        var tong = cart.Sum(x => x.ThanhTien);

        var model = new ThanhToanVM
        {
            HoTen = User.FindFirst("HoTen")?.Value ?? User.Identity?.Name ?? "",
            SoDienThoai = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "",
            Email = User.FindFirst(ClaimTypes.Email)?.Value ?? "",
            DiaChi = "",
            GioHang = cart,
            TongTien = tong
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DatHang(ThanhToanVM model)
    {
        var cart = DocGioHang();
        if (cart.Count == 0)
            return RedirectToAction(nameof(GioHang));

        if (!ModelState.IsValid)
        {
            model.GioHang = cart;
            model.TongTien = cart.Sum(x => x.ThanhTien);
            return View(model);
        }

        var userId = GetUserId();

        // Create order
        var donHang = new DonHang
        {
            NguoiDungMa = userId,
            HoTen = model.HoTen,
            SoDienThoai = model.SoDienThoai,
            Email = model.Email,
            DiaChiGiaoHang = model.DiaChiGiaoHang,
            GhiChu = model.GhiChu,
            TongTien = cart.Sum(x => x.ThanhTien),
            TrangThai = "ChoXacNhan",
            NgayDatHang = DateTime.Now,
            NgayCapNhat = DateTime.Now,
            ChiTietDonHangs = []
        };

        // Load product details and create order details
        foreach (var item in cart)
        {
            var sp = await _context.SanPhams.FirstOrDefaultAsync(p => p.Slug == item.Ma);
            if (sp != null)
            {
                donHang.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    SanPhamMa = sp.Ma,
                    SoLuong = item.SoLuong,
                    DonGia = item.DonGia,
                    ThanhTien = item.ThanhTien
                });
            }
        }

        _context.DonHangs.Add(donHang);
        await _context.SaveChangesAsync();

        if (!string.IsNullOrEmpty(model.Email))
        {
            var emailSubject = $"Xác nhận đơn hàng #{donHang.Ma} từ LjruiFood";
            var emailBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <h2 style='color: #e52d27;'>Cảm ơn bạn đã đặt hàng tại Ljrui Food!</h2>
                    <p>Xin chào <strong>{model.HoTen}</strong>,</p>
                    <p>Đơn hàng của bạn đã được nhận và đang chờ xử lý.</p>
                    <div style='background-color: #f9f9f9; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                        <p style='margin: 5px 0;'><strong>Mã đơn hàng:</strong> #{donHang.Ma}</p>
                        <p style='margin: 5px 0;'><strong>Tổng thanh toán:</strong> <span style='color: #e52d27; font-weight: bold;'>{donHang.TongTien.ToString("N0")} đ</span></p>
                        <p style='margin: 5px 0;'><strong>Địa chỉ giao hàng:</strong> {model.DiaChiGiaoHang}</p>
                        <p style='margin: 5px 0;'><strong>Số điện thoại:</strong> {model.SoDienThoai}</p>
                    </div>
                    <p>Chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất để giao hàng.</p>
                    <p>Trân trọng,<br>Đội ngũ Ljrui Food.</p>
                </div>
            ";
            try
            {
                await _emailService.SendEmailAsync(model.Email, emailSubject, emailBody);
            }
            catch
            {
                // Ignore email sending error to avoid blocking the checkout
            }
        }

        HttpContext.Session.Remove(CartSessionKey);
        TempData["OrderId"] = donHang.Ma;
        return RedirectToAction("OrderCompleted", "Home");
    }

    [Authorize]
    public IActionResult OrderCompleted()
    {
        if (TempData["OrderId"] is not int orderId)
            return RedirectToAction("Index");

        return View(model: orderId);
    }

    [HttpGet]
    public IActionResult HoSo()
    {
        return View(new HoSoFormModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult HoSo(HoSoFormModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["HoSoDaLuu"] = true;
        return RedirectToAction(nameof(HoSoDaLuu));
    }

    public IActionResult HoSoDaLuu()
    {
        if (TempData["HoSoDaLuu"] is null)
            return RedirectToAction(nameof(HoSo));

        return View();
    }

    public IActionResult Review()
    {
        var reviews = _context.DanhGias.OrderByDescending(r => r.NgayDanhGia).ToList();
        return View(reviews);
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View(new LienHeFormModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GuiLienHe(LienHeFormModel model)
    {
        if (!ModelState.IsValid)
            return View("Contact", model);

        var tomTat = model.NoiDung.Trim();
        if (tomTat.Length > 200)
            tomTat = tomTat[..200] + "…";

        TempData["LienHeTomTat"] = tomTat;
        return RedirectToAction(nameof(CamOnLienHe));
    }

    public IActionResult CamOnLienHe()
    {
        var nd = TempData["LienHeTomTat"] as string;
        if (string.IsNullOrEmpty(nd))
            return RedirectToAction(nameof(Contact));

        return View(model: nd);
    }

    public IActionResult MangXaHoi(string? kenh)
    {
        kenh = string.IsNullOrWhiteSpace(kenh) ? "tong-quan" : kenh.ToLowerInvariant();
        var hopLe = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "facebook", "twitter", "instagram", "youtube", "tong-quan"
        };
        if (!hopLe.Contains(kenh))
            kenh = "tong-quan";

        return View(model: kenh);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Blog()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private List<GioHangItemViewModel> DocGioHang()
    {
        var raw = HttpContext.Session.GetString(CartSessionKey);
        if (string.IsNullOrEmpty(raw))
            return [];

        try
        {
            return JsonSerializer.Deserialize<List<GioHangItemViewModel>>(raw, JsonOpts) ?? [];
        }
        catch
        {
            return [];
        }
    }

    private void LuuGioHang(List<GioHangItemViewModel> cart)
    {
        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart, JsonOpts));
    }

    private int? GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim != null && int.TryParse(claim.Value, out int id))
            return id;
        return null;
    }
}
