using APIDemo.App_Start;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace APIDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            //注册异常村程序
            //config.Services.Replace(typeof(IExceptionHandler), new MyExceptionHandler());     

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // 干掉XML序列化器   两种都可以
            //GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //配置json数据格式
            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
            //忽略循环引用，如果设置为Error，则遇到循环引用的时候报错（建议设置为Error，这样更规范）
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error;
            //日期格式化，默认的格式也不好看
            jsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            // 对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
            jsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //json中属性开头字母小写的驼峰命名
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
