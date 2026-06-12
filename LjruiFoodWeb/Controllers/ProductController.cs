using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Models;
using LjruiFoodWeb.Repositories;
using LjruiFoodWeb.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace LjruiFoodWeb.Controllers;

public class ProductController : Controller
{
    private readonly ISanPhamRepository _sanPhamRepo;
    private readonly IDanhMucRepository _danhMucRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(ISanPhamRepository sanPhamRepo, IDanhMucRepository danhMucRepo, IWebHostEnvironment webHostEnvironment)
    {
        _sanPhamRepo = sanPhamRepo;
        _danhMucRepo = danhMucRepo;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> List(string? danhMuc = "tatca", string? sapXep = "mac-dinh", int page = 1)
    {
        const int pageSize = 6;
        if (page < 1) page = 1;

        var query = _sanPhamRepo.GetAllQueryable();

        // Lọc theo danh mục
        if (!string.IsNullOrEmpty(danhMuc) && danhMuc != "tatca")
        {
            query = query.Where(p => p.DanhMuc != null && p.DanhMuc.Slug == danhMuc);
        }

        // Sắp xếp
        query = sapXep switch
        {
            "gia-thap-cao" => query.OrderBy(p => p.GiaHienTai),
            "gia-cao-thap" => query.OrderByDescending(p => p.GiaHienTai),
            "ban-chay" => query.OrderByDescending(p => p.SoLuongDaBan),
            "danh-gia-cao" => query.OrderByDescending(p => p.SoSaoDanhGia),
            "moi-nhat" => query.OrderByDescending(p => p.NgayTao),
            _ => query.OrderBy(p => p.Ma)
        };

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var sanPhams = await query
            .Include(p => p.DanhMuc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var danhMucs = await _danhMucRepo.GetAllAsync();

        ViewBag.DanhMucs = new SelectList(danhMucs, "Slug", "Ten", danhMuc);
        ViewBag.DanhMucHienTai = danhMuc;
        ViewBag.SapXepHienTai = sapXep;
        ViewBag.Pagination = new ViewModels.PaginationViewModel
        {
            CurrentPage = page,
            TotalPages = totalPages,
            TotalItems = totalItems,
            PageSize = pageSize,
            DanhMuc = danhMuc,
            SapXep = sapXep
        };

        return View(sanPhams);
    }

    public async Task<IActionResult> Details(string slug)
    {
        var sanPham = await _sanPhamRepo.GetBySlugAsync(slug);

        if (sanPham is null)
            return RedirectToAction("List", "Product");

        return View(sanPham);
    }

    public async Task<IActionResult> AddToCart(string ma)
    {
        if (string.IsNullOrWhiteSpace(ma))
            return RedirectToAction("List");

        var sp = await _sanPhamRepo.GetBySlugAsync(ma);
        if (sp is null)
            return RedirectToAction("List");

        var cart = DocGioHang();
        var line = cart.FirstOrDefault(x => x.Ma == sp.Slug);
        if (line is null)
            cart.Add(new ViewModels.GioHangItemViewModel { Ma = sp.Slug, Ten = sp.Ten, DonGia = sp.GiaHienTai, SoLuong = 1 });
        else
            line.SoLuong++;

        LuuGioHang(cart);
        TempData["ThongBaoGio"] = $"Đã thêm «{sp.Ten}» vào giỏ hàng.";
        return RedirectToAction("List");
    }

    private List<ViewModels.GioHangItemViewModel> DocGioHang()
    {
        var raw = HttpContext.Session.GetString("ljrui_gio_hang");
        if (string.IsNullOrEmpty(raw))
            return new List<ViewModels.GioHangItemViewModel>();

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<ViewModels.GioHangItemViewModel>>(raw) ?? new List<ViewModels.GioHangItemViewModel>();
        }
        catch
        {
            return new List<ViewModels.GioHangItemViewModel>();
        }
    }

    private void LuuGioHang(List<ViewModels.GioHangItemViewModel> cart)
    {
        HttpContext.Session.SetString("ljrui_gio_hang", System.Text.Json.JsonSerializer.Serialize(cart));
    }

}
