using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;
using ViewModel.BaoCaoThongKe;

namespace DataLayer.Data
{
    public interface ICongSoProvider
    {
        List<CONG_SO> getAll();
        List<CONG_SO> getAllByDate(DateTime ngayChamCong);
        bool Create(CONG_SO congSo);
        bool Edit(CONG_SO congSo);
        CONG_SO Detail(int id);
        bool Delete(int id);
        List<CongSoVM> getAllCongSoVMByDate(DateTime ngayChamcong);
        List<CongSoVM> getDataCongSoDaInsert(DateTime ngayChamCong);
        bool InsertEmptyCongSo(List<CONG_SO> congSos);

        List<ThongKeNhanVienNghi> getBaoCaoNgayNghiCuaNhanVien();
    }
}
