using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.Webmaster
{
    /// <summary>
    /// 短信中心管理-充值审核
    /// </summary>
    /// 汪奇志 2012-04-17
    public partial class smsbank : WebmasterPageBase
    {
        #region attributes
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页索引
        /// </summary>
        protected int PageIndex = 1;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("isshenhe") == "400") ShenHe();

            InitCharges();
        }

        #region private members
        /// <summary>
        /// 初始化充值明细集合
        /// </summary>
        void InitCharges()
        {
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;

            var searchInfo = new EyouSoft.Model.SmsStructure.MSmsBankChargeSearchInfo();
            var items = new EyouSoft.BLL.SmsStructure.BSmsAccount().GetSmsBankCharges(PageSize, PageIndex, out recordCount, searchInfo);

            if (items != null && items.Count > 0)
            {
                this.rptCharges.DataSource = items;
                this.rptCharges.DataBind();
                this.phNotFound.Visible = false;

                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", PageSize, PageIndex, recordCount));
            }
            else
            {
                this.phNotFound.Visible = true;
            }
        }

        /// <summary>
        /// 审核操作
        /// </summary>
        void ShenHe()
        {
            var info = new EyouSoft.Model.SmsStructure.MSetSmsBankChargeStatusInfo();
            info.ChargeId = Utils.GetFormValue("chongZhiBianHao");
            info.RealAmount = Utils.GetDecimal(Utils.GetFormValue("shiJiChongZhiJinE"));
            info.ShenHeBeiZhu = Utils.GetFormValue("shenHeBeiZhu");
            info.ShenHeRen = Utils.GetFormValue("shenHeRen");
            info.Status = Utils.GetInt(Utils.GetFormValue("status"));

            string retCode = "0";

            if (new EyouSoft.BLL.SmsStructure.BSmsAccount().SetSmsBankRechargeStatus(info))
            {
                retCode = "1";
            }

            Response.Clear();
            Response.Write(retCode);
            Response.End();
        }
        #endregion

        #region protected members
        /// <summary>
        /// rptCharges_ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptCharges_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex == -1) return;
            Literal ltrStatus = (Literal)e.Item.FindControl("ltrStatus");
            Literal ltrHandler = (Literal)e.Item.FindControl("ltrHandler");
            if (ltrStatus == null || ltrHandler == null) return;

            var info = (EyouSoft.Model.SmsStructure.MSmsBankChargeInfo)e.Item.DataItem;
            if (info == null) return;

            string status = string.Empty;
            string handler = string.Empty;

            switch (info.Status)
            {
                case 0:
                    status = "<b>未审核</b>";
                    handler = string.Format("<a data_chargeid=\"{0}\" data_jine=\"{1}\" href=\"javascript:void(0)\" onclick=\"return openCashierBox(this);\">审核</a>", info.ChargeId, info.ChargeAmount);
                    break;
                case 1:
                    status = string.Format("<b style=\"color:green;\" title=\"审核人：{0}，审核时间：{1}，审核备注：{2}\">实际充值{3}</b>", info.ShenHeRen, info.ShenHeShiJian.HasValue ? info.ShenHeShiJian.Value.ToString() : string.Empty, info.ShenHeBeiZhu, info.RealAmount.ToString("C2"));
                    break;
                case 2:
                    status = string.Format("<b style=\"color:red;\" title=\"审核人：{0}，审核时间：{1}，审核备注：{2}\">未通过</b>", info.ShenHeRen, info.ShenHeShiJian.HasValue ? info.ShenHeShiJian.Value.ToString() : string.Empty, info.ShenHeBeiZhu);
                    break;
                default: break;
            }

            ltrStatus.Text = status;
            ltrHandler.Text = handler;
            
        }
        #endregion
    }
}
