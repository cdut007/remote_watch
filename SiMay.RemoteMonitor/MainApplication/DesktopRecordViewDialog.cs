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
    public partial class DesktopRecordViewDialog : Form
    {
        public DesktopRecordViewDialog()
        {
            InitializeComponent();
        }
        private IEnumerable<DesktopRecordViewItemApplication> GetSelectedDesktopRecordViewItem()
        {
            if (desktopRecordViews.SelectedItems.Count != 0)
            {
                var selectItem = desktopRecordViews.SelectedItems;
                for (int i = 0; i < selectItem.Count; i++)
                    desktopRecordViews.Items[selectItem[i].Index].Checked = true;

                foreach (DesktopRecordViewItemApplication item in desktopRecordViews.Items)
                {
                    if (item.Checked)
                    {
                        yield return item;
                        item.Checked = false;
                    }
                }
            }
        }

        private void 开始录制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in GetSelectedDesktopRecordViewItem())
                item.StartDesktopRecord();
        }

        private void 停止录制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in GetSelectedDesktopRecordViewItem())
                item.StopDesktopRecord();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecordDesktopOptionsDialog dlg = new RecordDesktopOptionsDialog();
            dlg.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DesktopRecordViewItemApplication item in desktopRecordViews.Items)
                item.Checked = true;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DesktopRecordViewItemApplication item in desktopRecordViews.Items)
                item.Checked = false;
        }

        private void DesktopRecordViewDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void 结束录制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in GetSelectedDesktopRecordViewItem())
                item.CloseDesktopRecord();
        }
    }
}
