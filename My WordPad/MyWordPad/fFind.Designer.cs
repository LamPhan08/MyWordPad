
namespace MyWordPad
{
    partial class fFind
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fFind));
            this.lblfindwhat = new System.Windows.Forms.Label();
            this.txttofind = new System.Windows.Forms.TextBox();
            this.btnfindnext = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbmatchcase = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDown = new System.Windows.Forms.RadioButton();
            this.rbUp = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblfindwhat
            // 
            this.lblfindwhat.AutoSize = true;
            this.lblfindwhat.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfindwhat.Location = new System.Drawing.Point(11, 17);
            this.lblfindwhat.Name = "lblfindwhat";
            this.lblfindwhat.Size = new System.Drawing.Size(58, 19);
            this.lblfindwhat.TabIndex = 0;
            this.lblfindwhat.Text = "Tìm gì:";
            // 
            // txttofind
            // 
            this.txttofind.Location = new System.Drawing.Point(92, 12);
            this.txttofind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txttofind.Name = "txttofind";
            this.txttofind.Size = new System.Drawing.Size(273, 22);
            this.txttofind.TabIndex = 1;
            this.txttofind.TextChanged += new System.EventHandler(this.txttofind_TextChanged);
            // 
            // btnfindnext
            // 
            this.btnfindnext.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfindnext.Location = new System.Drawing.Point(375, 12);
            this.btnfindnext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnfindnext.Name = "btnfindnext";
            this.btnfindnext.Size = new System.Drawing.Size(101, 30);
            this.btnfindnext.TabIndex = 3;
            this.btnfindnext.Text = "Tìm Tiếp";
            this.btnfindnext.UseVisualStyleBackColor = true;
            this.btnfindnext.Click += new System.EventHandler(this.btnfindnext_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(375, 47);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "Hủy bỏ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbmatchcase
            // 
            this.cbmatchcase.AutoSize = true;
            this.cbmatchcase.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbmatchcase.Location = new System.Drawing.Point(12, 62);
            this.cbmatchcase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmatchcase.Name = "cbmatchcase";
            this.cbmatchcase.Size = new System.Drawing.Size(97, 21);
            this.cbmatchcase.TabIndex = 5;
            this.cbmatchcase.Text = "Trùng khớp";
            this.cbmatchcase.UseVisualStyleBackColor = true;
            this.cbmatchcase.CheckedChanged += new System.EventHandler(this.cbmatchcase_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDown);
            this.groupBox1.Controls.Add(this.rbUp);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(165, 39);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(200, 48);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hướng";
            // 
            // rbDown
            // 
            this.rbDown.AutoSize = true;
            this.rbDown.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDown.Location = new System.Drawing.Point(71, 22);
            this.rbDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbDown.Name = "rbDown";
            this.rbDown.Size = new System.Drawing.Size(75, 23);
            this.rbDown.TabIndex = 1;
            this.rbDown.TabStop = true;
            this.rbDown.Text = "Xuống";
            this.rbDown.UseVisualStyleBackColor = true;
            // 
            // rbUp
            // 
            this.rbUp.AutoSize = true;
            this.rbUp.Location = new System.Drawing.Point(5, 22);
            this.rbUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbUp.Name = "rbUp";
            this.rbUp.Size = new System.Drawing.Size(56, 23);
            this.rbUp.TabIndex = 0;
            this.rbUp.TabStop = true;
            this.rbUp.Text = "Lên";
            this.rbUp.UseVisualStyleBackColor = true;
            this.rbUp.CheckedChanged += new System.EventHandler(this.rbUp_CheckedChanged);
            // 
            // fFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 96);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbmatchcase);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnfindnext);
            this.Controls.Add(this.txttofind);
            this.Controls.Add(this.lblfindwhat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fFind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm";
            this.Load += new System.EventHandler(this.fFind_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblfindwhat;
        private System.Windows.Forms.TextBox txttofind;
        private System.Windows.Forms.Button btnfindnext;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbmatchcase;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDown;
        private System.Windows.Forms.RadioButton rbUp;
    }
}