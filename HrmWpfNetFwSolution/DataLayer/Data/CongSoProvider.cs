using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;
using ViewModel.BaoCaoThongKe;

namespace DataLayer.Data
{
    public class CongSoProvider : ICongSoProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public CongSoProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public CongSoProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<CONG_SO> getAll()
        {
            List<CONG_SO> congSos = new List<CONG_SO>();
            congSos = _context.CONG_SO.ToList();
            return congSos;
        }

        public bool Create(CONG_SO congSo)
        {
            try
            {
                _context.CONG_SO.Add(congSo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(CONG_SO congSo)
        {
            try
            {
                var cs = _context.CONG_SO.FirstOrDefault(s => s.Id == congSo.Id);
                cs.IdNhanVien = congSo.IdNhanVien;
                cs.NgayChamCong = congSo.NgayChamCong;
                cs.InTime = congSo.InTime;
                cs.OutTime = congSo.OutTime;
                cs.State = congSo.State;
                cs.IsCoPhep = congSo.IsCoPhep;
                cs.OtTime = congSo.OtTime;
                cs.Note = congSo.Note;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CONG_SO Detail(int id)
        {
            CONG_SO congSo = new CONG_SO();
            try
            {
                congSo = _context.CONG_SO.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                congSo = null;
            }
            return congSo;
        }

        public bool Delete(int id)
        {
            try
            {
                var congSo = _context.CONG_SO.FirstOrDefault(s => s.Id == id);
                if (congSo == null)
                {
                    return false;
                }
                else
                {
                    _context.CONG_SO.Remove(congSo);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<CONG_SO> getAllByDate(DateTime ngayChamCong)
        {
            List<CONG_SO> congSos = new List<CONG_SO>();
            congSos = _context.CONG_SO.Where(s=>s.NgayChamCong == ngayChamCong).ToList();
            return congSos;
        }

        public List<CongSoVM> getAllCongSoVMByDate(DateTime ngayChamCong)
        {
            List<CongSoVM> congSoVMs = new List<CongSoVM>();
            var query = from nv in _context.NHAN_VIEN.ToList()
                        join cs in _context.CONG_SO.ToList() on nv.Id equals cs.IdNhanVien into cso
                        from cs in cso.DefaultIfEmpty()
                        select new CongSoVM()
                        {
                            Id = cs?.Id,
                            IdNhanVien = nv.Id,
                            TenNhanVien = nv.HoTen,
                            SoDienThoai = nv.SoDienThoai,
                            NgayChamCong = cs?.NgayChamCong,
                            InTime = cs?.InTime,
                            OutTime = cs?.OutTime,
                            State = cs?.State,
                            IsCoPhep = cs?.IsCoPhep,
                            OtTime = cs?.OtTime,
                            Note = cs?.Note
                        };
            congSoVMs = query.ToList();
            congSoVMs = congSoVMs.Where(s => s.NgayChamCong == ngayChamCong).ToList();
            return congSoVMs;
        }


        public List<CongSoVM> getDataCongSoDaInsert(DateTime ngayChamCong)
        {
            List<CongSoVM> congSoVMs = new List<CongSoVM>();
            var query = from nv in _context.NHAN_VIEN.ToList()
                        join cs in _context.CONG_SO.ToList() on nv.Id equals cs.IdNhanVien into cso
                        from cs in cso.DefaultIfEmpty()
                        select new CongSoVM()
                        {
                            Id = cs?.Id,
                            IdNhanVien = nv.Id,
                            TenNhanVien = nv.HoTen,
                            SoDienThoai = nv.SoDienThoai,
                            NgayChamCong = cs?.NgayChamCong,
                            InTime = cs?.InTime,
                            OutTime = cs?.OutTime,
                            State = cs?.State,
                            IsCoPhep = cs?.IsCoPhep,
                            OtTime = cs?.OtTime,
                            Note = cs?.Note
                        };
            congSoVMs = query.ToList();
            congSoVMs = congSoVMs.Where(s => s.NgayChamCong == ngayChamCong).ToList();
            return congSoVMs;
        }

        public bool InsertEmptyCongSo(List<CONG_SO> congSos)
        {
            try
            {
                foreach (var item in congSos)
                {
                    _context.CONG_SO.Add(item);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ThongKeNhanVienNghi> getBaoCaoNgayNghiCuaNhanVien()
        {
            List<ThongKeNhanVienNghi> thongKeNgayNghiNVs = new List<ThongKeNhanVienNghi>();

            var query = from cs in _context.CONG_SO.ToList()
                        where cs.State == 2
                        join nv in _context.NHAN_VIEN.ToList() on cs.IdNhanVien equals nv.Id
                        group nv by nv.HoTen into nvGroup
                        select new ThongKeNhanVienNghi()
                        {
                            TenNhanVien = nvGroup.Key,
                            SoLanNghi = nvGroup.Count()
                        };
            thongKeNgayNghiNVs = query.ToList();

            return thongKeNgayNghiNVs;
        }
    }
}
