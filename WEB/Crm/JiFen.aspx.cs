using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.Crm
{
    public partial class JiFen : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private readonly int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 100;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            if (doType == "save")
            {
                Response.Clear();
                Response.Write(PageSave());
                Response.End();
            }

            if (!IsPostBack)
            {
                string crmId = Utils.GetQueryStringValue("crmId");
                if (crmId != "")
                {
                    PageInit(crmId);
                }
            }
        }

        #region 根据个人会员编号初始化积分列表
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void PageInit(string crmId)
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            IList<EyouSoft.Model.CrmStructure.MJiFenInfo> list = new EyouSoft.BLL.CrmStructure.BCrmMember().GetJiFens(crmId, pageSize, pageIndex, ref recordCount);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                BindPage();
            }
            else {
                this.rptList.Controls.Add(new Literal() { Text = "<tr><td align='center' colspan='5'>暂无积分明细!</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
        #endregion

        #region 新增积分
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private string PageSave() {
            string msg = string.Empty;
            #region 获取表单
            string type = Utils.GetFormValue("sltType");
            DateTime txtDateTime = Utils.GetDateTime(Utils.GetFormValue("txtDateTime"), DateTime.Now);
            decimal txtSource = Utils.GetDecimal(Utils.GetFormValue("txtSource"));
            string txtRemarks = Utils.GetFormValue("txtRemarks");
            string crmId = Utils.GetQueryStringValue("crmId");
            #endregion

            EyouSoft.Model.CrmStructure.MJiFenInfo model = new EyouSoft.Model.CrmStructure.MJiFenInfo();
            EyouSoft.BLL.CrmStructure.BCrmMember bll = new EyouSoft.BLL.CrmStructure.BCrmMember();
            model.CrmId = crmId;
            model.IssueTime = DateTime.Now;
            model.JiFen = txtSource;
            model.JiFenShiJian = txtDateTime;
            model.OperatorId = SiteUserInfo.UserId;
            model.Remark = txtRemarks;
            model.ZengJianLeiBie = type == "1" ? EyouSoft.Model.EnumType.CrmStructure.JiFenZengJianLeiBie.增加 : EyouSoft.Model.EnumType.CrmStructure.JiFenZengJianLeiBie.减少;
            if (bll.SetJiFen(model))
            {
                msg = UtilsCommons.AjaxReturnJson("1", "操作成功!");
            }
            else {
                msg = UtilsCommons.AjaxReturnJson("0", "操作失败!");
            }
            return msg;
        }
        #endregion

        #region 分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        #endregion

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
    }
}
