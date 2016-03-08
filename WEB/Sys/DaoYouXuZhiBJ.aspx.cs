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
    public partial class DaoYouXuZhiBJ : BackPage
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
            string txttypename = Utils.GetFormEditorValue(txt_area.ClientID);
            if (string.IsNullOrEmpty(txttypename))
            {
                msg += "导游须知不能为空!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }

            EyouSoft.Model.SysStructure.MSysGuideKonw model = new EyouSoft.Model.SysStructure.MSysGuideKonw();
            model.KnowMark = txttypename;
            model.DepartId = Utils.GetInt(Utils.GetFormValue(SelectSection1.SelectIDClient));
            model.CompanyId = SiteUserInfo.CompanyId;
            model.OperatorDeptId = SiteUserInfo.DeptId;
            model.OperatorId = SiteUserInfo.UserId;
            model.Operator = SiteUserInfo.Name;
            model.IssueTime = DateTime.Now;
            model.Id = Utils.GetInt(Utils.GetQueryStringValue("id"));

            if (model.Id == 0)
            {
                if (new EyouSoft.BLL.SysStructure.BSysOptionConfig().AddGuideKnow(model) == 1 ? true : false)
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
                if (new EyouSoft.BLL.SysStructure.BSysOptionConfig().UpdateGuideKnow(model) == 1 ? true : false)
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
            int ID = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (ID != 0)
            {
                var model = new EyouSoft.BLL.SysStructure.BSysOptionConfig().GetGuideKnow(ID);
                if (model != null)
                {
                    txt_area.Text = model.KnowMark;
                    SelectSection1.SectionID = model.DepartId.ToString();
                    SelectSection1.SectionName = getComName(model.DepartId);
                }


            }
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_导游需知栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_导游需知栏目, false);
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

        /// 基础设置菜单编号
        /// </summary>
        public string memuid { get { return Utils.GetQueryStringValue("memuid"); } }

        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="ComID">部门编号</param>
        /// <returns></returns>
        protected string getComName(int ComID)
        {
            EyouSoft.Model.ComStructure.MComDepartment model = new EyouSoft.BLL.ComStructure.BComDepartment().GetModel(ComID, SiteUserInfo.CompanyId);
            if (model != null) return model.DepartName;

            return "";
        }
    }
}
