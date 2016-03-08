using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.Sys
{
    /// <summary>
    /// 支付方式列表
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class ZhiFuFangShi : BackPage
    {
        protected int i = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                if (Utils.GetQueryStringValue("state") == "del")
                {
                    DelPayStyle();
                }
                PageInit();
            }
        }

        //删除支付方式
        private void DelPayStyle()
        {
            int StardId = Utils.GetInt(Utils.GetQueryStringValue("ids"));
            Response.Clear();
            if (new EyouSoft.BLL.ComStructure.BComPayment().Delete(StardId, SiteUserInfo.CompanyId))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "删除成功!"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "支付方式与系统有关联，删除失败!"));
            }
            Response.End();
        }

        protected void PageInit()
        {
            IList<EyouSoft.Model.ComStructure.MComPayment> list = new EyouSoft.BLL.ComStructure.BComPayment().GetList(SiteUserInfo.CompanyId);
            this.repList.DataSource = list;
            this.repList.DataBind();
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目, false);
                return;
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.general;
        }

        /// 基础设置菜单编号
        /// </summary>
        public string memuid { get { return Utils.GetQueryStringValue("memuid"); } }
    }
}
