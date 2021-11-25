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
    /// Interaction logic for RP_DanhSachKyLuat.xaml
    /// </summary>
    public partial class RP_DanhSachKyLuat : Window
    {

        KyLuatNhanVienProvider _kyLuat = new KyLuatNhanVienProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        public RP_DanhSachKyLuat()
        {
            InitializeComponent();

            ReportDocument report = new ReportDocument();
            report.Load("../../SourceCrystalReport/rpDanhSachKyLuat.rpt");
            rpDsKyLuat.ViewerCore.ReportSource = report;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            List<KyLuatNhanVienVM> kyLuatVMs = getDanhSachKyLuatNhanVien(Util.CnvObjToInt32(txtThang.Text), Util.CnvObjToInt32(txtNam.Text));

            ReportDocument report = new ReportDocument();
            report.Load("../../SourceCrystalReport/rpDanhSachKyLuat.rpt");

            List<crystal_KyLuatNV> crystal_KhenThuongNVs = new List<crystal_KyLuatNV>();
            foreach (var item in kyLuatVMs)
            {
                crystal_KyLuatNV crystal_KyLuatNV = new crystal_KyLuatNV();
                crystal_KyLuatNV.HoTen = item.TenNhanVien;
                if (item.NgayKyLuat == null)
                {
                    crystal_KyLuatNV.NgayKyLuat = new DateTime().ToString("yyyy-MM-dd");
                }
                else
                {
                    crystal_KyLuatNV.NgayKyLuat = Util.CnvObjToDatetime(item.NgayKyLuat).ToString("yyyy-MM-dd"); ;
                }
                crystal_KyLuatNV.NoiDungKyLuat = item.NoiDungKyLuat;
                crystal_KyLuatNV.LoaiHinhPhat = item.TenLoaiHinhPhat;
                crystal_KyLuatNV.HinhPhat = item.HinhPhat;
                crystal_KhenThuongNVs.Add(crystal_KyLuatNV);
            }

            report.SetDataSource(crystal_KhenThuongNVs);

            rpDsKyLuat.ViewerCore.ReportSource = report;
        }

        public List<KyLuatNhanVienVM> getDanhSachKyLuatNhanVien(int month, int year)
        {
            List<KyLuatNhanVienVM> kyLuatNvVMs = new List<KyLuatNhanVienVM>();
            if (month == 0 || year == 0)
            {
                return kyLuatNvVMs;
            }
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lasttDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            kyLuatNvVMs = _kyLuat.getAllKyLuatNhanVienVM()
                .Where(s => s.NgayKyLuat >= firstDayOfMonth && s.NgayKyLuat <= lasttDayOfMonth).ToList();
            return kyLuatNvVMs;
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
