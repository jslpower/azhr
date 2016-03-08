using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.SysStructure;
using EyouSoft.Common;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.Web.Sys
{
    public partial class GuoJiaGuanLiBJ : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int countryID = Utils.GetInt(Utils.GetQueryStringValue("id"));
            string mark = Utils.GetQueryStringValue("mark");
            if (mark != "")
            {
                save(mark);
            }
            initPage(countryID);
        }
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="dotype">操作类型</param>
        protected void save(string dotype)
        {
            BGeography bll = new BGeography();
            MSysCountry model = new MSysCountry();
            model.CompanyId = SiteUserInfo.CompanyId;
            model.Name = Utils.GetFormValue(txt_cnName.UniqueID);
            model.EnName = Utils.GetFormValue(txt_enName.UniqueID);
            //model.ThName = Utils.GetFormValue(txt_tnName.UniqueID);
            model.JP = Utils.GetFormValue(this.txtJP.UniqueID);
            model.QP = Utils.GetFormValue(this.txtQP.UniqueID);


            if (dotype == "add")
            {
                int result = bll.InsertCountry(model);
                RCWE(UtilsCommons.AjaxReturnJson(result.ToString(), result == 0 ? "添加失败" : "添加成功"));
            }
            else
            {
                model.CountryId = Utils.GetInt(Utils.GetQueryStringValue("id"));
                int result = bll.UpdateCountry(model);
                RCWE(UtilsCommons.AjaxReturnJson(result.ToString(), result == 0 ? "修改失败" : "修改成功"));
            }
        }

        protected void initPage(int id)
        {
            MSysCountry model = new MSysCountry();
            BGeography bll = new BGeography();
            model = bll.GetCountry(SiteUserInfo.CompanyId, id);
            if (model != null)
            {
                txt_cnName.Text = model.Name;
                txt_enName.Text = model.EnName;
                //txt_tnName.Text = model.ThName;
                this.txtJP.Text = model.JP;
                this.txtQP.Text = model.QP;
            }
        }
    }
}
