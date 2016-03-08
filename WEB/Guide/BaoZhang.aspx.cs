using System;
using System.Collections;

namespace EyouSoft.Web.Guide
{
    using System.Collections.Generic;

    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;

    public partial class BaoZhang : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;

        /// <summary>
        /// 打印页面Url
        /// </summary>
        protected string PrintUrl = string.Empty;
        //团队状态html
        protected System.Text.StringBuilder TourStatusHtml = new System.Text.StringBuilder();

        /// <summary>
        /// 列表操作栏
        /// </summary>
        protected string _caozuo;
        /// <summary>
        /// 二级栏目枚举
        /// </summary>
        protected Menu2 _sl;
        /// <summary>
        /// 报销报账枚举
        /// </summary>
        protected BZList _type;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            PowerControl();

            //销售员 Id
            string SelerId = Utils.GetQueryStringValue(this.sellers1.SellsIDClient);
            //销售员 
            string SelerName = Utils.GetQueryStringValue(this.sellers1.SellsNameClient);
            if (!string.IsNullOrEmpty(SelerId))
            {
                this.sellers1.SellsID = SelerId;
            }
            if (!string.IsNullOrEmpty(SelerName))
            {
                this.sellers1.SellsName = SelerName;
            }
            //导游
            string guidID = Utils.GetQueryStringValue(this.guid1.SellsIDClient);
            string guidName = Utils.GetQueryStringValue(this.guid1.SellsNameClient);
            if (!string.IsNullOrEmpty(guidID))
            {
                this.guid1.SellsID = guidID;
            }
            if (!string.IsNullOrEmpty(guidName))
            {
                this.guid1.SellsName = guidName;
            }
            //计调
            string planerId = Utils.GetQueryStringValue(this.planer.SellsIDClient);
            string planerNm = Utils.GetQueryStringValue(this.planer.SellsNameClient);
            if (!string.IsNullOrEmpty(planerId))
            {
                this.planer.SellsID = planerId;
            }
            if (!string.IsNullOrEmpty(planerNm))
            {
                this.planer.SellsName = planerNm;
            }

            //绑定团队状态
            string tourState = Utils.GetQueryStringValue("tourState");
            BindTourState(tourState);

