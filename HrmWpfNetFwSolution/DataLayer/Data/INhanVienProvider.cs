using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace DataLayer.Data
{
    public interface INhanVienProvider
    {
        List<NHAN_VIEN> getAll();
        bool Create(NHAN_VIEN nhanVien);
        bool Edit(NHAN_VIEN nhanVien);
        NHAN_VIEN Detail(int id);
        bool Delete(int id);
        NHAN_VIEN getById(int id);
        List<NhanVienVM> getAllNhanVienVM();
    }
}
