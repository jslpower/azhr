using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.SmsCenter
{
    /// <summary>
    /// 短信常用语选择
    /// 修改记录:
    /// 1、2012-04-25 曹胡生  创建
    /// </summary>
    public partial class SmsMessageSel : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void PageInit()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            var list = new EyouSoft.BLL.SmsStructure.BSmsPhrase().GetSmsPhraseList(pageSize, pageIndex, ref recordCount, CurrentUserCompanyID, null);
            if (list == null || list.Count == 0)
            {

                this.ExporPageInfoSelect1.Visible = false;
                this.repList.EmptyText = "<tr><td colspan=\"3\" align=\"center\">暂无相关记录!</td></tr>";
            }
            else
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindPage();
            }
        }
       
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

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
    }
}
