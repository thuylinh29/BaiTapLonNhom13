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
    public partial class SP : Form
    {
        DataTable tblSP;
        public SP()
        {
            InitializeComponent();
        }

        private void SP_Load(object sender, EventArgs e)
        {
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "select MaSP, TenSP, SL, MaMau, MaNoiSX, MaLoaiSP, GiaNhap, GiaThue from SP";
            Functions.Connect();
            tblSP = Functions.GetDataToTable(sql);
            dataGridView1.DataSource = tblSP;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (tblSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            txtMaSP.Text = dataGridView1.CurrentRow.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = dataGridView1.CurrentRow.Cells["Tenhang"].Value.ToString();
            txtSoLuong.Text = dataGridView1.CurrentRow.Cells["SL"].Value.ToString();
            txtGiaNhap.Text = dataGridView1.CurrentRow.Cells["Dongianhap"].Value.ToString();
            txtGiaThue.Text = dataGridView1.CurrentRow.Cells["Dongiaban"].Value.ToString();
            txtAnh.Text = Functions.GetFieldValues("SELECT Anh FROM SP WHERE MaSP = N'" + txtAnh.Text + "'");
            picAnh.Image = Image.FromFile(txtAnh.Text);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|gif(*.gif)|*.gif|all files(*.*)|*.*";
            dlgOpen.InitialDirectory = "D:\\";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn hình ảnh";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "SELECT MaSP FROM SP WHERE MaSP=N'" + txtMaSP.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSP.Focus();
                txtMaSP.Text = "";
                return;
            }

            string sql1 = "insert into SP(MaSP,TenSP,Anh,MaMau,MaNoiSX,SL,GiaNhap,GiaThue,MaLoaiSP) values ('" + txtMaSP.Text.Trim() + "','" + txtTenSP.Text.Trim() + "','" + txtAnh.Text +"','" + txtMaMau.Text.Trim() + "','" + txtMaNoiSX.Text.Trim() + "','" + txtSoLuong.Text.Trim() + "','" + txtGiaNhap.Text + "','" +txtGiaThue.Text + "','" + txtMaLoaiSP.Text.Trim() + "')";

            Functions.RunSql(sql1);
            loadDataToGridView();
            resetvalue();
            btnXoa.Enabled = true;          
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaSP.Enabled = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            if (tblSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (txtMaSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sql = "Update SP set TenSP=N'" + txtTenSP.Text.Trim().ToString() +"',Anh=N'" + txtAnh.Text + "',MaMau='" + txtMaMau.Text + "',MaNoiSX='" + txtMaNoiSX.Text + "',SL='" + txtSoLuong.Text +"',GiaNhap='" + txtGiaNhap.Text + "',GiaThue='" + txtGiaThue.Text + "',MaLoaiSP='" + txtMaLoaiSP.Text + "'where MaSP='" + txtMaSP.Text + "'";
            Functions.Runsql(sql);
            loadDataToGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from SanPham where MaSP='" + txtMaSP.Text + "'";
            Functions.Runsql(sql);
            loadDataToGridView();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemSanPham open = new TimKiemSanPham();
            open.ShowDialog();
        }

        
        private void resetvalue()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuong.Text = "";
            txtMaMau.Text = "";
            txtMaNoiSX.Text = "";
            txtGiaNhap.Text = "";
            txtGiaThue.Text = "";
            txtMaLoaiSP.Text = "";

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnTimKiem.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e)
        {
            double dgn, dgt;
            if (txtGiaNhap.Text == "") dgn = 0;
            else dgn = Convert.ToDouble(txtGiaNhap.Text);
            dgt = dgn * 0.5;
            txtGiaThue.Text = dgt.ToString();
            loadDataToGridView();
        }
    }
}
