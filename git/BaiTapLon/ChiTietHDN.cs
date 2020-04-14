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
    public partial class ChiTietHDN : Form
    {
        DataTable tblHDN;
        public ChiTietHDN()
        {
            InitializeComponent();
        }

        private void ChiTietHDN_Load(object sender, EventArgs e)
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

        private void txtMaSP_TextChanged(object sender, EventArgs e)
        {
            string str = "SELECT GiaNhap FROM SP WHERE MaSP =N'" + txtMaSP.Text+ "'";
            txtGiaNhap.Text = Functions.GetFieldValues(str);
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            int x;
            double dongia, tt;
            dongia = Convert.ToDouble(txtGiaNhap.Text);
            x = Convert.ToInt32(txtSL.Text);
            tt = x * dongia;
            txtThanhTien.Text = tt.ToString();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtHDN.Text = dataGridView1.CurrentRow.Cells["MaHDN"].Value.ToString();
            txtMaSP.Text = dataGridView1.CurrentRow.Cells["MaSP"].Value.ToString();
            txtSL.Text = dataGridView1.CurrentRow.Cells["SL"].Value.ToString();
            txtGiaNhap.Text = dataGridView1.CurrentRow.Cells["GiaNhap"].Value.ToString();
            txtThanhTien.Text = dataGridView1.CurrentRow.Cells["ThanhTien"].Value.ToString();
            btnXoa.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            double s, sl;
            double x;
            if (txtSL.Text == "") btnLuu.Enabled = false;
            s= Convert.ToDouble(txtSL.Text);
            string sql1 = "Insert into ChiTietHDN(MaHDN,MaSP,DonGia,SL,ThanhTien) values ('" + txtHDN.Text.Trim() + "','" + txtMaSP.Text.Trim() + "','" + txtGiaNhap.Text.Trim() + "','" + txtSL.Text.Trim() + "','" + txtThanhTien.Text.Trim() + "')";
            Functions.RunSql(sql1);
            loadDataToGridView();
            string sql = "SELECT SL FROM SP WHERE Mahang = N'" + txtMaSP.Text + "'";
            sl = Convert.ToDouble(Functions.GetFieldValues(sql));
            x = sl + s;
            sql = "UPDATE tblHang SET Soluong =" + x + " WHERE Mahang= N'" + txtMaSP.Text + "'";
            Functions.RunSql(sql);

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from ChiTietHDN where MaHDN='" + txtHDN.Text + "'";
            Functions.Runsql(sql);
            loadDataToGridView();
        }
    }
}
