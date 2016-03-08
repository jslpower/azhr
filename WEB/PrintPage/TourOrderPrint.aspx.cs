using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Common.Function;

namespace EyouSoft.Web.PrintPage
{
    using EyouSoft.Common.Page;

    public partial class TourOrderPrint : BackPage
    {
        protected StringBuilder str = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["txt_isdocx"] == "1")
            {
                ResponseToXls(Request.Form["txt_docx_html"]);
            }

            InitPage();
        }
        /// <summary>
        /// word导出
        /// </summary>
        protected void ResponseToXls(string html)
        {
            string saveFileName = HttpUtility.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", saveFileName));
            Response.ContentType = "application/ms-word";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<html>\n<head>\n<meta http-equiv=Content-Type content=\"text/html; charset=gb2312\">\n<meta name=ProgId content=Word.Document>");
            strHtml.Append("<style>" + "\n" +
                "<!--" + "\n" +
                "BODY { MARGIN: 0px }" + "\n" +
                "TABLE { BORDER-COLLAPSE: collapse }" + "\n" +
                "TD { FONT-SIZE: 12px; WORD-BREAK: break-all; LINE-HEIGHT: 100%; TEXT-DECORATION: none }" + "\n" +
                "BODY { FONT-SIZE: 12px; WORD-BREAK: break-all; TEXT-DECORATION: none;font-family:宋体;mso-bidi-font-family:宋体;mso-font-kerning:0pt }" + "\n" +
                "p.MsoNormal, li.MsoNormal, div.MsoNormal" + "\n" +
                "{mso-style-parent:\"\";" + "\n" +
                "margin:0cm;" + "\n" +
                "margin-bottom:.0001pt;" + "\n" +
                "text-align:justify;" + "\n" +
                "text-justify:inter-ideograph;" +
                "mso-pagination:none;" + "\n" +
                "font-size:10.5pt;" + "\n" +
                "mso-bidi-font-size:12.0pt;" + "\n" +
                "font-family:\"Times New Roman\";" + "\n" +
                "mso-fareast-font-family:宋体;" + "\n" +
                "mso-font-kerning:1.0pt;}" + "\n" +
                "@page" + "\n" +
                "{mso-page-border-surround-header:no;" + "\n" +
                "mso-page-border-surround-footer:no;}" + "\n" +
                "@page Section1" + "\n" +
                "{size:595.3pt 841.9pt;" + "\n" +
                "margin:1.0cm 1.0cm 1.0cm 1.0cm;" + "\n" +
                "mso-header-margin:0cm;" + "\n" +
                "mso-footer-margin:0cm;" + "\n" +
                "mso-paper-source:0;" + "\n" +
                "layout-grid:15.6pt;}" + "\n" +
                "body{ background:#fff; font-size:12px; font-family:Verdana, Geneva, sans-serif,宋体; margin:5px auto; color:#000;}" + "\n" +
                GetClassStyle() +
                "-->" + "\n" +
                "</style>");
            strHtml.Append("</head>\n");
            strHtml.Append("<body lang=ZH-CN style='tab-interval:21.0pt;text-justify-trim:punctuation'>\n<div class=Section1 style='layout-grid:15.6pt'>\n");
            //内容开始
            strHtml.Append(html);
            //内容结束
            strHtml.Append("</div>\n</body>\n</html>");
            //保存现有线路信息到文件
            Random rnd = new Random();
            //获得文件名
            string RouteInfoFileName = DateTime.Now.ToFileTime().ToString() + rnd.Next(1000, 99999).ToString() + ".doc";
            string tmpName = DateTime.Now.ToFileTime().ToString() + rnd.Next(1000, 99999).ToString() + ".doc";
            string WordTemplateFile = "/ExcelDownTemp/default.dot";
            //if (CustomerConfig != null)
            //{
            //    if (!string.IsNullOrEmpty(CustomerConfig.WordTemplate) && CustomerConfig.WordTemplate.Trim() != "")
            //    {
            //        //判断文件是否存在
            //        if (System.IO.File.Exists(Server.MapPath(CustomerConfig.WordTemplate)))
            //        {
            //            if (System.IO.Path.GetExtension(CustomerConfig.WordTemplate) == ".dot")
            //            {
            //                WordTemplateFile = CustomerConfig.WordTemplate;
            //            }
            //        }
            //    }
            //}
            StringValidate objFile = new StringValidate();
            objFile.WriteTextToFile(Server.MapPath("/temp/word/" + RouteInfoFileName), strHtml.ToString());
            //保存到WORD文件
            Adpost.Common.Office.InteropWord objWord = new Adpost.Common.Office.InteropWord();//定义对象
            objWord.Add(Server.MapPath(WordTemplateFile));                                    //打开模板
            objWord.InsertWordFile(Server.MapPath("/temp/word/" + RouteInfoFileName));
            objWord.Width = 28;
            objWord.SaveAs(Server.MapPath("/temp/word/") + tmpName);
            objFile.FileDel(Server.MapPath("/temp/word/" + RouteInfoFileName));
            objWord.Dispose();
            Response.Clear();
            Response.Redirect("/temp/word/" + tmpName);
            Response.End();
        }

        /// <summary>
        /// 拼接class
        /// </summary>
        /// <returns></returns>
        private string GetClassStyle()
        {
            string str = string.Empty;
            str = "body{ background:#fff; font-size:12px; font-family:Verdana, Geneva, sans-serif,宋体; margin:5px auto; color:#000;}" + "\n" +
                   "a{ text-decoration:none; cursor:pointer;}" + "\n" +
                   "table{ border-collapse:collapse; margin-top:7px;}" + "\n" +
                   "tr,td{ border-collapse:collapse;}" + "\n" +
                    ".font14{ font-size:14px;}" + "\n" +
                    ".font16{ font-size:16px;}" + "\n" +
                    ".font24{ font-size:24px;}" + "\n" +
                    ".fontred16{ font-size:16px; color:#f00;}" + "\n" +
                    ".fontred22{ font-size:22px; color:#f00;}" + "\n" +
                    ".hanggao{ line-height:26px;}" + "\n" +
                    ".padd5{ padding-left:5px;}" + "\n" +
                    ".input100{width:100px;}" + "\n" +
                    "input{ border:none 0px; width:50px; text-align:center; font-weight:bold;}" + "\n" +
                    ".small_title{ background:#c5e6f9; border:#c5e6f9 solid 1px; height:30px; line-height:30px;text-align:left; padding-left:5px;}" + "\n" +
                    ".td_text{ padding:5px;line-height:26px;text-align:left; vertical-align:top; }" + "\n" +
                    ".borderbot_1 td{ border-bottom:#676564 solid .5pt}" + "\n" +
                    ".borderline_1{border:#676564 solid 1px;}" + "\n" +
                    ".borderline_1 td{border:#676564 solid 1px;}" + "\n" +
                    ".table_1 td{border:#676564 solid .5pt; line-height:26px; padding-left:5px;}" + "\n" +
                    ".list_1 th,.list_1 td{ line-height:30px;border:#676564 solid .5pt;padding-left:3px;}" + "\n" +
                    ".list_1 th{ padding-right:3px; background:#c5e6f9; font-size:14px; font-weight:bold;}" + "\n" +
                    ".borderbot_2 td{ border-bottom:#676564 dashed .5pt}" + "\n" +
                    ".borderline_2{border:#676564 dashed .5pt;}" + "\n" +
                    ".borderline_2 th,.borderline_2 td{ line-height:30px;border:#676564 dashed .5pt;padding-left:3px;}" + "\n" +
                    ".table_2 td{border:#676564 dashed .5pt; line-height:26px; padding-left:5px;}" + "\n" +
                    ".list_2 th,.list_2 td{ line-height:30px;border:#676564 dashed .5pt;padding-left:3px;}" + "\n" +
                    ".list_2 th{ padding-right:3px; background:#c5e6f9; font-size:14px; font-weight:bold;}" + "\n" +
                    ".borderbot_3 td{ border:0;}" + "\n" +
                    ".borderline_3{border:0;}" + "\n" +
                    ".table_3 td{border:0; line-height:26px; padding-left:5px;}" + "\n" +
                    ".list_3 th,.list_3 td{ line-height:30px;border:0;padding-left:3px;}" + "\n" +
                    ".list_3 th{ padding-right:3px; background:#c5e6f9; font-size:14px; font-weight:bold;}" + "\n" +
                    ".w180{width:180px;}" + "\n" +
                    ".w70{width:70px;}" + "\n" +
                    ".w80{width:80px;}" + "\n" +
                    ".w100{width:100px;}" + "\n" +
                    ".w120{width:120px;}" + "\n";
            return str;
        }




        ///// <summary>
        ///// response to xls
        ///// </summary>
        ///// <param name="s">要写入 HTTP 输出流的字符串。</param>
        ///// <param name="encoding">encoding</param>
        ///// <param name="filename">filename</param>
        //private void ResponseToXls(string s, Encoding encoding, string filename)
        //{
        //    if (string.IsNullOrEmpty(filename)) filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
        //    if (System.IO.Path.GetExtension(filename).ToLower() != ".doc") filename += ".doc";

        //    Response.Clear();
        //    Response.ContentEncoding = encoding;
        //    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
        //    Response.ContentType = "application/msword";
        //    Response.Write(s.ToString());
        //    Response.End();
        //}

        /// <summary>
        /// 订单人数
        /// </summary>
        private int PeopleCount = 0;
        /// <summary>
        /// 合计金额
        /// </summary>
        private decimal HeJiMoney = 0;
        /// <summary>
        /// 合计金额（税）
        /// </summary>
        private decimal HeJiShou = 0;

        private decimal adultprice = 0;
        private decimal childprice = 0;
        protected void InitPage()
        {
            string orderId = Utils.GetQueryStringValue("orderid");
            string tourId = Utils.GetQueryStringValue("tourid");
            EyouSoft.BLL.TourStructure.BTourOrder bllorder = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.BLL.HTourStructure.BTour bllTour = new EyouSoft.BLL.HTourStructure.BTour();
            if (!string.IsNullOrEmpty(orderId))
            {
                EyouSoft.Model.TourStructure.MTourOrderExpand orderModel = bllorder.GetTourOrderExpandByOrderId(orderId);
                PeopleCount = orderModel.Adults + orderModel.Childs;
                adultprice = orderModel.AdultPrice;
                childprice = orderModel.ChildPrice;
                CompanyName2.Value = orderModel.BuyCompanyName;
                this.lblOrderCode.Text = orderModel.OrderCode;
                var m = new BLL.CrmStructure.BCrm().GetInfo(orderModel.BuyCompanyId);
                if (m!=null)
                {
                    this.lblcustomername.Text = m.BrevityCode;
                }
            }
            if (!string.IsNullOrEmpty(tourId))
            {
                var tourInfo = bllTour.GetTourModel(tourId);
                this.lblLDate.Text = tourInfo.LDate.ToShortDateString();
                if (tourInfo.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品)
                {
                    if (tourInfo.Adults > 0)
                    {
                        str.AppendFormat("<tr><td height='30' align='center'><input type='text' value='{0}' class='input100'></td><td align='center'>成人</td><td align='center'>{1}</td><td align='center'><input type='text' value='' class='input100'></td><td align='center'><input type='text' value='{2}' class='input100'></td><td align='center'><input type='text' value='{3}' class='input100'></td><td align='center'><input type='text' value='{4}' id='' class='input100'></td></tr>", PeopleCount.ToString(), tourInfo.RouteName, adultprice.ToString("C2"), (adultprice * PeopleCount).ToString("C2"), (adultprice * PeopleCount - adultprice * PeopleCount / 1.1M).ToString("C2"));
                        HeJiMoney += (adultprice * PeopleCount);
                        HeJiShou += (adultprice * PeopleCount - adultprice * PeopleCount / 1.1M);
                    }
                    if (tourInfo.Childs > 0)
                    {
                        str.AppendFormat("<tr><td height='30' align='center'><input type='text' value='{0}' class='input100'></td><td align='center'>儿童</td><td align='center'>{1}</td><td align='center'><input type='text' value='' class='input100'></td><td align='center'><input type='text' value='{2}' class='input100'></td><td align='center'><input type='text' value='{3}' class='input100'></td><td align='center'><input type='text' value='{4}' id='' class='input100'></td></tr>", PeopleCount.ToString(), tourInfo.RouteName, childprice.ToString("C2"), (childprice * PeopleCount).ToString("C2"), (childprice * PeopleCount - childprice * PeopleCount / 1.1M).ToString("C2"));
                        HeJiMoney += (childprice * PeopleCount);
                        HeJiShou += (childprice * PeopleCount - childprice * PeopleCount / 1.1M);
                    }
                }
                else
                {
                    //房间数量
                    int roomcount = 0;
                    if (tourInfo.TourRoomList != null && tourInfo.TourRoomList.Count > 0)
                    {
                        for (int j = 0; j < tourInfo.TourRoomList.Count; j++)
                        {
                            roomcount += tourInfo.TourRoomList[j].Num;
                        }
                    }

                    //行程安排
                    var tourplan = tourInfo.TourPlanList;
                    //风味餐
                    var footlist = tourInfo.TourFootList;
                    //小费
                    var tiplist = tourInfo.TourTipList;
                    if (tourplan != null && tourplan.Count > 0)
                    {
                        for (int i = 0; i < tourplan.Count; i++)
                        {
                            //城市交通信息
                            TrafficHtml(tourplan[i].TourPlanCityList, tourInfo.LDate.AddDays(tourplan[i].Days));
                            //酒店
                            if (!string.IsNullOrEmpty(tourplan[i].HotelName1))
                                HotelHtml(tourplan[i].HotelName1, tourplan[i].HotelPrice1, tourInfo.LDate.AddDays(tourplan[i].Days), roomcount);
                            //用餐
                            if (tourplan[i].IsBreakfast)
                                DinnerHtml("早餐", tourplan[i].BreakfastMenu, tourplan[i].BreakfastPrice, tourInfo.LDate.AddDays(tourplan[i].Days));
                            if (tourplan[i].IsLunch)
                                DinnerHtml("中餐", tourplan[i].LunchMenu, tourplan[i].LunchPrice, tourInfo.LDate.AddDays(tourplan[i].Days));
                            if (tourplan[i].IsSupper)
                                DinnerHtml("晚餐", tourplan[i].SupperMenu, tourplan[i].SupperPrice, tourInfo.LDate.AddDays(tourplan[i].Days));
                            //景点
                            if (tourplan[i].TourPlanSpotList != null && tourplan[i].TourPlanSpotList.Count > 0)
                            {
                                foreach (var item in tourplan[i].TourPlanSpotList)
                                {
                                    ScenicHtml(item.SpotName, item.Price, tourInfo.LDate.AddDays(tourplan[i].Days));
                                }
                            }
                        }
                    }
                    //风味餐
                    if (footlist != null && footlist.Count > 0)
                    {
                        foreach (var item in footlist)
                        {
                            if (string.IsNullOrEmpty(item.FootId.Trim()) && !string.IsNullOrEmpty(item.Menu))
                            {
                                FengWeiCanHtml(item.Menu, item.Price);
                            }
                        }
                    }
                    //小费
                    if (tiplist != null && tiplist.Count > 0)
                    {
                        foreach (var item in tiplist)
                        {
                            if (!string.IsNullOrEmpty(item.Tip))
                            {
                                TipHtml(item.Tip, item.Price, item.Days);
                            }
                        }
                    }

                }
                //合计
                HeJiHtml();
                var m = new EyouSoft.BLL.TourStructure.BTourOrder().GetOrderMoney(Utils.GetQueryStringValue("orderid"));
                if(m!=null)
                this.txtAmount.Value = m.ReceivedMoney.ToString("C2");
            }
        }
        /// <summary>
        /// 合计
        /// </summary>
        private void HeJiHtml()
        {
            if (string.IsNullOrEmpty(str.ToString())) return;
            str.AppendFormat("<tr><td height='30' align='right' style='font-weight:bold' colspan='5'>合计：</td><td height='30'  align='center'><input class='input100' type='text' style='font-weight:bold' value='" + HeJiMoney.ToString("C2") + "'></td><td  align='center'><input class='input100' type='text' style='font-weight:bold' value='" + HeJiShou.ToString("C2") + "'></td></tr>");
        }

        /// <summary>
        /// 小费
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        private void TipHtml(string name, decimal price, int days)
        {
            str.AppendFormat("<tr><td height='30' align='center'><input type='text' value='{0}' class='input100'></td><td align='center'>小费</td><td align='center'>{1}</td><td align='center'><input type='text' value='' class='input100'></td><td align='center'><input type='text' value='{2}' class='input100'></td><td align='center'><input type='text' value='{3}' class='input100'></td><td align='center'><input type='text' value='{4}' id='' class='input100'></td></tr>", days.ToString(), name, price.ToString("C2"), (price * days).ToString("C2"), (price * days - price * days / 1.1M).ToString("C2"));
            HeJiMoney += (price * days);
            HeJiShou += (price * days - price * days / 1.1M);
        }
        /// <summary>
        /// 风味餐
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        private void FengWeiCanHtml(string name, decimal price)
        {
            str.AppendFormat("<tr><td height='30' align='center'><input type='text' value='{0}' class='input100'></td><td align='center'>风味餐</td><td align='center'>{1}</td><td align='center'><input type='text' value='' class='input100'></td><td align='center'><input type='text' value='{2}' class='input100'></td><td align='center'><input type='text' value='{3}' class='input100'></td><td align='center'><input type='text' value='{4}' id='' class='input100'></td></tr>", PeopleCount.ToString(), name, price.ToString("C2"), (price * PeopleCount).ToString("C2"), (price * PeopleCount - price * PeopleCount / 1.1M).ToString("C2"));
            HeJiMoney += (price * PeopleCount);
            HeJiShou += (price * PeopleCount - price * PeopleCount / 1.1M);
        }

        /// <summary>
        /// 景点
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private void ScenicHtml(string name, decimal price, DateTime date)
        {
            str.AppendFormat("<tr><td height='30' align='center'><input type='text' value='{0}' class='input100'></td><td align='center'>景点</td><td align='center'>{1}</td><td align='center'><input type='text' value='{2}' class='input100'></td><td align='center'><input type='text' value='{3}' class='input100'></td><td align='center'><input type='text' value='{4}' class='input100'></td><td align='center'><input type='text' value='{5}' id='' class='input100'></td></tr>", PeopleCount.ToString(), name, date.ToString("yyyy-MM-dd"), price.ToString("C2"), (price * PeopleCount).ToString("C2"), (price * PeopleCount - price * PeopleCount / 1.1M).ToString("C2"));
            HeJiMoney += (price * PeopleCount);
            HeJiShou += (price * PeopleCount - price * PeopleCount / 1.1M);
        }

        /// <summary>
        /// 用餐
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private void DinnerHtml(string type, string name, decimal price, DateTime date)
        {
            str.AppendFormat("<tr><td height='30' align='center'><input type='text' value='{0}' class='input100'></td><td align='center'>" + type + "</td><td align='center'>{1}</td><td align='center'><input type='text' value='{2}' class='input100'></td><td align='center'><input type='text' value='{3}' class='input100'></td><td align='center'><input type='text' value='{4}' class='input100'></td><td align='center'><input type='text' value='{5}' id='' class='input100'></td></tr>", PeopleCount.ToString(), name, date.ToString("yyyy-MM-dd"), price.ToString("C2"), (price * PeopleCount).ToString("C2"), (price * PeopleCount - price * PeopleCount / 1.1M).ToString("C2"));
            HeJiMoney += (price * PeopleCount);
            HeJiShou += (price * PeopleCount - price * PeopleCount / 1.1M);
        }
        /// <summary>
        /// 酒店信息
        /// </summary>
        /// <param name="hotelname"></param>
        /// <param name="price"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private void HotelHtml(string hotelname, decimal price, DateTime date, int roomcount)
        {
            str.AppendFormat("<tr><td height='30' align='center'><input type='text' value='{0}' class='input100'></td><td align='center'>酒店</td><td align='center'>{1}</td><td align='center'><input type='text' value='{2}' class='input100'></td><td align='center'><input type='text' value='{3}' class='input100'></td><td align='center'><input type='text' value='{4}' class='input100'></td><td align='center'><input type='text' value='{5}' id='' class='input100'></td></tr>", roomcount.ToString(), hotelname, date.ToString("yyyy-MM-dd"), price.ToString("C2"), (price * roomcount).ToString("C2"), (price * roomcount - price * roomcount / 1.1M).ToString("C2"));
            HeJiMoney += (price * roomcount);
            HeJiShou += (price * roomcount - price * roomcount / 1.1M);

        }

        /// <summary>
        /// 行程城市信息
        /// </summary>
        /// <param name="PlanCity"></param>
        /// <returns></returns>
        private void TrafficHtml(IList<MTourPlanCity> PlanCity, DateTime date)
        {
            if (PlanCity != null && PlanCity.Count > 0)
            {
                for (int i = 0; i < PlanCity.Count; i++)
                {
                    str.AppendFormat("<tr><td height='30' align='center'><input type='text' value='{0}' class='input100'></td><td align='center'>{6}</td><td align='center'>{1}</td><td align='center'><input type='text' value='{2}' class='input100'></td><td align='center'><input type='text' value='{3}' class='input100'></td><td align='center'><input type='text' value='{4}' class='input100'></td><td align='center'><input type='text' value='{5}' id='' class='input100'></td></tr>", PeopleCount.ToString(), PlanCity[i].JiaoTong + " 到 " + PlanCity[i].CityName, date.ToString("yyyy-MM-dd"), PlanCity[i].JiaoTongJiaGe.ToString("C2"), (PeopleCount * PlanCity[i].JiaoTongJiaGe).ToString("C2"), (PeopleCount * PlanCity[i].JiaoTongJiaGe - PeopleCount * PlanCity[i].JiaoTongJiaGe / 1.1M).ToString("C2"),PlanCity[i].JiaoTong);
                    HeJiMoney += (PlanCity[i].JiaoTongJiaGe * PeopleCount);
                    HeJiShou += (PlanCity[i].JiaoTongJiaGe * PeopleCount - PlanCity[i].JiaoTongJiaGe * PeopleCount / 1.1M);
                }
            }
        }
    }
}
