using System;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.BLL.TourStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.BLL.ComStructure;
    
    public partial class HeSuan : BackPage
    {
        protected string PrintUri = string.Empty;

        protected bool IsShowGouWu = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            //初始化
            DataInit();
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            #region 查询实体
            MBZSearch queryModel = new MBZSearch();
            queryModel.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("SDate"));
            queryModel.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EDate"));
            queryModel.TourCode = Utils.GetQueryStringValue("teamNumber");
            queryModel.RouteName = Utils.GetQueryStringValue("lineName");
            queryModel.IsDealt = Utils.GetIntSign(Utils.GetQueryStringValue("adjustAccountsType"), -1) > 0;
            queryModel.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            //导游
            queryModel.Guide = txt_Guide.SellsName = Utils.GetQueryStringValue(txt_Guide.SellsNameClient);
            //导游Id
            queryModel.GuideId = txt_Guide.SellsID = Utils.GetQueryStringValue(txt_Guide.SellsIDClient);
            //计调
            queryModel.Planer = txt_Plan.SellsName = Utils.GetQueryStringValue(txt_Plan.SellsNameClient);
            //计调Id
            queryModel.PlanerId = txt_Plan.SellsID = Utils.GetQueryStringValue(txt_Plan.SellsIDClient);
            //销售
            queryModel.SellerName = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            //销售Id
            queryModel.SellerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            //是否显示购物收入、其他收入、其他支出、利润分配
            queryModel.IsShowGouWu = IsShowGouWu;
            #endregion
            PrintUri = new BComSetting().GetPrintUri(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);
            IList<MBZInfo> ls = new BTour().GetFinHSList(
                CurrentUserCompanyID,
                pageSize,
                pageIndex,
                ref recordCount,
                queryModel,
                SiteUserInfo.DeptId);


            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            switch ((Menu2)Utils.GetInt(SL))
            {
                case Menu2.财务管理_单团核算:
                    if (!CheckGrant(Privs.财务管理_单团核算_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.财务管理_单团核算_栏目, false);
                    }
                    break;
                case Menu2.统计分析_单团核算:
                    if (!CheckGrant(Privs.统计分析_单团核算_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.统计分析_单团核算_栏目, false);
                    }
                    this.IsShowGouWu = true;
                    break;
                default:
                    Utils.ResponseGoBack();
                    break;
            }
        }

        #endregion
    }
}
