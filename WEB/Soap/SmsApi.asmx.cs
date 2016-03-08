using System;
using System.Web.Services;
using EyouSoft.Model.EnumType.SmsStructure;

namespace Web.Soap
{
    /// <summary>
    /// 此服务用于BackgroundServices发送短信
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SmsApi : WebService
    {
        /// <summary>
        /// 服务安全认证对象
        /// </summary>
        public EyouSoft.Toolkit.SoapHeader.SmsApiSoapHeader SmsApiSoapHeader = new EyouSoft.Toolkit.SoapHeader.SmsApiSoapHeader();

        /// <summary>
        /// 发送短信/定时发送
        /// </summary>
        /// <param name="message">发送短信提交的业务实体</param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapHeader("SmsApiSoapHeader")]
        [WebMethod]
        public EyouSoft.Model.SmsStructure.MSendResult SendMessage(EyouSoft.Model.SmsStructure.MSendMessage message)
        {
            if (!SmsApiSoapHeader.IsSafeCall)
            {
                throw new Exception("对不起，您没有权限调用此服务！");
            }

            var sysModel = new EyouSoft.Model.SmsStructure.MSendResult();
            if (message == null)
            {
                sysModel.IsSucceed = false;
                sysModel.ErrorMessage = "没有构造发送短信实体";

                return sysModel;
            }
            message.SendType = SendType.直接发送;
            return new EyouSoft.BLL.SmsStructure.BSendMessage().Send(message);
        }
    }
}
