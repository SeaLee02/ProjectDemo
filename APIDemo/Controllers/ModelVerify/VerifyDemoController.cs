using APIDemo.App_Start;
using APIDemo.Controllers.ModelVerify.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIDemo.Controllers.ModelVerify
{

    [MyActionFilter]
    public class VerifyDemoController : ApiController
    {

        /// <summary>
        /// 使用强类型接受，返回强类型     推荐这种，输入，输出都新建一个对应的Dto进行处理
        /// </summary>
        /// <param name="inDto">输入类</param>
        /// <returns>输出类</returns>
        [HttpPost]
        public async Task<TestOutDto> TestAsync(TestInDto inDto)
        {
            TestOutDto outDto = new TestOutDto();
            outDto.errcode = 0;
            return await Task.FromResult(outDto);
        }
    }
}
