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
    public partial class MemberTypeEdit : BackPage
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
            string txttypename = Utils.GetFormValue(txtTypeName.UniqueID);
            if (string.IsNullOrEmpty(txttypename))
            {
                msg += "类型名称不能为空!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }

            EyouSoft.Model.ComStructure.MComMemberType model = new EyouSoft.Model.ComStructure.MComMemberType();
            model.TypeName = txttypename;
            model.CompanyId = SiteUserInfo.CompanyId;
            model.Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (model.Id==0)
            {
                if (new EyouSoft.BLL.ComStructure.BComMemberType().Add(model))
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
                if (new EyouSoft.BLL.ComStructure.BComMemberType().Update(model))
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
            int ID=Utils.GetInt(Utils.GetQueryStringValue("id"));
            if(ID!=0){
                txtTypeName.Text = new EyouSoft.BLL.ComStructure.BComMemberType().GetModel(SiteUserInfo.CompanyId, ID).TypeName;
            }
        }

        /// <summary>
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
            this.PageType = PageType.boxyPage;
        }

        /// 基础设置菜单编号
        /// </summary>
        public string memuid { get { return Utils.GetQueryStringValue("memuid"); } }
    }
}
