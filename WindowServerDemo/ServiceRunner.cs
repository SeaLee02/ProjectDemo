using Quartz;
using Quartz.Impl;
using Topshelf;

namespace WindowServerDemo
{
    public class ServiceRunner:ServiceControl, ServiceSuspend
    {
        private readonly IScheduler scheduler;
        public ServiceRunner()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        }

        /// <summary>
        /// 线程开始方法
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Start(HostControl hostControl)
        {
            scheduler.Start();
            MyLoggerManager.AppLogger.InfoFormat("线程开始");
            return true;
        }

        /// <summary>
        /// 线程结束
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Stop(HostControl hostControl)
        {
            scheduler.Shutdown(false);
            MyLoggerManager.AppLogger.InfoFormat("线程结束");
            return true;
        }

        public bool Continue(HostControl hostControl)
        {
            scheduler.ResumeAll();
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            scheduler.PauseAll();
            return true;
        }
    }
}
