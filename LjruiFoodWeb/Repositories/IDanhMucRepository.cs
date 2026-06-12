using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.Repositories;

public interface IDanhMucRepository
{
    Task<IEnumerable<DanhMuc>> GetAllAsync();
    Task<DanhMuc?> GetByIdAsync(int id);
    Task AddAsync(DanhMuc danhMuc);
    Task UpdateAsync(DanhMuc danhMuc);
    Task DeleteAsync(int id);
}
