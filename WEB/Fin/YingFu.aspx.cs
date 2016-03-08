using System;
using System.Collections;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;
    using System.Text;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    
    public partial class YingFu : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            string doType = Utils.GetQueryStringValue("doType");
            switch (doType)
            {
                case "PlanCostConfirmed": PlanCostConfirmed(); break;
                case "PiLiangChengBenQueRen": PiLiangChengBenQueRen(); break;
                default: break;
            }

            if (UtilsCommons.IsToXls()) ToXls();

            DataInit();
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            CustomerUnitSelect1.DefaultTab = PlanProject.酒店;

            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion

            MPayableBase queryModel = GetChaXunInfo();
            MPayableSum sum = new MPayableSum();
            IList<MPayable> ls = new BFinance().GetPayableLst(
                pageSize,
                pageIndex,
                ref recordCount,
                ref sum,
                queryModel);

            if (ls != null && ls.Count > 0)
            {
                pan_sum.Visible = true;
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
            }

            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
            lbl_TotalPaid.Text = UtilsCommons.GetMoneyString(sum.TotalPaid, ProviderToMoney);
            lbl_TotalPayable.Text = UtilsCommons.GetMoneyString(sum.TotalPayable, ProviderToMoney);
            lbl_TotalUnchecked.Text = UtilsCommons.GetMoneyString(sum.TotalUnchecked, ProviderToMoney);
            lbl_TotalUnpaid.Text = UtilsCommons.GetMoneyString(sum.TotalUnpaid, ProviderToMoney);
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 成本确认
        /// </summary>
        void PlanCostConfirmed()
        {
            bool retBool = new BFinance().SetPlanCostConfirmed(
                Utils.GetQueryStringValue("planId"),
                SiteUserInfo.UserId,
                SiteUserInfo.Name,
                Utils.GetQueryStringValue("costRemark"),
                Utils.GetDecimal(Utils.GetQueryStringValue("confirmation")));

            if (retBool) AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
            else AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "确认失败!"));
        }

        /// <summary>
        /// 导出
        /// </summary>
        void ToXls()
        {
            int recordCount = 0;
            //金额汇总信息
            MPayableSum sum = new MPayableSum();
            MPayableBase queryModel = GetChaXunInfo();
            IList<MPayable> ls = new BFinance().GetPayableLst(UtilsCommons.GetToXlsRecordCount(), 1, ref  recordCount, ref sum, queryModel);

            if (ls == null || ls.Count == 0) ResponseToXls(string.Empty);

            StringBuilder sb = new StringBuilder();
            //if (!queryModel.IsClean)
            //{
                //应付账款
            sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\n",
                    "Date",
                    "FILE NO.",
                    "INVOICE No.",
                    "Detail",
                    "Detail",
                    "$",
                    "ISSUE DATE",
                    "PAY DETAIL",
                    "PAID DATE",
                    "$");
            //}
            //else
            //{
            //    //已结清账款
            //    sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\n",
            //        "计调项",
            //        "团号",
            //        "单位名称",
            //        "数量",
            //        "出团时间",
            //        "销售员",
            //        "计调员",
            //        "状态",
            //        "应付金额",
            //        "已付金额",
            //        "未付金额");
            //}




            foreach (MPayable item in ls)
            {
                sb.Append(item.IssueTime.ToShortDateString() + "\t");
                sb.Append(item.TourCode + "\t");
                sb.Append(string.Empty + "\t");
                sb.Append(item.PlanItem.ToString() + "\t");
                sb.Append(item.Supplier + "\t");
                sb.Append(item.Payable.ToString("F2") + "\t");
                var l = new BLL.FinStructure.BFinance().GetPayRegisterLstByPlanId(item.PlanId);
                if (l != null && l.Count > 0)
                {
                    var isFirst = true;
                    foreach (var m in l)
                    {
                        if (!isFirst)
                        {
                            sb.Append(string.Empty + "\t");
                            sb.Append(string.Empty + "\t");
                            sb.Append(string.Empty + "\t");
                            sb.Append(string.Empty + "\t");
                            sb.Append(string.Empty + "\t");
                            sb.Append(string.Empty + "\t");
                        }
                        sb.Append(m.IssueTime.ToShortDateString() + "\t");
                        sb.Append(m.PaymentName + "\t");
                        sb.Append((m.PayTime.HasValue?m.PayTime.Value.ToString("dd-MM-yy"):string.Empty) + "\t");
                        sb.Append(m.PaymentAmount.ToString("F2") + "\n");
                        isFirst = false;
                    }
                }
                else
                {
                    sb.Append(string.Empty + "\t");
                    sb.Append(string.Empty + "\t");
                    sb.Append(string.Empty + "\t");
                    sb.Append(string.Empty + "\n");
                }
            }
            ResponseToXls(sb.ToString());
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        void InitPrivs()
        {
            if (!CheckGrant(Privs.财务管理_应付管理_栏目)) Utils.ResponseNoPermit(Privs.财务管理_应付管理_栏目, true);
            else pan_quantityRegister.Visible = CheckGrant(Privs.财务管理_应付管理_付款登记);
        }

        /// <summary>
        /// 批量成本确认
        /// </summary>
        void PiLiangChengBenQueRen()
        {
            string[] txtQueRenIds = Utils.GetFormValues("txtQueRenIds[]");

            if (txtQueRenIds == null || txtQueRenIds.Length == 0) AjaxResponse(UtilsCommons.AjaxReturnJson("1", "批量成本确认成功!"));

            foreach (var s in txtQueRenIds)
            {
                new BFinance().SetPlanCostConfirmed(s, SiteUserInfo.UserId, SiteUserInfo.Name, string.Empty, 0);
            }

            AjaxResponse(UtilsCommons.AjaxReturnJson("1", "批量成本确认成功!"));
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        /// <returns></returns>
        MPayableBase GetChaXunInfo()
        {
            MPayableBase info = new MPayableBase();

            //系统公司Id
            info.CompanyId = CurrentUserCompanyID;
            //是否结清（未结清无参数,已结清有参数）
            info.IsClean = Utils.GetQueryStringValue("IsClean") == "1";
            switch (Utils.GetQueryStringValue("tourType"))
            {
                case "1": info.IsConfirmed = true; break;
                case "2": info.IsConfirmed = false; break;
                default: break;
            }
            //出团时间开始
            info.LDateStart = Utils.GetQueryStringValue("SDate");
            //出团时间结束
            info.LDateEnd = Utils.GetQueryStringValue("EDate");
            //付款时间开始
            info.PaymentDateS = Utils.GetQueryStringValue("payDateS");
            //付款时间结束
            info.PaymentDateE = Utils.GetQueryStringValue("payDateE");
            //计调类别
            info.PlanItem = (PlanProject?)Utils.GetEnumValueNull(typeof(PlanProject), Utils.GetQueryStringValue("sustainType"));
            //客户单位
            info.Supplier = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            info.SupplierId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            //销售员
            info.SalesmanId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            info.Salesman = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            //计调员
            info.Planer = txt_Plan.SellsName = Utils.GetQueryStringValue(txt_Plan.SellsNameClient);
            info.PlanerId = txt_Plan.SellsID = Utils.GetQueryStringValue(txt_Plan.SellsIDClient);
            //已付金额            
            info.SignPaid = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(Paid.ClientUniqueIDOperator));
            info.Paid = Utils.GetDecimalNull(Utils.GetQueryStringValue(Paid.ClientUniqueIDOperatorNumber));
            //未付金额
            info.SignUnpaid = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(Unpaid.ClientUniqueIDOperator));
            info.Unpaid = Utils.GetDecimalNull(Utils.GetQueryStringValue(Unpaid.ClientUniqueIDOperatorNumber));
            //团号
            info.TourCode = Utils.GetQueryStringValue("TourCode");

            return info;
        }
        #endregion
    }
}
