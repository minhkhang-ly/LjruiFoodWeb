using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Data;
using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _context.DonHangs
            .OrderByDescending(o => o.NgayDatHang)
            .ToListAsync();
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _context.DonHangs
            .Include(o => o.ChiTietDonHangs)
            .ThenInclude(c => c.SanPham)
            .FirstOrDefaultAsync(o => o.Ma == id);

        if (order == null)
            return NotFound();

        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string trangThai)
    {
        var order = await _context.DonHangs.FindAsync(id);
        if (order == null)
            return NotFound();

        order.TrangThai = trangThai;
        order.NgayCapNhat = DateTime.Now;

        _context.DonHangs.Update(order);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Cập nhật trạng thái đơn hàng #{id} thành '{trangThai}' thành công!";
        return RedirectToAction(nameof(Details), new { id = order.Ma });
    }
}
