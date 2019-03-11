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
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace APIDemo.App_Start
{
    /// <summary>
    /// 异常处理程序
    /// </summary>
    public class MyExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            object obj = new
            {
                errcode = -1,
                errmsg = context.Exception
            };
            context.Result = new MyErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content =JsonConvert.SerializeObject(obj)
            };
            return base.HandleAsync(context, cancellationToken);
        }
    }

    public class MyErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response =
                             new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.Content = new StringContent(Content, Encoding.UTF8, "application/json");
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }

    }
}