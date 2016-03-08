using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.SysStructure;
using EyouSoft.BLL.SysStructure;

namespace EyouSoft.Web.Sys
{
    public partial class BaoJiaBeiZhuBJ : BackPage
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
                    MBackPriceMark model = new BSysOptionConfig().GetBackPriceMark(id);
                    txt_backMark.Text = model != null ? model.BackMark : "";
                }
            }


        }
        protected void save(string dotype)
        {
            int result = 0;
            BSysOptionConfig bll = new BSysOptionConfig();
            MBackPriceMark model = new MBackPriceMark();
            int masterID = Utils.GetInt(Utils.GetQueryStringValue("masterID"));
            model.Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            model.BackMark = Utils.GetFormEditorValue(txt_backMark.ClientID);
            model.CompanyId = SiteUserInfo.CompanyId;
            model.IssueTime = DateTime.Now;
            model.Operator = SiteUserInfo.Name;
            model.OperatorId = SiteUserInfo.UserId;
            model.OperatorDeptId = SiteUserInfo.DeptId;

            switch (dotype)
            {
                case "":
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "信息丢失!"));
                    break;
                case "update":
                    model.MasterId = masterID != 0 ? masterID : model.Id;
                    model.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(Utils.GetFormValue("lngType"));
                    if (bll.isEsistsMBackPriceMark(model.MasterId, model.LngType))
                    {
                        result = bll.UpdateMBackPriceMark(model);
                    }
                    else
                    {
                        result = bll.AddMBackPriceMark(model);
                    }
                    AjaxResponse(UtilsCommons.AjaxReturnJson(result.ToString(), result == 0 ? "修改失败" : "修改成功"));
                    break;
                case "add":
                    model.MasterId = 0;
                    model.LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
                    result = bll.AddMBackPriceMark(model);
                    AjaxResponse(UtilsCommons.AjaxReturnJson(result.ToString(), result == 0 ? "添加失败" : "添加成功"));
                    break;
                default:
                    break;
            }




        }
    }
}
