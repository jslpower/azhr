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
    /// 客户等级列表
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class KeHuDengJi : BackPage
    {
        protected int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                if (Utils.GetQueryStringValue("state") == "del")
                {
                    DelStard();
                }
                PageInit();
            }
        }

        //删除客户等级
        private void DelStard()
        {
            int[] LevelIds = Utils.GetIntArray(Utils.GetQueryStringValue("ids"), ",");
            if (new EyouSoft.BLL.ComStructure.BComLev().Delete(SiteUserInfo.CompanyId, LevelIds))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1", "删除成功"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("0", "删除失败"));
            }
        }

        protected void PageInit()
        {
            IList<EyouSoft.Model.ComStructure.MComLev> list = new EyouSoft.BLL.ComStructure.BComLev().GetList(SiteUserInfo.CompanyId);
            if (list != null)
            {
                i = list.Count;
                this.CustomRepeater2.DataSource = list;
                this.CustomRepeater2.DataBind();
            }
        }

        /// <summary>
        /// 基础设置菜单编号
        /// </summary>
        public string memuid { get { return Utils.GetQueryStringValue("memuid"); } }

        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_客户等级栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_客户等级栏目, false);
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
    }
}
