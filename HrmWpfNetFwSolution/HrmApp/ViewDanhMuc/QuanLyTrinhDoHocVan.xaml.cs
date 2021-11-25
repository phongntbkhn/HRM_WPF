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
    /// Interaction logic for TrinhDoHocVan.xaml
    /// </summary>
    public partial class QuanLyTrinhDoHocVan : Window
    {
        TrinhDoHocVanProvider _trinh_do_hoc_van = new TrinhDoHocVanProvider();

        public QuanLyTrinhDoHocVan()
        {
            InitializeComponent();

            dataGrid1.ItemsSource = getDanhSachTrinhDoHocVan();
        }

        public List<TRINH_DO_HOC_VAN> getDanhSachTrinhDoHocVan()
        {
            List<TRINH_DO_HOC_VAN> trinhDoHocVans = new List<TRINH_DO_HOC_VAN>();
            trinhDoHocVans = _trinh_do_hoc_van.getAll();

            return trinhDoHocVans;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTimKiem = txtTimKiem.Text;
            List<TRINH_DO_HOC_VAN> trinhDoHocVans = _trinh_do_hoc_van.getAll();
            trinhDoHocVans = trinhDoHocVans.Where(s => s.TenTDHV.Contains(strTimKiem)
            ).ToList();

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = trinhDoHocVans;
        }

        private void btnThemTrinhDo_Click(object sender, RoutedEventArgs e)
        {
            TRINH_DO_HOC_VAN trinhDoHocVan = new TRINH_DO_HOC_VAN();

            string strTenTdhv = txtTenTrinhDo.Text;

            if (string.IsNullOrWhiteSpace(strTenTdhv))
            {
                MessageBox.Show(Msg.TRINHDOHOCVAN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            trinhDoHocVan.TenTDHV = strTenTdhv;

            bool insertTDHV = _trinh_do_hoc_van.Create(trinhDoHocVan);
            if (insertTDHV)
            {
                MessageBox.Show(Msg.TRINHDOHOCVAN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.TRINHDOHOCVAN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _trinh_do_hoc_van.getAll();
        }

        private void btnCapNhatTrinhDo_Click(object sender, RoutedEventArgs e)
        {
            TRINH_DO_HOC_VAN trinhDoHocVan = new TRINH_DO_HOC_VAN();

            string strTrinhDoHocVan = txtTenTrinhDo.Text;

            if (string.IsNullOrWhiteSpace(strTrinhDoHocVan))
            {
                MessageBox.Show(Msg.TRINHDOHOCVAN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            trinhDoHocVan.TenTDHV = strTrinhDoHocVan;
            trinhDoHocVan.Id = Util.CnvObjToInt32(lblTrinhDoHocVan.Content);

            bool updateTDHV = _trinh_do_hoc_van.Edit(trinhDoHocVan);
            if (updateTDHV)
            {
                MessageBox.Show(Msg.TRINHDOHOCVAN_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.TRINHDOHOCVAN_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _trinh_do_hoc_van.getAll();
        }

        private void btnXoaTrinhDo_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                TRINH_DO_HOC_VAN item = (TRINH_DO_HOC_VAN)dataGrid1.SelectedItem;
                bool deleteTDHV = _trinh_do_hoc_van.Delete(item.Id);
                if (deleteTDHV)
                {
                    MessageBox.Show(Msg.TRINHDOHOCVAN_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.TRINHDOHOCVAN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _trinh_do_hoc_van.getAll();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                TRINH_DO_HOC_VAN item = (TRINH_DO_HOC_VAN)dataGrid1.SelectedItem;

                txtTenTrinhDo.Text = item.TenTDHV;

                lblTrinhDoHocVan.Content = item.Id;
            }
        }
    }
}
