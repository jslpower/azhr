using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
namespace Web.SmsCenter
{
    /// <summary>
    /// 常用短信列表页
    /// 修改记录：
    /// 1、2010-04-17 曹胡生 创建
    /// </summary>
    public partial class SmsList : BackPage
    {
        #region 分页参数
        protected int PageSize = 20;
        protected int PageIndex = 1;
        protected int RecordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            if (Utils.GetQueryStringValue("dotype") == "del")
            {
                Del();
            }
            PageInit();
            BindPage();
        }

        private void PageInit()
        {
            EyouSoft.Model.SmsStructure.MQuerySmsPhrase model = new EyouSoft.Model.SmsStructure.MQuerySmsPhrase()
            {
                SmsPhraseType = new EyouSoft.Model.SmsStructure.MSmsPhraseTypeBase() { TypeId = Utils.GetInt(Utils.GetQueryStringValue("type")) },
                KeyWord = Utils.GetQueryStringValue("iptKeyWord")
            };
            var List = new EyouSoft.BLL.SmsStructure.BSmsPhrase().GetSmsPhraseList(PageSize, PageIndex, ref RecordCount, SiteUserInfo.CompanyId, model);
            if (List != null && List.Count > 0)
            {
                this.repList.DataSource = List;
                this.repList.DataBind();
            }
            else
            {
                this.repList.EmptyText = "<tr align=\"center\"><td colspan=\"3\">暂无常用短信！</td></tr>";
                this.ExporPageInfoSelect1.Visible = false;
            }
        }

        private void Del()
        {
            int[] ids = Utils.GetIntArray(Utils.GetQueryStringValue("ids"), ",");
            int result = new EyouSoft.BLL.SmsStructure.BSmsPhrase().DelSmsPhrase(ids);
            if (result == 1)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('删除成功!');window.location.href='SmsList.aspx?sl={0}';", Utils.GetQueryStringValue("sl")));
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('删除失败!');window.location.href='SmsList.aspx?sl={0}';", Utils.GetQueryStringValue("sl")));
            }
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        private void PowerControl()
        {
            //判断权限
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目))
            {
                EyouSoft.Common.Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目, true);
                return;
            }
            else if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_常用短信栏目))
            {
                EyouSoft.Common.Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_常用短信栏目, true);
                return;
            }
        }

        /// <summary>
        /// 获得类型列表
        /// </summary>
        /// <returns></returns>
        protected string GetTypeList()
        {
            var list = new EyouSoft.BLL.SmsStructure.BSmsPhraseType().GetSmsPhraseTypeList(CurrentUserCompanyID);
            string str = "<option value=\"0\">-请选择-</option>";
            foreach (var item in list)
            {
                str += string.Format("<option value=\"{0}\" {1}>{2}</option>", item.TypeId, Utils.GetInt(Utils.GetQueryStringValue("type")) == item.TypeId ? "selected=\"selected\"" : "", item.TypeName);
            }
            return str;
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = PageSize;
            this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
            this.ExporPageInfoSelect1.intRecordCount = RecordCount;
        }
    }
}
