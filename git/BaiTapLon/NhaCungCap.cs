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
    public partial class NhaCungCap : Form
    {
        DataTable tblLoaiSP;
        public NhaCungCap()
        {
            InitializeComponent();
        }

        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            Load_DataGridView();
        }
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaNCC, TenNCC,DiaChi,SDT FROM NCC";
            tblLoaiSP = Class.Functions.GetDataToTable(sql);
            dataGridView1.DataSource = tblLoaiSP;
            dataGridView1.Columns[0].HeaderText = "Mã NCC";
            dataGridView1.Columns[1].HeaderText = "Tên loại SP";
            dataGridView1.Columns[2].HeaderText = "địa chỉ";
            dataGridView1.Columns[3].HeaderText = "SĐT";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[3].Width = 300;

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
     MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
  MessageBoxIcon.Information);
                return;
            }
            txtMaKH.Text = dataGridView1.CurrentRow.Cells["MaNCC"].Value.ToString();
            txtTenKH.Text = dataGridView1.CurrentRow.Cells["TenNCC"].Value.ToString();
            txtDiaChi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtCMND.Text = dataGridView1.CurrentRow.Cells["SDT"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtCMND.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKH.Enabled = true;
            txtMaKH.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (txtCMND.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập SĐT", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCMND.Focus();
                return;
            }
            sql = "SELECT MaNCC FROM NCC WHERE MaNCC=N'" + txtMaKH.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                txtMaKH.Text = "";
                return;
            }
            sql = "INSERT INTO NCC(MaNCC,TenNCC,DiaChi,SDT) VALUES (N'" + txtMaKH.Text.Trim() + "',N'" + txtTenKH.Text.Trim() + "',N'" + txtDiaChi.Text.Trim() + "','" + txtCMND.Text + "')";
            Functions.RunSql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaKH.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên NCC", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (txtCMND.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập SĐT", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCMND.Focus();
                return;
            }
            sql = "UPDATE NCC SET  TenNCC=N'" + txtTenKH.Text.Trim().ToString() + "',DiaChi=N'" + txtDiaChi.Text.Trim().ToString() + "',SDT='" + txtCMND.Text.ToString() + "' WHERE MaNCC=N'" + txtMaKH.Text + "'";
            Functions.RunSql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLoaiSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE NCC WHERE MaNCC=N'" + txtMaKH.Text + "'";
                Functions.RunSqlDel(sql);
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
            txtMaKH.Enabled = false;
        }
        private void txtMakhach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
