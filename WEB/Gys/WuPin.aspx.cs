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

namespace EyouSoft.Web.Gys
{
    public partial class WuPin : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        bool Privs_Delete = false;
        bool Privs_LingYong = false;
        bool Privs_FaFang = false;
        bool Privs_JieYue = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (Utils.GetQueryStringValue("doType") == "Delete") Delete();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "无栏目权限"));
            }

            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_入库登记);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_物品修改);
            Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_物品修改);
            Privs_LingYong = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_领用登记);
            Privs_FaFang = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_发放登记);
            Privs_JieYue = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_借阅管理);
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

            var items = new EyouSoft.BLL.HGysStructure.BWuPin().GetWuPins(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.HGysStructure.MWuPinChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MWuPinChaXunInfo();
            info.ETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtETime"));
            info.Name = Utils.GetQueryStringValue("txtName");
            info.STime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSTime"));

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

            var bll = new EyouSoft.BLL.HGysStructure.BWuPin();

            foreach (var item in items)
            {
                if (bll.Delete(SiteUserInfo.CompanyId, item) == 1)
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
