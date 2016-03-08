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
    public partial class MemberTypeList : BackPage
    {
        public int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                if (Utils.GetQueryStringValue("state") == "del")
                {
                    DelMemberType();
                }
                PageInit();
            }
        }

        //删除会员类型
        private void DelMemberType()
        {
            int[] MemberTypeIds = Utils.GetIntArray(Utils.GetQueryStringValue("ids"), ",");
            if (new EyouSoft.BLL.ComStructure.BComMemberType().Delete(SiteUserInfo.CompanyId, MemberTypeIds))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('删除成功');window.location.href='MemberTypeList.aspx?sl={0}&memuid={1}';", SL,memuid));
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('删除失败');window.location.href='MemberTypeList.aspx?sl={0}&memuid={1}';", SL, memuid));
            }
        }

        protected void PageInit()
        {
            IList<EyouSoft.Model.ComStructure.MComMemberType> list = new EyouSoft.BLL.ComStructure.BComMemberType().GetList(SiteUserInfo.CompanyId);
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

        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_会员类型栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_会员类型栏目, false);
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
