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
    /// Interaction logic for QuanLyPhongBan.xaml
    /// </summary>
    public partial class QuanLyPhongBan : Window
    {
        PhongBanProvider _phongBan = new PhongBanProvider();

        public QuanLyPhongBan()
        {
            InitializeComponent();

            dataGrid1.ItemsSource = getDanhSachPhongBan();
        }

        public List<PHONG_BAN> getDanhSachPhongBan()
        {
            List<PHONG_BAN> phongBans = new List<PHONG_BAN>();
            phongBans = _phongBan.getAll();

            return phongBans;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTimKiem = txtTimKiem.Text;
            List<PHONG_BAN> phongBans = _phongBan.getAll();
            phongBans = phongBans.Where(s => s.TenPB.Contains(strTimKiem)
            || s.DiaChiPB.Contains(strTimKiem)
            || s.SdtPB.Contains(strTimKiem)
            ).ToList();

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = phongBans;
        }

        private void btnThemPhongBan_Click(object sender, RoutedEventArgs e)
        {
            PHONG_BAN phongBan = new PHONG_BAN();

            string strTenPhongBan = txtTenPhongBan.Text;
            string strDiaChi = txtDiachiPhongBan.Text;
            string strSoDienThoai = txtSoDienThoai.Text;

            if (string.IsNullOrWhiteSpace(strTenPhongBan))
            {
                MessageBox.Show(Msg.PHONGBAN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            phongBan.TenPB = strTenPhongBan;
            phongBan.DiaChiPB = strDiaChi;
            phongBan.SdtPB = strSoDienThoai;

            bool insertPhongBan = _phongBan.Create(phongBan);
            if (insertPhongBan)
            {
                MessageBox.Show(Msg.PHONGBAN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.PHONGBAN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _phongBan.getAll();
        }

        private void btnCapNhatPhongBan_Click(object sender, RoutedEventArgs e)
        {
            PHONG_BAN phongBan = new PHONG_BAN();

            string strTenPhongBan = txtTenPhongBan.Text;
            string strDiaChi = txtDiachiPhongBan.Text;
            string strSoDienThoai = txtSoDienThoai.Text;

            if (string.IsNullOrWhiteSpace(strTenPhongBan))
            {
                MessageBox.Show(Msg.PHONGBAN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            phongBan.TenPB = strTenPhongBan;
            phongBan.DiaChiPB = strDiaChi;
            phongBan.SdtPB = strSoDienThoai;
            phongBan.Id = Util.CnvObjToInt32(lblIdPhongBan.Content);

            bool updatePhongBan = _phongBan.Edit(phongBan);
            if (updatePhongBan)
            {
                MessageBox.Show(Msg.PHONGBAN_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.PHONGBAN_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _phongBan.getAll();
        }

        private void btnXoaPhongBan_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                PHONG_BAN item = (PHONG_BAN)dataGrid1.SelectedItem;
                bool deletePhongBan = _phongBan.Delete(item.Id);
                if (deletePhongBan)
                {
                    MessageBox.Show(Msg.PHONGBAN_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.PHONGBAN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _phongBan.getAll();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                PHONG_BAN item = (PHONG_BAN)dataGrid1.SelectedItem;

                txtTenPhongBan.Text = item.TenPB;
                txtDiachiPhongBan.Text = item.DiaChiPB;
                txtSoDienThoai.Text = item.SdtPB;

                lblIdPhongBan.Content = item.Id;
            }
        }
    }
}
