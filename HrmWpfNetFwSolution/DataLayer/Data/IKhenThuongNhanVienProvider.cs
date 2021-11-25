using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;
using ViewModel.BaoCaoThongKe;

namespace DataLayer.Data
{
    public interface IKhenThuongNhanVienProvider
    {
        List<KHEN_THUONG_NHAN_VIEN> getAll();
        bool Create(KHEN_THUONG_NHAN_VIEN khenThuongNhanVien);
        bool Edit(KHEN_THUONG_NHAN_VIEN khenThuongNhanVien);
        KHEN_THUONG_NHAN_VIEN Detail(int id);
        bool Delete(int id);
        List<KhenThuongNhanVienVM> getAllKhenThuongNhanVienVM();
        List<ThongKeKhenThuongNV> getBaoCaoKhenThuongNhanVien();
    }
}
