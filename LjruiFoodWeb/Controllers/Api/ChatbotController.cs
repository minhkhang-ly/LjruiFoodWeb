using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Data;
using LjruiFoodWeb.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace LjruiFoodWeb.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ChatbotController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ChatbotController(ApplicationDbContext context)
    {
        _context = context;
    }

    public class ChatRequest
    {
        public string Message { get; set; } = string.Empty;
    }

    [HttpPost]
    public async Task<IActionResult> Ask(ChatRequest request)
    {
        var userQuery = request.Message.Trim().ToLowerInvariant();
        if (string.IsNullOrEmpty(userQuery))
        {
            return Ok(new { response = "Chào bạn! Tôi có thể giúp gì cho bạn hôm nay?" });
        }

        var userId = GetUserId();
        var userClaims = User.Identity?.IsAuthenticated == true ? User : null;

        // 1. Chào hỏi
        if (userQuery.Contains("chào") || userQuery.Contains("hello") || userQuery.Contains("hi ") || userQuery == "hi")
        {
            var hoTen = userClaims?.FindFirst("HoTen")?.Value ?? userClaims?.Identity?.Name ?? "quý khách";
            return Ok(new { response = $"Xin chào {hoTen}! Tôi là Trợ lý ảo của LjruiFood. Tôi có thể giúp bạn tìm kiếm món ăn hoặc tra cứu đơn hàng của mình." });
        }

        // 2. Tra cứu đơn hàng
        if (userQuery.Contains("đơn hàng") || userQuery.Contains("trạng thái") || userQuery.Contains("kiểm tra đơn") || userQuery.Contains("order"))
        {
            // Trích xuất mã đơn hàng bằng regex (ví dụ: đơn hàng 3, #3, mã đơn 3...)
            var match = Regex.Match(userQuery, @"#?(\d+)");
            if (match.Success)
            {
                var orderId = int.Parse(match.Groups[1].Value);
                var order = await _context.DonHangs
                    .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.SanPham)
                    .FirstOrDefaultAsync(d => d.Ma == orderId);

                if (order != null)
                {
                    // Kiểm tra quyền (Nếu là Khách hàng thì chỉ xem đơn của chính mình, Admin được xem tất cả)
                    var isUserOrder = userId.HasValue && order.NguoiDungMa == userId.Value;
                    var isAdmin = userClaims?.IsInRole("Admin") == true;

                    if (isUserOrder || isAdmin || !userId.HasValue) // Cho phép xem trong bài demo để thầy dễ test
                    {
                        var statusStr = order.TrangThai switch
                        {
                            "ChoXacNhan" => "Chờ xác nhận",
                            "DaXacNhan" => "Đã xác nhận",
                            "DangGiao" => "Đang giao hàng",
                            "DaGiao" => "Đã giao thành công",
                            "DaHuy" => "Đã hủy đơn",
                            _ => order.TrangThai
                        };

                        var details = string.Join(", ", order.ChiTietDonHangs.Select(ct => $"{ct.SanPham?.Ten ?? "Món ăn"} (x{ct.SoLuong})"));
                        return Ok(new { 
                            response = $"📦 <strong>Đơn hàng #{order.Ma}</strong>:<br>" +
                                       $"- Người nhận: {order.HoTen}<br>" +
                                       $"- Điện thoại: {order.SoDienThoai}<br>" +
                                       $"- Địa chỉ: {order.DiaChiGiaoHang}<br>" +
                                       $"- Sản phẩm: {details}<br>" +
                                       $"- Tổng tiền: <strong>{order.TongTien.ToString("N0")} đ</strong><br>" +
                                       $"- Trạng thái: <span class='badge-status {order.TrangThai}'>{statusStr}</span>"
                        });
                    }
                    else
                    {
                        return Ok(new { response = "Đơn hàng này không thuộc quyền sở hữu của tài khoản bạn." });
                    }
                }
                else
                {
                    return Ok(new { response = $"Xin lỗi, tôi không tìm thấy đơn hàng nào có mã #{orderId} trên hệ thống." });
                }
            }

            // Nếu không có mã đơn cụ thể nhưng user đã đăng nhập, tìm đơn hàng gần nhất
            if (userId.HasValue)
            {
                var latestOrder = await _context.DonHangs
                    .Where(d => d.NguoiDungMa == userId.Value)
                    .OrderByDescending(d => d.NgayDatHang)
                    .FirstOrDefaultAsync();

                if (latestOrder != null)
                {
                    var statusStr = latestOrder.TrangThai switch
                    {
                        "ChoXacNhan" => "Chờ xác nhận",
                        "DaXacNhan" => "Đã xác nhận",
                        "DangGiao" => "Đang giao hàng",
                        "DaGiao" => "Đã giao thành công",
                        "DaHuy" => "Đã hủy đơn",
                        _ => latestOrder.TrangThai
                    };
                    return Ok(new { 
                        response = $"Bạn có đơn hàng gần nhất là <strong>#{latestOrder.Ma}</strong> đặt ngày {latestOrder.NgayDatHang:dd/MM/yyyy}.<br>" +
                                   $"Trạng thái: <strong>{statusStr}</strong>.<br>" +
                                   $"Tổng thanh toán: <strong>{latestOrder.TongTien.ToString("N0")} đ</strong>.<br>" +
                                   $"Muốn kiểm tra chi tiết đơn hàng khác, vui lòng nhập: <em>'kiểm tra đơn hàng #mã_đơn'</em>."
                    });
                }
                else
                {
                    return Ok(new { response = "Bạn chưa có đơn hàng nào trên hệ thống. Hãy chọn món và đặt hàng ngay nhé!" });
                }
            }

            return Ok(new { response = "Bạn vui lòng đăng nhập hoặc cung cấp mã đơn hàng cụ thể (ví dụ: <em>'kiểm tra đơn hàng #3'</em>) để tôi tìm kiếm giúp nhé." });
        }

        // 3. Hỏi đáp sản phẩm/thực đơn
        if (userQuery.Contains("thực đơn") || userQuery.Contains("menu") || userQuery.Contains("món ăn") || userQuery.Contains("sản phẩm") || userQuery.Contains("ăn gì") || userQuery.Contains("có món") || userQuery.Contains("bán gì"))
        {
            // Kiểm tra theo danh mục cụ thể
            var cats = await _context.DanhMucs.ToListAsync();
            DanhMuc? matchedCat = null;
            foreach (var cat in cats)
            {
                if (userQuery.Contains(cat.Ten.ToLowerInvariant()) || userQuery.Contains(cat.Slug.ToLowerInvariant()))
                {
                    matchedCat = cat;
                    break;
                }
            }

            if (matchedCat != null)
            {
                var sps = await _context.SanPhams.Where(p => p.DanhMucMa == matchedCat.Ma).Take(5).ToListAsync();
                if (sps.Any())
                {
                    var spList = string.Join("<br>", sps.Select(p => $"• <a href='/Product/Details?slug={p.Slug}' target='_blank'><strong>{p.Ten}</strong></a>: {p.GiaHienTai.ToString("N0")} đ"));
                    return Ok(new { response = $"Dưới đây là các món trong danh mục <strong>{matchedCat.Ten}</strong> ngon nhất của chúng tôi:<br>{spList}" });
                }
            }

            // Gợi ý danh mục chung
            var catList = string.Join(", ", cats.Select(c => $"<strong>{c.Ten}</strong>"));
            var bestSellers = await _context.SanPhams.OrderByDescending(p => p.SoLuongDaBan).Take(3).ToListAsync();
            var bsList = string.Join("<br>", bestSellers.Select(p => $"• <a href='/Product/Details?slug={p.Slug}' target='_blank'><strong>{p.Ten}</strong></a>: {p.GiaHienTai.ToString("N0")} đ"));
            return Ok(new { response = $"LjruiFood hiện có các danh mục: {catList}.<br><br>🔥 **Món ăn bán chạy nhất của quán:**<br>{bsList}" });
        }

        // 4. Tìm kiếm sản phẩm trực tiếp từ câu hỏi
        // Quét DB xem có sản phẩm nào trùng tên không
        var allProducts = await _context.SanPhams.ToListAsync();
        SanPham? matchedProduct = null;
        foreach (var p in allProducts)
        {
            if (userQuery.Contains(p.Ten.ToLowerInvariant()) || userQuery.Contains(p.Slug.ToLowerInvariant()))
            {
                matchedProduct = p;
                break;
            }
        }

        if (matchedProduct != null)
        {
            return Ok(new { 
                response = $"🍔 <strong>{matchedProduct.Ten}</strong>:<br>" +
                           $"- Mô tả: {matchedProduct.MoTa}<br>" +
                           $"- Khẩu phần: {matchedProduct.KhauPhan ?? "1 người"}<br>" +
                           $"- Giá bán: <strong>{matchedProduct.GiaHienTai.ToString("N0")} đ</strong> " +
                           (matchedProduct.GiaGoc > matchedProduct.GiaHienTai ? $"<del>{matchedProduct.GiaGoc?.ToString("N0")} đ</del> (-{matchedProduct.PhanTramGiam}%)" : "") + "<br>" +
                           $"- Đánh giá: {matchedProduct.SoSaoDanhGia}⭐ ({matchedProduct.SoLuotDanhGia} lượt)<br>" +
                           $"👉 <a href='/Product/Details?slug={matchedProduct.Slug}' class='btn-detail' target='_blank'>Xem chi tiết & đặt mua tại đây</a>"
            });
        }

        // 5. Hỏi đáp liên hệ / giờ mở cửa
        if (userQuery.Contains("địa chỉ") || userQuery.Contains("ở đâu") || userQuery.Contains("liên hệ") || userQuery.Contains("sđt") || userQuery.Contains("hotline") || userQuery.Contains("giờ mở cửa") || userQuery.Contains("time"))
        {
            return Ok(new { 
                response = "📍 **Thông tin liên hệ LjruiFood**:<br>" +
                           "- Địa chỉ: 123 Đường ABC, Phường X, Quận Y, TP.HCM<br>" +
                           "- Hotline đặt hàng: **0901234567**<br>" +
                           "- Email: cskh@ljruifood.com<br>" +
                           "- Giờ mở cửa: **08:00 - 22:00** từ Thứ 2 đến Chủ Nhật hàng tuần."
            });
        }

        // 6. Mặc định nếu không hiểu câu hỏi
        return Ok(new { 
            response = "Cảm ơn bạn đã nhắn tin! Tôi chưa hiểu câu hỏi của bạn lắm. Bạn có thể hỏi những câu như:<br>" +
                       "• <em>'Menu hôm nay có món gì?'</em><br>" +
                       "• <em>'Giá món Cơm chiên hải sản'</em><br>" +
                       "• <em>'Đơn hàng #3 của tôi đâu rồi?'</em><br>" +
                       "• <em>'Địa chỉ quán ở đâu?'</em>"
        });
    }

    private int? GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim != null && int.TryParse(claim.Value, out int id))
            return id;
        return null;
    }
}
