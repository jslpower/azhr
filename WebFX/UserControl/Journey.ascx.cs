using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Common;

namespace EyouSoft.WebFX.UserControl
{
    public partial class Journey : System.Web.UI.UserControl
    {
        private IList<EyouSoft.Model.HTourStructure.MQuotePlan> _setPlanList;
        public IList<EyouSoft.Model.HTourStructure.MQuotePlan> SetPlanList
        {
            get { return _setPlanList; }
            set { _setPlanList = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SetPlanList != null && SetPlanList.Count > 0)
            {
                this.rptJourney.DataSource = SetPlanList.OrderBy(p => p.Days);
                this.rptJourney.DataBind();
            }
        }
        private string _companyId;
        public string CompanyID
        {
            get { return _companyId; }
            set { _companyId = value; }
        }

        /// <summary>
        /// 获取当天购物点
        /// </summary>
        /// <param name="objshop">购物点</param>
        /// <param name="objcity">当天城市</param>
        /// <returns></returns>
        protected string getshop(object objshop, object objcity)
        {
            //当天选择的购物点
            IList<MQuoteShop> shop = (IList<MQuoteShop>)objshop;
            //当天经过的城市
            IList<MQuotePlanCity> city = (IList<MQuotePlanCity>)objcity;
            IList<string> listshop = new List<string>();
            string citysid = string.Empty;
            if (city != null)
            {
                for (int i = 0; i < city.Count; i++)
                {
                    if (i == city.Count - 1)
                        citysid += city[i].CityId.ToString();
                    else
                        citysid += city[i].CityId.ToString() + ",";
                }
            }
            EyouSoft.BLL.HGysStructure.BGys bll = new EyouSoft.BLL.HGysStructure.BGys();
            //该城市下所以的购物点
            IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> allshop = null;
            if (citysid != "0")
            {
                allshop = bll.GetXuanYongGouWuDians(CompanyID, Utils.GetIntArray(citysid, ","));
            }
            if (shop != null)
            {
                foreach (var model in shop)
                {
                    if (model.ShopId != "")
                        listshop.Add(model.ShopId.ToString());
                }
            }
            int count = 0;
            if (allshop != null)
            {
                string html = string.Empty;
                string val = string.Empty;
                string text = string.Empty;
                foreach (var item in allshop)
                {
                    if (listshop != null)
                    {
                        if (listshop.Contains(item.GysId.ToString()))
                        {
                            val += item.GysId + ",";
                            text += item.GysName + ",";
                            html += "<span id='" + item.GysId + "' class='planshopspan'><input type='checkbox' checked='checked' onclick='Journey.SetShopValue(this)' name='ckgouwuid' data-name='" + item.GysName + "' value='" + item.GysId + "' />" + item.GysName + "</span>";

                        }
                        else
                            html += "<span id='" + item.GysId + "' class='planshopspan'><input type='checkbox' onclick='Journey.SetShopValue(this)' name='ckgouwuid' data-name='" + item.GysName + "' value='" + item.GysId + "' />" + item.GysName + "</span>";
                    }
                    if (count % 6 == 0 && count > 0)
                    {
                        html += ("<br />");
                    }
                    count++;
                }
                if (val.Length >= 1 && text.Length >= 1)
                {
                    val = val.Substring(0, val.Length - 1);
                    text = text.Substring(0, text.Length - 1);
                }
                return "<input type='hidden' class='shopid' name='hidshopid' value='" + val + "' /><input type='hidden' class='shopname' name='hidshopname' value='" + text + "' />" + html;
            }
            return "<input type='hidden' class='shopid' name='hidshopid' value='' /><input type='hidden' class='shopname' name='hidshopname' value='' />";
        }
        /// <summary>
        /// 初始化交通方式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetTraffic(object obj)
        {
            string trafficstr = string.Empty;
            PlanProject type = (PlanProject)obj;
            switch (type)
            {
                case PlanProject.飞机:
                    trafficstr = "<a data-value='" + ((int)type).ToString() + "' href='javascript:;'><span><img class='lag flagvisibility' src='/images/jt_feiji.png' alt=''></span></a>";
                    break;
                case PlanProject.火车:
                    trafficstr = "<a data-value='" + ((int)type).ToString() + "' href='javascript:;'><span><img class='lag flagvisibility' src='/images/jt_huoche.png' alt=''></span></a>";
                    break;
                case PlanProject.汽车:
                    trafficstr = "<a data-value='" + ((int)type).ToString() + "' href='javascript:;'><span><img class='lag flagvisibility' src='/images/jt_qiche.png' alt=''></span></a>";
                    break;
                case PlanProject.轮船:
                    trafficstr = "<a data-value='" + ((int)type).ToString() + "' href='javascript:;'><span><img class='lag flagvisibility' src='/images/jt_youlun.png' alt=''></span></a>";
                    break;
                default:
                    trafficstr = "<a href='javascript:;' data-value='-1'><span>请选择</span></a>";
                    break;
            }
            return trafficstr;
        }

