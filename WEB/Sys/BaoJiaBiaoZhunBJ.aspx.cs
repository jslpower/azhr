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
    public partial class BaoJiaBiaoZhunBJ : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                PageInit();
            }
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
        }

        private void Save()
        {
            string msg = string.Empty;
            string txtquotename = Utils.GetFormValue(txtQuoteName.UniqueID);
            if (string.IsNullOrEmpty(txtquotename))
            {
                msg += "等级名称不能为空!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }

            EyouSoft.Model.ComStructure.MComStand model = new EyouSoft.Model.ComStructure.MComStand();
            model.Name = txtquotename;
            model.CompanyId = SiteUserInfo.CompanyId;
            model.OperatorId = SiteUserInfo.UserId;
            if (Id == 0)
            {
                if (new EyouSoft.BLL.ComStructure.BComStand().Add(model))
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1", "添加成功"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "添加失败"));
                }
            }
            else
            {
                model.Id = Id;
                if (new EyouSoft.BLL.ComStructure.BComStand().Update(model))
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "修改失败"));
                }
            }
        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            txtQuoteName.Text = Id == 0 ? "" : Name;
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
        /// 报价标准编号
        /// </summary>
        public int Id { get { return Utils.GetInt(Utils.GetQueryStringValue("id")); } }

        /// <summary>
        /// 报价标准名称
        /// </summary>
        public string Name { get { return Server.UrlDecode(Utils.GetQueryStringValue("Name")); } }

        /// <summary>
        /// 基础设置菜单编号
        /// </summary>
        public string memuid { get { return Utils.GetQueryStringValue("memuid"); } }
    }
}
