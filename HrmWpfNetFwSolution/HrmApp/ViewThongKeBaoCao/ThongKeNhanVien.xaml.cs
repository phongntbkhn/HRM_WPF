using DataLayer.Data;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel.BaoCaoThongKe;

namespace HrmApp.ViewThongKeBaoCao
{
    /// <summary>
    /// Interaction logic for ThongKeNhanVien.xaml
    /// </summary>
    public partial class ThongKeNhanVien : Window
    {
        BaoCaoThongKeProvider _thongKe = new BaoCaoThongKeProvider();

        public ThongKeNhanVien()
        {
            InitializeComponent();

            getThongKeNvPhongBan();
            getThongKeNvChucVu();
            getThongKeNvHocVan();
            getThongKeNvMucLuong();
        }

        public void getThongKeNvPhongBan()
        {
            List<ThongKeNvByPhongBan> nhanVienVms = _thongKe.getThongKeNvByPhongBan();

            dgNvPhongBan.CanUserAddRows = false;
            dgNvPhongBan.ItemsSource = nhanVienVms;
        }

        public void getThongKeNvChucVu()
        {
            List<ThongKeNvByChucVu> nhanVienVms = _thongKe.getThongKeNvByChucVu();

            dgNvChucVu.CanUserAddRows = false;
            dgNvChucVu.ItemsSource = nhanVienVms;
        }

        public void getThongKeNvHocVan()
        {
            List<ThongKeNvByTrinhDoHocVan> nhanVienVms = _thongKe.getThongKeNvByTrinhDoHocVan();

            dgNvTrinhDoHocVan.CanUserAddRows = false;
            dgNvTrinhDoHocVan.ItemsSource = nhanVienVms;
        }

        public void getThongKeNvMucLuong()
        {
            List<ThongKeNvByMucLuong> nhanVienVms = _thongKe.getThongKeNvByMucLuong();

            dgNvDmLuong.CanUserAddRows = false;
            dgNvDmLuong.ItemsSource = nhanVienVms;
        }
    }
}
