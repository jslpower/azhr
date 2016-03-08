using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;

namespace Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ReceiveJob : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = Utils.GetQueryStringValue("type");
            //团号
            string tourID = Utils.GetQueryStringValue("tourId");
            //公司id
            string comID = Utils.GetQueryStringValue("com");
            //操作人
            string Operator = HttpContext.Current.Server.UrlDecode(Utils.GetQueryStringValue("Operator"));
            //操作人id
            string OperatorID = Utils.GetQueryStringValue("OperatorID");
            //操作人部门id
            string OperatDepID = Utils.GetQueryStringValue("OperatDepID");
            //团队状态
            string state = Utils.GetQueryStringValue("state");
            if (type == "receive")
            {
                if (!string.IsNullOrEmpty(tourID) && !string.IsNullOrEmpty(comID) && !string.IsNullOrEmpty(Operator) && !string.IsNullOrEmpty(OperatorID) && !string.IsNullOrEmpty(OperatDepID))
                {
                    EyouSoft.Model.HTourStructure.MTourStatusChange TourStatusChange = new EyouSoft.Model.HTourStructure.MTourStatusChange();
                    TourStatusChange.CompanyId = comID;
                    TourStatusChange.TourId = tourID;
                    TourStatusChange.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置;
                    TourStatusChange.Operator = Operator;
                    TourStatusChange.OperatorId = OperatorID;
                    TourStatusChange.OperatorDeptId = Utils.GetInt(OperatDepID);
                    TourStatusChange.IsJieShou = state == "1" ? false : true;
                    switch (new EyouSoft.BLL.HTourStructure.BTour().UpdateTourStatus(TourStatusChange))
                    {
                        case 0:
                            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"接收失败!\"}"); break;
                        case 1:
                            if (state == "1")
                            {
                                switch (new EyouSoft.BLL.HPlanStructure.BPlan().PlanYuAnPai(TourStatusChange))
                                {
                                    case 0:
                                        context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"接收成功,预安排失败!\"}"); break;
                                    case 1:
                                        context.Response.Write("{\"result\":\"" + true + "\",\"msg\":\"接收成功!!\"}"); break;
                                }
                            }
                            else
                            {
                                context.Response.Write("{\"result\":\"" + true + "\",\"msg\":\"接收成功!!\"}");
                            }
                            break;
                    }
                }
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
