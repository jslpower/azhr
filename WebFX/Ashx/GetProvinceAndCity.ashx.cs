using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.WebFX.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    public class GetProvinceAndCity : IHttpHandler
    {
        EyouSoft.Model.EnumType.SysStructure.LngType LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string getType = Utils.GetQueryStringValue("get");

            StringBuilder sb = new StringBuilder();

            int gID = Utils.GetInt(Utils.GetQueryStringValue("gid"));
            int pID = Utils.GetInt(Utils.GetQueryStringValue("pid"));
            int cID = Utils.GetInt(Utils.GetQueryStringValue("cid"));
            int xID = Utils.GetInt(Utils.GetQueryStringValue("xid"));

            LngType = Utils.GetEnumValue<EyouSoft.Model.EnumType.SysStructure.LngType>(Utils.GetQueryStringValue("lng"), EyouSoft.Model.EnumType.SysStructure.LngType.中文);

            var domain = EyouSoft.Security.Membership.UserProvider.GetDomain();


            var bll = new EyouSoft.BLL.SysStructure.BGeography();

            switch (getType)
            {

                case "g":

                    IList<EyouSoft.Model.SysStructure.MSysCountry> gList = new EyouSoft.BLL.SysStructure.BGeography().GetCountrys(domain.CompanyId);
                    if (gList != null && gList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < gList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + gList[i].CountryId.ToString() + "\",\"name\":\"" + GetName(gList[i]) + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }
                    break;
                case "p":
                    IList<EyouSoft.Model.SysStructure.MSysProvince> pList = bll.GetProvinces(domain.CompanyId, gID);
                    if (pList != null && pList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < pList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + pList[i].ProvinceId.ToString() + "\",\"name\":\"" + GetName(pList[i]) + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }

                    break;
                case "c":
                    IList<EyouSoft.Model.SysStructure.MSysCity> cList = bll.GetCitys(domain.CompanyId, pID);
                    if (cList != null && cList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < cList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + cList[i].CityId.ToString() + "\",\"name\":\"" + GetName(cList[i]) + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }
                    break;
                case "x":
                    IList<EyouSoft.Model.SysStructure.MSysDistrict> xList = bll.GetDistricts(domain.CompanyId, cID);
                    if (xList != null && xList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < xList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + xList[i].DistrictId.ToString() + "\",\"name\":\"" + GetName(xList[i]) + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }
                    break;
            }

            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string GetName(EyouSoft.Model.SysStructure.MGeography info)
        {
            if (info == null) return string.Empty;

            string s = info.Name;
            switch (LngType)
            {
                case EyouSoft.Model.EnumType.SysStructure.LngType.英文: s = info.EnName; break;
                //case EyouSoft.Model.EnumType.SysStructure.LngType.泰文: s = info.ThName; break;
                default: break;
            }

            return s;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
