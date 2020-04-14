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
    public partial class HoaDonNhap : Form
    {
        DataTable tblHDN;
        public HoaDonNhap()
        {
            InitializeComponent();
        }

        private void HoaDonNhap_Load(object sender, EventArgs e)
        {
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "Select * From HoaDonNhap";
            Functions.Connect();
            tblHDN = Functions.GetDataToTable(sql);
            dataGridView1.DataSource = tblHDN;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHDN.Text = dataGridView1.CurrentRow.Cells["MaHDN"].Value.ToString();
            mskNgayNhap.Text = dataGridView1.CurrentRow.Cells["NgayNhap"].Value.ToString();
            txtTongTien.Text = dataGridView1.CurrentRow.Cells["TongTien"].Value.ToString();
            txtMaNV.Text = dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString();
            txtHDN.Text = dataGridView1.CurrentRow.Cells["MaNCC"].Value.ToString();
            btnXoa.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
        
            string sql1 = "Insert into HoaDonNhap(MaHDN,NgayNhap,TongTien,MaNV,NCC) values ('" + txtHDN.Text.Trim()+"','"+mskNgayNhap.Text.Trim()+ "','"+txtTongTien.Text.Trim()+ "','"+txtMaNV.Text.Trim()+ "','"+txtMaNCC.Text.Trim()+"')";
            Functions.RunSql(sql1);
            loadDataToGridView();
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            txtHDN.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from HoaDonNhap where MaHDN='" + txtHDN.Text + "'";
            Functions.Runsql(sql);
            loadDataToGridView();
        }

        private void btnChiTietHoaDonNhap_Click(object sender, EventArgs e)
        {
            ChiTietHDN f = new ChiTietHDN();
            f.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
