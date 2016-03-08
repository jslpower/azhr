using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Model.HPlanStructure;
using System.Web.UI.WebControls;
using EyouSoft.Model.EnumType.FinStructure;

using System.Data;
using System.Data.SqlTypes;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace EyouSoft.Common
{
    using EyouSoft.Model.ComStructure;

    public class UtilsCommons
    {
        /// <summary>
        /// 获得线路区域的Options
        /// </summary>
        /// <param name="selectIndex">设置选择项</param>
        /// <returns></returns>
        public static string GetAreaLineForSelect(int selectIndex, string companyID)
        {
            StringBuilder sb = new StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComArea> list = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(companyID).Where(m => m.LngType == EyouSoft.Model.EnumType.SysStructure.LngType.中文).ToList();
            sb.Append("<option value='0'>-请选择-</option>");
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].AreaId != selectIndex)
                    {
                        sb.Append("<option data-type='" + ((int)list[i].Type).ToString() + "' value='" + list[i].AreaId + "'>" + list[i].AreaName + "</option>");
                    }
                    else
                    {
                        sb.Append("<option data-type='" + ((int)list[i].Type).ToString() + "' value='" + list[i].AreaId + "' selected=selected>" + list[i].AreaName + "</option>");
                    }
                }
            }
            return sb.ToString();

        }

        /// <summary>
        /// 获得同行端线路区域的Options
        /// </summary>
        /// <param name="selectIndex">设置选择项</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="lngType">语言类型 1中 2英 3泰 null所有</param>
        /// <returns></returns>
        public static string GetAreaLineForSelect(int selectIndex, string companyId, EyouSoft.Model.EnumType.SysStructure.LngType? lngType)
        {
            var sb = new StringBuilder();
            var list = lngType.HasValue ? new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(companyId).Where(m => m.LngType == lngType.Value).ToList() : new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(companyId);
            sb.Append("<option value='0'>-None-</option>");
            if (list != null && list.Count > 0)
            {
                foreach (var t in list)
                {
                    if (t.MasterId != selectIndex)
                    {
                        sb.Append("<option data-type='" + ((int)t.Type) + "' value='" + t.MasterId + "'>" + t.AreaName + "</option>");
                    }
                    else
                    {
                        sb.Append("<option data-type='" + ((int)t.Type) + "' value='" + t.MasterId + "' selected=selected>" + t.AreaName + "</option>");
                    }
                }
            }
            return sb.ToString();

        }

        #region ajax request,response josn data.  create by cyn
        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <returns></returns>
        /// 
        public static string AjaxReturnJson(string retCode)
        {
            return AjaxReturnJson(retCode, string.Empty);
        }
        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <param name="retMsg">return msg</param>
        /// <returns></returns>
        public static string AjaxReturnJson(string retCode, string retMsg)
        {
            return AjaxReturnJson(retCode, retMsg, null);
        }

        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <param name="retMsg">return msg</param>
        /// <param name="retObj">return object</param>
        /// <returns></returns>
        public static string AjaxReturnJson(string retCode, string retMsg, object retObj)
        {
            string output = "{}";

            if (retObj != null)
            {
                output = Newtonsoft.Json.JsonConvert.SerializeObject(retObj);
            }

            return string.Format("{{\"result\":\"{0}\",\"msg\":\"{1}\",\"obj\":{2}}}", retCode, retMsg, output);
        }
        #endregion

        #region 格式转换 create by dyz
        /// <summary>
        /// 金额显示格式处理
        /// </summary>
        /// <param name="m">金额</param>
        /// <param name="name">预定义的 System.Globalization.CultureInfo 名称或现有 System.Globalization.CultureInfo名称</param>
        /// <returns></returns>
        public static string GetMoneyString(decimal m, string name)
        {
            System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture(name);

            return m.ToString("c2", cultureInfo);
        }

        /// <summary>
        /// 金额显示格式处理
        /// </summary>
        /// <param name="o">金额</param>
        /// <param name="name">预定义的 System.Globalization.CultureInfo 名称或现有 System.Globalization.CultureInfo名称</param>
        /// <returns></returns>
        public static string GetMoneyString(object o, string name)
        {
            if (o != null)
            {
                return GetMoneyString(Utils.GetDecimal(o.ToString()), name);
            }

            return string.Empty;
        }

        /// <summary>
        /// 时间显示格式处理
        /// </summary>
        /// <param name="d">时间值</param>
        /// <param name="format">格式字符串。</param>
        /// <returns></returns>
        public static string GetDateString(DateTime d, string format)
        {
            if (d == null || d.ToString() == "" || d.Equals(Utils.GetDateTime("1900-1-1 0:00:00")) || d.Equals(Utils.GetDateTime("0001-01-01 0:00:00")))
            {
                return "";
            }
            else
            {
                return d.ToString(format);
            }
        }
        /// <summary>
        /// 时间显示格式处理
        /// </summary>
        /// <param name="d">时间值</param>
        /// <param name="format">格式字符串。</param>
        /// <returns></returns>
        public static string GetDateString(DateTime? d)
        {
            if (d == null || d.ToString() == "" || d.Equals(Utils.GetDateTime("1900-1-1 0:00:00")) || d.Equals(Utils.GetDateTime("0001-01-01 0:00:00")))
            {
                return "";
            }
            else
            {
                return UtilsCommons.GetDateString(d, "yyyy-MM-dd");
            }
        }
        /// <summary>
        /// 时间显示格式处理
        /// </summary>
        /// <param name="d">时间值</param>
        /// <param name="format">格式字符串。</param>
        /// <returns></returns>
        public static string GetDateString(object d, string format)
        {
            if (d != null)
            {
                return GetDateString(Utils.GetDateTime(d.ToString()), format);
            }

            return string.Empty;
        }
        #endregion

        #region 获取枚举下拉菜单 create by cyn
        /// <summary>
        ///  获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls)
        {
            return GetEnumDDL(ls, string.Empty);
        }
        /// <summary>
        ///  获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, object selectedVal)
        {
            if (selectedVal == null)
            {
                return GetEnumDDL(ls);
            }
            else
            {
                return GetEnumDDL(ls, selectedVal.ToString());
            }

        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选择value值</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal)
        {
            return GetEnumDDL(ls, selectedVal ?? "-1", false);

        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选择value值</param>
        /// <param name="isFirst">是否存在默认请选择项</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, bool isFirst)
        {

            return GetEnumDDL(ls, selectedVal, isFirst, "-1", "-请选择-");
        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选中的值</param>
        /// <param name="defaultKey">默认值Id</param>
        /// <param name="defaultVal">默认值文本</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, string defaultKey, string defaultVal)
        {

            return GetEnumDDL(ls, selectedVal, true, defaultKey, defaultVal);
        }
        /// <summary>
        /// 获取枚举下拉菜单(该方法isFirst为否则后2个属性无效)
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选中的值</param>
        /// <param name="isFirst">是否有默认值</param>
        /// <param name="defaultKey">默认值Id</param>
        /// <param name="defaultVal">默认值文本</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, bool isFirst, string defaultKey, string defaultVal)
        {
            StringBuilder sb = new StringBuilder();
            if (isFirst)
            {
                sb.Append("<option value=\"" + defaultKey + "\" selected=\"selected\">" + defaultVal + "</option>");
            }

            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Value != selectedVal.Trim())
                {
                    sb.Append("<option value=\"" + ls[i].Value.Trim() + "\">" + ls[i].Text.Trim() + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + ls[i].Value.Trim() + "\" selected=\"selected\">" + ls[i].Text.Trim() + "</option>");
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 财务 销售 获取支付方式  create by cyn
        /// <summary>
        /// 财务 销售 获取支付方式
        /// </summary>
        /// <param name="currentUserCompanyID">公司Id</param>
        /// <param name="itemType">收入支出枚举</param>
        /// <returns>支付方式下拉字符串</returns>
        public static string GetStrPaymentList(string currentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType itemType)
        {
            IList<EyouSoft.Model.ComStructure.MComPayment> ls = new EyouSoft.BLL.ComStructure.BComPayment().GetList(currentUserCompanyID, null, itemType);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ls)
                {
                    sb.Append("<option value=" + item.PaymentId + ">" + item.Name + "</option>");
                }
                return sb.ToString();
            }
            return "<option value=-1>-无支付方式-</option>";
        }



        /// <summary>
        /// 财务 销售 获取支付方式 
        /// create by wl
        /// </summary>
        /// <param name="currentUserCompanyID">公司Id</param>
        /// <param name="itemType">收入支出枚举</param>
        /// <returns>支付方式下拉字符串</returns>
        public static string GetStrPaymentList(string currentUserCompanyID, string currentPayModel, EyouSoft.Model.EnumType.ComStructure.ItemType itemType)
        {
            IList<EyouSoft.Model.ComStructure.MComPayment> ls = new EyouSoft.BLL.ComStructure.BComPayment().GetList(currentUserCompanyID, null, itemType);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ls)
                {
                    if (!string.IsNullOrEmpty(currentPayModel) && Utils.GetInt(currentPayModel) == item.PaymentId)
                    {

                        sb.Append("<option selected='selected' value=" + item.PaymentId + ">" + item.Name + "</option>");

                    }
                    else
                    {
                        sb.Append("<option value=" + item.PaymentId + ">" + item.Name + "</option>");
                    }
                }
                return sb.ToString();
            }
            return "<option value=-1>-无支付方式-</option>";
        }
        /// <summary>
        /// 财务 销售 获取支付方式
        /// </summary>
        /// <param name="currentUserCompanyID">公司Id</param>
        /// <param name="itemType">收入支出枚举</param>
        /// <returns>支付方式实体列表</returns>
        public static IList<EyouSoft.Model.ComStructure.MComPayment> GetPaymentList(string currentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType itemType)
        {
            return new EyouSoft.BLL.ComStructure.BComPayment().GetList(currentUserCompanyID, null, itemType);
        }
        #endregion

        #region 返回计调图标
        /// <summary>
        /// 获取计调安排状态显示HTML
        /// </summary>
        /// <param name="status">安排状态</param>
        /// <param name="dataType">dataType</param>
        /// <param name="name">计调安排类型名称</param>
        /// <returns></returns>
        private static string GetJiDaioIconHtml(PlanState status, string dataType, string name)
        {
            if (status == PlanState.未安排)
            {
                return string.Format("<b class=\"fontred\"><span class='a_jidiaoicon'>{0}</span></b>", name);
            }
            else if (status == PlanState.未落实)
            {
                return string.Format("<b class=\"fontred\"  data-class=\"ShowSourceName\" ><span class='a_jidiaoicon'>{0}</span></b><div data-type=\"{1}\" style=\"display: none;\"></div>", name, dataType);
            }
            else if (status == PlanState.无计调任务)
            {
                return string.Format("<b class=\"fontgray\"><span class='a_jidiaoicon'>{0}</span></b>", name);
            }
            else if (status == PlanState.已落实)
            {
                return string.Format("<b class=\"fontblue\"  data-class=\"ShowSourceName\"><span class='a_jidiaoicon'>{0}</span></b><div data-type=\"{1}\" style=\"display: none;\"></div>", name, dataType);
            }

            return string.Empty;
        }

        /// <summary>
        /// 返回计调图标
        /// 无计调任务,未安排:灰色 未落实:红色 其它:蓝色
        /// </summary>
        /// <param name="MTourPlanStatus">计调</param>
        /// <returns></returns>
        public static string GetJiDiaoIcon(MTourPlanStatus MPS)
        {
            if (MPS != null)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(GetJiDaioIconHtml(MPS.DJ, "1", "接"));
                sb.Append(GetJiDaioIconHtml(MPS.Hotel, "2", "房"));
                sb.Append(GetJiDaioIconHtml(MPS.Dining, "3", "餐"));
                sb.Append(GetJiDaioIconHtml(MPS.Spot, "4", "景"));
                sb.Append(GetJiDaioIconHtml(MPS.Car, "5", "车"));
                sb.Append(GetJiDaioIconHtml(MPS.Guide, "7", "导"));
                sb.Append(GetJiDaioIconHtml(MPS.Shopping, "9", "购"));


                string ship = string.Empty;

                if (MPS.CShip == PlanState.无计调任务 || MPS.FShip == PlanState.无计调任务)
                {
                    ship = GetJiDaioIconHtml(PlanState.无计调任务, "6", "船");
                }
                if (MPS.CShip == PlanState.未安排 || MPS.FShip == PlanState.未安排)
                {
                    ship = GetJiDaioIconHtml(PlanState.未安排, "6", "船");
                }
                if (MPS.CShip == PlanState.未落实 || MPS.FShip == PlanState.未落实)
                {
                    ship = GetJiDaioIconHtml(PlanState.未落实, "6", "船");
                }
                if (MPS.CShip == PlanState.已落实 || MPS.FShip == PlanState.已落实)
                {
                    ship = GetJiDaioIconHtml(PlanState.已落实, "6", "船");
                }
                sb.Append(ship);

                string ticket = string.Empty;

                if (MPS.TrainTicket == PlanState.无计调任务 || MPS.PlaneTicket == PlanState.无计调任务 || MPS.CarTicket == PlanState.无计调任务)
                {
                    ticket = GetJiDaioIconHtml(PlanState.无计调任务, "8", "票");
                }
                if (MPS.TrainTicket == PlanState.未安排 || MPS.PlaneTicket == PlanState.未安排 || MPS.CarTicket == PlanState.未安排)
                {
                    ticket = GetJiDaioIconHtml(PlanState.未安排, "8", "票");
                }
                if (MPS.TrainTicket == PlanState.未落实 || MPS.PlaneTicket == PlanState.未落实 || MPS.CarTicket == PlanState.未落实)
                {
                    ticket = GetJiDaioIconHtml(PlanState.未落实, "8", "票");
                }
                if (MPS.TrainTicket == PlanState.已落实 || MPS.PlaneTicket == PlanState.已落实 || MPS.CarTicket == PlanState.已落实)
                {
                    ticket = GetJiDaioIconHtml(PlanState.已落实, "8", "票");
                }
                sb.Append(ticket);

                return sb.ToString();
            }
            else
            {
                return "<b class=\"fontgray\"><span class='a_jidiaoicon'>接</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>房</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>餐</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>景</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>车</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>船</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>导</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>票</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>购</span></b>";
            }
        }
        #endregion

        #region 显示或者隐藏计调项的修改删除
        /// <summary>
        /// 显示或者隐藏计调项的修改删除
        /// </summary>
        /// <param name="status">计调项状态</param>
        /// <returns></returns>
        public static bool GetUpdateAndDeleteByStatus(string tourID, string UserId)
        {
            int bllRetCode = new EyouSoft.BLL.HPlanStructure.BPlan().IsYunXuCaoZuo(tourID, "A", UserId);

            return bllRetCode == 1;
        }
        #endregion

        /// <summary>
        /// 是否导出列表
        /// </summary>
        /// <returns></returns>
        public static bool IsToXls()
        {
            return Utils.GetQueryStringValue("toxls") == "1";
        }

        /// <summary>
        /// 获取导出列表导出的记录数
        /// </summary>
        /// <returns></returns>
        public static int GetToXlsRecordCount()
        {
            return Utils.GetInt(Utils.GetQueryStringValue("toxlsrecordcount"));
        }

        /// <summary>
        /// 获取分页页索引，url页索引查询参数为Page
        /// </summary>
        /// <returns></returns>
        public static int GetPadingIndex()
        {
            return GetPadingIndex("Page");
        }

        /// <summary>
        /// 获取分页页索引
        /// </summary>
        /// <param name="s">url页索引查询参数</param>
        /// <returns></returns>
        public static int GetPadingIndex(string s)
        {
            int index = Utils.GetInt(Utils.GetQueryStringValue(s), 1);

            return index < 1 ? 1 : index;
        }

        /// <summary>
        /// 给属于推荐的公司加上对应的图标
        /// </summary>
        /// <param name="Istuijian">推荐</param>
        /// <param name="Isqiandan">签单</param>
        /// <returns></returns>
        public static string GetCompanyRecommend(object Istuijian, object Isqiandan)
        {
            bool istuijian = Convert.ToBoolean(Istuijian);
            bool isqiandan = Convert.ToBoolean(Isqiandan);
            string Imgstr = string.Empty;
            if (istuijian)
            {
                Imgstr += "<img src=\"/images/jian.gif\" width=\"13px\" height=\"13px\" title=\"推荐\"/> ";
            }
            if (isqiandan)
            {
                Imgstr += " <img src=\"/images/qian.gif\" width=\"13px\" height=\"13px\" title=\"签单\"/>";
            }
            return Imgstr;
        }
        /// <summary>
        /// 获得计划行程集合
        /// </summary>
        /// <returns></returns>
        public static IList<EyouSoft.Model.HTourStructure.MQuotePlan> GetPlanList()
        {
            IList<EyouSoft.Model.HTourStructure.MQuotePlan> list = new List<EyouSoft.Model.HTourStructure.MQuotePlan>();
            //城市
            string[] cityid = Utils.GetFormValues("hidcityids");
            string[] cityname = Utils.GetFormValues("hidcitys");
            //交通
            string[] traffictype = Utils.GetFormValues("hidtraffics");
            string[] trafficprice = Utils.GetFormValues("hidtrafficprices");
            //酒店1
            string[] hotelid1 = Utils.GetFormValues("hidhotel1id");
            string[] hotelid2 = Utils.GetFormValues("hidhotel2id");
            string[] hotelname1 = Utils.GetFormValues("txthotel1");
            //酒店2
            string[] hotelname2 = Utils.GetFormValues("txthotel2");
            string[] hotelprice1 = Utils.GetFormValues("txthotel1price");
            string[] hotelprice2 = Utils.GetFormValues("txthotel2price");
            //早餐
            string[] eatFrist = Utils.GetFormValues("eatFrist");
            string[] breakprice = Utils.GetFormValues("txtbreakprice");
            string[] breakid = Utils.GetFormValues("hidbreak");
            string[] breakname = Utils.GetFormValues("txtbreakname");
            string[] breakmenuid = Utils.GetFormValues("hidbreakmenuid");
            string[] breakfootid = Utils.GetFormValues("hidfastfootid");
            string[] breakpricejs = Utils.GetFormValues("hidfastprice");
            //午餐
            string[] eatSecond = Utils.GetFormValues("eatSecond");
            string[] secondprice = Utils.GetFormValues("txtsecondprice");
            string[] secondid = Utils.GetFormValues("hidsecond");
            string[] secondname = Utils.GetFormValues("txtsecondname");
            string[] secondmenuid = Utils.GetFormValues("hidsecondmenuid");
            string[] secondfootid = Utils.GetFormValues("hidsecondfootid");
            string[] secondpricejs = Utils.GetFormValues("hidsecondprice");
            //晚餐
            string[] eatThird = Utils.GetFormValues("eatThird");
            string[] thirdprice = Utils.GetFormValues("txtthirdprice");
            string[] thirdid = Utils.GetFormValues("hidthird");
            string[] thirdname = Utils.GetFormValues("txtthirdname");
            string[] thirdmenuid = Utils.GetFormValues("hidthirdmenuid");
            string[] thirdfootid = Utils.GetFormValues("hidthirdfootid");
            string[] thirdpricejs = Utils.GetFormValues("hidthirdprice");
            //景区
            string[] spotId = Utils.GetFormValues("hd_scenery_spot");
            string[] sportName = Utils.GetFormValues("show_scenery_spot");

            //行程内容
            string[] Content = Utils.GetFormEditorValues("txtContent");

            //景点图片
            string[] filepath = Utils.GetFormValues("filepath");
            string[] spotpricejss = Utils.GetFormValues("hidpricejs");//结算价
            string[] spotpriceths = Utils.GetFormValues("hidpriceth");//同行价

            //购物供应商编号
            string[] shopids = Utils.GetFormValues("hidshopid");
            //购物供应商名称
            string[] shopnames = Utils.GetFormValues("hidshopname");

            if (cityid.Length > 0)
            {
                for (int i = 0; i < cityid.Length; i++)
                {
                    if (cityid[i] != "" && cityid[i] != "0")
                    {
                        EyouSoft.Model.HTourStructure.MQuotePlan model = new EyouSoft.Model.HTourStructure.MQuotePlan();
                        model.FilePath = filepath[i];
                        model.BreakfastMenu = breakname[i];
                        model.BreakfastMenuId = breakmenuid[i];
                        model.BreakfastPrice = Utils.GetDecimal(breakprice[i].ToString());
                        model.BreakfastRestaurantId = breakid[i];
                        model.BreakfastId = breakfootid[i];
                        model.BreakfastSettlementPrice = Utils.GetDecimal(breakpricejs[i]);

                        model.Content = Content[i];
                        model.Days = (i + 1);
                        model.FilePath = filepath[i];

                        if (hotelname1[i].ToString() != "" && hotelid1[i].ToString() != "")
                        {
                            model.HotelId1 = hotelid1[i];
                            model.HotelName1 = hotelname1[i];
                            model.HotelPrice1 = Utils.GetDecimal(hotelprice1[i].ToString());
                        }
                        if (hotelid2[i].ToString() != "" && hotelname2[i].ToString() != "")
                        {
                            model.HotelId2 = hotelid2[i];
                            model.HotelName2 = hotelname2[i];
                            model.HotelPrice2 = Utils.GetDecimal(hotelprice2[i].ToString());
                        }
                        model.IsBreakfast = Convert.ToBoolean(Utils.GetInt(eatFrist[i].ToString()));
                        model.IsLunch = Convert.ToBoolean(Utils.GetInt(eatSecond[i].ToString()));
                        model.IsSupper = Convert.ToBoolean(Utils.GetInt(eatThird[i].ToString()));
                        model.LunchMenu = secondname[i];
                        model.LunchMenuId = secondmenuid[i];
                        model.LunchPrice = Utils.GetDecimal(secondprice[i].ToString());
                        model.LunchSettlementPrice = Utils.GetDecimal(secondpricejs[i]);
                        model.LunchRestaurantId = secondid[i];
                        model.LunchId = secondfootid[i];
                        model.SupperMenu = thirdname[i];
                        model.SupperMenuId = thirdmenuid[i];
                        model.SupperPrice = Utils.GetDecimal(thirdprice[i].ToString());
                        model.SupperSettlementPrice = Utils.GetDecimal(thirdpricejs[i]);
                        model.SupperRestaurantId = thirdid[i];
                        model.SupperId = thirdfootid[i];
                        //model.Traffic = Utils.GetEnumValue<EyouSoft.Model.EnumType.PlanStructure.PlanProject>(traffictype[i], PlanProject.火车);
                        //model.TrafficPrice = Utils.GetDecimal(trafficprice[i].ToString());


                        IList<MQuoteShop> shopList = new List<MQuoteShop>();
                        if (shopids.Length > 0)
                        {
                            string[] shopid = shopids[i].Split(',');
                            string[] shopname = shopnames[i].Split(',');
                            for (int k = 0; k < shopid.Length; k++)
                            {
                                if (!string.IsNullOrEmpty(shopid[k]))
                                {
                                    MQuoteShop shopmodel = new MQuoteShop();
                                    shopmodel.ShopId = shopid[k];
                                    shopmodel.ShopName = shopname[k];
                                    shopList.Add(shopmodel);
                                }
                            }
                        }

                        model.QuotePlanShopList = shopList;



                        IList<MQuotePlanCity> citylist = new List<MQuotePlanCity>();

                        if (cityid.Length > 0)
                        {
                            string[] cityids = cityid[i].Split(',');
                            string[] citynames = cityname[i].Split(',');
                            string[] traffictypes = traffictype[i].Split(',');
                            string[] trafficprices = trafficprice[i].Split(',');
                            if (cityids.Length > 0)
                            {
                                for (int o = 0; o < cityids.Length; o++)
                                {
                                    MQuotePlanCity citymodel = new MQuotePlanCity();
                                    citymodel.CityId = Utils.GetInt(cityids[o]);
                                    citymodel.CityName = citynames[o];
                                    citymodel.JiaoTong = Utils.GetEnumValue<EyouSoft.Model.EnumType.PlanStructure.PlanProject>(traffictypes[o], PlanProject.火车);
                                    citymodel.JiaoTongJiaGe = Utils.GetDecimal(trafficprices[o].ToString());
                                    citylist.Add(citymodel);
                                }
                            }
                            else
                            {
                                MQuotePlanCity citymodel = new MQuotePlanCity();
                                citymodel.CityId = Utils.GetInt(cityid[i]);
                                citymodel.CityName = cityname[i];
                                citylist.Add(citymodel);
                            }
                        }

                        model.QuotePlanCityList = citylist;



                        IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot> spotlist = new List<EyouSoft.Model.HTourStructure.MQuotePlanSpot>();
                        if (spotId.Length > 0 && spotId[i].Length > 0)
                        {
                            string[] spotIdArray = spotId[i].Split(',');
                            string[] spotNameArray = sportName[i].Split(',');
                            string[] spotpricejs = spotpricejss[i].Split(',');
                            string[] spotpriceth = spotpriceths[i].Split(',');
                            if (spotIdArray.Length > 0)
                            {
                                for (int j = 0; j < spotIdArray.Length; j++)
                                {
                                    EyouSoft.Model.HTourStructure.MQuotePlanSpot spotModel = new EyouSoft.Model.HTourStructure.MQuotePlanSpot();
                                    spotModel.SpotId = spotIdArray[j];
                                    spotModel.SpotName = System.Web.HttpContext.Current.Server.UrlDecode(spotNameArray[j]);
                                    spotModel.Price = Utils.GetDecimal(spotpriceth[j]);
                                    spotModel.SettlementPrice = Utils.GetDecimal(spotpricejs[j]);
                                    spotlist.Add(spotModel);
                                }
                            }
                            else
                            {
                                EyouSoft.Model.HTourStructure.MQuotePlanSpot spotModel = new EyouSoft.Model.HTourStructure.MQuotePlanSpot();
                                spotModel.SpotId = spotId[i];
                                spotModel.SpotName = sportName[i];
                                spotModel.Price = Utils.GetDecimal(spotpriceths[i]);
                                spotModel.SettlementPrice = Utils.GetDecimal(spotpricejss[i]);
                                spotlist.Add(spotModel);
                            }
                        }
                        model.QuotePlanSpotList = spotlist;

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 安排明细
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAPMX(object list, string type)
        {
            StringBuilder str = new StringBuilder();
            if (list != null)
            {
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanHotelRoom>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}&nbsp;/&nbsp;{1}间<br/>", lis[i].RoomType, lis[i].Quantity);
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanHotelRoom>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}&nbsp;/&nbsp;{1}{2}&nbsp;/&nbsp;{3}<br/>", lis[i].RoomType, lis[i].Quantity, lis[i].PriceType.ToString(), lis[i].Total.ToString("C2"));
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanCar>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{2}天数:{0}&nbsp;/&nbsp;金额:{1}<br/>", lis[i].Days, lis[i].SumPrice.ToString("C2"), string.IsNullOrEmpty(lis[i].Models) ? "" : "车型:" + lis[i].Models + "&nbsp;/&nbsp;");
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanLargeFrequency>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("舱位:{0}&nbsp;/&nbsp;乘客类型:{1}&nbsp;/&nbsp;人数:{2}&nbsp;/&nbsp;价格:{3}&nbsp;/&nbsp;保险:{4}&nbsp;/&nbsp;机建费:{5}&nbsp;/&nbsp;燃油费:{6}&nbsp;/&nbsp;折扣:{7}&nbsp;/&nbsp;小计:{8}<br/>", lis[i].SeatType.ToString(), lis[i].AdultsType, lis[i].PepolePayNum, lis[i].FarePrice.ToString("C2"), lis[i].InsuranceHandlFee.ToString("C2"), lis[i].Fee.ToString("C2"), lis[i].Surcharge.ToString("C2"), lis[i].Discount, lis[i].SumPrice.ToString("C2"));
                    }
                }

                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanLargeFrequency>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("价格:{0}&nbsp;/&nbsp;手续费:{1}&nbsp;/&nbsp;付费数量:{2}&nbsp;/&nbsp;免费数量:{3}&nbsp;/&nbsp;小计:{4}<br/>", lis[i].FarePrice.ToString("C2"), lis[i].InsuranceHandlFee.ToString("C2"), lis[i].PepolePayNum, lis[i].FreeNumber, lis[i].SumPrice.ToString("C2"));
                    }
                }

                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanLargeFrequency>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}&nbsp;/&nbsp;{1}&nbsp;/&nbsp;{2}<br/>", lis[i].Numbers, lis[i].SumPrice.ToString("C2"), lis[i].PepolePayNum);
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanLargeFrequency>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat(" 乘客类型:{0}&nbsp;/&nbsp; 保险:{1}&nbsp;/&nbsp;机建费:{2}&nbsp;/&nbsp;燃油附加费:{3}&nbsp;/&nbsp;折扣:{4}&nbsp;/&nbsp;小计:{5}<br/>", lis[i].AdultsType, lis[i].InsuranceHandlFee.ToString("C2"), lis[i].Fee.ToString("C2"), lis[i].Surcharge.ToString("C2"), lis[i].Discount, lis[i].SumPrice.ToString("C2"));
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanAttractions>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}:{1}/{2}+{3}/{4}+{5}/{6}<br/>", lis[i].VisitTime.Value.ToString("yyyy-MM-dd"), lis[i].Attractions, lis[i].AdultNumber, lis[i].ChildNumber, lis[i].AdultPrice.ToString("C2"), lis[i].ChildPrice.ToString("C2"), lis[i].SumPrice.ToString("C2"));
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanDining>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}:成人{1}人,儿童{2}人,{3}桌&nbsp;&nbsp;{4}<br/>", lis[i].MenuName, lis[i].AdultNumber, lis[i].ChildNumber, lis[i].TableNumber, lis[i].SumPrice.ToString("C2"));
                    }
                }

            }
            return str.ToString();
        }


        /// <summary>
        /// 供应商分销商 订单状态的颜色显示
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetOrderStateForHtml(int state)
        {
            StringBuilder sb = new StringBuilder();
            switch (state)
            {
                case 0: sb.Append("<font class=\"Fontred\">未处理</font>"); break;
                case 1: sb.Append("<font class=\"Fontgreen\">已留位</font>"); break;
                case 2: sb.Append("<font class=\"Fontblue\">留位过期</font>"); break;
                case 3: sb.Append("<font class=\"Fontgray\">不受理</font>"); break;
                case 4: sb.Append("<font font-size='12px'>已成交</font>"); break;
                case 5: sb.Append("<font class=\"Fontgray\">已取消</font>"); break;
                case 6: sb.Append("<font class=\"Fontred\">垫付申请审核</font>"); break;
                case 7: sb.Append("<font class=\"Fontgreen\">垫付申请审核成功</font>"); break;
                case 8: sb.Append("<font class=\"Fontgray\">垫付申请审核失败</font>"); break;
                default: break;
            }
            return sb.ToString();
        }


        /// <summary>
        /// 订单状态(用于供应商、分销商，有别于订单状态的枚举)
        /// 王磊 2012-7-30
        /// desc：分销商供应商的订单状态同系统后台的订单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetGroupEndOrderStatus(string value)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value=''>-请选择订单状态-</option>");

            Array values = Enum.GetValues(typeof(EyouSoft.Model.EnumType.TourStructure.OrderStatus));

            //   int[] not = new int[] { (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核, (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核成功, (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核失败 };

            if (values.Length != 0)
            {
                foreach (var item in values)
                {
                    int Value = (int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.OrderStatus), item.ToString());

                    if (Value != (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核 && Value != (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核成功 && Value != (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核失败 && Value != (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.资金超限)
                    {
                        string Text = Enum.GetName(typeof(EyouSoft.Model.EnumType.TourStructure.OrderStatus), item);
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
            }
            return sb.ToString();

        }


        #region 获得游客集合 create by dyz
        public static IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTravelList()
        {
            //旅客编号
            string[] treavelID = Utils.GetFormValues("txt_TravelControl_TreavelID");
            //旅客姓名
            string[] txtUserName = Utils.GetFormValues("txt_TravelControl_Name");
            //旅客类型
            string[] peopleType = Utils.GetFormValues("slt_TravelControl_PeopleType");
            //证件类型
            string[] proveType = Utils.GetFormValues("slt_TravelControl_ProveType");
            //证件号码
            string[] txtProveNumber = Utils.GetFormValues("txt_TravelControl_Prove");
            //性别
            string[] txtUserSex = Utils.GetFormValues("slt_TravelControl_UserSex");
            //电话
            string[] txtPhone = Utils.GetFormValues("txt_TravelControl_Phone");
            //备注
            string[] txtRemarks = Utils.GetFormValues("txt_TravelControl_Remarks");


            IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();
            if (txtUserName.Length > 0 && peopleType.Length > 0 && proveType.Length > 0 && txtProveNumber.Length > 0 && txtUserSex.Length > 0 && txtPhone.Length > 0 && txtRemarks.Length > 0 )
            {
                for (int i = 0; i < txtUserName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(txtUserName[i]))
                    {
                        EyouSoft.Model.TourStructure.MTourOrderTraveller model = new EyouSoft.Model.TourStructure.MTourOrderTraveller();
                        //旅客姓名
                        model.CnName = txtUserName[i];
                        //证件号码
                        model.CardNumber = txtProveNumber[i];
                        //联系电话
                        model.Contact = txtPhone[i];
                        //编号
                        model.TravellerId = treavelID[i];
                        //游客类型
                        model.VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)Utils.GetInt(peopleType[i]);
                        //证件类型
                        model.CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)Utils.GetInt(proveType[i]);
                        //旅客性别
                        model.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(txtUserSex[i]);
                        //备注
                        model.Remark = txtRemarks[i];

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public static IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTravelListS()
        {

            //旅客编号
            string[] travellerId = Utils.GetFormValues("txt_TravelControlS_TreavelID");
            //旅客姓名Cn
            string[] userNameCn = Utils.GetFormValues("txt_TravelControlS_TreavelID_NameCn");
            //旅客姓名En
            string[] userNameEn = Utils.GetFormValues("txt_TravelControlS_TreavelID_NameEn");
            //证件类型
            string[] proveType = Utils.GetFormValues("slt_TravelControlS_ProveType");
            //身份证
            string[] userSfz = Utils.GetFormValues("slt_TravelControlS_Sfz");
            //旅客类型
            string[] peopleType = Utils.GetFormValues("slt_TravelControlS_PeoType");

            //证件号码
            string[] txtProveNumber = Utils.GetFormValues("txt_TravelControlS_CardNum");
            //性别
            string[] txtUserSex = Utils.GetFormValues("slt_TravelControlS_Sex");
            //电话
            string[] txtPhone = Utils.GetFormValues("txt_TravelControlS_ContactTel");
            //备注
            string[] txtRemarks = Utils.GetFormValues("txt_TravelControlS_Remarks");
            //出团提醒
            string[] cbxLeaveMsg = Utils.GetFormValues("cbx_TravelControlS_LeaveMsg");
            //回团提醒
            string[] cbxBackMsg = Utils.GetFormValues("cbx_TravelControlS_BackMsg");
            //证件有效期
            string[] txtValidDate = Utils.GetFormValues("txt_TravelControlS_ValidDate");
            //是否办理
            string[] cbxHandle = Utils.GetFormValues("cbx_TravelControlS_IsBan");
            //签证状体
            string[] visaStatus = Utils.GetFormValues("slt_TravelControlS_State");
            IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();
            if (userNameCn.Length > 0 && peopleType.Length > 0 && proveType.Length > 0 && txtProveNumber.Length > 0 && txtUserSex.Length > 0 && txtPhone.Length > 0 && txtRemarks.Length > 0 && cbxBackMsg.Length > 0 && cbxLeaveMsg.Length > 0)
            {

                for (int i = 0; i < userNameCn.Length; i++)
                {

                    EyouSoft.Model.TourStructure.MTourOrderTraveller model = new EyouSoft.Model.TourStructure.MTourOrderTraveller();

                    //旅客姓名
                    model.CnName = userNameCn[i];
                    model.EnName = userNameEn[i];
                    //证件号码
                    model.CardNumber = txtProveNumber[i];
                    //身份证
                    model.CardId = userSfz[i];
                    //联系电话
                    model.Contact = txtPhone[i];
                    //编号
                    model.TravellerId = travellerId[i];
                    //游客类型
                    model.VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)Utils.GetInt(peopleType[i]);
                    //证件类型
                    model.CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)Utils.GetInt(proveType[i]);
                    //旅客性别
                    model.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(txtUserSex[i]);
                    //备注
                    model.Remark = txtRemarks[i];
                    //出团提醒
                    model.LNotice = cbxLeaveMsg[i] == "1" ? true : false;
                    //回团提醒
                    model.RNotice = cbxBackMsg[i] == "1" ? true : false;
                    //证件有效期
                    model.CardValidDate = txtValidDate[i];
                    //是否办理
                    model.IsCardTransact = cbxHandle[i] == "1" ? true : false;
                    //签证状态
                    model.VisaStatus = (EyouSoft.Model.EnumType.TourStructure.VisaStatus)Utils.GetInt(visaStatus[i]);
                    list.Add(model);

                }
            }
            return list;
        }
        #endregion

        #region 处理默认时间
        /// <summary>
        /// 处理默认时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SetDateTimeFormart(DateTime? dt)
        {
            if (dt == null || dt.Equals(Utils.GetDateTime("1900-1-1 0:00:00")) || dt.Equals(Utils.GetDateTime("0001-01-01 0:00:00")))
            {
                return "";
            }
            else
            {
                return UtilsCommons.GetDateString(dt, "yyyy-MM-dd");
            }
        }
        #endregion

        #region 获得散拼计划价格信息
        public static string GetPriceStandardTable(IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list, string providerToMoney)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                #region 拼接表头
                sb.Append("<table cellspacing='0' cellpadding='0' border='1' bordercolor='#EAB25A' width='100%' class='pp-tableclass2'>");
                sb.Append("<tr class='pp-table-title'>");
                sb.Append("<th height='23' align='center' rowspans='2'>标准</th>");
                #endregion
                //统计是否是第一次循环intcount=0为第一次加载 循环表头 intcount=1第二次 不循环表头
                int intcount = 0;
                //循环报价标准
                foreach (EyouSoft.Model.TourStructure.MTourPriceStandard item in list)
                {
                    if (intcount == 0)
                    {
                        //循环表头
                        foreach (EyouSoft.Model.TourStructure.MTourPriceLevel modelMTourPriceLevel in item.PriceLevel)
                        {
                            sb.Append("<th align='center' colspan='2'>" + modelMTourPriceLevel.LevelName + "</th>");
                        }
                        sb.Append("<tr class='pp-table-title'>");
                        sb.Append("<td align='center'></td>");
                        //循环表头
                        foreach (EyouSoft.Model.TourStructure.MTourPriceLevel modelMTourPriceLevel in item.PriceLevel)
                        {
                            sb.Append("<td align='center'>成人</td><td align='center'>儿童</td>");
                        }
                        sb.Append("<tr>");
                        intcount = 1;
                    }
                    sb.Append("<td align='center' nowrap='nowrap'>" + item.StandardName + "</td>");
                    //循环表体数据
                    foreach (EyouSoft.Model.TourStructure.MTourPriceLevel modelMTourPriceLevel in item.PriceLevel)
                    {
                        sb.Append("<td align='center' nowrap='nowrap'>" + UtilsCommons.GetMoneyString(modelMTourPriceLevel.AdultPrice, providerToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(modelMTourPriceLevel.ChildPrice, providerToMoney) + "</td>");
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 获取前台查看页面
        /// </summary>
        /// <param name="tourid">计划编号</param>
        /// <param name="orderid">订单编号</param>
        /// <param name="ordertype">订单类型</param>
        /// <param name="ordercode">订单号</param>
        /// <returns></returns>
        public static string GetDingDanChaKan(object tourid, object orderid, object ordertype, object ordercode)
        {
            var url = ordercode.ToString();

            switch ((EyouSoft.Model.EnumType.TourStructure.OrderType)ordertype)
            {
                case EyouSoft.Model.EnumType.TourStructure.OrderType.分销商下单:
                case EyouSoft.Model.EnumType.TourStructure.OrderType.代客预定:
                    url = string.Format("<a target='_blank' href='/Sales/SanPinBaoMing.aspx?OrderId={0}&sl={1}'>{2}</a>", orderid, 365, url);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.OrderType.团队计划:
                    url = string.Format("<a target='_blank' href='/Sales/ChanPinEdit.aspx?id={0}&sl={1}'>{2}</a>", tourid, 2, url);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.OrderType.单项服务:
                    url = "<a onclick=\"javascript:Boxy.iframeDialog({iframeUrl: '/Sales/SingleServerEdit.aspx?sl=376&id=" + tourid + "',title: '修改单项业务',modal: true,width: 1000,height: 800})\" href=\"javascript:void(0)\">" + url + "</a>";
                    break;
            }

            return url;
        }

    }
}