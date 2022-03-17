using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWordPad
{
    public partial class fDateAndTime : Form
    {
        fMyWordPad m;
        public fDateAndTime()
        {
            InitializeComponent();
            lstbxDate.SelectedIndex = 0;
        }
        public fDateAndTime(fMyWordPad main)
        {
            m = main;
            InitializeComponent();
        }
        private DateTime NgayGio()
        {
            DateTime now = DateTime.Now;
            return now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                switch (lstbxDate.SelectedItem.ToString())
                {
                    case "11/21/2021": m.rtbDate.SelectedText = String.Format("{0:d}", NgayGio()); break;
                    case "Sunday, November 21, 2021": m.rtbDate.SelectedText = String.Format("{0:D}", NgayGio()); break;
                    case "2:41 PM": m.rtbDate.SelectedText = String.Format("{0:t}", NgayGio()); break;
                    case "2:41:05 PM": m.rtbDate.SelectedText = String.Format("{0:T}", NgayGio()); break;
                    case "Sunday, November 21, 2021 2:41 PM": m.rtbDate.SelectedText = String.Format("{0:f}", NgayGio()); break;
                    case "Sunday, November 21, 2021 2:41:05 PM": m.rtbDate.SelectedText = String.Format("{0:F}", NgayGio()); break;
                    case "11/21/2021 2:41 PM": m.rtbDate.SelectedText = String.Format("{0:g}", NgayGio()); break;
                    case "11/21/2021 2:41:05 PM": m.rtbDate.SelectedText = String.Format("{0:G}", NgayGio()); break;
                    default: break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fDateAndTime_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
