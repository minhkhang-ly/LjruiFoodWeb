using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using LjruiFoodWeb.Models;
using LjruiFoodWeb.Repositories;
using LjruiFoodWeb.ViewModels;

namespace LjruiFoodWeb.Areas.Admin.Controllers;

[Area("Admin")]
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

    public async Task<IActionResult> Index()
    {
        var sanPhams = await _sanPhamRepo.GetAllAsync();
        return View(sanPhams);
    }

    public IActionResult ApiManage()
    {
        return View();
    }

    public async Task<IActionResult> Add()
    {
        var danhMucs = await _danhMucRepo.GetAllAsync();
        ViewBag.DanhMucs = new SelectList(danhMucs, "Ma", "Ten");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(SanPhamViewModel model)
    {
        if (ModelState.IsValid)
        {
            string hinhAnhFileName = "";
            if (model.HinhAnhUpload != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                hinhAnhFileName = Guid.NewGuid().ToString() + "_" + model.HinhAnhUpload.FileName;
                string filePath = Path.Combine(uploadDir, hinhAnhFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.HinhAnhUpload.CopyToAsync(fileStream);
                }
            }

            var sanPham = new SanPham
            {
                Ten = model.Ten,
                Slug = model.Ten.ToLower().Replace(" ", "-"),
                GiaHienTai = model.GiaHienTai,
                GiaGoc = model.GiaGoc,
                MoTa = model.MoTa,
                SoLuongTon = model.SoLuongTon,
                DanhMucMa = model.DanhMucMa,
                HinhAnh = hinhAnhFileName,
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now
            };

            await _sanPhamRepo.AddAsync(sanPham);
            return RedirectToAction(nameof(Index));
        }

        var danhMucs = await _danhMucRepo.GetAllAsync();
        ViewBag.DanhMucs = new SelectList(danhMucs, "Ma", "Ten", model.DanhMucMa);
        return View(model);
    }

    public async Task<IActionResult> Update(int id)
    {
        var sanPham = await _sanPhamRepo.GetByIdAsync(id);
        if (sanPham == null)
            return NotFound();

        var model = new SanPhamViewModel
        {
            Ma = sanPham.Ma,
            Ten = sanPham.Ten,
            GiaHienTai = sanPham.GiaHienTai,
            GiaGoc = sanPham.GiaGoc,
            MoTa = sanPham.MoTa,
            SoLuongTon = sanPham.SoLuongTon,
            DanhMucMa = sanPham.DanhMucMa,
            HinhAnhHienTai = sanPham.HinhAnh
        };

        var danhMucs = await _danhMucRepo.GetAllAsync();
        ViewBag.DanhMucs = new SelectList(danhMucs, "Ma", "Ten", sanPham.DanhMucMa);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, SanPhamViewModel model)
    {
        if (id != model.Ma)
            return NotFound();

        if (ModelState.IsValid)
        {
            var sanPham = await _sanPhamRepo.GetByIdAsync(id);
            if (sanPham == null)
                return NotFound();

            if (model.HinhAnhUpload != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string hinhAnhFileName = Guid.NewGuid().ToString() + "_" + model.HinhAnhUpload.FileName;
                string filePath = Path.Combine(uploadDir, hinhAnhFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.HinhAnhUpload.CopyToAsync(fileStream);
                }

                sanPham.HinhAnh = hinhAnhFileName;
            }

            sanPham.Ten = model.Ten;
            sanPham.GiaHienTai = model.GiaHienTai;
            sanPham.GiaGoc = model.GiaGoc;
            sanPham.MoTa = model.MoTa;
            sanPham.SoLuongTon = model.SoLuongTon;
            sanPham.DanhMucMa = model.DanhMucMa;
            sanPham.NgayCapNhat = DateTime.Now;

            await _sanPhamRepo.UpdateAsync(sanPham);
            return RedirectToAction(nameof(Index));
        }

        var danhMucs = await _danhMucRepo.GetAllAsync();
        ViewBag.DanhMucs = new SelectList(danhMucs, "Ma", "Ten", model.DanhMucMa);
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var sanPham = await _sanPhamRepo.GetByIdAsync(id);
        if (sanPham == null)
            return NotFound();

        return View(sanPham);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _sanPhamRepo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
