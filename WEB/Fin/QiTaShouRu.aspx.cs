using System;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.KingDee;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.FinStructure;

    public partial class QiTaShouRu : BackPage
    {
        /// <summary>
        /// index = 0 审核权限,index =1 开票权限
        /// </summary>
        protected string[] PrivsPage = new string[2] { "0", "0" };

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //存在ajax请求
            if (Utils.GetQueryStringValue("doType") == "del")
            {
                DeleteData();
            }
            #endregion
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
            #region 配置用户控件
            txt_SellsSelect.SetTitle = "选择 -销售员-";
            #endregion
            #region 查询实体
            MOtherFeeInOutBase queryModel = new MOtherFeeInOutBase();
            //公司编号
            queryModel.CompanyId = CurrentUserCompanyID;
            //收款时间--始
            queryModel.DealTimeS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txt_inDateS"));
            //收款时间--终
            queryModel.DealTimeE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txt_inDateE"));
            //收款项目
            queryModel.FeeItem = Utils.GetQueryStringValue("txt_inItemName");
            //客户单位Id
            queryModel.CrmId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            //客户单位Name
            queryModel.Crm = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            //销售员Id
            queryModel.DealerId = Utils.GetQueryStringValue(txt_SellsSelect.ClientID + "_hideSellID");
            //销售员Name
            queryModel.Dealer = Utils.GetQueryStringValue(txt_SellsSelect.ClientID + "_txtSellName");
            txt_SellsSelect.SellsID = queryModel.DealerId;
            txt_SellsSelect.SellsName = queryModel.Dealer;
            queryModel.Status = (FinStatus)Utils.GetIntSign("-1", -1);
            #endregion
            #region 绑定列表

            IList<MOtherFeeInOut> ls = new BFinance().GetOtherFeeInOutLst(
                pageSize,
                pageIndex,
                ref recordCount,
                ItemType.收入,
                queryModel
                );


            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;


            #endregion

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

        #region ajax方法
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <returns></returns>
        private void DeleteData()
        {
            bool retBool = new BFinance().DelOtherFeeInOut(
                  CurrentUserCompanyID,
                  ItemType.收入,
                  Utils.ConvertToIntArray(Utils.GetFormValue("Ids").Split(','))
                  ) > 0;
            if (retBool)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "删除失败!"));
            }

        }
        #endregion
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_其他收入_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_其他收入_栏目, true);
                return;
            }
            else
            {
                pan_Add.Visible = pan_copy.Visible = CheckGrant(Privs.财务管理_其他收入_登记);

                pan_update.Visible = CheckGrant(Privs.财务管理_其他收入_修改);

                pan_delete.Visible = CheckGrant(Privs.财务管理_其他收入_删除);

                pan_shenhe.Visible = CheckGrant(Privs.财务管理_其他收入_审核);
                PrivsPage[0] = CheckGrant(Privs.财务管理_其他收入_审核) ? "1" : "0";
            }
        }
        #endregion
    }
}
