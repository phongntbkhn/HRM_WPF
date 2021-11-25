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
using Utilities.Messages;
using Utilities.Utils;
using Utilities.Variable;
using ViewModel;

namespace HrmApp.ViewCongSo
{
    /// <summary>
    /// Interaction logic for TinhLuongNhanVien.xaml
    /// </summary>
    public partial class TinhLuongNhanVien : Window
    {
        NhanVienProvider _nhanVien = new NhanVienProvider();
        CongSoProvider _congSo = new CongSoProvider();
        LuongNhanVienProvider _luongNV = new LuongNhanVienProvider();
        LuongProvider _dmLuong = new LuongProvider();

        public TinhLuongNhanVien()
        {
            InitializeComponent();

            int intMonth = DateTime.Now.Month;
            int intYear = DateTime.Now.Year;

            txtThang.Text = Util.CnvObjToString(intMonth);
            txtNam.Text = Util.CnvObjToString(intYear);

            List<LuongNhanVienVM> luongNhanVienVMs = _luongNV.getDataLuongNvDaInsert(intMonth, intYear);
            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();

            if (nhanViens.Count > luongNhanVienVMs.Count)
            {
                insertEmptyLuongNhanVien(nhanViens, luongNhanVienVMs, intMonth, intYear);
            }
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
            string strTenNhanVien = txtTenNhanVienTimKiem.Text;
            string strSoDienThoai = txtSoDienThoaiTimKiem.Text;

            List<LuongNhanVienVM> luongNhanVienVMs = _luongNV.getDataLuongNvDaInsert(
                Util.CnvObjToInt32(txtThang.Text)
                , Util.CnvObjToInt32(txtNam.Text));

            if (!string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                luongNhanVienVMs = luongNhanVienVMs.Where(s => s.TenNhanVien.Contains(strTenNhanVien)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(strSoDienThoai))
            {
                luongNhanVienVMs = luongNhanVienVMs.Where(s => s.SoDienThoai.Contains(strSoDienThoai)).ToList();
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = luongNhanVienVMs;
        }

        private void insertEmptyLuongNhanVien(List<NHAN_VIEN> lstNV, List<LuongNhanVienVM> luongNhanVienVMs, int month, int year)
        {
            List<LUONG_NHAN_VIEN> luongNhanViens = new List<LUONG_NHAN_VIEN>();
            foreach (var item in lstNV)
            {
                var cs = luongNhanVienVMs.FirstOrDefault(s => s.IdNhanVien == item.Id);
                if (cs == null)
                {
                    LUONG_NHAN_VIEN luongNv = new LUONG_NHAN_VIEN();
                    luongNv.IdNhanVien = item.Id;
                    luongNv.ThangLuong = month;
                    luongNv.NamLuong = year;
                    luongNv.TienLuong = 0;
                    luongNhanViens.Add(luongNv);
                }
            }
            var insertEmptyCongSo = _luongNV.InsertEmptyCongSo(luongNhanViens);
        }


        private void btnTinhLuong_Click(object sender, RoutedEventArgs e)
        {
            int countTinhLuongNhanVienOK = 0;
            int countTinhLuongNhanVienFalse = 0;
            foreach (LuongNhanVienVM item in dataGrid1.Items)
            {
                try
                {
                    int intId = Util.CnvObjToInt32(item.Id);
                    int intMonth = Util.CnvObjToInt32(txtThang.Text);
                    int intYear = Util.CnvObjToInt32(txtNam.Text);

                    LUONG_NHAN_VIEN luongNhanVien = new LUONG_NHAN_VIEN();
                    luongNhanVien.IdNhanVien = item.IdNhanVien;
                    luongNhanVien.ThangLuong = intMonth;
                    luongNhanVien.NamLuong = intYear;

                    DateTime startDateInMonth = new DateTime(intYear, intMonth, 1);
                    DateTime endDateInMonth = startDateInMonth.AddMonths(1).AddDays(-1);

                    decimal tienLuong = _luongNV.getTienLuongNhanVienByThang(item.IdNhanVien, startDateInMonth, endDateInMonth, GlobalVale.SO_GIO_LAM_1THANG);
                    luongNhanVien.TienLuong = tienLuong;

                    if (intId == 0)
                    {
                        var insertCongSo = _luongNV.Create(luongNhanVien);
                    }
                    else
                    {
                        luongNhanVien.id = intId;
                        var updateCongSo = _luongNV.Edit(luongNhanVien);
                    }
                    countTinhLuongNhanVienOK++;
                }
                catch (Exception ex)
                {
                    countTinhLuongNhanVienFalse++;
                }
                finally
                {

                }
            }
            if (countTinhLuongNhanVienOK > 0)
            {
                MessageBox.Show(String.Format(Msg.TINHLUONGNHANVIEN_THANHCONG, countTinhLuongNhanVienOK), Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else if (countTinhLuongNhanVienFalse > 0)
            {
                MessageBox.Show(String.Format(Msg.TINHLUONGNHANVIEN_THATBAI, countTinhLuongNhanVienFalse), Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            getDanhSachLuongNhanVien(Util.CnvObjToInt32(txtThang.Text), Util.CnvObjToInt32(txtNam.Text));
        }

        #region Setting textbox input only number
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        private void txtThang_TextChanged(object sender, TextChangedEventArgs e)
        {
            int intMonth = Util.CnvObjToInt32(txtThang.Text);
            int intYear = Util.CnvObjToInt32(txtNam.Text);
            List<LuongNhanVienVM> luongNhanVienVMs = _luongNV.getDataLuongNvDaInsert(intMonth, intYear);
            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();

            if (nhanViens.Count > luongNhanVienVMs.Count)
            {
                insertEmptyLuongNhanVien(nhanViens, luongNhanVienVMs, intMonth, intYear);
            }
            getDanhSachLuongNhanVien(intMonth, intYear);
        }

        private void txtNam_TextChanged(object sender, TextChangedEventArgs e)
        {
            int intMonth = Util.CnvObjToInt32(txtThang.Text);
            int intYear = Util.CnvObjToInt32(txtNam.Text);

            List<LuongNhanVienVM> luongNhanVienVMs = _luongNV.getDataLuongNvDaInsert(intMonth, intYear);
            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();

            if (nhanViens.Count > luongNhanVienVMs.Count)
            {
                insertEmptyLuongNhanVien(nhanViens, luongNhanVienVMs, intMonth, intYear);
            }
            getDanhSachLuongNhanVien(intMonth, intYear);
        }
    }
}
