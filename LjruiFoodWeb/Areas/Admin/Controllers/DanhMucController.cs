using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LjruiFoodWeb.Models;
using LjruiFoodWeb.Repositories;

namespace LjruiFoodWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DanhMucController : Controller
{
    private readonly IDanhMucRepository _danhMucRepository;

    public DanhMucController(IDanhMucRepository danhMucRepository)
    {
        _danhMucRepository = danhMucRepository;
    }

    public async Task<IActionResult> Index()
    {
        var danhMucs = await _danhMucRepository.GetAllAsync();
        return View(danhMucs);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DanhMuc danhMuc)
    {
        if (ModelState.IsValid)
        {
            await _danhMucRepository.AddAsync(danhMuc);
            TempData["SuccessMessage"] = "Thêm danh mục thành công!";
            return RedirectToAction(nameof(Index));
        }
        return View(danhMuc);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var danhMuc = await _danhMucRepository.GetByIdAsync(id);
        if (danhMuc == null)
            return NotFound();

        return View(danhMuc);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DanhMuc danhMuc)
    {
        if (id != danhMuc.Ma)
            return NotFound();

        if (ModelState.IsValid)
        {
            await _danhMucRepository.UpdateAsync(danhMuc);
            TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
            return RedirectToAction(nameof(Index));
        }
        return View(danhMuc);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var danhMuc = await _danhMucRepository.GetByIdAsync(id);
        if (danhMuc == null)
            return NotFound();

        return View(danhMuc);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _danhMucRepository.DeleteAsync(id);
        TempData["SuccessMessage"] = "Xóa danh mục thành công!";
        return RedirectToAction(nameof(Index));
    }
}
