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
    
    public partial class LUONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LUONG()
        {
            this.NHAN_VIEN = new HashSet<NHAN_VIEN>();
        }
    
        public int Id { get; set; }
        public string TenDmLuong { get; set; }
        public Nullable<decimal> LuongCoBan { get; set; }
        public Nullable<decimal> HeSoLuong { get; set; }
        public Nullable<decimal> PhuCapLuong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAN_VIEN> NHAN_VIEN { get; set; }
    }
}