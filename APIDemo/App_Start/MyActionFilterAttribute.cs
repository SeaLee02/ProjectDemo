using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace APIDemo.App_Start
{
    public class MyActionFilterAttribute: ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (!actionContext.ModelState.IsValid)
            {
                List<KeyValuePair<string, ModelState>> vs = actionContext.ModelState.ToList();
                List<object> objList = new List<object>();
                foreach (KeyValuePair<string, ModelState> item in vs)
                {
                    IList<string> strList = new List<string>();
                    foreach (ModelError err in item.Value.Errors)
                    {
                        strList.Add(err.ErrorMessage);
                    }
                    objList.Add(new
                    {
                        key = item.Key.Split('.')[1],
                        errorMessage = strList
                    });
                }
                var obj = new
                {
                    errcode = -1,
                    err = objList
                };
                actionContext.Response = new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")

                };
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}