using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SmsStructure
{
    /// <summary>
    /// 短信账户相关业务逻辑
    /// </summary>
    public class BSmsAccount
    {
        #region private members
        /// <summary>
        /// 构造安全访问短信中心Api的对象
        /// </summary>
        /// <returns></returns>
        SmsCenter.SmsCenter GetSmsCenterApi()
        {
            var api = new SmsCenter.SmsCenter();
            var apiHeader = new SmsCenter.SmsCenterApiSoapHeader
            {
                SecretKey = Toolkit.ConfigHelper.ConfigClass.GetConfigString("SmsCenter_ApiKey")
            };

            api.SmsCenterApiSoapHeaderValue = apiHeader;

            return api;
        }

        /// <summary>
        /// 构造安全访问短信中心Api的对象(webmaster)
        /// </summary>
        /// <returns></returns>
        SmsCenter.SmsCenter GetSmsWebmasterCenterApi()
        {
            var api = new SmsCenter.SmsCenter();
            var apiHeader = new SmsCenter.SmsCenterWebmasterApiSoapHeader
            {
                SecretKey = Toolkit.ConfigHelper.ConfigClass.GetConfigString("SmsCenter_Webmaster_ApiKey")
            };

            api.SmsCenterWebmasterApiSoapHeaderValue = apiHeader;

            return api;
        }

        /// <summary>
        /// 设置短信账户价格信息
        /// </summary>
        /// <param name="accountId">短信账户编号</param>
        /// <param name="appKey">短信账户appKey</param>
        /// <param name="appSecret">短信账户appSecret</param>
        /// <param name="items">价格信息集合</param>
        /// <returns></returns>
        bool SetSmsPrices(string accountId, string appKey, string appSecret, IList<EyouSoft.Model.SmsStructure.MSmsPriceInfo> items)
        {
            var api = GetSmsWebmasterCenterApi();
            var account = new EyouSoft.BLL.SmsCenter.MSmsAccountBase();
            var prices = new EyouSoft.BLL.SmsCenter.MSmsChannelInfo[items.Count];

            account.AccountId = accountId;
            account.AppKey = appKey;
            account.AppSecret = appSecret;

            int i = 0;
            foreach (var item in items)
            {
                prices[i] = new EyouSoft.BLL.SmsCenter.MSmsChannelInfo();
                prices[i].Cnannel = (EyouSoft.BLL.SmsCenter.Channel)item.ChannelIndex;
                prices[i].Price = item.Price;

                i++;
            }

            return api.SetSmsUnitPrices(account, prices) == 1;
        }
        #endregion

        #region public members
        /// <summary>
        /// 创建短信账户
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.ComStructure.MSmsConfigInfo CreateSmsAccount()
        {
            var api = GetSmsCenterApi();
            var apiRetInfo = api.CreateSmsAccount(EyouSoft.BLL.SmsCenter.SystemType.峡州);

            if (apiRetInfo.Code != 1) return null;

            var info = new EyouSoft.Model.ComStructure.MSmsConfigInfo();
            info.Account = apiRetInfo.Account;
            info.AppKey = apiRetInfo.AppKey;
            info.AppSecret = apiRetInfo.AppSecret;

            //设置短信账户价格信息
            var channels = GetSmsChannels();
            if (channels != null)
            {
                IList<EyouSoft.Model.SmsStructure.MSmsPriceInfo> prices = new List<EyouSoft.Model.SmsStructure.MSmsPriceInfo>();

                foreach (var channel in channels)
                {
                    prices.Add(new EyouSoft.Model.SmsStructure.MSmsPriceInfo()
                    {
                        ChannelIndex = channel.Index,
                        Price = 0.1M
                    });
                }

                SetSmsPrices(info.Account, info.AppKey, info.AppSecret, prices);
            }

            return info;
        }

        /// <summary>
        /// 获取短信账户信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SmsStructure.MSmsAccountInfo GetAccountInfo(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
            if (setting == null || setting.SmsConfig == null || !setting.SmsConfig.IsEnabled) return null;

            var api = GetSmsCenterApi();

            var account = new EyouSoft.BLL.SmsCenter.MSmsAccountBase();
            account.AccountId = setting.SmsConfig.Account;
            account.AppKey = setting.SmsConfig.AppKey;
            account.AppSecret = setting.SmsConfig.AppSecret;

            var apiRetInfo = api.GetSmsAccount(account);
            if (apiRetInfo != null)
            {
                var info = new EyouSoft.Model.SmsStructure.MSmsAccountInfo();

                info.Account = apiRetInfo.AccountId;
                info.AppKey = apiRetInfo.AppKey;
                info.AppSecret = apiRetInfo.AppSecret;
                info.YuE = apiRetInfo.Amount;


                if (apiRetInfo.SmsUnitPrice != null && apiRetInfo.SmsUnitPrice.Length > 0)
                {
                    info.Prices = new List<EyouSoft.Model.SmsStructure.MSmsPriceInfo>();
                    foreach (var item in apiRetInfo.SmsUnitPrice)
                    {
                        var tmp = new EyouSoft.Model.SmsStructure.MSmsPriceInfo();
                        tmp.ChannelIndex = (int)item.Cnannel;
                        tmp.Price = item.Price;
                        tmp.ChannelName = item.Name;

                        info.Prices.Add(tmp);
                    }
                }

                return info;
            }

            return null;
        }

        /// <summary>
        /// 获取短信账户发送短信价格信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SmsStructure.MSmsPriceInfo> GetSmsPrices(string companyId)
        {
            var account = GetAccountInfo(companyId);

            if (account != null) return account.Prices;

            return null;
        }

        /// <summary>
        /// 获取短信通道信息集合
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SmsStructure.MSmsChannelInfo> GetSmsChannels()
        {
            var api = GetSmsCenterApi();

            var apiRetItems = api.GetSmsChannels();

            if (apiRetItems != null && apiRetItems.Length > 0)
            {
                IList<EyouSoft.Model.SmsStructure.MSmsChannelInfo> items = new List<EyouSoft.Model.SmsStructure.MSmsChannelInfo>();

                foreach (var item in apiRetItems)
                {
                    EyouSoft.Model.SmsStructure.MSmsChannelInfo tmp = new EyouSoft.Model.SmsStructure.MSmsChannelInfo();
                    tmp.Index = (int)item.Cnannel;
                    tmp.Name = item.Name;

                    items.Add(tmp);
                }

                return items;
            }

            return null;
        }

        /// <summary>
        /// 设置短信账户价格信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="items">价格信息集合</param>
        /// <returns></returns>
        public bool SetSmsPrices(string companyId, IList<EyouSoft.Model.SmsStructure.MSmsPriceInfo> items)
        {
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
            if (setting == null || setting.SmsConfig == null || !setting.SmsConfig.IsEnabled) return false;
            if (items == null || items.Count == 0) return false;


            return SetSmsPrices(setting.SmsConfig.Account, setting.SmsConfig.AppKey, setting.SmsConfig.AppSecret, items);
        }

        /// <summary>
        /// 获取短信充值明细集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SmsStructure.MSmsBankChargeInfo> GetSmsBankCharges(int pageSize, int pageIndex, out int recordCount, EyouSoft.Model.SmsStructure.MSmsBankChargeSearchInfo searchInfo)
        {
            recordCount = 0;
            IList<EyouSoft.Model.SmsStructure.MSmsBankChargeInfo> items = new List<EyouSoft.Model.SmsStructure.MSmsBankChargeInfo>();
            var api = GetSmsCenterApi();
            var apiSearchInfo = new EyouSoft.BLL.SmsCenter.MQuerySmsBankCharge();

            if (searchInfo != null)
            {
                apiSearchInfo.AccountId = searchInfo.AccountId;
                apiSearchInfo.AppKey = searchInfo.AppKey;
                apiSearchInfo.AppSecret = searchInfo.AppSecret;
                apiSearchInfo.ChargeComName = searchInfo.ChargeComName;
                apiSearchInfo.ChargeName = searchInfo.ChargeName;
                apiSearchInfo.EndTime = searchInfo.EndTime;
                apiSearchInfo.StartTime = searchInfo.StartTime;
                apiSearchInfo.Status = (EyouSoft.BLL.SmsCenter.ChargeStatus?)searchInfo.Status;
            }

            var apiRetItems = api.GetSmsBankCharge(pageSize, pageIndex, ref recordCount, apiSearchInfo);

            if (apiRetItems != null && apiRetItems.Length > 0)
            {
                foreach (var item in apiRetItems)
                {
                    var tmp =new EyouSoft.Model.SmsStructure.MSmsBankChargeInfo();
                    tmp.AccountId = item.AccountId;
                    tmp.AppKey = item.AppKey;                    
                    tmp.AppSecret = item.AppSecret;
                    tmp.ChargeAmount = item.ChargeAmount;
                    tmp.ChargeComName = item.ChargeComName;
                    tmp.ChargeId = item.ChargeId;
                    tmp.ChargeName = item.ChargeName;
                    tmp.ChargeTelephone = item.ChargeTelephone;
                    tmp.IssueTime = item.IssueTime;
                    tmp.RealAmount = item.RealAmount;
                    tmp.Status = (int)item.Status;
                    tmp.SysTypeName = item.SysType.ToString();
                    tmp.ShenHeBeiZhu = item.ShenHeBeiZhu;
                    tmp.ShenHeRen = item.ShenHeRen;
                    tmp.ShenHeShiJian = item.ShenHeShiJian;

                    items.Add(tmp);
                }
            }

            return items;
        }

        /// <summary>
        /// 短信账户充值
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool SmsBankRecharge(EyouSoft.Model.SmsStructure.MSmsBankChargeInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.AccountId)
                || string.IsNullOrEmpty(info.AppKey) || string.IsNullOrEmpty(info.AppSecret)
                || info.ChargeAmount <= 0 || string.IsNullOrEmpty(info.ChargeComName)) return false;

            var api = GetSmsCenterApi();
            var apiRequestInfo = new EyouSoft.BLL.SmsCenter.MSmsBankCharge();
            apiRequestInfo.AccountId = info.AccountId;
            apiRequestInfo.AppKey = info.AppKey;
            apiRequestInfo.AppSecret = info.AppSecret;
            apiRequestInfo.ChargeAmount = info.ChargeAmount;
            apiRequestInfo.ChargeComName = info.ChargeComName;
            apiRequestInfo.ChargeName = info.ChargeName;
            apiRequestInfo.ChargeTelephone = info.ChargeTelephone;
            apiRequestInfo.IssueTime = DateTime.Now;
            apiRequestInfo.RealAmount = 0;
            apiRequestInfo.Status = EyouSoft.BLL.SmsCenter.ChargeStatus.未审核;
            apiRequestInfo.SysType = EyouSoft.BLL.SmsCenter.SystemType.峡州;

            return api.RechargeByCostomer(apiRequestInfo) == 1;
        }


        /// <summary>
        /// 获取短信账户余额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public decimal GetSmsAccountYuE(string companyId)
        {
            var account = GetAccountInfo(companyId);

            if (account != null) return account.YuE;

            return 0;
        }

        /// <summary>
        /// 账户充值审核
        /// </summary>
        /// <param name="info">充值审核信息业务实体</param>
        /// <returns></returns>
        public bool SetSmsBankRechargeStatus(EyouSoft.Model.SmsStructure.MSetSmsBankChargeStatusInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.ChargeId) || info.RealAmount <= 0 || string.IsNullOrEmpty(info.ShenHeRen)) return false;
            var api = GetSmsWebmasterCenterApi();
            var apiRequestInfo = new EyouSoft.BLL.SmsCenter.MCheckSmsBankCharge();

            apiRequestInfo.ChargeId = info.ChargeId;
            apiRequestInfo.RealAmount = info.RealAmount;
            apiRequestInfo.Status = (EyouSoft.BLL.SmsCenter.ChargeStatus)info.Status;
            apiRequestInfo.ShenHeRen = info.ShenHeRen;
            apiRequestInfo.ShenHeBeiZhu = info.ShenHeBeiZhu;
            apiRequestInfo.ShenHeShiJian = DateTime.Now;

            return api.SetSmsBankRechargeStatus(apiRequestInfo);
        }
        #endregion
    }
}
