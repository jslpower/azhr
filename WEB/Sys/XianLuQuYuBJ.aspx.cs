using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.ComStructure;
using EyouSoft.BLL.ComStructure;

namespace EyouSoft.Web.Sys
{
    public partial class XianLuQuYuBJ : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mark = Utils.GetQueryStringValue("mark");
            if (mark != "")
            {
                save(mark);
            }
            initPage();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        protected void initPage()
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            if (dotype == "add")
            {
                lit_option.Text = "<option value=\"" + (int)EyouSoft.Model.EnumType.SysStructure.LngType.中文 + "\" selected=\"selected\">" + EyouSoft.Model.EnumType.SysStructure.LngType.中文.ToString() + "</option>";
            }
            else
            {
                string queryLngType = Utils.GetQueryStringValue("lngType");
                this.lit_option.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SysStructure.LngType)), queryLngType);
                int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
                if (id != 0)
                {
                    EyouSoft.Model.ComStructure.MComArea model = new EyouSoft.BLL.ComStructure.BComArea().GetModel(id, SiteUserInfo.CompanyId);
                    if (model != null)
                    {
                        txt_backMark.Text = model.AreaName;
                        SelectSection1.SectionID = model.ChildCompanyId.ToString();
                        SelectSection1.SectionName = getComName(model.ChildCompanyId);
                        txtKeyword.Text = model.Keyword;
                    }

                }
            }


        }
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="dotype"></param>
        protected void save(string dotype)
        {
            string msg = string.Empty;
            string txtname = Utils.GetFormValue(txt_backMark.UniqueID);
            var keyword = Utils.GetFormValue(txtKeyword.UniqueID);
            if (string.IsNullOrEmpty(txtname))
            {
                msg += "区域名称 不能为空!<br/>";
            }
            if (string.IsNullOrEmpty(keyword))
            {
                msg += "关键字 不能为空!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }

            int result = 0;
            BComArea bll = new BComArea();
            MComArea model = new MComArea();
            int masterID = Utils.GetInt(Utils.GetQueryStringValue("masterID"));

            model.AreaName = txtname;
            model.ChildCompanyId = Utils.GetInt(Utils.GetFormValue(SelectSection1.SelectIDClient));
            model.CompanyId = SiteUserInfo.CompanyId;
            model.OperatorDeptId = SiteUserInfo.DeptId;
            model.OperatorId = SiteUserInfo.UserId;
            model.Operator = SiteUserInfo.Name;
            model.IssueTime = DateTime.Now;
            model.AreaId = Utils.GetInt(Utils.GetQueryStringValue("id"));
            model.Keyword = keyword;

            switch (dotype)
            {
                case "":
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "信息丢失!"));
                    break;
                case "update":
                    model.MasterId = masterID != 0 ? masterID : model.AreaId;
                    model.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(Utils.GetFormValue("lngType"));
                    if (bll.isEsistsArea(model.MasterId, model.LngType))
                    {
                        result = bll.Update(model) ? 1 : 0;
                    }
                    else
                    {
                        result = bll.Add(model) ? 1 : 0;
                    }
                    AjaxResponse(UtilsCommons.AjaxReturnJson(result.ToString(), result == 0 ? "修改失败" : "修改成功"));
                    break;
                case "add":
                    model.MasterId = 0;
                    model.LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
                    result = bll.Add(model) ? 1 : 0;
                    AjaxResponse(UtilsCommons.AjaxReturnJson(result.ToString(), result == 0 ? "添加失败" : "添加成功"));
                    break;
                default:
                    break;
            }

        }
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
