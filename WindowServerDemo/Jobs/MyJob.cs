using Quartz;
using System;
using System.Threading.Tasks;

namespace WindowServerDemo.Jobs
{
    /// <summary>
    /// job(可以设置多个job，频率设置不一样，就会执行各自的方法)
    /// </summary>
    public class MyJob: IJob
    {
        /// <summary>
        /// 执行的入口，你的业务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
           
            //每过多长时间会执行这个方法
            await Console.Out.WriteLineAsync($"{DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss")} 开始执行服务");
        }
    }

}
