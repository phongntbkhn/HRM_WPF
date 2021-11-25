using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace DataLayer.Data
{
    public interface ITaiKhoanProvider
    {
        List<TAI_KHOAN> getAll();
        List<TaiKhoanVM> getAllTaiKhoanMV();
        bool Create(TAI_KHOAN taiKhoan);
        bool Edit(TAI_KHOAN taiKhoan);
        TAI_KHOAN Detail(int id);
        bool Delete(int id);
        TAI_KHOAN checkLogin(string ten_dang_nhap, string mat_khau, out string message);
    }
}
