using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;
using System.Linq;

namespace EyouSoft.WebFX
{
    public partial class AcceptPlan : FrontPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int pageIndex = 1;
        /// <summary>
        /// 总条数
        /// </summary>
        private int recordCount = 0;

        /// <summary>
        /// 是否模版团
        /// </summary>
        protected bool IsParent = false;

        /// <summary>
        /// rowspan
        /// </summary>
        protected int RowSpan = 1;

        #endregion

        protected string PrintPageSp = string.Empty;
        //短线是否可用
        protected bool ShorLineEnable = false;
        //判断是否有更改信息 
        protected int countMSG = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.BLL.ComStructure.BComSetting comSettingBll = new EyouSoft.BLL.ComStructure.BComSetting();
            PrintPageSp = comSettingBll.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台散拼线路行程单);
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "Area":
                        Response.Write(GetArea());
                        Response.End();
                        break;
                }
            }

            if (!IsPostBack)
            {
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                this.HeadDistributorControl1.IsPubLogin =
   System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == this.SiteUserInfo.Username;
                //绑定数据、分页
                BindSource();
            }
            
        }




        /// <summary>
        /// 绑定数据、分页控件
        /// </summary>
        public void BindSource()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            string levelId = SiteUserInfo.TourCompanyInfo.LevelId.ToString();
            var keyword = Utils.GetQueryStringValue("keyword");
            var parentid = Utils.GetQueryStringValue("parentid");

            MTourSaleSearch search = new MTourSaleSearch();
            search.AreaId = Utils.GetInt(Utils.GetQueryStringValue("AreaId"));
            search.RouteName = Utils.GetQueryStringValue("txtRouteName");
            search.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginLDate"));
            search.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndLDate"));
            if (!string.IsNullOrEmpty(parentid))
            {
                search.IsChuTuan = false;
                search.ParentId = parentid;
                search.TourType = TourType.散拼产品;
                this.IsParent = false;
                this.RowSpan = 2;
            }
            else
            {
                search.TourType = TourType.散拼模版团;
                this.IsParent = true;
                this.RowSpan = 1;
            }
            search.Keyword = keyword;

            BTour tour = new BTour();
            IList<MTourSanPinInfo> list = tour.GetTourSaleList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, search, levelId);
            this.RtPlan.DataSource = list;
            this.RtPlan.DataBind();

            //绑定线路区域
            EyouSoft.BLL.ComStructure.BComArea bArea = new EyouSoft.BLL.ComStructure.BComArea();
            IList<EyouSoft.Model.ComStructure.MComArea> areaList = bArea.GetAreaByCID(SiteUserInfo.CompanyId);
            this.rptkeyword.DataSource = areaList.Select(m => new { m.Keyword }).Distinct();
            this.rptkeyword.DataBind();

            this.rpArea.DataSource = areaList.Where(m => m.Keyword == keyword).ToList();
            this.rpArea.DataBind();

            BindPage();

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            if (recordCount == 0)
            {
                this.PhPage.Visible = false;
                this.litMsg.Visible = true;
            }
            else
            {
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
        }

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <returns></returns>
        public string GetArea()
        {
            string area = string.Empty;
            EyouSoft.BLL.ComStructure.BComArea bArea = new EyouSoft.BLL.ComStructure.BComArea();
            IList<EyouSoft.Model.ComStructure.MComArea> list = bArea.GetAreaByCID(SiteUserInfo.CompanyId);
            if (list != null)
            {
                if (list.Count != 0)
                {
                    area = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                }
            }
            return area;
        }


        /// <summary>
        /// 根据计调集合获取计调员信息（用于Repeater行绑定计调员）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetPlaner(IList<MTourPlaner> list)
        {
            string planer = string.Empty;
            if (list != null)
            {
                if (list.Count != 0)
                {
                    foreach (MTourPlaner plan in list)
                    {
                        planer += plan.Planer + ",";
                    }
                    planer = planer.Substring(0, planer.Length - 1);
                }
            }

            return planer;

        }

        /// <summary>
        /// 订单信息汇总表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetPrintPage(string tourId, string type)
        {
            string url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台订单信息汇总单);
            url = url + "?tourId=" + tourId + "&type=" + type;
            return url;

        }

        /// <summary>
        /// 根据变更信息显示
        /// </summary>
        /// <param name="isChange"></param>
        /// <param name="isSure"></param>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetChangeInfo(bool isChange, bool isSure, string tourId, string tourStatus)
        {
            string str = string.Empty;
            if (isChange)
            {
                str = "<span ><a  class='fontred' target='_blank' href='" + PrintPageSp + "?tourId=" + tourId + "&type=1'>(变)</a></span>";
            }
            if (isSure)
            {
                str = "<span ><a class='fontgreen' target='_blank' href='" + PrintPageSp + "?tourId=" + tourId + "&type=1'>(变)</a></span>";
            }
            if (tourStatus == "已取消")
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 截取AreaName
        /// </summary>
        /// <param name="areaName"></param>
        /// <returns></returns>
        protected string GetAreaName(string areaName)
        {
            if (!string.IsNullOrEmpty(areaName))
            {
                if (areaName.Length > 5)
                {
                    areaName = areaName.Substring(0, 5) + "...";
                }
            }
            return areaName;
        }
    }

}
