using System;
using System.Collections.Generic;

namespace LjruiFoodWeb.Models;

public partial class DanhGium
{
    public int Ma { get; set; }

    public int SanPhamMa { get; set; }

    public int? NguoiDungMa { get; set; }

    public string HoTen { get; set; } = null!;

    public int SoSao { get; set; }

    public string? BinhLuan { get; set; }

    public DateTime? NgayDanhGia { get; set; }

    public virtual NguoiDung? NguoiDungMaNavigation { get; set; }

    public virtual SanPham SanPhamMaNavigation { get; set; } = null!;
}
