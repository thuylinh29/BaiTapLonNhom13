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

    public partial class frmLoaiSP : Form
    {
        DataTable tblLoaiSP;
        public frmLoaiSP()
        {
            InitializeComponent();
        }

        private void frmLoaiSP_Load(object sender, EventArgs e)
        {
            txtMaLoaiSP.Enabled = false;
            Load_DataGridView();
        }
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaLoaiSP, TenLoaiSP FROM LoaiSP";
            tblLoaiSP = Class.Functions.GetDataToTable(sql);
            dataGridView1.DataSource = tblLoaiSP;
            dataGridView1.Columns[0].HeaderText = "Mã loại SP";
            dataGridView1.Columns[1].HeaderText = "Tên loại SP";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 300;

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
     MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiSP.Focus();
                return;
            }
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
  MessageBoxIcon.Information);
                return;
            }
            txtMaLoaiSP.Text = dataGridView1.CurrentRow.Cells["MaLoaiSP"].Value.ToString();
            txtTenLoaiSP.Text = dataGridView1.CurrentRow.Cells["TenLoaiSP"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMaLoaiSP.Text = "";
            txtTenLoaiSP.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaLoaiSP.Enabled = true;
            txtMaLoaiSP.Focus();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaLoaiSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã loại sản phẩm", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoaiSP.Focus();
                return;
            }
            if (txtTenLoaiSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên loại sản phẩm", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoaiSP.Focus();
                return;
            }
            sql = "SELECT MaLoaiSP FROM LoaiSP WHERE MaLoaiSP=N'" +
txtMaLoaiSP.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoaiSP.Focus();
                txtMaLoaiSP.Text = "";
                return;
            }
            sql = "INSERT INTO LoaiSP(MaLoaiSP,TenLoaiSP) VALUES(N'" +
txtMaLoaiSP.Text + "',N'" + txtTenLoaiSP.Text + "')";
            Class.Functions.RunSql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaLoaiSP.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenLoaiSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoaiSP.Focus();
                return;
            }
            sql = "UPDATE LoaiSP SET TenLoaiSP=N'" + txtTenLoaiSP.Text.ToString() +
"' WHERE MaLoaiSP=N'" + txtMaLoaiSP.Text + "'";
            Class.Functions.RunSql(sql);
            Load_DataGridView();
            ResetValues();


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE LoaiSP WHERE MaLoaiSP=N'" + txtMaLoaiSP.Text + "'";
                Class.Functions.RunSqlDel(sql);
                Load_DataGridView();
                ResetValues();
            }

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaLoaiSP.Enabled = false;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
