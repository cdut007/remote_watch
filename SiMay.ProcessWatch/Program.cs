using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiMay.ProcessWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, "SiMayRemoteMonitor.exe");
            if (File.Exists(fileName))
            {
                while (true)
                {
                    Console.WriteLine("正在启动进程!");
                    Process process = Process.Start(fileName);
                    process.WaitForExit();
                    Console.WriteLine("SiMayRemoteMonitor.exe 进程已退出!");
                    Thread.Sleep(5000);
                }
            }
            else
            {
                Console.WriteLine("SiMayRemoteMonitor.exe文件未找到");
            }
        }
    }
}
