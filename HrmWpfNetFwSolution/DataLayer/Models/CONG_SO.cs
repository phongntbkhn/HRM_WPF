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
    
    public partial class CONG_SO
    {
        public int Id { get; set; }
        public Nullable<int> IdNhanVien { get; set; }
        public Nullable<System.DateTime> NgayChamCong { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> IsCoPhep { get; set; }
        public Nullable<decimal> OtTime { get; set; }
        public string Note { get; set; }
    
        public virtual NHAN_VIEN NHAN_VIEN { get; set; }
    }
}
