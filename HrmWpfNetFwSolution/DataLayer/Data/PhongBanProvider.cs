using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Data
{
    public class PhongBanProvider : IPhongBanProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();
        public PhongBanProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public PhongBanProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<PHONG_BAN> getAll()
        {
            List<PHONG_BAN> phongBans = new List<PHONG_BAN>();
            phongBans = _context.PHONG_BAN.ToList();
            return phongBans;
        }

        public bool Create(PHONG_BAN phongBan)
        {
            try
            {
                _context.PHONG_BAN.Add(phongBan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(PHONG_BAN phongBan)
        {
            try
            {
                PHONG_BAN pb = _context.PHONG_BAN.FirstOrDefault(s => s.Id == phongBan.Id);
                pb.TenPB = phongBan.TenPB;
                pb.DiaChiPB = phongBan.DiaChiPB;
                pb.SdtPB = phongBan.SdtPB;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PHONG_BAN Detail(int id)
        {
            PHONG_BAN phongBan = new PHONG_BAN();
            try
            {
                var pBan = _context.PHONG_BAN.FirstOrDefault(s => s.Id == id);
                phongBan = pBan;
            }
            catch (Exception ex)
            {
                phongBan = null;
            }
            return phongBan;
        }

        public bool Delete(int id)
        {
            try
            {
                var phongBan = _context.PHONG_BAN.FirstOrDefault(s => s.Id == id);
                if (phongBan == null)
                {
                    return false;
                }
                else
                {
                    _context.PHONG_BAN.Remove(phongBan);
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
