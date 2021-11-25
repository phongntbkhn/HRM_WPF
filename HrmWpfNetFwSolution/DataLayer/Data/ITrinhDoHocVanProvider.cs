using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data
{
    public interface ITrinhDoHocVanProvider
    {
        List<TRINH_DO_HOC_VAN> getAll();
        bool Create(TRINH_DO_HOC_VAN trinhDoHocVan);
        bool Edit(TRINH_DO_HOC_VAN trinhDoHocVan);
        TRINH_DO_HOC_VAN Detail(int id);
        bool Delete(int id);
    }
}
