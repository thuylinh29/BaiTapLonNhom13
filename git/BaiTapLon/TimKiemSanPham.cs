using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaiTapLon.Class;
using System.Data.SqlClient;

namespace BaiTapLon
{
    public partial class TimKiemSanPham : Form
    {
        DataTable tblTKSP;
        public TimKiemSanPham()
        {
            InitializeComponent();
        }

        private void TimKiemSanPham_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtTenSP.Text == "") && (txtTenLoaiSP.Text == "") && (txtTenNoiSX.Text == ""))
            {
                MessageBox.Show("Hãy nhập thông tin tìm kiếm!", "Yeu cau...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "select a.MaSP, a.TenSP, a.MaMau, a.MaNoiSX, a.SL, a.GiaNhap, a.GiaThue, a.MaLoaiSP  from SP as a, LoaiSP as b, NoiSX as c " +
                "where a.MaLoaiSP=b.MaLoaiSP AND a.MaNoiSX=c.MaNoiSX ";

            if (txtTenSP.Text != "")
            {
                sql = sql + "AND TenSP Like N'%" + txtTenSP.Text + "%'";
            }

            if (txtTenLoaiSP.Text != "")
            {
                sql = sql + "AND TenLoaiSP Like N'%" + txtTenLoaiSP.Text + "%'";
            }

            if (txtTenNoiSX.Text != "")
            {
                sql = sql + "AND DiaChi Like N'%" + txtTenNoiSX.Text + "%'";
            }

            tblTKSP = Functions.GetDataToTable(sql);

            if (tblTKSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Có" + tblTKSP.Rows.Count + "bản ghi thỏa mãn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            dataGridView1.DataSource = tblTKSP;
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            dataGridView1.Columns[0].HeaderText = "Mã sản phẩm";
            dataGridView1.Columns[1].HeaderText = "Tên sản phẩm";
            dataGridView1.Columns[2].HeaderText = "Mã màu";
            dataGridView1.Columns[3].HeaderText = "Mã nơi sản xuất";
            dataGridView1.Columns[4].HeaderText = "Số lượng";
            dataGridView1.Columns[5].HeaderText = "Đơn giá nhập";
            dataGridView1.Columns[6].HeaderText = "Đơn giá thuê";
            dataGridView1.Columns[7].HeaderText = "Mã loại sản phẩm";
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 80;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
