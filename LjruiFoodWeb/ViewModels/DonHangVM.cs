using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.ViewModels;

public class DonHangVM
{
    public int Ma { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string DiaChiGiaoHang { get; set; } = string.Empty;
    public string? GhiChu { get; set; }
    public decimal TongTien { get; set; }
    public string TrangThai { get; set; } = string.Empty;
    public DateTime NgayDatHang { get; set; }
    public DateTime NgayCapNhat { get; set; }
    public List<ChiTietDonHangVM> ChiTiet { get; set; } = [];
}

public class ChiTietDonHangVM
{
    public int Ma { get; set; }
    public string TenSanPham { get; set; } = string.Empty;
    public string? HinhAnh { get; set; }
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien { get; set; }
}

public static class DonHangMapper
{
    public static DonHangVM ToVM(this DonHang d)
    {
        return new DonHangVM
        {
            Ma = d.Ma,
            HoTen = d.HoTen,
            SoDienThoai = d.SoDienThoai,
            Email = d.Email,
            DiaChiGiaoHang = d.DiaChiGiaoHang,
            GhiChu = d.GhiChu,
            TongTien = d.TongTien,
            TrangThai = d.TrangThai,
            NgayDatHang = d.NgayDatHang,
            NgayCapNhat = d.NgayCapNhat,
            ChiTiet = d.ChiTietDonHangs.Select(c => c.ToVM()).ToList()
        };
    }

    public static ChiTietDonHangVM ToVM(this ChiTietDonHang c)
    {
        return new ChiTietDonHangVM
        {
            Ma = c.Ma,
            TenSanPham = c.SanPham?.Ten ?? "Không rõ",
            HinhAnh = c.SanPham?.HinhAnh,
            SoLuong = c.SoLuong,
            DonGia = c.DonGia,
            ThanhTien = c.ThanhTien
        };
    }
}
