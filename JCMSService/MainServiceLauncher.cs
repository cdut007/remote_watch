using SiMay.ServiceCore;
using SiMay.ServiceCore.MainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCMSService
{
    [Guid("F8B7DC46-E5D2-4547-AE6C-76827577B998")]
    [ComVisible(true)]
    public interface IMainServiceLauncher
    {
        [DispId(1)]
        void StartMainService(string hostAddress, int port, long accessKey, string des, string groupName);
    }

    [Guid("4E7811BE-D2A4-4AE9-B5DC-DC6A3276F488")]
    [ClassInterface(ClassInterfaceType.None)]
    public class MainServiceLauncher : IMainServiceLauncher
    {
        /// <summary>
        /// COM接口
        /// </summary>
        /// <param name="hostAddress">服务器地址(可域名)</param>
        /// <param name="port">连接端口</param>
        /// <param name="accessKey">连接Key</param>
        /// <param name="des">备注</param>
        /// <param name="groupName">分组名称</param>
        public void StartMainService(string hostAddress, int port, long accessKey, string des, string groupName)
        {
            MessageBox.Show(hostAddress);
            try
            {
                //new MainService(new StartParameterEx()
                //{
                //    Host = "127.0.0.1",
                //    Port = 5200,
                //    GroupName = "默认",
                //    RemarkInformation = "AAAAA",
                //    IsHide = false,
                //    IsMutex = false,
                //    IsAutoStart = false,
                //    SessionMode = 0,
                //    //SessionMode = 1,
                //    AccessKey = 5200,
                //    ServiceVersion = "正式6.0",
                //    RunTimeText = DateTime.Now.ToString(),
                //    UniqueId = "ASDDDSDSDDGDFGIJIOLIEWFJOVNCILSDG" + Environment.MachineName,
                //    ServiceName = "SiMayService",
                //    ServiceDisplayName = "SiMay远程被控服务",
                //    InstallService = false
                //});
                new MainService(new StartParameterEx()
                {
                    Host = hostAddress,
                    Port = port,
                    GroupName = groupName,
                    RemarkInformation = des,
                    IsHide = false,
                    IsMutex = false,
                    IsAutoStart = false,
                    SessionMode = 0,
                    //SessionMode = 1,
                    AccessKey = accessKey,
                    ServiceVersion = "正式6.0",
                    RunTimeText = DateTime.Now.ToString(),
                    UniqueId = "ASDDDSDSDDGDFGIJIOLIEWFJOVNCILSDG" + Environment.MachineName,
                    ServiceName = "SiMayService",
                    ServiceDisplayName = "SiMay远程被控服务",
                    InstallService = false
                });
            }
            catch (Exception ex)
            {
                WriteException("MainService ExceptionError!", ex);
            }
        }

        private void WriteException(string msg, Exception ex)
        {
//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine(msg);
//            sb.Append(ex.Message);
//            sb.Append(ex.StackTrace);

//            LogHelper.WriteErrorByCurrentMethod(sb.ToString());
//#if DEBUG
//            if (File.Exists("SiMaylog.log"))
//                File.SetAttributes("SiMaylog.log", FileAttributes.Hidden);
//#endif
        }
    }
}
