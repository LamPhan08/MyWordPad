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
    public partial class fHistory : Form
    {
        SqlConnection sqlConnection;
        SqlDataAdapter sqlDataAdapter;
        DataTable dataTable;
        BindingManagerBase bindingManagerBase;
        public fHistory()
        {
            InitializeComponent();
        }

        private void fHistory_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            try
            {
                sqlConnection = new SqlConnection("Server=DESKTOP-G8ANP0F\\SQLEXPRESS;Database=Users;Integrated Security=true");
                fLogin fLogin = new fLogin();
                SqlCommandBuilder sqlCommandBuilder;
                sqlDataAdapter = new SqlDataAdapter("select * from HISTORY", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                dataTable = new DataTable();
                sqlDataAdapter.FillSchema(dataTable, SchemaType.Mapped);
                sqlDataAdapter.Fill(dataTable);
                dgvHistory.DataSource = dataTable;
                bindingManagerBase = BindingContext[dataTable];
                bindingManagerBase.PositionChanged += BindingManagerBase_PositionChanged;
                BindingManagerBase_PositionChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindingManagerBase_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                if (bindingManagerBase.Position >= 0 && bindingManagerBase.Position <= bindingManagerBase.Count)
                {
                    DataRow row = dataTable.Rows[bindingManagerBase.Position];
                    txtusername.Text = row["USERNAME"].ToString();
                    txtdatetime.Text = row["TIMESTAMP"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bindingManagerBase.Position = 0;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bindingManagerBase.Position = bindingManagerBase.Count;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (bindingManagerBase.Position > 0)
                bindingManagerBase.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (bindingManagerBase.Position < bindingManagerBase.Count)
                bindingManagerBase.Position++;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Bạn có muốn xóa không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dataTable.Rows[bindingManagerBase.Position].Delete();
                    sqlDataAdapter.Update(dataTable);
                    BindingManagerBase_PositionChanged(sender, e);
                    MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
