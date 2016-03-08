using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.TourStructure;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.WebFX.PrintPage.xz.fxs
{
    public partial class sanpin : System.Web.UI.Page
    {
        protected MUserInfo SiteUserInfo = null;
        protected string ProviderToMoney = "zh-cn";
        protected void Page_Load(object sender, EventArgs e)
        {
            string tourid = EyouSoft.Common.Utils.GetQueryStringValue("tourId");
            string type = EyouSoft.Common.Utils.GetQueryStringValue("type");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            this.Title = PrintTemplateType.分销商平台散拼线路行程单.ToString();
            if (_IsLogin)
            {
                PageInit(tourid, type);
            }
        }

        private void PageInit(string tourid, string type)
        {
            this.txtsourcename.Text = SiteUserInfo.CompanyName;
            this.txtname.Text = SiteUserInfo.Name;
            this.txttel.Text = SiteUserInfo.Telephone;
            this.txtfax.Text = SiteUserInfo.Fax;

            //团实体
            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourSanPinInfo model = null;
            EyouSoft.Model.EnumType.TourStructure.TourType tourtype = bll.GetTourType(tourid);
            switch (tourtype)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品:
                case EyouSoft.Model.EnumType.TourStructure.TourType.团队产品:
                case EyouSoft.Model.EnumType.TourStructure.TourType.线路产品:
                    //跳转到团队打印单
                    EyouSoft.BLL.ComStructure.BComSetting bcom = new EyouSoft.BLL.ComStructure.BComSetting();
                    Response.Redirect(bcom.GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单) + "?tourId=" + tourid);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.单项业务:
                    this.TSelfProject.Visible = false;
                    this.TService.Visible = false;
                    this.TShopping.Visible = false;
                    this.TWarmRemind.Visible = false;
                    return;
            }
            if (type == "")
            {
                model = (EyouSoft.Model.TourStructure.MTourSanPinInfo)bll.GetTourInfo(tourid);
            }
            else
            {
                model = (EyouSoft.Model.TourStructure.MTourSanPinInfo)bll.GetOldTourInfo(tourid, this.SiteUserInfo.CompanyId);
            }
            this.lbTourCode.Text = model.TourCode;
            this.lbRouteName.Text = model.RouteName;
            if (model != null)
            {
                #region 行程
                IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> planinfo = model.TourPlan.OrderBy(m => m.Days).ToList();
                if (planinfo != null && planinfo.Count > 0)
                {
                    StringBuilder strAllDateInfo = new StringBuilder();
                    string Dinner = string.Empty;//包餐(早、中、晚)
                    foreach (EyouSoft.Model.TourStructure.MPlanBaseInfo Plan in planinfo)
                    {
                        if (Plan.Breakfast) { Dinner += "早、"; }
                        if (Plan.Lunch) { Dinner += "中、"; }
                        if (Plan.Supper) { Dinner += "晚、"; }
                        strAllDateInfo.AppendFormat("<table width='696' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td width='35%' class='small_title'><b class='font16'>第{0}天  {6}</b></td><td width='15%' class='small_title'><b class='font14'>交通：{1}</b></td><td width='20%' class='small_title'><b class='font14'>餐：{2}</b></td><td width='30%' class='small_title'><b class='font14'>住宿：{3}</b></td></tr></table><table width='696' border='0' align='center' cellpadding='0' cellspacing='0' class='list_2' style='margin-top:0px;'><tr><td class='td_text' style='border-top:none;' width='{7}'>{4}</td>{5}</tr></table>", Plan.Days.ToString(), Plan.Traffic, Dinner, Plan.Hotel, Plan.Content, string.IsNullOrEmpty(Plan.FilePath) ? "" : "<td style='border-top:none;'><img src='http://" + Request.Url.Authority + Plan.FilePath + "' width='202' height='163' /></td>", Plan.Section, string.IsNullOrEmpty(Plan.FilePath) ? "100%" : "480px");
                        Dinner = string.Empty;
                    }
                    this.lbtourplan.Text = strAllDateInfo.ToString();
                }
                #endregion

                #region 线路特色
                if (string.IsNullOrEmpty(model.PlanFeature))
                {
                    this.TPlanFeature.Visible = false;
                }
                else
                {
                    this.lbPlanFeature.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.PlanFeature);
                }
                #endregion

                #region 计划服务
                if (model.TourService != null)
                {
                    #region 服务标准
                    if (string.IsNullOrEmpty(model.TourService.ServiceStandard))
                    {
                        this.TService.Visible = false;
                    }
                    else
                    {
                        this.lbService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ServiceStandard);
                    }
                    #endregion

                    #region 服务不含
                    if (string.IsNullOrEmpty(model.TourService.NoNeedItem))
                    {
                        this.TNoService.Visible = false;
                    }
                    else
                    {
                        this.lbnoService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.NoNeedItem);
                    }
                    #endregion

                    #region 购物安排
                    if (string.IsNullOrEmpty(model.TourService.ShoppingItem))
                    {
                        this.TShopping.Visible = false;
                    }
                    else
                    {
                        this.lbshopping.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ShoppingItem);
                    }
                    #endregion

                    #region 儿童安排
                    if (string.IsNullOrEmpty(model.TourService.ChildServiceItem))
                    {
                        this.TChildren.Visible = false;
                    }
                    else
                    {
                        this.lbchildren.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.ChildServiceItem);
                    }
                    #endregion

                    #region 自费项目
                    if (string.IsNullOrEmpty(model.TourService.OwnExpense))
                    {
                        this.TSelfProject.Visible = false;
                    }
                    else
                    {
                        this.lbselfproject.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.OwnExpense);
                    }
                    #endregion

                    #region 温馨提醒
                    if (string.IsNullOrEmpty(model.TourService.WarmRemind))
                    {
                        this.TWarmRemind.Visible = false;
                    }
                    else
                    {
                        this.lbwarmremind.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.WarmRemind);
                    }
                    #endregion

                    #region 注意事项
                    if (string.IsNullOrEmpty(model.TourService.NeedAttention))
                    {
                        this.TNeedAttention.Visible = false;
                    }
                    else
                    {
                        this.lbneedattention.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.TourService.NeedAttention);
                    }
                    #endregion
                }
                else
                {
                    this.TPlanService.Visible = false;
                }
                #endregion

                #region 价格组成
                if (model.MTourPriceStandard != null && model.MTourPriceStandard.Count > 0)
                {
                    this.lbPriceStand.Text = GetPriceStandardTable(model.MTourPriceStandard);
                }
                #endregion
            }
        }
        /// <summary>
        /// 拼接价格组成表格
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetPriceStandardTable(IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                #region 拼接表头
                sb.Append("<table width='696' border='0' align='center' cellpadding='0' cellspacing='0' class='list_2'>");
                sb.Append("<tr>");
                sb.Append("<th align='center'>标准</th>");
                #endregion
                //统计是否是第一次循环intcount=0为第一次加载 循环表头 intcount=1第二次 不循环表头
                int intcount = 0;
                //循环报价标准
                foreach (MTourPriceStandard item in list)
                {
                    if (intcount == 0)
                    {
                        sb.Append("<th align='center'>成人价</th><th align='center'>儿童价</th>");
                        sb.Append("<tr>");
                        intcount = 1;
                    }
                    //循环表体数据
                    foreach (MTourPriceLevel modelMTourPriceLevel in item.PriceLevel)
                    {
                        if (modelMTourPriceLevel.LevType == EyouSoft.Model.EnumType.ComStructure.LevType.门市价)
                        {
                            sb.Append("<td align='center'>" + item.StandardName + "</td><td align='center'>" + UtilsCommons.GetMoneyString(modelMTourPriceLevel.AdultPrice, ProviderToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(modelMTourPriceLevel.ChildPrice, ProviderToMoney) + "</td>");
                        }
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            return sb.ToString();
        }
    }
}
