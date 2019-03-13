using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIDemo.Controllers.ModelVerify.Dto
{
    public class TestInDto
    {
        [Required(ErrorMessage ="名称不能为空")]
        [StringLength(250,ErrorMessage ="名称不能大于250个字节")]
        public string Name { get; set; }


    }

    public class TestOutDto
    {

        public int errcode { get; set; }

        public string data { get; set; }

        public string errmsg { get; set; }
    }
}