
namespace MyWordPad
{
    partial class fReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fReplace));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtfindwhat = new System.Windows.Forms.TextBox();
            this.txtreplacewith = new System.Windows.Forms.TextBox();
            this.btnfindnext = new System.Windows.Forms.Button();
            this.btnreplace = new System.Windows.Forms.Button();
            this.btnreplaceall = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.cbmatchcase = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDown = new System.Windows.Forms.RadioButton();
            this.rbUp = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm gì:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Thay thế bằng:";
            // 
            // txtfindwhat
            // 
            this.txtfindwhat.Location = new System.Drawing.Point(112, 14);
            this.txtfindwhat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtfindwhat.Name = "txtfindwhat";
            this.txtfindwhat.Size = new System.Drawing.Size(265, 22);
            this.txtfindwhat.TabIndex = 2;
            this.txtfindwhat.TextChanged += new System.EventHandler(this.txtfindwhat_TextChanged);
            // 
            // txtreplacewith
            // 
            this.txtreplacewith.Location = new System.Drawing.Point(112, 47);
            this.txtreplacewith.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtreplacewith.Name = "txtreplacewith";
            this.txtreplacewith.Size = new System.Drawing.Size(265, 22);
            this.txtreplacewith.TabIndex = 3;
            // 
            // btnfindnext
            // 
            this.btnfindnext.AutoSize = true;
            this.btnfindnext.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfindnext.Location = new System.Drawing.Point(393, 11);
            this.btnfindnext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnfindnext.Name = "btnfindnext";
            this.btnfindnext.Size = new System.Drawing.Size(135, 31);
            this.btnfindnext.TabIndex = 4;
            this.btnfindnext.Text = "Tìm Tiếp";
            this.btnfindnext.UseVisualStyleBackColor = true;
            this.btnfindnext.Click += new System.EventHandler(this.btnfindnext_Click);
            // 
            // btnreplace
            // 
            this.btnreplace.AutoSize = true;
            this.btnreplace.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreplace.Location = new System.Drawing.Point(393, 44);
            this.btnreplace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnreplace.Name = "btnreplace";
            this.btnreplace.Size = new System.Drawing.Size(135, 31);
            this.btnreplace.TabIndex = 5;
            this.btnreplace.Text = "Thay thế";
            this.btnreplace.UseVisualStyleBackColor = true;
            this.btnreplace.Click += new System.EventHandler(this.btnreplace_Click);
            // 
            // btnreplaceall
            // 
            this.btnreplaceall.AutoSize = true;
            this.btnreplaceall.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreplaceall.Location = new System.Drawing.Point(393, 78);
            this.btnreplaceall.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnreplaceall.Name = "btnreplaceall";
            this.btnreplaceall.Size = new System.Drawing.Size(135, 31);
            this.btnreplaceall.TabIndex = 6;
            this.btnreplaceall.Text = "Thay thế Tất cả";
            this.btnreplaceall.UseVisualStyleBackColor = true;
            this.btnreplaceall.Click += new System.EventHandler(this.btnreplaceall_Click);
            // 
            // btncancel
            // 
            this.btncancel.AutoSize = true;
            this.btncancel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(393, 111);
            this.btncancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(135, 31);
            this.btncancel.TabIndex = 7;
            this.btncancel.Text = "Hủy bỏ";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // cbmatchcase
            // 
            this.cbmatchcase.AutoSize = true;
            this.cbmatchcase.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbmatchcase.Location = new System.Drawing.Point(12, 95);
            this.cbmatchcase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmatchcase.Name = "cbmatchcase";
            this.cbmatchcase.Size = new System.Drawing.Size(106, 23);
            this.cbmatchcase.TabIndex = 8;
            this.cbmatchcase.Text = "Trùng khớp";
            this.cbmatchcase.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDown);
            this.groupBox1.Controls.Add(this.rbUp);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(223, 74);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(165, 59);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hướng";
            // 
            // rbDown
            // 
            this.rbDown.Location = new System.Drawing.Point(75, 26);
            this.rbDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbDown.Name = "rbDown";
            this.rbDown.Size = new System.Drawing.Size(84, 28);
            this.rbDown.TabIndex = 1;
            this.rbDown.TabStop = true;
            this.rbDown.Text = "Xuống";
            this.rbDown.UseVisualStyleBackColor = true;
            // 
            // rbUp
            // 
            this.rbUp.Location = new System.Drawing.Point(5, 26);
            this.rbUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbUp.Name = "rbUp";
            this.rbUp.Size = new System.Drawing.Size(79, 28);
            this.rbUp.TabIndex = 0;
            this.rbUp.TabStop = true;
            this.rbUp.Text = "Lên";
            this.rbUp.UseVisualStyleBackColor = true;
            // 
            // fReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 145);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbmatchcase);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnreplaceall);
            this.Controls.Add(this.btnreplace);
            this.Controls.Add(this.btnfindnext);
            this.Controls.Add(this.txtreplacewith);
            this.Controls.Add(this.txtfindwhat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thay thế";
            this.Load += new System.EventHandler(this.fReplace_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtfindwhat;
        private System.Windows.Forms.TextBox txtreplacewith;
        private System.Windows.Forms.Button btnfindnext;
        private System.Windows.Forms.Button btnreplace;
        private System.Windows.Forms.Button btnreplaceall;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.CheckBox cbmatchcase;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDown;
        private System.Windows.Forms.RadioButton rbUp;
    }
}