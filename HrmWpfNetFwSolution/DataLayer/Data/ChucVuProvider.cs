using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Data
{
    public class ChucVuProvider : IChucVuProvider
    {

        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public ChucVuProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public ChucVuProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<CHUC_VU> getAll()
        {
            List<CHUC_VU> chucVus = new List<CHUC_VU>();
            chucVus = _context.CHUC_VU.ToList();
            return chucVus;
        }

        public bool Create(CHUC_VU chucVu)
        {
            try
            {
                _context.CHUC_VU.Add(chucVu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(CHUC_VU chucVu)
        {
            try
            {
                var cv = _context.CHUC_VU.FirstOrDefault(s => s.Id == chucVu.Id);
                cv.TenChucVu = chucVu.TenChucVu;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CHUC_VU Detail(int id)
        {
            CHUC_VU chucVu = new CHUC_VU();
            try
            {
                chucVu = _context.CHUC_VU.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                chucVu = null;
            }
            return chucVu;
        }

        public bool Delete(int id)
        {
            try
            {
                var chucVu = _context.CHUC_VU.FirstOrDefault(s => s.Id == id);
                if (chucVu == null)
                {
                    return false;
                }
                else
                {
                    _context.CHUC_VU.Remove(chucVu);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
