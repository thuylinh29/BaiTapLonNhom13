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
    public partial class BaoCaoHoaDonNhap : Form
    {
        DataTable tblBC;
        public BaoCaoHoaDonNhap()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            ResetValues();
            dataGridView1.DataSource = null;
        }
        private void ResetValues()
        {
            txtThang.Text = "";
            txtNam.Text = "";
            txtTong.Text = "0";
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            string sql;
            Double tong;
            sql = "select a.MaHDN.a.NgayNhap,b.MaSP,b.SL,b.DonGia,b.ThanhTien from HoaDonNhap as a , ChiTietHDN as b WHERE 1=1 AND a.MaHD=b.MaHD";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(NgayNhap) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR(NgayNhap) =" + txtNam.Text;


            tblBC = Functions.GetDataToTable(sql);
            if (tblBC.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetValues();
            }
            else
            {
                MessageBox.Show("Có " + tblBC.Rows.Count + " bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tong = Convert.ToDouble(Functions.GetFieldValues("SELECT sum(TongTien) FROM HoaDonNhap WHERE MONTH(NgayNhap)=N'" + txtThang.Text + "' AND YEAR(NgayNhap)=N'" + txtNam.Text + "'"));
                Functions.RunSql(sql);
                txtTong.Text = tong.ToString();
            }
            dataGridView1.DataSource = tblBC;
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridView1 = null;
        }
    }
}
