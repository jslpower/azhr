using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.Sys
{
    public partial class XingChengLiangDian : BackPage
    {
        #region 分页参数
        int recordCount = 0, pageIndex = 1, pageSize = 20;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("state") == "del")
            {
                DelLight();
            }
            initList();
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        protected void initList()
        {
            pageIndex = EyouSoft.Common.UtilsCommons.GetPadingIndex();
            IList<EyouSoft.Model.SysStructure.MSysJourneyLight> list = new EyouSoft.BLL.SysStructure.BSysOptionConfig().GetJourneyLightList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, null);
            if (list != null && list.Count > 0)
            {
                this.rpt_list.DataSource = list;
                this.rpt_list.DataBind();
                BindPage();
            }
            else
            {
                this.rpt_list.EmptyText = "<tr><td colspan=\"5\">暂无线路区域!</td></tr>";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }


        private void DelLight()
        {
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            Response.Clear();
            if (new EyouSoft.BLL.SysStructure.BSysOptionConfig().DeleteJourneyLight(id) == 1 ? true : false)
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "删除成功"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "删除失败"));
            }
            Response.End();
        }



        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_行程亮点栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_行程亮点栏目, false);
                return;
            }
        }
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
        /// <summary>
        /// 获取线路名称
        /// </summary>
        /// <param name="ComID"></param>
        /// <returns></returns>
        protected string getAreaName(string AreaID)
        {
            int id = Utils.GetInt(AreaID);
            EyouSoft.Model.ComStructure.MComArea model = new EyouSoft.BLL.ComStructure.BComArea().GetModel(id, SiteUserInfo.CompanyId);
            if (model != null) return model.AreaName;

            return "";
        }
    }
}
