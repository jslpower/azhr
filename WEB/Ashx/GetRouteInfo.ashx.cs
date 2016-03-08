using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetRouteInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string routeID = Utils.GetQueryStringValue("routeID");
            StringBuilder json = new StringBuilder();
            if (routeID != "")
            {
                var model = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(routeID);
                if (model != null)
                {
                    json = new StringBuilder("{\"result\":\"1\",\"data\":" + Newtonsoft.Json.JsonConvert.SerializeObject(model) + "}");
                }
            }
            if (json.ToString() == string.Empty)
            {
                context.Response.Write("{\"result\":\"0\",\"data\":\"\"}");
                return;
            }
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
