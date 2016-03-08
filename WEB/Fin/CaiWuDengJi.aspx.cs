using System;

namespace EyouSoft.Web.Fin
{
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.FinStructure;

    public partial class CaiWuDengJi : BackPage
    {
        protected int PageSize = 20;
        protected int PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int RecordCount = 0;

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
            var queryModel = GetChaXunInfo();

            var ls = new BFinance().GetDengJiList(
                this.PageSize,
                this.PageIndex,
                ref  this.RecordCount,
                queryModel);

            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage();
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && this.RecordCount > this.PageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = this.PageSize;
            ExporPageInfoSelect1.CurrencyPage = this.PageIndex;
            ExporPageInfoSelect1.intRecordCount = this.RecordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_财务情况登记_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_财务情况登记_栏目, true);
                return;
            }
            else
            {
                pan_Add.Visible = CheckGrant(Privs.财务管理_财务情况登记_新增);
                pan_Upd.Visible = CheckGrant(Privs.财务管理_财务情况登记_修改);
                pan_Del.Visible = CheckGrant(Privs.财务管理_财务情况登记_删除);
            }
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <returns></returns>
        private void DeleteData()
        {
            var retBool = new BFinance().DelDengJi(CurrentUserCompanyID, Utils.ConvertToIntArray(Utils.GetFormValue("Ids").Split(',')));
            this.AjaxResponse(retBool ? UtilsCommons.AjaxReturnJson("1") : UtilsCommons.AjaxReturnJson("-1", "删除失败!"));
        }

        /// <summary>
        /// 获取查询信息
        /// </summary>
        /// <returns></returns>
        MCaiWuDengJi GetChaXunInfo()
        {
            var info = new MCaiWuDengJi
                {
                    //系统公司编号
                    CompanyId = this.CurrentUserCompanyID,
                    //财务类型
                    Typ = (Model.EnumType.FinStructure.CaiWuDengJi)Utils.GetInt(Utils.GetQueryStringValue("selTyp")),
                    //发生时间_开始
                    ApplyDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sdate")),
                    //发生时间_结束
                    ApplyDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("edate")),
                    //标题
                    Title = Utils.GetQueryStringValue("txtTitle")
                };
            return info;
        }
        #endregion 
    }
}
