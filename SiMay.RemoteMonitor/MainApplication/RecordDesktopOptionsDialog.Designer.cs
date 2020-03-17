namespace SiMay.RemoteMonitor.MainApplication
{
    partial class RecordDesktopOptionsDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.formatsBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "自动保存间隔:";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(155, 72);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(114, 25);
            this.txtInterval.TabIndex = 1;
            this.txtInterval.Text = "1800";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "秒";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(438, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 44);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(155, 110);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(342, 25);
            this.txtRoot.TabIndex = 5;
            this.txtRoot.Text = "1800";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "保存根目录:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(505, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 26);
            this.button2.TabIndex = 6;
            this.button2.Text = "浏览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "视频格式:";
            // 
            // formatsBox
            // 
            this.formatsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatsBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.formatsBox.FormattingEnabled = true;
            this.formatsBox.Items.AddRange(new object[] {
            "H264",
            "MPEG4"});
            this.formatsBox.Location = new System.Drawing.Point(155, 149);
            this.formatsBox.Name = "formatsBox";
            this.formatsBox.Size = new System.Drawing.Size(114, 23);
            this.formatsBox.TabIndex = 8;
            // 
            // RecordDesktopOptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 260);
            this.Controls.Add(this.formatsBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtRoot);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RecordDesktopOptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录制设置";
            this.Load += new System.EventHandler(this.RecordDesktopOptionsDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox formatsBox;
    }
}