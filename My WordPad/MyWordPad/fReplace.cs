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
    public partial class fReplace : Form
    {
        FindNextSearch find = new FindNextSearch();
        public RichTextBox Editor;
        public Editing editing;
        fMyWordPad f1;

        public FindNextSearch Find { get => find; set => find = value; }

        public fReplace(fMyWordPad f)
        {
            f1 = f;
            InitializeComponent();
        }
        public fReplace()
        {
            InitializeComponent();
        }

        private bool LockFindNext(bool check)
        {
            btnfindnext.Enabled = !check;
            return btnfindnext.Enabled;
        }

        private bool LockReplace(bool check)
        {
            btnreplace.Enabled = !check;
            return btnreplace.Enabled;
        }

        private bool LockReplaceAll(bool check)
        {
            btnreplaceall.Enabled = !check;
            return btnreplaceall.Enabled;
        }

        private void btnfindnext_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSearchQuery();
                FindNextResult result = editing.FindNext(this.Find);
                if (result.SearchStatus)
                    Editor.Select(result.SelectionStart, txtfindwhat.Text.Length);
                else MessageBox.Show("Đã tìm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Đã tìm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void UpdateSearchQuery()
        {
            find.SearchString = txtfindwhat.Text;
            find.Direction = rbUp.Checked ? "UP" : "DOWN";
            find.MatchCase = cbmatchcase.Checked;
            find.Content = Editor.Text;
            find.Position = Editor.SelectionStart;
        }
        private void btnreplace_Click(object sender, EventArgs e)
        {
            try
            {
                if (Editor.SelectedText == "") btnfindnext.PerformClick();
                else Editor.SelectedText = txtreplacewith.Text;
            }
            catch
            {
                MessageBox.Show("Đã tìm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnreplaceall_Click(object sender, EventArgs e)
        {
            try
            {
                if (Editor.SelectedText == txtfindwhat.Text)
                {
                    if (txtreplacewith.Text != "")
                    {
                        Editor.Rtf = Editor.Rtf.Replace(txtfindwhat.Text, txtreplacewith.Text);
                    }
                    else
                    {
                        Editor.Rtf = Editor.Rtf.Replace(txtfindwhat.Text, "");
                    }

                }
                else
                {
                    MessageBox.Show("Đã tìm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //=============================================================================================================
            }
            catch (Exception)
            {
                MessageBox.Show("Đã tìm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fReplace_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            LockFindNext(true);
            LockReplace(true);
            LockReplaceAll(true);
            rbDown.Checked = true;
            if (Editor.SelectedText == "")
                txtfindwhat.Text = "";
            else
                txtfindwhat.Text = Editor.SelectedText;
        }

        private void txtfindwhat_TextChanged(object sender, EventArgs e)
        {
            if(txtfindwhat.Text == "")
            {
                LockFindNext(true);
                LockReplace(true);
                LockReplaceAll(true);
            }
            else
            {
                LockFindNext(false);
                LockReplace(false);
                LockReplaceAll(false);
            }
        }
    }
}
