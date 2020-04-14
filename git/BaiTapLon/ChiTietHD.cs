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
    public partial class ChiTietHD : Form
    {
        DataTable tblHDN;
        public ChiTietHD()
        {
            InitializeComponent();
        }

        private void ChiTietHD_Load(object sender, EventArgs e)
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            double s, sl;
            double x;
            if (txtSL.Text == "") btnLuu.Enabled = false;
            s = Convert.ToDouble(txtSL.Text);
            string sql1 = "Insert into HD(MaHD,MaSP,ChuaTra,SL,NgayThue) values ('" + txtHD.Text.Trim() + "','" + txtMaSP.Text.Trim() + "','" + txtChuaTra.Text.Trim() + "','" + txtSL.Text.Trim() + "','" + txtNgayThue.Text.Trim() + "')";
            Functions.RunSql(sql1);
            loadDataToGridView();
            string sql = "SELECT SL FROM SP WHERE Mahang = N'" + txtMaSP.Text + "'";
            sl = Convert.ToDouble(Functions.GetFieldValues(sql));
            x = sl - s;
            sql = "UPDATE tblHang SET Soluong =" + x + " WHERE Mahang= N'" + txtMaSP.Text + "'";
            Functions.RunSql(sql);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from ChiTietHD where MaHD='" + txtHD.Text + "'";
            Functions.Runsql(sql);
            loadDataToGridView();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
