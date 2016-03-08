using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.SysStructure;
using EyouSoft.BLL.SysStructure;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Sys
{
    public partial class ChengShiGuanLi : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] getID = Utils.GetIntArray(Utils.GetQueryStringValue("id"), ",");
            int[] getPID = Utils.GetIntArray(Utils.GetQueryStringValue("pid"), ",");
            int[] getDID = Utils.GetIntArray(Utils.GetQueryStringValue("did"), ",");
            string dotype = Utils.GetQueryStringValue("dotype");
            if (!string.IsNullOrEmpty(dotype) && dotype == "del")
            {
                StringBuilder strbu = new StringBuilder("");
                int cresult = 0, presult = 0, dresult = 0;
                if (getDID != null) dresult = delDistrict(getDID);
                if (getID != null) cresult = delCitys(getID);
                if (getPID != null) presult = delProvince(getPID);
                if (dresult != 0)
                {

                    if (dresult == 1) strbu.Append("县区删除成功  ");
                    else strbu.Append("县区删除失败  ");

                }

                if (cresult != 0)
                {
                    if (cresult == getID.Length) strbu.Append("城市删除成功  ");
                    if (cresult != getID.Length) strbu.Append("城市下有区县的未删除   ");
                }
                if (presult != 0)
                {
                    if (presult == getPID.Length) strbu.Append("省份删除成功  ");
                    if (presult != getPID.Length) strbu.Append("省份下有城市的未删除  ");

                }

                RCWE(UtilsCommons.AjaxReturnJson("1", strbu.ToString()));

            }
        }
        #region 删除城市
        protected int delCitys(int[] idArr)
        {
            int result = 0;
            BGeography bll = new BGeography();
            if (idArr.Length > 0)
            {
                for (int i = 0; i < idArr.Length; i++)
                {
                    result += bll.DeleteCity(SiteUserInfo.CompanyId, idArr[i]);
                }
            }
            return result;
            //

        }
        #endregion

        #region 删除省份
        protected int delProvince(int[] idArr)
        {
            int result = 0;
            BGeography bll = new BGeography();

            if (idArr.Length > 0)
            {
                for (int i = 0; i < idArr.Length; i++)
                {
                    result += bll.DeleteProvince(SiteUserInfo.CompanyId, idArr[i]);
                }
            }
            return result;
            // RCWE(UtilsCommons.AjaxReturnJson(result.ToString(), result != idArr.Length ? "该省份下有城市，删除失败" : "删除成功"));

        }
        #endregion

        #region 删除县区
        protected int delDistrict(int[] idArr)
        {
            int result = 0;
            BGeography bll = new BGeography();
            result = bll.DeleteDistrict(SiteUserInfo.CompanyId, idArr);
            return result;

        }
        #endregion
        #region 返回表格HTML
        protected string initList()
        {
            StringBuilder strCountry = new StringBuilder("");
            BGeography bll = new BGeography();
            IList<MSysCountry> list = bll.GetCountrys(SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    strCountry.AppendFormat("<div class=\" tablehead\" style=\"text-align: center; font-weight: bold; color: #00477F\"><span>{0}</span></div>{1}", list[i].Name, getProvinces(list[i].CountryId));
                }
            }
            return strCountry.ToString();

        }
        /// <summary>
        /// 返回省份html
        /// </summary>
        /// <param name="countryID">国家编号</param>
        /// <returns></returns>
        protected string getProvinces(int countryID)
        {
            StringBuilder strProvince = new StringBuilder("");
            BGeography bll = new BGeography();
            IList<MSysProvince> list = bll.GetProvinces(SiteUserInfo.CompanyId, countryID);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    strProvince.AppendFormat(" <div class=\"jichu_city\"> <span class=\"formtableT03\"><input type=\"checkbox\" value=\"{2}\"  name=\"chk_pid\">&nbsp;{0}</span>{1}</div>", list[i].Name, getCitys(list[i].ProvinceId), list[i].ProvinceId);
                }
            }
            return strProvince.ToString();
        }
        /// <summary>
        /// 返回城市html
        /// </summary>
        /// <param name="provinceID">省份编号</param>
        /// <returns></returns>
        protected string getCitys(int provinceID)
        {
            StringBuilder strCity = new StringBuilder("");
            BGeography bll = new BGeography();
            IList<MSysCity> list = bll.GetCitys(SiteUserInfo.CompanyId, provinceID);
            if (list != null && list.Count > 0)
            {
                strCity.AppendFormat("<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tbody>");
                for (int i = 0; i < list.Count; i++)
                {
                    strCity.AppendFormat("<tr><th   bgcolor=\"#FFFFFF\" align=\"left\"><label><input type=\"checkbox\" value=\"{0}\" id=\"{0}\" name=\"chk_id\"> {1}</label></th><td   bgcolor=\"#FFFFFF\" align=\"left\">{2}</td></tr>", list[i].CityId, list[i].Name, getDistris(list[i].CityId));


                }
                strCity.AppendFormat("</tbody></table>");

            }
            return strCity.ToString();
        }
        /// <summary>
        /// 返回县区html
        /// </summary>
        /// <param name="provinceID">省份编号</param>
        /// <returns></returns>
        protected string getDistris(int cityID)
        {
            StringBuilder strDistris = new StringBuilder("");
            BGeography bll = new BGeography();
            IList<MSysDistrict> list = bll.GetDistricts(SiteUserInfo.CompanyId, cityID);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    strDistris.AppendFormat("<label><input type=\"checkbox\" value=\"{0}\" id=\"{0}\" name=\"chk_did\"> {1}&nbsp;</label>", list[i].DistrictId, list[i].Name);


                }
            }
            return strDistris.ToString();
        }

        #endregion

    }
}
