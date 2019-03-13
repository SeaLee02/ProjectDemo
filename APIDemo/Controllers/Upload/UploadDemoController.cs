using Sealee.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace APIDemo.Controllers.Upload
{
    public class UploadDemoController : ApiController
    {
        /// <summary>
        /// 上传文件保存到服务器，并重名明，返回名称集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> TestAsync()
        {
            IList<string> paths = new List<string>();
            var files = HttpContext.Current.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[0];
                string filePath = $@"upload/{DateTime.Now.ToString("yyyyMMddssfff")}_{file.FileName}";
                DirectoryInfo rootDir2 = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
                string path = System.IO.Path.Combine(
                     rootDir2.FullName, filePath);
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                file.SaveAs(path);
                paths.Add(path);
            }
            return await Task.FromResult(Ok(new { errcode = 0, data = paths }));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> UploadFile()
        {
            try
            {
                //得到上传的文件
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                //转DataTable
                DataTable dt = NopiHelper.ExcelToTable(file.InputStream, 0, 0, Path.GetExtension(file.FileName));
                //通过DataTable转实体
                //List<ModelDemo> list = NopiHelper.Mapper<ModelDemo>(dt);    
                return await Task.FromResult(Ok(new { errcode = 0, data = "成功" }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Ok(new { errcode = 0, data = ex.Message }));
            }
        }



        /// <summary>
        /// 下载只能使用Get方式，需要我们返回 HttpResponseMessage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Test()
        {
            // DataTable dt = NopiHelper.Mapper<TestA>();
            DataTable dt = new DataTable();
            //导出的名字，需要带扩展名 *.xls   *.xlsx
            string fileName = "测试.xls";
            Stream stream = NopiHelper.StreamFromDataTable(dt, fileName);
            //设置状态
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            //内容
            httpResponseMessage.Content = new StreamContent(stream);
            //类型
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            //响应内容的值，附件形式
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = HttpUtility.UrlEncode(Path.GetFileName(fileName))
            };
            return await Task.FromResult(httpResponseMessage);
        }
    }
}
