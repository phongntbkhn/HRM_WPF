using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;
using ViewModel.BaoCaoThongKe;

namespace DataLayer.Data
{
    public class KyLuatNhanVienProvider : IKyLuatNhanVienProvider
    {

        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public KyLuatNhanVienProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public KyLuatNhanVienProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<KY_LUAT_NHAN_VIEN> getAll()
        {
            List<KY_LUAT_NHAN_VIEN> kyLuatNV = new List<KY_LUAT_NHAN_VIEN>();
            kyLuatNV = _context.KY_LUAT_NHAN_VIEN.ToList();
            return kyLuatNV;
        }

        public bool Create(KY_LUAT_NHAN_VIEN kyLuatNhanVien)
        {
            try
            {
                _context.KY_LUAT_NHAN_VIEN.Add(kyLuatNhanVien);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public KY_LUAT_NHAN_VIEN Detail(int id)
        {
            KY_LUAT_NHAN_VIEN kyLuatNhanvien = new KY_LUAT_NHAN_VIEN();
            try
            {
                kyLuatNhanvien = _context.KY_LUAT_NHAN_VIEN.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                kyLuatNhanvien = null;
            }
            return kyLuatNhanvien;
        }

        public bool Delete(int id)
        {
            try
            {
                var kyLuatNhanVien = _context.KY_LUAT_NHAN_VIEN.FirstOrDefault(s => s.Id == id);
                if (kyLuatNhanVien == null)
                {
                    return false;
                }
                else
                {
                    _context.KY_LUAT_NHAN_VIEN.Remove(kyLuatNhanVien);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(KY_LUAT_NHAN_VIEN kyLuatNhanVien)
        {
            try
            {
                var klNhanVien = _context.KY_LUAT_NHAN_VIEN.FirstOrDefault(s => s.Id == kyLuatNhanVien.Id);
                klNhanVien.IdNhanVien = kyLuatNhanVien.IdNhanVien;
                klNhanVien.NgayKyLuat = kyLuatNhanVien.NgayKyLuat;
                klNhanVien.NoiDungKyLuat = kyLuatNhanVien.NoiDungKyLuat;
                klNhanVien.HinhPhat = kyLuatNhanVien.HinhPhat;
                klNhanVien.LoaiHinhPhat = kyLuatNhanVien.LoaiHinhPhat;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       

        public List<KyLuatNhanVienVM> getAllKyLuatNhanVienVM()
        {
            List<KyLuatNhanVienVM> kyLuatNhanVienVMs = new List<KyLuatNhanVienVM>();

            var query = from kl in _context.KY_LUAT_NHAN_VIEN.ToList()
                        join nv in _context.NHAN_VIEN.ToList() on kl.IdNhanVien equals nv.Id
                        select new KyLuatNhanVienVM()
                        {
                            Id = kl.Id,
                            IdNhanVien = nv.Id,
                            TenNhanVien = nv.HoTen,
                            NgayKyLuat = kl.NgayKyLuat,
                            NoiDungKyLuat = kl.NoiDungKyLuat,
                            HinhPhat = kl.HinhPhat,
                            LoaiHinhPhat = kl.LoaiHinhPhat,
                            TenLoaiHinhPhat = (kl.LoaiHinhPhat == null || kl.LoaiHinhPhat == 2) ? "Phạt khác" : "Phạt tiền"
                        };
            kyLuatNhanVienVMs = query.ToList();

            return kyLuatNhanVienVMs;
        }

        public List<ThongKeKyLuatNV> getBaoCaoKyLuatNhanVien()
        {
            List<ThongKeKyLuatNV> thongKeKyLuatNVs = new List<ThongKeKyLuatNV>();

            var query = from kl in _context.KY_LUAT_NHAN_VIEN.ToList()
                        join nv in _context.NHAN_VIEN.ToList() on kl.IdNhanVien equals nv.Id
                        group nv by nv.HoTen into nvGroup
                        select new ThongKeKyLuatNV()
                        {
                            TenNhanVien = nvGroup.Key,
                            SoLanKyLuat = nvGroup.Count()
                        };
            thongKeKyLuatNVs = query.ToList();

            return thongKeKyLuatNVs;
        }
    }
}
