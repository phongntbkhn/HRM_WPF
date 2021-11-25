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
    /// Interaction logic for RP_LuongNhanVien.xaml
    /// </summary>
    public partial class RP_LuongNhanVien : Window
    {
        NhanVienProvider _nhanVien = new NhanVienProvider();
        CongSoProvider _congSo = new CongSoProvider();
        LuongNhanVienProvider _luongNV = new LuongNhanVienProvider();
        LuongProvider _dmLuong = new LuongProvider();

        public RP_LuongNhanVien()
        {
            InitializeComponent();
            ReportDocument report = new ReportDocument();
            report.Load("../../SourceCrystalReport/rpLuongNhanVien.rpt");
            rpLuongNV.ViewerCore.ReportSource = report;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            List<LuongNhanVienVM> luongNVs = _luongNV.getDataLuongNvDaInsert(Util.CnvObjToInt32(txtThang.Text), Util.CnvObjToInt32(txtNam.Text));
            ReportDocument report = new ReportDocument();
            report.Load("../../SourceCrystalReport/rpLuongNhanVien.rpt");

            List<crystalLuongNV> crystal_LuongNVs = new List<crystalLuongNV>();
            foreach (var item in luongNVs)
            {
                crystalLuongNV crystal_LuongNV = new crystalLuongNV();
                crystal_LuongNV.HoTen = item.TenNhanVien;
                crystal_LuongNV.SoDienThoai = item.SoDienThoai;
                crystal_LuongNV.ThangLuong = Util.CnvObjToInt32(item.ThangLuong);
                crystal_LuongNV.NamLuong = Util.CnvObjToInt32(item.NamLuong) ;
                crystal_LuongNV.TienLuong = Util.CnvObjToDecimal(item.TienLuongNV) ;
                crystal_LuongNVs.Add(crystal_LuongNV);
            }

            report.SetDataSource(crystal_LuongNVs);

            rpLuongNV.ViewerCore.ReportSource = report;
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
