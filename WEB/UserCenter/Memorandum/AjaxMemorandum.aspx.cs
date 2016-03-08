using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.IndStructure;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.EnumType.IndStructure;

namespace Web.UserCenter.Memorandum
{
    public partial class AjaxMemorandum : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 修改人：蔡永辉
        /// 创建时间：2012-4-27
        /// 说明：个人中心：AJAX获得日历
        protected void Page_Load(object sender, EventArgs e)
        {
            string ajaxtype = Utils.GetQueryStringValue("ajaxtype");
            if (!string.IsNullOrEmpty(ajaxtype))
                Ajax(ajaxtype);

            if (!Page.IsPostBack)
            {
                this.CalendarDate.VisibleDate = Utils.GetDateTime(Utils.GetQueryStringValue("date"));
                this.CalendarDate.TodayDayStyle.CssClass = "today";
            }
        }

        #region ajax操作
        private void Ajax(string type)
        {
            switch (type)
            {
                case "MemoCount":
                    GetMemoCount();
                    break;

                case "GetMemoAll":
                    GetHaveMemoByDate();
                    break;
            }
        }
        /// <summary>
        /// 获取一个月之内的备忘录
        /// </summary>
        private void GetHaveMemoByDate()
        {
            string result = "";
            string resultDate = "";
            string[] arguments = Server.UrlDecode(Utils.GetQueryStringValue("argument")).Split('$');
            BIndividual bllBIndividual = new BIndividual();
            if (!string.IsNullOrEmpty(arguments[0]) && !string.IsNullOrEmpty(arguments[1]))
            {
                IList<MMemo> list = bllBIndividual.GetMemoLst(0, SiteUserInfo.UserId,
                    Utils.GetDateTimeNullable(Utils.GetDateTime(arguments[0]).ToShortDateString()),
                    Utils.GetDateTimeNullable(Utils.GetDateTime(arguments[1]).ToShortDateString()));
                if (list != null && list.Count > 0)
                {
                    //定义list  返回日期list
                    List<string> listDate = new List<string>();
                    list.ToList().ForEach(r => { listDate.Add(r.MemoTime.ToString("M")); });
                    //linq循环出重复项 而且求出重复出现次数
                    var retDateCount = from item in listDate   //每一项 
                                       group item by item into gro   //按项分组，没组就是gro
                                       orderby gro.Count() descending   //按照每组的数量进行排序
                                       select new { num = gro.Key, nums = gro.Count() };
                    foreach (var item in retDateCount.Take(retDateCount.Count()))
                    {
                        int count = item.nums;
                        foreach (MMemo modelMMemo in list)
                        {
                            if (modelMMemo != null)
                            {
                                if (item.num == modelMMemo.MemoTime.ToString("M"))
                                {
                                    if (count == item.nums)
                                        result += "<font class='red'>" + item.num + "</font>,您有<font class='red'>" + item.nums + "</font>条事件:</font></br>";
                                    result += "<img src='../../Images/icon_05.gif' style='no-repeat left 2px; padding-left: 15px;float: left;' />";
                                    if (modelMMemo.UrgentType == MemoUrgent.紧急)
                                        result += "<p style='text-indent:2px'><a href='../UserCenter/Memo/MemoInfo.aspx?Id=" + modelMMemo.Id + "' class='fontred' data-title=" + item.num + " title=" + MemoUrgent.紧急.ToString() + ":" + modelMMemo.MemoTitle + ">" + Utils.GetText2(modelMMemo.MemoTitle, 7, true) + "</a></br></p>";
                                    else
                                        result += "<p style='text-indent:2px'><a href='../UserCenter/Memo/MemoInfo.aspx?Id=" + modelMMemo.Id + "' class='ck_date' data-title=" + item.num + " title=" + modelMMemo.MemoTitle + ">" + Utils.GetText2(modelMMemo.MemoTitle, 7, true) + "</a></br></p>";
                                    count--;
                                    //一天的所有记事 循环之后加$ 标记为一个字符串实体 前台js靠这个分隔
                                    if (count == 0)
                                    {
                                        result += "$";
                                        if (!resultDate.Contains(modelMMemo.MemoTime.ToString("M")))
                                            resultDate += modelMMemo.MemoTime.ToString("M") + "$";
                                    }
                                }

                            }
                        }
                    }
                }
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(resultDate.TrimEnd('$'), result.TrimEnd('$')));
            Response.End();
        }

        /// <summary>
        /// 获取备忘事件数量
        /// </summary>
        private void GetMemoCount()
        {
            string result = "";
            string stardate = Utils.GetQueryStringValue("argument");
            BIndividual bllBIndividual = new BIndividual();
            if (!string.IsNullOrEmpty(stardate))
            {
                //获取当前一个月里面的所有备忘录
                IList<MMemo> list = bllBIndividual.GetMemoLst(10, SiteUserInfo.UserId,
                    Utils.GetDateTimeNullable(Utils.GetDateTime(stardate).ToShortDateString()),
                    Utils.GetDateTimeNullable(Utils.GetDateTime(stardate).ToShortDateString()));
                if (list != null && list.Count > 0)
                {
                    string str = Utils.GetDateTime(stardate).ToShortDateString() + ",您有" + list.Count + "条事件:";
                    foreach (MMemo item in list)
                    {
                        str += "<a href='../UserCenter/Memo/MemoInfo.aspx?Id=" + item.Id + "' class='ck_date' title=" + item.MemoTitle + ">" + Utils.GetText2(item.MemoTitle, 15, true) + "</a>";
                    }
                    result = UtilsCommons.AjaxReturnJson("true", str);
                }
                else
                    result = UtilsCommons.AjaxReturnJson("false", "数量为空");
            }
            else
            {
                result = UtilsCommons.AjaxReturnJson("false", "参数为空");
            }

            Response.Clear();
            Response.Write(result);
            Response.End();
        }

        #endregion


        protected void CalendarDate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (!e.Day.IsOtherMonth)
            {
                if (e.Day.Date.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    e.Cell.Style.Add("border", "2px solid #dadada");
                    e.Cell.Style.Add("background-color", "#FECE7E");
                }
                int year = e.Day.Date.Year;
                int month = e.Day.Date.Month;

                DateTime BeginDate = e.Day.Date.AddDays(-(e.Day.Date.Day) + 1);
                DateTime EndDate = e.Day.Date.AddMonths(1).AddDays(-(e.Day.Date.Day));
            }
            else
            {
                e.Cell.Text = string.Empty;
            }
        }
    }
}
