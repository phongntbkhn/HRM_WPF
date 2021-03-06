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
    
    public partial class NHAN_VIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHAN_VIEN()
        {
            this.CONG_SO = new HashSet<CONG_SO>();
            this.KHEN_THUONG_NHAN_VIEN = new HashSet<KHEN_THUONG_NHAN_VIEN>();
            this.KY_LUAT_NHAN_VIEN = new HashSet<KY_LUAT_NHAN_VIEN>();
            this.NHAN_VIEN_HOP_DONG = new HashSet<NHAN_VIEN_HOP_DONG>();
            this.TAI_KHOAN = new HashSet<TAI_KHOAN>();
        }
    
        public int Id { get; set; }
        public string HoTen { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public Nullable<int> GioiTinh { get; set; }
        public Nullable<int> IdDanToc { get; set; }
        public string SoDienThoai { get; set; }
        public Nullable<int> IdPhongBan { get; set; }
        public Nullable<int> IdChucVu { get; set; }
        public Nullable<int> IdTrinhDoHocVan { get; set; }
        public Nullable<int> IdLuong { get; set; }
    
        public virtual CHUC_VU CHUC_VU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONG_SO> CONG_SO { get; set; }
        public virtual DAN_TOC DAN_TOC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHEN_THUONG_NHAN_VIEN> KHEN_THUONG_NHAN_VIEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KY_LUAT_NHAN_VIEN> KY_LUAT_NHAN_VIEN { get; set; }
        public virtual LUONG LUONG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAN_VIEN_HOP_DONG> NHAN_VIEN_HOP_DONG { get; set; }
        public virtual PHONG_BAN PHONG_BAN { get; set; }
        public virtual TRINH_DO_HOC_VAN TRINH_DO_HOC_VAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAI_KHOAN> TAI_KHOAN { get; set; }
    }
}
