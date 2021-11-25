using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data
{
    public interface IPhongBanProvider
    {
        List<PHONG_BAN> getAll();
        bool Create(PHONG_BAN phongBan);
        bool Edit(PHONG_BAN phongBan);
        PHONG_BAN Detail(int id);
        bool Delete(int id);
    }
}
