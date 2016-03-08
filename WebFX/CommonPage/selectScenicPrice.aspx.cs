using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.WebFX.CommonPage
{
    public partial class selectScenicPrice : EyouSoft.Common.Page.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //供应商编号
            string jingdianid = EyouSoft.Common.Utils.GetQueryStringValue("hideID");
            if (!IsPostBack)
            {
                PageInit(jingdianid);
            }
        }

        private void PageInit(string JingDianId)
        {

            EyouSoft.BLL.HGysStructure.BJiaGe bll = new EyouSoft.BLL.HGysStructure.BJiaGe();
            EyouSoft.Model.HGysStructure.MJiaGeChaXunInfo searchmodel = new EyouSoft.Model.HGysStructure.MJiaGeChaXunInfo();
            string binketype = Utils.GetQueryStringValue("binkemode");

            if (!string.IsNullOrEmpty(binketype))
            {
                EyouSoft.BLL.SysStructure.BGeography bllsys = new EyouSoft.BLL.SysStructure.BGeography();
                EyouSoft.Model.SysStructure.MGeography model = bllsys.GetCountry(SiteUserInfo.CompanyId, Utils.GetInt(binketype));
                if (model != null)
                {
                    if (model.Name == "中国")
                        searchmodel.BinKeLeiXing = EyouSoft.Model.EnumType.CrmStructure.CustomType.内宾;
                    else
                    {
                        searchmodel.BinKeLeiXing = EyouSoft.Model.EnumType.CrmStructure.CustomType.外宾;
                    }
                }
                else
                {
                    searchmodel.BinKeLeiXing = EyouSoft.Model.EnumType.CrmStructure.CustomType.内宾;
                }
            }
            IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> list = bll.GetJingDianJiaGes(JingDianId, searchmodel);
            if (list != null && list.Count > 0)
            {
                this.RepList.DataSource = list;
                this.RepList.DataBind();
            }
            else
            {
                this.lbemptymsg.Text = "<tr class='old'><td colspan='7' align='center'>" + (String)GetGlobalResourceObject("string", "暂无数据") + "</td></tr>";
            }
        }

        protected string GetDateTime(object obj)
        {
            string str = string.Empty;
            DateTime dt = (DateTime)obj;
            if (dt != null)
            {
                str = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
            }
            return str;
        }
    }
}
