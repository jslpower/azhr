using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.WebFX
{
    public partial class MyOrder : FrontPage
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
        #endregion

        protected string PrintPageSp = string.Empty;
        protected string PrintPageDd = string.Empty;
        protected string PrintPageYk = string.Empty;
        //是否已读
        protected string queryID = string.Empty;
        //根据团号筛选订单
        protected string queryTourId = string.Empty;
        //获取订单号
        protected string queryOrderID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.BLL.ComStructure.BComSetting comSettingBll = new EyouSoft.BLL.ComStructure.BComSetting();
            PrintPageSp = comSettingBll.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台散拼线路行程单);
            PrintPageDd = comSettingBll.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台订单信息汇总单);
            PrintPageYk = comSettingBll.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台游客信息打印单);



            //ajax
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Cancle"))
                {
                    Response.Clear();
                    Response.Write(Cancle());
                    Response.End();
                }
            }
            queryID = Utils.GetQueryStringValue("id");
            queryTourId = Utils.GetQueryStringValue("tourid");
            queryOrderID = Utils.GetQueryStringValue("orderId");
            if (!IsPostBack)
            {
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                this.HeadDistributorControl1.IsPubLogin =
   System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == this.SiteUserInfo.Username;
                BindSource();
            }

            //将获取订单信息改为已读状态

            if (queryID != "")
            {
                EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
                bll.IsReadCarTypeSeatChange(queryID);
            }

            string getdate = Utils.GetQueryStringValue("getdate");
            if (!string.IsNullOrEmpty(getdate))
            {
                Response.Clear();
                Response.Write(GetShortLine());
                Response.End();
            }
        }

        /// <summary>
        /// 绑定数据源、分页控件
        /// </summary>
        public void BindSource()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

            MSearchFinancialOrder search = new MSearchFinancialOrder();
            search.AreaId = !string.IsNullOrEmpty(Utils.GetQueryStringValue("ddlArea")) ? Utils.GetInt(Utils.GetQueryStringValue("ddlArea")) : 0;
            search.CompanyId = SiteUserInfo.CompanyId;
            search.CrmId = SiteUserInfo.TourCompanyInfo.CompanyId;
            search.DCompanyName = !string.IsNullOrEmpty(Utils.GetQueryStringValue("DCompanyName")) ? Utils.GetQueryStringValue("DCompanyName") : string.Empty;
            search.Status = !string.IsNullOrEmpty(Utils.GetQueryStringValue("ddlOrderStatus")) ? (EyouSoft.Model.EnumType.TourStructure.OrderStatus?)Utils.GetInt(Utils.GetQueryStringValue("ddlOrderStatus")) : null;

            BTourOrder order = new BTourOrder();
            IList<MFinancialOrder> list = order.GetOrderList(search, pageSize, pageIndex, ref recordCount);

            if (list != null && list.Count > 0)
            {
                if (queryTourId != "")
                {
                    this.RtOrder.DataSource = list.Where(m => m.TourId == queryTourId).ToList();
                    this.RtOrder.DataBind();
                }
                else if (queryOrderID != "")
                {
                    this.RtOrder.DataSource = list.Where(i => i.OrderId == queryOrderID).ToList();
                    this.RtOrder.DataBind();
                }
                else
                {
                    this.RtOrder.DataSource = list;
                    this.RtOrder.DataBind();
                }
            }

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
        /// 绑定线路区域的下拉框
        /// </summary>
        /// <returns></returns>
        public string GetArea(string area)
        {
            System.Text.StringBuilder option = new System.Text.StringBuilder();
            option.Append("<option value=''>-请选择-</option>");
            EyouSoft.BLL.ComStructure.BComArea bArea = new EyouSoft.BLL.ComStructure.BComArea();
            IList<EyouSoft.Model.ComStructure.MComArea> list = bArea.GetAreaByCID(SiteUserInfo.CompanyId);

            if (list != null)
            {
                if (list.Count != 0)
                {
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

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <returns></returns>
        private string Cancle()
        {
            string msg = string.Empty;
            string orderId = Utils.GetFormValue("OrderId");
            BTourOrder bOrder = new BTourOrder();
            if (bOrder.UpdateTourOrderExpand(orderId, EyouSoft.Model.EnumType.TourStructure.OrderStatus.已取消, null))
            {
                msg = UtilsCommons.AjaxReturnJson("1", "订单已取消！");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "取消失败！");
            }

            return msg;
        }


        /// <summary>
        /// 线路区域的paopao漂浮
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sellerName"></param>
        /// <param name="sellerPhone"></param>
        /// <param name="sellerMobile"></param>
        /// <returns></returns>
        protected string GetRoutePaoPao(IList<Planer> list, string sellerName, string sellerPhone, string sellerMobile)
        {
            System.Text.StringBuilder pao = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(sellerName))
            {
                pao.AppendFormat("销售员：{0}　{1}　{2}<br />", sellerName, sellerPhone, sellerMobile);
            }
            if (list != null)
            {
                if (list.Count != 0)
                {
                    foreach (Planer plan in list)
                    {
                        pao.AppendFormat("计调员：{0}　{1}　{2}<br />", plan.ContactName, plan.ContactTel, plan.ContactMobile);
                    }
                }
            }

            return pao.ToString();
        }


        /// <summary>
        /// 游客名单
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetPrintPage(string tourId, string orderId)
        {
            string url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台游客信息打印单);
            if (url != "javascript:void(0)")
            {
                url = url + "?tourId=" + tourId + "&orderId=" + orderId;

            }
            return url;

        }

        /// <summary>
        /// 获取短线更改提醒
        /// </summary>
        private string GetShortLine()
        {
            string result = string.Empty;
            System.Text.StringBuilder strbuder = new System.Text.StringBuilder();

            EyouSoft.Model.ComStructure.MComSetting model = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);

            if (model != null && model.IsEnableDuanXian)
            {
                IList<EyouSoft.Model.TourStructure.MCarTypeSeatChange> list = new BTour().GetCarTypeSeatChangeList(SiteUserInfo.CompanyId, SiteUserInfo.TourCompanyInfo.CompanyId, 5, null);
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        strbuder.AppendFormat(" <li><em>{0}</em> <a href='{1}'> {2}({4})</a><span class='date'>{3}</span> </li>", (i + 1).ToString(), list[i].CarChangeType == CarChangeType.上车地点变更 ? "MyOrder.aspx?orderId=" + list[i].OrderId + "&id=" + list[i].Id : "MyOrder.aspx?tourid=" + list[i].TourId + "&id=" + list[i].Id, list[i].RouteName.Length >= 20 ? list[i].RouteName.Substring(0, 10) : list[i].RouteName, list[i].IssueTime.ToString("MM-dd"), list[i].CarChangeType == CarChangeType.车型座次变更 ? "计划变更" : "订单变更");
                    }
                    result = "{\"result\":\"1\",\"msg\":\"" + strbuder.ToString() + "\"}";
                }
                else
                {
                    result = "{\"result\":\"0\",\"msg\":\"\"}";
                }
            }
            return result;
        }

        /// <summary>
        /// 获取座位人数
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        private string GetPeopleNum(string orderid)
        {
            EyouSoft.BLL.TourStructure.BTourOrder bll = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MTourOrderExpand orderModel = bll.GetTourOrderExpandByOrderId(orderid);
            int peonum = 0;
            if (orderModel != null)
            {
                peonum = orderModel.Adults + orderModel.Childs;
            }
            return peonum.ToString();
        }
        /// <summary>
        /// 是否显示查看链接
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="tourid"></param>
        /// <returns></returns>
        protected string GetIsShow(object orderid, object tourid)
        {
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourBaseInfo model = bll.GetTourInfo(Convert.ToString(tourid));
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            if (model != null)
            {
                if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品)
                {
                    EyouSoft.BLL.TourStructure.BTourOrder bllorder = new EyouSoft.BLL.TourStructure.BTourOrder();
                    IList<EyouSoft.Model.TourStructure.MTourOrderSeatInfo> list = bllorder.GetTourOrderSeatInfo(tourid.ToString());
                    if (list != null && list.Count > 0)
                    {
                        str.AppendFormat("<a href='javascript:void(0);' data-class='showCarModel' data-peonum='{0}' data-tourid='{1}' data-orderid='{2}'>查看</a>", GetPeopleNum(Convert.ToString(orderid)), Convert.ToString(tourid), Convert.ToString(orderid));
                    }
                }
            }
            return str.ToString();

        }

    }
}
