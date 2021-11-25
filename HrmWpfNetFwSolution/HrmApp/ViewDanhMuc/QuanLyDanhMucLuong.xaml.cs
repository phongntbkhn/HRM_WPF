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
    /// Interaction logic for QuanLyDanhMucLuong.xaml
    /// </summary>
    public partial class QuanLyDanhMucLuong : Window
    {
        LuongProvider _luong = new LuongProvider();

        public QuanLyDanhMucLuong()
        {
            InitializeComponent();

            dataGrid1.ItemsSource = getDanhSachLuong();
        }

        public List<LUONG> getDanhSachLuong()
        {
            List<LUONG> luongs = new List<LUONG>();
            luongs = _luong.getAll();

            return luongs;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTimKiem = txtTimKiem.Text;
            List<LUONG> luongs = _luong.getAll();
            luongs = luongs.Where(s => s.TenDmLuong.Contains(strTimKiem)
            || s.LuongCoBan.Equals(strTimKiem)
            || s.PhuCapLuong.Equals(strTimKiem)
            ).ToList();

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = luongs;
        }

        private void btnThemDmLuong_Click(object sender, RoutedEventArgs e)
        {
            LUONG luong = new LUONG();

            string strDmLuong = txtTenDmLuong.Text;
            string strHeSoLuong = txtHeSoLuong.Text;
            string strLuongCoBan = txtLuongCoBan.Text;
            string strPhuCap = txtPhuCap.Text;

            if (string.IsNullOrWhiteSpace(strDmLuong))
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(strHeSoLuong))
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(strLuongCoBan))
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(strPhuCap))
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            luong.TenDmLuong = strDmLuong;
            luong.HeSoLuong = Util.CnvObjToDecimal(strHeSoLuong);
            luong.LuongCoBan = Util.CnvObjToDecimal(strLuongCoBan);
            luong.PhuCapLuong = Util.CnvObjToDecimal(strPhuCap);

            bool insertDmLuong = _luong.Create(luong);
            if (insertDmLuong)
            {
                MessageBox.Show(Msg.DMLUONG_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _luong.getAll();
        }

        private void btnCapNhatDmLuong_Click(object sender, RoutedEventArgs e)
        {
            LUONG luong = new LUONG();

            string strDmLuong = txtTenDmLuong.Text;
            string strHeSoLuong = txtHeSoLuong.Text;
            string strLuongCoBan = txtLuongCoBan.Text;
            string strPhuCap = txtPhuCap.Text;

            if (string.IsNullOrWhiteSpace(strDmLuong))
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(strHeSoLuong))
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(strLuongCoBan))
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(strPhuCap))
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            luong.TenDmLuong = strDmLuong;
            luong.HeSoLuong = Util.CnvObjToDecimal(strHeSoLuong);
            luong.LuongCoBan = Util.CnvObjToDecimal(strLuongCoBan);
            luong.PhuCapLuong = Util.CnvObjToDecimal(strPhuCap);
            luong.Id = Util.CnvObjToInt32(lblIdDmLuong.Content);

            bool updateDmLuong = _luong.Edit(luong);
            if (updateDmLuong)
            {
                MessageBox.Show(Msg.DMLUONG_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.DMLUONG_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _luong.getAll();
        }

        private void btnXoaDmLuong_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                LUONG item = (LUONG)dataGrid1.SelectedItem;
                bool deleteLuong = _luong.Delete(item.Id);
                if (deleteLuong)
                {
                    MessageBox.Show(Msg.DMLUONG_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.DMLUONG_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _luong.getAll();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                LUONG item = (LUONG)dataGrid1.SelectedItem;

                txtTenDmLuong.Text = item.TenDmLuong;
                txtHeSoLuong.Text = Util.CnvObjToString(item.HeSoLuong);
                txtLuongCoBan.Text = Util.CnvObjToString(item.LuongCoBan);
                txtPhuCap.Text = Util.CnvObjToString(item.PhuCapLuong);

                lblIdDmLuong.Content = item.Id;
            }
        }
    }
}
