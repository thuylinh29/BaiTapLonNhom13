using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BaiTapLon
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Class.Functions.Disconnect();
            Application.Exit();
        }

        private void mnuLoaiSP_Click(object sender, EventArgs e)
        {
            frmLoaiSP f = new frmLoaiSP();
            f.ShowDialog();
        }

        private void mnuMauSP_Click(object sender, EventArgs e)
        {
            frmMauSP f = new frmMauSP();
            f.ShowDialog();
        }

        private void mãChuyênMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChuyenMon f = new ChuyenMon();
            f.ShowDialog();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhanVien f = new NhanVien();
            f.ShowDialog();
        }

        private void mnuKH_Click(object sender, EventArgs e)
        {
            KhachHang f = new KhachHang();
            f.ShowDialog();
        }

        private void mnuNCC_Click(object sender, EventArgs e)
        {
            NhaCungCap f = new NhaCungCap();
            f.ShowDialog();
        }

        private void mnuNoiSX_Click(object sender, EventArgs e)
        {
            NoiSX f = new NoiSX();
            f.ShowDialog();
        }

        private void mnuSP_Click(object sender, EventArgs e)
        {
            SP f = new SP();
            f.ShowDialog();
        }

        private void mnuThanhToan_Click(object sender, EventArgs e)
        {
            ThanhToan f = new ThanhToan();
            f.ShowDialog();
        }

        private void mnuHD_Click(object sender, EventArgs e)
        {
            HopDong f = new HopDong();
            f.ShowDialog();
        }

        private void mnuHDN_Click(object sender, EventArgs e)
        {
            HoaDonNhap f = new HoaDonNhap();
            f.ShowDialog();
        }

        private void mnuDoanhThu_Click(object sender, EventArgs e)
        {
            BaoCaoDoanhThu f = new BaoCaoDoanhThu();
            f.ShowDialog();
        }

        private void mnuTongNhap_Click(object sender, EventArgs e)
        {
            BaoCaoHoaDonNhap f = new BaoCaoHoaDonNhap();
            f.ShowDialog();
        }
    }
}
