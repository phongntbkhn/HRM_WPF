using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data
{
    public interface ILuongProvider
    {
        List<LUONG> getAll();
        bool Create(LUONG luong);
        bool Edit(LUONG luong);
        LUONG Detail(int id);
        bool Delete(int id);
    }
}
