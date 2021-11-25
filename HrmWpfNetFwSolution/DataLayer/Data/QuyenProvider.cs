using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Data
{
    public class QuyenProvider : IQuyenProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public QuyenProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public QuyenProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<QUYEN> getAll()
        {
            List<QUYEN> quyens = new List<QUYEN>();
            quyens = _context.QUYENs.ToList();
            return quyens;
        }

        public bool Create(QUYEN quyen)
        {
            try
            {
                _context.QUYENs.Add(quyen);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(QUYEN quyen)
        {
            try
            {
                var q = _context.QUYENs.FirstOrDefault(s => s.Id == quyen.Id);
                q.TEN_QUYEN = quyen.TEN_QUYEN;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public QUYEN Detail(int id)
        {
            QUYEN quyen = new QUYEN();
            try
            {
                quyen = _context.QUYENs.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                quyen = null;
            }
            return quyen;
        }

        public bool Delete(int id)
        {
            try
            {
                var quyen = _context.QUYENs.FirstOrDefault(s => s.Id == id);
                if (quyen == null)
                {
                    return false;
                }
                else
                {
                    _context.QUYENs.Remove(quyen);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<QUYEN> getByTenQuyen(string tenQuyen)
        {
            List<QUYEN> quyens = new List<QUYEN>();
            quyens = _context.QUYENs.Where(s=>s.TEN_QUYEN.Equals(tenQuyen)).ToList();
            return quyens;
        }
    }
}
