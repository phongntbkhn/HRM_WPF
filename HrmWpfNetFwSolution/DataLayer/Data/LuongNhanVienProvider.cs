using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;


namespace DataLayer.Data
{
    public class LuongNhanVienProvider : ILuongNhanVienProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public LuongNhanVienProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public LuongNhanVienProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<LUONG_NHAN_VIEN> getAll()
        {
            List<LUONG_NHAN_VIEN> luongNhanViens = new List<LUONG_NHAN_VIEN>();
            luongNhanViens = _context.LUONG_NHAN_VIEN.ToList();
            return luongNhanViens;
        }

        public bool Create(LUONG_NHAN_VIEN luongNv)
        {
            try
            {
                _context.LUONG_NHAN_VIEN.Add(luongNv);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(LUONG_NHAN_VIEN luongNv)
        {
            try
            {
                var lnv = _context.LUONG_NHAN_VIEN.FirstOrDefault(s => s.id == luongNv.id);
                lnv.IdNhanVien = luongNv.IdNhanVien;
                lnv.ThangLuong = luongNv.ThangLuong;
                lnv.NamLuong = luongNv.NamLuong;
                lnv.TienLuong = luongNv.TienLuong;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LUONG_NHAN_VIEN Detail(int id)
        {
            LUONG_NHAN_VIEN luongNhanVien = new LUONG_NHAN_VIEN();
            try
            {
                luongNhanVien = _context.LUONG_NHAN_VIEN.FirstOrDefault(s => s.id == id);
            }
            catch (Exception ex)
            {
                luongNhanVien = null;
            }
            return luongNhanVien;
        }

        public bool Delete(int id)
        {
            try
            {
                var luongNhanVien = _context.LUONG_NHAN_VIEN.FirstOrDefault(s => s.id == id);
                if (luongNhanVien == null)
                {
                    return false;
                }
                else
                {
                    _context.LUONG_NHAN_VIEN.Remove(luongNhanVien);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<LuongNhanVienVM> getDataLuongNvDaInsert(int month, int year)
        {
            List<LuongNhanVienVM> luongNvVM = new List<LuongNhanVienVM>();
            var query = from nv in _context.NHAN_VIEN.ToList()
                        join lnv in _context.LUONG_NHAN_VIEN.ToList() on nv.Id equals lnv.IdNhanVien
                        join l in _context.LUONGs.ToList() on nv.IdLuong equals l.Id
                        select new LuongNhanVienVM()
                        {
                            Id = lnv.id,
                            IdNhanVien = lnv.IdNhanVien,
                            TenNhanVien = nv.HoTen,
                            TenDmLuong = l.TenDmLuong,
                            LuongCoBan = l.LuongCoBan,
                            HeSoLuong = l.HeSoLuong,
                            PhuCapLuong = l.PhuCapLuong,
                            SoDienThoai = nv.SoDienThoai,
                            ThangLuong = lnv.ThangLuong,
                            NamLuong = lnv.NamLuong,
                            TienLuongNV = lnv.TienLuong
                        };
            luongNvVM = query.ToList();
            luongNvVM = luongNvVM.Where(s => s.ThangLuong == month && s.NamLuong == year).ToList();
            return luongNvVM;
        }

        public bool InsertEmptyCongSo(List<LUONG_NHAN_VIEN> luongNhanViens)
        {
            try
            {
                foreach (var item in luongNhanViens)
                {
                    _context.LUONG_NHAN_VIEN.Add(item);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public decimal getTienLuongNhanVienByThang(int? idNhanVien, DateTime startNgay, DateTime endNgay , decimal thoiGianLamViecChuan)
        {
            decimal tienLuong = 0;
            decimal so_gio_lam = 0;
            // Lấy tổng thời gian làm việc trong tháng
            List<CONG_SO> congSos = new List<CONG_SO>();
            var query = from cs in _context.CONG_SO.ToList()
                        where cs.IdNhanVien == idNhanVien && cs.NgayChamCong >= startNgay && cs.NgayChamCong <= endNgay
                        select cs;
            congSos = query.ToList();
            foreach (var item in congSos)
            {
                if (item.State == 1)
                {
                    // Ngày làm việc bình thường là 8 giờ
                    so_gio_lam = so_gio_lam + 8;
                    if (item.OtTime > 0)
                    {
                        // Thời gian tăng ca mặc định = 2 lần thời gian làm việc bình thường
                        so_gio_lam = so_gio_lam + (Convert.ToInt32(item.OtTime) * 2);
                    }
                }
            }

            // Lấy danh mục Lương của nhân viên
            var query2 = from nv in _context.NHAN_VIEN.ToList()
                         join l in _context.LUONGs.ToList() on nv.IdLuong equals l.Id
                        where nv.Id == idNhanVien
                         select l;
            LUONG dmLuong = query2.FirstOrDefault();


            tienLuong = (so_gio_lam / thoiGianLamViecChuan) * Convert.ToInt32(dmLuong.LuongCoBan) * Convert.ToDecimal(dmLuong.HeSoLuong) + Convert.ToInt32(dmLuong.PhuCapLuong);


            // Tiền thưởng nhân viên trong tháng
            var queryThuong = from nv in _context.NHAN_VIEN.ToList()
                              join t in _context.KHEN_THUONG_NHAN_VIEN on nv.Id equals t.IdNhanVien
                              where nv.Id == idNhanVien && t.LoaiPhanThuong == 1 && t.NgayKhenThuong >= startNgay && t.NgayKhenThuong <= endNgay
                              select  t.PhanThuong;

            decimal tienThuongNvByThang = 0;
            foreach (var item in queryThuong.ToList())
            {
                tienThuongNvByThang += Convert.ToDecimal(item);
            }

            // Tiền thưởng nhân viên trong tháng
            var queryPhat = from nv in _context.NHAN_VIEN.ToList()
                              join kl in _context.KY_LUAT_NHAN_VIEN on nv.Id equals kl.IdNhanVien
                              where nv.Id == idNhanVien && kl.LoaiHinhPhat == 1 && kl.NgayKyLuat >= startNgay && kl.NgayKyLuat <= endNgay
                              select kl.HinhPhat;

            decimal tienPhatNvByThang = 0;
            foreach (var item in queryPhat.ToList())
            {
                tienPhatNvByThang += Convert.ToDecimal(item);
            }

            tienLuong = tienLuong + tienThuongNvByThang - tienPhatNvByThang;

            tienLuong = Math.Round(tienLuong, 0);
            return tienLuong;
        }
    }
}
