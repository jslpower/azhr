using System;
using System.Collections;

namespace EyouSoft.Web.Guide
{
    using System.Collections.Generic;
    using System.Text;

    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.ComStructure;

    public partial class DaoYouPaiBan : BackPage
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

        protected int Year;//年份
        protected int Month;//月份
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            string GuidName = Utils.GetQueryStringValue("txtguidname");
            Year = Utils.GetInt(Utils.GetQueryStringValue("seleYear")) > 0 ? Utils.GetInt(Utils.GetQueryStringValue("seleYear")) : DateTime.Now.Year;
            Month = Utils.GetInt(Utils.GetQueryStringValue("seleMonth")) > 0 ? Utils.GetInt(Utils.GetQueryStringValue("seleMonth")) : DateTime.Now.Month;
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStarTime"));
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            string location = Utils.GetQueryStringValue("txtLocation");
            if (!IsPostBack)
            {
                if (startTime != null && endTime == null)
                {
                    Year = startTime.Value.Year;
                    Month = startTime.Value.Month;
                }
                if (startTime == null && endTime != null)
                {
                    Year = endTime.Value.Year;
                    Month = endTime.Value.Month;
                }
                if (startTime != null && endTime != null)
                {
                    if (Month < startTime.Value.Month || Month > endTime.Value.Month)
                    {
                        Year = startTime.Value.Year;
                        Month = startTime.Value.Month;
                    }
                }
                DataInit(GuidName, Year, Month, startTime, endTime, location);
            }
        }

        #region 拼接查询条件
        protected string GetYearHtml()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<option value='-1'>--请选择--</option>");
            sb.Append("<option value='" + (DateTime.Now.Year - 5) + "'>" + (DateTime.Now.Year - 5) + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year - 4) + "'>" + (DateTime.Now.Year - 4) + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year - 3) + "'>" + (DateTime.Now.Year - 3) + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year - 2) + "'>" + (DateTime.Now.Year - 2) + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year - 1) + "'>" + (DateTime.Now.Year - 1) + "</option>");
            sb.Append("<option selected='selected' value='" + DateTime.Now.Year + "'>" + DateTime.Now.Year + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year + 1) + "'>" + (DateTime.Now.Year + 1) + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year + 2) + "'>" + (DateTime.Now.Year + 2) + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year + 3) + "'>" + (DateTime.Now.Year + 3) + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year + 4) + "'>" + (DateTime.Now.Year + 4) + "</option>");
            sb.Append("<option value='" + (DateTime.Now.Year + 5) + "'>" + (DateTime.Now.Year + 5) + "</option>");
            return sb.ToString();
        }
        #endregion


        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string guidname, int year, int month, DateTime? startTime, DateTime? endTime, string location)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            var bll = new EyouSoft.BLL.ComStructure.BDaoYou();
            startTime = (startTime == null ? null : (DateTime?)startTime);
            endTime = (endTime == null ? null : (DateTime?)endTime);
            var list = bll.GetGuidePlanWork(this.SiteUserInfo.CompanyId, guidname, year, month, startTime, endTime, location, pageIndex, pageSize, ref recordCount);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                //绑定分页
                //BindPage();
            }
            else
            {
                this.lbMsg.Text = "<tr><td colspan=\"36\" align=\"center\">暂无信息</td></tr>";
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        //private void BindPage()
        //{
        //    this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        //    this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
        //    this.ExporPageInfoSelect1.intPageSize = pageSize;
        //    this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
        //    this.ExporPageInfoSelect1.intRecordCount = recordCount;

        //    this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        //    this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
        //    this.ExporPageInfoSelect2.intPageSize = pageSize;
        //    this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
        //    this.ExporPageInfoSelect2.intRecordCount = recordCount;
        //}

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游排班_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游排班_栏目, false);
                return;
            }
        }

        #endregion

        #region 前台使用方法
        protected string GetState(object obj, string year, string month, int date, object name, object guideid)
        {
            name = name.ToString();
            guideid = guideid.ToString();
            if (month == "4" || month == "6" || month == "9" || month == "11")
            {
                if (date == 31)
                {
                    return "";
                }
            }
            //闰年
            if (month == "2" && (Utils.GetInt(year) % 4) == 0)
            {
                if (date > 29)
                {
                    return "";
                }
            }
            //平年
            if (month == "2" && (Utils.GetInt(year) % 4) > 0)
            {
                if (date > 28)
                {
                    return "";
                }
            }

            DateTime currentTime = DateTime.Parse(year + "-" + month + "-" + (date.ToString()));
            IList<MGuidePlanWorkType> list = (IList<MGuidePlanWorkType>)obj;
            StringBuilder str = new StringBuilder();

            if (list != null && list.Count > 0)
            {
                //=0 （无任务），=1（假期），=2（停职），=3（上团），=4（行程中），=5（下团），=6（同天上下团），=7（套团）
                int type = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    DateTime OnTime = DateTime.Parse(list[i].OnTime.Value.ToShortDateString());
                    DateTime NextTime = list[i].NextTime.Value;
                    if (currentTime >= OnTime && currentTime <= NextTime)
                    {
                        if (list[i].Type == "假期")
                        {
                            type = 1;
                            break;
                        }
                        else if (list[i].Type == "停职")
                        {
                            type = 2;
                            break;
                        }
                        else
                        {
                            switch (type)
                            {
                                case 3:
                                    type = 7;
                                    break;
                                case 4:
                                    type = 7;
                                    break;
                                case 5:
                                    type = 7;
                                    break;
                                case 6:
                                    type = 7;
                                    break;
                                case 7:
                                    type = 7;
                                    break;
                                default:
                                    if (OnTime == currentTime && NextTime != currentTime)
                                        type = 3;
                                    if (OnTime < currentTime && NextTime > currentTime)
                                        type = 4;
                                    if (OnTime != currentTime && NextTime == currentTime)
                                        type = 5;
                                    if (OnTime == currentTime && NextTime == currentTime)
                                        type = 6;
                                    break;
                            }
                        }
                    }
                }
                switch (type)
                {
                    case 1:
                        //假期
                        str.Append("<a class='linkcc shangtuan' data-type='假期' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' href='javascript:void(0)' bt-xtitle='' title=''><img border='0' src='/images/dy-center/jia.gif' alt='假期' title='假期' /></a>");
                        break;
                    case 2:
                        //停职
                        str.Append("<a class='linkcc shangtuan' data-type='停职' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' href='javascript:void(0)' bt-xtitle='' title=''><img border='0' alt='停职' src='/images/dy-center/ting.gif' title='停职' /></a>");
                        break;
                    case 3:
                        //已安排  上团
                        str.Append("<a class='linkaa shangtuan' data-type='已安排' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' href='javascript:void(0)' bt-xtitle='' title=''><img border='0' src='/images/dy-center/daoshang.gif' alt='上团' title='上团' /></a>");
                        break;
                    case 4:
                        //已安排  行程中
                        str.Append("<a class='linkaa shangtuan' data-type='已安排' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' href='javascript:void(0)' bt-xtitle='' title=''><img border='0' src='/images/dy-center/daoqi.gif' alt='行程中' title='行程中' /></a>");
                        break;
                    case 5:
                        //已安排  下团
                        str.Append("<a class='linkaa shangtuan' data-type='已安排' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' href='javascript:void(0)' bt-xtitle='' title=''><img border='0' src='/images/dy-center/daoxia.gif' alt='下团' title='下团' /></a>");
                        break;
                    case 6:
                        //已安排  同天上下团
                        str.Append("<a class='linkaa shangtuan' data-type='已安排' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' href='javascript:void(0)' bt-xtitle='' title=''><img border='0' src='/images/dy-center/daoshangxia.gif' alt='同天上下团' title='同天上下团' /></a>");
                        break;
                    case 7:
                        //已安排  套团
                        str.Append("<a class='linkaa shangtuan' data-type='已安排' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' href='javascript:void(0)' bt-xtitle='' title=''><img border='0' src='/images/dy-center/daoq.gif' alt='套团' title='套团' /></a>");
                        break;
                    default:
                        //无任务
                        str.Append("<a class='linkbb' data-type='无任务' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' data-name='" + name + "' href='javascript:void(0)' bt-xtitle='' title=''>—</a>");
                        break;
                }
            }
            else
            {
                str.Append("<a class='linkbb' data-type='无任务' data-time='" + currentTime.ToString("yyyy-MM-dd") + "' data-name='" + name + "' href='javascript:void(0)' bt-xtitle='' title=''>—</a>");
            }
            return str.ToString();
        }

        #endregion
    }
}
