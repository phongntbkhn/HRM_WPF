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
    
    public partial class KY_LUAT_NHAN_VIEN
    {
        public int Id { get; set; }
        public Nullable<int> IdNhanVien { get; set; }
        public Nullable<System.DateTime> NgayKyLuat { get; set; }
        public string NoiDungKyLuat { get; set; }
        public string HinhPhat { get; set; }
        public Nullable<int> LoaiHinhPhat { get; set; }
    
        public virtual NHAN_VIEN NHAN_VIEN { get; set; }
    }
}