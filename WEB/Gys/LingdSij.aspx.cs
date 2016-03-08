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
using System.Text;

namespace EyouSoft.Web.Gys
{
    public partial class LingdSij : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// privs insert
        /// </summary>
        bool Privs_Insert = false;
        /// <summary>
        /// privs update
        /// </summary>
        bool Privs_Update = false;
        bool Privs_Delete = false;

        EyouSoft.Model.EnumType.GysStructure.GysLeiXing LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.领队;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SL == "43") LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.司机;

            InitPrivs();

            if (UtilsCommons.IsToXls()) ToXls();
            if (Utils.GetQueryStringValue("dotype") == "Delete") Delete();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.领队)
            {
                if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_领队管理_栏目))
                {
                    RCWE(UtilsCommons.AjaxReturnJson("0", "无栏目权限"));
                }

                Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_领队管理_新增);
                Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_领队管理_修改);
                Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_领队管理_删除);
            }

            if (LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.司机)
            {
                if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_司机管理_栏目))
                {
                    RCWE(UtilsCommons.AjaxReturnJson("0", "无栏目权限"));
                }

                Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_司机管理_新增);
                Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_司机管理_修改);
                Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_司机管理_删除);
            }
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

            var items = new EyouSoft.BLL.HGysStructure.BSiJi().GetSiJis(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.HGysStructure.MSiJiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MSiJiChaXunInfo();

            info.LeiXing = LeiXing;
            info.Name = Utils.GetQueryStringValue("txtName");

            return info;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();            
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.HGysStructure.BSiJi().GetSiJis(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();

            s.Append("姓名\t评价\n");

            foreach (var item in items)
            {
                s.Append(item.Name + "\t");
                s.Append(item.PingJiaNeiRong + "\n");
            }            

            ResponseToXls(s.ToString());
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

            var bll = new EyouSoft.BLL.HGysStructure.BSiJi();

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
