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

namespace HrmApp.ViewDanhMuc
{
    /// <summary>
    /// Interaction logic for QuanLyLoaiHopDong.xaml
    /// </summary>
    public partial class QuanLyLoaiHopDong : Window
    {
        LoaiHopDongProvider _loaiHopDong = new LoaiHopDongProvider();

        public QuanLyLoaiHopDong()
        {
            InitializeComponent();

            dataGrid1.ItemsSource = getDanhSachLoaiHopDong();
        }

        public List<LOAI_HOP_DONG> getDanhSachLoaiHopDong()
        {
            List<LOAI_HOP_DONG> loaiHopDongs = new List<LOAI_HOP_DONG>();
            loaiHopDongs = _loaiHopDong.getAll();

            return loaiHopDongs;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTimKiem = txtTimKiem.Text;
            List<LOAI_HOP_DONG> loaiHopDongs = _loaiHopDong.getAll();
            loaiHopDongs = loaiHopDongs.Where(s => s.TenLoaiHopDong.Contains(strTimKiem)
            ).ToList();

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = loaiHopDongs;
        }

        private void btnThemLoaiHopDong_Click(object sender, RoutedEventArgs e)
        {
            LOAI_HOP_DONG loaiHopDongs = new LOAI_HOP_DONG();

            string strTenLoaiHopdong = txtLoaiHopDong.Text;

            if (string.IsNullOrWhiteSpace(strTenLoaiHopdong))
            {
                MessageBox.Show(Msg.LOAIHOPDONG_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            loaiHopDongs.TenLoaiHopDong = strTenLoaiHopdong;

            bool insertLoaiHopDong = _loaiHopDong.Create(loaiHopDongs);
            if (insertLoaiHopDong)
            {
                MessageBox.Show(Msg.LOAIHOPDONG_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.LOAIHOPDONG_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _loaiHopDong.getAll();
        }

        private void btnCapNhatLoaiHopDong_Click(object sender, RoutedEventArgs e)
        {
            LOAI_HOP_DONG loaiHopDong = new LOAI_HOP_DONG();

            string strTenLoaiHopDong = txtLoaiHopDong.Text;

            if (string.IsNullOrWhiteSpace(strTenLoaiHopDong))
            {
                MessageBox.Show(Msg.LOAIHOPDONG_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            loaiHopDong.TenLoaiHopDong = strTenLoaiHopDong;
            loaiHopDong.Id = Util.CnvObjToInt32(lblIdLoaiHopDong.Content);

            bool updateTDHV = _loaiHopDong.Edit(loaiHopDong);
            if (updateTDHV)
            {
                MessageBox.Show(Msg.LOAIHOPDONG_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.LOAIHOPDONG_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _loaiHopDong.getAll();
        }

        private void btnXoaLoaiHopDong_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                LOAI_HOP_DONG item = (LOAI_HOP_DONG)dataGrid1.SelectedItem;
                bool deleteLoaiHopDong = _loaiHopDong.Delete(item.Id);
                if (deleteLoaiHopDong)
                {
                    MessageBox.Show(Msg.LOAIHOPDONG_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.LOAIHOPDONG_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _loaiHopDong.getAll();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                LOAI_HOP_DONG item = (LOAI_HOP_DONG)dataGrid1.SelectedItem;

                txtLoaiHopDong.Text = item.TenLoaiHopDong;

                lblIdLoaiHopDong.Content = item.Id;
            }
        }
    }
}