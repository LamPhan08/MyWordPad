using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace MyWordPad
{
    public partial class fMyWordPad : Form
    {
        private byte tempBold = 0, tempItalic = 0, tempUnderline = 0, tempStrikethrough = 0,
             tempbullet = 0;//tempsubscript = 0, tempsuperscript = 0; sẽ bỏ 2 biến này.
        private bool isExit = true;
        private long checkModified = 0; //Biến này đếm số lần thay đổi trong file văn bản.
        Editing editing;
        public Editing Editing { get => editing; set => editing = value; }
        
        public fMyWordPad()
        {
            InitializeComponent();
            editing = new Editing();
            rchtbxText.HideSelection = false;
        }
        #region
        private void SaveSetting()
        {
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Height = this.Height;
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Font = rchtbxText.Font;
            Properties.Settings.Default.WordWrap = rchtbxText.WordWrap;
            Properties.Settings.Default.StatusBar = statusStrip1.Visible;
            Properties.Settings.Default.Save();
        }
        private void LoadSetting()
        {
            this.Location = Properties.Settings.Default.Location;
            this.Height = Properties.Settings.Default.Height;
            this.Width = Properties.Settings.Default.Width;
            rchtbxText.Font = Properties.Settings.Default.Font;
            rchtbxText.WordWrap = Properties.Settings.Default.WordWrap;
            cbwordwrap.Checked = rchtbxText.WordWrap;
            statusStrip1.Visible = Properties.Settings.Default.StatusBar;
            cbstatusbar.Checked = statusStrip1.Visible;
            LockCut(true);
            LockCopy(true);
            LockFind(true);
            LockReplace(true);
            LockSelectAll(true);
            //rchtbxText.Font = new Font("Calibri", rchtbxText.Font.Size);
            rchtbxText.Font = new Font("Calibri", 12);
        }

        private bool LockBullet(bool check)
        {
            rchtbxText.SelectionBullet = !check;
            return rchtbxText.SelectionBullet;
        }

        private bool LockCut(bool check)
        {
            btnCut.Enabled = !check;
            return btnCut.Enabled;
        }

        private bool LockCopy(bool check)
        {
            btnCopy.Enabled = !check;
            return btnCopy.Enabled;
        }

        private bool LockFind(bool check)
        {
            btnFind.Enabled = !check;
            return btnFind.Enabled;
        }

        private bool LockReplace(bool check)
        {
            btnReplace.Enabled = !check;
            return btnReplace.Enabled;
        }

        private bool LockSelectAll(bool check)
        {
            btnSelectAll.Enabled = !check;
            return btnSelectAll.Enabled;
        }

        private bool LockTSM_Cut(bool check)
        {
            tsm1_Cut.Enabled = !check;
            return tsm1_Cut.Enabled;
        }

        private bool LockTSM_Copy(bool check)
        {
            tsm2_Copy.Enabled = !check;
            return tsm2_Copy.Enabled;
        }

        private bool LockAdminitrator(bool check)
        {
            btnAdmin.Enabled = !check;
            return btnAdmin.Enabled;
        }
        
        private bool LockUndo(bool check)
        {
            btnUndo.Enabled = !check;
            return btnUndo.Enabled;
        }

        private bool LockRedo(bool check)
        {
            btnRedo.Enabled = !check;
            return btnRedo.Enabled;
        }
        private bool LockSubscript(bool check)
        {
            btnsubscript.Enabled = !check;
            return btnsubscript.Enabled;
        }
        private bool LockSuperscript(bool check)
        {
            btnsuperscript.Enabled = !check;
            return btnsuperscript.Enabled;
        }
        private void fMyWordPad_Load(object sender, EventArgs e)
        {
            LoadSetting();
            foreach (FontFamily font in FontFamily.Families)
            {
                cbbFontFamily.Items.Add(font.Name.ToString());
            }
            cbbFontFamily.Text = "Calibri";
            cbbFontSize.Text = "12";
            //LockBullet(true);
            rchtbxText.AcceptsTab = true;
            if (fLogin.priority == 1)
            {
                LockAdminitrator(false);
            }
            else
            {
                LockAdminitrator(true);
            }
            LockUndo(true);
            LockRedo(true);
            rchtbxText.EnableAutoDragDrop = true;
        }

        private void fMyWordPad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (ShowSafeCloseFormDialog() == "Cancel")
                {
                    e.Cancel = true;
                }
            }
        }

        private void fMyWordPad_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSetting();
            
        }

        
        private void rchtbxText_TextChanged(object sender, EventArgs e)
        {
            if (rchtbxText.Modified)
            {
                this.Text = "My WordPad - " + fname.Substring(fname.LastIndexOf("\\") + 1) + "*";
                
            }
            checkModified++;
            if (rchtbxText.CanUndo) LockUndo(false);
            else LockUndo(true);
            if (rchtbxText.CanRedo) LockRedo(false);
            else LockRedo(true);
            if (rchtbxText.Text == "")
            {
                LockCut(true);
                LockCopy(true);
                LockFind(true);
                LockReplace(true);
                LockSelectAll(true);
                LockTSM_Cut(true);
                LockTSM_Copy(true);
            }
            else
            {
                LockCut(false);
                LockCopy(false);
                LockFind(false);
                LockReplace(false);
                LockSelectAll(false);
                LockTSM_Cut(false);
                LockTSM_Copy(false);
            }
        }
        private void rchtbxText_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
        #endregion
        //==================================================================================================================================================================================
        //Ở mục "file"
        #region
        string fname = "";
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkModified != 0)
            {
                if (MessageBox.Show("Bạn có muốn lưu tập tin đang soạn thảo này không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }
            fname = "";
            rchtbxText.Text = "";
            this.Text = "My WordPad - " + fname;
            cbbFontFamily.Text = "Calibri";
            cbbFontSize.Text = "12";
            checkModified = 0;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open file";
            open.Filter = "Doc files (*.doc)|*.doc|Rtf files (*.rtf)|*.rtf|Txt files (*.txt)|*.txt";
            if (checkModified != 0)
            {
                if (MessageBox.Show("Bạn có muốn lưu tập tin đang soạn thảo này không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }
            if (open.ShowDialog() == DialogResult.OK)
            {
                fname = open.FileName;
                switch (Path.GetExtension(fname))
                {
                    case ".doc": case ".rtf": rchtbxText.LoadFile(fname, RichTextBoxStreamType.RichText); break;
                    case ".txt": rchtbxText.LoadFile(fname, RichTextBoxStreamType.PlainText); break;
                    default: break;
                }
                this.Text = "My Wordpad - " + fname.Substring(open.FileName.LastIndexOf("\\") + 1);
                checkModified = 0;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fname == "")
            {
                saveAsToolStripMenuItem_Click(null, null);
            }
            else
            {
                switch (Path.GetExtension(fname))
                {
                    case ".doc": case ".rtf": rchtbxText.SaveFile(fname, RichTextBoxStreamType.RichText); break;
                    case ".txt": rchtbxText.SaveFile(fname, RichTextBoxStreamType.PlainText); break;
                    default: break;
                }
                checkModified = 0;
                this.Text = "My Wordpad - " + fname.Substring(fname.LastIndexOf("\\") + 1);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save as";
            save.Filter = "Doc files (*.doc)|*.doc|Rtf Files (*.rtf)|*.rtf|Text files (*.txt)|*.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                fname = save.FileName;
                switch (Path.GetExtension(save.FileName))
                {
                    case ".doc": case ".rtf": rchtbxText.SaveFile(fname, RichTextBoxStreamType.RichText); break;
                    case ".txt": rchtbxText.SaveFile(fname, RichTextBoxStreamType.PlainText); break;
                    default: break;
                }
                checkModified = 0;
                this.Text = "My Wordpad - " + fname.Substring(save.FileName.LastIndexOf("\\") + 1);
            }
        }
        

        private void aboutMyWordpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAbout f3 = new fAbout(this);
            f3.Show(this);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkModified != 0)
            {
                if (MessageBox.Show("Bạn có muốn lưu tập tin đang soạn thảo này không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }
            isExit = false;
            Application.Exit();
            SaveSetting();
        }

        private string ShowSafeCloseFormDialog()
        {
            if (rchtbxText.Modified)
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu tập tin đang soạn thảo này không?", "Thông báo!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                    return "Yes";
                }
                else
                {
                    if (result == DialogResult.No)
                    {
                        return "No";
                    }
                    else
                    {
                        return "Cancel";
                    }
                }
            }
            return "Nothing";
        }

        //...

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkModified != 0)
                {
                    if (MessageBox.Show("Bạn có muốn lưu tập tin đang soạn thảo này không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        saveToolStripMenuItem_Click(null, null);
                    }
                }
                isExit = false;
                DialogResult r = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    SaveSetting();
                    //Close();
                    Hide();
                    fLogin login = new fLogin();
                    login.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (rchtbxText.CanUndo)
                rchtbxText.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (rchtbxText.CanRedo)
                rchtbxText.Redo();
        }
        #endregion
        //==================================================================================================================================================================================
        //Ở tab "Home"
        #region
        //1. Clipboard
        private void btnCut_Click(object sender, EventArgs e)
        {
            rchtbxText.Cut();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            rchtbxText.Copy();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            rchtbxText.Paste();
        }

        //2. Font
        //Tách ra hàm viết chữ ĐẬM
        private void btnBold_Click(object sender, EventArgs e)
        {
            try
            {
                if(rchtbxText.SelectedText != "")
                {
                    if (tempBold == 0)
                    {
                        int start = rchtbxText.SelectionStart,
                            end = rchtbxText.SelectionLength;
                        for (int i = start; i <= start + end - 1; i++)
                        {
                            rchtbxText.Select(i, 1);
                            rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Bold);
                        }
                        rchtbxText.Select(start, end);
                        tempBold = 1;
                        btnBold.BackColor = Color.White;
                        rchtbxText.Focus();
                    }
                    else
                    {
                        int start = rchtbxText.SelectionStart,
                            end = rchtbxText.SelectionLength;
                        for (int i = start; i <= start + end - 1; i++)
                        {
                            rchtbxText.Select(i, 1);
                            rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style & ~FontStyle.Bold);
                        }
                        rchtbxText.Select(start, end);
                        tempBold = 0;
                        btnBold.BackColor = Color.Transparent;
                        rchtbxText.Focus();
                    }
                }
                else
                {
                    if (tempBold == 0) 
                    {
                        //rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Bold);
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Bold);
                        tempBold = 1;
                        btnBold.BackColor = Color.White;
                        rchtbxText.Focus();
                    }
                    else
                    {
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style & ~FontStyle.Bold);
                        tempBold = 0;
                        btnBold.BackColor = Color.Transparent;
                        rchtbxText.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Tách ra hàm viết chữ NGHIÊNG
        private void btnItalic_Click(object sender, EventArgs e)
        {
            try
            {
                if(rchtbxText.SelectedText != "")
                {
                    if (tempItalic == 0)
                    {
                        int start = rchtbxText.SelectionStart,
                            end = rchtbxText.SelectionLength;
                        for (int i = start; i <= start + end - 1; i++)
                        {
                            rchtbxText.Select(i, 1);
                            rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Italic);
                        }
                        rchtbxText.Select(start, end);
                        tempItalic = 1;
                        btnItalic.BackColor = Color.White;
                        rchtbxText.Focus();
                    }
                    else
                    {
                        int start = rchtbxText.SelectionStart,
                            end = rchtbxText.SelectionLength;
                        for (int i = start; i <= start + end - 1; i++)
                        {
                            rchtbxText.Select(i, 1);
                            rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style & ~FontStyle.Italic);
                        }
                        rchtbxText.Select(start, end);
                        tempItalic = 0;
                        btnItalic.BackColor = Color.Transparent;
                        rchtbxText.Focus();
                    }
                }
                else
                {
                    if (tempItalic == 0)
                    {
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Italic);
                        tempItalic = 1;
                        btnItalic.BackColor = Color.White;
                        rchtbxText.Focus();
                    }
                    else
                    {
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style & ~FontStyle.Italic);
                        tempItalic = 0;
                        btnItalic.BackColor = Color.Transparent;
                        rchtbxText.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Tách ra hàm viết chữ GẠCH CHÂN
        private void btnUnderline_Click(object sender, EventArgs e)
        {
            try
            {
                if(rchtbxText.SelectedText != "")
                {
                    if (tempUnderline == 0)
                    {
                        int start = rchtbxText.SelectionStart,
                            end = rchtbxText.SelectionLength;
                        for (int i = start; i <= start + end - 1; i++)
                        {
                            rchtbxText.Select(i, 1);
                            rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Underline);
                        }
                        rchtbxText.Select(start, end);
                        tempUnderline = 1;
                        btnUnderline.BackColor = Color.White;
                        rchtbxText.Focus();
                    }
                    else
                    {
                        int start = rchtbxText.SelectionStart,
                            end = rchtbxText.SelectionLength;
                        for (int i = start; i <= start + end - 1; i++)
                        {
                            rchtbxText.Select(i, 1);
                            rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style & ~FontStyle.Underline);
                        }
                        rchtbxText.Select(start, end);
                        tempUnderline = 0;
                        btnUnderline.BackColor = Color.Transparent;
                        rchtbxText.Focus();
                    }
                }
                else
                {
                    if (tempUnderline == 0)
                    {
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Underline);
                        tempUnderline = 1;
                        btnUnderline.BackColor = Color.White;
                        rchtbxText.Focus();
                    }
                    else
                    {
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style & ~FontStyle.Underline);
                        tempUnderline = 0;
                        btnUnderline.BackColor = Color.Transparent;
                        rchtbxText.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Tách ra hàm viết chữ GẠCH NGANG
        private void btnStrikethrough_Click(object sender, EventArgs e)
        {
            try
            {
                if(rchtbxText.SelectedText != "")
                {
                    if (tempStrikethrough == 0)
                    {
                        int start = rchtbxText.SelectionStart,
                            end = rchtbxText.SelectionLength;
                        for (int i = start; i <= start + end - 1; i++)
                        {
                            rchtbxText.Select(i, 1);
                            rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Strikeout);
                        }
                        rchtbxText.Select(start, end);
                        tempStrikethrough = 1;
                        btnStrikethrough.BackColor = Color.White;
                        rchtbxText.Focus();
                    }
                    else
                    {
                        int start = rchtbxText.SelectionStart,
                            end = rchtbxText.SelectionLength;
                        for (int i = start; i <= start + end - 1; i++)
                        {
                            rchtbxText.Select(i, 1);
                            rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style & ~FontStyle.Strikeout);
                        }
                        rchtbxText.Select(start, end);
                        tempStrikethrough = 0;
                        btnStrikethrough.BackColor = Color.Transparent;
                        rchtbxText.Focus();
                    }
                }
                else
                {
                    if (tempStrikethrough == 0)
                    {
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style | FontStyle.Strikeout);
                        tempStrikethrough = 1;
                        btnStrikethrough.BackColor = Color.White;
                        rchtbxText.Focus();
                    }
                    else
                    {
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style & ~FontStyle.Strikeout);
                        tempStrikethrough = 0;
                        btnStrikethrough.BackColor = Color.Transparent;
                        rchtbxText.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTextColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (rchtbxText.SelectedText != "")
                {
                    rchtbxText.SelectionColor = colorDialog.Color;
                    btnfordecoration1.BackColor = colorDialog.Color;
                }
                else
                {
                    rchtbxText.SelectionColor = colorDialog.Color;
                    btnfordecoration1.BackColor = colorDialog.Color;
                }
            }
        }

        private void btnhighlight_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if(rchtbxText.SelectedText != "")
                {
                    rchtbxText.SelectionBackColor = colorDialog.Color;
                    btnfordecoration2.BackColor = colorDialog.Color;
                }
                else
                {
                    rchtbxText.SelectionBackColor = colorDialog.Color;
                    btnfordecoration2.BackColor = colorDialog.Color;
                }
            }
        }

        //...
        
        
        private void cbbFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rchtbxText.SelectedText == "")
            {
                //rchtbxText.SelectionFont = new Font(cbbFontFamily.Text, rchtbxText.Font.Size);
                rchtbxText.SelectionFont = new Font(cbbFontFamily.Text, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style);
                return;
            }
            else
            {

                //rchtbxText.SelectionFont = new Font(cbbFontFamily.Text, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style);
                int start = rchtbxText.SelectionStart,
                    end = rchtbxText.SelectionLength;
                for (int i = start; i <= start + end - 1; i++)
                {
                    rchtbxText.Select(i, 1);
                    rchtbxText.SelectionFont = new Font(cbbFontFamily.Text, rchtbxText.SelectionFont.Size, rchtbxText.SelectionFont.Style);
                    //rchtbxText.SelectionFont = new Font(cbbFontFamily.Text, int.Parse(cbbFontSize.Text.ToString()), rchtbxText.SelectionFont.Style);
                }
                rchtbxText.Select(start, end);
            }
        }

        //Hàm thay đổi kích thước chữ
        void ChangingSize(RichTextBox rtb, float size)
        {
            try
            {
                //Font newFont = new Font(rtb.SelectionFont.FontFamily.Name, size, rchtbxText.SelectionFont.Style);
                //rtb.SelectionFont = newFont;
                int start = rtb.SelectionStart,
                        end = rtb.SelectionLength;
                for (int i = start; i <= start + end - 1; i++)
                {
                    rtb.Select(i, 1);
                    rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily.Name, size, rchtbxText.SelectionFont.Style);
                }
                rtb.Select(start, end);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cbbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rchtbxText.SelectedText == "")
                {
                    rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, float.Parse(cbbFontSize.Text), rchtbxText.SelectionFont.Style);
                    return;
                }
                else
                {
                    ChangingSize(rchtbxText, int.Parse(cbbFontSize.Text));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btngrowfont_Click(object sender, EventArgs e)
        {
            int a = int.Parse(cbbFontSize.Text.ToString());
            if (a <= 72)
            {
                a += 2;
                cbbFontSize.Text = a.ToString();
                ChangingSize(rchtbxText, a);
            }
        }

        private void btnshrinkfont_Click(object sender, EventArgs e)
        {
            int a = int.Parse(cbbFontSize.Text.ToString());
            if (a >= 8)
            {
                a -= 2;
                cbbFontSize.Text = a.ToString();
                ChangingSize(rchtbxText, a);
            }
        }
        private void btnsubscript_Click(object sender, EventArgs e)
        {
            try
            {
                if(rchtbxText.SelectedText != "")
                {
                    //float a = (cbbFontSize.Text != "") ? int.Parse(cbbFontSize.Text) : 8.0f;
                    float a = (rchtbxText.SelectionFont.Size >= 8.0f) ? int.Parse(cbbFontSize.Text) : 8.0f;
                    int b = int.Parse(a.ToString());
                    //===============================================================================================================================
                    if (rchtbxText.SelectionCharOffset == -b / 2)
                    {
                        rchtbxText.SelectionCharOffset += b / 2;
                        ChangingSize(rchtbxText, a);
                    }
                    else if (rchtbxText.SelectionCharOffset == b / 2)
                    {
                        rchtbxText.SelectionCharOffset -= b;
                        ChangingSize(rchtbxText, (a * 1.0f / 1.5f));
                    }
                    else
                    {
                        rchtbxText.SelectionCharOffset -= b / 2;
                        ChangingSize(rchtbxText, (a * 1.0f / 1.5f));
                    }

                }
                else
                {
                    //float a = (cbbFontSize.Text != "") ? int.Parse(cbbFontSize.Text) : 8.0f;
                    float a = (rchtbxText.SelectionFont.Size >= 8.0f) ? int.Parse(cbbFontSize.Text) : 8.0f;
                    int b = int.Parse(a.ToString());
                    //============================================================================================================================================
                    if (rchtbxText.SelectionCharOffset == -b / 2)
                    {
                        rchtbxText.SelectionCharOffset += b / 2;
                        //ChangingSize(rchtbxText, a);
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, a, rchtbxText.SelectionFont.Style);
                    }
                    else if (rchtbxText.SelectionCharOffset == b / 2)
                    {
                        rchtbxText.SelectionCharOffset -= b;
                        //ChangingSize(rchtbxText, (a * 1.0f / 1.5f));
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, (a * 1.0f / 1.5f), rchtbxText.SelectionFont.Style);
                    }
                    else
                    {
                        rchtbxText.SelectionCharOffset -= b / 2;
                        //ChangingSize(rchtbxText, (a * 1.0f / 1.5f));
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, (a * 1.0f / 1.5f), rchtbxText.SelectionFont.Style);
                    }
                    //==============================================================================================================================================
                }
            }
            catch
            {
                MessageBox.Show("Không chuyển đổi được đoạn văn bản này!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsuperscript_Click(object sender, EventArgs e)
        {
            try
            {
                if(rchtbxText.SelectedText != "")
                {
                    //float a = (cbbFontSize.Text != "") ? int.Parse(cbbFontSize.Text) : 8.0f;
                    float a = (rchtbxText.SelectionFont.Size >= 8.0f) ? int.Parse(cbbFontSize.Text) : 8.0f;
                    int b = int.Parse(a.ToString());
                    //===============================================================================================================================
                    if (rchtbxText.SelectionCharOffset == -b / 2)
                    {
                        rchtbxText.SelectionCharOffset += b;
                        ChangingSize(rchtbxText, (a * 1.0f / 1.5f));
                    }
                    else if (rchtbxText.SelectionCharOffset == b / 2)
                    {
                        rchtbxText.SelectionCharOffset -= b / 2;
                        ChangingSize(rchtbxText, a);
                    }
                    else
                    {
                        rchtbxText.SelectionCharOffset += b / 2;
                        ChangingSize(rchtbxText, (a * 1.0f / 1.5f));
                    }
                }
                else
                {
                    float a = (rchtbxText.SelectionFont.Size >= 8.0f) ? int.Parse(cbbFontSize.Text) : 8.0f;
                    int b = int.Parse(a.ToString());
                    if (rchtbxText.SelectionCharOffset == -b / 2)
                    {
                        rchtbxText.SelectionCharOffset += b;
                        //ChangingSize(rchtbxText, (a * 1.0f / 1.5f));
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, (a * 1.0f / 1.5f), rchtbxText.SelectionFont.Style);
                    }
                    else if (rchtbxText.SelectionCharOffset == b / 2)
                    {
                        rchtbxText.SelectionCharOffset -= b / 2;
                        //ChangingSize(rchtbxText, a);
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, a, rchtbxText.SelectionFont.Style);
                    }
                    else
                    {
                        rchtbxText.SelectionCharOffset += b / 2;
                        //ChangingSize(rchtbxText, (a * 1.0f / 1.5f));
                        rchtbxText.SelectionFont = new Font(rchtbxText.SelectionFont.FontFamily.Name, (a * 1.0f / 1.5f), rchtbxText.SelectionFont.Style);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Không chuyển đổi được đoạn văn bản này!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //3. Paragraph
        private void btnAlignTextLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (rchtbxText.Text == "")
                {
                    rchtbxText.SelectAll();
                    rchtbxText.SelectionAlignment = HorizontalAlignment.Left;
                    rchtbxText.Focus();
                }
                else
                {
                    rchtbxText.SelectionAlignment = HorizontalAlignment.Left;
                    rchtbxText.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCenterText_Click(object sender, EventArgs e)
        {
            try
            {
                if (rchtbxText.Text == "")
                {
                    rchtbxText.SelectAll();
                    rchtbxText.SelectionAlignment = HorizontalAlignment.Center;
                    rchtbxText.Focus();
                }
                else
                {
                    rchtbxText.SelectionAlignment = HorizontalAlignment.Center;
                    rchtbxText.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlignTextRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (rchtbxText.Text == "")
                {
                    rchtbxText.SelectAll();
                    rchtbxText.SelectionAlignment = HorizontalAlignment.Right;
                    rchtbxText.Focus();
                }
                else
                {
                    rchtbxText.SelectionAlignment = HorizontalAlignment.Right;
                    rchtbxText.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndecreaseindent_Click(object sender, EventArgs e)
        {
            rchtbxText.SelectionIndent -= 40;//lôi lề của text ra ngoài
        }

        private void btnincreaseindent_Click(object sender, EventArgs e)
        {
            rchtbxText.SelectionIndent += 40;
        }

        private void btnbullet_Click(object sender, EventArgs e)
        {
            try
            {
                if (tempbullet == 0)
                {
                    LockBullet(false);
                    rchtbxText.SelectionIndent = 30;
                    rchtbxText.Focus();
                    tempbullet = 1;
                    cbbFontFamily_SelectedIndexChanged(null, null);
                }
                else
                {
                    LockBullet(true);
                    rchtbxText.SelectionIndent = 0;
                    rchtbxText.Focus();
                    tempbullet = 0;
                    cbbFontFamily_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //4. Insert
        private void btnPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Chèn hình ảnh";
            open.DefaultExt = "rtf";
            open.Filter = "PNG Files|*.png|JPG files|*.jpg|Bitmap Files|*.bmp|JPEG Files|*.jpeg|GIF Files|*.gif";
            open.FilterIndex = 1;
            open.ShowDialog();
            if (open.FileName == "")
            {
                return;
            }
            try
            {
                string strImagePath = open.FileName;
                Image img;
                img = Image.FromFile(strImagePath);
                Clipboard.SetDataObject(img);
                DataFormats.Format df;
                df = DataFormats.GetFormat(DataFormats.Bitmap);
                if (this.rchtbxText.CanPaste(df))
                {
                    this.rchtbxText.Paste(df);
                }
            }
            catch
            {
                MessageBox.Show("Không thể chèn hình ảnh!", "Lỗi!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        public RichTextBox rtbDate = new RichTextBox();
        private void btnDateAndTime_Click(object sender, EventArgs e)
        {
            fDateAndTime fDate = new fDateAndTime(this);
            rtbDate = rchtbxText;
            fDate.Show(this);
        }
        //5. Editing
        public RichTextBox rtb = new RichTextBox();


        fFind fFind;
        //hàm tìm chuỗi trùng với chuỗi cần tìm (kể cả về độ dài)
        private void btnFind_Click(object sender, EventArgs e)
        {
            fFind = new fFind(this);
            fFind.Editor = rchtbxText;
            fFind.Show(this);
        }
        fReplace fReplace;

        private void btnReplace_Click(object sender, EventArgs e)
        {
            //fReplace f3 = new fReplace(this);
            //rtb = rchtbxText;
            //f3.Show(this);
            fReplace = new fReplace(this);
            fReplace.Editor = rchtbxText;
            fReplace.editing = editing;
            fReplace.Show(this);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            rchtbxText.SelectAll();
            rchtbxText.Focus();
        }

        //...
        #endregion
        //==================================================================================================================================================================================
        //Ở tab "View"
        #region
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            double a = double.Parse(toolStripStatusLabel1.Text);
            try
            {
                float zoom = rchtbxText.ZoomFactor;
                if (zoom + 0.5 < 64)
                {
                    rchtbxText.ZoomFactor = zoom + 0.5f;
                }
                int x = rchtbxText.Location.X;
                Point p;
                if (x >= 0)
                {
                    rchtbxText.Width += 16;
                    x -= 8;
                    p = new Point(x, rchtbxText.Location.Y);
                    rchtbxText.Location = p;
                    a += 10;
                    toolStripStatusLabel1.Text = a.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            double a = double.Parse(toolStripStatusLabel1.Text);
            try
            {
                float zoom = rchtbxText.ZoomFactor;
                if (zoom - 0.5 >= 1 )
                {
                    rchtbxText.ZoomFactor = zoom - 0.5f;
                }
                int x = rchtbxText.Location.X;
                Point p;
                if (x <= 362 && a > 50)
                {
                    rchtbxText.Width -= 16;
                    x += 8;
                    p = new Point(x, rchtbxText.Location.Y);
                    rchtbxText.Location = p;
                    a -= 10;
                    toolStripStatusLabel1.Text = a.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbwordwrap_CheckedChanged(object sender, EventArgs e)
        {
            if (cbwordwrap.Checked == true)
                rchtbxText.WordWrap = true;
            else
                rchtbxText.WordWrap = false;
        }

        //...

        private void cbstatusbar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbstatusbar.Checked == true)
                statusStrip1.Visible = true;
            else
                statusStrip1.Visible = false;
        }

        private void rchtbxText_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            fUsersManager fUsersManager = new fUsersManager();
            fUsersManager.lblHienThiUser.Text = sttlblUser.Text;
            fUsersManager.Show();
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fHistory fHistory = new fHistory();
            fHistory.Show();
        }

        

        private void UpdateStatus()
        {
            try
            {
                int position = rchtbxText.SelectionStart;
                int line = rchtbxText.GetLineFromCharIndex(position) + 1;
                int column = position - rchtbxText.GetFirstCharIndexOfCurrentLine() + 1;
                status1.Text = "Ln " + line + ", Col " + column;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Thông báo lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        //==================================================================================================================================================================================
        //Một số sự kiện đối với màn hình form
        #region
        private void tsm1_Cut_Click(object sender, EventArgs e)
        {
            btnCut_Click(null, null);
        }

        private void tbHome_Click(object sender, EventArgs e)
        {

        }

        private void tsm2_Copy_Click(object sender, EventArgs e)
        {
            btnCopy_Click(null, null);
        }

        private void tsm3_Paste_Click(object sender, EventArgs e)
        {
            btnPaste_Click(null, null);
        }

        

        private void tsm4_Save_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(null, null);
        }

        private void tsm5_SaveAs_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem_Click(null, null);
        }
        #endregion

        private void InsertSymbols(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Inserting(btn.Text);
        }

        private void Inserting(string number)
        {
            rchtbxText.SelectedText = number;
        }
    }
}
