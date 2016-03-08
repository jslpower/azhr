using System;
using System.Collections.Generic;
using System.Web.Services;

namespace EyouSoft.SmsWeb
{
    /// <summary>
    /// 易诺短信中心服务，供各系统向短信中心发送指令
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SmsCenter : WebService
    {
        #region static constants
        //static constants
        /// <summary>
        /// 服务安全认证对象
        /// </summary>
        public EyouSoft.Toolkit.SoapHeader.SmsCenterApiSoapHeader SmsCenterApiSoapHeader = new EyouSoft.Toolkit.SoapHeader.SmsCenterApiSoapHeader();
        /// <summary>
        /// 服务安全认证对象(webmaster)
        /// </summary>
        public EyouSoft.Toolkit.SoapHeader.SmsCenterWebmasterApiSoapHeader WebmasterSmsCenterApiSoapHeader = new EyouSoft.Toolkit.SoapHeader.SmsCenterWebmasterApiSoapHeader();
        /// <summary>
        /// 充值明细业务逻辑
        /// </summary>
        private readonly Bll.BSmsBankCharge _bankCharge = new Bll.BSmsBankCharge();
        /// <summary>
        /// 发送短信业务逻辑
        /// </summary>
        private readonly Bll.BSendMessage _sendMessage = new Bll.BSendMessage();
        #endregion

        #region exception handler members
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="msg">异常消息</param>
        void ExceptionHandler(string msg)
        {
            var ex = new System.Exception(msg);            
            Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex, System.Web.HttpContext.Current));
            throw ex;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="ex">System.Exception</param>
        void ExceptionHandler(System.Exception ex)
        {
            Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex, System.Web.HttpContext.Current));
            throw ex;
        }
        #endregion

        #region web methods
        /// <summary>
        /// 获取公司可用余额
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="appKey">appKey</param>
        /// <returns>返回账户基础信息实体</returns>
        [System.Web.Services.Protocols.SoapHeader("SmsCenterApiSoapHeader")]
        [WebMethod]
        public Model.MSmsAccountBase GetSmsAccountYuE(string accountId, string appKey)
        {
            if (!SmsCenterApiSoapHeader.IsSafeCall) ExceptionHandler("对不起，您没有权限调用此服务！");

            string strAi = accountId;
            string strAk = appKey;
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(appKey)) return null;

            return new Bll.BSmsAccount().GetSmsAccount(strAi, strAk);
        }

        /// <summary>
        /// 客户充值，返回1成功，其它失败
        /// </summary>
        /// <param name="model">充值明细实体</param>
        /// <returns>返回1成功，其他失败</returns>
        [System.Web.Services.Protocols.SoapHeader("SmsCenterApiSoapHeader")]
        [WebMethod]
        public int RechargeByCostomer(Model.MSmsBankCharge model)
        {
            if (!SmsCenterApiSoapHeader.IsSafeCall) ExceptionHandler("对不起，您没有权限调用此服务！");

            if (model == null || string.IsNullOrEmpty(model.AccountId) || string.IsNullOrEmpty(model.AppKey) || model.ChargeAmount <= 0) return 0;

            model.AccountId = model.AccountId;
            model.AppKey =model.AppKey;
            model.AppSecret = model.AppSecret;
            model.ChargeComName = model.ChargeComName;
            model.ChargeName = model.ChargeName;
            model.ChargeTelephone = model.ChargeTelephone;

            return _bankCharge.RechargeByCostomer(model);
        }

        /// <summary>
        /// 查询充值明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns>返回充值明细集合</returns>
        [System.Web.Services.Protocols.SoapHeader("SmsCenterApiSoapHeader")]
        [WebMethod]
        public List<Model.MSmsBankCharge> GetSmsBankCharge(int pageSize, int pageIndex, ref int recordCount
            , Model.MQuerySmsBankCharge queryModel)
        {            
            if (!SmsCenterApiSoapHeader.IsSafeCall) ExceptionHandler("对不起，您没有权限调用此服务！");

            if (pageSize < 1 || pageIndex < 1 || queryModel == null) return null;

            queryModel.AccountId = queryModel.AccountId;
            queryModel.AppKey = queryModel.AppKey;
            queryModel.AppSecret = queryModel.AppSecret;
            queryModel.ChargeComName = queryModel.ChargeComName;
            queryModel.ChargeName =queryModel.ChargeName;

            return _bankCharge.GetSmsBankCharge(pageSize, pageIndex, ref recordCount, queryModel);
        }

        /// <summary>
        /// 验证要发送的短信
        /// </summary>
        /// <param name="smsPlan">发送短信提交的业务实体</param>
        /// <returns>返回验证短信结果实体</returns>
        [System.Web.Services.Protocols.SoapHeader("SmsCenterApiSoapHeader")]
        [WebMethod]
        public Model.MSendResult ValidateSend(Model.MSmsPlan smsPlan)
        {
            if (!SmsCenterApiSoapHeader.IsSafeCall) ExceptionHandler("对不起，您没有权限调用此服务！");

            return _sendMessage.ValidateSend(smsPlan);
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="smsPlan">发送短信提交的业务实体</param>
        /// <returns>返回发送短信结果实体</returns>
        [System.Web.Services.Protocols.SoapHeader("SmsCenterApiSoapHeader")]
        [WebMethod]
        public Model.MSendResult SendMessage(Model.MSmsPlan smsPlan)
        {
            if (!SmsCenterApiSoapHeader.IsSafeCall) ExceptionHandler("对不起，您没有权限调用此服务！");

            var sysModel = new Model.MSendResult();
            if (smsPlan == null)
            {
                sysModel.IsSucceed = false;
                sysModel.ErrorMessage = "没有构造发送短信实体";

                return sysModel;
            }

            smsPlan.Content = smsPlan.Content;
            smsPlan.SmsAccount.AccountId = smsPlan.SmsAccount.AccountId;
            smsPlan.SmsAccount.AppKey = smsPlan.SmsAccount.AppKey;
            smsPlan.SmsAccount.AppSecret = smsPlan.SmsAccount.AppSecret;

            return _sendMessage.Send(smsPlan);
        }

        /// <summary>
        /// 短信账户开户，返回1成功，其它失败
        /// </summary>
        /// <param name="sysType">系统类型</param>
        /// <returns>返回1成功，其它失败</returns>
        [System.Web.Services.Protocols.SoapHeader("SmsCenterApiSoapHeader")]
        [WebMethod]
        public EyouSoft.SmsWeb.Model.MRetCreateAccount CreateSmsAccount(EyouSoft.SmsWeb.Model.SystemType sysType)
        {
            var retInfo = new EyouSoft.SmsWeb.Model.MRetCreateAccount();

            if (!SmsCenterApiSoapHeader.IsSafeCall)
            {
                retInfo.Code = -1;
                retInfo.Desc = "无权限使用此服务";
                return retInfo;
            }

            if (sysType == EyouSoft.SmsWeb.Model.SystemType.None)
            {
                retInfo.Code = -2;
                retInfo.Desc = "请求错误";
                return retInfo;
            }

            EyouSoft.SmsWeb.Model.MSmsAccount info = new EyouSoft.SmsWeb.Model.MSmsAccount();
            info.SysType = sysType;

            var account = new EyouSoft.SmsWeb.Bll.BSmsAccount().AddSmsAccount(info);

            if (account == null)
            {
                retInfo.Code = -3;
                retInfo.Desc = "创建账户失败";
                return retInfo;
            }

            retInfo.Code = 1;
            retInfo.Desc = "创建账户成功";
            retInfo.Account = account.AccountId;
            retInfo.AppKey = account.AppKey;
            retInfo.AppSecret = account.AppSecret;

            return retInfo;
        }

        /// <summary>
        /// 获取短信账户信息
        /// </summary>
        /// <param name="accountId">账户信息</param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapHeader("SmsCenterApiSoapHeader")]
        [WebMethod]
        public Model.MSmsAccount GetSmsAccount(EyouSoft.SmsWeb.Model.MSmsAccountBase account)
        {
            //无权限使用此服务
            if (!SmsCenterApiSoapHeader.IsSafeCall) return null;
            //短信账户错误
            if (account == null || !new EyouSoft.SmsWeb.Bll.BSmsAccount().IsExists(account)) return null;

            var info = new Bll.BSmsAccount().GetFullSmsAccount(account.AccountId, account.AppKey);

            return info;
        }

        /// <summary>
        /// 获取短信发送通道信息集合
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapHeader("SmsCenterApiSoapHeader")]
        [WebMethod]
        public List<Model.MSmsChannelInfo> GetSmsChannels()
        {
            List<Model.MSmsChannelInfo> items = new List<EyouSoft.SmsWeb.Model.MSmsChannelInfo>();

            items.Add(new EyouSoft.SmsWeb.Model.MSmsChannelInfo()
            {
                Cnannel = EyouSoft.SmsWeb.Model.Channel.通用通道,
                Name = "通知类短信通道"
            });

            items.Add(new EyouSoft.SmsWeb.Model.MSmsChannelInfo()
            {
                Cnannel = EyouSoft.SmsWeb.Model.Channel.广告通道,
                Name = "广告营销类短信通道"
            });

            return items;
        }
        #endregion

        #region webmaster web methods
        /// <summary>
        /// 设置账户通道及单价信息，返回1成功，其它失败
        /// </summary>
        /// <param name="accountId">账户信息</param>
        /// <param name="smsUnitPrice">单价信息集合</param>
        /// <returns>返回1成功，其他失败</returns>
        [System.Web.Services.Protocols.SoapHeader("WebmasterSmsCenterApiSoapHeader")]
        [WebMethod]
        public int SetSmsUnitPrices(EyouSoft.SmsWeb.Model.MSmsAccountBase account, List<EyouSoft.SmsWeb.Model.MSmsChannelInfo> items)
        {
            //无权限使用此服务
            if (!WebmasterSmsCenterApiSoapHeader.IsSafeCall) return 0;
            //短信账户错误
            if (account == null || !new EyouSoft.SmsWeb.Bll.BSmsAccount().IsExists(account)) return -1;
            //价格验证
            if (items == null || items.Count == 0) return -2;

            if (new EyouSoft.SmsWeb.Bll.BSmsAccount().SetSmsUnitPrice(account.AccountId, items) == 1) return 1;

            return -3;
        }

        /// <summary>
        /// 短信中心充值审核
        /// </summary>
        /// <param name="info">充值审核信息业务实体</param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapHeader("WebmasterSmsCenterApiSoapHeader")]
        [WebMethod]
        public bool SetSmsBankRechargeStatus(EyouSoft.SmsWeb.Model.MCheckSmsBankCharge info)
        {
            if (!WebmasterSmsCenterApiSoapHeader.IsSafeCall) return false;

            if (info == null || string.IsNullOrEmpty(info.ChargeId) || info.Status == EyouSoft.SmsWeb.Model.ChargeStatus.未审核) return false;

            return new Bll.BSmsBankCharge().CheckRechargeState(info) == 1;
        }
        #endregion
    }
}
