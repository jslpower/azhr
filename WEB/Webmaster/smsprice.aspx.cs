using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.Webmaster
{
    public partial class smsprice : WebmasterPageBase
    {
        #region attributes
        /// <summary>
        /// company id
        /// </summary>
        string CompanyId = string.Empty;
        /// <summary>
        /// sys id
        /// </summary>
        string SysId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyId = Utils.GetQueryStringValue("cid");
            SysId = Utils.GetQueryStringValue("sysid");

            if (!IsPostBack)
            {
                var sysInfo = new EyouSoft.BLL.SysStructure.BSys().GetSysInfo(SysId);

                if (sysInfo == null)
                {
                    RegisterAlertAndRedirectScript("请求异常。", "systems.aspx");
                }

                ltrSysName.Text = sysInfo.SysName;

                InitChannels();
                InitPrices();
            }
        }

        #region private members
        /// <summary>
        /// init prices,register prices scripts.
        /// </summary>
        void InitPrices()
        {
            var items = new EyouSoft.BLL.SmsStructure.BSmsAccount().GetSmsPrices(CompanyId);
            string s = "var prices={0};";

            if (items == null || items.Count == 0)
            {
                s = string.Format(s, "[]");
            }
            else
            {
                s = string.Format(s, Newtonsoft.Json.JsonConvert.SerializeObject(items));
            }

            RegisterScript(s);
        }

        /// <summary>
        /// init channels,register channels scripts.
        /// </summary>
        void InitChannels()
        {
            var items = new EyouSoft.BLL.SmsStructure.BSmsAccount().GetSmsChannels();
            var s = "var channels={0};";

            if (items == null || items.Count == 0)
            {
                s = string.Format(s, "[]");
            }
            else
            {
                s = string.Format(s, Newtonsoft.Json.JsonConvert.SerializeObject(items));
            }

            RegisterScript(s);
        }
        #endregion

        #region protected members
        /// <summary>
        /// btnSubmit click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            IList<EyouSoft.Model.SmsStructure.MSmsPriceInfo> items = new List<EyouSoft.Model.SmsStructure.MSmsPriceInfo>();

            #region get and validate form values 
            string[] channels = Utils.GetFormValues("txtChannel");
            string[] prices = Utils.GetFormValues("txtPrice");

            if (channels == null || channels.Length == 0 || prices == null || prices.Length == 0 || channels.Length != prices.Length)
            {
                RegisterAlertAndReloadScript("表单提交数据错误。");
                return;
            }

            IList<int> _exists = new List<int>();
            bool isexists = false;
            for (int i = 0; i < channels.Length; i++)
            {
                var item = new EyouSoft.Model.SmsStructure.MSmsPriceInfo();
                item.ChannelIndex = Utils.GetInt(channels[i]);
                item.Price = Utils.GetDecimal(prices[i]);

                if (item.Price <= 0) item.Price = 0.1M;

                items.Add(item);

                if (_exists.Contains(item.ChannelIndex)) { isexists = true; break; }

                _exists.Add(item.ChannelIndex);
            }

            if (items.Count == 0)
            {
                RegisterAlertAndReloadScript("表单提交数据错误（未提交任何通道价格信息）。");
                return;
            }

            if (isexists)
            {
                RegisterAlertAndReloadScript("表单提交数据错误（通道有重复）。");
                return;
            }
            #endregion

            if (new EyouSoft.BLL.SmsStructure.BSmsAccount().SetSmsPrices(CompanyId, items))
            {
                this.RegisterAlertAndReloadScript("短信通道价格设置成功");
            }
            else
            {
                this.RegisterAlertAndReloadScript("短信通道价格设置失败");
            }            
        }
        #endregion
    }
}
