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
    public partial class JiuDian : EyouSoft.Common.Page.BackPage
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (UtilsCommons.IsToXls()) ToXls();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "无栏目权限"));
            }

            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_修改);
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

            var items = new EyouSoft.BLL.HGysStructure.BGys().GetGyss(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.HGysStructure.MLBChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MLBChaXunInfo();

            info.LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店;
            info.CountryId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCountryId"));
            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("txtProvinceId"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCityId"));
            info.GysName = Utils.GetQueryStringValue("txtName");
            info.JiuDianXingJi = (EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi), Utils.GetQueryStringValue("txtXingJi"));

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
            var items = new EyouSoft.BLL.HGysStructure.BGys().GetGyss(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            s.Append("所在地\t酒店名称\t星级\t前台电话\t联系人\t交易次数\t间数\t结算金额\t未付金额\n");
            foreach (var item in items)
            {
                s.Append(item.CPCD.ProvinceName + "-" + item.CPCD.CityName + "\t");
                s.Append(item.GysName + "\t");
                s.Append(item.JiuDianXingJi + "\t");
                s.Append(item.JiuDianQianTaiTelephone + "\t");
                s.Append(item.LxrName + "\t");
                s.Append(item.JiaoYiXX.JiaoYiCiShu + "\t");
                s.Append(item.JiaoYiXX.JiaoYiShuLiang + "\t");
                s.Append(item.JiaoYiXX.JieSuanJinE + "\t");
                s.Append(item.JiaoYiXX.WeiZhiFuJinE + "\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
