using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using SiMay.Core;
using System.Net.Sockets;
using System.Diagnostics;
using SiMay.Sockets.Tcp.Client;
using SiMay.Sockets.Tcp;
using SiMay.Sockets.Delegate;
using System.Text;
using SiMay.Core.Entitys;
using SiMay.Serialize;
using SiMay.Basic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using SiMay.ServiceCore.MainService;
using SiMay.Serialize.Standard;

namespace SiMay.ServiceCore
{
    public class StartParameterEx
    {
        public string UniqueId { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string RemarkInformation { get; set; }
        public string GroupName { get; set; }
        public bool IsAutoStart { get; set; }
        public bool IsHide { get; set; }
        public int SessionMode { get; set; }
        public long AccessKey { get; set; }
        public bool IsMutex { get; set; }
        public string ServiceVersion { get; set; }
        public string RunTimeText { get; set; }
        public bool InstallService { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
    }
    static class Program
    {
        /// <summary>
        /// 服务启动参数
        /// </summary>
        const string SERVICE_START = "-serviceStart";

        /// <summary>
        /// SYSTEM用户进程启动参数
        /// </summary>
        const string SERVICE_USER_START = "-user";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //if (args.Any(c => c.Equals(SERVICE_START, StringComparison.OrdinalIgnoreCase)))
            //{
            //    ServiceBase.Run(new ServiceBase[]
            //    {
            //        new Service()
            //    });
            //}
            //else//非服务启动
            //{
                var ip = args[0];
                var port = int.Parse(args[1]);
                var accessKey = long.Parse(args[2]);
                var des = args[3];
                var groupName = args[4];
                var startParameter = new StartParameterEx()
                {
                    Host = ip,
                    Port = port,
                    //Port = 522,
                    GroupName = groupName,
                    RemarkInformation = des,
                    IsHide = false,
                    IsMutex = true,
                    IsAutoStart = false,
                    SessionMode = 0,
                    //SessionMode = 1,
                    AccessKey = accessKey,
                    ServiceVersion = "正式6.0",
                    RunTimeText = DateTime.Now.ToString(),
                    UniqueId = "AAAAAAAAAAAAAAA11111111" + $"_{Environment.MachineName}",//被控端唯一ID，用于标识
                    ServiceName = "SiMayService",
                    ServiceDisplayName = "SiMay远程被控服务",
                    InstallService = true
                };
                //try
                //{
                //    byte[] binary = File.ReadAllBytes(Application.ExecutablePath);
                //    var sign = BitConverter.ToInt16(binary, binary.Length - sizeof(Int16));
                //    if (sign == 9999)
                //    {
                //        var length = BitConverter.ToInt32(binary, binary.Length - sizeof(Int16) - sizeof(Int32));
                //        byte[] bytes = new byte[length];
                //        Array.Copy(binary, binary.Length - sizeof(Int16) - sizeof(Int32) - length, bytes, 0, length);

                //        var options = PacketSerializeHelper.DeserializePacket<ServiceOptions>(bytes);
                //        startParameter.Host = options.Host;
                //        startParameter.Port = options.Port;
                //        startParameter.RemarkInformation = options.Remark;
                //        startParameter.IsAutoStart = options.IsAutoRun;
                //        startParameter.IsHide = options.IsHide;
                //        startParameter.AccessKey = options.AccessKey;
                //        startParameter.SessionMode = options.SessionMode;
                //        startParameter.UniqueId = options.Id + $"_{Environment.MachineName}";
                //        startParameter.IsMutex = options.IsMutex;
                //        startParameter.GroupName = options.GroupName;
                //        startParameter.InstallService = options.InstallService;
                //        startParameter.ServiceName = options.ServiceName;
                //        startParameter.ServiceDisplayName = options.ServiceDisplayName;
                //    }
                //}
                //catch { }

                if (startParameter.IsMutex)
                {
                    //进程互斥体
                    bool bExist;
                    Mutex mutex = new Mutex(true, startParameter.UniqueId + "_SiMayService", out bExist);
                    if (!bExist)
                        Environment.Exit(0);
                }

                //AppConfiguartion.HasSystemAuthority = args.Any(c => c.Equals(SERVICE_USER_START, StringComparison.OrdinalIgnoreCase));
                //AppConfiguartion.ServiceName = startParameter.ServiceName.IsNullOrEmpty() ? "SiMayService" : startParameter.ServiceName;
                //AppConfiguartion.ServiceDisplayName = startParameter.ServiceDisplayName.IsNullOrEmpty() ? "SiMay远程被控服务" : startParameter.ServiceDisplayName;

                ////初始化连接服务
                //if (args.Any(c => c.Equals(SERVICE_USER_START, StringComparison.OrdinalIgnoreCase)))
                //{
                //    LogHelper.DebugWriteLog("初始化连接服务");
                //    new UserTrunkContext(args);
                //}

                ////非SYSTEM用户进程启动则进入安装服务
                //if (startParameter.InstallService && !args.Any(c => c.Equals(SERVICE_USER_START, StringComparison.OrdinalIgnoreCase)))
                //    SystemSessionHelper.InstallAutoStartService();

                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.ThreadException += Application_ThreadException;
                try
                {
                    new MainService.MainService(startParameter);
                }
                catch (Exception ex)
                {
                    WriteException("main service exception!", ex);
                }
                //SystemMessageNotify.InitializeNotifyIcon();
                SystemMessageNotify.ShowTip("SiMay远程控制被控服务已启动!");
                Application.Run();
            //}
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
            => WriteException("main thread exception!", e.Exception);

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
            => WriteException("thread exception!", e.ExceptionObject as Exception);

        private static void WriteException(string msg, Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(msg);
            sb.Append(ex.Message);
            sb.Append(ex.StackTrace);

            using (StreamWriter fs = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "SiMaylog.log"), true))
                fs.WriteLine(sb.ToString());
#if DEBUG
            if (File.Exists("SiMaylog.log"))
                File.SetAttributes("SiMaylog.log", FileAttributes.Hidden);
#endif
        }


    }
}
