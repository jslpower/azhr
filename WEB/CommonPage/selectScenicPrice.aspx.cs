using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.CommonPage
{
    public partial class selectScenicPrice : BackPage
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
            string tourmode = Utils.GetQueryStringValue("tourmode");
            DateTime? Sdate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sdate"));
            DateTime? Edate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("edate"));
            if (!string.IsNullOrEmpty(binketype))
            {
                if (binketype == "中文")
                {
                    searchmodel.BinKeLeiXing = EyouSoft.Model.EnumType.CrmStructure.CustomType.内宾;
                }
                else if (binketype == "外文")
                {
                    searchmodel.BinKeLeiXing = EyouSoft.Model.EnumType.CrmStructure.CustomType.外宾;
                }
                else
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
            }
            if (!string.IsNullOrEmpty(tourmode))
            {
                if (tourmode == "2")
                    searchmodel.TuanXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaTuanXing.散;
                else
                    searchmodel.TuanXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaTuanXing.团;
            }
            searchmodel.Time1 = Sdate;
            searchmodel.Time2 = Edate;
            IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> list = bll.GetJingDianJiaGes(JingDianId, searchmodel);
            if (list != null && list.Count > 0)
            {
                this.RepList.DataSource = list;
                this.RepList.DataBind();
            }
            else
            {
                this.lbemptymsg.Text = "<tr class='old'><td colspan='7' align='center'>没有相关数据</td></tr>";
            }
        }
    }
}
