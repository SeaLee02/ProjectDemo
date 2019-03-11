using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace APIDemo.App_Start
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            object obj = new
            {
                errcode = -1,
                errmsg = actionExecutedContext.Exception
            };
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
            {
                Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            };

            //可以根据具体情况判断
            //if (actionExecutedContext.Exception is NotImplementedException)
            //{
            //    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            //    };
            //}
            //else
            //{
            //    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            //}
            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}