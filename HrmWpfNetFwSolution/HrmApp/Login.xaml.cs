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
using System.Windows.Shapes;
using Utilities.Variable;

namespace HrmApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public TaiKhoanProvider _taiKhoan = new TaiKhoanProvider();

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string strTaiKhoan = txtTaiKhoan.Text;
            string strMatKhau = txtMatKhau.Password;
            string message = "";

            var taiKhoan = _taiKhoan.checkLogin(strTaiKhoan, strMatKhau, out message);

            if (taiKhoan != null)
            {
                GlobalVale.taiKhoan = taiKhoan;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Owner = this;
                this.Hide();
                mainWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show(message, "Thông báo");
            }
        }
    }
}
