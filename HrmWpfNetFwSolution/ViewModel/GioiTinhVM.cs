using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class GioiTinhVM
    {
        public GioiTinhVM(int id, string ten)
        {
            this.Id = id;
            this.TenGioiTinh = ten;
        }

        public int Id { get; set; }
        public string TenGioiTinh { get; set; }
    }
}
