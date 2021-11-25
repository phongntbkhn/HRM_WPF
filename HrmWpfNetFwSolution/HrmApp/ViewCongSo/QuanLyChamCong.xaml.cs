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
using Utilities.Messages;
using Utilities.Utils;
using ViewModel;

namespace HrmApp.ViewCongSo
{
    /// <summary>
    /// Interaction logic for QuanLyChamCong.xaml
    /// </summary>
    public partial class QuanLyChamCong : Window
    {
        NhanVienProvider _nhanVien = new NhanVienProvider();
        CongSoProvider _congSo = new CongSoProvider();

        public QuanLyChamCong()
        {
            InitializeComponent();

            string dateTimeNow = DateTime.Now.Date.ToString("MM-dd-yyyy");
            dateNgayChamCong.Text = dateTimeNow;

            List<CongSoVM> congSoVMs = _congSo.getDataCongSoDaInsert(Util.CnvObjToDatetime(dateNgayChamCong.Text));
            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();

            if (nhanViens.Count > congSoVMs.Count)
            {
                insertEmptyCongSo(nhanViens, congSoVMs);
            }
            getDanhSachNhanVien();
        }

        public void getDanhSachNhanVien()
        {
            List<CongSoVM> congSoVMs = _congSo.getAllCongSoVMByDate(Util.CnvObjToDatetime(dateNgayChamCong.Text));
            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = congSoVMs;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTenNhanVien = txtTenNhanVienTimKiem.Text;
            string strSoDienThoai = txtSoDienThoaiTimKiem.Text;

            List<CongSoVM> congSoVMs = _congSo.getAllCongSoVMByDate(Util.CnvObjToDatetime(dateNgayChamCong.Text));

            if (!string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                congSoVMs = congSoVMs.Where(s => s.TenNhanVien.Contains(strTenNhanVien)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(strSoDienThoai))
            {
                congSoVMs = congSoVMs.Where(s => s.SoDienThoai.Contains(strSoDienThoai)).ToList();
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = congSoVMs;
        }

        private void btnNhapCongSo_Click(object sender, RoutedEventArgs e)
        {
            int countCongSo = 0;
            int countCongSoFalse = 0;
            foreach (CongSoVM item in dataGrid1.Items)
            {
                try
                {
                    int intId = Util.CnvObjToInt32(item.Id);
                    string strInTime = Util.CnvObjToDatetime(item.InTime).ToString("HH:mm");
                    string strOutTime = Util.CnvObjToDatetime(item.OutTime).ToString("HH:mm");

                    CONG_SO congSo = new CONG_SO();
                    congSo.IdNhanVien = item.IdNhanVien;
                    if (!string.IsNullOrWhiteSpace(dateNgayChamCong.Text))
                    {
                        congSo.NgayChamCong = Util.CnvObjToDatetime(dateNgayChamCong.Text);
                    }
                    else
                    {
                        congSo.NgayChamCong = null;
                    }
                    if (item.NgayChamCong != null)
                    {
                        strInTime = Util.CnvObjToDatetime(item.NgayChamCong).ToString("MM-dd-yyyy") + " " + strInTime + ":00";
                    }

                    DateTime dateInTime = Util.CnvObjToDatetime(strInTime);
                    congSo.InTime = dateInTime;
                    if (item.NgayChamCong != null)
                    {
                        strOutTime = Util.CnvObjToDatetime(item.NgayChamCong).ToString("MM-dd-yyyy") + " " + strOutTime + ":00";
                    }

                    DateTime dateOutTime = Util.CnvObjToDatetime(strOutTime);
                    congSo.OutTime = dateOutTime;
                    congSo.State = item.State;
                    congSo.IsCoPhep = item.IsCoPhep;
                    congSo.OtTime = item.OtTime;
                    congSo.Note = item.Note;

                    if (congSo.NgayChamCong == null)
                    {
                        countCongSoFalse++;
                        continue;
                    }

                    if (congSo.State == null)
                    {
                        countCongSoFalse++;
                        continue;
                    }

                    if (intId == 0)
                    {
                        var insertCongSo = _congSo.Create(congSo);
                    }
                    else
                    {
                        congSo.Id = intId;
                        var updateCongSo = _congSo.Edit(congSo);
                    }
                    countCongSo++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Msg.NHAPCONGSO_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (countCongSo > 0)
            {
                MessageBox.Show(String.Format(Msg.NHAPCONGSO_THANHCONG, countCongSo), Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else if (countCongSoFalse > 0)
            {
                MessageBox.Show(String.Format(Msg.NHAPCONGSO_ERROR_1, countCongSoFalse), Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            dataGrid1.CanUserAddRows = false;
            List<CongSoVM> congSoVMs = _congSo.getAllCongSoVMByDate(Util.CnvObjToDatetime(dateNgayChamCong.Text));
            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = congSoVMs;
        }

        private void btnHuongDan_Click(object sender, RoutedEventArgs e)
        {
            HuongDanNhapCongSo huongDanNhapCongSo = new HuongDanNhapCongSo();
            huongDanNhapCongSo.Owner = this;
            huongDanNhapCongSo.ShowDialog();
        }

        private void insertEmptyCongSo(List<NHAN_VIEN> lstNV, List<CongSoVM> lstCongSoVM)
        {
            List<CONG_SO> congSos = new List<CONG_SO>();
            foreach (var item in lstNV)
            {
                var cs = lstCongSoVM.FirstOrDefault(s => s.IdNhanVien == item.Id);
                if (cs == null)
                {
                    CONG_SO congSo = new CONG_SO();
                    congSo.IdNhanVien = item.Id;
                    congSo.NgayChamCong = Util.CnvObjToDatetime(dateNgayChamCong.Text);
                    congSo.InTime = null;
                    congSo.OutTime = null;
                    congSo.State = null;
                    congSo.IsCoPhep = null;
                    congSo.OtTime = null;
                    congSo.Note = null;
                    congSos.Add(congSo);
                }
            }
            var insertEmptyCongSo = _congSo.InsertEmptyCongSo(congSos);
        }

        private void dateNgayChamCong_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            List<CongSoVM> congSoVMs = _congSo.getDataCongSoDaInsert(Util.CnvObjToDatetime(dateNgayChamCong.Text));
            List<NHAN_VIEN> nhanViens = _nhanVien.getAll();

            if (nhanViens.Count > congSoVMs.Count)
            {
                insertEmptyCongSo(nhanViens, congSoVMs);
            }
            getDanhSachNhanVien();
        }
    }
}
