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
    /// Interaction logic for QuanLyChucVu.xaml
    /// </summary>
    public partial class QuanLyChucVu : Window
    {
        HumanResourceManagementDbContext _context;
        ChucVuProvider _chucVu = new ChucVuProvider();

        public QuanLyChucVu()
        {
            InitializeComponent();

            dataGrid1.ItemsSource = getDanhSachchucVu();
        }

        public List<CHUC_VU> getDanhSachchucVu()
        {
            List<CHUC_VU> chucVus = new List<CHUC_VU>();
            chucVus = _chucVu.getAll();

            return chucVus;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTimKiem = txtTimKiem.Text;
            List<CHUC_VU> chucVus = _chucVu.getAll();
            chucVus = chucVus.Where(s => s.TenChucVu.Contains(strTimKiem)
            ).ToList();

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = chucVus;
        }

        private void btnThemChucVu_Click(object sender, RoutedEventArgs e)
        {
            CHUC_VU chucVus = new CHUC_VU();

            string strTenChucVu = txtTenChucVu.Text;

            if (string.IsNullOrWhiteSpace(strTenChucVu))
            {
                MessageBox.Show(Msg.CHUCVU_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            chucVus.TenChucVu = strTenChucVu;

            bool insertTDHV = _chucVu.Create(chucVus);
            if (insertTDHV)
            {
                MessageBox.Show(Msg.CHUCVU_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.CHUCVU_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _chucVu.getAll();
        }

        private void btnCapNhatChucVu_Click(object sender, RoutedEventArgs e)
        {
            CHUC_VU chucVu = new CHUC_VU();

            string strChucVu = txtTenChucVu.Text;

            if (string.IsNullOrWhiteSpace(strChucVu))
            {
                MessageBox.Show(Msg.CHUCVU_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            chucVu.TenChucVu = strChucVu;
            chucVu.Id = Util.CnvObjToInt32(lblIdChucVu.Content);

            bool updateTDHV = _chucVu.Edit(chucVu);
            if (updateTDHV)
            {
                MessageBox.Show(Msg.CHUCVU_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.CHUCVU_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _chucVu.getAll();
        }

        private void btnXoaChucVu_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                CHUC_VU item = (CHUC_VU)dataGrid1.SelectedItem;
                bool deleteChucVu = _chucVu.Delete(item.Id);
                if (deleteChucVu)
                {
                    MessageBox.Show(Msg.CHUCVU_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.CHUCVU_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _chucVu.getAll();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                CHUC_VU item = (CHUC_VU)dataGrid1.SelectedItem;

                txtTenChucVu.Text = item.TenChucVu;

                lblIdChucVu.Content = item.Id;
            }
        }
    }
}
