using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace DataLayer.Data
{
    public interface IDuAnProvider
    {
        List<DU_AN> getAll();
        bool Create(DU_AN duAn);
        bool Edit(DU_AN duAn);
        DU_AN Detail(int id);
        bool Delete(int id);

        List<DuAnVM> getAllDuAnVM();
    }
}
