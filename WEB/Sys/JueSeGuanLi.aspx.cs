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
    /// <summary>
    /// 权限组列表
    /// </summary>
    /// 修改记录：
    /// 1、2013-6-5 刘树超 创建
    public partial class JueSeGuanLi : BackPage
    {
        public int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl(Utils.GetQueryStringValue("dotype"));
                string dotype = Utils.GetQueryStringValue("dotype");
                if (dotype == "del")
                {
                    Response.Clear();
                    DelRole();
                    Response.End();
                }
                PageInit();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            IList<EyouSoft.Model.ComStructure.MComRole> list = new EyouSoft.BLL.ComStructure.BComRole().GetList(SiteUserInfo.CompanyId);
            if (list == null || list.Count == 0)
            {
                this.repList.EmptyText = "<tr align=\"center\"><td colspan=\"6\">暂无角色信息！</td></tr>";
            }
            else
            {
                i = list.Count;
                this.repList.DataSource = list;
                this.repList.DataBind();
            }
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        protected void DelRole()
        {
            int[] ids = Utils.GetIntArray(Utils.GetQueryStringValue("ids"), ",");
            bool result = new EyouSoft.BLL.ComStructure.BComRole().Delete(ids);
            if (result)
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "删除成功!"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "删除失败!"));
            }
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="dotype"></param>
        private void PowerControl(string dotype)
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_角色管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_角色管理_栏目, false);
                return;
            }
            switch (dotype)
            {
                case "del":
                    {
                        if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_删除))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_删除, false);
                            return;
                        }
                        break;
                    }
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
