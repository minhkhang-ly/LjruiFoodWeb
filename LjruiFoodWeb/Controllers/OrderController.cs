using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Data;
using LjruiFoodWeb.Models;
using LjruiFoodWeb.ViewModels;

namespace LjruiFoodWeb.Controllers;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
    private const string CartSessionKey = "ljrui_gio_hang";

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();
        if (userId == null)
            return RedirectToAction("DangNhap", "Account");

        var orders = await _context.DonHangs
            .Include(d => d.ChiTietDonHangs)
                .ThenInclude(c => c.SanPham)
            .Where(d => d.NguoiDungMa == userId)
            .OrderByDescending(d => d.NgayDatHang)
            .ToListAsync();

        var vms = orders.Select(d => d.ToVM()).ToList();
        return View(vms);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var userId = GetUserId();
        if (userId == null)
            return RedirectToAction("DangNhap", "Account");

        var order = await _context.DonHangs
            .Include(d => d.ChiTietDonHangs)
                .ThenInclude(c => c.SanPham)
            .FirstOrDefaultAsync(d => d.Ma == id && d.NguoiDungMa == userId);

        if (order == null)
            return RedirectToAction(nameof(Index));

        return View(order.ToVM());
    }

    private int? GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim != null && int.TryParse(claim.Value, out int id))
            return id;
        return null;
    }
}
