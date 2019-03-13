using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using APIDemo.App_Start;

namespace APIDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //注册全局异常
            //GlobalConfiguration.Configuration.Filters.Add(new MyExceptionFilterAttribute());

            //注册全局模型过滤
            //GlobalConfiguration.Configuration.Filters.Add(new MyActionFilterAttribute());
        }
    }
}
