using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class JiuDianJiaGe : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected string GysId = string.Empty;
        bool Privs_Delete = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {            
            GysId = Utils.GetQueryStringValue("gysid");
            if (string.IsNullOrEmpty(GysId)) RCWE("异常请求");

            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "delete") Delete();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_新增);
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var items = new EyouSoft.BLL.HGysStructure.BJiaGe().GetJiuDianJiaGes(GysId);
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        void Delete()
        {
            if (!Privs_Delete) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));

            string s = Utils.GetQueryStringValue("jiageid");
            int bllRetCode = new EyouSoft.BLL.HGysStructure.BJiaGe().DeleteJiuDianJiaGe(SiteUserInfo.CompanyId, GysId, s);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("1", "操作失败"));
        }
        #endregion
    }
}
