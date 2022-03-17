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
    public partial class fLogin : Form
    {
        public static int priority;
        public DateTime Now()
        {
            DateTime now = DateTime.Now;
            return now;
        }
        public fLogin()
        {
            InitializeComponent();
        }

        private bool LockLogin(bool check)
        {
            btnLogin.Enabled = !check;
            return btnLogin.Enabled;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            
            try
            {
                Database.connection = "Server=DESKTOP-G8ANP0F\\SQLEXPRESS;Database=Users;Integrated Security=true";
                Database TableRight = new Database("INFORMATION", "select PRIORITY from INFORMATION where USERNAME = '" + txtUsername.Text
                    + "' and USER_PASSWORD = '" + txtPassword.Text + "'");
                if (TableRight.Rows.Count > 0)
                {
                    if (TableRight.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show(txtUsername.Text + " đã đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        priority = 1;
                        Database.sqlConnection.Open();//String.Format("{0:f}", DateTime.Now)           DateTime.Now
                        SqlCommand sqlCommand = new SqlCommand("insert into HISTORY values ('" + txtUsername.Text + "', '" + String.Format("{0:G}", Now()) + "')", Database.sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        Database.sqlConnection.Close();
                        fMyWordPad myWordPad = new fMyWordPad();
                        Hide();
                        myWordPad.sttlblUser.Text = txtUsername.Text;
                        myWordPad.ShowDialog();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(txtUsername.Text + " đã đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        priority = 0;
                        Database.sqlConnection.Open();
                        SqlCommand sqlCommand = new SqlCommand("insert into HISTORY values ('" + txtUsername.Text + "', '" + String.Format("{0:G}", Now()) + "')", Database.sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        Database.sqlConnection.Close();
                        fMyWordPad myWordPad = new fMyWordPad();
                        Hide();
                        myWordPad.sttlblUser.Text = txtUsername.Text;
                        myWordPad.ShowDialog();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                LockLogin(true);
            }
            else
            {
                LockLogin(false);
            }
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtUsername.Focus();
            LockLogin(true);
        }

        private void chkHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienThiMatKhau.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else 
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            fDangKy dangKy = new fDangKy();
            dangKy.ShowDialog();
        }
    }
}