        /// <summary>
        /// 通过景区的数据返回html
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetSinceByList(object o)
        {
            if (o != null)
            {
                //获得景点集合
                IList<MQuotePlanSpot> list = (IList<MQuotePlanSpot>)o;
                if (list.Count > 0 && list[0].SpotId.ToString().Trim() != "")
                {
                    string val = string.Empty;
                    string text = string.Empty;
                    string html = string.Empty;
                    string pricejs = string.Empty;
                    string priceth = string.Empty;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i == 0)
                        {
                            pricejs += list[i].SettlementPrice.ToString("f2");
                            priceth += list[i].Price.ToString("f2");
                        }
                        else
                        {
                            pricejs += "," + list[i].SettlementPrice.ToString("f2");
                            priceth += "," + list[i].Price.ToString("f2");
                        }
                        val += list[i].SpotId + ",";
                        text += Server.UrlEncode(list[i].SpotName) + ",";
                        html += "<span id='" + list[i].SpotId + "' data-istuijian='" + GetScenicInfo(list[i].SpotId, "tuijian") + "' data-fujian='" + GetScenicInfo(list[i].SpotId, "path") + "' class='upload_filename'><a data-class='a_Journey_Since' data-pricejs='" + list[i].SettlementPrice.ToString("f2") + "' data-priceth='" + list[i].Price.ToString("f2") + "' data-for='" + list[i].SpotId + "'> " + list[i].SpotName + " </a> <a data-for='" + list[i].SpotId + "' href='javascript:void(0);' onclick='Journey.RemoveSince(this)'><img src='/images/cha.gif'></a></span>";
                    }
                    val = val.Substring(0, val.Length - 1);
                    text = text.Substring(0, text.Length - 1);
                    return "<input type='hidden' name='hd_scenery_spot' value='" + val + "' /><input type='hidden' name='show_scenery_spot' value='" + text + "' /> <input type='hidden' name='hidpriceth' value='" + priceth + "' /><input type='hidden' name='hidpricejs' value='" + pricejs + "' /><span data-class='fontblue' class='fontblue'>" + html + "</span>";
                }
            }
            return "<input type='hidden' name='hd_scenery_spot' value='' /><input type='hidden' name='show_scenery_spot' value='' /><input type='hidden' name='hidpriceth' value='' /><input type='hidden' name='hidpricejs' value='' /><span data-class='fontblue' class='fontblue'></span>";
        }



        /// <summary>
        /// 获取景点信息(是否推荐、景点图片)
        /// </summary>
        /// <param name="scenicid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetScenicInfo(string scenicid, string type)
        {
            string scenicinfo = string.Empty;
            if (!string.IsNullOrEmpty(scenicid))
            {
                EyouSoft.BLL.HGysStructure.BGys bll = new EyouSoft.BLL.HGysStructure.BGys();
                EyouSoft.Model.HGysStructure.MJingDianJingDianInfo model = bll.GetJingDianInfo(scenicid);
                if (model != null)
                {
                    if (type == "path")
                    {
                        if (model.FuJian != null)
                        {
                            scenicinfo = model.FuJian.FilePath;
                        }
                    }
                    if (type == "tuijian")
                    {
                        scenicinfo = Convert.ToInt32(model.IsTuiJian).ToString();
                    }
                }
            }
            return scenicinfo;
        }

        protected void rptXingCheng_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex == -1) return;

            var rpt = (Repeater)e.Item.FindControl("rptCityAndTraffic");

            if (rpt == null) return;

            var data = (EyouSoft.Model.HTourStructure.MQuotePlan)e.Item.DataItem;

            if (data != null && data.QuotePlanCityList != null)
            {
                rpt.DataSource = data.QuotePlanCityList;
                rpt.DataBind();
            }

        }

    }
}