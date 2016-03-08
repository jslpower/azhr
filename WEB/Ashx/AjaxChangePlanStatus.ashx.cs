using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace EyouSoft.Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxChangePlanStatus : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            switch (new EyouSoft.BLL.HPlanStructure.BPlan().ChangePlanStatus(EyouSoft.Common.Utils.GetQueryStringValue("PlanId"),(EyouSoft.Model.EnumType.PlanStructure.PlanState)EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("Status"))))
            {
                case 0:
                    context.Response.Write("更新失败！");
                    break;
                case 1:
                    context.Response.Write("更新成功！");
                    break;
            }
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
