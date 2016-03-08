using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.SysStructure;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.Web.Sys
{
    public partial class XingChengLiangDianBJ : BackPage
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
            BindAreaList(0);
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
                    MSysJourneyLight model = new BSysOptionConfig().GetJourneyLight(id);
                    if (model != null)
                    {
                        txt_backMark.Text = model.JourneySpot;
                        SelectSection1.SectionID = model.DeptID.ToString();
                        SelectSection1.SectionName = getComName(model.DeptID.ToString());
                        BindAreaList(model.AreaID);
                    }

                }
            }
        }

        protected void save(string dotype)
        {
            int result = 0;
            BSysOptionConfig bll = new BSysOptionConfig();
            MSysJourneyLight model = new MSysJourneyLight();
            int masterID = Utils.GetInt(Utils.GetQueryStringValue("masterID"));
            model.CompanyId = SiteUserInfo.CompanyId;
            model.DeptID = Utils.GetInt(Utils.GetFormValue(SelectSection1.SelectIDClient));
            model.AreaID = Utils.GetInt(Utils.GetFormValue("sltArea"));
            model.IssueTime = DateTime.Now;
            model.JourneySpot = Utils.GetFormEditorValue(txt_backMark.UniqueID);
            model.OperatorDeptId = SiteUserInfo.DeptId;
            model.OperatorId = SiteUserInfo.UserId;
            model.Operator = SiteUserInfo.Name;
            switch (dotype)
            {
                case "":
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "信息丢失!"));
                    break;
                case "update":
                    model.MasterId = masterID != 0 ? masterID : model.Id;
                    model.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(Utils.GetFormValue("lngType"));
                    if (bll.isEsistsJourneyLight(model.MasterId, model.LngType))
                    {
                        result = bll.UpdateJourneyLight(model);
                    }
                    else
                    {
                        result = bll.AddJourneyLight(model);
                    }
                    AjaxResponse(UtilsCommons.AjaxReturnJson(result.ToString(), result == 0 ? "修改失败" : "修改成功"));
                    break;
                case "add":
                    model.MasterId = 0;
                    model.LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
                    result = bll.AddJourneyLight(model);
                    AjaxResponse(UtilsCommons.AjaxReturnJson(result.ToString(), result == 0 ? "添加失败" : "添加成功"));
                    break;
                default:
                    break;
            }
        }

        #region 绑定线路区域
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void BindAreaList(int selectIndex)
        {

            StringBuilder sb = new StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComArea> list = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(SiteUserInfo.CompanyId);
            sb.Append("<option value=\"0\">-请选择-</option>");
            if (list != null && list.Count > 0)
            {
                string type = string.Empty;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].AreaId != selectIndex)
                    {
                        sb.Append("<option  value=\"" + list[i].AreaId + "\">" + list[i].AreaName + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value=\"" + list[i].AreaId + "\" selected=\"selected\">" + list[i].AreaName + "</option>");
                    }
                }
            }
            this.litArea.Text = sb.ToString();

        }

        #endregion

        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="ComID"></param>
        /// <returns></returns>
        protected string getComName(string ComID)
        {
            int id = Utils.GetInt(ComID);
            EyouSoft.Model.ComStructure.MComDepartment model = new EyouSoft.BLL.ComStructure.BComDepartment().GetModel(id, SiteUserInfo.CompanyId);
            if (model != null) return model.DepartName;

            return "";
        }


    }
}
