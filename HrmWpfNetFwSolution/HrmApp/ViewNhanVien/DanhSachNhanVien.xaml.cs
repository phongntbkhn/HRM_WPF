using DataLayer.Data;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities.Utils;
using ViewModel;

namespace HrmApp.ViewNhanVien
{
    /// <summary>
    /// Interaction logic for DanhSachNhanVien.xaml
    /// </summary>
    public partial class DanhSachNhanVien : Window
    {
        NhanVienProvider _nhanVien = new NhanVienProvider();
        DanTocProvider _danToc = new DanTocProvider();
        PhongBanProvider _phongBan = new PhongBanProvider();
        ChucVuProvider _chucVu = new ChucVuProvider();
        TrinhDoHocVanProvider _trinhDoHocVan = new TrinhDoHocVanProvider();
        LuongProvider _luong = new LuongProvider();

        public DanhSachNhanVien()
        {
            InitializeComponent();

            getDanhSachNhanVien();

            List<PHONG_BAN> phongBans = new List<PHONG_BAN>();
            PHONG_BAN phongBan = new PHONG_BAN(0, "Tất cả");
            phongBans.Add(phongBan);
            phongBans.AddRange(_phongBan.getAll());
            cbbPhongBan.ItemsSource = phongBans;

            List<CHUC_VU> chucVus = new List<CHUC_VU>();
            CHUC_VU chucVu = new CHUC_VU(0, "Tất cả");
            chucVus.Add(chucVu);
            chucVus.AddRange(_chucVu.getAll());
            cbbChucVu.ItemsSource = chucVus;

            List<TRINH_DO_HOC_VAN> trinhDoHocVans = new List<TRINH_DO_HOC_VAN>();
            TRINH_DO_HOC_VAN trinhDoHocVan = new TRINH_DO_HOC_VAN(0, "Tất cả");
            trinhDoHocVans.Add(trinhDoHocVan);
            trinhDoHocVans.AddRange(_trinhDoHocVan.getAll());
            cbbTrinhDoHocVan.ItemsSource = trinhDoHocVans;
        }

        public void getDanhSachNhanVien()
        {
            List<NhanVienVM> nhanVienVms = _nhanVien.getAllNhanVienVM();

            dataGrid1.ItemsSource = nhanVienVms;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTenNhanVien = txtTenNhanVien.Text;
            string strQueQuan = txtQueQuan.Text;
            string strSoDienThoai = txtSoDienThoai.Text;

            int idPhongBan = 0;
            int idChucVu = 0;
            int idTrinhDocVan = 0;

            List<NhanVienVM> nhanVienVMs = _nhanVien.getAllNhanVienVM();

            if (!string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                nhanVienVMs = nhanVienVMs.Where(s => s.HoTen.Contains(strTenNhanVien)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(strQueQuan))
            {
                nhanVienVMs = nhanVienVMs.Where(s => s.QueQuan.Contains(strQueQuan)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(strSoDienThoai))
            {
                nhanVienVMs = nhanVienVMs.Where(s => s.SoDienThoai.Contains(strSoDienThoai)).ToList();
            }

            PHONG_BAN phongBanSelected = (PHONG_BAN)cbbPhongBan.SelectedItem;
            if (phongBanSelected != null)
            {
                idPhongBan = Util.CnvObjToInt32(phongBanSelected.Id);
                if (idPhongBan != 0)
                {
                    nhanVienVMs = nhanVienVMs.Where(s => s.IdPhongBan == idPhongBan).ToList();
                }
            }

            CHUC_VU chucVuSelected = (CHUC_VU)cbbChucVu.SelectedItem;
            if (chucVuSelected != null)
            {
                idChucVu = Util.CnvObjToInt32(chucVuSelected.Id);
                if (idChucVu != 0)
                {
                    nhanVienVMs = nhanVienVMs.Where(s => s.IdChucVu == idChucVu).ToList();
                }
            }

            TRINH_DO_HOC_VAN tdhvSelected = (TRINH_DO_HOC_VAN)cbbTrinhDoHocVan.SelectedItem;
            if (tdhvSelected != null)
            {
                idTrinhDocVan = Util.CnvObjToInt32(tdhvSelected.Id);
                if (idTrinhDocVan != 0)
                {
                    nhanVienVMs = nhanVienVMs.Where(s => s.IdTrinhDoHocVan == idTrinhDocVan).ToList();
                }
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = nhanVienVMs;
        }
    }
}
