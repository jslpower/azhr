using System;
using System.Collections;

namespace EyouSoft.Web.Sales
{
    using System.Collections.Generic;

    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;

    public partial class YuFuQueRen : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 页记录数
        /// </summary>
        int pageSize = 20;
        /// <summary>
        /// 页索引
        /// </summary>
        int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {


            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType == "ShenHe")
            {
                PageShenHe();
            }
            #endregion
            if (!IsPostBack)
            {
                DataInit();
            }
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageShenHe()
        {

            int[] registerIds = Utils.ConvertToIntArray(Utils.GetQueryStringValue("RegisterId").Split(','));
            string msg = string.Empty;
            bool result = false;
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_预付确认_预付确认操作))
            {
                msg = "您没有预付确认操作权限！";
            }
            if (registerIds.Length <= 0)
            {
                msg = "审批失败!";
            }
            if (msg.Length <= 0)
            {
                int i = new EyouSoft.BLL.FinStructure.BFinance().SetRegisterApprove(
                     SiteUserInfo.UserId, //审核人ID
                     SiteUserInfo.Name,//审核人
                     DateTime.Now,//审核时间(当前时间)
                     "审核意见",//审核意见
                     EyouSoft.Model.EnumType.FinStructure.FinStatus.财务待审批,//审核状态
                     CurrentUserCompanyID,//公司ID
                     registerIds//登记编号集合
                     );
                if (i > 0)
                {
                    result = true;
                    msg = "审核成功！";
                }
                else
                {
                    result = false;
                    msg = "审批失败！";
                }
            }
            Response.Clear();
            Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
            Response.End();

        }
        #endregion

        #region private members
        /// <summary>
        /// 权限验证
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_预付确认_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_预付确认_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.Model.FinStructure.MPayableApproveBase queryModel = new EyouSoft.Model.FinStructure.MPayableApproveBase();
            #region 查询参数
            //公司id
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.Status = (FinStatus?)Utils.GetInt(Utils.GetQueryStringValue("selType"));
            queryModel.IsPrepaidConfirm = true;
            queryModel.TourCode = Utils.GetQueryStringValue("th");
            queryModel.AreaName = Utils.GetQueryStringValue("qu");
            queryModel.DeadlineS = Utils.GetQueryStringValue("sd");
            queryModel.DeadlineE = Utils.GetQueryStringValue("ed");
            queryModel.Dealer = Utils.GetQueryStringValue("sq");
            queryModel.SellerName = Utils.GetQueryStringValue("yw");
            queryModel.Supplier = Utils.GetQueryStringValue("nm");
            queryModel.SL = (Menu2)Utils.GetInt(SL);
            #endregion
            decimal sum = 0;
            IList<EyouSoft.Model.FinStructure.MPayableApprove> ls = new EyouSoft.BLL.FinStructure.BFinance().GetMPayableApproveLst(
                pageSize,
                pageIndex,
                ref recordCount,
                ref sum,
                queryModel);

            if (ls != null && ls.Count > 0)
            {
                rpt.DataSource = ls;
                rpt.DataBind();
            }
            BindPage();
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
            ExporPageInfoSelect1.Visible = recordCount > pageSize;
            pan_Msg.Visible = recordCount == 0;
        }

        #endregion
    }
}
