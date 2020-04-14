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
    public partial class BaoCaoDoanhThu : Form
    {
        DataTable tblDT;
        public BaoCaoDoanhThu()
        {
            InitializeComponent();
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
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridView1 = null;
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            string sql;
            Double tong;
            sql = "select * from ThanhToan WHERE 1=1 ";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(NgayThanhToan) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR(NgayThanhToan) =" + txtNam.Text;


            tblDT = Functions.GetDataToTable(sql);
            if (tblDT.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetValues();
            }
            else
            {
                MessageBox.Show("Có " + tblDT.Rows.Count + " bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tong = Convert.ToDouble(Functions.GetFieldValues("SELECT sum(TongTien) FROM ThanhToan WHERE MONTH(NgayThanhToan)=N'" + txtThang.Text + "' AND YEAR(NgayThanhToan)=N'" + txtNam.Text + "'"));
                Functions.RunSql(sql);
                txtTong.Text = tong.ToString();
            }
            dataGridView1.DataSource = tblDT;
        }
    }
}
