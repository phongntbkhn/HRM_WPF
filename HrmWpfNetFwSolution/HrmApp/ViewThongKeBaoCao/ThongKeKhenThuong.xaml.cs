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
using ViewModel.BaoCaoThongKe;
using Excel = Microsoft.Office.Interop.Excel;
using Utilities.Constants;
using System.IO;
using Utilities.Utils;

namespace HrmApp.ViewThongKeBaoCao
{
    /// <summary>
    /// Interaction logic for ThongKeKhenThuong.xaml
    /// </summary>
    public partial class ThongKeKhenThuong : Window
    {
        KhenThuongNhanVienProvider _khenThuong = new KhenThuongNhanVienProvider();
        NhanVienProvider _nhanVien = new NhanVienProvider();

        public ThongKeKhenThuong()
        {
            InitializeComponent();

            getBaoCaoKhenThuongNhanVien();
        }

        public void getBaoCaoKhenThuongNhanVien()
        {
            List<ThongKeKhenThuongNV> khenThuongNhanVienVMs = _khenThuong.getBaoCaoKhenThuongNhanVien();

            dataGrid1.ItemsSource = khenThuongNhanVienVMs;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTenNhanVien = txtTimKiem.Text;

            List<ThongKeKhenThuongNV> khenThuongNhanVienVMs = _khenThuong.getBaoCaoKhenThuongNhanVien();

            if (!string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                khenThuongNhanVienVMs = khenThuongNhanVienVMs.Where(s => s.TenNhanVien.Contains(strTenNhanVien)).ToList();
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = khenThuongNhanVienVMs;
        }

        private void btnExxportExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            try
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string folderTemplate = Directory.GetParent(startupPath).Parent.FullName;
                folderTemplate = Directory.GetParent(folderTemplate).FullName;
                string pathFile = folderTemplate + @"\TemplateExcel\KHEN_THUONG_NHAN_VIEN.xls";
                string outPathFile = folderTemplate + @"\ExportExcel\KHEN_THUONG_NHAN_VIEN.xls";

                string strTenNhanVien = txtTimKiem.Text;

                List<ThongKeKhenThuongNV> khenThuongNhanVienVMs = _khenThuong.getBaoCaoKhenThuongNhanVien();

                if (!string.IsNullOrWhiteSpace(strTenNhanVien))
                {
                    khenThuongNhanVienVMs = khenThuongNhanVienVMs.Where(s => s.TenNhanVien.Contains(strTenNhanVien)).ToList();
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
                xlWorkSheet.Cells[3, 1] = "Năm : " + Util.CnvObjToString(DateTime.Now.Year);

                foreach (var item in khenThuongNhanVienVMs)
                {
                    xlWorkSheet.Cells[i, 1] = (i + 1).ToString();

                    xlWorkSheet.Cells[i, 2] = item.TenNhanVien;
                    xlWorkSheet.Cells[i, 3] = item.SoLanKhenThuong;
                    i++;
                }

                xlWorkSheet.Cells[i + 1, 3] = ConstantsValue._NGUOI_LAP_BAO_CAO;


                Excel.Range r = xlWorkSheet.get_Range("A8", "C" + (i - 1).ToString());
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
    }
}
