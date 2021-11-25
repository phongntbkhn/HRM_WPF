using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;
using ViewModel.BaoCaoThongKe;

namespace DataLayer.Data
{
    public class KhenThuongNhanVienProvider : IKhenThuongNhanVienProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public KhenThuongNhanVienProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public KhenThuongNhanVienProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<KHEN_THUONG_NHAN_VIEN> getAll()
        {
            List<KHEN_THUONG_NHAN_VIEN> khenThuongNV = new List<KHEN_THUONG_NHAN_VIEN>();
            khenThuongNV = _context.KHEN_THUONG_NHAN_VIEN.ToList();
            return khenThuongNV;
        }

        public bool Create(KHEN_THUONG_NHAN_VIEN khenThuongNhanVien)
        {
            try
            {
                _context.KHEN_THUONG_NHAN_VIEN.Add(khenThuongNhanVien);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(KHEN_THUONG_NHAN_VIEN khenThuongNhanVien)
        {
            try
            {
                var ktNhanVien = _context.KHEN_THUONG_NHAN_VIEN.FirstOrDefault(s => s.Id == khenThuongNhanVien.Id);
                ktNhanVien.IdNhanVien = khenThuongNhanVien.IdNhanVien;
                ktNhanVien.NgayKhenThuong = khenThuongNhanVien.NgayKhenThuong;
                ktNhanVien.NoiDungKhenThuong = khenThuongNhanVien.NoiDungKhenThuong;
                ktNhanVien.PhanThuong = khenThuongNhanVien.PhanThuong;
                ktNhanVien.LoaiPhanThuong = khenThuongNhanVien.LoaiPhanThuong;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public KHEN_THUONG_NHAN_VIEN Detail(int id)
        {
            KHEN_THUONG_NHAN_VIEN khenThuongNV = new KHEN_THUONG_NHAN_VIEN();
            try
            {
                khenThuongNV = _context.KHEN_THUONG_NHAN_VIEN.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                khenThuongNV = null;
            }
            return khenThuongNV;
        }

        public bool Delete(int id)
        {
            try
            {
                var khenThuongNV = _context.KHEN_THUONG_NHAN_VIEN.FirstOrDefault(s => s.Id == id);
                if (khenThuongNV == null)
                {
                    return false;
                }
                else
                {
                    _context.KHEN_THUONG_NHAN_VIEN.Remove(khenThuongNV);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<KhenThuongNhanVienVM> getAllKhenThuongNhanVienVM()
        {
            List<KhenThuongNhanVienVM> khenThuongNhanVienVMs = new List<KhenThuongNhanVienVM>();

            var query = from kt in _context.KHEN_THUONG_NHAN_VIEN.ToList()
                        join nv in _context.NHAN_VIEN.ToList() on kt.IdNhanVien equals nv.Id
                        select new KhenThuongNhanVienVM()
                        {
                            Id = kt.Id,
                            IdNhanVien = nv.Id,
                            TenNhanVien = nv.HoTen,
                            NgayKhenThuong = kt.NgayKhenThuong,
                            NoiDungKhenThuong = kt.NoiDungKhenThuong,
                            PhanThuong = kt.PhanThuong,
                            LoaiPhanThuong = kt.LoaiPhanThuong,
                            TenLoaiPhanThuong = (kt.LoaiPhanThuong == null || kt.LoaiPhanThuong == 2)? "Thưởng khác" : "Thưởng tiền",
                        };
            khenThuongNhanVienVMs = query.ToList();

            return khenThuongNhanVienVMs;
        }

        public List<ThongKeKhenThuongNV> getBaoCaoKhenThuongNhanVien()
        {
            List<ThongKeKhenThuongNV> thongKeKhenThuongs = new List<ThongKeKhenThuongNV>();

            var query = from kt in _context.KHEN_THUONG_NHAN_VIEN.ToList()
                        join nv in _context.NHAN_VIEN.ToList() on kt.IdNhanVien equals nv.Id
                        group nv by nv.HoTen into nvGroup
                        select new ThongKeKhenThuongNV()
                        {
                            TenNhanVien = nvGroup.Key,
                            SoLanKhenThuong = nvGroup.Count()
                        };
            thongKeKhenThuongs = query.ToList();

            return thongKeKhenThuongs;
        }
    }
}
