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

namespace EyouSoft.Web.Sta
{
    public partial class RenShu : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            if (UtilsCommons.IsToXls()) ToXls();
            InitDept();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.统计分析_人数统计表_栏目))
            {
                RCWE("没有权限");
            }
        }

        /// <summary>
        /// init dept
        /// </summary>
        void InitDept()
        {
            var items = new EyouSoft.BLL.ComStructure.BComDepartment().GetList(SiteUserInfo.CompanyId);

            StringBuilder s = new StringBuilder();

            s.AppendFormat("<option value='{0}'>{1}</option>", string.Empty, "请选择");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("<option value='{0}'>{1}</option>", item.DepartId, item.DepartName);
                }
            }

            ltrDept.Text = s.ToString();
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
            var items = new EyouSoft.BLL.TongJiStructure.BRenShu().GetRenShus(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.TongJiStructure.MRenShuChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.TongJiStructure.MRenShuChaXunInfo();

            info.ETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtETime"));
            info.STime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSTime"));
            info.DeptId = Utils.GetIntNull(Utils.GetQueryStringValue("txtDeptId"));

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
            var items = new EyouSoft.BLL.TongJiStructure.BRenShu().GetRenShus(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            s.Append("部门\t团队名称\t国籍\t客户单位\t接待日期\t人数\t来杭人数\t来杭天数\t来杭人天数\t来浙人数\t来浙天数\t来浙人天数\t来华人数\t来华天数\t来华人天数\t备注\n");
            foreach (var item in items)
            {
                s.AppendFormat(item.DeptName + "\t");
                s.AppendFormat(item.RouteName + "\t");
                s.AppendFormat(item.GuoJi + "\t");
                s.AppendFormat(item.KeHuName + "\t");
                s.AppendFormat(item.LDate.ToString("yyyy-MM-dd") + "-" + item.RDate.ToString("yyyy-MM-dd") + "\t");
                s.AppendFormat(item.RJRS + "\t");
                s.AppendFormat(item.HZRS + "\t");
                s.AppendFormat(item.HZTS + "\t");
                s.AppendFormat(item.HZRTS + "\t");
                s.AppendFormat(item.ZJRS + "\t");
                s.AppendFormat(item.ZJTS + "\t");
                s.AppendFormat(item.ZJRTS + "\t");
                s.AppendFormat(item.RJRS + "\t");
                s.AppendFormat(item.TS + "\t");
                s.AppendFormat(item.RTS + "\t");
                s.AppendFormat("" + "\n");

            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
