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

namespace HrmApp.ViewNhanVien
{
    /// <summary>
    /// Interaction logic for QuanLyNhanVien.xaml
    /// </summary>
    public partial class QuanLyNhanVien : Window
    {
        NhanVienProvider _nhanVien = new NhanVienProvider();
        DanTocProvider _danToc = new DanTocProvider();
        PhongBanProvider _phongBan = new PhongBanProvider();
        ChucVuProvider _chucVu = new ChucVuProvider();
        TrinhDoHocVanProvider _trinhDoHocVan = new TrinhDoHocVanProvider();
        LuongProvider _luong = new LuongProvider(); 

        public QuanLyNhanVien()
        {
            InitializeComponent();

            getDanhSachNhanVien();

            bindingCbbGioiTinh();
            bindingCbbDanToc();
            bindingCbbPhongBan();
            bindingCbbChucVu();
            bindingCbbTrinhDoHocVan();
            bindingCbbDmLuong();
        }

        public void getDanhSachNhanVien()
        {
            List<NhanVienVM> nhanVienVms = _nhanVien.getAllNhanVienVM();

            dataGrid1.ItemsSource = nhanVienVms;
        }

        public void bindingCbbGioiTinh()
        {
            List<GioiTinhVM> gioiTinhVMs = new List<GioiTinhVM>();
            GioiTinhVM gioiTinhVM1 = new GioiTinhVM(1, "Nam");
            GioiTinhVM gioiTinhVM2 = new GioiTinhVM(2, "Nữ");
            gioiTinhVMs.Add(gioiTinhVM1);
            gioiTinhVMs.Add(gioiTinhVM2);

            cbbGioiTinh.ItemsSource = gioiTinhVMs;
        }

        public void bindingCbbDanToc()
        {
            List<DAN_TOC> danTocs = new List<DAN_TOC>();
            danTocs = _danToc.getAll();
            cbbDanToc.ItemsSource = danTocs;
        }

        public void bindingCbbPhongBan()
        {
            List<PHONG_BAN> phongBans = new List<PHONG_BAN>();
            phongBans = _phongBan.getAll();
            cbbPhongBan.ItemsSource = phongBans;
        }

        public void bindingCbbChucVu()
        {
            List<CHUC_VU> chucVus = new List<CHUC_VU>();
            chucVus = _chucVu.getAll();
            cbbChucVu.ItemsSource = chucVus;
        }

        public void bindingCbbTrinhDoHocVan()
        {
            List<TRINH_DO_HOC_VAN> trinhDoHocVan = new List<TRINH_DO_HOC_VAN>();
            trinhDoHocVan = _trinhDoHocVan.getAll();
            cbbTrinhDoHocVan.ItemsSource = trinhDoHocVan;
        }

        public void bindingCbbDmLuong()
        {
            List<LUONG> luongs = new List<LUONG>();
            luongs = _luong.getAll();
            cbbDmLuong.ItemsSource = luongs;
        }

