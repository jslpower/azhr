using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.HGysStructure;
using EyouSoft.Model.HGysStructure;
using System.Text;

namespace EyouSoft.WebFX.CommonPage.AjaxRequest
{
    public partial class AjaxSupplier : EyouSoft.Common.Page.FrontPage
    {
        protected int pageSize = 24;
        protected int pageIndex = 0;
        protected int recordCount = 0;
        protected int listCount = 0;
        protected string NodataMsg = string.Empty;
        private string CompanyId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyId = this.SiteUserInfo.CompanyId;
            if (!IsPostBack)
            {
                //国家
                string country = Utils.GetQueryStringValue("country");
                //省份
                string Provice = Utils.GetQueryStringValue("provice");
                //城市
                string City = Utils.GetQueryStringValue("city");
                //县区
                string Area = Utils.GetQueryStringValue("area");
                //名称
                string Name = Utils.GetQueryStringValue("name");
                ListDataInit(country, Provice, City, Area, Name);
            }
        }
        #region 初始化列表
        /// <summary>
        /// 列表数据初始化
        /// </summary>
        /// <param name="searchModel"></param>
        private void ListDataInit(string country, string provice, string city, string area, string name)
        {
            //供应商BLL
            BGys bsource = new BGys();
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //供应商选用类别
            string type = Utils.GetQueryStringValue("type");
            EyouSoft.Model.HGysStructure.MGysInfo msource = new EyouSoft.Model.HGysStructure.MGysInfo();
            EyouSoft.BLL.HGysStructure.BGys Bll = new BGys();
            MXuanYongChaXunInfo searchmodel = new MXuanYongChaXunInfo();
            if (Utils.GetInt(country) > 0)
            {
                searchmodel.CountryId = Utils.GetInt(country);
            }
            if (Utils.GetInt(provice) > 0)
                searchmodel.ProvinceId = Utils.GetInt(provice);
            if (Utils.GetInt(area) > 0)
                searchmodel.DistrictId = Utils.GetInt(area);
            if (Utils.GetInt(city) > 0)
                searchmodel.CityId = Utils.GetInt(city);
            //国家ID
            var countryId = Utils.GetInt(Utils.GetQueryStringValue("country"));
            if (countryId > 0)
            {
                searchmodel.CountryId = countryId;
            }
            searchmodel.GysName = name;

            IList<MXuanYongInfo> list = null;
            searchmodel.LeiXing = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)Utils.GetInt(type);
            searchmodel.IsLxr = true;

            if (searchmodel.LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.景点 || searchmodel.LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店)
            {
                searchmodel.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(Utils.GetQueryStringValue("LgType"));
                searchmodel.JiuDianXingJi = Utils.GetIntNull(Utils.GetQueryStringValue("jiudianxingji"));
            }
            list = Bll.GetXuanYongs(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searchmodel);
            if (list != null && list.Count > 0)
            {
                listCount = list.Count;
                RepList.DataSource = list;
                RepList.DataBind();
            }
            else
            {
                NodataMsg = "<tr class='old'><td colspan='11' align='center'>" + (String)GetGlobalResourceObject("string", "暂无数据") + "</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
            BindPage();
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        #endregion
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="linkManlist"></param>
        /// <returns></returns>
        protected string GetContactInfo(object linkManlist, string type, string sid, string sName)
        {
            IList<MLxrInfo> list = (IList<MLxrInfo>)linkManlist;
            StringBuilder stb = new System.Text.StringBuilder();
            switch (type)
            {
                case "name":
                    if (list != null && list.Count > 0)
                    {
                        stb.Append(list[0].Name);
                    }
                    break;
                case "tel":
                    if (list != null && list.Count > 0)
                    {
                        stb.Append(string.IsNullOrEmpty(list[0].Telephone) ? list[0].Mobile : list[0].Telephone);
                    }
                    break;
                case "fax":
                    if (list != null && list.Count > 0)
                    {
                        stb.Append(list[0].Fax);
                    }
                    break;
                case "mobile":
                    if (list != null && list.Count > 0)
                    {
                        stb.Append(list[0].Mobile);
                    }
                    break;
                case "list":
                    if (list != null && list.Count > 0)
                    {
                        stb.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' width='7%' align='center'></th><th width='19%' align='center'>联系人</th><th align='center' width='20%'>电话</th><th align='center'>手机</th><th align='center' width='18%'>传真</th></tr>");
                        for (int i = 0; i < list.Count; i++)
                        {
                            stb.Append("<tr><td align='center' width='7%'><input onclick='useSupplierPage.RadioClickFun(this);' type='radio' value='" + sid + "," + sName + "," + list[i].Name + "," + list[i].Telephone + "," + list[i].Fax + "' /></td><td  width='19%' align='center'>" + list[i].Name + "</td><td align='center' width='20%'>" + list[i].Telephone + "</td><td align='center' width='18%'>" + list[i].Mobile + "</td><td align='center' width='19%'>" + list[i].Fax + "</td></tr>");
                        }
                        stb.Append("</table>");
                    }
                    break;
            }
            return stb.ToString();
        }

        protected string GetContactInfo(object linkManlist, string type)
        {
            return GetContactInfo(linkManlist, type, "", "");
        }

        /// <summary>
        /// 获取酒店旺季价格
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetHotelPrice(object obj)
        {
            var s = new StringBuilder();
            if (string.IsNullOrEmpty(obj.ToString())) return string.Empty;
            var info = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(obj.ToString());
            if (info != null && info.JiuDian != null && info.JiuDian.BaoJias != null && info.JiuDian.BaoJias.Count > 0)
            {
                s.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th align='center'>用房类型</th><th align='center'>价格类型</th><th align='center'>平季价格</th><th align='center'>价格有效</th><th align='center'>淡季价格</th><th align='center'>价格有效</th><th align='center'>旺季价格</th><th align='center'>价格有效</th></tr>");
                foreach (var m in info.JiuDian.BaoJias)
                {
                    s.AppendFormat("<tr><td align='center' >{4}</td><td align='center' >{5}</td><td align='center' ><input name='{3}' type='radio' value='{0}' onclick='SetJiuDianBaoJia(this)'/>{0}</td><td align='center' >{6}</td><td align='center'><input name='{3}' type='radio' value='{1}' onclick='SetJiuDianBaoJia(this)'/>{1}</td><td align='center' >{7}</td><td align='center'><input name='{3}' type='radio' value='{2}' onclick='SetJiuDianBaoJia(this)'/>{2}</td><td align='center' >{8}</td></tr>", m.PJiaGe.ToString("F2"), m.DJiaGe.ToString("F2"), m.WJiaGe.ToString("F2"), info.GysId, m.BinKeLeiXing, m.TuanXing, m.PBeiZhu, m.DBeiZhu, m.WBeiZhu);
                }
                s.Append("</table>");
            }
            return s.ToString();
        }
    }
}
