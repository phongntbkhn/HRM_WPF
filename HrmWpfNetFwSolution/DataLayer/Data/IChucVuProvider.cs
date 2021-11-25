using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data
{
    public interface IChucVuProvider
    {
        List<CHUC_VU> getAll();
        bool Create(CHUC_VU chucVu);
        bool Edit(CHUC_VU chucVu);
        CHUC_VU Detail(int id);
        bool Delete(int id);
    }
}
