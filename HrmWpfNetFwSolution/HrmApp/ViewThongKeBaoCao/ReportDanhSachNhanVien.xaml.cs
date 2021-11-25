using DataLayer.Data;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using ViewModel;
using System.Linq;
using ViewModel.BaoCaoThongKe;
using System.Data;
using Utilities.Utils;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Utilities.Constants;
using Spire.Xls;


namespace HrmApp.ViewThongKeBaoCao
{
    /// <summary>
    /// Interaction logic for ReportDanhSachNhanVien.xaml
    /// </summary>
    public partial class ReportDanhSachNhanVien : Window
    {
        NhanVienProvider _nhanVien = new NhanVienProvider();
        ChucVuProvider _chucVu = new ChucVuProvider();
        PhongBanProvider _phongBan = new PhongBanProvider();
        TrinhDoHocVanProvider _trinhDo = new TrinhDoHocVanProvider();

        public ReportDanhSachNhanVien()
        {
            InitializeComponent();

            lblTenCongTy.Content = ConstantsValue._TEN_CONG_TY;
            lblDiaChiCongTy.Content = ConstantsValue._DIACHI_CONG_TY;
            lblSoDienThoai.Content = ConstantsValue._SODIENTHOAI_CONG_TY;
            getDanhSachNhanVien();
        }

        public void getDanhSachNhanVien()
        {
            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = getAllListReportNhanVienVM();
        }

        public List<ReportNhanVienVM> getAllListReportNhanVienVM()
        {
            List<NhanVienVM> nhanViens = _nhanVien.getAllNhanVienVM();
            if (nhanViens != null)
            {
                nhanViens = nhanViens.OrderBy(s => s.IdChucVu).ToList();
            }

            List<ReportNhanVienVM> ReportNhanVienVMs = new List<ReportNhanVienVM>();
            int stt = 1;
            foreach (var item in nhanViens)
            {
                ReportNhanVienVM ReportNhanVienVM = new ReportNhanVienVM();
                ReportNhanVienVM.STT = stt;
                ReportNhanVienVM.Id = item.Id;
                ReportNhanVienVM.HoTen = item.HoTen;
                ReportNhanVienVM.NgaySinh = item.NgaySinh;
                ReportNhanVienVM.QueQuan = item.QueQuan;
                ReportNhanVienVM.GioiTinh = item.GioiTinh;
                ReportNhanVienVM.IdDanToc = item.IdDanToc;
                ReportNhanVienVM.TenDanToc = item.TenDanToc;
                ReportNhanVienVM.SoDienThoai = item.SoDienThoai;
                ReportNhanVienVM.IdPhongBan = item.IdPhongBan;
                ReportNhanVienVM.PhongBan = item.PhongBan;
                ReportNhanVienVM.IdChucVu = item.IdChucVu;
                ReportNhanVienVM.ChucVu = item.ChucVu;
                ReportNhanVienVM.IdTrinhDoHocVan = item.IdTrinhDoHocVan;
                ReportNhanVienVM.TrinhDoHocVan = item.TrinhDoHocVan;
                ReportNhanVienVM.IdDmLuong = item.IdDmLuong;
                ReportNhanVienVM.DmLuong = item.DmLuong;
                ReportNhanVienVMs.Add(ReportNhanVienVM);
                stt++;
            }

            return ReportNhanVienVMs;
        }

        private void btnXuatExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string folderTemplate = Directory.GetParent(startupPath).Parent.FullName;
            string pathFile = folderTemplate + @"\TemplateExcel\DANH_SACH_NHAN_VIEN.xls";
            string outPathFile = folderTemplate + @"\ExportExcel\DANH_SACH_NHAN_VIEN.xls";

            List<ReportNhanVienVM> reportNhanVienVMs = getAllListReportNhanVienVM();

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(pathFile);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int i = 8;
            // Tên công ty, địa chỉ, số điện thoại
            xlWorkSheet.Cells[1, 1] = Util.CnvObjToString(lblTenCongTy.Content);
            xlWorkSheet.Cells[2, 1] = Util.CnvObjToString(lblDiaChiCongTy.Content);
            xlWorkSheet.Cells[3, 1] = Util.CnvObjToString(lblSoDienThoai.Content);

            // Năm
            xlWorkSheet.Cells[3, 1] = "NĂM: " + Util.CnvObjToString(DateTime.Now.Year);

            foreach (var item in reportNhanVienVMs)
            {
                xlWorkSheet.Cells[i, 1] = (i + 1).ToString();

                xlWorkSheet.Cells[i, 2] = item.ChucVu;
                xlWorkSheet.Cells[i, 3] = item.Id;
                xlWorkSheet.Cells[i, 4] = item.HoTen;
                xlWorkSheet.Cells[i, 5] = item.PhongBan;
                xlWorkSheet.Cells[i, 6] = item.TrinhDoHocVan;
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

        private void btnXuatPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string folderTemplate = Directory.GetParent(startupPath).Parent.FullName;
                string dateNow = DateTime.Now.ToString("yyyyMMdd");
                string pathFile = folderTemplate + @"\TemplateExcel\DANH_SACH_NHAN_VIEN.xls";
                string outPathFile = folderTemplate + @"\ExportExcel\TEMP_DANH_SACH_NHAN_VIEN_" + dateNow + ".xls";

                List<ReportNhanVienVM> reportNhanVienVMs = getAllListReportNhanVienVM();

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(pathFile);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                int i = 8;
                // Tên công ty, địa chỉ, số điện thoại
                xlWorkSheet.Cells[1, 1] = Util.CnvObjToString(lblTenCongTy.Content);
                xlWorkSheet.Cells[2, 1] = Util.CnvObjToString(lblDiaChiCongTy.Content);
                xlWorkSheet.Cells[3, 1] = Util.CnvObjToString(lblSoDienThoai.Content);

                // Năm
                xlWorkSheet.Cells[4, 1] = "NĂM: " + Util.CnvObjToString(DateTime.Now.Year);

                foreach (var item in reportNhanVienVMs)
                {
                    xlWorkSheet.Cells[i, 1] = (i + 1).ToString();

                    xlWorkSheet.Cells[i, 2] = item.ChucVu;
                    xlWorkSheet.Cells[i, 3] = item.Id;
                    xlWorkSheet.Cells[i, 4] = item.HoTen;
                    xlWorkSheet.Cells[i, 5] = item.PhongBan;
                    xlWorkSheet.Cells[i, 6] = item.TrinhDoHocVan;
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
                string outPdf = folderTemplate + @"\ExportPdf\DANH_SACH_NHAN_VIEN_" + dateNow + ".pdf";
                workbook.SaveToFile(outPdf, FileFormat.PDF);

                //Delete Temp Excel 
                if (File.Exists(outPathFile))
                {
                    File.Delete(outPathFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export PDF bị lỗi", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            RP_DanhSachNhanVien rP_DanhSachNhanVien = new RP_DanhSachNhanVien();

            rP_DanhSachNhanVien.Owner = this;
            rP_DanhSachNhanVien.ShowDialog();
        }
    }
}
