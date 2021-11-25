using DataLayer.Data;
using DataLayer.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using Utilities.Constants;
using Utilities.Utils;
using ViewModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace HrmApp.ViewThongKeBaoCao
{
    /// <summary>
    /// Interaction logic for ReportKhenThuongNhanVien.xaml
    /// </summary>
    public partial class ReportKhenThuongNhanVien : Window
    {
        KhenThuongNhanVienProvider _khenThuong = new KhenThuongNhanVienProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        public ReportKhenThuongNhanVien()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            List<KhenThuongNhanVienVM> khenThuongNhanVienVMs = getDanhSachKhenThuongNhanVien(Util.CnvObjToInt32(txtThang.Text), Util.CnvObjToInt32(txtNam.Text));
            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = khenThuongNhanVienVMs;
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

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            try
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string folderTemplate = Directory.GetParent(startupPath).Parent.FullName;
                string dateNow = DateTime.Now.ToString("yyyyMMdd");
                string pathFile = folderTemplate + @"\TemplateExcel\BC_KHEN_THUONG_NHAN_VIEN.xls";
                string outPathFile = folderTemplate + @"\ExportExcel\BC_KHEN_THUONG_NHAN_VIEN_" + dateNow + ".xls";

                List<KhenThuongNhanVienVM> khenThuongNhanVienVMs = (List<KhenThuongNhanVienVM>)dataGrid1.ItemsSource;

                if (khenThuongNhanVienVMs == null)
                {
                    khenThuongNhanVienVMs = new List<KhenThuongNhanVienVM>();
                }

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

                foreach (var item in khenThuongNhanVienVMs)
                {
                    xlWorkSheet.Cells[i, 1] = (i + 1).ToString();

                    xlWorkSheet.Cells[i, 2] = item.TenNhanVien;
                    xlWorkSheet.Cells[i, 3] = item.NgayKhenThuong;
                    xlWorkSheet.Cells[i, 4] = item.NoiDungKhenThuong;
                    xlWorkSheet.Cells[i, 5] = item.TenLoaiPhanThuong;
                    if (item.LoaiPhanThuong == 1)
                    {
                        xlWorkSheet.Cells[i, 6] = Util.CnvObjToDecimal(item.PhanThuong).ToString("#,#", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        xlWorkSheet.Cells[i, 6] = item.PhanThuong;
                    }
                    i++;
                }

                xlWorkSheet.Cells[i + 1, 5] = ConstantsValue._NGUOI_LAP_BAO_CAO;


                Excel.Range r = xlWorkSheet.get_Range("A8", "F" + (i - 1).ToString());
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
            Microsoft.Office.Interop.Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            try
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string folderTemplate = Directory.GetParent(startupPath).Parent.FullName;
                string dateNow = DateTime.Now.ToString("yyyyMMdd");
                string pathFile = folderTemplate + @"\TemplateExcel\BC_KHEN_THUONG_NHAN_VIEN.xls";
                string outPathFile = folderTemplate + @"\ExportExcel\TEMP_BC_KHEN_THUONG_NHAN_VIEN_" + dateNow + ".xls";

                List<KhenThuongNhanVienVM> khenThuongNhanVienVMs = (List<KhenThuongNhanVienVM>)dataGrid1.ItemsSource;

                if (khenThuongNhanVienVMs == null)
                {
                    khenThuongNhanVienVMs = new List<KhenThuongNhanVienVM>();
                }

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

                foreach (var item in khenThuongNhanVienVMs)
                {
                    xlWorkSheet.Cells[i, 1] = (i + 1).ToString();

                    xlWorkSheet.Cells[i, 2] = item.TenNhanVien;
                    xlWorkSheet.Cells[i, 3] = item.NgayKhenThuong;
                    xlWorkSheet.Cells[i, 4] = item.NoiDungKhenThuong;
                    xlWorkSheet.Cells[i, 5] = item.TenLoaiPhanThuong;
                    if (item.LoaiPhanThuong == 1)
                    {
                        xlWorkSheet.Cells[i, 6] = Util.CnvObjToDecimal(item.PhanThuong).ToString("#,#", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        xlWorkSheet.Cells[i, 6] = item.PhanThuong;
                    }
                    i++;
                }

                xlWorkSheet.Cells[i + 1, 5] = ConstantsValue._NGUOI_LAP_BAO_CAO;


                Excel.Range r = xlWorkSheet.get_Range("A8", "F" + (i - 1).ToString());
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
                string outPdf = folderTemplate + @"\ExportPdf\BC_KHEN_THUONG_NHAN_VIEN_" + dateNow + ".pdf";
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
            RP_DanhSachKhenThuong rP_DanhSachKhenThuong = new RP_DanhSachKhenThuong();

            rP_DanhSachKhenThuong.Owner = this;
            rP_DanhSachKhenThuong.ShowDialog();
        }
    }
}
