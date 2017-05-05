using System;
using SynchronTool.Task;

namespace SynchronTool
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("不可识别的参数或参数错误。");
                return;
            }
            if (args[0].ToLower() != "synchron") return;
            Console.WriteLine("windows服务准备启动……");
            switch (args[1].ToLower())
            {
                case "user":
                    SynchronUserToDataCenterTask userTask = new SynchronUserToDataCenterTask();
                    userTask.Start();
                    break;
            }
            Console.WriteLine("windows服务准备启动完成……");
            Console.WriteLine("按任意键结束windows服务。");
            Console.ReadKey();
        }
    }
}