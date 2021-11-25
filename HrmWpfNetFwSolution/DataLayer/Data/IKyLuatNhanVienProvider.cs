using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;
using ViewModel.BaoCaoThongKe;

namespace DataLayer.Data
{
    public interface IKyLuatNhanVienProvider
    {
        List<KY_LUAT_NHAN_VIEN> getAll();
        bool Create(KY_LUAT_NHAN_VIEN kyLuatNhanVien);
        bool Edit(KY_LUAT_NHAN_VIEN kyLuatNhanVien);
        KY_LUAT_NHAN_VIEN Detail(int id);
        bool Delete(int id);
        List<KyLuatNhanVienVM> getAllKyLuatNhanVienVM();
        List<ThongKeKyLuatNV> getBaoCaoKyLuatNhanVien();
    }
}
