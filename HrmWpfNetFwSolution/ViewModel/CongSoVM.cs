using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class CongSoVM
    {
        public int? Id { get; set; }
        public int IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgayChamCong { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public int? State { get; set; }
        public int? IsCoPhep { get; set; }
        public decimal? OtTime { get; set; }
        public string Note { get; set; }
    }
}
