using SiMay.Core;
using SiMay.RemoteControlsCore;
using SiMay.RemoteControlsCore.HandlerAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SiMay.Basic;
using SiMay.RemoteMonitor.UserControls;
using SiMay.Core.Packets;
using SiMay.RemoteControlsCore.Enum;
using System.Drawing;
using System.IO;
using SiMay.Core.ScreenSpy.Entitys;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
using Accord.Video.FFMPEG;

namespace SiMay.RemoteMonitor
{
    [UnconventionalApp]
    [Application(typeof(RemoteScreenAdapterHandler), AppJobConstant.REMOTE_DESKTOP_RECORD, 0)]
    public class DesktopRecordViewItemApplication : ListViewItem, IApplication
    {
        [ApplicationAdapterHandler]
        public RemoteScreenAdapterHandler RemoteScreenAdapterHandler { get; set; }


        private int _screentHeight = 0;
        private int _screenWidth = 0;
        private bool _allowStart = true;
        private bool _stop;
        private DateTime _lastStartTime;
        private Bitmap _currentFrame;
        private Graphics _currentFrameGraphics;
        private VideoFileWriter videoWriter;
        private UListView _desktopRecordListViews;
        private ListViewSubItem _statusViewItem = new ListViewSubItem();
        private IDictionary<string, DesktopRecordViewItemApplication> _desktopRecordTasks;
        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        private DateTime _lastFrameHandlerTime;
        public void ContinueTask(ApplicationAdapterHandler handler)
        {
            _stop = false;
            RemoteScreenAdapterHandler.GetInitializeBitInfo();//获取远程桌面初始化信息
        }

        public void SessionClose(ApplicationAdapterHandler handler)
        {
            _stop = true;
            _statusViewItem.Text = "重连中...";
        }

        public void SetParameter(object arg)
        {
            var args = arg.ConvertTo<object[]>();
            _desktopRecordListViews = args[0].ConvertTo<UListView>();
            _desktopRecordTasks = args[1].ConvertTo<Dictionary<string, DesktopRecordViewItemApplication>>();
        }

        public void Start()
        {
            RemoteScreenAdapterHandler.OnServcieInitEventHandler += OnServcieInitEventHandler;
            RemoteScreenAdapterHandler.OnScreenFragmentEventHandler += OnScreenFragmentEventHandler;

            this._lastStartTime = DateTime.Now;
            this.Text = RemoteScreenAdapterHandler.OriginName;
            this.SubItems.Add(_statusViewItem);
            this._desktopRecordListViews.Items.Add(this);

            RemoteScreenAdapterHandler.GetInitializeBitInfo();//获取初始化信息

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000;
            _timer.Tick += (s, e) =>
            {
                if ((int)(DateTime.Now - _lastFrameHandlerTime).TotalSeconds > 5 && !_stop) {
                    ResetScreen(); StartNextScreen(); LogHelper.DebugWriteLog("TimeOut!");
                }
            };
            _timer.Start();
        }

        /// <summary>
        /// 视频录制线程
        /// </summary>
        private void CreateDesktopRecordThread()
        {
            var targetDirectory = Path.Combine(AppConfiguration.RecordFileSaveRoot, RemoteScreenAdapterHandler.GroupName, RemoteScreenAdapterHandler.OriginName);
            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);
            var fileName = Path.Combine(targetDirectory, $"桌面录制_{RemoteScreenAdapterHandler.OriginName}_{DateTime.Now.ToString("yyyy-MM-dd hhmmss")}.MP4");

            videoWriter = new VideoFileWriter();
            videoWriter.Open(fileName, _screenWidth, _screentHeight, AppConfiguration.RecordRate, (AppConfiguration.RecordFileFormat == 0 ? VideoCodec.H264 : VideoCodec.MPEG4));//录制为MP4,录制MP4对CPU、内存占用较小;

            var autoIntervalExit = false;
            Bitmap endFrame = null;
            _lastStartTime = DateTime.Now;
            do
            {
                if (!_currentFrame.IsNull())
                {
                    lock (this)
                        videoWriter.WriteVideoFrame(_currentFrame);
                    GC.Collect();
                }
                //当线程运行至
                if ((int)(DateTime.Now - _lastStartTime).TotalSeconds > AppConfiguration.AutoSaveInterval)
                {
                    lock (this)
                        endFrame = _currentFrame.Clone().ConvertTo<Bitmap>();
                    autoIntervalExit = true;
                    break;
                }

                Thread.Sleep(100); //帧率控制,每秒10帧，防止写入过快
            } while (!_stop);
            videoWriter.Close();

            if (autoIntervalExit)//自动保存退出
            {
                lock (this)
                {
                    _currentFrameGraphics.Dispose();
                    _currentFrame = endFrame;
                    _currentFrameGraphics = Graphics.FromImage(_currentFrame);
                }
                Task.Run(CreateDesktopRecordThread);//重新创建录制线程
            }
        }

