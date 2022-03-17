using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyWordPad
{
    public partial class fDangKy : Form
    {
        public fDangKy()
        {
            InitializeComponent();
        }

        private void chkbxHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxHienThiMatKhau.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
                txtXacNhanMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
                txtXacNhanMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtXacNhanMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (txtXacNhanMatKhau.Text == txtMatKhau.Text)
            {
                lblCheckMatKhau.ForeColor = Color.Green;
                lblCheckMatKhau.Text = "Mật khẩu trùng khớp";
                btnDangKy.Enabled = true;
            }
            else
            {
                lblCheckMatKhau.ForeColor = Color.Red;
                lblCheckMatKhau.Text = "Mật khẩu không trùng khớp";
                btnDangKy.Enabled = false;
            }
        }

        private void fDangKy_Load(object sender, EventArgs e)
        {
            btnDangKy.Enabled = false;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTenNguoiDung.Text == "")
                {
                    MessageBox.Show("Đăng ký không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenNguoiDung.Clear();
                    txtMatKhau.Clear();
                    txtXacNhanMatKhau.Clear();
                    txtTenNguoiDung.Focus();
                    lblCheckMatKhau.Text = "";
                }
                else
                {
                    SqlConnection connection = new SqlConnection("Server=DESKTOP-G8ANP0F\\SQLEXPRESS;Database=Users;Integrated Security=true");
                    connection.Open();
                    SqlCommand command = new SqlCommand("insert into INFORMATION values ('" + txtTenNguoiDung.Text + "', '" + txtMatKhau.Text + "', " + 0 + ")", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenNguoiDung.Clear();
                    txtMatKhau.Clear();
                    txtXacNhanMatKhau.Clear();
                    txtTenNguoiDung.Focus();
                    lblCheckMatKhau.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Tên người dùng đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenNguoiDung.Clear();
                txtMatKhau.Clear();
                txtXacNhanMatKhau.Clear();
                txtTenNguoiDung.Focus();
                lblCheckMatKhau.Text = "";
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "" && txtXacNhanMatKhau.Text == "")
            {
                lblCheckMatKhau.Text = "";
            }
            else
            {
                if (txtXacNhanMatKhau.Text == txtMatKhau.Text)
                {
                    lblCheckMatKhau.ForeColor = Color.Green;
                    lblCheckMatKhau.Text = "Mật khẩu trùng khớp";
                    btnDangKy.Enabled = true;
                }
                else
                {
                    lblCheckMatKhau.ForeColor = Color.Red;
                    lblCheckMatKhau.Text = "Mật khẩu không trùng khớp";
                }
            }
            if (txtMatKhau.Text == "" && txtXacNhanMatKhau.Text != "")
            {
                lblCheckMatKhau.Text = "";
            }
        }
    }
}
