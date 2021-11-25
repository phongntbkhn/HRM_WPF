using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class HopDongNhanVienVM
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public string HoTen { get; set; }
        public int IdLoaiHopDong { get; set; }
        public string TenLoaiHopDong { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
}
