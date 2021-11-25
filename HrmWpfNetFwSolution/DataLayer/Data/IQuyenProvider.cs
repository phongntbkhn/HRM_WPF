using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data
{
    public interface IQuyenProvider
    {
        List<QUYEN> getAll();
        bool Create(QUYEN quyen);
        bool Edit(QUYEN quyen);
        QUYEN Detail(int id);
        bool Delete(int id);
        List<QUYEN> getByTenQuyen(string tenQuyen);
    }
}
