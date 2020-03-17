using SiMay.RemoteControlsCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiMay.RemoteMonitor.MainApplication
{
    public partial class RecordDesktopOptionsDialog : Form
    {
        public RecordDesktopOptionsDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtInterval.Text, out var interval))
            {
                AppConfiguration.AutoSaveInterval = interval;
                AppConfiguration.RecordFileFormat = formatsBox.SelectedIndex;
                MessageBox.Show("设置成功!", "提示", 0, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("请输入正确的整数秒!", "提示", 0, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtRoot.Text = folderBrowserDialog.SelectedPath;
                AppConfiguration.RecordFileSaveRoot = folderBrowserDialog.SelectedPath;
            }
        }

        private void RecordDesktopOptionsDialog_Load(object sender, EventArgs e)
        {
            txtInterval.Text = AppConfiguration.AutoSaveInterval.ToString();
            txtRoot.Text = AppConfiguration.RecordFileSaveRoot;
            formatsBox.Text = AppConfiguration.RecordFileFormat == 0 ? "H264" : "MPEG4";
        }
    }
}
