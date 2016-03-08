﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common.Function;
using System.Text;

namespace EyouSoft.Web.MasterPage.GroupEnd
{
    /// <summary>
    /// 分销商打印模版页
    /// </summary>
    public partial class Print : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 公章
        /// </summary>
        protected string DepartStamp = string.Empty;
        /// <summary>
        /// 页眉
        /// </summary>
        protected string PageHeadFile = string.Empty;
        /// <summary>
        /// 页脚
        /// </summary>
        protected string PageFootFile = string.Empty;
        protected MUserInfo SiteUserInfo = null;
        protected EyouSoft.Model.CrmStructure.MCrm CustomerConfig = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (SiteUserInfo == null)
            {
                EyouSoft.Model.SysStructure.MSysDomain sysDomain = EyouSoft.Security.Membership.UserProvider.GetDomain();
                if (sysDomain != null)
                {
                    EyouSoft.BLL.SysStructure.BSys Customer = new EyouSoft.BLL.SysStructure.BSys();

                    CustomerConfig = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(sysDomain.CompanyId);

                    if (CustomerConfig != null)
                    {
                        DepartStamp = "http://" + Request.Url.Authority + CustomerConfig.Seal;
                        PageHeadFile = "http://" + Request.Url.Authority + CustomerConfig.PrintHeader;
                        PageFootFile = "http://" + Request.Url.Authority + CustomerConfig.PrintFooter;
                    }
                }
            }
            else
            {
                CustomerConfig = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(SiteUserInfo.TourCompanyInfo.CompanyId);
                if (CustomerConfig != null)
                {
                    DepartStamp = "http://" + Request.Url.Authority + CustomerConfig.Seal;
                    PageHeadFile = "http://" + Request.Url.Authority + CustomerConfig.PrintHeader;
                    PageFootFile = "http://" + Request.Url.Authority + CustomerConfig.PrintFooter;
                }

            }
            this.ibtnWord.Attributes.Add("onclick", "ReplaceInput();");
        }

        /// <summary>
        /// word导出
        /// </summary>
        protected void ibtnWord_Click(object sender, ImageClickEventArgs e)
        {
            string printHtml = Request.Form["hidPrintHTML"];
            string saveFileName = HttpUtility.UrlEncode(this.hidDocName.Value + ".doc");
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
            strHtml.Append(printHtml);
            //内容结束
            strHtml.Append("</div>\n</body>\n</html>");
            //保存现有线路信息到文件
            Random rnd = new Random();
            //获得文件名
            string RouteInfoFileName = DateTime.Now.ToFileTime().ToString() + rnd.Next(1000, 99999).ToString() + ".doc";
            string tmpName = DateTime.Now.ToFileTime().ToString() + rnd.Next(1000, 99999).ToString() + ".doc";
            string WordTemplateFile = "/ExcelDownTemp/default.dot";
            if (CustomerConfig != null)
            {
                if (!string.IsNullOrEmpty(CustomerConfig.PrintTemplates) && CustomerConfig.PrintTemplates.Trim() != "")
                {
                    //判断文件是否存在
                    if (System.IO.File.Exists(Server.MapPath(CustomerConfig.PrintTemplates)))
                    {
                        if (System.IO.Path.GetExtension(CustomerConfig.PrintTemplates) == ".dot")
                        {
                            WordTemplateFile = CustomerConfig.PrintTemplates;
                        }
                    }
                }
            }
            StringValidate objFile = new StringValidate();
            objFile.WriteTextToFile(Server.MapPath("/temp/word/" + RouteInfoFileName), strHtml.ToString());
            //保存到WORD文件
            Adpost.Common.Office.InteropWord objWord = new Adpost.Common.Office.InteropWord();//定义对象
            objWord.Add(Server.MapPath(WordTemplateFile));                                    //打开模板
            objWord.InsertWordFile(Server.MapPath("/temp/word/" + RouteInfoFileName));
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

                    ".borderbot_1 td{ border-bottom:#676564 solid 1px}" + "\n" +
                    ".borderline_1{border:#676564 solid 1px;}" + "\n" +
                    ".table_1 td{border:#676564 solid 1px; line-height:26px; padding-left:5px;}" + "\n" +
                    ".list_1 th,.list_1 td{ line-height:30px;border:#676564 solid 1px;padding-left:3px;}" + "\n" +
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
                    ".list_3 th{ padding-right:3px; background:#c5e6f9; font-size:14px; font-weight:bold;}" + "\n";
            return str;
        }
    }
}
