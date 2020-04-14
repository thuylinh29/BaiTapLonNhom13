using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BaiTapLon.Class;

namespace BaiTapLon
{
    public partial class HopDong : Form
    {
        DataTable tblHDN;
        public HopDong()
        {
            InitializeComponent();
        }

        private void HopDong_Load(object sender, EventArgs e)
        {
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "Select * From HD";
            Functions.Connect();
            tblHDN = Functions.GetDataToTable(sql);
            dataGridView1.DataSource = tblHDN;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = dataGridView1.CurrentRow.Cells["MaHD"].Value.ToString();
            txtNgayThue.Text = dataGridView1.CurrentRow.Cells["NgayThue"].Value.ToString();
            txtMaKH.Text = dataGridView1.CurrentRow.Cells["MaKH"].Value.ToString();
            txtKhuyenMai.Text = dataGridView1.CurrentRow.Cells["KhuyenMai"].Value.ToString();
            txtTamUng.Text = dataGridView1.CurrentRow.Cells["TamUng"].Value.ToString();
            btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from HD where MaHD='" + txtMaHD.Text + "'";
            Functions.Runsql(sql);
            loadDataToGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql1 = "Insert into HD(MaHD,NgayThue,TamUng,KhuyenMai,MaKH) values ('" + txtMaHD.Text.Trim() + "','" + txtNgayThue.Text.Trim() + "','" + txtTamUng.Text.Trim() + "','" + txtKhuyenMai.Text.Trim() + "','" + txtMaKH.Text.Trim() + "')";
            Functions.RunSql(sql1);
            loadDataToGridView();

        }

        private void btnChiTietHopDong_Click(object sender, EventArgs e)
        {
            ChiTietHD f = new ChiTietHD();
            f.ShowDialog();
        }
    }
}
