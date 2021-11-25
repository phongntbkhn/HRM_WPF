using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class KhenThuongNhanVienVM
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime? NgayKhenThuong { get; set; }
        public string NoiDungKhenThuong { get; set; }
        public string PhanThuong { get; set; }
        public int? LoaiPhanThuong { get; set; }
        public string TenLoaiPhanThuong { get; set; }
    }
}
