using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetCityInfo : IHttpHandler
    {

        EyouSoft.Model.EnumType.SysStructure.LngType LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
        public void ProcessRequest(HttpContext context)
        {
            string q = Utils.GetQueryStringValue("q");
            string companyID = Utils.GetQueryStringValue("companyID");
            context.Response.ContentType = "text/plain";
            StringBuilder sb = new StringBuilder();
            var bll = new EyouSoft.BLL.SysStructure.BGeography();
            if (q != "" && companyID != "")
            {
                int recordCount = 0;
                IList<EyouSoft.Model.SysStructure.MSysDistrict> areaList = bll.GetAreas(1, 10, ref recordCount, companyID, q);
                if (areaList != null && areaList.Count > 0)
                {
                    for (int i = 0; i < areaList.Count; i++)
                    {
                        sb.Append(areaList[i].Name + "|" + areaList[i].DistrictId + "\n");
                    }
                }
                else
                {
                    sb.Append("未匹配到城市!||");
                }
            }
            else
            {
                sb.Append("未匹配到城市!||");
            }
            context.Response.Write(sb.ToString());
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
