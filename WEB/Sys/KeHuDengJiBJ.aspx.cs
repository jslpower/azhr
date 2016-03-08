using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.ComStructure;

namespace Web.Sys
{
    /// <summary>
    /// 客户等级编辑
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class KeHuDengJiBJ : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (!IsPostBack)
            {
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
            string txtlevelname = Utils.GetFormValue(txtLevelName.UniqueID);
            string txtBack = Utils.GetFormValue(txt_backMark.UniqueID);
            string price = Utils.GetFormValue(txt_price.UniqueID);
            if (string.IsNullOrEmpty(txtlevelname))
            {
                msg += "等级名称不能为空!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            EyouSoft.Model.ComStructure.MComLev model = new EyouSoft.Model.ComStructure.MComLev();
            model.Name = txtlevelname;
            model.BackMark = txtBack;
            model.FloatMoney = Utils.GetDecimal(price);
            model.CompanyId = SiteUserInfo.CompanyId;
            model.OperatorId = SiteUserInfo.UserId;
            model.Operator = SiteUserInfo.Name;
            model.OperatorDeptId = SiteUserInfo.DeptId;
            model.IssueTime = DateTime.Now;

            model.LevType = EyouSoft.Model.EnumType.ComStructure.LevType.其他;
            if (Id == 0)
            {
                if (new EyouSoft.BLL.ComStructure.BComLev().Add(model))
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
                if (new EyouSoft.BLL.ComStructure.BComLev().Update(model))
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
            txtLevelName.Text = Id == 0 ? "" : Name;
            MComLev model = new EyouSoft.BLL.ComStructure.BComLev().GetInfo(Id, SiteUserInfo.CompanyId);
            if (model != null)
            {
                txt_backMark.Text = model.BackMark;
                txt_price.Text = model.FloatMoney.ToString("0.00");
            }

        }

        /// <summary>
        /// 权限判断
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
            this.PageType = PageType.boxyPage;
        }

        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int Id { get { return Utils.GetInt(Utils.GetQueryStringValue("id")); } }

        /// <summary>
        /// 客户等级名称
        /// </summary>
        public string Name { get { return Server.UrlDecode(Utils.GetQueryStringValue("Name")); } }

        /// <summary>
        /// 基础设置菜单编号
        /// </summary>
        public string memuid { get { return Utils.GetQueryStringValue("memuid"); } }
    }
}
