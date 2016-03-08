using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.Sys
{
    public partial class BaoJiaBiaoZhun : BackPage
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

        //删除报价标准
        private void DelStard()
        {
            int[] StardIds = Utils.GetIntArray(Utils.GetQueryStringValue("ids"), ",");
            if (new EyouSoft.BLL.ComStructure.BComStand().Delete(SiteUserInfo.CompanyId, StardIds))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('删除成功');window.location.href='BaoJiaBiaoZhun.aspx?sl={0}&memuid={1}';", SL, memuid));
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('删除失败');window.location.href='BaoJiaBiaoZhun.aspx?sl={0}&memuid={1}';", SL, memuid));
            }
        }

        protected void PageInit()
        {
            IList<EyouSoft.Model.ComStructure.MComStand> list = new EyouSoft.BLL.ComStructure.BComStand().GetList(SiteUserInfo.CompanyId);
            if (list != null)
            {
                i = list.Count;
                this.repList.DataSource = list;
                this.repList.DataBind();
            }
        }

        /// <summary>
        /// 基础设置菜单编号
        /// </summary>
        public string memuid { get { return Utils.GetQueryStringValue("memuid"); } }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            //if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_报价标准栏目))
            //{
            //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_报价标准栏目, false);
            //    return;
            //}
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
