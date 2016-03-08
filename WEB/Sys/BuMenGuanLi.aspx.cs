using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Function;
using EyouSoft.Model.ComStructure;
namespace EyouSoft.Web.Sys
{
    /// <summary>
    /// 行政中心-组织机构-列表
    /// </summary>
    /// 修改人：刘树超
    /// 创建时间：2013-6-17
    public partial class BuMenGuanLi : BackPage
    {
        protected string depStrHTML = string.Empty;//部门绑定数据
        /// <summary>
        /// 部门管理权限
        /// </summary>
        protected bool IsBuMenGuanLi = false;
        protected bool IsFenGongSi = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Request.QueryString["doType"];
            if (doType == "delete")
            {
                AJAX();
            }
            if (!IsPostBack)
            {
                PowerControl();
                DataInit(doType);
            }
        }
        /// <summary>
        ///获取下级部门 
        /// </summary>
        protected string GetSonDeparts(int departId)
        {
            int step = Utils.GetInt(Utils.GetQueryStringValue("step"), 0);//获取当前级数
            step = step + 1;
            int padd = step * 20;//获取缩进
            EyouSoft.BLL.ComStructure.BComDepartment BLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            IList<EyouSoft.Model.ComStructure.MComDepartment> list = null;
            IList<EyouSoft.Model.ComStructure.MComDepartment> lstPart = this.getAllInfo();
            if (departId != 0)
            {
                if (null != lstPart && lstPart.Count > 0)
                {
                    list = this.getAllInfo().Where(i => (i.PrevDepartId == departId)).ToList();
                }
            }
            else
            {
                if (null != lstPart && lstPart.Count > 0)
                {
                    list = this.getAllInfo().Where(i => (i.PrevDepartId == 0)).ToList();
                }
            }
            StringBuilder depBuilder = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (MComDepartment d in list)
                {
                    bool b = BLL.IsExistSubDept(d.DepartId);
                    depBuilder.AppendFormat("<tr id=\"tr_{0}\" sid=\"{0}\" parentId=\"{1}\">", d.DepartId, d.PrevDepartId);
                    depBuilder.AppendFormat("<td height='28' align='left' valign='center' class='step_{0}' style='padding-left:{1}px;'>", step, padd);
                    depBuilder.AppendFormat("<img src='/images/organization_0{0}.gif' width='9' height='9' {1}/>", b ? 5 : 7,
                        b ? "onclick=\"return DM.getSonD2(this);\"" : "");
                    depBuilder.AppendFormat("<a href='javascript:void(0);' step='{0}' {1} id=\"son{2}\" class='fontSize_{0}' >", step,
                        b ? "onclick=\"return DM.getSonD(this,'" + d.DepartId + "');\"" : "", d.DepartId);
                    depBuilder.AppendFormat("<strong id=\"strong{2}\"  style=\"{0}\">{1}</strong></a>", "color:#000000;", d.DepartName, d.DepartId);
                    depBuilder.AppendFormat("<span style='float:right;font-size: 12px;'>");
                    if (d.PrevDepartId == 0)
                    {
                        if (IsBuMenGuanLi)
                        {
                            depBuilder.AppendFormat("<a href='javascript:void(0);' onclick=\"return DM.addD('{0}',this);\">[添加下级部门]</a>", d.DepartId);
                        }
                        if (IsFenGongSi)
                        {
                            depBuilder.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"return DM.updateD('{0}',this);\">[编辑]</a>", d.DepartId);
                            depBuilder.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"return DM.delD('{0}',this,{1});\">[删除]</a></span>", d.DepartId, d.PrevDepartId);
                        }
                    }
                    else
                    {
                        if (IsBuMenGuanLi)
                        {
                            depBuilder.AppendFormat("<a href='javascript:void(0);' onclick=\"return DM.addD('{0}',this);\">[添加下级部门]</a>", d.DepartId);
                            depBuilder.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"return DM.updateD('{0}',this);\">[编辑]</a>", d.DepartId);
                            depBuilder.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"return DM.delD('{0}',this,{1});\">[删除]</a></span>", d.DepartId, d.PrevDepartId);
                        }
                    }

                    depBuilder.AppendFormat("</td></tr>");
                    if (departId == 0)
                    {
                        padd = 20;
                    }
                }
            }
            return depBuilder.ToString();
        }
        protected IList<EyouSoft.Model.ComStructure.MComDepartment> getAllInfo()
        {
            EyouSoft.BLL.ComStructure.BComDepartment BLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            IList<EyouSoft.Model.ComStructure.MComDepartment> list = BLL.GetList(this.SiteUserInfo.CompanyId);
            return list;
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string doType)
        {
            EyouSoft.BLL.ComStructure.BComDepartment BLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            //如果部门Id不为空则获取子集部门
            int departId = Utils.GetInt(Utils.GetQueryStringValue("id"));
            //如果不是删除操作则获取部门
            if (doType != "delete")
            {
                depStrHTML = GetSonDeparts(departId);
                if (depStrHTML == "")
                {
                    depStrHTML = "<tr id='noDepart'><td>对不起，暂无部门信息！</td></tr>";
                }
                //如果部门id不是0则是ajax请求获取子部门操作
                if (departId != 0)
                {
                    Response.Clear();
                    Response.Write(depStrHTML);
                    Response.End();
                    return;
                }
            }
            else
            {   //删除部门
                this.AJAX();
            }
        }
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX()
        {
            string msg = string.Empty;
            string id = Utils.GetQueryStringValue("id");
            EyouSoft.BLL.ComStructure.BComDepartment BLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            int result = BLL.Delete(Utils.GetInt(id), this.SiteUserInfo.CompanyId);
            switch (result)
            {
                case 1:
                    msg = "该部门已有用户，不能删除！";
                    break;
                case 2:
                    msg = "该部门已有下级部门，不能删除！";
                    break;
                case 3:
                    msg = "删除成功！";
                    break;
                case 4:
                    msg = "删除失败！";
                    break;
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result.ToString(), msg));
            Response.End();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_部门管理_部门管理))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_部门管理_部门管理, false);
            }

            IsBuMenGuanLi = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_部门管理_分公司管理);
            IsFenGongSi = this.ph_Add.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_部门管理_分公司管理);
        }
        #endregion
    }
}
