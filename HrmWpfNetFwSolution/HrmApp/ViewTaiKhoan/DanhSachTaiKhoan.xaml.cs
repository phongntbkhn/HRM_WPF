using DataLayer.Data;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace HrmApp.ViewTaiKhoan
{
    /// <summary>
    /// Interaction logic for DanhSachTaiKhoan.xaml
    /// </summary>
    public partial class DanhSachTaiKhoan : Window
    {
        TaiKhoanProvider _taiKhoan = new TaiKhoanProvider();

        public DanhSachTaiKhoan()
        {
            InitializeComponent();
            getDataTaiKhoanNguoiDung();
        }

        public void getDataTaiKhoanNguoiDung()
        {
            List<TaiKhoanVM> taiKhoans = _taiKhoan.getAllTaiKhoanMV();
            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = taiKhoans;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTimKiem = txtTimKiem.Text;
            List<TaiKhoanVM> taiKhoans = _taiKhoan.getAllTaiKhoanMV();
            taiKhoans = taiKhoans.Where(s => s.TenDangNhap.Contains(strTimKiem)
            || s.TenHienThi.Contains(strTimKiem)
            || s.TenNhanVien.Contains(strTimKiem)
            || s.TenQuyen.Contains(strTimKiem)
            ).ToList();

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = taiKhoans;
        }
    }
}
