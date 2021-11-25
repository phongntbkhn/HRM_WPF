using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace DataLayer.Data
{
    public class TaiKhoanProvider : ITaiKhoanProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public TaiKhoanProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public TaiKhoanProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
            _context = new HumanResourceManagementDbContext();
        }

        public List<TAI_KHOAN> getAll()
        {
            List<TAI_KHOAN> taiKhoans = new List<TAI_KHOAN>();
            taiKhoans = _context.TAI_KHOAN.ToList();
            return taiKhoans;
        }

        public List<TaiKhoanVM> getAllTaiKhoanMV()
        {
            var queryGetListTaiKhoan = from tk in _context.TAI_KHOAN.ToList()
                                       join q in _context.QUYENs.ToList() on tk.IdQuyen equals q.Id
                                       join nv in _context.NHAN_VIEN.ToList() on tk.IdNhanVien equals nv.Id
                                       select new TaiKhoanVM()
                                       {
                                           Id = tk.Id,
                                           TenDangNhap = tk.TenDangNhap,
                                           TenHienThi = tk.TenHienThi,
                                           TenQuyen = q.TEN_QUYEN,
                                           TenNhanVien = nv.HoTen
                                       };
            List<TaiKhoanVM> taiKhoanVMs = queryGetListTaiKhoan.ToList();

            return taiKhoanVMs;
        }

        public bool Create(TAI_KHOAN taiKhoan)
        {
            try
            {
                _context.TAI_KHOAN.Add(taiKhoan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(TAI_KHOAN taiKhoan)
        {
            try
            {
                var tKhoan = _context.TAI_KHOAN.FirstOrDefault(s => s.Id == taiKhoan.Id);
                tKhoan.TenDangNhap = taiKhoan.TenDangNhap;
                tKhoan.MatKhau = taiKhoan.MatKhau;
                tKhoan.TenHienThi = taiKhoan.TenHienThi;
                tKhoan.IdQuyen = taiKhoan.IdQuyen;
                tKhoan.IdNhanVien = taiKhoan.IdNhanVien;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TAI_KHOAN Detail(int id)
        {
            TAI_KHOAN tk = new TAI_KHOAN();
            try
            {
                var taiKhoan = _context.TAI_KHOAN.FirstOrDefault(s => s.Id == id);
                tk = taiKhoan;
            }
            catch (Exception ex)
            {
                tk = null;
            }
            return tk;
        }

        public bool Delete(int id)
        {
            try
            {
                var taiKhoan = _context.TAI_KHOAN.FirstOrDefault(s => s.Id == id);
                if (taiKhoan == null)
                {
                    return false;
                }
                else
                {
                    _context.TAI_KHOAN.Remove(taiKhoan);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TAI_KHOAN checkLogin(string ten_dang_nhap, string mat_khau, out string message)
        {
            TAI_KHOAN taiKhoanNd = new TAI_KHOAN();
            try
            {
                var taiKhoan = _context.TAI_KHOAN.FirstOrDefault(s => s.TenDangNhap == ten_dang_nhap && s.MatKhau == mat_khau);
                if (taiKhoan == null)
                {
                    taiKhoanNd = null;
                    message = "Tài khoản đăng nhập không tồn tại!";
                }
                else
                {
                    taiKhoanNd = taiKhoan;
                    message = "Đăng nhập thành công!";
                }
                return taiKhoanNd;
            }
            catch (Exception ex)
            {
                taiKhoanNd = null;
                message = ex.Message.ToString();
                return taiKhoanNd;
            }
        }
    }
}
