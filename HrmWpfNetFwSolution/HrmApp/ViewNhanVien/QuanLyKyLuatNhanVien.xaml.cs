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
    /// Interaction logic for QuanLyKyLuatNhanVien.xaml
    /// </summary>
    public partial class QuanLyKyLuatNhanVien : Window
    {
        KyLuatNhanVienProvider _kyLuat = new KyLuatNhanVienProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        Regex regex = new Regex("^[0-9]+$");

        public QuanLyKyLuatNhanVien()
        {
            InitializeComponent();

            bindingCbbNhanVien();
            getDanhSachKyLuatNhanVien();
        }

        public void getDanhSachKyLuatNhanVien()
        {
            List<KyLuatNhanVienVM> kyLuatNhanVienVMs = _kyLuat.getAllKyLuatNhanVienVM();

            dataGrid1.ItemsSource = kyLuatNhanVienVMs;
        }

        public void bindingCbbNhanVien()
        {
            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();
            cbbNhanVien.ItemsSource = nhanViens;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTenNhanVien = txtTenNhanVienTimKiem.Text;

            List<KyLuatNhanVienVM> kyLuatNhanVienVMs = _kyLuat.getAllKyLuatNhanVienVM();

            if (!string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                kyLuatNhanVienVMs = kyLuatNhanVienVMs.Where(s => s.TenNhanVien.Contains(strTenNhanVien)).ToList();
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = kyLuatNhanVienVMs;
        }

        private void btnThemKyLuatNV_Click(object sender, RoutedEventArgs e)
        {
            KY_LUAT_NHAN_VIEN kyLuatNhanVien = new KY_LUAT_NHAN_VIEN();

            int intIdNhanVien = 0;

            string dNgayKyLuat = dateNgayKyLuat.Text;
            string strNoiDungKyLuat = txtNoiDungKyLuat.Text;
            string strHinhPhat = txtHinhPhat.Text;

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
                if (intIdNhanVien == 0)
                {
                    MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(dNgayKyLuat))
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(strNoiDungKyLuat))
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((bool)grdTien.IsChecked == false && (bool)grdKhac.IsChecked == false)
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_8, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((bool)grdTien.IsChecked)
            {
                if (!regex.IsMatch(strHinhPhat))
                {
                    MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            kyLuatNhanVien.IdNhanVien = intIdNhanVien;
            kyLuatNhanVien.NgayKyLuat = Util.CnvObjToDatetime(dNgayKyLuat);
            kyLuatNhanVien.NoiDungKyLuat = strNoiDungKyLuat;
            kyLuatNhanVien.HinhPhat = strHinhPhat;

            if ((bool)grdTien.IsChecked)
            {
                kyLuatNhanVien.LoaiHinhPhat = 1;
            }
            else
            {
                kyLuatNhanVien.LoaiHinhPhat = 2;
            }

            bool insertKyLuatNhanVien = _kyLuat.Create(kyLuatNhanVien);
            if (insertKyLuatNhanVien)
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _kyLuat.getAllKyLuatNhanVienVM();
        }

        private void btnCapNhatKyLuatNV_Click(object sender, RoutedEventArgs e)
        {
            KY_LUAT_NHAN_VIEN kyLuatNhanVien = new KY_LUAT_NHAN_VIEN();

            int intIdNhanVien = 0;

            string dNgayKyLuat = dateNgayKyLuat.Text;
            string strNoiDungKyLuat = txtNoiDungKyLuat.Text;
            string strHinhPhat = txtHinhPhat.Text;

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
                if (intIdNhanVien == 0)
                {
                    MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(dNgayKyLuat))
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(strNoiDungKyLuat))
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((bool)grdTien.IsChecked == false && (bool)grdKhac.IsChecked == false)
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_8, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((bool)grdTien.IsChecked)
            {
                if (!regex.IsMatch(strHinhPhat))
                {
                    MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            kyLuatNhanVien.IdNhanVien = intIdNhanVien;
            kyLuatNhanVien.NgayKyLuat = Util.CnvObjToDatetime(dNgayKyLuat);
            kyLuatNhanVien.NoiDungKyLuat = strNoiDungKyLuat;
            kyLuatNhanVien.HinhPhat = strHinhPhat;
            kyLuatNhanVien.Id = Util.CnvObjToInt32(lblIdKyLuatNhanVien.Content);

            if ((bool)grdTien.IsChecked)
            {
                kyLuatNhanVien.LoaiHinhPhat = 1;
            }
            else
            {
                kyLuatNhanVien.LoaiHinhPhat = 2;
            }

            bool updateKyLuatNhanVien = _kyLuat.Edit(kyLuatNhanVien);
            if (updateKyLuatNhanVien)
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _kyLuat.getAllKyLuatNhanVienVM();
        }

        private void btnXoaKyLuatNV_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                KyLuatNhanVienVM item = (KyLuatNhanVienVM)dataGrid1.SelectedItem;
                bool deleteKyLuatNhanVien = _kyLuat.Delete(item.Id);
                if (deleteKyLuatNhanVien)
                {
                    MessageBox.Show(Msg.KYLUATNHANVIEN_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.KYLUATNHANVIEN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _kyLuat.getAllKyLuatNhanVienVM();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                KyLuatNhanVienVM item = (KyLuatNhanVienVM)dataGrid1.SelectedItem;

                cbbNhanVien.SelectedItem = _nhanVien.Detail(item.IdNhanVien);
                dateNgayKyLuat.Text = Util.CnvObjToString(item.NgayKyLuat);
                txtNoiDungKyLuat.Text = item.NoiDungKyLuat;
                txtHinhPhat.Text = item.HinhPhat;

                if (item.LoaiHinhPhat == 1)
                {
                    grdTien.IsChecked = true;
                }
                else
                {
                    grdKhac.IsChecked = true;
                }

                lblIdKyLuatNhanVien.Content = item.Id;
            }
        }
    }
}
