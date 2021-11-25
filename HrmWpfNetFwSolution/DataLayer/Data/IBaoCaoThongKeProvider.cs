using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.BaoCaoThongKe;

namespace DataLayer.Data
{
    public interface IBaoCaoThongKeProvider
    {
        List<ThongKeNvByChucVu> getThongKeNvByChucVu();
        List<ThongKeNvByGioiTinh> getThongKeNvByGioiTinh();
        List<ThongKeNvByMucLuong> getThongKeNvByMucLuong();
        List<ThongKeNvByPhongBan> getThongKeNvByPhongBan();
        List<ThongKeNvByTrinhDoHocVan> getThongKeNvByTrinhDoHocVan();
    }
}
