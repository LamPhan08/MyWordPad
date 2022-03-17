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
    public partial class fUsersManager : Form
    {
        SqlConnection sqlConnection;
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
        BindingManagerBase managerBase;
        bool isAdded = false;
        public fUsersManager()
        {
            InitializeComponent();
            cbbpriority.SelectedIndex = 0;
        }

        private void fUsersManager_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            try
            {
                sqlConnection = new SqlConnection("Server=DESKTOP-G8ANP0F\\SQLEXPRESS;Database=Users;Integrated Security=true");
                dataAdapter = new SqlDataAdapter("select * from INFORMATION", sqlConnection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);
                dataTable = new DataTable();
                dataAdapter.FillSchema(dataTable, SchemaType.Mapped);
                dataAdapter.Fill(dataTable);
                dgvUsers.DataSource = dataTable;
                managerBase = BindingContext[dataTable];
                managerBase.PositionChanged += ManagerBase_PositionChanged;
                ManagerBase_PositionChanged(null, null);
                LockControl(true);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManagerBase_PositionChanged(object sender, EventArgs e)
        {
            if(managerBase.Position >= 0)
            {
                DataRow row = dataTable.Rows[managerBase.Position];
                txtusername.Text = row["USERNAME"].ToString();
                txtpassword.Text = row["USER_PASSWORD"].ToString();
                cbbpriority.Text = (row["PRIORITY"].ToString() == "1")? "Administrator": "User";
            }
        }

        private void LockControl(bool flag)
        {
            txtusername.ReadOnly = txtpassword.ReadOnly = flag;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            managerBase.Position = 0;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (managerBase.Position > 0)
                managerBase.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (managerBase.Position < managerBase.Count)
                managerBase.Position++;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            managerBase.Position = managerBase.Count;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            isAdded = true;
            txtusername.Text = txtpassword.Text = cbbpriority.Text = "";
            LockControl(false);
            txtusername.Focus();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtusername.Text == "" || txtpassword.Text == "")
                {
                    MessageBox.Show("Không thể cập nhật dữ liệu!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataRow row;
                    if (isAdded)
                        row = dataTable.NewRow();
                    else
                        row = dataTable.Rows[managerBase.Position];
                    row["USERNAME"] = txtusername.Text;
                    row["USER_PASSWORD"] = txtpassword.Text;
                    switch (cbbpriority.SelectedItem.ToString())
                    {
                        case "User": row["PRIORITY"] = 0; break;
                        case "Administrator": row["PRIORITY"] = 1; break;
                        default: break;
                    }
                    if (isAdded)
                    {
                        dataTable.Rows.Add(row);
                        managerBase.Position = managerBase.Count;
                    }
                    dataAdapter.Update(dataTable);
                    dataTable.AcceptChanges();
                    isAdded = false;
                    LockControl(true);
                    MessageBox.Show("Đã cập nhật dữ liệu thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Tên người dùng đã được sử dụng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusername.Clear();
                txtpassword.Clear();
                txtusername.Focus();
            }
        }

        private void btnNoWrite_Click(object sender, EventArgs e)
        {
            isAdded = false;
            LockControl(true);
            ManagerBase_PositionChanged(sender, e);
        }

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtusername.ReadOnly = true;
            txtpassword.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Bạn có muốn xóa người dùng này không?", "Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row = dataTable.Rows[managerBase.Position];
                    if (row["USERNAME"].ToString() == lblHienThiUser.Text)
                    {
                        dataTable.RejectChanges();
                        MessageBox.Show("Không thể xóa vì người dùng đang truy cập!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        sqlConnection = new SqlConnection("Server=DESKTOP-G8ANP0F\\SQLEXPRESS;Database=Users;Integrated Security=true");
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand("Delete from HISTORY where USERNAME = '" + row["USERNAME"].ToString() + "'", sqlConnection);
                        command.ExecuteNonQuery();
                        sqlConnection.Close();
                        dataTable.Rows[managerBase.Position].Delete();
                        dataAdapter.Update(dataTable);
                        ManagerBase_PositionChanged(null, null);
                        MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception)
            {
                dataTable.RejectChanges();
                MessageBox.Show("Không thể xóa người dùng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
