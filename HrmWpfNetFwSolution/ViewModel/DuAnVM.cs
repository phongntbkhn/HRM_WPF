using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class DuAnVM
    {
        public int Id { get; set; }
        public string TenDuAn { get; set; }
        public string KhanhHang { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string MoTa { get; set; }
        public int? IdNguoiQuanLy { get; set; }
        public string TenNguoiQuanLy { get; set; }
        public string DanhSachNhanVien { get; set; }
    }
}
