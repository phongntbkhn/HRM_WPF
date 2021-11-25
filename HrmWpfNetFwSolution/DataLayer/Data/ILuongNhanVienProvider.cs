using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace DataLayer.Data
{
    public interface ILuongNhanVienProvider
    {
        List<LUONG_NHAN_VIEN> getAll();
        bool Create(LUONG_NHAN_VIEN luongNv);
        bool Edit(LUONG_NHAN_VIEN luongNv);
        LUONG_NHAN_VIEN Detail(int id);
        bool Delete(int id);
        List<LuongNhanVienVM> getDataLuongNvDaInsert(int month, int year);
        bool InsertEmptyCongSo(List<LUONG_NHAN_VIEN> luongNhanViens);
        decimal getTienLuongNhanVienByThang(int? idNhanVien, DateTime startNgay, DateTime endNgay, decimal thoiGianLamViecChuan);
    }
}
