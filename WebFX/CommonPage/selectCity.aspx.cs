using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.WebFX.CommonPage
{
    public partial class selectCity : EyouSoft.Common.Page.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string areasid = Utils.GetQueryStringValue("citysid").Trim(',');
            string type = Utils.GetQueryStringValue("isxuanyong");

            if (type == "1")
            {
                if (!string.IsNullOrEmpty(areasid))
                {
                    Response.Clear();
                    Response.Write(GetGouWu(areasid));
                    Response.End();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(areasid))
                {
                    Response.Clear();
                    Response.Write(GetGouWu(areasid));
                    Response.End();
                }
            }

        }
        /// <summary>
        /// 获取该县区下面的购物店
        /// </summary>
        /// <param name="areasid"></param>
        /// <returns></returns>
        private string GetGouWu(string areasid)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            EyouSoft.BLL.HGysStructure.BGys bll = new EyouSoft.BLL.HGysStructure.BGys();

            IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> gouwulist = bll.GetXuanYongGouWuDians(SiteUserInfo.CompanyId, Utils.GetIntArray(areasid, ","));

            string msg = string.Empty;
            if (gouwulist.Count > 0)
            {
                msg = UtilsCommons.AjaxReturnJson("1", str.ToString(), gouwulist);
            }
            else
                msg = UtilsCommons.AjaxReturnJson("0", "");

            return msg;
        }
    }
}
