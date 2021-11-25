using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class KyLuatNhanVienVM
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime? NgayKyLuat { get; set; }
        public string NoiDungKyLuat { get; set; }
        public string HinhPhat { get; set; }
        public int? LoaiHinhPhat { get; set; }
        public string TenLoaiHinhPhat { get; set; }
    }
}
