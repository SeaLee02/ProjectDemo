using log4net;

namespace WindowServerDemo
{
    public class MyLoggerManager
    {
        /// <summary>
        /// 创建写入log
        /// </summary>
        public static readonly ILog AppLogger = LogManager.GetLogger(typeof(MyLoggerManager));
    }
}
