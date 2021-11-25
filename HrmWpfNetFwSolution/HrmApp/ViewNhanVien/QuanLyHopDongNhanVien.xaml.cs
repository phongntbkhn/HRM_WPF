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

namespace HrmApp.ViewNhanVien
{
    /// <summary>
    /// Interaction logic for QuanLyHopDongNhanVien.xaml
    /// </summary>
    public partial class QuanLyHopDongNhanVien : Window
    {
        NhanVienProvider _nhanVien = new NhanVienProvider();
        LoaiHopDongProvider _loaiHopDong = new LoaiHopDongProvider();
        NhanVienHopDongProvider _nvHopDong = new NhanVienHopDongProvider();

        public QuanLyHopDongNhanVien()
        {
            InitializeComponent();

            bindingCbbNhanVien();
            bindingCbbLoaiHopDong();
            getDanhSachHopDongNhanVien();

        }

        public void bindingCbbNhanVien()
        {
            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();
            cbbNhanVien.ItemsSource = nhanViens;
        }

        public void bindingCbbLoaiHopDong()
        {
            List<LOAI_HOP_DONG> loaiHopDongs = _loaiHopDong.getAll();
            cbbLoaiHopDong.ItemsSource = loaiHopDongs;
        }

        public void getDanhSachHopDongNhanVien()
        {
            List<HopDongNhanVienVM> nhanVienHopDongs = _nvHopDong.getAllNhanVienHopDongs();

            dataGrid1.ItemsSource = nhanVienHopDongs;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTenNhanVien = txtTenNhanVienTimKiem.Text;
            string strLoaiHopDong = txtLoaiHopDongTimKiem.Text;

            List<HopDongNhanVienVM> hopDongNhanViens = _nvHopDong.getAllNhanVienHopDongs();

            if (!string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                hopDongNhanViens = hopDongNhanViens.Where(s => s.HoTen.Contains(strTenNhanVien)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(strLoaiHopDong))
            {
                hopDongNhanViens = hopDongNhanViens.Where(s => s.TenLoaiHopDong.Contains(strLoaiHopDong)).ToList();
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = hopDongNhanViens;
        }

        private void btnThemHopDong_Click(object sender, RoutedEventArgs e)
        {
            NHAN_VIEN_HOP_DONG nvHopDong = new NHAN_VIEN_HOP_DONG();

            int intIdNhanVien = 0;
            int intIdHopDong = 0;

            string dTuNgay = dateTuNgay.Text;
            string dDenNgay = dateDenNgay.Text;

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
                if (intIdNhanVien == 0)
                {
                    MessageBox.Show(Msg.HOPDONGNHANVIEN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            LOAI_HOP_DONG loaiHopDongSelected = (LOAI_HOP_DONG)cbbLoaiHopDong.SelectedItem;
            if (loaiHopDongSelected != null)
            {
                intIdHopDong = Util.CnvObjToInt32(loaiHopDongSelected.Id);
                if (intIdHopDong == 0)
                {
                    MessageBox.Show(Msg.HOPDONGNHANVIEN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            nvHopDong.IdNhanVien = intIdNhanVien;
            nvHopDong.IdLoaiHopDong = intIdHopDong;
            nvHopDong.TuNgay = Util.CnvObjToDatetime(dTuNgay);
            nvHopDong.DenNgay = Util.CnvObjToDatetime(dDenNgay);

            bool insertHopDongNhanVien = _nvHopDong.Create(nvHopDong);
            if (insertHopDongNhanVien)
            {
                MessageBox.Show(Msg.HOPDONGNHANVIEN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.HOPDONGNHANVIEN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _nvHopDong.getAllNhanVienHopDongs();
        }

        private void btnCapNhatHopDong_Click(object sender, RoutedEventArgs e)
        {
            NHAN_VIEN_HOP_DONG nvHopDong = new NHAN_VIEN_HOP_DONG();

            int intIdNhanVien = 0;
            int intIdHopDong = 0;

            string dTuNgay = dateTuNgay.Text;
            string dDenNgay = dateDenNgay.Text;

            NHAN_VIEN nhanVienSelected = (NHAN_VIEN)cbbNhanVien.SelectedItem;
            if (nhanVienSelected != null)
            {
                intIdNhanVien = Util.CnvObjToInt32(nhanVienSelected.Id);
                if (intIdNhanVien == 0)
                {
                    MessageBox.Show(Msg.HOPDONGNHANVIEN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            LOAI_HOP_DONG loaiHopDongSelected = (LOAI_HOP_DONG)cbbLoaiHopDong.SelectedItem;
            if (loaiHopDongSelected != null)
            {
                intIdHopDong = Util.CnvObjToInt32(loaiHopDongSelected.Id);
                if (intIdHopDong == 0)
                {
                    MessageBox.Show(Msg.HOPDONGNHANVIEN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            nvHopDong.IdNhanVien = intIdNhanVien;
            nvHopDong.IdLoaiHopDong = intIdHopDong;
            nvHopDong.TuNgay = Util.CnvObjToDatetime(dTuNgay);
            nvHopDong.DenNgay = Util.CnvObjToDatetime(dDenNgay);

            nvHopDong.Id = Util.CnvObjToInt32(lblIdHopDongNhanVien.Content);

            bool insertHopDongNhanVien = _nvHopDong.Edit(nvHopDong);
            if (insertHopDongNhanVien)
            {
                MessageBox.Show(Msg.HOPDONGNHANVIEN_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.HOPDONGNHANVIEN_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _nvHopDong.getAllNhanVienHopDongs();
        }

        private void btnXoaHopDong_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                HopDongNhanVienVM item = (HopDongNhanVienVM)dataGrid1.SelectedItem;
                bool deleteHopDongNhanVien = _nvHopDong.Delete(item.Id);
                if (deleteHopDongNhanVien)
                {
                    MessageBox.Show(Msg.HOPDONGNHANVIEN_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.HOPDONGNHANVIEN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _nvHopDong.getAllNhanVienHopDongs();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                HopDongNhanVienVM item = (HopDongNhanVienVM)dataGrid1.SelectedItem;

                cbbNhanVien.SelectedItem = _nhanVien.Detail(item.IdNhanVien);
                cbbLoaiHopDong.SelectedItem = _loaiHopDong.Detail(item.IdLoaiHopDong);

                dateTuNgay.Text = Util.CnvObjToString(item.TuNgay);
                dateDenNgay.Text = Util.CnvObjToString(item.DenNgay);
                lblIdHopDongNhanVien.Content = item.Id;
            }
        }
    }
}
