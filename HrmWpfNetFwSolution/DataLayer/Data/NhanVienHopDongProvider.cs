using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;

namespace DataLayer.Data
{
    public class NhanVienHopDongProvider : INhanVienHopDongProvider
    {

        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public NhanVienHopDongProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public NhanVienHopDongProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<NHAN_VIEN_HOP_DONG> getAll()
        {
            List<NHAN_VIEN_HOP_DONG> nhanVienHopDongs = new List<NHAN_VIEN_HOP_DONG>();
            nhanVienHopDongs = _context.NHAN_VIEN_HOP_DONG.ToList();
            return nhanVienHopDongs;
        }

        public bool Create(NHAN_VIEN_HOP_DONG nhanVienHopDong)
        {
            try
            {
                _context.NHAN_VIEN_HOP_DONG.Add(nhanVienHopDong);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(NHAN_VIEN_HOP_DONG nhanVienHopDong)
        {
            try
            {
                var nvHopDong = _context.NHAN_VIEN_HOP_DONG.FirstOrDefault(s => s.Id == nhanVienHopDong.Id);
                nvHopDong.IdNhanVien = nhanVienHopDong.IdNhanVien;
                nvHopDong.IdLoaiHopDong = nhanVienHopDong.IdLoaiHopDong;
                nvHopDong.TuNgay = nhanVienHopDong.TuNgay;
                nvHopDong.DenNgay = nhanVienHopDong.DenNgay;
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
                var nvHopDong = _context.NHAN_VIEN_HOP_DONG.FirstOrDefault(s => s.Id == id);
                if (nvHopDong == null)
                {
                    return false;
                }
                else
                {
                    _context.NHAN_VIEN_HOP_DONG.Remove(nvHopDong);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public NHAN_VIEN_HOP_DONG Detail(int id)
        {
            throw new NotImplementedException();
        }

        public List<HopDongNhanVienVM> getAllNhanVienHopDongs()
        {
            var queryGetListNhanVienHopDong = from hdnv in _context.NHAN_VIEN_HOP_DONG.ToList()
                                       join nv in _context.NHAN_VIEN.ToList() on hdnv.IdNhanVien equals nv.Id
                                       join lhd in _context.LOAI_HOP_DONG.ToList() on hdnv.IdLoaiHopDong equals lhd.Id
                                       select new HopDongNhanVienVM()
                                       {
                                           Id = hdnv.Id,
                                           IdNhanVien = nv.Id,
                                           HoTen = nv.HoTen,
                                           IdLoaiHopDong = lhd.Id,
                                           TenLoaiHopDong = lhd.TenLoaiHopDong,
                                           SoDienThoai = nv.SoDienThoai,
                                           TuNgay = hdnv.TuNgay,
                                           DenNgay = hdnv.DenNgay
                                       };
            List<HopDongNhanVienVM> hopDongNhanVienVMs = queryGetListNhanVienHopDong.ToList();
            return hopDongNhanVienVMs;
        }
    }
}
