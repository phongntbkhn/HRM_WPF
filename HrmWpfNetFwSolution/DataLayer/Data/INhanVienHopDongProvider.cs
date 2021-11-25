using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace DataLayer.Data
{
    public interface INhanVienHopDongProvider
    {
        List<NHAN_VIEN_HOP_DONG> getAll();
        bool Create(NHAN_VIEN_HOP_DONG nhanVienHopDong);
        bool Edit(NHAN_VIEN_HOP_DONG nhanVienHopDong);
        NHAN_VIEN_HOP_DONG Detail(int id);
        bool Delete(int id);
        List<HopDongNhanVienVM> getAllNhanVienHopDongs();
    }
}
