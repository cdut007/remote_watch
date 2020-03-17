﻿using SiMay.RemoteMonitor.UserControls;

namespace SiMay.RemoteMonitor.Application
{
    partial class SystemApplication
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.processList = new SiMay.RemoteMonitor.UserControls.UListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.systemInfoList = new SiMay.RemoteMonitor.UserControls.UListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.立即刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新速度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.正常ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.低ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.暂停ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_proNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.cpuUse = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.moryUse = new System.Windows.Forms.ToolStripStatusLabel();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.sessionsListView = new SiMay.RemoteMonitor.UserControls.UListView();
            this.userName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sessionId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.state = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.windowsStationName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userProcessCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(616, 430);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.processList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(608, 404);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "进程管理";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(319, 370);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 24);
            this.button4.TabIndex = 14;
            this.button4.Text = "最小化";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(413, 370);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 24);
            this.button3.TabIndex = 13;
            this.button3.Text = "最大化";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(507, 370);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 24);
            this.button2.TabIndex = 12;
            this.button2.Text = "结束进程";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // processList
            // 
            this.processList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.processList.CheckBoxes = true;
            this.processList.FullRowSelect = true;
            this.processList.HideSelection = false;
            this.processList.Location = new System.Drawing.Point(13, 14);
            this.processList.Name = "processList";
            this.processList.Size = new System.Drawing.Size(582, 351);
            this.processList.TabIndex = 11;
            this.processList.UseCompatibleStateImageBehavior = false;
            this.processList.UseWindowsThemStyle = true;
            this.processList.View = System.Windows.Forms.View.Details;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.sessionsListView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(608, 404);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "会话管理";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tabPage2.Controls.Add(this.systemInfoList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(608, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "系统信息";
            // 
            // systemInfoList
            // 
            this.systemInfoList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.systemInfoList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.systemInfoList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.systemInfoList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.systemInfoList.FullRowSelect = true;
            this.systemInfoList.HideSelection = false;
            this.systemInfoList.Location = new System.Drawing.Point(6, 6);
            this.systemInfoList.Name = "systemInfoList";
            this.systemInfoList.Size = new System.Drawing.Size(596, 395);
            this.systemInfoList.TabIndex = 12;
            this.systemInfoList.UseCompatibleStateImageBehavior = false;
            this.systemInfoList.UseWindowsThemStyle = true;
            this.systemInfoList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "信息项";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "值";
            this.columnHeader2.Width = 300;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.刷新信息ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(629, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭窗口ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 关闭窗口ToolStripMenuItem
            // 
            this.关闭窗口ToolStripMenuItem.Name = "关闭窗口ToolStripMenuItem";
            this.关闭窗口ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭窗口ToolStripMenuItem.Text = "关闭窗口";
            this.关闭窗口ToolStripMenuItem.Click += new System.EventHandler(this.关闭窗口ToolStripMenuItem_Click);
            // 
            // 刷新信息ToolStripMenuItem
            // 
            this.刷新信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.立即刷新ToolStripMenuItem,
            this.更新速度ToolStripMenuItem});
            this.刷新信息ToolStripMenuItem.Name = "刷新信息ToolStripMenuItem";
            this.刷新信息ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.刷新信息ToolStripMenuItem.Text = "选项";
            // 
            // 立即刷新ToolStripMenuItem
            // 
            this.立即刷新ToolStripMenuItem.Name = "立即刷新ToolStripMenuItem";
            this.立即刷新ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.立即刷新ToolStripMenuItem.Text = "立即刷新";
            this.立即刷新ToolStripMenuItem.Click += new System.EventHandler(this.立即刷新ToolStripMenuItem_Click);
            // 
            // 更新速度ToolStripMenuItem
            // 
            this.更新速度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.高ToolStripMenuItem,
            this.正常ToolStripMenuItem,
            this.低ToolStripMenuItem,
            this.暂停ToolStripMenuItem});
            this.更新速度ToolStripMenuItem.Name = "更新速度ToolStripMenuItem";
            this.更新速度ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.更新速度ToolStripMenuItem.Text = "更新速度";
            // 
            // 高ToolStripMenuItem
            // 
            this.高ToolStripMenuItem.Name = "高ToolStripMenuItem";
            this.高ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.高ToolStripMenuItem.Text = "高";
            this.高ToolStripMenuItem.Click += new System.EventHandler(this.高ToolStripMenuItem_Click);
            // 
            // 正常ToolStripMenuItem
            // 
            this.正常ToolStripMenuItem.Checked = true;
            this.正常ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.正常ToolStripMenuItem.Name = "正常ToolStripMenuItem";
            this.正常ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.正常ToolStripMenuItem.Text = "正常";
            this.正常ToolStripMenuItem.Click += new System.EventHandler(this.正常ToolStripMenuItem_Click);
            // 
            // 低ToolStripMenuItem
            // 
            this.低ToolStripMenuItem.Name = "低ToolStripMenuItem";
            this.低ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.低ToolStripMenuItem.Text = "低";
            this.低ToolStripMenuItem.Click += new System.EventHandler(this.低ToolStripMenuItem_Click);
            // 
            // 暂停ToolStripMenuItem
            // 
            this.暂停ToolStripMenuItem.Name = "暂停ToolStripMenuItem";
            this.暂停ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.暂停ToolStripMenuItem.Text = "暂停";
            this.暂停ToolStripMenuItem.Click += new System.EventHandler(this.暂停ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.m_proNum,
            this.cpuUse,
            this.toolStripStatusLabel2,
            this.moryUse});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(629, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(47, 21);
            this.toolStripStatusLabel1.Text = "进程数:";
            // 
            // m_proNum
            // 
            this.m_proNum.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.m_proNum.Name = "m_proNum";
            this.m_proNum.Size = new System.Drawing.Size(19, 21);
            this.m_proNum.Text = "0";
            // 
            // cpuUse
            // 
            this.cpuUse.Name = "cpuUse";
            this.cpuUse.Size = new System.Drawing.Size(93, 21);
            this.cpuUse.Text = "CPU 使用率:0%";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(359, 21);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // moryUse
            // 
            this.moryUse.Name = "moryUse";
            this.moryUse.Size = new System.Drawing.Size(96, 21);
            this.moryUse.Text = "内存:1024/1024";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 1500;
            this.refreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // sessionsListView
            // 
            this.sessionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionsListView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sessionsListView.CheckBoxes = true;
            this.sessionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.userName,
            this.sessionId,
            this.state,
            this.windowsStationName,
            this.userProcessCreated});
            this.sessionsListView.FullRowSelect = true;
            this.sessionsListView.HideSelection = false;
            this.sessionsListView.Location = new System.Drawing.Point(13, 14);
            this.sessionsListView.Name = "sessionsListView";
            this.sessionsListView.Size = new System.Drawing.Size(582, 349);
            this.sessionsListView.TabIndex = 12;
            this.sessionsListView.UseCompatibleStateImageBehavior = false;
            this.sessionsListView.UseWindowsThemStyle = true;
            this.sessionsListView.View = System.Windows.Forms.View.Details;
            // 
            // userName
            // 
            this.userName.Text = "用户名";
            this.userName.Width = 100;
            // 
            // sessionId
            // 
            this.sessionId.Text = "会话标识";
            this.sessionId.Width = 100;
            // 
            // state
            // 
            this.state.Text = "会话状态";
            this.state.Width = 100;
            // 
            // windowsStationName
            // 
            this.windowsStationName.Text = "窗口站";
            this.windowsStationName.Width = 100;
            // 
            // userProcessCreated
            // 
            this.userProcessCreated.Text = "被控用户进程";
            this.userProcessCreated.Width = 100;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(507, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 24);
            this.button1.TabIndex = 13;
            this.button1.Text = "创建用户进程";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SystemApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 486);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SystemApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SystemManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SystemManagerFom_FormClosing);
            this.Load += new System.EventHandler(this.SystemManager_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private UListView processList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 刷新信息ToolStripMenuItem;
        private UListView systemInfoList;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel cpuUse;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel moryUse;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel m_proNum;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 立即刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更新速度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 正常ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 低ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 暂停ToolStripMenuItem;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TabPage tabPage3;
        private UListView sessionsListView;
        private System.Windows.Forms.ColumnHeader userName;
        private System.Windows.Forms.ColumnHeader sessionId;
        private System.Windows.Forms.ColumnHeader state;
        private System.Windows.Forms.ColumnHeader windowsStationName;
        private System.Windows.Forms.ColumnHeader userProcessCreated;
        private System.Windows.Forms.Button button1;
    }
}