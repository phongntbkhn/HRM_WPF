using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HrmApp.ViewThongKeBaoCao
{
    /// <summary>
    /// Interaction logic for RP_DanhSachNhanVien.xaml
    /// </summary>
    public partial class RP_DanhSachNhanVien : Window
    {
        public RP_DanhSachNhanVien()
        {
            InitializeComponent();

            ReportDocument report = new ReportDocument();
            report.Load("../../SourceCrystalReport/rpDanhSachNhanVien.rpt");
            //List<TaiKhoan> taiKhoans = new List<TaiKhoan>()
            //{
            //    new TaiKhoan(){Id = 1, TenDangNhap = "phong"},
            //    new TaiKhoan(){Id = 2, TenDangNhap = "phong2"},
            //    new TaiKhoan(){Id = 3, TenDangNhap = "phong3"}
            //};
            //report.SetDataSource(taiKhoans);
            crDsNhanVien.ViewerCore.ReportSource = report;
        }
    }
}
