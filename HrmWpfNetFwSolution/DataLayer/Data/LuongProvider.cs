using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Data
{
    public class LuongProvider : ILuongProvider
    {
        private readonly HumanResourceManagementDbContext _context = new HumanResourceManagementDbContext();

        public LuongProvider()
        {
            _context = new HumanResourceManagementDbContext();
        }

        public LuongProvider(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public List<LUONG> getAll()
        {
            List<LUONG> luongs = new List<LUONG>();
            luongs = _context.LUONGs.ToList();
            return luongs;
        }

        public bool Create(LUONG luong)
        {
            try
            {
                _context.LUONGs.Add(luong);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(LUONG luong)
        {
            try
            {
                var l = _context.LUONGs.FirstOrDefault(s => s.Id == luong.Id);
                l.LuongCoBan = luong.LuongCoBan;
                l.HeSoLuong = luong.HeSoLuong;
                l.PhuCapLuong = luong.PhuCapLuong;
                l.TenDmLuong = luong.TenDmLuong;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LUONG Detail(int id)
        {
            LUONG luong = new LUONG();
            try
            {
                luong = _context.LUONGs.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                luong = null;
            }
            return luong;
        }

        public bool Delete(int id)
        {
            try
            {
                var luong = _context.LUONGs.FirstOrDefault(s => s.Id == id);
                if (luong == null)
                {
                    return false;
                }
                else
                {
                    _context.LUONGs.Remove(luong);
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
