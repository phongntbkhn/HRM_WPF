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
    /// Interaction logic for QuanLyDanToc.xaml
    /// </summary>
    public partial class QuanLyDanToc : Window
    {
        DanTocProvider _danToc = new DanTocProvider();

        public QuanLyDanToc()
        {
            InitializeComponent();

            dataGrid1.ItemsSource = getDanhSachDanToc();
        }

        public List<DAN_TOC> getDanhSachDanToc()
        {
            List<DAN_TOC> danTocs = new List<DAN_TOC>();
            danTocs = _danToc.getAll();

            return danTocs;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTimKiem = txtTimKiem.Text;
            List<DAN_TOC> danTocs = _danToc.getAll();
            danTocs = danTocs.Where(s => s.TenDanToc.Contains(strTimKiem)
            ).ToList();

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = danTocs;
        }

        private void btnThemDanToc_Click(object sender, RoutedEventArgs e)
        {
            DAN_TOC danToc = new DAN_TOC();

            string strTenDanToc = txtTenDanToc.Text;

            if (string.IsNullOrWhiteSpace(strTenDanToc))
            {
                MessageBox.Show(Msg.DANTOC_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            danToc.TenDanToc = strTenDanToc;

            bool insertDanToc = _danToc.Create(danToc);
            if (insertDanToc)
            {
                MessageBox.Show(Msg.DANTOC_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.DANTOC_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _danToc.getAll();
        }

        private void btnCapNhatDanToc_Click(object sender, RoutedEventArgs e)
        {
            DAN_TOC danToc = new DAN_TOC();

            string strDanToc = txtTenDanToc.Text;

            if (string.IsNullOrWhiteSpace(strDanToc))
            {
                MessageBox.Show(Msg.DANTOC_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            danToc.TenDanToc = strDanToc;
            danToc.Id = Util.CnvObjToInt32(lblIdDanToc.Content);

            bool updateDanToc = _danToc.Edit(danToc);
            if (updateDanToc)
            {
                MessageBox.Show(Msg.DANTOC_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.DANTOC_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _danToc.getAll();
        }

        private void btnXoaDanToc_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                DAN_TOC item = (DAN_TOC)dataGrid1.SelectedItem;
                bool deleteDanToc = _danToc.Delete(item.Id);
                if (deleteDanToc)
                {
                    MessageBox.Show(Msg.DANTOC_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.DANTOC_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _danToc.getAll();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                DAN_TOC item = (DAN_TOC)dataGrid1.SelectedItem;

                txtTenDanToc.Text = item.TenDanToc;

                lblIdDanToc.Content = item.Id;
            }
        }
    }
}
