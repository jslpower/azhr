using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;
using System.Text;
using EyouSoft.Model.EnumType.PlanStructure;

namespace EyouSoft.WEB.Sales
{
    /// <summary>
    /// 单项业务
    /// 创建人：刘树超
    /// 时间：2011-10-21
    /// </summary>
    public partial class SingleServerList : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion
        protected string NodataMsg = string.Empty;
        protected string StatusList = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Utils.GetQueryStringValue("doType"))
            {
                case "delete": Delete(); break;
                case "quxiao": QuXiao(); break;
                default: break;
            }

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //订单编号
                string OrderId = Utils.GetQueryStringValue("txtOrderId");
                //单位名称
                string UnitName = Utils.GetQueryStringValue("txtUnitName");
                //下单开始时间
                string OrderSTime = Utils.GetQueryStringValue("txtOrderSTime");
                //下单结束时间
                string OrderETime = Utils.GetQueryStringValue("txtOrderETime");
                //客户单位
                string comId = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHBH);
                string comName = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHMC);
                int status = Utils.GetInt(Utils.GetQueryStringValue("status"), -1);
                this.CustomerUnitSelect1.CustomerUnitId = comId;
                this.CustomerUnitSelect1.CustomerUnitName = comName;
                //销售员
                string opeaterId = Utils.GetQueryStringValue(this.SellsSelect1.SellsIDClient);
                string opeaterName = Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);
                this.SellsSelect1.SellsID = opeaterId;
                this.SellsSelect1.SellsName = opeaterName;
                //初始化
                DataInit(OrderId, UnitName, OrderSTime, OrderETime, opeaterName, comName, status);
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string orderid, string unitname, string orderstime, string orderetime, string operater, string customunit, int status)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.TourStructure.BSingleService bll = new EyouSoft.BLL.TourStructure.BSingleService();
            EyouSoft.Model.TourStructure.MSeachSingleService model = new EyouSoft.Model.TourStructure.MSeachSingleService();
            model.BeginLDate = Utils.GetDateTimeNullable(orderstime);
            model.TourStatus = status == -1 ? null : (EyouSoft.Model.EnumType.TourStructure.TourStatus?)status;
            model.EndLDate = Utils.GetDateTimeNullable(orderetime);
            model.BuyCompanyName = customunit;
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.Operator = operater;
            model.OrderCode = orderid;
            model.SWeiTuoRiQi = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSWeiTuoRiQi"));
            model.EWeiTuoRiQi = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEWeiTuoRiQi"));

            //model.XiaoShouYuanId =this.txtXiaoShouYuan.SellsID = Utils.GetQueryStringValue(this.txtXiaoShouYuan.SellsIDClient);
            //model.JiDiaoYuanId = this.txtJiDiaoYuan.SellsID = Utils.GetQueryStringValue(this.txtJiDiaoYuan.SellsIDClient);

            //model.XiaoShouYuanName = this.txtXiaoShouYuan.SellsName = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient);
            //model.JiDiaoYunaName = this.txtJiDiaoYuan.SellsName = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsNameClient);

            IList<MSingleService> list = bll.GetSingleServiceList(model, pageSize, pageIndex, ref recordCount);
            if (list != null && list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            else
            {
                this.litMsg.Text = "<tr class='old'><td colspan='12' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
            //绑定分页
            BindPage();
        }
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
        void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_栏目, false);
                return;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        void Delete()
        {
            string txtTourId = Utils.GetFormValue("txtTourId");
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_删除)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：没有操作权限。"));

            string txtQuXiaoId = Utils.GetFormValue("txtQuXiaoId");

            int bllRetCode = new EyouSoft.BLL.TourStructure.BSingleService().DeleteSingleServiceByTourId(txtTourId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -99) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：当前状态不允许删除。"));
            RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// 取消
        /// </summary>
        void QuXiao()
        {
            //if(!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_取消)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：没有操作权限。"));

            string txtQuXiaoId = Utils.GetFormValue("txtQuXiaoId");
            string txtYuanYin = Utils.GetFormValue("txtYuanYin");

            int bllRetCode = 100;// new EyouSoft.BLL.TourStructure.BSingleService().QuXiao(CurrentUserCompanyID, txtQuXiaoId, txtYuanYin, SiteUserInfo.UserId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：当前状态不允许取消。"));
            else if (bllRetCode == -97) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：已经存在收款，不允许取消。"));
            else if (bllRetCode == -96) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：已经存在付款，不允许取消。"));
            else if (bllRetCode == -95) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：不是该团销售员或OP，不允许取消。"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：异常代码[" + bllRetCode + "]"));
        }
        #endregion

        #region 前台调用方法
        protected string GetTourStatus(object obj, object yuanYin)
        {
            EyouSoft.Model.EnumType.TourStructure.TourStatus status = (EyouSoft.Model.EnumType.TourStructure.TourStatus)obj;
            string s = string.Empty;
            switch (status)
            {
                //case EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕: s = "已落实"; break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划: s = "操作中"; break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.封团: s = "核算结束"; break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消:
                    s = "已取消";
                    break;
                default: s = status.ToString(); break;
            }

            return s;
        }

        /// <summary>
        /// 返回计调图标
        /// 无计调任务,未安排:灰色 未落实:红色 其它:蓝色
        /// </summary>
        /// <param name="MTourPlanStatus">计调</param>
        /// <returns></returns>
        public static string GetJiDiaoIcon(string tourID)
        {
            EyouSoft.Model.HTourStructure.MTourPlanStatus MPS = new EyouSoft.Model.HTourStructure.MTourPlanStatus();
            var tourModel = new EyouSoft.BLL.HTourStructure.BTour().GetTourInfoModel(tourID);
            if (tourModel != null)
            {
                MPS = tourModel.TourPlanStatus;
            }
            if (MPS != null)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(GetJiDaioIconHtml(MPS.Hotel, "1", "订房"));
                sb.Append(GetJiDaioIconHtml(MPS.PlaneTicket, "9", "飞机"));
                sb.Append(GetJiDaioIconHtml(MPS.TrainTicket, "10", "火车"));
                sb.Append(GetJiDaioIconHtml(MPS.Spot, "3", "景点"));
                //sb.Append(GetJiDaioIconHtml(MPS.Guide, "4", "导服"));
                sb.Append(GetJiDaioIconHtml(MPS.Other, "13", "其他"));
                sb.Append(GetJiDaioIconHtml(MPS.Car, "2", "用车"));
                sb.Append(GetJiDaioIconHtml(MPS.Dining, "6", "餐馆"));


                return sb.ToString();
            }
            else
            {//订房 飞机 火车 景点 导服  其它
                return string.Format("<b class=\"fontgray\"><span class='a_jidiaoicon'>订房</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>飞机</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>火车</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>景点</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>导服</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>其它</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>用车</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>餐馆</span></b>");
            }
        }
        /// <summary>
        /// 获取计调安排状态显示HTML
        /// </summary>
        /// <param name="status">安排状态</param>
        /// <param name="dataType">dataType</param>
        /// <param name="name">计调安排类型名称</param>
        /// <returns></returns>
        protected static string GetJiDaioIconHtml(PlanState status, string dataType, string name)
        {
            if (status == PlanState.未安排)
            {
                return string.Format(" <a class=\"ObjAdd_a\" data-objtype=\"{1}\" href=\"javascript:;\"><b class=\"fontred\"><span class='a_jidiaoicon'>{0}</span></b></a>", name, dataType);
            }
            else if (status == PlanState.未落实)
            {
                return string.Format(" <a class=\"ObjAdd_a\" data-objtype=\"{1}\" href=\"javascript:;\"><b class=\"fontred\"  data-class=\"ShowSourceName\" ><span class='a_jidiaoicon'>{0}</span></b></a><div data-type=\"{1}\" style=\"display: none;\"></div>", name, dataType);
            }
            else if (status == PlanState.无计调任务)
            {
                return string.Format(" <a class=\"ObjAdd_a\" data-objtype=\"{1}\" href=\"javascript:;\"><b class=\"fontgray\"><span class='a_jidiaoicon'>{0}</span></b></a>", name, dataType);
            }
            else if (status == PlanState.已落实)
            {
                return string.Format(" <a class=\"ObjAdd_a\" data-objtype=\"{1}\" href=\"javascript:;\"><b class=\"fontblue\"  data-class=\"ShowSourceName\"><span class='a_jidiaoicon'>{0}</span></b></a><div data-type=\"{1}\" style=\"display: none;\"></div>", name, dataType);
            }

            return string.Empty;
        }


        protected string getState(object obj)
        {
            EyouSoft.Model.EnumType.TourStructure.TourStatus state = (EyouSoft.Model.EnumType.TourStructure.TourStatus)obj;
            string msg = string.Empty;
            switch (state)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划:
                    msg = "操作中";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置:
                    msg = "已落实";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.财务待审:
                    msg = "待终审";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.单团核算:
                    msg = "财务核算";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.封团:
                    msg = "核算结束";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消:
                    msg = "已取消";
                    break;
                default:
                    break;
            }
            return msg;
        }

        protected string GetKeRen(object orderid)
        {
            var s = string.Empty;
            var l = new BLL.TourStructure.BTourOrder().GetTourOrderBuyCompanyTravellerByOrderId(orderid.ToString());
            if(l!=null&&l.Count>0)
            {
                s = l[0].CnName;
            }
            return s;
        }
        #endregion
    }
}
