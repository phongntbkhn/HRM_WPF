//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DU_AN
    {
        public int id { get; set; }
        public string TenDuAn { get; set; }
        public string KhanhHang { get; set; }
        public Nullable<System.DateTime> NgayBatDau { get; set; }
        public Nullable<System.DateTime> NgayKetThuc { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> IdNguoiQuanLy { get; set; }
        public string DanhSachNhanVien { get; set; }
    }
}