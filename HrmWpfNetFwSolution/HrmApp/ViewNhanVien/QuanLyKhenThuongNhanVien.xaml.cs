using DataLayer.Data;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities.Messages;
using Utilities.Utils;
using ViewModel;

namespace HrmApp.ViewNhanVien
{
    /// <summary>
    /// Interaction logic for QuanLyKhenThuongNhanVien.xaml
    /// </summary>
    public partial class QuanLyKhenThuongNhanVien : Window
    {
        KhenThuongNhanVienProvider _khenThuong = new KhenThuongNhanVienProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        Regex regex = new Regex("^[0-9]+$");

        public QuanLyKhenThuongNhanVien()
        {
            InitializeComponent();

            bindingCbbNhanVien();
            getDanhSachKhenThuongNhanVien();
        }

        public void getDanhSachKhenThuongNhanVien()
        {
            List<KhenThuongNhanVienVM> khenThuongNhanVienVMs = _khenThuong.getAllKhenThuongNhanVienVM();

            dataGrid1.ItemsSource = khenThuongNhanVienVMs;
        }

        public void bindingCbbNhanVien()
        {
            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();
            cbbNhanVien.ItemsSource = nhanViens;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTenNhanVien = txtTenNhanVienTimKiem.Text;

            List<KhenThuongNhanVienVM> khenThuongNhanVienVMs = _khenThuong.getAllKhenThuongNhanVienVM();

            if (!string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                khenThuongNhanVienVMs = khenThuongNhanVienVMs.Where(s => s.TenNhanVien.Contains(strTenNhanVien)).ToList();
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = khenThuongNhanVienVMs;
        }

        private void btnThemKhenThuongNV_Click(object sender, RoutedEventArgs e)
        {
            KHEN_THUONG_NHAN_VIEN khenThuongNhanVien = new KHEN_THUONG_NHAN_VIEN();

            int intIdNhanVien = 0;

            string dNgayKhenThuong = dateNgayKhenThuong.Text;
            string strNoiDungKhenThuong = txtNoiDungKhenThuong.Text;
            string strPhanThuong = txtPhanThuong.Text;

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
                if (intIdNhanVien == 0)
                {
                    MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(dNgayKhenThuong))
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(strNoiDungKhenThuong))
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((bool)grdTien.IsChecked == false && (bool)grdKhac.IsChecked == false)
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_8, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((bool)grdTien.IsChecked)
            {
                if (!regex.IsMatch(strPhanThuong))
                {
                    MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            khenThuongNhanVien.IdNhanVien = intIdNhanVien;
            khenThuongNhanVien.NgayKhenThuong = Util.CnvObjToDatetime(dNgayKhenThuong);
            khenThuongNhanVien.NoiDungKhenThuong = strNoiDungKhenThuong;
            khenThuongNhanVien.PhanThuong = strPhanThuong;
            if ((bool)grdTien.IsChecked)
            {
                khenThuongNhanVien.LoaiPhanThuong = 1;
            }
            else
            {
                khenThuongNhanVien.LoaiPhanThuong = 2;
            }

            bool insertKhenThuongNhanVien = _khenThuong.Create(khenThuongNhanVien);
            if (insertKhenThuongNhanVien)
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _khenThuong.getAllKhenThuongNhanVienVM();
        }

        private void btnCapNhatKhenThuongNV_Click(object sender, RoutedEventArgs e)
        {
            KHEN_THUONG_NHAN_VIEN khenThuongNhanVien = new KHEN_THUONG_NHAN_VIEN();

            int intIdNhanVien = 0;

            string dNgayKhenThuong = dateNgayKhenThuong.Text;
            string strNoiDungKhenThuong = txtNoiDungKhenThuong.Text;
            string strPhanThuong = txtPhanThuong.Text;

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
                if (intIdNhanVien == 0)
                {
                    MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(dNgayKhenThuong))
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(strNoiDungKhenThuong))
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((bool)grdTien.IsChecked == false && (bool)grdKhac.IsChecked == false)
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_8, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((bool)grdTien.IsChecked)
            {
                if (!regex.IsMatch(strPhanThuong))
                {
                    MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            khenThuongNhanVien.IdNhanVien = intIdNhanVien;
            khenThuongNhanVien.NgayKhenThuong = Util.CnvObjToDatetime(dNgayKhenThuong);
            khenThuongNhanVien.NoiDungKhenThuong = strNoiDungKhenThuong;
            khenThuongNhanVien.PhanThuong = strPhanThuong;

            if ((bool)grdTien.IsChecked)
            {
                khenThuongNhanVien.LoaiPhanThuong = 1;
            }
            else
            {
                khenThuongNhanVien.LoaiPhanThuong = 2;
            }

            khenThuongNhanVien.Id = Util.CnvObjToInt32(lblIdKhenThuongNhanVien.Content);

            bool insertKhenThuongNhanVien = _khenThuong.Edit(khenThuongNhanVien);
            if (insertKhenThuongNhanVien)
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _khenThuong.getAllKhenThuongNhanVienVM();
        }

        private void btnXoaKhenThuongNV_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                KhenThuongNhanVienVM item = (KhenThuongNhanVienVM)dataGrid1.SelectedItem;
                bool deleteKhenThuongNhanVien = _khenThuong.Delete(item.Id);
                if (deleteKhenThuongNhanVien)
                {
                    MessageBox.Show(Msg.KHENTHUONGNHANVIEN_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.KHENTHUONGNHANVIEN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _khenThuong.getAllKhenThuongNhanVienVM();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                KhenThuongNhanVienVM item = (KhenThuongNhanVienVM)dataGrid1.SelectedItem;

                cbbNhanVien.SelectedItem = _nhanVien.Detail(item.IdNhanVien);
                dateNgayKhenThuong.Text = Util.CnvObjToString(item.NgayKhenThuong);
                txtNoiDungKhenThuong.Text = item.NoiDungKhenThuong;
                txtPhanThuong.Text = item.PhanThuong;
                if (item.LoaiPhanThuong == 1)
                {
                    grdTien.IsChecked = true;
                }
                else
                {
                    grdKhac.IsChecked = true;
                }

                lblIdKhenThuongNhanVien.Content = item.Id;
            }
        }
    }
}
