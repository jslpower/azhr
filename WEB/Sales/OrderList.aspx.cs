using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.Web.Sales
{
    public partial class OrderList : BackPage
    {
        #region attributes
        /// <summary>
        /// 结算单url
        /// </summary>
        public string Print_JieSuanDan = "";
        /// <summary>
        /// 合计金额
        /// </summary>
        protected decimal SumMoney = 0;
        /// <summary>
        /// 总人数
        /// </summary>
        protected int SumCount = 0; 

        string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourID");
            PowerControl();
            InitPrintPath();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            MOrderSum orderSum = new MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> list = null;

            var model = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(Convert.ToString(TourId));

            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("plan")))//计调订单查询的订单为已成交订单
            {
                list = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref orderSum, TourId);
            }
            else
            {
                list = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(TourId, ref orderSum);
            }

            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    SumCount += (list[i].Adults + list[i].Childs);
                    SumMoney += list[i].ConfirmMoney;
                }
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                this.litMsg.Visible = false;
            }
            else
            {
                this.litMsg.Visible = true;
                this.litMsg.Text = "<tr><td align='center' colspan='10'>没有订单!</td></tr>";
            }

            list = null;
            orderSum = null;
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_栏目, false);
                return;
            }
        }

        /// <summary>
        /// 初始化打印路径信息
        /// </summary>
        void InitPrintPath()
        {
            var printBLL = new EyouSoft.BLL.ComStructure.BComSetting();
            Print_JieSuanDan = printBLL.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.结算单);
            printBLL = null;
        }
        #endregion


        #region protected members
        /// <summary>
        /// 获取结算单打印路径
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="tourType"></param>
        /// <returns></returns>
        protected string GetPrintJieSuanDan(object orderId, object tourType)
        {
            if (orderId == null || tourType == null || Print_JieSuanDan == "javascript:void(0)") return "javascript:void(0)";
            string _orderId = orderId.ToString();
            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            return Print_JieSuanDan + "?OrderId=" + _orderId + "&tourType=" + (int)_tourType;
        }

        /// <summary>
        /// 获取游客确认单打印路径
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="tourType">团队类型</param>
        /// <returns></returns>
        protected string GetPrintYouKeQueRenDan(object orderId, object tourType)
        {
            if (orderId == null || tourType == null || Print_JieSuanDan == "javascript:void(0)") return "javascript:void(0)";

            string _orderId = orderId.ToString();
            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            return Print_JieSuanDan + "?OrderId=" + _orderId + "&tourType=" + (int)_tourType + "&ykxc=1";
        }
        #endregion
    }
}
