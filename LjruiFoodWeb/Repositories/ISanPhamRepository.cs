using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.Repositories;

public interface ISanPhamRepository
{
    IQueryable<SanPham> GetAllQueryable();
    Task<IEnumerable<SanPham>> GetAllAsync();
    Task<SanPham?> GetByIdAsync(int id);
    Task<SanPham?> GetBySlugAsync(string slug);
    Task AddAsync(SanPham sanPham);
    Task UpdateAsync(SanPham sanPham);
    Task DeleteAsync(int id);
}