        /// <summary>
        /// 远程桌面初始化回调事件
        /// </summary>
        /// <param name="adapterHandler"></param>
        /// <param name="height">图像高</param>
        /// <param name="width">图像宽</param>
        /// <param name="currentMonitorIndex">当前屏幕索引</param>
        /// <param name="monitorItems">所有监视器</param>
        private void OnServcieInitEventHandler(RemoteScreenAdapterHandler adapterHandler, int height, int width, int currentMonitorIndex, MonitorItem[] monitorItems)
        {
            //本地是否记录不对此被控服务录制
            this._allowStart = !AppConfiguration.NotAllowDesktopRecord.Split(',').Any(c => c.Equals(RemoteScreenAdapterHandler.OriginName, StringComparison.OrdinalIgnoreCase));
            if (this._allowStart)
            {
                try
                {
                    if (!_currentFrame.IsNull())
                        _currentFrame.Dispose();
                    if (!_currentFrameGraphics.IsNull())
                        _currentFrameGraphics.Dispose();
                }
                catch { }

                _statusViewItem.Text = "正在录制";
                _screentHeight = height; _screenWidth = width;
                _currentFrame = new Bitmap(width, height);
                _currentFrameGraphics = Graphics.FromImage(_currentFrame);

                this.StartNextScreen();

                Task.Run(CreateDesktopRecordThread);//重新创建录制线程
            }
            else _statusViewItem.Text = "已停止";
        }

        private void OnScreenFragmentEventHandler(RemoteScreenAdapterHandler adapterHandler, Fragment[] fragments, ScreenReceivedType type)
        {
            _lastFrameHandlerTime = DateTime.Now;
            switch (type)
            {
                case ScreenReceivedType.Noninterlaced:
                    this.FrameDataHandler(fragments);
                    break;
                case ScreenReceivedType.Difference:
                    break;
                case ScreenReceivedType.DifferenceEnd:
                    break;
                default:
                    break;
            }
        }

        private async void FrameDataHandler(Fragment[] fragments)
            => await Task.Run(() =>
            {
                lock (this)
                {
                    foreach (var fragment in fragments)
                    {
                        using (MemoryStream ms = new MemoryStream(fragment.FragmentData))
                        {
                            var bit = Image.FromStream(ms);
                            _currentFrameGraphics.DrawImage(bit, new Rectangle(fragment.X, fragment.Y, fragment.Width, fragment.Height));
                            bit.Dispose();
                        }
                    }
                }
                this.GetNextScreen();
            });


        private void StartNextScreen()
        {
            if (!this._allowStart)
                return;
            this.RemoteScreenAdapterHandler.StartGetScreen(_screentHeight, _screenWidth, 0, 0, ScreenDisplayMode.Original);
        }

        private void ResetScreen()
        {
            if (!this._allowStart)
                return;
            this.RemoteScreenAdapterHandler.ResetPFrame();
        }

        private void GetNextScreen()
        {
            if (!this._allowStart)
                return;
            this.RemoteScreenAdapterHandler.GetNextScreen(_screentHeight, _screenWidth, 0, 0, ScreenDisplayMode.Original);
        }

        public void CloseDesktopRecord()
        {
            if (this._allowStart)
            {
                this._allowStart = false;
                this._stop = true;
                var originName = RemoteScreenAdapterHandler.OriginName;
                var notAllowList = new List<string>(AppConfiguration.NotAllowDesktopRecord.Split(',').Where(c => !c.IsNullOrEmpty()));
                notAllowList.Add(originName);
                AppConfiguration.NotAllowDesktopRecord = string.Join(",", notAllowList);

                _timer.Stop();
                _timer.Dispose();

                //移除任务列表
                _desktopRecordTasks.Remove(this.RemoteScreenAdapterHandler.OriginName);

                //移出列表
                _desktopRecordListViews.Items.Remove(this);

                //主动关闭会话
                RemoteScreenAdapterHandler.CloseSession();
            }
        }

        public void StopDesktopRecord()
        {
            if (this._allowStart)
            {
                this._allowStart = false;
                this._stop = true;
                this._statusViewItem.Text = "已停止";
                var originName = RemoteScreenAdapterHandler.OriginName;
                var notAllowList = new List<string>(AppConfiguration.NotAllowDesktopRecord.Split(',').Where(c => !c.IsNullOrEmpty()));
                notAllowList.Add(originName);
                AppConfiguration.NotAllowDesktopRecord = string.Join(",", notAllowList);
            }
        }

        public void StartDesktopRecord()
        {
            if (!this._allowStart)
            {
                this._stop = false;
                this._allowStart = true;
                this._statusViewItem.Text = "正在录制";
                var originName = RemoteScreenAdapterHandler.OriginName;
                AppConfiguration.NotAllowDesktopRecord = string.Join(",", AppConfiguration.NotAllowDesktopRecord.Split(',').Where(c => !c.Equals(originName)));
                RemoteScreenAdapterHandler.GetInitializeBitInfo();//获取远程桌面初始化信息
            }
        }
    }
}
