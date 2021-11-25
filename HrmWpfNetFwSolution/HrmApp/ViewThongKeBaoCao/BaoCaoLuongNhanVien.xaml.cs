using DataLayer.Data;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Excel = Microsoft.Office.Interop.Excel;
using Utilities.Constants;
using System.IO;
using Spire.Xls;
using System.Globalization;

namespace HrmApp.ViewThongKeBaoCao
{
    /// <summary>
    /// Interaction logic for BaoCaoLuongNhanVien.xaml
    /// </summary>
    public partial class BaoCaoLuongNhanVien : Window
    {
        NhanVienProvider _nhanVien = new NhanVienProvider();
        CongSoProvider _congSo = new CongSoProvider();
        LuongNhanVienProvider _luongNV = new LuongNhanVienProvider();
        LuongProvider _dmLuong = new LuongProvider();

        public BaoCaoLuongNhanVien()
        {
            InitializeComponent();

            int intMonth = DateTime.Now.Month;
            int intYear = DateTime.Now.Year;

            txtThang.Text = Util.CnvObjToString(intMonth);
            txtNam.Text = Util.CnvObjToString(intYear);

            getDanhSachLuongNhanVien(intMonth, intYear);
        }

        public void getDanhSachLuongNhanVien(int month, int year)
        {
            List<LuongNhanVienVM> congSoVMs = _luongNV.getDataLuongNvDaInsert(month, year);
            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = congSoVMs;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            getDanhSachLuongNhanVien(Util.CnvObjToInt32(txtThang.Text), Util.CnvObjToInt32(txtNam.Text));
        }

        #region Setting textbox input only number
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            try
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string folderTemplate = Directory.GetParent(startupPath).Parent.FullName;
                string pathFile = folderTemplate + @"\TemplateExcel\TIEN_LUONG_NHAN_VIEN.xls";
                string outPathFile = folderTemplate + @"\ExportExcel\TIEN_LUONG_NHAN_VIEN.xls";

                List<LuongNhanVienVM> luongNhanVienVMs = _luongNV.getDataLuongNvDaInsert(Util.CnvObjToInt32(txtThang.Text), Util.CnvObjToInt32(txtNam.Text));

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(pathFile);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                int i = 8;
                // Tên công ty, địa chỉ, số điện thoại
                xlWorkSheet.Cells[1, 1] = ConstantsValue._TEN_CONG_TY;
                xlWorkSheet.Cells[2, 1] = ConstantsValue._DIACHI_CONG_TY;
                xlWorkSheet.Cells[3, 1] = ConstantsValue._SODIENTHOAI_CONG_TY;

                // Năm
                xlWorkSheet.Cells[5, 1] = "THÁNG : " + $"{Util.CnvObjToInt32(txtThang.Text)}/{Util.CnvObjToInt32(txtNam.Text)}";

                foreach (var item in luongNhanVienVMs)
                {
                    xlWorkSheet.Cells[i, 1] = (i + 1).ToString();

                    xlWorkSheet.Cells[i, 2] = item.TenNhanVien;
                    xlWorkSheet.Cells[i, 3] = item.SoDienThoai;
                    xlWorkSheet.Cells[i, 4] = Util.CnvObjToDecimal(item.TienLuongNV).ToString("#,#", CultureInfo.InvariantCulture);
                    i++;
                }

                xlWorkSheet.Cells[i + 1, 3] = ConstantsValue._NGUOI_LAP_BAO_CAO;


                Excel.Range r = xlWorkSheet.get_Range("A8", "D" + (i - 1).ToString());
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;

                xlWorkBook.SaveAs(outPathFile, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show($"File đã được tạo , bạn có thể tìm file theo đường dẫn: {outPathFile}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnExportPDF_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            try
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string folderTemplate = Directory.GetParent(startupPath).Parent.FullName;
                string dateNow = DateTime.Now.ToString("yyyyMMdd");
                string pathFile = folderTemplate + @"\TemplateExcel\TIEN_LUONG_NHAN_VIEN.xls";
                string outPathFile = folderTemplate + @"\ExportExcel\TIEN_LUONG_NHAN_VIEN_" + dateNow + ".xls";

                List<LuongNhanVienVM> luongNhanVienVMs = _luongNV.getDataLuongNvDaInsert(Util.CnvObjToInt32(txtThang.Text), Util.CnvObjToInt32(txtNam.Text));

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(pathFile);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                int i = 8;
                // Tên công ty, địa chỉ, số điện thoại
                xlWorkSheet.Cells[1, 1] = ConstantsValue._TEN_CONG_TY;
                xlWorkSheet.Cells[2, 1] = ConstantsValue._DIACHI_CONG_TY;
                xlWorkSheet.Cells[3, 1] = ConstantsValue._SODIENTHOAI_CONG_TY;

                // Năm
                xlWorkSheet.Cells[5, 1] = "THÁNG : " + $"{Util.CnvObjToInt32(txtThang.Text)}/{Util.CnvObjToInt32(txtNam.Text)}";

                foreach (var item in luongNhanVienVMs)
                {
                    xlWorkSheet.Cells[i, 1] = (i + 1).ToString();

                    xlWorkSheet.Cells[i, 2] = item.TenNhanVien;
                    xlWorkSheet.Cells[i, 3] = item.SoDienThoai;
                    xlWorkSheet.Cells[i, 4] = Util.CnvObjToDecimal(item.TienLuongNV).ToString("#,#", CultureInfo.InvariantCulture);
                    i++;
                }

                xlWorkSheet.Cells[i + 1, 3] = ConstantsValue._NGUOI_LAP_BAO_CAO;


                Excel.Range r = xlWorkSheet.get_Range("A8", "D" + (i - 1).ToString());
                r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                r.Borders.Weight = Excel.XlBorderWeight.xlThin;

                xlWorkBook.SaveAs(outPathFile, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                // Convert Excel to PDF
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(outPathFile, ExcelVersion.Version2016);

                //Worksheet sheet = workbook.Worksheets[0];
                string outPdf = folderTemplate + @"\ExportPdf\TIEN_LUONG_NHAN_VIEN_"+ dateNow + ".pdf";
                workbook.SaveToFile(outPdf, FileFormat.PDF);

                //Delete Temp Excel 
                if (File.Exists(outPathFile))
                {
                    File.Delete(outPathFile);
                }

                MessageBox.Show($"File Export PDF đã được tạo , bạn có thể tìm file theo đường dẫn: {outPdf}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export PDF bị lỗi", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            RP_LuongNhanVien rpLuongNV = new RP_LuongNhanVien();

            rpLuongNV.Owner = this;
            rpLuongNV.ShowDialog();
        }
    }
}
