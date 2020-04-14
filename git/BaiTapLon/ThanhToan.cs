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
    public partial class ThanhToan : Form
    {
        DataTable tblThanhToan;
        DateTime ngaytra;
        DateTime ngaythue;
        double tamung, sl;
        double giathue;
        public ThanhToan()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void loadDataToGridView()
        {
            string sql = "Select * from ThanhToan";
            Functions.Connect();
            tblThanhToan = Functions.GetDataToTable(sql);
            dataGridView1.DataSource = tblThanhToan;
        }

            private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from ChiTietThanhToan where MaThanhToan='" + txtSoTienThanhToan.Text + "'";
            Functions.Runsql(sql);
            string sqll = "delete from ThanhToan where MaThanhToan='" + txtSoTienThanhToan.Text + "'";
            Functions.Runsql(sqll);
            loadDataToGridView();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "insert into ThanhToan(MaThahToan,MaHD,MaNV,NgayThanhToan,TongTien,SoTienThanhToan) values"+ "('"+txtMaThanhToan.Text.Trim() +"','"+txtMaHD.Text.Trim()+ "','"+txtMaNV.Text.Trim()+ "','"+txtNgayTra.Text.Trim()+ "','"+txtThanhTien.Text.Trim()+ "','"+txtSoTienThanhToan.Text.Trim()+ "')";
            Functions.Runsql(sql);
            string sql1 = "Insert into ChiTietThanhToan(MaThanhToan,NgayTra,ThanhTien,MaSP) values" + "('"+txtMaThanhToan.Text.Trim()+ "','"+txtNgayTra.Text.Trim()+ "','"+txtThanhTien.Text.Trim()+ "','"+txtMaSP.Text.Trim()+ "')";
            Functions.Runsql(sql);
            loadDataToGridView();
        }

        private void txtMaHD_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT NgayThue FROM ChiTietHD WHERE MaHD = N'" + txtMaHD.Text + "'";
            ngaythue = Convert.ToDateTime(Functions.GetFieldValues(sql));

            string sql1 = "SELECT MaNV FROM HD WHERE MaHD = N'" + txtMaHD.Text + "'";
            txtMaNV.Text = Convert.ToString(Functions.GetFieldValues(sql1));

            string sql2 = "SELECT TamUng FROM HD WHERE MaHD = N'" + txtMaHD.Text + "'";
            tamung = Convert.ToDouble(Functions.GetFieldValues(sql2));

            string sql3 = "SELECT SL FROM ChiTietHD WHERE MaHD = N'" + txtMaHD.Text + "'";
            sl = Convert.ToDouble(Functions.GetFieldValues(sql3));

            string sql5 = "SELECT MaSP FROM ChiTietHD WHERE MaHD = N'" + txtMaHD.Text + "'";
            string msp = Convert.ToString(Functions.GetFieldValues(sql5));

            string sql4 = "SELECT GiaThue FROM SP WHERE MaHD = N'" +msp + "'";
            giathue = Convert.ToDouble(Functions.GetFieldValues(sql4));


        }

        private void txtNgayTra_TextChanged(object sender, EventArgs e)
        {
            ngaytra = Convert.ToDateTime(txtNgayTra.Text);
            TimeSpan time = ngaytra - ngaythue;
            int Tongsongay = time.Days;
            double thanhtien = Tongsongay * giathue;
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            TimeSpan time = ngaytra - ngaythue;
            int Tongsongay = time.Days;
            double tienthanhtoan = Tongsongay * giathue - tamung;
            txtSoTienThanhToan.Text = tienthanhtoan.ToString();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
