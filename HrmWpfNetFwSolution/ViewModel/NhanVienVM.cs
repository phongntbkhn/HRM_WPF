using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class NhanVienVM
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public string GioiTinh { get; set; }
        public int? IdDanToc { get; set; }
        public string TenDanToc { get; set; }
        public string SoDienThoai { get; set; }
        public int? IdPhongBan { get; set; }
        public string PhongBan { get; set; }
        public int? IdChucVu { get; set; }
        public string ChucVu { get; set; }
        public int? IdTrinhDoHocVan { get; set; }
        public string TrinhDoHocVan { get; set; }
        public int? IdDmLuong { get; set; }
        public string DmLuong { get; set; }
    }
}
