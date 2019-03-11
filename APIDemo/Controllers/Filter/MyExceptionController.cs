using APIDemo.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIDemo.Controllers.Filter
{
    //[MyExceptionFilter]
    public class MyExceptionController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetTest()
        {
            HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("没有找到ID值")
            };
            //这并不会触发我们的异常过滤器，他会直接返回
            throw new HttpResponseException(resp);
        }

       
        [HttpGet]
        public async Task<IHttpActionResult> GetTest2()
        {
            //主动抛出异常
            throw new NotImplementedException("代码出错");
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTest3()
        {
            //自动处理异常
            string a = "a";
            int i = int.Parse(a);
            return await Task.FromResult(Ok(""));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTest4()
        {         
            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "错了脆了"));
        }

    }
}
