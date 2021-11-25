using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Data
{
    public class TrinhDoHocVanProvider : ITrinhDoHocVanProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public TrinhDoHocVanProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public TrinhDoHocVanProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<TRINH_DO_HOC_VAN> getAll()
        {
            List<TRINH_DO_HOC_VAN> trinhDoHocVans = new List<TRINH_DO_HOC_VAN>();
            trinhDoHocVans = _context.TRINH_DO_HOC_VAN.ToList();
            return trinhDoHocVans;
        }

        public bool Create(TRINH_DO_HOC_VAN trinhDoHocVan)
        {
            try
            {
                _context.TRINH_DO_HOC_VAN.Add(trinhDoHocVan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(TRINH_DO_HOC_VAN trinhDoHocVan)
        {
            try
            {
                var tdhv = _context.TRINH_DO_HOC_VAN.FirstOrDefault(s => s.Id == trinhDoHocVan.Id);
                tdhv.TenTDHV = trinhDoHocVan.TenTDHV;
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
                var trinhDoHocVan = _context.TRINH_DO_HOC_VAN.FirstOrDefault(s => s.Id == id);
                if (trinhDoHocVan == null)
                {
                    return false;
                }
                else
                {
                    _context.TRINH_DO_HOC_VAN.Remove(trinhDoHocVan);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TRINH_DO_HOC_VAN Detail(int id)
        {
            TRINH_DO_HOC_VAN trinhDoHocVan = new TRINH_DO_HOC_VAN();
            try
            {
                var tdHocVan = _context.TRINH_DO_HOC_VAN.FirstOrDefault(s => s.Id == id);
                trinhDoHocVan = tdHocVan;
            }
            catch (Exception ex)
            {
                trinhDoHocVan = null;
            }
            return trinhDoHocVan;
        }

        


    }
}
