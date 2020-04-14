using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class frmMauSP : Form
    {
        DataTable tblLoaiSP;
        public frmMauSP()
        {
            InitializeComponent();
        }

        private void frmMauSP_Load(object sender, EventArgs e)
        {
            txtMaMau.Enabled = false;
            Load_DataGridView();
        }
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaMau, TenMau FROM Mau";
            tblLoaiSP = Class.Functions.GetDataToTable(sql);
            dataGridView1.DataSource = tblLoaiSP;
            dataGridView1.Columns[0].HeaderText = "Mã mẫu";
            dataGridView1.Columns[1].HeaderText = "Tên mẫu";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 300;

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
     MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaMau.Focus();
                return;
            }
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
  MessageBoxIcon.Information);
                return;
            }
            txtMaMau.Text = dataGridView1.CurrentRow.Cells["MaMau"].Value.ToString();
            txtTenMau.Text = dataGridView1.CurrentRow.Cells["TenMau"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMaMau.Text = "";
            txtTenMau.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaMau.Enabled = true;
            txtTenMau.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaMau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã mẫu sản phẩm", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMau.Focus();
                return;
            }
            if (txtMaMau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên loại sản phẩm", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMau.Focus();
                return;
            }
            sql = "SELECT MaMau FROM Mau WHERE MaMau=N'" + txtMaMau.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã mẫu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMau.Focus();
                txtMaMau.Text = "";
                return;
            }
            sql = "INSERT INTO Mau(MaMau,TenMau) VALUES(N'" +
txtMaMau.Text + "',N'" + txtTenMau.Text + "')";
            Class.Functions.RunSql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaMau.Enabled = false;
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
            if (txtMaMau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenMau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên mẫu", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMau.Focus();
                return;
            }
            sql = "UPDATE Mau SET TenMau=N'" + txtTenMau.Text.ToString() +
"' WHERE MaMau=N'" + txtMaMau.Text + "'";
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
            if (txtMaMau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE Mau WHERE MaMau=N'" + txtMaMau.Text + "'";
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
            txtMaMau.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
