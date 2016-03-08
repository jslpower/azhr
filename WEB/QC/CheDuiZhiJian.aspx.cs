using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;

namespace EyouSoft.Web.QC
{
    public partial class CheDuiZhiJian : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        bool Privs_Delete = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "Delete") Delete();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质检中心_车队质检_栏目))
            {
                RCWE("无权限");
            }

            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质检中心_车队质检_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质检中心_车队质检_修改);
            Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质检中心_车队质检_删除);
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            int pageSize = 20;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;

            var items = new EyouSoft.BLL.BQC.BCarTeamQC().GetCarTeamQCList(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
            else
            {
                phEmpty.Visible = true;
                paging.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.QC.MCarTeamQCSearch GetChaXunInfo()
        {
            var info = new EyouSoft.Model.QC.MCarTeamQCSearch();
            info.CarTeamName = Utils.GetQueryStringValue("txtCheDuiName");
            info.TourCode = Utils.GetQueryStringValue("txtTourCode");

            return info;
        }

        /// <summary>
        /// delete
        /// </summary>
        void Delete()
        {
            string s = Utils.GetQueryStringValue("deleteids");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));

            string[] items = s.Split(',');
            if (items == null || items.Length == 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));


            if (!Privs_Delete) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "没有删除权限！"));

            int f1 = 0;
            int f2 = 0;

            var bll = new EyouSoft.BLL.BQC.BCarTeamQC();

            foreach (var item in items)
            {
                if (bll.DeleteCarTeamQC( item))
                {
                    f1++;
                }
                else
                {
                    f2++;
                }
            }

            bll = null;

            if (f1 > 0 && f2 == 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
            if (f1 > 0 && f2 > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", f1 + "个信息删除成功，" + f2 + "个信息不允许删除。"));
            if (f1 == 0 && f2 > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", f2 + "个信息不允许删除。"));

            Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败"));
        }
        #endregion
    }
}
