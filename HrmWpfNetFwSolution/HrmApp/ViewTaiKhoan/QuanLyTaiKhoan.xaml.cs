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

namespace HrmApp.ViewTaiKhoan
{
    /// <summary>
    /// Interaction logic for ThemMoiTaiKhoan.xaml
    /// </summary>
    public partial class QuanLyTaiKhoan : Window
    {
        TaiKhoanProvider _taiKhoan = new TaiKhoanProvider();
        QuyenProvider _quyen = new QuyenProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        public QuanLyTaiKhoan()
        {
            InitializeComponent();

            getDataTaiKhoanNguoiDung();

            List<QUYEN> quyens = _quyen.getAll();
            cbbQuyen.ItemsSource = quyens;

            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();
            cbbNhanVien.ItemsSource = nhanViens;
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

        private void btnDangKyTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            TAI_KHOAN taiKhoan = new TAI_KHOAN();

            string strTenDangNhap = txtTenDangNhap.Text;
            string strMatKhau = txtMatKhau.Password;
            string strTenHienThi = txtTenHienThi.Text;
            int intIdQuyen = 0;
            int intIdNhanVien = 0;

            QUYEN quyenSelected = (QUYEN)cbbQuyen.SelectedItem;
            if (quyenSelected != null)
            {
                intIdQuyen = Util.CnvObjToInt32(quyenSelected.Id);
            }

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
            }

            if (string.IsNullOrWhiteSpace(strTenDangNhap))
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(strMatKhau))
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (intIdQuyen == 0)
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (intIdNhanVien == 0)
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            taiKhoan.TenDangNhap = strTenDangNhap;
            taiKhoan.MatKhau = strMatKhau;
            taiKhoan.TenHienThi = strTenHienThi;
            if (string.IsNullOrWhiteSpace(strTenHienThi))
            {
                var nhanVien = _nhanVien.getById(intIdNhanVien);
                if (nhanVien != null)
                {
                    taiKhoan.TenHienThi = nhanVien.HoTen;
                }
            }
            taiKhoan.IdQuyen = intIdQuyen;
            taiKhoan.IdNhanVien = intIdNhanVien;

            bool ketQua = _taiKhoan.Create(taiKhoan);
            if (ketQua)
            {
                MessageBox.Show(Msg.TAIKHOAN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            getDataTaiKhoanNguoiDung();
        }

        private void btnCapNhatTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            TAI_KHOAN taiKhoan = new TAI_KHOAN();

            string strTenDangNhap = txtTenDangNhap.Text;
            string strMatKhau = txtMatKhau.Password;
            string strTenHienThi = txtTenHienThi.Text;
            int intIdQuyen = 0;
            int intIdNhanVien = 0;

            QUYEN quyenSelected = (QUYEN)cbbQuyen.SelectedItem;
            if (quyenSelected != null)
            {
                intIdQuyen = Util.CnvObjToInt32(quyenSelected.Id);
            }

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
            }

            if (string.IsNullOrWhiteSpace(strTenDangNhap))
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(strMatKhau))
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (intIdQuyen == 0)
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (intIdNhanVien == 0)
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            taiKhoan.TenDangNhap = strTenDangNhap;
            taiKhoan.MatKhau = strMatKhau;
            taiKhoan.TenHienThi = strTenHienThi;
            if (string.IsNullOrWhiteSpace(strTenHienThi))
            {
                var nhanVien = _nhanVien.getById(intIdNhanVien);
                if (nhanVien != null)
                {
                    taiKhoan.TenHienThi = nhanVien.HoTen;
                }
            }
            taiKhoan.IdQuyen = intIdQuyen;
            taiKhoan.IdNhanVien = intIdNhanVien;

            taiKhoan.Id = Util.CnvObjToInt32(lblIdTaiKhoan.Content);

            bool ketQua = _taiKhoan.Edit(taiKhoan);
            if (ketQua)
            {
                MessageBox.Show(Msg.TAIKHOAN_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            getDataTaiKhoanNguoiDung();
        }



        private void btnXoaTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            TAI_KHOAN taiKhoan = new TAI_KHOAN();

            string strTenDangNhap = txtTenDangNhap.Text;
            string strMatKhau = txtMatKhau.Password;
            string strTenHienThi = txtTenHienThi.Text;
            int intIdQuyen = 0;
            int intIdNhanVien = 0;

            QUYEN quyenSelected = (QUYEN)cbbQuyen.SelectedItem;
            if (quyenSelected != null)
            {
                intIdQuyen = Util.CnvObjToInt32(quyenSelected.Id);
            }

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
            }

            taiKhoan.TenDangNhap = strTenDangNhap;
            taiKhoan.MatKhau = strMatKhau;
            taiKhoan.TenHienThi = strTenHienThi;
            if (string.IsNullOrWhiteSpace(strTenHienThi))
            {
                var nhanVien = _nhanVien.getById(intIdNhanVien);
                if (nhanVien != null)
                {
                    taiKhoan.TenHienThi = nhanVien.HoTen;
                }
            }
            taiKhoan.IdQuyen = intIdQuyen;
            taiKhoan.IdNhanVien = intIdNhanVien;

            taiKhoan.Id = Util.CnvObjToInt32(lblIdTaiKhoan.Content);

            bool ketQua = _taiKhoan.Delete(Util.CnvObjToInt32(lblIdTaiKhoan.Content));
            if (ketQua)
            {
                MessageBox.Show(Msg.TAIKHOAN_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.TAIKHOAN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            getDataTaiKhoanNguoiDung();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                TaiKhoanVM item = (TaiKhoanVM)dataGrid1.SelectedItem;
                TAI_KHOAN taiKhoan = new TAI_KHOAN();

                taiKhoan = _taiKhoan.Detail(item.Id);

                txtTenDangNhap.Text = taiKhoan.TenDangNhap;
                txtMatKhau.Password = taiKhoan.MatKhau;
                txtTenHienThi.Text = taiKhoan.TenHienThi;

                cbbQuyen.SelectedItem = _quyen.Detail(Util.CnvObjToInt32(taiKhoan.IdQuyen));
                cbbNhanVien.SelectedItem = _nhanVien.Detail(Util.CnvObjToInt32(taiKhoan.IdNhanVien));

                lblIdTaiKhoan.Content = item.Id;
            }
        }
    }
}
