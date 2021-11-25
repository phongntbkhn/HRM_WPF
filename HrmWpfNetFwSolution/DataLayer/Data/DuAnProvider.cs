using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;

namespace DataLayer.Data
{
    public class DuAnProvider : IDuAnProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public DuAnProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public DuAnProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<DU_AN> getAll()
        {
            List<DU_AN> duAns = new List<DU_AN>();
            duAns = _context.DU_AN.ToList();
            return duAns;
        }

        public bool Create(DU_AN duAn)
        {
            try
            {
                _context.DU_AN.Add(duAn);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(DU_AN duAn)
        {
            try
            {
                var dAn = _context.DU_AN.FirstOrDefault(s => s.id == duAn.id);
                dAn.TenDuAn = duAn.TenDuAn;
                dAn.KhanhHang = duAn.KhanhHang;
                dAn.NgayBatDau = duAn.NgayBatDau;
                dAn.NgayKetThuc = duAn.NgayKetThuc;
                dAn.MoTa = duAn.MoTa;
                dAn.IdNguoiQuanLy = duAn.IdNguoiQuanLy;
                dAn.DanhSachNhanVien = duAn.DanhSachNhanVien;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var duAn = _context.DU_AN.FirstOrDefault(s => s.id == id);
                if (duAn == null)
                {
                    return false;
                }
                else
                {
                    _context.DU_AN.Remove(duAn);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DU_AN Detail(int id)
        {
            DU_AN duAn = new DU_AN();
            try
            {
                 duAn = _context.DU_AN.FirstOrDefault(s => s.id == id);
            }
            catch (Exception ex)
            {
                duAn = null;
            }
            return duAn;
        }

        public List<DuAnVM> getAllDuAnVM()
        {
            List<DuAnVM> duAnVMs = new List<DuAnVM>();
            var query = from da in _context.DU_AN.ToList()
                        join nv in _context.NHAN_VIEN.ToList() on da.IdNguoiQuanLy equals nv.Id
                        select new DuAnVM()
                        {
                            Id = da.id,
                            TenDuAn = da.TenDuAn,
                            KhanhHang = da.KhanhHang,
                            NgayBatDau = da.NgayBatDau,
                            NgayKetThuc = da.NgayKetThuc,
                            MoTa = da.MoTa,
                            IdNguoiQuanLy = da.IdNguoiQuanLy,
                            TenNguoiQuanLy = nv.HoTen,
                            DanhSachNhanVien = da.DanhSachNhanVien
                        };
            duAnVMs = query.ToList();

            return duAnVMs;
        }
    }
}
