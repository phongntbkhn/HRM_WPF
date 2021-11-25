using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class LuongNvVM
    {
        public int IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public int IdLuoong { get; set; }
        public string TenLuong { get; set; }
        public decimal? SoNgayLamViecThang { get; set; }
        public decimal? SoNgayLamViecNhanVien { get; set; }
        public decimal? TienLuong { get; set; }
    }
}
