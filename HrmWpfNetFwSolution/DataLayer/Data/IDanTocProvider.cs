using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data
{
    public interface IDanTocProvider
    {
        List<DAN_TOC> getAll();
        bool Create(DAN_TOC danToc);
        bool Edit(DAN_TOC danToc);
        DAN_TOC Detail(int id);
        bool Delete(int id);
    }
}
