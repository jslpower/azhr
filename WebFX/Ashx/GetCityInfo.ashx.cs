using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.WebFX.Ashx
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
            LngType = Utils.GetEnumValue<EyouSoft.Model.EnumType.SysStructure.LngType>(Utils.GetQueryStringValue("LgType"), EyouSoft.Model.EnumType.SysStructure.LngType.中文);
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
                        switch (LngType)
                        {
                            case EyouSoft.Model.EnumType.SysStructure.LngType.中文:
                                sb.Append(areaList[i].Name + "|" + areaList[i].DistrictId + "\n");
                                break;
                            case EyouSoft.Model.EnumType.SysStructure.LngType.英文:
                                sb.Append(areaList[i].EnName + "|" + areaList[i].DistrictId + "\n");
                                break;
                            //case EyouSoft.Model.EnumType.SysStructure.LngType.泰文:
                            //    sb.Append(areaList[i].ThName + "|" + areaList[i].DistrictId + "\n");
                            //    break;
                            default:
                                sb.Append(areaList[i].Name + "|" + areaList[i].DistrictId + "\n");
                                break;
                        }

                    }
                }
                else
                {
                    switch (LngType)
                    {
                        case EyouSoft.Model.EnumType.SysStructure.LngType.英文:
                            sb.Append("未匹配到城市!||");
                            break;
                        //case EyouSoft.Model.EnumType.SysStructure.LngType.泰文:
                        //    sb.Append("ไม่ได้รับการจับคู่กับเมือง||");
                        //    break;
                        default:
                            break;
                    }

                }
            }
            else
            {
                switch (LngType)
                {
                    case EyouSoft.Model.EnumType.SysStructure.LngType.英文:
                        sb.Append("未匹配到城市!||");
                        break;
                    //case EyouSoft.Model.EnumType.SysStructure.LngType.泰文:
                    //    sb.Append("ไม่ได้รับการจับคู่กับเมือง||");
                    //    break;
                    default:
                        break;
                }
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
