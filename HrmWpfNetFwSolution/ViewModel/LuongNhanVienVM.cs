using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class LuongNhanVienVM
    {
        public int Id { get; set; }
        public int? IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
        public string TenDmLuong { get; set; }
        public decimal? LuongCoBan { get; set; }
        public decimal? HeSoLuong { get; set; }
        public decimal? PhuCapLuong { get; set; }
        public int? ThangLuong { get; set; }
        public int? NamLuong { get; set; }
        public decimal? TienLuongNV { get; set; }
    }
}
