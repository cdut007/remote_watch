﻿using SiMay.Basic;
using SiMay.Core;
using SiMay.Core.Enums;
using SiMay.Net.SessionProvider;
using SiMay.RemoteControlsCore;
using SiMay.RemoteMonitor.Application;
using SiMay.RemoteMonitor.Extensions;
using SiMay.RemoteMonitor.Properties;
using SiMay.RemoteMonitor.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SiMay.RemoteMonitor.MainApplication
{
    public partial class MainApplication : Form
    {
        public MainApplication()
        {

            InitializeComponent();
        }

        private bool _isRun = true;
        private int _connect_count = 0;
        private long _sendTransferredBytes = 0;
        private long _receiveTransferredBytes = 0;
        private const string GROUP_ALL = "全部";

        private System.Timers.Timer _flowCalcTimer;
        private System.Timers.Timer _viewCarouselTimer;
        private DesktopViewSettingContext _viewCarouselContext = new DesktopViewSettingContext();
        private Color _closeScreenColor = Color.FromArgb(127, 175, 219);
        private ImageList _imgList;

        private Dictionary<string, DesktopRecordViewItemApplication> _desktopRecordTasks = new Dictionary<string, DesktopRecordViewItemApplication>();
        private DesktopRecordViewDialog _desktopRecordViewDialog = new DesktopRecordViewDialog();
        private MainApplicationAdapterHandler _appMainAdapterHandler = new MainApplicationAdapterHandler();
        private void MainApplication_Load(object sender, EventArgs e)
        {
            this.ViewOnAdaptiveHandler();
            this.OnLoadConfiguration();
            this.RegisterMessageHandler();
        }

        /// <summary>
        /// 加载配置信息，及创建主控窗体
        /// </summary>
        private void OnLoadConfiguration()
        {
            this.Text = "JCMS远程监控管理系统-IOASJHD 正式版_" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (AppConfiguration.WindowMaximize)
                this.WindowState = FormWindowState.Maximized;

            this._imgList = new ImageList();
            this._imgList.Images.Add("ok", Resources.ok);
            this._imgList.Images.Add("error", Resources.erro);

            //计算实时上下传输流量
            this._flowCalcTimer = new System.Timers.Timer();
            this._flowCalcTimer.Interval = 1000;
            this._flowCalcTimer.Elapsed += (a, b) =>
            {
                if (!_isRun)
                {
                    _flowCalcTimer.Stop();
                    _flowCalcTimer.Dispose();
                    return;
                }

                this.BeginInvoke(new Action(() =>
                {
                    this.strdownflow.Text = (this._receiveTransferredBytes / (float)1024).ToString("0.00");
                    this._receiveTransferredBytes = 0;

                    this.struflow.Text = (this._sendTransferredBytes / (float)1024).ToString("0.00");
                    this._sendTransferredBytes = 0;
                }));

            };
            this._flowCalcTimer.Start();
            if (AppConfiguration.SessionMode == "1")
            {
                this.stripHost.Text = AppConfiguration.ServiceIPAddress;
                this.stripPort.Text = AppConfiguration.ServicePort.ToString();
            }
            else
            {
                this.stripHost.Text = AppConfiguration.IPAddress;
                this.stripPort.Text = AppConfiguration.Port.ToString();
            }

            this.rowtrackBar.Value = this._viewCarouselContext.ViewRow;
            this.columntrackBar.Value = this._viewCarouselContext.ViewColum;
            this.viewRow.Text = columntrackBar.Value.ToString();
            this.viewColumn.Text = rowtrackBar.Value.ToString();
            this.splitContainer.SplitterDistance = (splitContainer.Width / 4);

            this.logList.SmallImageList = _imgList;
            this.logList.Columns.Add("发生时间", 150);
            this.logList.Columns.Add("发生事件", 1000);

            this.groupBox.Text = GROUP_ALL;
            string[] columnsTitle = new string[]
            {
                "IP地址",
                "计算机名",
                "操作系统",
                "处理器信息",
                "核心数量",
                "运行内存",
                "系统账户",
                "摄像设备",
                "录音设备",
                "播放设备",
                "备注信息",
                "服务版本",
                "启动时间",
                "分组名称"
            };

            for (int i = 0; i < columnsTitle.Length; i++)
                this.servicesOnlineList.Columns.Insert(i, columnsTitle[i], 150);

            var apps = SysUtil.ApplicationTypes.OrderByDescending(x => x.Rank).ToList();
            apps.ForEach(c =>
            {
                var type = c.Type;
                if (type.IsUnconventionalApp())
                    return;

                var stripMenu = new UToolStripMenuItem(type.GetApplicationName(), c.Type);
                stripMenu.Click += StripMenu_Click;
                this.cmdContext.Items.Insert(0, stripMenu);

                if (type.OnTools())
                {
                    var stripButton = new UToolStripButton(type.GetApplicationName(), SysUtilExtend.GetResourceImageByName(type.GetIconResourceName()), type);
                    stripButton.Click += StripButton_Click;
                    this.toolStrip1.Items.Insert(3, stripButton);
                }
            });

            if (AppConfiguration.WindowsIsLock) //锁住主控界面
                LockWindow();

            _viewCarouselTimer = new System.Timers.Timer(_viewCarouselContext.ViewCarouselInterval);
            _viewCarouselTimer.Elapsed += ViewCarouselFunc;

            if (_viewCarouselContext.CarouselEnabled)
                _viewCarouselTimer.Start();

        }
        private void desktopViewLayout_Resize(object sender, EventArgs e)
        {
            this.ViewOnAdaptiveHandler();
        }
        private void desktopViewLayout_Scroll(object sender, ScrollEventArgs e)
        {
            //this.ViewOnAdaptiveHandler();
        }
        private void ViewOnAdaptiveHandler()
        {
            var viewCount = _viewCarouselContext.ViewColum * _viewCarouselContext.ViewRow;
            var containerRectangle = this.desktopViewLayout.DisplayRectangle;
            var marginalRight = (_viewCarouselContext.ViewColum * 9) / this._viewCarouselContext.ViewColum;
            var width = (this.desktopViewLayout.Width / _viewCarouselContext.ViewColum) - marginalRight;
            var height = (this.desktopViewLayout.Height / _viewCarouselContext.ViewRow) - marginalRight;

            this._viewCarouselContext.ViewWidth = width;
            this._viewCarouselContext.ViewHeight = height;
            foreach (var view in desktopViewLayout.Controls.Cast<IDesktopView>())
            {
                if (view.Width == width && view.Height == height)
                    continue;

                this.InvokeUI(() =>
                {
                    view.Height = height;
                    view.Width = width;
                });
            }

            //if (this.desktopViewLayout.Controls.Count <= 0)
            //    return;

            //var displayTop = Math.Abs(containerRectangle.Y);//容器的可视top(y偏差纠正)
            //var displayBottom = Math.Abs(displayTop + desktopViewLayout.Height);//容器的可视bottom

            //var startControlIndex = (displayTop / height) * this._viewCarouselContext.ViewColum;
            //var endControlIndex = (displayBottom / height) * this._viewCarouselContext.ViewColum;

            //Console.WriteLine(containerRectangle.Top + "|" + containerRectangle.Bottom);
            //Console.WriteLine($"{startControlIndex} | {endControlIndex}");

            //int index = 0;
            //foreach (var view in desktopViewLayout.Controls.Cast<IDesktopView>())
            //{
            //    if (view is Control control)
            //    {
            //        var viewX = Math.Abs(control.Location.X);
            //        var viewY = Math.Abs(control.Location.Y);
            //        var controlRectangle = new Rectangle(viewX, viewY, control.Width, control.Height);
            //        view.InVisbleArea = RectangleHelper.WhetherContainsInDisplayRectangle(new Rectangle(0, displayTop, this.desktopViewLayout.Width, this.desktopViewLayout.Height), controlRectangle);
            //        Console.WriteLine($"{startControlIndex} | {endControlIndex} - " + view.InVisbleArea + " " + index);
            //    }
            //    if (view.Width == width && view.Height == height && index++ == viewCount)
            //        continue;

            //    this.InvokeUI(() =>
            //    {
            //        view.Height = height;
            //        view.Width = width;
            //    });
            //}
        }

        private void ViewCarouselFunc(object sender, System.Timers.ElapsedEventArgs e)
        {
            var viewCount = _viewCarouselContext.ViewColum * _viewCarouselContext.ViewRow;
            if (this.desktopViewLayout.Controls.Count > viewCount)
            {
                this.InvokeUI(() =>
                {
                    var view = this.desktopViewLayout.Controls[this.desktopViewLayout.Controls.Count - 1].ConvertTo<IDesktopView>();
                    if (!_viewCarouselContext.AlwaysViews.Contains(view))
                        this.desktopViewLayout.Controls.SetChildIndex(view as Control, _viewCarouselContext.AlwaysViews.Count);
                });
            }

            this.ViewOnAdaptiveHandler();
        }

        private void InvokeUI(Action action) => this.Invoke(new Action(action));

        /// <summary>
        /// 初始化通信库
        /// </summary>
        private void RegisterMessageHandler()
        {
            this._appMainAdapterHandler.ViewRefreshInterval = _viewCarouselContext.ViewFreshInterval;
            this._appMainAdapterHandler.SynchronizationContext = SynchronizationContext.Current;
            this._appMainAdapterHandler.OnProxyNotifyHandlerEvent += OnProxyNotify;
            this._appMainAdapterHandler.OnReceiveHandlerEvent += OnReceiveHandlerEvent;
            this._appMainAdapterHandler.OnTransmitHandlerEvent += OnTransmitHandlerEvent;
            this._appMainAdapterHandler.OnLogOutHandlerEvent += OnLogOutHandlerEvent;
            this._appMainAdapterHandler.OnCreateDesktopViewHandlerEvent += OnCreateDesktopViewHandlerEvent;
            this._appMainAdapterHandler.OnLoginHandlerEvent += OnLoginHandlerEvent;
            this._appMainAdapterHandler.OnApplicationCreatedEventHandler += OnApplicationCreatedEventHandler;
            this._appMainAdapterHandler.OnLoginUpdateHandlerEvent += OnLoginUpdateHandlerEvent;
            this._appMainAdapterHandler.OnLogHandlerEvent += OnLogHandlerEvent;
            this._appMainAdapterHandler.StartApp();
        }

        private bool OnApplicationCreatedEventHandler(IApplication app)
        {
            if (app is DesktopRecordViewItemApplication desktopRecordViewItemApplication)
            {
                var originName = desktopRecordViewItemApplication.RemoteScreenAdapterHandler.OriginName;
                if (_desktopRecordTasks.ContainsKey(originName))
                    return false;
                else _desktopRecordTasks.Add(originName, desktopRecordViewItemApplication);

                app.SetParameter(new object[] { _desktopRecordViewDialog.desktopRecordViews, _desktopRecordTasks });
            }
            return true;
        }

        private void OnLoginUpdateHandlerEvent(SessionSyncContext syncContext)
        {
            var listItem = syncContext.KeyDictions[SysConstantsExtend.SessionListItem].ConvertTo<USessionListItem>(); ;
            listItem.UpdateListItemText();

            if (!syncContext.KeyDictions[SysConstants.OpenScreenWall].ConvertTo<bool>())
                listItem.BackColor = _closeScreenColor;
        }

        private void OnLogHandlerEvent(string log, LogSeverityLevel level)
        {
            switch (level)
            {
                case LogSeverityLevel.Information:
                    this.WriteRuninglog(log, "ok");
                    break;
                case LogSeverityLevel.Warning:
                    this.WriteRuninglog(log, "error");
                    break;
                case LogSeverityLevel.Error:
                    break;
                default:
                    break;
            }
        }

        private void OnLoginHandlerEvent(SessionSyncContext syncContext)
        {
            var listItem = new USessionListItem(syncContext);
            syncContext.KeyDictions.Add(SysConstantsExtend.SessionListItem, listItem);

            //是否开启桌面视图
            if (!syncContext.KeyDictions[SysConstants.OpenScreenWall].ConvertTo<bool>())
                listItem.BackColor = _closeScreenColor;

            var groupName = syncContext.KeyDictions[SysConstants.GroupName].ConvertTo<string>();
            if (!groupBox.Items.Contains(groupName))
                this.groupBox.Items.Add(groupName);

            //分组
            if (groupBox.Text == groupName || groupBox.Text == GROUP_ALL)
                this.servicesOnlineList.Items.Add(listItem);

            this._connect_count++;
            this.stripConnectedNum.Text = _connect_count.ToString();

            Win32Api.FlashWindow(this.Handle, true); //上线任务栏图标闪烁
        }

        private void OnLogOutHandlerEvent(SessionSyncContext syncContext)
        {
            if (syncContext.KeyDictions.ContainsKey(SysConstants.DesktopView))                    //如果屏幕墙已开启,移除桌面墙
                this.DisposeDesktopView(syncContext.KeyDictions[SysConstants.DesktopView].ConvertTo<UDesktopView>());

            syncContext.KeyDictions[SysConstantsExtend.SessionListItem].ConvertTo<USessionListItem>().Remove();

            _connect_count--;
            stripConnectedNum.Text = _connect_count.ToString();
        }

        private IDesktopView OnCreateDesktopViewHandlerEvent(SessionSyncContext syncContext)
        {
            var view = new UDesktopView(syncContext)
            {
                Height = this._viewCarouselContext.ViewHeight,
                Width = this._viewCarouselContext.ViewWidth
            };
            view.OnDoubleClickEvent += DesktopViewDbClick;
            this.desktopViewLayout.Controls.Add(view);

            return view;
        }

        private void OnTransmitHandlerEvent(SessionProviderContext session)
            => this._sendTransferredBytes += session.SendTransferredBytes;

        private void OnReceiveHandlerEvent(SessionProviderContext session)
            => this._receiveTransferredBytes += session.ReceiveTransferredBytes;


        /// <summary>
        /// 代理协议事件
        /// </summary>
        /// <param name="notify"></param>
        private void OnProxyNotify(ProxyProviderNotify notify, EventArgs arg)
        {
            switch (notify)
            {
                case ProxyProviderNotify.AccessIdOrKeyWrong:
                    this.InvokeUI(() => this.WriteRuninglog("AccessKey错误,与会话服务器的连接自动关闭!", "error"));
                    break;
                case ProxyProviderNotify.LogOut:
                    if (MessageBox.Show($"{arg.ConvertTo<LogOutEventArgs>().Message},本次连接已自动关闭,是否重新连接?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.OK)
                    {
                        this._appMainAdapterHandler.StartService();
                    }
                    break;
            }
        }


        /// <summary>
        /// 从主控端移除桌面墙
        /// </summary>
        /// <param name="view"></param>
        private void DisposeDesktopView(UDesktopView view)
        {
            this.desktopViewLayout.Controls.Remove(view);
            view.OnDoubleClickEvent -= DesktopViewDbClick;
            view.Dispose();
        }

        /// <summary>
        /// 向已选择的桌面墙发送命令
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isBock"></param>
        /// <returns></returns>
        private IEnumerable<UDesktopView> GetSelectedDesktopView()
        {
            foreach (UDesktopView view in desktopViewLayout.Controls)
            {
                if (view.Checked)
                {
                    yield return view;
                    view.Checked = false;
                }
            }
        }

        /// <summary>
        /// 向选择了的列表发送命令
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerable<USessionListItem> GetSelectedListItem()
        {
            if (servicesOnlineList.SelectedItems.Count != 0)
            {
                var SelectItem = servicesOnlineList.SelectedItems;
                for (int i = 0; i < SelectItem.Count; i++)
                    servicesOnlineList.Items[SelectItem[i].Index].Checked = true;

                foreach (USessionListItem item in servicesOnlineList.Items)
                {
                    if (item.Checked)
                    {
                        yield return item;
                        item.Checked = false;
                    }
                }
            }
        }

        private void LockWindow()
        {
            this.Hide();
            AppConfiguration.WindowsIsLock = true;
            LockWindowsForm form = new LockWindowsForm();
            form.ShowDialog();
            this.Show();
        }

        private void StripButton_Click(object sender, EventArgs e)
        {
            var ustripbtn = sender as UToolStripButton;
            string appkey = ustripbtn.CtrlType.GetApplicationKey();
            this.GetSelectedDesktopView().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteActiveService(c.SessionSyncContext, appkey);
            });
        }

        private void StripMenu_Click(object sender, EventArgs e)
        {
            var ustripbtn = sender as UToolStripMenuItem;
            string appkey = ustripbtn.ApplicationType.GetApplicationKey();
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteActiveService(c.SessionSyncContext, appkey);
            });
        }

        /// <summary>
        /// 双击屏幕墙执行一些任务
        /// </summary>
        /// <param name="session"></param>
        private void DesktopViewDbClick(SessionSyncContext syncContext)
        {
            this._appMainAdapterHandler.RemoteActiveService(syncContext, AppConfiguration.DbClickViewExc);
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="key"></param>
        private void WriteRuninglog(string log, string key = "ok")
        {
            ListViewItem logItem = new ListViewItem();
            logItem.ImageKey = key;
            logItem.Text = DateTime.Now.ToString();
            logItem.SubItems.Add(log);

            LogHelper.WriteLog(log, "OnRun.log");

            if (logList.Items.Count >= 1)
                logList.Items.Insert(1, logItem);
            else
                logList.Items.Insert(0, logItem);
        }

        /// <summary>
        /// 清除日志
        /// </summary>
        private void Clearlogs()
        {
            int i = 0;
            foreach (ListViewItem item in logList.Items)
            {
                i++;
                if (i > 1)
                    item.Remove();
            }
        }

        private void SystemOption(object sender, EventArgs e)
        {
            AppSettingForm configForm = new AppSettingForm();
            configForm.ShowDialog();
        }

        private void CmdContext_Opening(object sender, CancelEventArgs e)
        {
            if (servicesOnlineList.SelectedItems.Count == 0)
                cmdContext.Enabled = false;
            else
                cmdContext.Enabled = true;
        }

        private void CreateService(object sender, EventArgs e)
        {
            BuilderServiceForm serviceBuilder = new BuilderServiceForm();
            serviceBuilder.ShowDialog();
        }

        private void RemoteShutdown(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定关闭远程计算机吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.Shutdown);
            });
        }

        private void RemoteReboot(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重启远程计算机吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.Reboot);
            });
        }

        private void RemoteStartup(object sender, EventArgs e)
        {
            if (MessageBox.Show("该操作可能导致远程计算机安全软件警示，继续操作吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.RegStart);
            });
        }

        private void RemoteUnStarup(object sender, EventArgs e)
        {
            if (MessageBox.Show("该操作可能导致远程计算机安全软件警示，继续操作吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;

            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.RegCancelStart);
            });
        }

        private void RemoteHideServiceFile(object sender, EventArgs e)
        {
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.AttributeHide);
            });
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.AttributeShow);
            });
        }

        private void UninstallService(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定解除对该远程计算机的控制吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.Unstall);
            });
        }

        private void ModifyRemark(object sender, EventArgs e)
        {
            EnterForm f = new EnterForm();
            f.Caption = "请输入备注名称";
            DialogResult result = f.ShowDialog();
            if (f.Value != "" && result == DialogResult.OK)
            {
                this.GetSelectedListItem().ForEach(c =>
                {
                    this._appMainAdapterHandler.RemoteSetRemarkInformation(c.SessionSyncContext, f.Value);
                });
            }
        }

        private void CopyRuningLog(object sender, EventArgs e)
        {
            if (logList.SelectedItems.Count != 0)
            {
                Clipboard.SetText(logList.Items[logList.SelectedItems[0].Index].SubItems[1].Text);
            }
        }

        private void DeleteRuningLog(object sender, EventArgs e)
        {
            if (logList.SelectedItems.Count != 0)
            {
                int Index = logList.SelectedItems[0].Index;
                if (Index >= 1)
                    logList.Items[Index].Remove();
            }
        }

        private void OnlineList_OnSelected(object sender, EventArgs e)
        {
            foreach (ListViewItem item in servicesOnlineList.Items)
                item.Checked = true;
        }

        private void OnileList_OnUnSelected(object sender, EventArgs e)
        {
            foreach (ListViewItem item in servicesOnlineList.Items)
                item.Checked = false;
        }

        private void ClearRuningLog(object sender, EventArgs e)
        {
            this.Clearlogs();
        }

        private void SendMessageBox(object sender, EventArgs e)
        {
            NotifyMessageBoxForm dlg = new NotifyMessageBoxForm();
            DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.GetSelectedListItem().ForEach(c =>
                {
                    this._appMainAdapterHandler.RemoteMessageBox(c.SessionSyncContext, dlg.MessageBody, dlg.MessageTitle, dlg.MsgBoxIcon);
                });
            }
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusToolStripMenuItem.Checked == true)
            {
                statusToolStripMenuItem.Checked = false;
                statusStrip1.Visible = false;
            }
            else
            {
                statusToolStripMenuItem.Checked = true;
                statusStrip1.Visible = true;
            }
        }

        private void RemoteDownloadExecete(object sender, EventArgs e)
        {
            EnterForm input = new EnterForm();
            input.Caption = "可执行文件的下载地址!";
            DialogResult result = input.ShowDialog();
            if (input.Value != "" && result == DialogResult.OK)
            {
                if (input.Value.IndexOf("http://") == -1 && input.Value.IndexOf("https://") == -1)
                {
                    MessageBoxHelper.ShowBoxExclamation("输入的网址不合法");
                    return;
                }
                this.GetSelectedListItem().ForEach(c =>
                {
                    this._appMainAdapterHandler.RemoteHttpDownloadExecute(c.SessionSyncContext, input.Value);
                });
            }
        }

        private void About(object sender, EventArgs e)
        {
            AboutForm dlg = new AboutForm();
            dlg.ShowDialog();
        }

        private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.onlineToolStripMenuItem.Checked == true)
            {
                this.splitContainer1.Panel2Collapsed = true;
                this.onlineToolStripMenuItem.Checked = false;
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = false;
                this.onlineToolStripMenuItem.Checked = true;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteOpenDesktopView(c.SessionSyncContext);
                c.BackColor = Color.Transparent;
            });
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.GetSelectedListItem().ForEach(c =>
            {
                var syncContext = c.SessionSyncContext;
                this._appMainAdapterHandler.RemoteCloseDesktopView(c.SessionSyncContext);
                if (syncContext.KeyDictions.ContainsKey(SysConstants.DesktopView))
                {
                    var view = syncContext.KeyDictions[SysConstants.DesktopView].ConvertTo<UDesktopView>();
                    syncContext.KeyDictions.Remove(SysConstants.DesktopView);
                    this.DisposeDesktopView(view);
                }
                c.BackColor = _closeScreenColor;
            });
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this._viewCarouselContext.ViewRow = rowtrackBar.Value;
            this._viewCarouselContext.ViewColum = columntrackBar.Value;

            this.ViewOnAdaptiveHandler();

            this.WriteRuninglog("设置已保存!", "ok");
        }

        private void RowtrackBar_Scroll(object sender, EventArgs e)
        {
            this.viewRow.Text = columntrackBar.Value.ToString();
        }

        private void ColumntrackBar_Scroll(object sender, EventArgs e)
        {
            this.viewColumn.Text = rowtrackBar.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (UDesktopView item in desktopViewLayout.Controls)
                item.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (UDesktopView item in desktopViewLayout.Controls)
                item.Checked = false;
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            NotifyMessageBoxForm dlg = new NotifyMessageBoxForm();
            DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.GetSelectedDesktopView().ForEach(c =>
                {
                    this._appMainAdapterHandler.RemoteMessageBox(c.SessionSyncContext, dlg.MessageBody, dlg.MessageTitle, dlg.MsgBoxIcon);
                });
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            EnterForm input = new EnterForm();
            input.Caption = "可执行文件的下载地址";
            DialogResult result = input.ShowDialog();
            if (input.Value != "" && result == DialogResult.OK)
            {
                if (input.Value.IndexOf("http://") == -1 && input.Value.IndexOf("https://") == -1)
                {
                    MessageBoxHelper.ShowBoxExclamation("输入的网址不合法");
                    return;
                }
                this.GetSelectedDesktopView().ForEach(c =>
                {
                    this._appMainAdapterHandler.RemoteHttpDownloadExecute(c.SessionSyncContext, input.Value);
                });
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定解除对该远程计算机的控制吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;
            this.GetSelectedDesktopView().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.Unstall);
            });
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            AppSettingForm appConfigForm = new AppSettingForm();
            appConfigForm.ShowDialog();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            BuilderServiceForm serviceBuilder = new BuilderServiceForm();
            serviceBuilder.ShowDialog();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ToolStripMenuItem.Checked == true)
            {
                this.ToolStripMenuItem.Checked = false;
                this.toolStrip1.Visible = false;
            }
            else
            {
                this.ToolStripMenuItem.Checked = true;
                this.toolStrip1.Visible = true;
            }
        }

        private void toolStripMenuItem6_Click_1(object sender, EventArgs e)
        {
            EnterForm input = new EnterForm();
            input.Caption = "请输入要打开的网页地址!";
            DialogResult result = input.ShowDialog();
            if (input.Value != "" && result == DialogResult.OK)
            {
                this.GetSelectedListItem().ForEach(c =>
                {
                    this._appMainAdapterHandler.RemoteOpenUrl(c.SessionSyncContext, input.Value);
                });
            }
        }

        private void MainApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出系统吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                this._isRun = false;
                this._appMainAdapterHandler.Dispose();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            foreach (UDesktopView view in desktopViewLayout.Controls)
            {
                if (view.Checked)
                {
                    this._appMainAdapterHandler.RemoteCloseDesktopView(view.SessionSyncContext);
                    this.DisposeDesktopView(view);

                    view.SessionSyncContext.KeyDictions.Remove(SysConstants.DesktopView);
                    view.SessionSyncContext.KeyDictions[SysConstantsExtend.SessionListItem].ConvertTo<USessionListItem>().BackColor = _closeScreenColor;
                    view.Checked = false;
                }
            }
        }
        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LockWindow();
        }
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            foreach (var item in this.GetSelectedListItem())
            {
                var dlg = new DesktopRecordForm(item.SessionSyncContext);
                dlg.Show();
            }
        }

        private void viewReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var recordViewer = new DesktopRecordViewerForm();
            recordViewer.Show();
        }

        private void logList_MouseEnter(object sender, EventArgs e)
        {
            this.splitContainer.SplitterDistance = splitContainer.Width - (splitContainer.Width / 4);
        }

        private void onlineList_MouseEnter(object sender, EventArgs e)
        {
            this.splitContainer.SplitterDistance = (splitContainer.Width / 4);
        }

        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            EnterForm input = new EnterForm();
            input.Caption = "请输入分组名称";
            DialogResult result = input.ShowDialog();
            if (input.Value != "" && result == DialogResult.OK)
            {
                this.GetSelectedListItem().ForEach(c =>
                {
                    this._appMainAdapterHandler.RemoteSetGroupName(c.SessionSyncContext, input.Value);
                });
            }
        }

        private void GroupBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //界面初始化完成会被触发一次。。

            foreach (var item in this._appMainAdapterHandler.SessionSyncContexts)
                item.KeyDictions[SysConstantsExtend.SessionListItem].ConvertTo<USessionListItem>().Remove();

            foreach (var item in this._appMainAdapterHandler.SessionSyncContexts)
            {
                if (item.KeyDictions[SysConstants.GroupName].ConvertTo<string>() == groupBox.Text || groupBox.Text == GROUP_ALL)
                    this.servicesOnlineList.Items.Add(item.KeyDictions[SysConstantsExtend.SessionListItem].ConvertTo<USessionListItem>());
            }
        }

        private void UpdateClient_Click(object sender, EventArgs e)
        {
            using (var dlg = new RemoteUpdateServiceForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.GetSelectedListItem().ForEach(c =>
                    {
                        this._appMainAdapterHandler.RemoteServiceUpdate(c.SessionSyncContext, dlg.UrlOrFileUpdate, File.ReadAllBytes(dlg.Value), dlg.Value);
                    });
                }
            }
        }

        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重新载入被控端吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                this.GetSelectedListItem().ForEach(c =>
                {
                    this._appMainAdapterHandler.RemoteServiceReload(c.SessionSyncContext);
                });
            }
        }

        private void installServiceMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定以系统服务方式启动吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.InstallService);
            });
        }

        private void unInstallServiceMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定卸载系统服务启动吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;
            this.GetSelectedListItem().ForEach(c =>
            {
                this._appMainAdapterHandler.RemoteSetSessionState(c.SessionSyncContext, SystemSessionType.UnInstallService);
            });
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dialog = new DesktopViewWallSettingForm(_viewCarouselContext);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _appMainAdapterHandler.ViewRefreshInterval = _viewCarouselContext.ViewFreshInterval;

                if (!_viewCarouselContext.CarouselEnabled)
                    this._viewCarouselTimer.Stop();
                else
                {
                    this._viewCarouselTimer.Interval = _viewCarouselContext.ViewCarouselInterval;
                    this._viewCarouselTimer.Start();
                }
                this.ViewOnAdaptiveHandler();
            }
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _desktopRecordViewDialog.Show();
        }
    }
}