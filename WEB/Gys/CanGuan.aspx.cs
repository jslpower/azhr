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
using System.Collections.Generic;

namespace EyouSoft.Web.Gys
{
    /// <summary>
    /// 餐馆列表
    /// </summary>
    public partial class CanGuan : EyouSoft.Common.Page.BackPage
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
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "无栏目权限"));
            }

            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_修改);
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

            info.LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.餐馆;
            info.CountryId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCountryId"));
            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("txtProvinceId"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCityId"));
            info.GysName = Utils.GetQueryStringValue("txtName");
            info.CanGuanCaiXi = Utils.GetQueryStringValue("txtCaiXi");

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
            s.Append("所在地\t餐厅名称\t菜系\t联系人\t餐标区间\t交易次数\t交易人数\t结算金额\t未付金额\n");
            foreach (var item in items)
            {
                s.Append(item.CPCD.ProvinceName + "-" + item.CPCD.CityName + "\t");
                s.Append(item.GysName + "\t");
                s.Append(item.CanGuanCaiXi + "\t");
                s.Append(item.LxrName + "\t");
                s.Append(item.CanGuanCanBiao + "\t");
                s.Append(item.JiaoYiXX.JiaoYiCiShu + "\t");
                s.Append(item.JiaoYiXX.JiaoYiShuLiang + "\t");
                s.Append(item.JiaoYiXX.JieSuanJinE + "\t");
                s.Append(item.JiaoYiXX.WeiZhiFuJinE + "\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }

    /// <summary>
    /// 供应商公用
    /// </summary>
    public class GY
    {
        /// <summary>
        /// 获取推荐、签章小图标
        /// </summary>
        /// <param name="isTuiJian"></param>
        /// <param name="isQianDan"></param>
        /// <returns></returns>
        public static string GetTuiJianHtml(object isTuiJian, object isQianDan)
        {
            if (isTuiJian == null || isQianDan == null) return string.Empty;

            bool _isTuiJian = (bool)isTuiJian;
            bool _isQianDan = (bool)isQianDan;

            string s = string.Empty;

            if (_isTuiJian) s += "<img src=\"/images/jian.gif\" width=\"13px\" height=\"13px\" title=\"推荐\"/> ";
            if (_isQianDan) s += " <img src=\"/images/qian.gif\" width=\"13px\" height=\"13px\" title=\"签单\"/>";

            return s;
        }

        /// <summary>
        /// 获取供应商联系人浮动信息HTML
        /// </summary>
        /// <param name="lxrs"></param>
        /// <returns></returns>
        public static string GetLxrHtml(object lxrs)
        {
            if (lxrs == null) return string.Empty;
            IList<EyouSoft.Model.HGysStructure.MLxrInfo> items = (IList<EyouSoft.Model.HGysStructure.MLxrInfo>)lxrs;

            StringBuilder s = new StringBuilder();
            s.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>编号</th><th align='center'>联系人</th><th align='center'>电话</th><th align='center'>手机</th><th align='center'>传真</th><th align='center'>QQ</th><th align='center'>MSN-SKYPE</th><th align='center'>E-mail</th></tr>");
            if (items != null && items.Count > 0)
            {
                int i = 1;
                foreach (var item in items)
                {
                    s.Append("<tr><td align='center'>" + i.ToString() + "</td><td align='center'>" + item.Name + "</td><td align='center' >" + item.Telephone + "</td><td align='center'>" + item.Mobile + "</td><td align='center'>" + item.Fax + "</td><td align='center'>" + item.QQ + "</td><td align='center'>" + item.MSN + "</td><td align='center'>" + item.Email + "</td></tr>");
                    i++;
                }
            }
            s.Append("</table>");

            return s.ToString();
        }
    }
}
