using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;

namespace EyouSoft.Web.Ashx
{
    /// <summary>
    /// 用户名验证
    /// </summary>
    /// 汪奇志 2012-04-26
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class IsExistsUsername : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string httpMethod = context.Request.HttpMethod.ToLower();
            if (httpMethod != "post")
            {
                AjaxResponse(context, UtilsCommons.AjaxReturnJson("0", "请求异常0001"));
            }

            string companyId = Utils.GetFormValue("companyid");
            string username = Utils.GetFormValue("username");

            /*if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(username))
            {
                AjaxResponse(context, UtilsCommons.AjaxReturnJson("0", "请求异常0002"));
            }*/

            if (string.IsNullOrEmpty(username))
            {
                AjaxResponse(context, UtilsCommons.AjaxReturnJson("0", "用户名为空"));
            }

            var domain = EyouSoft.Security.Membership.UserProvider.GetDomain();
            if (domain == null && domain.CompanyId != companyId)
            {
                AjaxResponse(context, UtilsCommons.AjaxReturnJson("0", "请求异常0003"));
            }

            companyId = domain.CompanyId;

            if (new EyouSoft.BLL.ComStructure.BComUser().IsExistsUserName(username, companyId,string.Empty))
            {
                AjaxResponse(context, UtilsCommons.AjaxReturnJson("-1", "用户名重复"));
            }
            else
            {
                AjaxResponse(context, UtilsCommons.AjaxReturnJson("1", "用户名可用"));
            }
        }

        /// <summary>
        /// ajax response
        /// </summary>
        /// <param name="context">httpcontext</param>
        /// <param name="s">输出字符</param>
        void AjaxResponse(HttpContext context, string s)
        {
            context.Response.Clear();
            context.Response.Write(s);
            context.Response.End();
        }

        #region IsReusable
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
