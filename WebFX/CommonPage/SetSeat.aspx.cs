using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.WebFX.CommonPage
{
    public partial class SetSeat : BackPage
    {
        /// <summary>
        /// 当前订单编号
        /// </summary>
        string orderId = Utils.GetQueryStringValue("orderId");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void InitPage()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                string tourCarTypeId = Utils.GetQueryStringValue("tourCarTypeId");//计划车型编号
                EyouSoft.BLL.TourStructure.BTourOrder bll = new EyouSoft.BLL.TourStructure.BTourOrder();
                if (!string.IsNullOrEmpty(tourCarTypeId))
                {
                    this.ph_BusTypeList.Visible = false;
                    this.ph_Auto.Visible = false;
                    this.ph_SaveBtn.Visible = false;
                    IList<EyouSoft.Model.TourStructure.MTourOrderSeatInfo> list = bll.GetTourOrderSeatInfo(tourId);
                    list = list.Where(item => (item.TourCarTypeId == tourCarTypeId)).ToList();
                    if (list != null && list.Count > 0)
                    {
                        //绑定车型对应的座位坐标集合
                        this.rpt_busBoxList.DataSource = list;
                        this.rpt_busBoxList.DataBind();
                    }
                }
                else
                {
                    IList<EyouSoft.Model.TourStructure.MTourOrderSeatInfo> list = bll.GetTourOrderSeatInfo(tourId);
                    //仅用作查看座位信息
                    string IsShow = Utils.GetQueryStringValue("isShow");
                    if (!string.IsNullOrEmpty(IsShow) && IsShow == "Show")
                    {
                        this.ph_SaveBtn.Visible = false;
                    }
                    if (list != null && list.Count > 0)
                    {
                        //绑定车型li集合
                        this.rpt_BusTypeList.DataSource = list;
                        this.rpt_BusTypeList.DataBind();
                        //绑定车型对应的座位坐标集合
                        this.rpt_busBoxList.DataSource = list;
                        this.rpt_busBoxList.DataBind();
                    }
                }
            }
        }

        /// <summary>
        /// 初始化座位分布数据的json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetJsonSeat(object obj)
        {
            string str = string.Empty;
            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list = (IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat>)obj;
            if (list != null && list.Count > 0)
            {
                str = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            }
            return str;
        }

        /// <summary>
        /// 加载已选座位数据的json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetJsonSeated(object obj)
        {
            string str = string.Empty;
            IList<EyouSoft.Model.TourStructure.MTourOrderCarTypeSeat> list = (IList<EyouSoft.Model.TourStructure.MTourOrderCarTypeSeat>)obj;
            if (list != null && list.Count > 0)
            {
                str = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            }
            return str;
        }
        /*
        /// <summary>
        /// 获取座位分布
        /// </summary>
        /// <param name="obj">车型座次分布XY</param>
        /// <param name="objtemp">订单已选座位集合</param>
        /// <returns></returns>
        protected string GetHtmlDiv(object obj, object objtemp)
        {
            var list = (List<EyouSoft.Model.SysStructure.MSysCarTypeSeat>)obj;
            list = list.OrderBy(i => (i.SeatNumber)).ToList();//升序排列座位
            var listtmp = (List<EyouSoft.Model.TourStructure.MTourOrderCarTypeSeat>)objtemp;
            //数据员obj转换成list数据 然后拼接html 返回
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                //属于当前订单的已占座位显示为黄色
                if (listtmp.Where(i => (i.SeatNumber == item.SeatNumber && i.OrderId == orderId)).ToList().Count > 0)
                {
                    sb.AppendFormat("<div class=\"movediv\" style=\"left: {0}px; top: {1}px;\"><a href=\"javascript:void(0);\" class=\"yellowbtn\">{2}</a></div>", item.PointX, item.PoinY, item.SeatNumber);
                }//不属于当前订单的已占座位显示为蓝色
                else if (listtmp.Where(i => (i.SeatNumber == item.SeatNumber && i.OrderId != orderId)).ToList().Count > 0)
                {
                    sb.AppendFormat("<div class=\"movediv\" style=\"left: {0}px; top: {1}px;\"><a href=\"javascript:void(0);\" class=\"bluebtn\">{2}</a></div>", item.PointX, item.PoinY, item.SeatNumber);
                }//否则显示为灰色
                else
                {
                    sb.AppendFormat("<div class=\"movediv\" style=\"left: {0}px; top: {1}px;\"><a href=\"javascript:void(0);\" class=\"graybtn\">{2}</a></div>", item.PointX, item.PoinY, item.SeatNumber);
                }

            }
            return sb.ToString();
        }
          
       */
    }
}
