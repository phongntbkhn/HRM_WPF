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
using Utilities.Messages;
using Utilities.Utils;
using ViewModel;

namespace HrmApp.ViewDuAn
{
    /// <summary>
    /// Interaction logic for QuanLyDuAn.xaml
    /// </summary>
    public partial class QuanLyDuAn : Window
    {
        DuAnProvider _duAn = new DuAnProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        public QuanLyDuAn()
        {
            InitializeComponent();

            getDanhSachDuAn();

            bindingNhanVien();
        }

        public void getDanhSachDuAn()
        {
            List<DuAnVM> duAns = _duAn.getAllDuAnVM();
            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = duAns;
        }

        public void bindingNhanVien()
        {
            List<NHAN_VIEN> nhanViens = new List<NHAN_VIEN>();
            nhanViens = _nhanVien.getAll();
            cbbNhanVien.ItemsSource = nhanViens;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTenDuAn = txtTenDuAnTimKiem.Text;
            string strTenNguoiQuanLyTimKiem = txtTenNguoiQuanLyTimKiem.Text;

            List<DuAnVM> duAnVMs = _duAn.getAllDuAnVM();

            if (!string.IsNullOrWhiteSpace(strTenDuAn))
            {
                duAnVMs = duAnVMs.Where(s => s.TenDuAn.Contains(strTenDuAn)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(strTenNguoiQuanLyTimKiem))
            {
                duAnVMs = duAnVMs.Where(s => s.TenNguoiQuanLy.Contains(strTenNguoiQuanLyTimKiem)).ToList();
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = duAnVMs;
        }

        private void btnThemDuAn_Click(object sender, RoutedEventArgs e)
        {
            DU_AN duAn = new DU_AN();

            string strTenDuAn = txtTenDuAn.Text;
            string strTenKhachHang = txtTenKhachHang.Text;
            string dNgayBatDau = dateNgayBatDau.Text;
            string dNgayKetThuc = dateNgayKetThuc.Text;
            string strMoTa = txtMoTa.Text;
            string strDanhSachNV = txtDanhSachNhanVien.Text;
            int idNhanVien = 0;

            if (string.IsNullOrWhiteSpace(strTenDuAn))
            {
                MessageBox.Show(Msg.DUAN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(dNgayBatDau))
            {
                MessageBox.Show(Msg.DUAN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                idNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
                if (idNhanVien == 0)
                {
                    MessageBox.Show(Msg.DUAN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show(Msg.DUAN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            duAn.TenDuAn = strTenDuAn;
            duAn.KhanhHang = strTenKhachHang;
            duAn.NgayBatDau = Util.CnvObjToDatetime(dNgayBatDau);
            duAn.NgayKetThuc = Util.CnvObjToDatetime(dNgayKetThuc);
            duAn.MoTa = strMoTa;
            duAn.IdNguoiQuanLy = idNhanVien;
            duAn.DanhSachNhanVien = strDanhSachNV;

            bool insertDuAn = _duAn.Create(duAn);
            if (insertDuAn)
            {
                MessageBox.Show(Msg.DUAN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.DUAN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update Data in Datagrid
            getDanhSachDuAn();
        }

        private void btnCapNhatDuAn_Click(object sender, RoutedEventArgs e)
        {
            DU_AN duAn = new DU_AN();

            string strTenDuAn = txtTenDuAn.Text;
            string strTenKhachHang = txtTenKhachHang.Text;
            string dNgayBatDau = dateNgayBatDau.Text;
            string dNgayKetThuc = dateNgayKetThuc.Text;
            string strMoTa = txtMoTa.Text;
            string strDanhSachNV = txtDanhSachNhanVien.Text;
            int idNhanVien = 0;

            if (string.IsNullOrWhiteSpace(strTenDuAn))
            {
                MessageBox.Show(Msg.DUAN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(dNgayBatDau))
            {
                MessageBox.Show(Msg.DUAN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                idNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
                if (idNhanVien == 0)
                {
                    MessageBox.Show(Msg.DUAN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            duAn.id = Util.CnvObjToInt32(lblIdDuAn.Content);
            duAn.TenDuAn = strTenDuAn;
            duAn.KhanhHang = strTenKhachHang;
            duAn.NgayBatDau = Util.CnvObjToDatetime(dNgayBatDau);
            duAn.NgayKetThuc = Util.CnvObjToDatetime(dNgayKetThuc);
            duAn.MoTa = strMoTa;
            duAn.IdNguoiQuanLy = idNhanVien;
            duAn.DanhSachNhanVien = strDanhSachNV;

            bool insertDuAn = _duAn.Edit(duAn);
            if (insertDuAn)
            {
                MessageBox.Show(Msg.DUAN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.DUAN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update Data in Datagrid
            getDanhSachDuAn();
        }

        private void btnXoaDuAn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                DuAnVM item = (DuAnVM)dataGrid1.SelectedItem;
                bool deleteDuAn = _duAn.Delete(item.Id);
                if (deleteDuAn)
                {
                    MessageBox.Show(Msg.DUAN_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.DUAN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Update Data in Datagrid
                getDanhSachDuAn();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                DuAnVM item = (DuAnVM)dataGrid1.SelectedItem;
                txtTenDuAn.Text = item.TenDuAn;
                txtTenKhachHang.Text = item.KhanhHang;
                dateNgayBatDau.Text = Util.CnvObjToString(item.NgayBatDau);
                dateNgayKetThuc.Text = Util.CnvObjToString(item.NgayKetThuc);
                txtMoTa.Text = item.MoTa;
                cbbNhanVien.SelectedItem = _nhanVien.Detail(Util.CnvObjToInt32(item.IdNguoiQuanLy));
                txtDanhSachNhanVien.Text = item.DanhSachNhanVien;

                lblIdDuAn.Content = item.Id;
            }
        }
    }
}
