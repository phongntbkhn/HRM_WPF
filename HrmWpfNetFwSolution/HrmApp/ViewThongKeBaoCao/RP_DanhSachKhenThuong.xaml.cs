using CrystalDecisions.CrystalReports.Engine;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities.Utils;
using ViewModel;
using ViewModel.CrystalReport;

namespace HrmApp.ViewThongKeBaoCao
{
    /// <summary>
    /// Interaction logic for RP_DanhSachKhenThuong.xaml
    /// </summary>
    public partial class RP_DanhSachKhenThuong : Window
    {
        KhenThuongNhanVienProvider _khenThuong = new KhenThuongNhanVienProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        public RP_DanhSachKhenThuong()
        {
            InitializeComponent();

            ReportDocument report = new ReportDocument();
            report.Load("../../SourceCrystalReport/rpDanhSachKhenThuong.rpt");
            crDsKhenThuong.ViewerCore.ReportSource = report;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            List<KhenThuongNhanVienVM> khenThuongNhanVienVMs = getDanhSachKhenThuongNhanVien(Util.CnvObjToInt32(txtThang.Text), Util.CnvObjToInt32(txtNam.Text));

            ReportDocument report = new ReportDocument();
            report.Load("../../SourceCrystalReport/rpDanhSachKhenThuong.rpt");

            List<crystal_KhenThuongNV> crystal_KhenThuongNVs = new List<crystal_KhenThuongNV>();
            foreach (var item in khenThuongNhanVienVMs)
            {
                crystal_KhenThuongNV crystal_KhenThuongNV = new crystal_KhenThuongNV();
                crystal_KhenThuongNV.HoTen = item.TenNhanVien;
                if (item.NgayKhenThuong == null)
                {
                    crystal_KhenThuongNV.NgayKhenThuong = new DateTime().ToString("yyyy-MM-dd");
                }
                else
                {
                    crystal_KhenThuongNV.NgayKhenThuong = Util.CnvObjToDatetime(item.NgayKhenThuong).ToString("yyyy-MM-dd"); ;
                }
                crystal_KhenThuongNV.NoiDungKhenThuong = item.NoiDungKhenThuong;
                crystal_KhenThuongNV.LoaiPT = item.TenLoaiPhanThuong;
                crystal_KhenThuongNV.PhanThuong = item.PhanThuong;
                crystal_KhenThuongNVs.Add(crystal_KhenThuongNV);
            }

            report.SetDataSource(crystal_KhenThuongNVs);

            crDsKhenThuong.ViewerCore.ReportSource = report;
        }

        public List<KhenThuongNhanVienVM> getDanhSachKhenThuongNhanVien(int month, int year)
        {
            List<KhenThuongNhanVienVM> khenThuongNhanVienVMs = new List<KhenThuongNhanVienVM>();
            if (month == 0 || year == 0)
            {
                return khenThuongNhanVienVMs;
            }
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lasttDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            khenThuongNhanVienVMs = _khenThuong.getAllKhenThuongNhanVienVM()
                .Where(s => s.NgayKhenThuong >= firstDayOfMonth && s.NgayKhenThuong <= lasttDayOfMonth).ToList();
            return khenThuongNhanVienVMs;
        }

        #region Setting textbox input only number
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}
