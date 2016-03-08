using System;
using System.Collections;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.BLL.TourStructure;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.BLL.ComStructure;
    using EyouSoft.Model.ComStructure;
    
    public partial class YingShouDeng : BackPage
    {
        #region attributes
        /// <summary>
        /// 1：收款登记 2：退款登记
        /// </summary>
        protected string ShouTuiType = string.Empty;
        /// <summary>
        /// 请求页面类型 1:财务收款 2:销售收款
        /// </summary>
        protected string RequestType = string.Empty;
        /// <summary>
        /// 是否开启KIS整合
        /// </summary>
        protected bool IsEnableKis = false;
        /// <summary>
        /// 是否有登记权限
        /// </summary>
        bool IsDengJiPrivs = false;
        /// <summary>
        /// 是否有审核权限
        /// </summary>
        bool IsShenHePrivs = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RequestType = Utils.GetQueryStringValue("ParentType");
            ShouTuiType = Utils.GetQueryStringValue("ReturnOrSet");

            InitPrivs();

            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("doType"))) Save();

            if (RequestType != "1" && RequestType != "2") AjaxResponse("错误的请求");
            if (ShouTuiType != "1" && ShouTuiType != "2") AjaxResponse("错误的请求");

            DataInit();
        }

        #region private members
        /// <summary>
        /// 权限处理
        /// </summary>
        void InitPrivs()
        {
            if (RequestType == "1") //财务管理-应收管理
            {
                if (ShouTuiType == "1")//收款
                {
                    IsDengJiPrivs = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_收款登记);
                    IsShenHePrivs = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_收款审核);
                }
                else if (ShouTuiType == "2")//退款
                {
                    IsDengJiPrivs = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_退款登记);
                    IsShenHePrivs = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_退款审核);
                }

                var setting = new BComSetting().GetModel(CurrentUserCompanyID);
                if (setting != null) IsEnableKis = setting.IsEnableKis;
            }
            else if (RequestType == "2") //销售中心-销售收款
            {
                if (ShouTuiType == "1")//收款
                {
                    IsDengJiPrivs = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_收款登记);
                }
                else if (ShouTuiType == "2")//退款
                {
                    IsDengJiPrivs = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_退款登记);
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            string orderId = Utils.GetQueryStringValue("OrderId");
            IList<MTourOrderSales> items = new BTourOrder().GetTourOrderSalesListByOrderId(orderId, ShouTuiType == "1" ? CollectionRefundState.收款 : CollectionRefundState.退款);
            if (items != null && items.Count > 0)
            {
                rpt_list.DataSource = items;
                rpt_list.DataBind();
            }
            txt_Sells.SellsID = SiteUserInfo.UserId;
            txt_Sells.SellsName = SiteUserInfo.Name;

            var info = new BTourOrder().GetOrderMoney(orderId);

            if (info != null)
            {
                lbl_listTitle.Text = "<span style=\"color:" + (info.IsConfirm ? "#000000" : "#ff0000") + "\">合同金额：" + UtilsCommons.GetMoneyString(info.ConfirmMoney, ProviderToMoney) + "</span>"
                    + "&nbsp;&nbsp;已收金额：" + UtilsCommons.GetMoneyString(info.CheckMoney, ProviderToMoney)
                    //+ "&nbsp;&nbsp;已退金额：" + UtilsCommons.GetMoneyString(info.ReturnMoney, ProviderToMoney)
                    + "<span style=\"color:#ff0000\">&nbsp;&nbsp;已收待审：" + UtilsCommons.GetMoneyString((info.ReceivedMoney - info.CheckMoney), ProviderToMoney) + "</span>"
                    //+ "<span style=\"color:#ff0000\">&nbsp;&nbsp;已退待审" + UtilsCommons.GetMoneyString((info.BackMoney - info.ReturnMoney), ProviderToMoney) + "</span>"
                    + "<span style=\"color:#ff0000\">&nbsp;&nbsp;未收金额：" + UtilsCommons.GetMoneyString((info.ConfirmMoney - info.CheckMoney + info.ReturnMoney), ProviderToMoney) + "</span>";
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (RequestType != "1" && RequestType != "2") AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "错误的请求"));
            if (ShouTuiType != "1" && ShouTuiType != "2") AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "错误的请求"));

            BTourOrder bll = new BTourOrder();
            MTourOrderSales model = bll.GetTourOrderSalesById(Utils.GetFormValue("TourOrderSalesID")) ?? new MTourOrderSales();
            string msg = string.Empty;
            string doType = Utils.GetQueryStringValue("doType");

            if ((doType == "Add" || doType == "Updata") && !GetVal(model, out msg))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "操作失败!<br/>" + msg));
            }

            switch (doType)
            {
                case "Add":
                    if (!IsDengJiPrivs) AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "操作失败：无权限。"));
                    if (RequestType == "1")
                    {
                        //根据系统配置配置是否需要审核
                        MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID);
                        model.IsCheck = !comModel.FinancialIncomeReview;
                        //如果默认为不需要审核 审核人默认为当前操作人
                        if (model.IsCheck)
                        {
                            model.Approver = SiteUserInfo.Name;
                            model.ApproverId = SiteUserInfo.UserId;
                            model.ApproverDeptId = SiteUserInfo.DeptId;
                            model.ApproveTime = DateTime.Now;
                        }
                    }
                    if (bll.AddTourOrderSales(model))
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "添加失败!"));
                    }
                    break;
                case "Updata":
                    if (!IsDengJiPrivs) AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "操作失败：无权限。"));
                    if (bll.UpdateTourOrderSales(model))
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "修改失败!"));
                    }
                    break;
                case "Delete":
                    if (!IsDengJiPrivs) AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "操作失败：无权限。"));
                    if (bll.DeleteTourOrderSales(Utils.GetFormValue("TourOrderSalesID")))
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "删除失败!"));
                    }
                    break;
                case "ExamineV":
                    if (!IsShenHePrivs) AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "操作失败：无权限。"));
                    model.Approver = SiteUserInfo.Name;
                    model.ApproverId = SiteUserInfo.UserId;
                    model.ApproverDeptId = SiteUserInfo.DeptId;
                    model.ApproveTime = DateTime.Now;
                    if (new BFinance().SetTourOrderSalesCheck(model))
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "审核失败!"));
                    }
                    break;
                default: AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "错误的请求!")); break;
            }

        }

        /// <summary>
        /// 获取参数并验证
        /// </summary>
        /// <param name="model">赋值实体</param>
        /// <param name="msg">验证提示语</param>
        /// <returns></returns>
        private bool GetVal(MTourOrderSales model, out string msg)
        {
            msg = string.Empty;

            model = model ?? new MTourOrderSales();
            CollectionRefundState ReturnOrSet = ShouTuiType == "1" ? CollectionRefundState.收款 : CollectionRefundState.退款;
            model.CollectionRefundDate = Utils.GetDateTime(Utils.GetFormValue("txt_collectionRefundDate"));
            if (model.CollectionRefundDate == DateTime.MinValue)
            {
                msg += ReturnOrSet + "时间不能为空!<br/>";
            }
            //收退款金额
            model.CollectionRefundAmount = Utils.GetDecimal(Utils.GetFormValue("txt_collectionRefundAmount"));
            if (model.CollectionRefundAmount <= 0)
            {
                msg += ReturnOrSet + "金额格式不正确!<br/>";
            }
            //收退款方式
            model.CollectionRefundMode = Utils.GetIntSign(Utils.GetFormValue("sel_collectionRefundMode"), -1);
            if (model.CollectionRefundMode < 0)
            {
                msg += ReturnOrSet + "方式异常!<br/>";
            }
            if (Utils.GetFormValue("sellsFormKey").Length > 0)
            {
                //收退款人
                model.CollectionRefundOperator = Request.Form[txt_Sells.SellsNameClient] ?? Utils.GetFormValue(Utils.GetFormValue("sellsFormKey") + txt_Sells.SellsNameClient);
                //收退款人ID
                model.CollectionRefundOperatorID = Request.Form[txt_Sells.SellsIDClient] ?? Utils.GetFormValue(Utils.GetFormValue("sellsFormKey") + txt_Sells.SellsIDClient);
            }
            if (model.CollectionRefundOperator.Length <= 0 || model.CollectionRefundOperatorID.Length <= 0)
            {
                msg += ReturnOrSet + "人异常,请单击选用按钮选取!<br/>";
            }

            //退款类型
            model.CollectionRefundState = ReturnOrSet;
            //备注
            model.Memo = Utils.GetFormValue("txt_Memo");
            //订单号
            model.OrderId = Utils.GetFormValue("OrderId");
            model.Operator = SiteUserInfo.Name;
            model.OperatorId = SiteUserInfo.UserId;
            model.IsDaiShou = Utils.GetFormValue("chkDaiShou").Equals("on");
            model.DaiShouRen = Utils.GetFormValue("txtDaiShouRen");
            return msg.Length <= 0;
        }
        #endregion

        #region protected members
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
        /// 获取操作列HTML
        /// </summary>
        /// <param name="isShenHe">登记是否审核</param>
        /// <param name="IsDaoYouShiShou">是否导游实收款登记</param>
        /// <param name="id">收款登记编号</param>
        /// <returns></returns>
        protected string GetCaoZuoHTML(bool isShenHe, bool IsDaoYouShiShou, object id, object shouKuanType)
        {
            //if (isShenHe && IsEnableKis && IsShenHePrivs && !IsDaoYouShiShou)//已审核&&开启KIS整合&&审核权限&&非导游实收款
            //{
            //    return ShouTuiType == "2" ? string.Empty : string.Format("<a class=\"a_InAccount\" href=\"javascript:void(0);\">{0}</a>", new BFinance().IsFinIn(DefaultProofType.订单收款, id.ToString(), this.SiteUserInfo.CompanyId) ? "已入帐" : "未入账");
            //}

            var t = (EyouSoft.Model.EnumType.FinStructure.ShouKuanType)shouKuanType;
            if (t == EyouSoft.Model.EnumType.FinStructure.ShouKuanType.供应商代收) return string.Empty;

            if (!isShenHe && !IsDaoYouShiShou)//未审核&&非导游实收款
            {
                string s = string.Empty;

                if (IsDengJiPrivs)
                {
                    s += "<a class=\"a_Updata\" href=\"javascript:void(0);\">修改</a>&nbsp;&nbsp;<a class=\"a_Delete\" href=\"javascript:void(0);\">删除</a>";
                }

                if (IsShenHePrivs && (EyouSoft.Model.EnumType.PrivsStructure.Menu2)Utils.GetInt(this.SL) == EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_应收管理)
                {
                    s += "&nbsp;&nbsp;<a class=\"a_ExamineV\" href=\"javascript:void(0);\">审核</a>";
                }

                return s;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取操作列(添加)HTML
        /// </summary>
        /// <returns></returns>
        protected string GetTianJiaHTML()
        {
            if (!IsDengJiPrivs) return string.Empty;

            return "<a id=\"AddSave\" href=\"javascript:void(0);\">添加</a>";
        }
        #endregion
    }
}