            DataInit();
        }

        #region 绑定团队状态
        /// <summary>
        /// 团队状态
        /// </summary>
        /// <param name="statusID">状态id</param>
        /// <returns></returns>
        protected string BindTourState(string statusID)
        {
            TourStatusHtml.Append("<select name=\"tourState\"  class=\"inputselect\"><option value=\"\">--请选择--</option>");
            List<EyouSoft.Common.EnumObj> tourStatus = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus));
            if (tourStatus != null && tourStatus.Count > 0)
            {
                for (int i = 0; i < tourStatus.Count; i++)
                {
                    if (tourStatus[i].Value == statusID)
                    {
                        TourStatusHtml.Append("<option value=\"" + tourStatus[i].Value + "\" selected=\"selected\">" + tourStatus[i].Text + "</option>");
                    }
                    else
                    {
                        TourStatusHtml.Append("<option value=\"" + tourStatus[i].Value + "\">" + tourStatus[i].Text + "</option>");
                    }
                }
            }
            TourStatusHtml.Append("</select>");
            return TourStatusHtml.ToString();
        }
        #endregion


        /// <summary>
        /// 绑定导游
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetGuidInfoHtml(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MGuidInfo> info = (IList<EyouSoft.Model.TourStructure.MGuidInfo>)o;
            if (info != null && info.Count > 0)
            {
                for (int i = 0; i < info.Count; i++)
                {
                    if (i == info.Count - 1)
                    {
                        sb.Append("" + info[i].Name + "");
                    }
                    else
                    {
                        sb.Append("" + info[i].Name + ",");
                    }
                }
            }
            return sb.ToString();
        }

        protected string GetOperaterList(object o)
        {
            System.Text.StringBuilder _s = new System.Text.StringBuilder();
            IList<EyouSoft.Model.TourStructure.MTourPlaner> PlanerList = (IList<EyouSoft.Model.TourStructure.MTourPlaner>)o;
            if (PlanerList != null && PlanerList.Count > 0)
            {
                for (int i = 0; i < PlanerList.Count; i++)
                {
                    if (i == PlanerList.Count - 1)
                    {
                        _s.Append("" + PlanerList[i].Planer + "");
                    }
                    else
                    {
                        _s.Append("" + PlanerList[i].Planer + ",");
                    }
                }
            }
            return _s.ToString();
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {

            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.Model.TourStructure.MBZSearch Search = new EyouSoft.Model.TourStructure.MBZSearch();
            Search.TourCode = Utils.GetQueryStringValue("txtTourCode");
            Search.RouteName = Utils.GetQueryStringValue("txtRouteName");
            Search.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStarTime"));
            Search.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStarEnd"));
            Search.SellerId = Utils.GetQueryStringValue(this.sellers1.SellsIDClient);
            Search.SellerName = Utils.GetQueryStringValue(this.sellers1.SellsNameClient);
            Search.GuideId = Utils.GetQueryStringValue(this.guid1.SellsIDClient);
            Search.Guide = Utils.GetQueryStringValue(this.guid1.SellsNameClient);
            Search.PlanerId = Utils.GetQueryStringValue(this.planer.SellsIDClient);
            Search.Planer = Utils.GetQueryStringValue(this.planer.SellsNameClient);
            Search.IsDealt = Utils.GetQueryStringValue("isDealt") == "1" || Utils.GetQueryStringValue("isDealt") == "2";
            Search.SL = this._sl;
            Search.Type = this._type;
            if (Utils.GetQueryStringValue("tourState") != "")
            {
                Search.TourStatus = (TourStatus)Utils.GetInt(Utils.GetQueryStringValue("tourState"));
            }

            IList<EyouSoft.Model.TourStructure.MBZInfo> list = new EyouSoft.BLL.TourStructure.BTour().GetBXBZList(this.SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, Search);
            if (list != null && list.Count > 0)
            {
                this.replist.DataSource = list;
                this.replist.DataBind();

                //绑定分页
                BindPage();
            }
            else
            {
                this.pan_Msg.Visible = true;
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
        #endregion

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            var bxbz = Utils.GetQueryStringValue("isDealt");

            this._sl = (Menu2)Utils.GetInt(SL);
            switch (this._sl)
            {
                case Menu2.导游中心_导游报账:
                    if (!CheckGrant(Privs.导游中心_导游报账_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.导游中心_导游报账_栏目, false);
                    }
                    this.phHead.Visible = true;
                    this._type = BZList.导游报账;
                    switch (bxbz)
                    {
                        case "1":
                            this._caozuo = "<a data-class=\"a_ExamineA\" href=\"javascript:void(0);\" class=\"check-btn\" title=\"查看\"></a>";
                            break;
                        default:
                            this._caozuo = "<a data-class=\"a_ExamineA\" href=\"javascript:void(0);\"><img alt=\"导游报账\" src=\"/images/baozhang-cy.gif\" />导游报账</a>";
                            break;
                    }
                    break;
                case Menu2.计调中心_计调报账:
                    if (!CheckGrant(Privs.计调中心_计调报账_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.计调中心_计调报账_栏目, false);
                    }
                    this.phHead.Visible = false;
                    this._type = BZList.计调报账;
                    this._caozuo = "<a data-class=\"a_ExamineA\" href=\"javascript:void(0);\"><img alt=\"计调报账\" src=\"/images/baozhang-cy.gif\" />计调报账</a>";
                    break;
                case Menu2.财务管理_报销报账:
                    if (!CheckGrant(Privs.财务管理_报销报账_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.财务管理_报销报账_栏目, false);
                    }
                    this.phHead.Visible = true;
                    this.phbx.Visible = true;
                    switch (bxbz)
                    {
                        case "-2":
                            this._caozuo = "<a data-class=\"a_ExamineA\" href=\"javascript:void(0);\">报销</a>";
                            this._type = BZList.报销;
                            break;
                        case "2":
                            this._caozuo = "<a data-class=\"a_ExamineA\" href=\"javascript:void(0);\" class=\"check-btn\" title=\"查看\"></a>";
                            this._type = BZList.报销;
                            break;
                        case "1":
                            this._caozuo = "<a data-class=\"a_Apply\" href=\"javascript:void(0);\" class=\"check-btn\" title=\"查看\"></a>";
                            this._type = BZList.报账;
                            break;
                        default:
                            this._caozuo = "<a data-class=\"a_Apply\" href=\"javascript:void(0);\">审批</a>";
                            this._type = BZList.报账;
                            break;
                    }
                    break;
                default:
                    Utils.ResponseGoBack();
                    break;
            }
        }
    }
}
