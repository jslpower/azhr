using System;
using System.Collections;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.BLL.ComStructure;

    public partial class QiTaZhiChu : BackPage
    {
        protected bool IsEnableKis;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetFormValue("doType").Length > 0)
            {
                DeleteData();
            }
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
            #region 配置用户控件
            txt_SellsSelect.SetTitle = "选择 -请款人-";

            //CustomerUnitSelect1.TxtCssClass = "formsize80";
            //CustomerUnitSelect1.BoxyTitle = unitTitle + "单位";
            #endregion
            #region 查询实体
            MOtherFeeInOutBase queryModel = new MOtherFeeInOutBase();
            //公司编号
            queryModel.CompanyId = CurrentUserCompanyID;
            //付款时间--始
            queryModel.DealTimeS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txt_inDateS"));
            //付款时间--终
            queryModel.DealTimeE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txt_inDateE"));
            //付款项目
            queryModel.FeeItem = Utils.GetQueryStringValue("txt_PayItemName");
            //客户单位Id
            queryModel.CrmId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            //客户单位Name
            queryModel.Crm = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            //销售员Id
            queryModel.DealerId = txt_SellsSelect.SellsID = Utils.GetQueryStringValue(txt_SellsSelect.SellsIDClient);
            //销售员Name
            queryModel.Dealer = txt_SellsSelect.SellsName = Utils.GetQueryStringValue(txt_SellsSelect.SellsNameClient);

            queryModel.Status = (FinStatus)Utils.GetIntSign(Utils.GetQueryStringValue("Status"), -1);

            #endregion

            #region 绑定列表
            BFinance bfBLL = new BFinance();
            IList<MOtherFeeInOut> ls = bfBLL.GetOtherFeeInOutLst(
                pageSize,
                pageIndex,
                ref recordCount,
                 ItemType.支出,
                 queryModel);
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
                  ItemType.支出,
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
            if (!CheckGrant(Privs.财务管理_其他支出_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_其他支出_栏目, true);
                return;
            }
            else
            {
                pan_Add.Visible = pan_copy.Visible = CheckGrant(Privs.财务管理_其他支出_登记);

                pan_update.Visible = CheckGrant(Privs.财务管理_其他支出_修改);

                pan_delete.Visible = CheckGrant(Privs.财务管理_其他支出_删除);

                pan_shenhe.Visible = CheckGrant(Privs.财务管理_其他支出_审批);

                pan_zhifu.Visible = CheckGrant(Privs.财务管理_其他支出_支付);
            }
        }
        #endregion
    }
}
