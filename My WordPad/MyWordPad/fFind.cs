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
    public partial class fFind : Form
    {
        fMyWordPad f1;
        Editing editing;
        FindNextSearch find = new FindNextSearch();
        public RichTextBox Editor { get; internal set; }
        public Editing Editing { get => editing; set => editing = value; }

        public fFind(fMyWordPad f)
        {
            InitializeComponent();
            f1 = f;
            rbDown.Checked = true;
            editing = f1.Editing;
            find.Success = false;
        }
        public fFind()
        {
            InitializeComponent();
        }

        private bool LockFindNext(bool check)
        {
            btnfindnext.Enabled = !check;
            return btnfindnext.Enabled;
        }

        private void btnfindnext_Click(object sender, EventArgs e)
        {
            try
            {
                //if (f1.rtb.Text.Trim() == "")//Trim là phương thức dùng để cắt đầu và cuối chuỗi
                //{
                //    MessageBox.Show("Văn bản trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.Close();
                //}

                //else if (txttofind.Text.Trim() == "")
                //{
                //    MessageBox.Show("Mời bạn nhập chuỗi cần tìm");
                //    txttofind.Focus();
                //    return;
                //}
                //else
                //{
                //    int point = f1.rtb.SelectionStart + 1;//lấy điểm bắt đầu của đoạn text đang được bôi đen
                //    StringComparison type;
                //    if (cbmatchcase.Checked == true)
                //        type = StringComparison.Ordinal;
                //    else
                //        type = StringComparison.OrdinalIgnoreCase;
                //    point = f1.rtb.Text.IndexOf(txttofind.Text.Trim(), point, type);
                //    if (point < 0 || point == 0)
                //    {
                //        MessageBox.Show("Đã tìm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //    f1.rtb.Select(point, txttofind.Text.Length);
                //    f1.rtb.ScrollToCaret();
                //    f1.Focus();
                //}
                UpdateSearchQuery();
                FindNextResult result = editing.FindNext(find);
                if (result.SearchStatus)
                    Editor.Select(result.SelectionStart, txttofind.Text.Length);
                else MessageBox.Show("Đã tìm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Đã tìm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txttofind_TextChanged(object sender, EventArgs e)
        {
            if(txttofind.Text == "")
            {
                LockFindNext(true);
                UpdateSearchQuery();
            }
            else
            {
                LockFindNext(false);
                UpdateSearchQuery();
            }
        }
        public void UpdateSearchQuery()
        {
            find.SearchString = txttofind.Text;
            find.Direction = rbUp.Checked ? "UP" : "DOWN";
            find.MatchCase = cbmatchcase.Checked;
            find.Content = Editor.Text;
            find.Position = Editor.SelectionStart;
        }

        private void fFind_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            LockFindNext(true);
            if (Editor.SelectedText == "")
                txttofind.Text = "";
            else
                txttofind.Text = Editor.SelectedText;
        }

        private void cbmatchcase_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSearchQuery();
        }

        private void rbUp_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSearchQuery();
        }
    }
}
