using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.Plan
{
    public partial class PlanList : BackPage
    {
        #region attributes
        /// <summary>
        /// 页记录数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 页索引
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        protected int RecordCount = 0;
        //团队行程单
        protected string teamPrintUrl = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //行程单
            teamPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 绑定团队状态
            int tourStatus = Utils.GetInt(Utils.GetQueryStringValue("sltTourStatus"), -1);
            this.litTourStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourSureStatus)), tourStatus.ToString(), true);
            #endregion
            #region 绑定操作状态
            int operaterStatus = Utils.GetInt(Utils.GetQueryStringValue("sltOperaterStatus"), -1);
            this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus)), operaterStatus.ToString(), true);
            #endregion
            #region 查询实体
            //客户单位Id
            string CustomerId = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHBH);
            //客户单位
            string CustomerName = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHMC);
            //销售员 计调员 Id
            string SelerId = Utils.GetQueryStringValue(this.SellsSelect1.SellsIDClient);
            string PlanerId = Utils.GetQueryStringValue(this.SellsSelect2.SellsIDClient);
            //销售员 计调员
            string SelerName = Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);
            string PlanerName = Utils.GetQueryStringValue(this.SellsSelect2.SellsNameClient);
            EyouSoft.Model.HPlanStructure.MPlanListSearch Search = new EyouSoft.Model.HPlanStructure.MPlanListSearch();
            //团号
            Search.TourCode = Utils.GetQueryStringValue("txtTourCode");
            //客源单位
            Search.CompanyInfo = new EyouSoft.Model.HPlanStructure.MCompanyInfo();
            Search.CompanyInfo.CompanyId = CustomerId;
            Search.CompanyInfo.CompanyName = CustomerName;
            //出团时间
            Search.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
            Search.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            //销售员
            Search.SellerId = SelerId;
            Search.SellerName = SelerName;
            //计调员       
            Search.PlanerId = PlanerId;
            Search.Planer = PlanerName;
            //团队状态
            if (operaterStatus > -1)
            {
                Search.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)operaterStatus;
            }
            //操作状态
            if (tourStatus > -1)
            {
                Search.TourSureStatus = (EyouSoft.Model.EnumType.TourStructure.TourSureStatus)tourStatus;
            }
            #region 查询赋值
            if (!string.IsNullOrEmpty(CustomerId) && !string.IsNullOrEmpty(CustomerName))
            {
                this.CustomerUnitSelect1.CustomerUnitId = CustomerId;
                this.CustomerUnitSelect1.CustomerUnitName = CustomerName;
            }
            if (!string.IsNullOrEmpty(SelerId) && !string.IsNullOrEmpty(SelerName))
            {
                this.SellsSelect1.SellsID = SelerId;
                this.SellsSelect1.SellsName = SelerName;
            }
            if (!string.IsNullOrEmpty(PlanerId) && !string.IsNullOrEmpty(PlanerName))
            {
                this.SellsSelect2.SellsID = PlanerId;
                this.SellsSelect2.SellsName = PlanerName;
            }
            #endregion
            #endregion
            //获取分页参数并强转
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            //计调列表
            IList<EyouSoft.Model.HPlanStructure.MPlanList> TeamOplist = new EyouSoft.BLL.HPlanStructure.BPlan().GetPlanList(SiteUserInfo.CompanyId, PageSize, PageIndex, ref RecordCount, Search);
            if (TeamOplist != null && TeamOplist.Count > 0)
            {
                this.rpt_list.DataSource = TeamOplist;
                this.rpt_list.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                this.pan_Msg.Visible = true;
            }
            TeamOplist = null;
            Search = null;

        }

        #region 计划变更 颜色处理
        /// <summary>
        /// 计划变更 颜色处理
        /// </summary>
        /// <param name="isChange">是否变更</param>
        /// <param name="IsSure">是否确认</param>
        /// <returns></returns>
        protected string GetTourPlanIschange(bool isChange, bool IsSure, string tourId)
        {
            System.Text.StringBuilder sbChange = new System.Text.StringBuilder();
            if (isChange)
            {
                //确认 绿色 未确认 红色
                if (IsSure)
                {
                    sbChange.Append("<span><a target=\"_blank\" href=\"" + teamPrintUrl + "?type=1&tourId=" + tourId + "&sl=" + Utils.GetQueryStringValue("sl") + "\" class=\"fontgreen\">(变)</a></span>");
                }
                else
                {
                    sbChange.Append("<span><a target=\"_blank\" href=\"" + teamPrintUrl + "?type=1&tourId=" + tourId + "&sl=" + Utils.GetQueryStringValue("sl") + "\" class=\"fontred\">(变)</a></span>");
                }
            }
            return sbChange.ToString();
        }
        #endregion

        #region 获取客户单位信息
        protected string GetCustomerInfo(object customer, string type)
        {
            System.Text.StringBuilder bs = new System.Text.StringBuilder();
            IList<EyouSoft.Model.HPlanStructure.MCompanyInfo> Company = (List<EyouSoft.Model.HPlanStructure.MCompanyInfo>)customer;
            if (Company != null && Company.Count > 0)
            {
                if (type == "single")
                {
                    bs.Append("" + Company[0].CompanyName + "");
                }
                else
                {
                    bs.Append("" + Company[0].CompanyName + "<br />联系人：" + Company[0].Contact + "<br />联系电话：" + Company[0].Phone + "");
                }
            }
            return bs.ToString();
        }
        #endregion

        #region 根据团队状态判断计调操作
        /// <summary>
        /// 根据团队状态判断计调操作
        /// </summary>
        /// <param name="state"></param>
        /// <param name="tourid"></param>
        /// <param name="tourQType">对外报价类型 分项 整团</param>
        /// <param name="tourType">团队类型 组团 散拼</param>
        /// <returns></returns>
        protected string GetOperate(EyouSoft.Model.EnumType.TourStructure.TourStatus state, string tourid, object list)
        {
            string str = string.Empty;
            string sl = Utils.GetQueryStringValue("sl");
            bool ret = false;
            bool isJS = true;
            IList<EyouSoft.Model.HTourStructure.MTourPlaner> Oplist = (List<EyouSoft.Model.HTourStructure.MTourPlaner>)list;
            {
                if (Oplist != null && Oplist.Count > 0)
                {
                    for (int i = 0; i < Oplist.Count; i++)
                    {
                        if (Oplist[i].PlanerId == this.SiteUserInfo.UserId)
                        {
                            ret = true;
                            isJS = Oplist[i].IsJieShou;
                        }
                    }
                }
            }
            //计调未接收的显示接收任务，计调已接收的显示安排
            if (state == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 || isJS == false)
            {
                str = "<a data-class=\"receiveOp\" data-TourId=" + tourid + " data-State=" + (int)state + " data-teamPlaner=\"" + ret + "\" href=\"javascript:void(0);\">接收任务</a>";
            }
            else
            {
                str = "<a href=\"/Plan/PlanLobal.aspx?&sl=" + sl + "&tourId=" + tourid + "\">安排</a>";
            }
            return str;
        }
        #endregion

        #region 获取销售员信息
        /// <summary>
        /// 销售员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string GetSellerInfo(object model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            EyouSoft.Model.TourStructure.MSaleInfo sale = (EyouSoft.Model.TourStructure.MSaleInfo)model;
            if (sale != null)
            {
                sb.Append("" + sale.Name + "");
            }
            return sb.ToString();
        }
        #endregion

        #region 获取计调员
        /// <summary>
        /// 计调员
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetOperaList(object list)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            IList<EyouSoft.Model.HTourStructure.MTourPlaner> Oplist = (List<EyouSoft.Model.HTourStructure.MTourPlaner>)list;
            if (Oplist != null && Oplist.Count > 0)
            {
                for (int i = 0; i < Oplist.Count; i++)
                {
                    if (i == Oplist.Count - 1)
                    {
                        sb.Append("" + Oplist[i].Planer + "");
                    }
                    else
                    {
                        sb.Append("" + Oplist[i].Planer + ",");
                    }
                }
            }
            return sb.ToString().Trim(',');
        }
        #endregion

        #region 分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = PageSize;
            this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
            this.ExporPageInfoSelect1.intRecordCount = RecordCount;
        }
        #endregion

        #region 计划类型
        /// <summary>
        /// 计划类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected bool GetTourType(EyouSoft.Model.EnumType.TourStructure.TourType type)
        {
            bool ret = false;
            if (type == EyouSoft.Model.EnumType.TourStructure.TourType.团队产品 || type == EyouSoft.Model.EnumType.TourStructure.TourType.自由行)
            {
                ret = true;
            }
            return ret;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            string sl = Utils.GetQueryStringValue("sl");
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_栏目) &&
                sl == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_计调列表).ToString())
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_栏目, false);
                return;
            }
        }
        #endregion

        protected string GetUrl(object tourtype,object tourid)
        {
            var u = "javascript:void(0);";
            switch ((EyouSoft.Model.EnumType.TourStructure.TourType)tourtype)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.团队产品:
                    u = "/Sales/ChanPinEdit.aspx?act=update&id=" + tourid + "&sl=2&type=";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.自由行:
                    u = "/Sales/ChanPinEdit.aspx?act=update&id=" + tourid + "&sl=3&type=free";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品:
                    u = "/Sales/AddSanpin.aspx?isparent=False&type=&sl=365&act=update&id=" + tourid;
                    break;
            }
            return u;
        }

        #endregion
    }
}
