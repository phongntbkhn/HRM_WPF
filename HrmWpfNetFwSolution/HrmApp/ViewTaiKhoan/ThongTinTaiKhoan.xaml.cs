using DataLayer.Data;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities.Utils;
using Utilities.Variable;

namespace HrmApp.ViewTaiKhoan
{
    /// <summary>
    /// Interaction logic for ThongTinTaiKhoan.xaml
    /// </summary>
    public partial class ThongTinTaiKhoan : Window
    {
        TaiKhoanProvider _taiKhoan = new TaiKhoanProvider();
        QuyenProvider _quyen = new QuyenProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        public ThongTinTaiKhoan()
        {
            InitializeComponent();

            setData();
        }

        public void setData()
        {
            lblTenDangNhap.Content = GlobalVale.taiKhoan.TenDangNhap;
            lblTenHienThi.Content = GlobalVale.taiKhoan.TenHienThi;
            lblQuyen.Content = _quyen.Detail(Util.CnvObjToInt32(GlobalVale.taiKhoan.IdQuyen)).TEN_QUYEN;

        }
    }
}
