using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Data
{
    public class LoaiHopDongProvider : ILoaiHopDongProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public LoaiHopDongProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public LoaiHopDongProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<LOAI_HOP_DONG> getAll()
        {
            List<LOAI_HOP_DONG> loaiHopDong = new List<LOAI_HOP_DONG>();
            loaiHopDong = _context.LOAI_HOP_DONG.ToList();
            return loaiHopDong;
        }

        public bool Create(LOAI_HOP_DONG loaiHopDong)
        {
            try
            {
                _context.LOAI_HOP_DONG.Add(loaiHopDong);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(LOAI_HOP_DONG loaiHopDong)
        {
            try
            {
                var lHopDong = _context.LOAI_HOP_DONG.FirstOrDefault(s => s.Id == loaiHopDong.Id);
                lHopDong.TenLoaiHopDong = loaiHopDong.TenLoaiHopDong;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LOAI_HOP_DONG Detail(int id)
        {
            LOAI_HOP_DONG loaiHopDong = new LOAI_HOP_DONG();
            try
            {
                loaiHopDong = _context.LOAI_HOP_DONG.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                loaiHopDong = null;
            }
            return loaiHopDong;
        }

        public bool Delete(int id)
        {
            try
            {
                var loaiHopDong = _context.LOAI_HOP_DONG.FirstOrDefault(s => s.Id == id);
                if (loaiHopDong == null)
                {
                    return false;
                }
                else
                {
                    _context.LOAI_HOP_DONG.Remove(loaiHopDong);
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
