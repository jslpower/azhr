using System;
using System.Collections;

namespace EyouSoft.Web.Guide
{
    using System.Collections.Generic;
    using System.Text;

    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.ComStructure;

    public partial class PaiBanTongJi : BackPage
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

        protected DateTime _df;//列表起始日期（上个月的倒数第四天）

        protected DateTime _dt;//列表截至日期（下个月的正数第五天）
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            string th = Utils.GetQueryStringValue("th");
            Year = Utils.GetInt(Utils.GetQueryStringValue("seleYear")) > 0 ? Utils.GetInt(Utils.GetQueryStringValue("seleYear")) : DateTime.Now.Year;
            Month = Utils.GetInt(Utils.GetQueryStringValue("seleMonth")) > 0 ? Utils.GetInt(Utils.GetQueryStringValue("seleMonth")) : DateTime.Now.Month;
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sd"));
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ed"));
            string tt = Utils.GetQueryStringValue("tt");
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
                this._df = new DateTime(Year, Month, 1);
                this._df = this._df.AddDays(-4);
                this._dt = new DateTime(Year, Month, 1);
                this._dt = this._dt.AddDays(DateTime.DaysInMonth(this._dt.Year, this._dt.Month) + 4);
                DataInit(th, Year, Month, startTime, endTime, tt);
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
        private void DataInit(string th, int year, int month, DateTime? startTime, DateTime? endTime, string tt)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            var bll = new EyouSoft.BLL.ComStructure.BDaoYou();
            startTime = (startTime == null ? null : (DateTime?)startTime);
            endTime = (endTime == null ? null : (DateTime?)endTime);
            var list = bll.GetPaiBanTongJi(this.SiteUserInfo.CompanyId, th, year, month, startTime, endTime, tt, pageIndex, pageSize, ref recordCount);
            if (list != null && list.Count > 0)
            {
                //this.rptList.DataSource = list;
                //this.rptList.DataBind();
                ////绑定分页
                //BindPage();

                var s = new StringBuilder();
                var df = this._df;
                var dt = this._dt;
                var lst = new List<MGuidePlanWork>();

                for (var i = 0; i < list.Count; i++)
                {
                    //当前行
                    s.Append("<tr>");

                    //设置截至日期
                    this._dt = list[i].RDate > dt ? dt : list[i].RDate;
                    s.Append(GetTable(list[i].LDate, list[i].RDate, list[i].TourCode, list[i].TourMark, list[i].SaleMark,list[i].ArriveCity,list[i].ArriveCityFlight,list[i].LeaveCity,list[i].LeaveCityFlight));
                    //设置起始日期
                    this._df = this._dt.AddDays(1);

                    //循环填充剩余记录的出团日期大于上个安排记录的回团日期的单元格内容
                    for (var j = i + 1; j < list.Count; j++)
                    {
                        if (list[j].LDate > list[i].RDate && list[j].LDate > this._dt)
                        {
                            //设置截至日期
                            this._dt = list[j].RDate > dt ? dt : list[j].RDate;
                            s.Append(GetTable(list[j].LDate, list[j].RDate, list[j].TourCode, list[j].TourMark, list[j].SaleMark, list[j].ArriveCity, list[j].ArriveCityFlight, list[j].LeaveCity, list[j].LeaveCityFlight));
                            //设置起始日期
                            this._df = this._dt.AddDays(1);
                            lst.Add(list[j]);
                        }
                    }

                    //根据当前截止日期循环补足单元格
                    for (var d = this._dt; d < dt; d=d.AddDays(1))
                    {
                        s.Append("<td></td>");
                    }
                    s.Append("</tr>");

                    //去掉已排记录
                    foreach (var m in lst)
                    {
                        list.Remove(m);
                    }
                    //重置新行的起始日期
                    this._df = df;
                    //重置新行的截止日期
                    this._dt = dt;
                }

                this.ltlList.Text = s.ToString();
            }
            else
            {
                this.lbMsg.Text = "<tr><td colspan=\"40\" align=\"center\">暂无信息</td></tr>";
            }
        }

        ///// <summary>
        ///// 绑定分页
        ///// </summary>
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
            //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_排班统计_栏目))
            //{
            //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_排班统计_栏目, false);
            //    return;
            //}
        }

        #endregion

        #region 前台使用方法
        /// <summary>
        /// 表头
        /// </summary>
        /// <returns></returns>
        protected string GetHead()
        {
            var h = new StringBuilder();

            //前一个月
            var d = DateTime.DaysInMonth(this._df.Year, this._df.Month);
            for (var i = d - 3; i <= d; i++)
            {
                h.Append("<th align=\"center\" class=\"th-line\">");
                h.Append(i);
                h.Append("</th>");
            }
            //当月
            d = DateTime.DaysInMonth(Year, Month);
            for (var i = 1; i <= d; i++)
            {
                h.Append("<th align=\"center\" class=\"th-line\">");
                h.Append(i);
                h.Append("</th>");
            }
            //后一个月
            for (var i = 1; i <= 5; i++)
            {
                h.Append("<th align=\"center\" class=\"th-line\">");
                h.Append(i);
                h.Append("</th>");
            }
            return h.ToString();
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="ldate">出团时间</param>
        /// <param name="rdate">回团时间</param>
        /// <param name="tourcode">团号</param>
        /// <param name="tourmark">团态标识</param>
        /// <param name="salemark">销售标识</param>
        /// <returns></returns>
        protected string GetTable(DateTime ldate, DateTime rdate, string tourcode, string tourmark,string salemark,string arrivecity,string arriveflight,string leavecity,string leaveflight)
        {
            var t = string.Empty;
            var isShow = false;
            //循环填充起始日期到截止日期的单元格内容
            for (var dt = this._df; dt <= this._dt; dt = dt.AddDays(1))
            {
                t += "<td align=\"center\" class=\"{0}\">";
                if(dt>=ldate&&dt<=rdate)
                {
                    t = string.Format(t,salemark);
                    if (dt == ldate)
                    {
                        t += "<a class=\"a_bt\" href=\"#\">";
                        t += tourcode;
                        t += "</a> <span style=\"display: none;\"><b>抵达城市：</b>";
                        t += arrivecity;
                        t += "<br/><b>抵达航班/时间：</b>";
                        t += arriveflight;
                        t += "<br/></span>";
                        isShow = true;
                    }
                    else if (isShow && !string.IsNullOrEmpty(tourmark))
                    {
                        t += tourmark;
                        isShow = false;
                    }
                    else
                    {
                        if (dt==rdate)
                        {
                            t += "<a class=\"a_bt\" href=\"#\">—</a> <span style=\"display: none;\"><b>离开城市：</b>";
                            t += leavecity;
                            t += "<br/><b>离开航班/时间：</b>";
                            t += leaveflight;
                            t += "<br/></span>";
                        }
                        else
                        {
                            t += "—";
                        }
                    }
                }
                else
                {
                    t = string.Format(t, string.Empty);
                }
                t += "</td>";
            }
            return t;
        }

        #endregion
    }
}
