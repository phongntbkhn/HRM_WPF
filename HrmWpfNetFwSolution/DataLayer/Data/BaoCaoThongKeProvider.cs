using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.BaoCaoThongKe;

namespace DataLayer.Data
{
    public class BaoCaoThongKeProvider : IBaoCaoThongKeProvider
    {
		private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

		public BaoCaoThongKeProvider()
		{
			_context = new HumanResourceManagementDbContext();
		}

		public BaoCaoThongKeProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<ThongKeNvByChucVu> getThongKeNvByChucVu()
        {
            List<ThongKeNvByChucVu> thongKeNvByChucVus = new List<ThongKeNvByChucVu>();

			var query = from cv in _context.CHUC_VU.ToList()
						join nv in _context.NHAN_VIEN.ToList() on cv.Id equals nv.IdChucVu
						group cv by cv.TenChucVu into nvGroup
						select new ThongKeNvByChucVu()
						{
							TenChucVu = nvGroup.Key,
							SoNhanVien = nvGroup.Count()
						};
            thongKeNvByChucVus = query.ToList();
			return thongKeNvByChucVus;
        }

        public List<ThongKeNvByGioiTinh> getThongKeNvByGioiTinh()
        {
            List<ThongKeNvByGioiTinh> thongKeNvByGioiTinhs = new List<ThongKeNvByGioiTinh>();

			var query = from cv in _context.NHAN_VIEN.ToList()
						group cv by cv.GioiTinh into nvGroup
						select new ThongKeNvByGioiTinh()
						{
							TenGioiTinh = (nvGroup.Key == 1) ? "Nam" : "Nữ",
							SoNhanVien = nvGroup.Count()
						};
            thongKeNvByGioiTinhs = query.ToList();
			return thongKeNvByGioiTinhs;
        }

        public List<ThongKeNvByMucLuong> getThongKeNvByMucLuong()
        {
            List<ThongKeNvByMucLuong> thongKeNvByMucLuongs = new List<ThongKeNvByMucLuong>();

			var query = from l in _context.LUONGs.ToList()
						join nv in _context.NHAN_VIEN.ToList() on l.Id equals nv.IdLuong
						group l by l.TenDmLuong into nvGroup
						select new ThongKeNvByMucLuong()
						{
							TenDmLuong = nvGroup.Key,
							SoNhanVien = nvGroup.Count()
						};
            thongKeNvByMucLuongs = query.ToList();
			return thongKeNvByMucLuongs;
        }

        public List<ThongKeNvByPhongBan> getThongKeNvByPhongBan()
        {
            List<ThongKeNvByPhongBan> thongKeNvByPhongBans = new List<ThongKeNvByPhongBan>();

			var query = from pb in _context.PHONG_BAN.ToList()
						join nv in _context.NHAN_VIEN.ToList() on pb.Id equals nv.IdPhongBan
						group pb by pb.TenPB into nvGroup
						select new ThongKeNvByPhongBan()
						{
							TenPb = nvGroup.Key,
							SoNhanVien = nvGroup.Count()
						};
            thongKeNvByPhongBans = query.ToList();
			return thongKeNvByPhongBans;
        }

        public List<ThongKeNvByTrinhDoHocVan> getThongKeNvByTrinhDoHocVan()
        {
            List<ThongKeNvByTrinhDoHocVan> thongKeNvByTrinhDoHocVans = new List<ThongKeNvByTrinhDoHocVan>();

			var query = from td in _context.TRINH_DO_HOC_VAN.ToList()
						join nv in _context.NHAN_VIEN.ToList() on td.Id equals nv.IdTrinhDoHocVan
						group td by td.TenTDHV into nvGroup
						select new ThongKeNvByTrinhDoHocVan()
						{
							TenTdhv = nvGroup.Key,
							SoNhanVien = nvGroup.Count()
						};
            thongKeNvByTrinhDoHocVans = query.ToList();
			return thongKeNvByTrinhDoHocVans;
        }
    }
}
