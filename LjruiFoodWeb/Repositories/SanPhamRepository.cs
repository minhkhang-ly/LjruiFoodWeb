using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Data;
using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.Repositories;

public class SanPhamRepository : ISanPhamRepository
{
    private readonly ApplicationDbContext _context;

    public SanPhamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<SanPham> GetAllQueryable()
    {
        return _context.SanPhams.AsQueryable();
    }

    public async Task<IEnumerable<SanPham>> GetAllAsync()
    {
        return await _context.SanPhams.Include(p => p.DanhMuc).ToListAsync();
    }

    public async Task<SanPham?> GetByIdAsync(int id)
    {
        return await _context.SanPhams.Include(p => p.DanhMuc).FirstOrDefaultAsync(p => p.Ma == id);
    }

    public async Task<SanPham?> GetBySlugAsync(string slug)
    {
        return await _context.SanPhams.Include(p => p.DanhMuc).Include(p => p.DanhGias).FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task AddAsync(SanPham sanPham)
    {
        _context.SanPhams.Add(sanPham);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SanPham sanPham)
    {
        _context.SanPhams.Update(sanPham);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var sanPham = await _context.SanPhams.FindAsync(id);
        if (sanPham != null)
        {
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
        }
    }
}