        private void btnThemNhanVien_Click(object sender, RoutedEventArgs e)
        {
            NHAN_VIEN nhanVien = new NHAN_VIEN();

            string strTenNhanVien = txtTenNhanVien.Text;
            string strNgaySinh = dateNgaySinh.Text;
            string strQueQuan = txtQueQuan.Text;
            string strSoDienThoai = txtSoDienThoai.Text;

            int intGioiTinh = 0;
            int intDanToc = 0;
            int intPhongBan = 0;
            int intChucVu = 0;
            int intTrinhDoHocVan = 0;
            int intMucLuong = 0;

            if (string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(strNgaySinh))
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(strQueQuan))
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            GioiTinhVM gioiTinhSelected = (GioiTinhVM)cbbGioiTinh.SelectedItem;
            if (gioiTinhSelected != null)
            {
                intGioiTinh = Util.CnvObjToInt32(gioiTinhSelected.Id);
                if (intGioiTinh == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_8, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            DAN_TOC danTocSelected = (DAN_TOC)cbbDanToc.SelectedItem;
            if (danTocSelected != null)
            {
                intDanToc = Util.CnvObjToInt32(danTocSelected.Id);
                if (intDanToc == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_9, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(strSoDienThoai))
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PHONG_BAN phongBanSelected = (PHONG_BAN)cbbPhongBan.SelectedItem;
            if (phongBanSelected != null)
            {
                intPhongBan = Util.CnvObjToInt32(phongBanSelected.Id);
                if (intPhongBan == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_10, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            CHUC_VU chucVuSelected = (CHUC_VU)cbbChucVu.SelectedItem;
            if (chucVuSelected != null)
            {
                intChucVu = Util.CnvObjToInt32(chucVuSelected.Id);
                if (intChucVu == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_11, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            TRINH_DO_HOC_VAN trinhDoHocVanSelected = (TRINH_DO_HOC_VAN)cbbTrinhDoHocVan.SelectedItem;
            if (trinhDoHocVanSelected != null)
            {
                intTrinhDoHocVan = Util.CnvObjToInt32(trinhDoHocVanSelected.Id);
                if (intTrinhDoHocVan == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_12, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            LUONG dmLuong = (LUONG)cbbDmLuong.SelectedItem;
            if (dmLuong != null)
            {
                intMucLuong = Util.CnvObjToInt32(dmLuong.Id);
                if (intMucLuong == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_13, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            nhanVien.HoTen = strTenNhanVien;
            nhanVien.NgaySinh = Util.CnvObjToDatetime(strNgaySinh);
            nhanVien.QueQuan = strQueQuan;
            nhanVien.GioiTinh = intGioiTinh;
            nhanVien.IdDanToc = intDanToc;
            nhanVien.SoDienThoai = strSoDienThoai;
            nhanVien.IdPhongBan = intPhongBan;
            nhanVien.IdChucVu = intChucVu;
            nhanVien.IdTrinhDoHocVan = intTrinhDoHocVan;
            nhanVien.IdLuong = intMucLuong;

            bool insertNhanVien = _nhanVien.Create(nhanVien);
            if (insertNhanVien)
            {
                MessageBox.Show(Msg.NHANVIEN_THEMMOI_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_2, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _nhanVien.getAllNhanVienVM();
        }

        private void btnCapNhatNhanVien_Click(object sender, RoutedEventArgs e)
        {
            NHAN_VIEN nhanVien = new NHAN_VIEN();

            string strTenNhanVien = txtTenNhanVien.Text;
            string strNgaySinh = dateNgaySinh.Text;
            string strQueQuan = txtQueQuan.Text;
            string strSoDienThoai = txtSoDienThoai.Text;

            int intGioiTinh = 0;
            int intDanToc = 0;
            int intPhongBan = 0;
            int intChucVu = 0;
            int intTrinhDoHocVan = 0;
            int intMucLuong = 0;

            if (string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_1, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(strNgaySinh))
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_5, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(strQueQuan))
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_6, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            GioiTinhVM gioiTinhSelected = (GioiTinhVM)cbbGioiTinh.SelectedItem;
            if (gioiTinhSelected != null)
            {
                intGioiTinh = Util.CnvObjToInt32(gioiTinhSelected.Id);
                if (intGioiTinh == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_8, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            DAN_TOC danTocSelected = (DAN_TOC)cbbDanToc.SelectedItem;
            if (danTocSelected != null)
            {
                intDanToc = Util.CnvObjToInt32(danTocSelected.Id);
                if (intDanToc == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_9, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(strSoDienThoai))
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_7, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PHONG_BAN phongBanSelected = (PHONG_BAN)cbbPhongBan.SelectedItem;
            if (phongBanSelected != null)
            {
                intPhongBan = Util.CnvObjToInt32(phongBanSelected.Id);
                if (intPhongBan == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_10, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            CHUC_VU chucVuSelected = (CHUC_VU)cbbChucVu.SelectedItem;
            if (chucVuSelected != null)
            {
                intChucVu = Util.CnvObjToInt32(chucVuSelected.Id);
                if (intChucVu == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_11, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            TRINH_DO_HOC_VAN trinhDoHocVanSelected = (TRINH_DO_HOC_VAN)cbbTrinhDoHocVan.SelectedItem;
            if (trinhDoHocVanSelected != null)
            {
                intTrinhDoHocVan = Util.CnvObjToInt32(trinhDoHocVanSelected.Id);
                if (intTrinhDoHocVan == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_12, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            LUONG dmLuong = (LUONG)cbbDmLuong.SelectedItem;
            if (dmLuong != null)
            {
                intMucLuong = Util.CnvObjToInt32(dmLuong.Id);
                if (intMucLuong == 0)
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_13, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            nhanVien.HoTen = strTenNhanVien;
            nhanVien.NgaySinh = Util.CnvObjToDatetime(strNgaySinh);
            nhanVien.QueQuan = strQueQuan;
            nhanVien.GioiTinh = intGioiTinh;
            nhanVien.IdDanToc = intDanToc;
            nhanVien.SoDienThoai = strSoDienThoai;
            nhanVien.IdPhongBan = intPhongBan;
            nhanVien.IdChucVu = intChucVu;
            nhanVien.IdTrinhDoHocVan = intTrinhDoHocVan;
            nhanVien.IdLuong = intMucLuong;
            nhanVien.Id = Util.CnvObjToInt32(lblIdNhanVien.Content);

            bool updateNhanVien = _nhanVien.Edit(nhanVien);
            if (updateNhanVien)
            {
                MessageBox.Show(Msg.NHANVIEN_CAPNHAT_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Msg.NHANVIEN_ERROR_3, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = _nhanVien.getAllNhanVienVM();
        }

        private void btnXoaNhanVien_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                NhanVienVM item = (NhanVienVM)dataGrid1.SelectedItem;
                bool deleteNhanVien = _nhanVien.Delete(item.Id);
                if (deleteNhanVien)
                {
                    MessageBox.Show(Msg.NHANVIEN_XOA_THANHCONG, Msg.THONG_BAO_SUSSESS, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Msg.NHANVIEN_ERROR_4, Msg.THONG_BAO_ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dataGrid1.CanUserAddRows = false;
                dataGrid1.ItemsSource = _nhanVien.getAllNhanVienVM();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                NhanVienVM item = (NhanVienVM)dataGrid1.SelectedItem;
                txtTenNhanVien.Text = item.HoTen;
                dateNgaySinh.Text = Util.CnvObjToString(item.NgaySinh);
                txtQueQuan.Text = item.QueQuan;
                cbbGioiTinh.SelectedIndex = intGetIndexGioiTinhByTen(item.GioiTinh);
                cbbDanToc.SelectedItem = _danToc.Detail(Util.CnvObjToInt32(item.IdDanToc));
                txtSoDienThoai.Text = item.SoDienThoai;
                cbbPhongBan.SelectedItem = _phongBan.Detail(Util.CnvObjToInt32(item.IdPhongBan));
                cbbChucVu.SelectedItem = _chucVu.Detail(Util.CnvObjToInt32(item.IdChucVu));
                cbbTrinhDoHocVan.SelectedItem = _trinhDoHocVan.Detail(Util.CnvObjToInt32(item.IdTrinhDoHocVan));
                cbbDmLuong.SelectedItem = _luong.Detail(Util.CnvObjToInt32(item.IdDmLuong));

                lblIdNhanVien.Content = item.Id;
            }
        }

        public List<GioiTinhVM> bindingGioiTinhByName(string ten)
        {
            List<GioiTinhVM> gioiTinhVMs = new List<GioiTinhVM>();
            if (ten.ToUpper().Equals("NAM"))
            {
                gioiTinhVMs.Add(new GioiTinhVM(1, "NAM"));
                gioiTinhVMs.Add(new GioiTinhVM(2, "NỮ"));
            }
            else if (ten.ToUpper().Equals("NỮ"))
            {
                gioiTinhVMs.Add(new GioiTinhVM(1, "NAM"));
                gioiTinhVMs.Add(new GioiTinhVM(2, "NỮ"));
            }
            return gioiTinhVMs;
        }

        public int intGetIndexGioiTinhByTen(string ten)
        {
            int index = 0;
            if (ten.ToUpper().Equals("NAM"))
            {
                index = 0;
            }
            else if (ten.ToUpper().Equals("NỮ"))
            {
                index = 1;
            }
            return index;
        }

        public GioiTinhVM getGioiTinhByTen(string ten)
        {
            GioiTinhVM gioiTinhVM = null;
            if (ten.ToUpper().Equals("NAM"))
            {
                gioiTinhVM = new GioiTinhVM(1, "NAM");
            }
            else if (ten.ToUpper().Equals("NỮ"))
            {
                gioiTinhVM = new GioiTinhVM(2, "NỮ");
            }
            return gioiTinhVM;
        }

        public int getIdGioiTinhByTen(string ten)
        {
            int IdGioiTinh = 0;
            if (ten.ToUpper().Equals("NAM"))
            {
                IdGioiTinh = 1;
            }
            else if (ten.ToUpper().Equals("NỮ"))
            {
                IdGioiTinh = 2;
            }
            return IdGioiTinh;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string strTenNhanVien = txtTenNhanVienTimKiem.Text;
            string strSoDienThoai = txtSoDienThoaiTimKiem.Text;

            List<NhanVienVM> nhanVienVMs = _nhanVien.getAllNhanVienVM();

            if (!string.IsNullOrWhiteSpace(strTenNhanVien))
            {
                nhanVienVMs = nhanVienVMs.Where(s => s.HoTen.Contains(strTenNhanVien)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(strSoDienThoai))
            {
                nhanVienVMs = nhanVienVMs.Where(s => s.SoDienThoai.Contains(strSoDienThoai)).ToList();
            }

            dataGrid1.CanUserAddRows = false;
            dataGrid1.ItemsSource = nhanVienVMs;
        }
    }
}
