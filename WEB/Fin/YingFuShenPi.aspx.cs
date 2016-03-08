using System;
using System.Collections;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using System.Text;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.BLL.ComStructure;
    
    public partial class YingFuShenPi : BackPage
    {
        protected bool IsEnableKis;
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        /// 需要在程序中改变则去掉readonly修饰
        private readonly int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            if (UtilsCommons.IsToXls())
            {
                ToXls();
            }
            //初始化
            DataInit();

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //系统配置实体
            MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID) ?? new MComSetting();
            IsEnableKis = comModel.IsEnableKis;
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            decimal sum = 0;

            IList<MPayableApprove> ls = new BFinance().GetMPayableApproveLst(
                pageSize,
                pageIndex,
                ref recordCount,
                ref sum,
                GetQuery());
            lbl_sum.Text = UtilsCommons.GetMoneyString(sum, ProviderToMoney);
            if (ls != null && ls.Count > 0)
            {
                pan_sum.Visible = true;
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage();
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;

        }

        MPayableApproveBase GetQuery()
        {
            MPayableApproveBase queryModel = new MPayableApproveBase();
            #region 查询参数
            //公司id
            queryModel.CompanyId = CurrentUserCompanyID;
            //
            int status = Utils.GetIntSign(Utils.GetQueryStringValue("Status"), -1);
            if (status >= 0)
            {
                queryModel.Status = (FinStatus)status;
            }

            queryModel.PlanTyp = (PlanProject?)Utils.GetIntNull(Utils.GetQueryStringValue("sustainType"));

            //团号
            queryModel.TourCode = Utils.GetQueryStringValue("txt_teamNumber");
            //供应商
            queryModel.Supplier = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            //供应商Id
            queryModel.SupplierId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            //请款人
            queryModel.Dealer = SellsSelect1.SellsName = Utils.GetQueryStringValue(SellsSelect1.SellsNameClient);
            //请款人Id
            queryModel.DealerId = SellsSelect1.SellsID = Utils.GetQueryStringValue(SellsSelect1.SellsIDClient);
            //付款时间 始
            queryModel.PaymentDateS = Utils.GetQueryStringValue("txt_paymentDateS");
            //付款时间 终
            queryModel.PaymentDateE = Utils.GetQueryStringValue("txt_paymentDateE");
            //最晚付款日期 始
            queryModel.DeadlineS = Utils.GetQueryStringValue("txt_DeadlineS");
            //最晚付款日期 终
            queryModel.DeadlineE = Utils.GetQueryStringValue("txt_DeadlineE");
            queryModel.SL = (Menu2)Utils.GetInt(SL);
            #endregion
            return queryModel;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 导出
        /// </summary>
        private void ToXls()
        {
            int recordCount = 0;
            //金额汇总信息
            decimal sum = 0;
            IList<MPayableApprove> ls = new BFinance().GetMPayableApproveLst(
                UtilsCommons.GetToXlsRecordCount(),
                1,
                ref  recordCount,
                ref sum,
                GetQuery());
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //应付账款
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\n",
                    "计调项",
                    "团号",
                    "出团时间",
                    "供应商单位",
                    "请款人",
                    "付款金额",
                    "最晚付款时间",
                    "备注");
                foreach (MPayableApprove item in ls)
                {
                    sb.Append(item.PlanTyp + "\t");
                    sb.Append(item.TourCode + "\t");
                    sb.Append(UtilsCommons.GetDateString(item.LDate, ProviderToDate) + "\t");
                    sb.Append(item.Supplier + "\t");
                    sb.Append(item.Dealer + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.PayAmount, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetDateString(item.PayExpire, ProviderToDate) + "\t");
                    sb.Append(item.Remark + "\n");
                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_应付管理_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_应付管理_栏目, true);
                return;
            }
            else
            {
                pan_quantityExamineA.Visible = CheckGrant(Privs.财务管理_应付管理_付款审批);

                pan_quantityPayMoney.Visible = CheckGrant(Privs.财务管理_应付管理_付款支付);
            }
        }
        #endregion
    }
}
