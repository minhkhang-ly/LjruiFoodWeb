using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Data;
using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.Repositories;

public class DanhMucRepository : IDanhMucRepository
{
    private readonly ApplicationDbContext _context;

    public DanhMucRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DanhMuc>> GetAllAsync()
    {
        return await _context.DanhMucs.ToListAsync();
    }

    public async Task<DanhMuc?> GetByIdAsync(int id)
    {
        return await _context.DanhMucs.FindAsync(id);
    }

    public async Task AddAsync(DanhMuc danhMuc)
    {
        _context.DanhMucs.Add(danhMuc);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DanhMuc danhMuc)
    {
        _context.DanhMucs.Update(danhMuc);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var danhMuc = await _context.DanhMucs.FindAsync(id);
        if (danhMuc != null)
        {
            _context.DanhMucs.Remove(danhMuc);
            await _context.SaveChangesAsync();
        }
    }
}
