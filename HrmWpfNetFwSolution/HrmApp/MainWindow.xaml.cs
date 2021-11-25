using DataLayer.Data;
using DataLayer.Models;
using HrmApp.ViewCongSo;
using HrmApp.ViewDanhMuc;
using HrmApp.ViewDuAn;
using HrmApp.ViewNhanVien;
using HrmApp.ViewTaiKhoan;
using HrmApp.ViewThongKeBaoCao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities.Utils;
using Utilities.Variable;

namespace HrmApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            cbbItemProfile.ItemsSource = getListItemProfile();
            cbbItemProfile.SelectedIndex = 0;

        }

        public class ItemProfile
        {
            public ItemProfile(int v, string n)
            {
                this.value = v;
                this.name = n;
            }

            public int value { get; set; }
            public string name { get; set; }
        }

        public List<ItemProfile> getListItemProfile()
        {
            List<ItemProfile> itemProfiles = new List<ItemProfile>();
            string strTenHienThi = GlobalVale.taiKhoan.TenHienThi;
            ItemProfile item1 = new ItemProfile(1, strTenHienThi);
            ItemProfile item2 = new ItemProfile(2, "Thông tin tài khoản");
            ItemProfile item3 = new ItemProfile(3, "Đổi mật khẩu tài khoản");
            ItemProfile item4 = new ItemProfile(4, "Đăng xuất");

            itemProfiles.Add(item1);
            itemProfiles.Add(item2);
            itemProfiles.Add(item3);
            itemProfiles.Add(item4);
            return itemProfiles;
        }


        private void DanhSachTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            DanhSachTaiKhoan danhSachTaiKhoan = new DanhSachTaiKhoan();
            danhSachTaiKhoan.Owner = this;
            danhSachTaiKhoan.ShowDialog();
        }

        private void ThemMoiTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            QuanLyTaiKhoan qlTaiKhoan = new QuanLyTaiKhoan();
            qlTaiKhoan.Owner = this;
            qlTaiKhoan.ShowDialog();
        }

        private void CapNhatTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            DoiMatKhauTaiKhoan doiMatKhauTK = new DoiMatKhauTaiKhoan();
            doiMatKhauTK.Owner = this;
            doiMatKhauTK.ShowDialog();
        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            this.Title = "Edit: " + item.Header;
        }

        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            this.Title = "View: " + item.Header;
        }

        private void cbbItemProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int valueProfile = 0;
            ItemProfile itemProfileSelect = (ItemProfile)cbbItemProfile.SelectedItem;
            if (itemProfileSelect != null)
            {
                valueProfile = Util.CnvObjToInt32(itemProfileSelect.value);

                if (valueProfile == 1)
                {
                    return;
                }
                else if (valueProfile == 2)
                {
                    ThongTinTaiKhoan thongTinTK = new ThongTinTaiKhoan();
                    thongTinTK.Owner = this;
                    thongTinTK.ShowDialog();
                }
                else if (valueProfile == 3)
                {
                    DoiMatKhauTaiKhoan doiMatKhauTK = new DoiMatKhauTaiKhoan();
                    doiMatKhauTK.Owner = this;
                    doiMatKhauTK.ShowDialog();
                }
                else
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
        }


        #region Quản lý danh mục
        private void QuanLyPhongBan_Click(object sender, RoutedEventArgs e)
        {
            QuanLyPhongBan quanLyPhongBan = new QuanLyPhongBan();

            quanLyPhongBan.Owner = this;
            quanLyPhongBan.ShowDialog();
        }

        private void QuanLyChucVu_Click(object sender, RoutedEventArgs e)
        {
            QuanLyChucVu quanLyChucVu = new QuanLyChucVu();

            quanLyChucVu.Owner = this;
            quanLyChucVu.ShowDialog();
        }

        private void TrinhDoHocVan_Click(object sender, RoutedEventArgs e)
        {
            QuanLyTrinhDoHocVan quanLyTrinhDoHocVan = new QuanLyTrinhDoHocVan();

            quanLyTrinhDoHocVan.Owner = this;
            quanLyTrinhDoHocVan.ShowDialog();
        }

        private void QuanLyLoaiHopDong_Click(object sender, RoutedEventArgs e)
        {
            QuanLyLoaiHopDong quanLyLoaiHopDong = new QuanLyLoaiHopDong();

            quanLyLoaiHopDong.Owner = this;
            quanLyLoaiHopDong.ShowDialog();
        }

        private void QuanLyDmLuong_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDanhMucLuong quanLyDmLuong = new QuanLyDanhMucLuong();

            quanLyDmLuong.Owner = this;
            quanLyDmLuong.ShowDialog();
        }

        private void QuanLyDanToc_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDanToc quanLyDanToc = new QuanLyDanToc();

            quanLyDanToc.Owner = this;
            quanLyDanToc.ShowDialog();
        }
        #endregion

        #region Nhân viên
        private void DanhSachNhanVien_Click(object sender, RoutedEventArgs e)
        {
            DanhSachNhanVien danhSachNhanVien = new DanhSachNhanVien();

            danhSachNhanVien.Owner = this;
            danhSachNhanVien.ShowDialog();
        }

        private void QuanLyNhanVien_Click(object sender, RoutedEventArgs e)
        {
            QuanLyNhanVien quanLyNhanVien = new QuanLyNhanVien();

            quanLyNhanVien.Owner = this;
            quanLyNhanVien.ShowDialog();
        }

        private void QuanLyHopDongNhanVien_Click(object sender, RoutedEventArgs e)
        {
            QuanLyHopDongNhanVien quanLyHopDongNhanVien = new QuanLyHopDongNhanVien();

            quanLyHopDongNhanVien.Owner = this;
            quanLyHopDongNhanVien.ShowDialog();
        }

        private void QuanLyKhenThuongNhanVien_Click(object sender, RoutedEventArgs e)
        {
            QuanLyKhenThuongNhanVien quanLyKhenThuongNhanVien = new QuanLyKhenThuongNhanVien();

            quanLyKhenThuongNhanVien.Owner = this;
            quanLyKhenThuongNhanVien.ShowDialog();
        }

        private void QuanLyKyLuatNhanVien_Click(object sender, RoutedEventArgs e)
        {
            QuanLyKyLuatNhanVien quanLyKyLuatNhanVien = new QuanLyKyLuatNhanVien();

            quanLyKyLuatNhanVien.Owner = this;
            quanLyKyLuatNhanVien.ShowDialog();
        }
        #endregion

        #region Lương Nhân viên
        private void ChamCongNhanVien_CLick(object sender, RoutedEventArgs e)
        {
            QuanLyChamCong quanLyChamCong = new QuanLyChamCong();

            quanLyChamCong.Owner = this;
            quanLyChamCong.ShowDialog();
        }

        private void TinhLuongNhanVien_Click(object sender, RoutedEventArgs e)
        {
            TinhLuongNhanVien tinhLuongNhanVien = new TinhLuongNhanVien();

            tinhLuongNhanVien.Owner = this;
            tinhLuongNhanVien.ShowDialog();
        }
        #endregion

        #region Báo cáo - Thống kê
        private void ThongKeNhanVien_Click(object sender, RoutedEventArgs e)
        {
            ThongKeNhanVien thongKeBaoCao = new ThongKeNhanVien();

            thongKeBaoCao.Owner = this;
            thongKeBaoCao.ShowDialog();
        }

        private void ReportDsNhanVien_Click(object sender, RoutedEventArgs e)
        {
            ReportDanhSachNhanVien reportDsNhanVien = new ReportDanhSachNhanVien();

            reportDsNhanVien.Owner = this;
            reportDsNhanVien.ShowDialog();
        }

        private void BaoCaoTienLuong_Click(object sender, RoutedEventArgs e)
        {
            BaoCaoLuongNhanVien bcLuongNV = new BaoCaoLuongNhanVien();

            bcLuongNV.Owner = this;
            bcLuongNV.ShowDialog();
        }

        private void BaoCaoKhenThuong_Click(object sender, RoutedEventArgs e)
        {
            ReportKhenThuongNhanVien rpKhenThuongNV = new ReportKhenThuongNhanVien();

            rpKhenThuongNV.Owner = this;
            rpKhenThuongNV.ShowDialog();
        }

        private void BaoCaoKyLuat_Click(object sender, RoutedEventArgs e)
        {
            ReportKyLuatNhanVien rpKyLuatNhanVien = new ReportKyLuatNhanVien();

            rpKyLuatNhanVien.Owner = this;
            rpKyLuatNhanVien.ShowDialog();
        }

        private void ThongKeKhenThuongNV_Click(object sender, RoutedEventArgs e)
        {
            ThongKeKhenThuong tkKhenThuongNV = new ThongKeKhenThuong();

            tkKhenThuongNV.Owner = this;
            tkKhenThuongNV.ShowDialog();
        }

        private void ThongKeKyLuatNV_Click(object sender, RoutedEventArgs e)
        {
            ThongKeKyLuat tkKyLuatNV = new ThongKeKyLuat();

            tkKyLuatNV.Owner = this;
            tkKyLuatNV.ShowDialog();
        }

        private void ThongKeSoNgayNghiNV_Click(object sender, RoutedEventArgs e)
        {
            ThongKeSoLanNghiCuaNhanVien tkSoNgayNghi = new ThongKeSoLanNghiCuaNhanVien();

            tkSoNgayNghi.Owner = this;
            tkSoNgayNghi.ShowDialog();
        }
        #endregion

        #region Quản lý dự án
        private void QuanLyDuAn_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDuAn quanLyDuAn = new QuanLyDuAn();

            quanLyDuAn.Owner = this;
            quanLyDuAn.ShowDialog();
        }
        #endregion
    }
}
