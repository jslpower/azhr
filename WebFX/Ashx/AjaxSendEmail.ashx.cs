namespace EyouSoft.WebFX.Ashx
{
    using System;
    using System.Web;
    using System.Web.Services;

    using EyouSoft.Common;
    using EyouSoft.Common.Function;

    /// <summary>
    /// 行程单、报价单、计调安排确认单发送邮件业务逻辑处理
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxSendEmail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var toEmail = Utils.GetFormValue("ToEmail");//接收方Email
                if (!StringValidate.IsEmail(toEmail))
                {
                    context.Response.Write("请输入正确的邮箱地址！");
                    return;
                }
                var CompanyId = Utils.GetFormValue("CompanyId"); //当前公司ID 
                var CompanyName = Utils.GetFormValue("CompanyName");
                var content = HttpContext.Current.Request.Form["Content"];
                var subject = Utils.GetFormValue("Subject");

                var mail = new Adpost.Common.Mail.EmailHelper { Subject = subject, Body = content, ReceiveAddress = toEmail, SenderName = CompanyName };
                //邮件接收的地址
                if (mail.Send())
                {
                    context.Response.Write("发送成功！");
                    return;
                }
                else
                {
                    context.Response.Write("发送失败！");
                    return;
                }
            }
            catch (Exception)
            {
                
                throw;
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
