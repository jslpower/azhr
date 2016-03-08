using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Collections.Generic;

namespace EyouSoft.Web.Quote
{
    public partial class BaoJia : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 100;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType.Length > 0)
            {
                AJAX(doType);
            }
            #endregion

            if (!IsPostBack)
            {
                InitPrivs();
                PageInit();
            }

        }


        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.整团报价_报价中心_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.整团报价_报价中心_栏目, true);
                return;
            }

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.整团报价_报价中心_新增))
            {
                this.phForAdd.Visible = false;
                this.phForCopy.Visible = false;
            }

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.整团报价_报价中心_删除))
            {
                this.phForDelete.Visible = false;
            }
        }

        /// <summary>
        /// 绑定页面数据
        /// </summary>
        private void PageInit()
        {
            EyouSoft.Model.HTourStructure.MQuoteSearch search = new EyouSoft.Model.HTourStructure.MQuoteSearch();
            string areaId = Utils.GetQueryStringValue("ddlArea");
            search.CompanyId = CurrentUserCompanyID;
            if (!string.IsNullOrEmpty(areaId))
            {
                search.AreaId = Utils.GetInt(areaId);
            }
            search.RouteName = Utils.GetQueryStringValue("txtRouteName");

            //对方业务员
            search.Contact = Utils.GetQueryStringValue("txtContact");

            //询价编号
            search.BuyId = Utils.GetQueryStringValue("txtBuyId");

            //询价单位
            string buyCompanyID = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHBH);
            string buyCompanyName = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHMC);

            this.CustomerUnitSelect1.CustomerUnitId = buyCompanyID;
            this.CustomerUnitSelect1.CustomerUnitName = buyCompanyName;

            search.BuyCompanyID = buyCompanyID;
            search.BuyCompanyName = buyCompanyName;

            //询价时间
            search.BeginBuyTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginBuyTime"));
            search.EndBuyTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndBuyTime"));

            //成团时间
            search.BeginTourTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginTourTime"));
            search.EndTourTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTourTime"));

            string minAdults = Utils.GetQueryStringValue("txtMinAdults");
            string maxAdults = Utils.GetQueryStringValue("txtMaxAdults");

            if (!string.IsNullOrEmpty(minAdults))
            {
                search.MinAdults = Utils.GetInt(minAdults);
            }

            if (!string.IsNullOrEmpty(maxAdults))
            {
                search.MaxAdults = Utils.GetInt(maxAdults);
            }



            //销售员
            string sellerId = Utils.GetQueryStringValue(this.SellsSelect1.ClientID + "_hideSellID");
            string sellerName = Utils.GetQueryStringValue(this.SellsSelect1.ClientID + "_txtSellName");
            this.SellsSelect1.SellsID = sellerId;
            this.SellsSelect1.SellsName = sellerName;

            search.SellerId = sellerId;
            search.SellerName = sellerName;

            //报价状态
            string quoteStatus = Utils.GetQueryStringValue("ddlQuoteStatus");
            if (!string.IsNullOrEmpty(quoteStatus))
            {
                search.QuoteStatus = (EyouSoft.Model.EnumType.TourStructure.QuoteState)Utils.GetInt(quoteStatus);
            }

            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);

            EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();

            IList<EyouSoft.Model.HTourStructure.MQuoteInfo> list = bll.GetQuoteList(pageSize, pageIndex, ref recordCount, search);
            if (list != null && list.Count > 0)
            {
                this.rpQuote.DataSource = list;
                this.rpQuote.DataBind();
                //绑定分页
                BindPage();
                this.litMsg.Visible = false;
            }
            else
            {
                this.litMsg.Visible = true;
                this.ExporPageInfoSelect1.Visible = false;
            }
        }


        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        #region  列表_绑定价格信息
        /// <summary>
        /// 列表_绑定价格信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetPrice(object obj)
        {
            if (obj == null) return null;
            System.Text.StringBuilder query = new System.Text.StringBuilder();

            IList<EyouSoft.Model.HTourStructure.MQuotePrice> list = (IList<EyouSoft.Model.HTourStructure.MQuotePrice>)obj;
            if (list != null && list.Count != 0)
            {
                list = list.Where(c => c.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格).ToList();
                if (list != null && list.Count != 0)
                {
                    decimal AdultPrice = list[0].AdultPrice;
                    query.AppendFormat("<b class='a_price_info fontblue'>{0}</b>", ToMoneyString(AdultPrice.ToString("f2")));
                    query.Append("<span style='display: none;'>");
                    for (int i = 0; i < list.Count; i++)
                    {
                        EyouSoft.Model.HTourStructure.MQuotePrice model = list[i];
                        query.AppendFormat("<b>价格{0}</b>", i + 1);
                        query.Append("</br>");
                        query.AppendFormat("成人价：{0}", ToMoneyString(model.AdultPrice.ToString("f2")));
                        query.Append("</br>");
                        query.AppendFormat("儿童价：{0}", ToMoneyString(model.ChildPrice.ToString("f2")));
                        query.Append("</br>");
                        query.AppendFormat("领队价：{0}", ToMoneyString(model.LeadPrice.ToString("f2")));
                        query.Append("</br>");
                    }
                    query.Append("</span>");
                }
            }

            return query.ToString();
        }
        #endregion

        #region 下拉框_绑定线路区域
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        protected string BindArea(string area)
        {
            System.Text.StringBuilder option = new System.Text.StringBuilder();
            option.Append("<option value=''>-请选择-</option>");
            EyouSoft.BLL.ComStructure.BComArea bArea = new EyouSoft.BLL.ComStructure.BComArea();
            IList<EyouSoft.Model.ComStructure.MComArea> list = bArea.GetAreaByCID(SiteUserInfo.CompanyId).Where(m => m.LngType == EyouSoft.Model.EnumType.SysStructure.LngType.中文).ToList();

            if (list != null)
            {
                if (list.Count != 0)
                {
                    list = list.Where(c => c.LngType == EyouSoft.Model.EnumType.SysStructure.LngType.中文).ToList();
                    foreach (var item in list)
                    {
                        if (item.AreaId.ToString().Equals(area))
                        {
                            option.AppendFormat("<option value='{0}'  selected='selected'>{1}</option>", item.AreaId, item.AreaName);
                        }
                        else
                        {
                            option.AppendFormat("<option value='{0}' >{1}</option>", item.AreaId, item.AreaName);
                        }
                    }
                }
            }
            return option.ToString();
        }
        #endregion

        #region 下拉框_绑定报价状态
        /// <summary>
        /// 绑定报价状态
        /// </summary>
        /// <param name="selectItem"></param>
        /// <returns></returns>
        protected string BindQuoteStatus(string value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<option value=''>-请选择-</option>");

            Array values = Enum.GetValues(typeof(EyouSoft.Model.EnumType.TourStructure.QuoteState));

            //   int[] not = new int[] { (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核, (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核成功, (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核失败 };
            if (values.Length != 0)
            {
                foreach (var item in values)
                {
                    int Value = (int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.QuoteState), item.ToString());
                    string Text = Enum.GetName(typeof(EyouSoft.Model.EnumType.TourStructure.QuoteState), item);
                    if (value.Equals(Value.ToString()))
                    {
                        sb.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", Value, Text);
                    }
                    else
                    {
                        sb.AppendFormat("<option value='{0}' >{1}</option>", Value, Text);
                    }
                }
            }
            return sb.ToString();
        }

        #endregion

        #region 根据报价状态和次数显示信息
        /// <summary>
        /// 根据报价状态和次数显示信息
        /// </summary>
        /// <param name="count">报价次数</param>
        /// <param name="quoteState">报价状态</param>
        /// <returns></returns>
        protected string GetHtmlByState(string count, EyouSoft.Model.EnumType.TourStructure.QuoteState quoteState, string cancelReason)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (quoteState)
            {
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功:
                    sb.Append("<span class='fontgreen' data-class='QuoteState' data-state='" + (int)EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功 + "'>报价成功</span>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.取消报价:
                    sb.Append("<a data-class='cancelReason'><span class='fontgray' data-class='QuoteState' data-state='0'>取消报价</span></a><div style='display:none'><b>取消原因</b>:" + EyouSoft.Common.Function.StringValidate.TextToHtml(cancelReason) + "</div>"); break;
                //case EyouSoft.Model.EnumType.TourStructure.QuoteState.垫付申请审核:
                //    sb.Append("<span data-class='QuoteState' data-state='" + (int)EyouSoft.Model.EnumType.TourStructure.QuoteState.垫付申请审核 + "'>垫付申请审核</span>"); break;
                //case EyouSoft.Model.EnumType.TourStructure.QuoteState.审核失败:
                //    sb.Append("<span class='fontgray' data-class='QuoteState' data-state='0'>审核失败</span>"); break;
                //case EyouSoft.Model.EnumType.TourStructure.QuoteState.审核成功:
                //    sb.Append("<span data-class='QuoteState' data-state='0'>审核成功</span>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理:
                    sb.Append("<span data-class='QuoteState' data-state='0'>第" + count + "次报价</span>"); break;
            }
            return sb.ToString();
        }
        #endregion

        #region 处理AJAX请求
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            //对应执行操作
            switch (doType)
            {
                case "delete":
                    //判断权限
                    string[] ids = Utils.GetQueryStringValue("ids").Split(',');
                    //执行并获取结果
                    msg = DeleteData(ids);
                    break;
                default:
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
        #endregion

        #region 列表删除
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string[] ids)
        {
            string msg = string.Empty;
            //删除操作
            if (ids.Length > 0)
            {
                EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
                foreach (string id in ids)
                {
                    bll.DeleteQuote(id);
                }
                msg = "{\"result\":\"1\",\"msg\":\"删除成功!\"}";
            }
            else
            {
                msg = "{\"result\":\"0\",\"msg\":\"请选择一行数据!\"}";
            }
            return msg;
        }
        #endregion
    }
}
