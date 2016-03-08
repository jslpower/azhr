using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.WebFX.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>


    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-20
    /// 说明：处理销售员，计调员，员工等输入匹配
    public class GetOrderSells : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string q = Utils.GetQueryStringValue("q");
            string companyID = Utils.GetQueryStringValue("companyID");
            context.Response.ContentType = "text/plain";
            StringBuilder sb = new StringBuilder();
            if (q != "" && companyID != "")
            {
                EyouSoft.Model.ComStructure.MComUserSearch searchModel = new EyouSoft.Model.ComStructure.MComUserSearch();
                //searchModel.UserName = q;
                searchModel.ContactName = q;
                int recordCount = 0;
                IList<EyouSoft.Model.ComStructure.MComUser> userList = new EyouSoft.BLL.ComStructure.BComUser().GetList(1, 10, ref recordCount, companyID, searchModel);
                if (userList != null && userList.Count > 0)
                {
                    for (int i = 0; i < userList.Count; i++)
                    {
                        sb.Append(userList[i].ContactName + "|" + userList[i].UserId + "|" + userList[i].DeptId + "|" + userList[i].DeptName + "|" + (int)userList[i].ContactSex + "|" + userList[i].ContactTel + "\n");
                    }
                }
                else
                {
                    sb.Append("未找到该记录| | |");
                }
            }
            context.Response.Write(sb.ToString());
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
