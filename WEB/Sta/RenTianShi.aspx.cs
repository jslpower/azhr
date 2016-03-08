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
    public partial class RenTianShi : EyouSoft.Common.Page.BackPage
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
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.统计分析_人天数统计_栏目))
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

            var items = new EyouSoft.BLL.TongJiStructure.BRenTian().GetRenTians(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.TongJiStructure.MRenTianChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.TongJiStructure.MRenTianChaXunInfo();

            info.CountyId = Utils.GetIntNull(Utils.GetQueryStringValue("CountyId"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCityId"));
            info.CountryId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCountryId"));
            info.DeptId = Utils.GetIntNull(Utils.GetQueryStringValue("txtDeptId"));
            info.ETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtETime"));
            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("txtProvinceId"));
            info.STime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSTime"));
            info.WanShu1 = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue("txtWanShu1"));
            info.WanShu2 = Utils.GetIntNull(Utils.GetQueryStringValue("txtWanShu2"));

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

            var items = new EyouSoft.BLL.TongJiStructure.BRenTian().GetRenTians(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun);
            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            StringBuilder s = new StringBuilder();

            s.AppendFormat("国家或地区\t部门\t团号\t人数\t(地域)\t到（地域）时间\t离（地域）日期\t在（地域）住宿（晚）\t人天数\t入住酒店\t入境航班号\t出境航班号\t领队\t导游\n");

            foreach (var item in items)
            {
                s.Append(item.GuoJi + "\t");
                s.Append(item.DeptName + "\t");
                s.Append(item.TourCode + "\t");
                s.Append(item.RJCR + "+" + item.RJET + "+" + item.RJLD + "\t");
                s.Append(item.CityName + "\t");
                s.Append(item.XCSTime.ToString("yyyy-MM-dd") + "\t");
                s.Append(item.XCETime.ToString("yyyy-MM-dd") + "\t");
                s.Append(item.ZhuSuWanShu + "\t");
                s.Append(item.RTS + "\t");
                s.Append(item.JiuDianName + "\t");
                s.Append(item.RHangBan + "\t");
                s.Append(item.CHangBan + "\t");
                s.Append(item.LingDuiName + "\t");
                s.Append(item.DaoYouName + "\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
