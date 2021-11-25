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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities.Messages;
using Utilities.Variable;
using ViewModel;

namespace HrmApp.ViewTaiKhoan
{
    /// <summary>
    /// Interaction logic for CapNhatTaiKhoan.xaml
    /// </summary>
    public partial class DoiMatKhauTaiKhoan : Window
    {
        TaiKhoanProvider _taiKhoan = new TaiKhoanProvider();

        public DoiMatKhauTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnCapNhatMatKhau_Click(object sender, RoutedEventArgs e)
        {
            TAI_KHOAN taiKhoan = GlobalVale.taiKhoan;
            string strMatKhauCu = txtMatKhauCu.Password;
            string strMatKhauMoi = txtMatKhauMoi.Password;
            string strMatKhauNhapLai = txtMatKhauNhapLai.Password;

            if (taiKhoan != null)
            {
                if (strMatKhauCu.Equals(taiKhoan.MatKhau))
                {
                    if (!strMatKhauMoi.Equals(strMatKhauNhapLai))
                    {
                        MessageBox.Show(Msg.TAIKHOAN_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        taiKhoan.MatKhau = strMatKhauMoi;
                        bool updateMatKhau = _taiKhoan.Edit(taiKhoan);
                        if (updateMatKhau)
                        {
                            MessageBox.Show(Msg.TAIKHOAN_DOI_MAT_KHAU_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                        }
                        else
                        {
                            MessageBox.Show(Msg.TAIKHOAN_ERROR_8, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Msg.TAIKHOAN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
