using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Data
{
    public class DanTocProvider : IDanTocProvider
    {

        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public DanTocProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public DanTocProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<DAN_TOC> getAll()
        {
            List<DAN_TOC> danTocs = new List<DAN_TOC>();
            danTocs = _context.DAN_TOC.ToList();
            return danTocs;
        }

        public bool Create(DAN_TOC danToc)
        {
            try
            {
                _context.DAN_TOC.Add(danToc);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(DAN_TOC danToc)
        {
            try
            {
                var dtoc = _context.DAN_TOC.FirstOrDefault(s => s.Id == danToc.Id);
                dtoc.TenDanToc = danToc.TenDanToc;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var danToc = _context.DAN_TOC.FirstOrDefault(s => s.Id == id);
                if (danToc == null)
                {
                    return false;
                }
                else
                {
                    _context.DAN_TOC.Remove(danToc);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DAN_TOC Detail(int id)
        {
            DAN_TOC danToc = new DAN_TOC();
            try
            {
                var dToc = _context.DAN_TOC.FirstOrDefault(s => s.Id == id);
                danToc = dToc;
            }
            catch (Exception ex)
            {
                danToc = null;
            }
            return danToc;
        }
    }
}
