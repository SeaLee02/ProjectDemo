using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace WindowServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        public static void Test()
        {
            //需要配置使用log4net
            FileInfo log = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(log);
            HostFactory.Run(x =>
            {
                x.UseLog4Net();
                x.Service<ServiceRunner>();
                x.RunAsLocalSystem();

                x.SetDescription("WindowServerDemo服务的描述"); //设置服务的描述
                x.SetDisplayName("WindowServerDemo显示名称");  //服务显示的名称
                x.SetServiceName("WindowServerDemo服务名称"); //服务名称
            });
        }
    }
}
