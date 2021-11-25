using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data
{
    public interface ILoaiHopDongProvider
    {
        List<LOAI_HOP_DONG> getAll();
        bool Create(LOAI_HOP_DONG loaiHopDong);
        bool Edit(LOAI_HOP_DONG loaiHopDong);
        LOAI_HOP_DONG Detail(int id);
        bool Delete(int id);
    }
}
