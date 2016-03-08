using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.BLL.ComStructure;
    using EyouSoft.Model.ComStructure;
    
    public partial class QiTaEdit : BackPage
    {
        /// <summary>
        /// 父级类型
        /// </summary>
        protected ItemType parent;
        /// <summary>
        /// 收入支出编号
        /// </summary>
        protected int OtherFeeID = 0;
        /// <summary>
        /// 客户单位联系人,练习电话
        /// </summary>
        protected string ContactName = string.Empty, ContactPhone = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utils.GetFormValue("doType") == "Save")
            {
                Save();
            }
            parent = (ItemType)Utils.GetInt(Utils.GetQueryStringValue("parent"));
            OtherFeeID = Utils.GetInt(Utils.GetQueryStringValue("OtherFeeID"));
            PageInit();

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            #region 权限判断
            switch (parent)
            {
                case ItemType.收入:
                    //if (!CheckGrant(TravelPermission.账务管理_杂费收入_收入登记))
                    //{
                    //    Utils.ResponseNoPermit(TravelPermission.账务管理_杂费收入_收入登记, true);
                    //    return;
                    //}
                    break;
                case ItemType.支出:
                    //if (!CheckGrant(TravelPermission.账务管理_杂费支出_支出登记))
                    //{
                    //    Utils.ResponseNoPermit(TravelPermission.账务管理_杂费支出_支出登记, true);
                    //    return;
                    //}
                    break;
            }
            #endregion
            CustomerUnitSelect1.CallBack = "Add.CustomerUnitSelectCallBack";
            #region 支付方式
            IList<MComPayment> ddlSl = UtilsCommons.GetPaymentList(CurrentUserCompanyID, parent);
            if (ddlSl != null && ddlSl.Count > 0)
            {
                ddl_PayType.DataTextField = "Name";
                ddl_PayType.DataValueField = "PaymentId";
                ddl_PayType.DataSource = ddlSl;
                ddl_PayType.DataBind();
            }
            #endregion
            txt_dealTime.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
            #region 页面基本数据
            MOtherFeeInOut model = new BFinance().GetOtherFeeInOut(
                parent,
                OtherFeeID,
                CurrentUserCompanyID);
            txt_dealer.SellsID = SiteUserInfo.UserId;
            txt_dealer.SellsName = SiteUserInfo.Name;
            if (model != null)
            {
                //收/付款时间
                txt_dealTime.Text = model.DealTime.ToString(ProviderToDate);
                //收支项目
                txt_feeItem.Text = model.FeeItem;
                //金额
                txt_feeAmount.Text = Utils.FilterEndOfTheZeroDecimal(model.FeeAmount);
                //备注
                txt_remark.Text = model.Remark;
                //销售员
                if (parent == ItemType.支出)
                {
                    txt_seller.SellsID = model.SellerId;
                    txt_seller.SellsName = model.Seller;
                }
                else
                {
                    txt_seller.SellsID = model.DealerId;
                    txt_seller.SellsName = model.Dealer;
                }
                CustomerUnitSelect1.CustomerUnitId = model.CrmId;
                CustomerUnitSelect1.CustomerUnitName = model.Crm;
                if (parent == ItemType.支出)
                {
                    //请款人
                    txt_dealer.SellsID = model.DealerId;
                    txt_dealer.SellsName = model.Dealer;
                }
                this.chkIsTax.Checked = model.IsTax;
                //复制清除OtherFeeID
                if (Utils.GetQueryStringValue("type") == "Copy")
                {
                    OtherFeeID = 0;
                }
                ddl_PayType.SelectedValue = model.PayType.ToString();

            }
            #endregion

            #region 配置用户控件
            txt_seller.SetTitle = "销售员";
            txt_dealer.SetTitle = "请款人";
            #endregion


        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string msg = string.Empty;
            //判断收付类型
            parent = (ItemType)Utils.GetInt(Utils.GetFormValue("parent"));
            //财务BLL
            BFinance bfBLL = new BFinance();
            MOtherFeeInOut model = new MOtherFeeInOut();

            model.Id = Utils.GetInt(Utils.GetFormValue("OtherFeeID"), -1);
            //获取页面数据并验证
            if (GetPageVal(model, ref msg))
            {
                PageResponse(UtilsCommons.AjaxReturnJson((model.Id > 0 ? bfBLL.UpdOtherFeeInOut(parent, model) : bfBLL.AddOtherFeeInOut(parent, model)) ? "1" : "提交失败!"));
            }
            else
            {
                PageResponse(UtilsCommons.AjaxReturnJson("-1", msg));
            }
        }
        /// <summary>
        /// 页面返回
        /// </summary>
        /// <param name="ret"></param>
        private void PageResponse(string ret)
        {
            Response.Clear();
            Response.Write(ret);
            Response.End();
        }
        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <param name="model">提交实体</param>
        /// <param name="msg">验证语</param>
        /// <returns></returns>
        private bool GetPageVal(MOtherFeeInOut model, ref string msg)
        {
            //系统公司Id
            model.CompanyId = CurrentUserCompanyID;
            //客户单位
            model.Crm = Utils.GetFormValue(CustomerUnitSelect1.ClientNameKHMC);
            //客户单位Id
            model.CrmId = Utils.GetFormValue(CustomerUnitSelect1.ClientNameKHBH);
            if (model.Crm.Length <= 0 || model.CrmId.Length <= 0)
            {
                msg += "客户单位不正确!请使用选用功能选择客户<br/>";
            }
            //客户单位联系人
            model.ContactName = Utils.GetFormValue("ContactName");
            //客户单位联系电话
            model.ContactPhone = Utils.GetFormValue("ContactPhone");
            //操作时间
            model.IssueTime = DateTime.Now;
            //操作人Id
            model.OperatorId = SiteUserInfo.UserId;
            model.Operator = SiteUserInfo.Name;
            model.DeptId = SiteUserInfo.DeptId;
            //收/付款时间
            model.DealTime = Utils.GetDateTime(Utils.GetFormValue(txt_dealTime.UniqueID));
            if (model.DealTime == DateTime.MinValue)
            {
                msg += (parent == ItemType.收入 ? "收款时间" : "付款时间") + "不正确!<br/>";
            }
            //收入/支出项目
            model.FeeItem = Utils.GetFormValue(txt_feeItem.UniqueID);
            if (model.FeeItem.Length <= 0)
            {
                msg += (parent == ItemType.收入 ? "收入项目" : "支付项目") + "不正确!<br/>";
            }
            //收/支金额
            model.FeeAmount = Utils.GetDecimal(Utils.GetFormValue(txt_feeAmount.UniqueID), -1);
            if (model.FeeAmount <= 0)
            {
                msg += (parent == ItemType.收入 ? "收款金额" : "支付金额") + "不正确!<br/>";
            }
            //有请款人
            if (parent == ItemType.支出)
            {
                model.DealerId = Utils.GetFormValue(txt_dealer.SellsIDClient);
                model.Dealer = Utils.GetFormValue(txt_dealer.SellsNameClient);
            }
            else
            {
                model.DealerId = Utils.GetFormValue(txt_seller.SellsIDClient);
                model.Dealer = Utils.GetFormValue(txt_seller.SellsNameClient);
            }
            //支出 销售员
            if (parent == ItemType.支出)
            {
                model.SellerId = Utils.GetFormValue(txt_seller.SellsIDClient);
                model.Seller = Utils.GetFormValue(txt_seller.SellsNameClient);
            }
            model.IsTax = this.chkIsTax.Checked;
            //支付类型
            model.PayType = Utils.GetIntSign(Utils.GetFormValue(ddl_PayType.UniqueID), -1);
            if (model.PayType < 0)
            {
                msg += "请选择支付方式!<br/>";
            }
            model.PayTypeName = Utils.GetFormValue("PayTypeName");
            //备注
            model.Remark = Utils.GetFormValue(txt_remark.UniqueID);
            return msg.Length <= 0;
        }

    }
}
