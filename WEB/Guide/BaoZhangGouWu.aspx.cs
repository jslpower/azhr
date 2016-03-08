using System;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.Guide
{
    using System.Collections.Generic;
    using System.Text;

    using EyouSoft.Common;
    using EyouSoft.Model.HPlanStructure;

    public partial class BaoZhangGouWu : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetFormValue("doType") == "Save")
            {
                Save();
            }
            this.PageInit();
        }

        private void PageInit()
        {
            var m = new EyouSoft.BLL.HPlanStructure.BPlan().GetGouWuShouRuMdl(Utils.GetQueryStringValue("PlanId"));
            if (m!=null&&m.GouMaiChanPin!=null&&m.GouMaiChanPin.Count>0)
            {
                this.rptChanPin.DataSource = m.GouMaiChanPin;
                this.rptChanPin.DataBind();
            }
            else
            {
                this.phTemp.Visible = true;
            }
            var s = "var BaoZhangGouWu={0};";
            s = string.Format(s, m != null ? Newtonsoft.Json.JsonConvert.SerializeObject(m) : "null");

            RegisterScript(s);
        }
        
        /// <summary>
        /// 产品选用初始化
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        protected string SelChanPinInit(string productId)
        {
            var sel = new StringBuilder();
            sel.Append("<select name=\"selChanPin\" class=\"selected\" onchange=\"Page.selChanPin(this)\">");
            sel.AppendFormat("<option value=\"{0}|{1}|{2}\">{2}</option>", 0, 0.00, "请选择产品");
            var m = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(Utils.GetQueryStringValue("sourceid"));
            if (m != null && m.GouWu != null && m.GouWu.ChanPins != null && m.GouWu.ChanPins.Count>0)
            {
                foreach (var mdl in m.GouWu.ChanPins)
                {
                    sel.AppendFormat("<option value=\"{0}|{1}|{2}\" {3}>{2}</option>",mdl.ChanPinId,mdl.FanDianJinE.ToString("F2"),mdl.Name,mdl.ChanPinId.Equals(productId)?"selected=\"selected\"":"");
                }
            }
            sel.Append("</select>");
            return sel.ToString();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            switch (new EyouSoft.BLL.HPlanStructure.BPlan().AddOrUpdGouWuShouRu(this.GetPageVal()))
            {
                case 0:
                    PageResponse(UtilsCommons.AjaxReturnJson("false","保存失败！"));
                    break;
                case 1:
                    PageResponse(UtilsCommons.AjaxReturnJson("true","保存成功！"));
                    break;
                case 2:
                    PageResponse(UtilsCommons.AjaxReturnJson("false","已提交财务！"));
                    break;
            }
        }

        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <returns></returns>
        private MGouWuShouRu GetPageVal()
        {
            var m = new MGouWuShouRu()
                {
                    PlanId = Utils.GetFormValue("PlanId"),
                    LiuShui = Utils.GetDecimal(Utils.GetFormValue("txtLiuShui")),
                    BaoDi = Utils.GetDecimal(Utils.GetFormValue("txtBaoDi")),
                    YingYe = Utils.GetDecimal(Utils.GetFormValue("txtYingYe")),
                    PeopleMoney = Utils.GetDecimal(Utils.GetFormValue("txtAdultMoney")),
                    ChildMoney = Utils.GetDecimal(Utils.GetFormValue("txtChildMoney")),
                    Adult = Utils.GetInt(Utils.GetFormValue("txtAdult")),
                    Child = Utils.GetInt(Utils.GetFormValue("txtChild")),
                    ToCompanyRenTou = Utils.GetDecimal(Utils.GetFormValue("txtToCompanyRenTou")),
                    ToCompanyBaoDi = Utils.GetDecimal(Utils.GetFormValue("txtToCompanyBaoDi")),
                    ToCompanyRenShu = Utils.GetInt(Utils.GetFormValue("txtToCompanyRenShu")),
                    ToCompanyBaoDi2 = Utils.GetDecimal(Utils.GetFormValue("txtToCompanyBaoDi2")),
                    ToCompanyRenShu2 = Utils.GetInt(Utils.GetFormValue("txtToCompanyRenShu2")),
                    ToCompanyFanDian = Utils.GetDecimal(Utils.GetFormValue("txtToCompanyFanDian")),
                    ToCompanyYingYe = Utils.GetDecimal(Utils.GetFormValue("txtToCompanyYingYe")),
                    ToCompanyTiQu = double.Parse(((double)Utils.GetDecimal(Utils.GetFormValue("txtToCompanyTiQu"))/100.00).ToString()),
                    ToCompanyTotal = Utils.GetDecimal(Utils.GetFormValue("txtToCompanyTotal")),
                    ToGuideYingYe = Utils.GetDecimal(Utils.GetFormValue("txtToGuideYingYe")),
                    ToGuideTiQu = double.Parse(((double)Utils.GetDecimal(Utils.GetFormValue("txtToGuideTiQu"))/100.00).ToString()),
                    ToGuideLu = Utils.GetDecimal(Utils.GetFormValue("txtToGuideLu")),
                    ToGuideShui = Utils.GetDecimal(Utils.GetFormValue("txtToGuideShui")),
                    ToGuidePei = Utils.GetDecimal(Utils.GetFormValue("txtToGuidePei")),
                    ToGuideJiao = Utils.GetDecimal(Utils.GetFormValue("txtToGuideJiao")),
                    ToGuideOther = Utils.GetDecimal(Utils.GetFormValue("txtToGuideOther")),
                    ToGuideLiuShui = Utils.GetDecimal(Utils.GetFormValue("txtToGuideLiuShui")),
                    ToGuideTotal = Utils.GetDecimal(Utils.GetFormValue("txtToGuideTotal")),
                    ToLeaderYingYe = Utils.GetDecimal(Utils.GetFormValue("txtToLeaderYingYe")),
                    ToLeaderTiQu = double.Parse(((double)Utils.GetDecimal(Utils.GetFormValue("txtToLeaderTiQu"))/100.00).ToString()),
                    ToLeaderTotal = Utils.GetDecimal(Utils.GetFormValue("txtToLeaderTotal")),
                    OperatorDeptId = this.SiteUserInfo.DeptId,
                    OperatorId = this.SiteUserInfo.UserId,
                    Operator = this.SiteUserInfo.Name,
                    GouMaiChanPin = GetChanPin()
                };
            return m;
        }

        /// <summary>
        /// 获取购买产品
        /// </summary>
        /// <returns></returns>
        IList<MGouMaiChanPin> GetChanPin()
        {
            var items = new List<MGouMaiChanPin>();

            var selChanPin = Utils.GetFormValues("selChanPin");
            var txtShuLiang = Utils.GetFormValues("txtShuLiang");
            var txtFanDian = Utils.GetFormValues("txtFanDian");

            if (selChanPin == null || selChanPin.Length == 0) return items;

            for (var i = 0; i < selChanPin.Length; i++)
            {
                if (string.IsNullOrEmpty(selChanPin[i])) continue;
                var item = new MGouMaiChanPin
                    {
                        PlanId = Utils.GetFormValue("PlanId"),
                        ProductId = selChanPin[i].Split('|')[0],
                        ProductName = selChanPin[i].Split('|')[2],
                        BuyAmount = Utils.GetInt(txtShuLiang[i]),
                        BackMoney = Utils.GetDecimal(txtFanDian[i])
                    };
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// 页面返回
        /// </summary>
        /// <param name="ret"></param>
        private void PageResponse(string ret)
        {
            Response.Clear();
            Response.Write(ret);
            Response.End();
        }
    }
}
