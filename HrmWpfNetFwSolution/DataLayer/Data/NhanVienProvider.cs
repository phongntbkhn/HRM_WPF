using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;

namespace DataLayer.Data
{
    public class NhanVienProvider : INhanVienProvider
    {

        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public NhanVienProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public NhanVienProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<NHAN_VIEN> getAll()
        {
            List<NHAN_VIEN> nhanViens = new List<NHAN_VIEN>();
            nhanViens = _context.NHAN_VIEN.ToList();
            return nhanViens;
        }

        public bool Create(NHAN_VIEN nhanVien)
        {
            try
            {
                _context.NHAN_VIEN.Add(nhanVien);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(NHAN_VIEN nhanVien)
        {
            try
            {
                var nv = _context.NHAN_VIEN.FirstOrDefault(s => s.Id == nhanVien.Id);
                nv.HoTen = nhanVien.HoTen;
                nv.NgaySinh = nhanVien.NgaySinh;
                nv.QueQuan = nhanVien.QueQuan;
                nv.GioiTinh = nhanVien.GioiTinh;
                nv.IdDanToc = nhanVien.IdDanToc;
                nv.SoDienThoai = nhanVien.SoDienThoai;
                nv.IdPhongBan = nhanVien.IdPhongBan;
                nv.IdChucVu = nhanVien.IdChucVu;
                nv.IdTrinhDoHocVan = nhanVien.IdTrinhDoHocVan;
                nv.IdLuong = nhanVien.IdLuong;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public NHAN_VIEN Detail(int id)
        {
            NHAN_VIEN nhanVien = new NHAN_VIEN();
            try
            {
                var nv = _context.NHAN_VIEN.FirstOrDefault(s => s.Id == id);
                nhanVien = nv;
            }
            catch (Exception ex)
            {
                nhanVien = null;
            }
            return nhanVien;
        }

        public bool Delete(int id)
        {
            try
            {
                var nhanVien = _context.NHAN_VIEN.FirstOrDefault(s => s.Id == id);
                if (nhanVien == null)
                {
                    return false;
                }
                else
                {
                    _context.NHAN_VIEN.Remove(nhanVien);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public NHAN_VIEN getById(int id)
        {
            NHAN_VIEN nhanVien = new NHAN_VIEN();
            try
            {
                var nv = _context.NHAN_VIEN.FirstOrDefault(s => s.Id == id);
                nhanVien = nv;
            }
            catch (Exception ex)
            {
                nhanVien = null;
            }
            return nhanVien;
        }

        public List<NhanVienVM> getAllNhanVienVM()
        {
            var queryGetListNhanVien = from nv in _context.NHAN_VIEN.ToList()
                                       join dt in _context.DAN_TOC.ToList() on nv.IdDanToc equals dt.Id
                                       join pb in _context.PHONG_BAN.ToList() on nv.IdPhongBan equals pb.Id
                                       join cv in _context.CHUC_VU.ToList() on nv.IdChucVu equals cv.Id
                                       join tdhv in _context.TRINH_DO_HOC_VAN.ToList() on nv.IdTrinhDoHocVan equals tdhv.Id
                                       join dml in _context.LUONGs.ToList() on nv.IdLuong equals dml.Id
                                       select new NhanVienVM()
                                       {
                                           Id = nv.Id,
                                           HoTen = nv.HoTen,
                                           NgaySinh = nv.NgaySinh,
                                           QueQuan = nv.QueQuan,
                                           GioiTinh = (nv.GioiTinh == 1) ? "Nam" : "Nữ",
                                           IdDanToc = dt.Id,
                                           TenDanToc = dt.TenDanToc,
                                           SoDienThoai = nv.SoDienThoai,
                                           IdPhongBan = pb.Id,
                                           PhongBan = pb.TenPB,
                                           IdChucVu = cv.Id,
                                           ChucVu = cv.TenChucVu,
                                           IdTrinhDoHocVan = tdhv.Id,
                                           TrinhDoHocVan = tdhv.TenTDHV,
                                           IdDmLuong = dml.Id,
                                           DmLuong = dml.TenDmLuong
                                       };
            List<NhanVienVM> nhanVienVMs = queryGetListNhanVien.ToList();
            return nhanVienVMs;
        }
    }
}
