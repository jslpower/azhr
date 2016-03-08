using System;
using System.Collections;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.BLL.ComStructure;
    
    public partial class JieKuan : BackPage
    {
        protected bool IsEnableKis;
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
            //系统配置实体
            MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID) ?? new MComSetting();
            IsEnableKis = comModel.IsEnableKis;
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            MDebitBase queryModel = new MDebitBase();
            queryModel.IsVerificated = Utils.GetQueryStringValue("verificated") == "1";
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.TourCode = Utils.GetQueryStringValue("txt_teamNumber");
            queryModel.Borrower = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);
            queryModel.BorrowerId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);
            IList<MDebit> ls = new BFinance().GetDebitLst(pageSize, pageIndex, ref recordCount, queryModel);
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
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
            if (!CheckGrant(Privs.财务管理_借款管理_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_借款管理_栏目, true);
                return;
            }
        }
        #endregion
    }
}
