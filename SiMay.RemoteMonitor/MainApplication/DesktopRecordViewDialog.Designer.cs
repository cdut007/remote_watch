namespace SiMay.RemoteMonitor.MainApplication
{
    partial class DesktopRecordViewDialog
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
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.desktopRecordViews = new SiMay.RemoteMonitor.UserControls.UListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.desktopRecordContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.开始录制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止录制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结束录制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.desktopRecordContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.linkLabel5);
            this.panel2.Controls.Add(this.linkLabel4);
            this.panel2.Controls.Add(this.linkLabel3);
            this.panel2.Controls.Add(this.desktopRecordViews);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(729, 355);
            this.panel2.TabIndex = 6;
            // 
            // linkLabel5
            // 
            this.linkLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(652, 330);
            this.linkLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(67, 15);
            this.linkLabel5.TabIndex = 5;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "录制设置";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(559, 330);
            this.linkLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(67, 15);
            this.linkLabel4.TabIndex = 4;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "取消选择";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(465, 330);
            this.linkLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(67, 15);
            this.linkLabel3.TabIndex = 3;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "全部选择";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // desktopRecordViews
            // 
            this.desktopRecordViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.desktopRecordViews.CheckBoxes = true;
            this.desktopRecordViews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.desktopRecordViews.ContextMenuStrip = this.desktopRecordContextMenu;
            this.desktopRecordViews.FullRowSelect = true;
            this.desktopRecordViews.HideSelection = false;
            this.desktopRecordViews.Location = new System.Drawing.Point(-1, -1);
            this.desktopRecordViews.Name = "desktopRecordViews";
            this.desktopRecordViews.Size = new System.Drawing.Size(729, 325);
            this.desktopRecordViews.TabIndex = 0;
            this.desktopRecordViews.UseCompatibleStateImageBehavior = false;
            this.desktopRecordViews.UseWindowsThemStyle = true;
            this.desktopRecordViews.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "备注";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "状态";
            this.columnHeader2.Width = 100;
            // 
            // desktopRecordContextMenu
            // 
            this.desktopRecordContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.desktopRecordContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始录制ToolStripMenuItem,
            this.停止录制ToolStripMenuItem,
            this.结束录制ToolStripMenuItem});
            this.desktopRecordContextMenu.Name = "desktopRecordContextMenu";
            this.desktopRecordContextMenu.Size = new System.Drawing.Size(139, 76);
            // 
            // 开始录制ToolStripMenuItem
            // 
            this.开始录制ToolStripMenuItem.Name = "开始录制ToolStripMenuItem";
            this.开始录制ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.开始录制ToolStripMenuItem.Text = "开始录制";
            this.开始录制ToolStripMenuItem.Click += new System.EventHandler(this.开始录制ToolStripMenuItem_Click);
            // 
            // 停止录制ToolStripMenuItem
            // 
            this.停止录制ToolStripMenuItem.Name = "停止录制ToolStripMenuItem";
            this.停止录制ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.停止录制ToolStripMenuItem.Text = "停止录制";
            this.停止录制ToolStripMenuItem.Click += new System.EventHandler(this.停止录制ToolStripMenuItem_Click);
            // 
            // 结束录制ToolStripMenuItem
            // 
            this.结束录制ToolStripMenuItem.Name = "结束录制ToolStripMenuItem";
            this.结束录制ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.结束录制ToolStripMenuItem.Text = "结束录制";
            this.结束录制ToolStripMenuItem.Click += new System.EventHandler(this.结束录制ToolStripMenuItem_Click);
            // 
            // DesktopRecordViewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 355);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DesktopRecordViewDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录制任务";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesktopRecordViewDialog_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.desktopRecordContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public UserControls.UListView desktopRecordViews;
        private System.Windows.Forms.ContextMenuStrip desktopRecordContextMenu;
        private System.Windows.Forms.ToolStripMenuItem 开始录制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止录制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 结束录制ToolStripMenuItem;
    }
}