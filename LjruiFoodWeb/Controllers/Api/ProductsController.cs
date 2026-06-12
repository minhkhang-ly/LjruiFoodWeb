using Microsoft.AspNetCore.Mvc;
using LjruiFoodWeb.Models;
using LjruiFoodWeb.Repositories;

namespace LjruiFoodWeb.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ISanPhamRepository _sanPhamRepo;

    public ProductsController(ISanPhamRepository sanPhamRepo)
    {
        _sanPhamRepo = sanPhamRepo;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SanPham>>> GetProducts()
    {
        var products = await _sanPhamRepo.GetAllAsync();
        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SanPham>> GetProduct(int id)
    {
        var sanPham = await _sanPhamRepo.GetByIdAsync(id);

        if (sanPham == null)
        {
            return NotFound();
        }

        return Ok(sanPham);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<SanPham>> PostProduct(SanPham sanPham)
    {
        if (string.IsNullOrEmpty(sanPham.Slug))
        {
            sanPham.Slug = sanPham.Ten?.ToLower().Replace(" ", "-") ?? string.Empty;
        }
        
        sanPham.NgayTao = DateTime.Now;
        sanPham.NgayCapNhat = DateTime.Now;

        await _sanPhamRepo.AddAsync(sanPham);

        return CreatedAtAction(nameof(GetProduct), new { id = sanPham.Ma }, sanPham);
    }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, SanPham sanPham)
    {
        if (id != sanPham.Ma)
        {
            return BadRequest();
        }

        var existingProduct = await _sanPhamRepo.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        // Copy fields
        existingProduct.Ten = sanPham.Ten;
        if (!string.IsNullOrEmpty(sanPham.Slug)) existingProduct.Slug = sanPham.Slug;
        existingProduct.GiaGoc = sanPham.GiaGoc;
        existingProduct.GiaHienTai = sanPham.GiaHienTai;
        existingProduct.MoTa = sanPham.MoTa;
        existingProduct.MoTaDayDu = sanPham.MoTaDayDu;
        if (!string.IsNullOrEmpty(sanPham.HinhAnh)) existingProduct.HinhAnh = sanPham.HinhAnh;
        existingProduct.DanhMucMa = sanPham.DanhMucMa;
        existingProduct.NgayCapNhat = DateTime.Now;

        await _sanPhamRepo.UpdateAsync(existingProduct);

        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var sanPham = await _sanPhamRepo.GetByIdAsync(id);
        if (sanPham == null)
        {
            return NotFound();
        }

        await _sanPhamRepo.DeleteAsync(id);

        return NoContent();
    }
}
