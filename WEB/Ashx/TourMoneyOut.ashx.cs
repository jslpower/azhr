using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;
using EyouSoft.Model.PlanStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.BLL.PlanStructure;

namespace Web.Ashx
{
    /// <summary>
    /// 报账页面添加,修改,删除计调安排项
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TourMoneyOut : IHttpHandler
    {
        #region attributes
        /// <summary>
        /// 计调安排编号
        /// </summary>
        string AnPaiId = string.Empty;
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string doType = Utils.GetQueryStringValue("dotype");
            AnPaiId = Utils.GetFormValue("planid");

            var uinfo = EyouSoft.Security.Membership.UserProvider.GetUserInfo();
            if (uinfo == null) Utils.RCWE(UtilsCommons.AjaxReturnJson("-1"));

            switch (doType)
            {
                case "Del": DeleteAnPai(); break;
                case "Add": InsertAnPai(); break;
                case "Updata": UpdateAnPai(); break;
                default: break;
            }

            Utils.RCWE(UtilsCommons.AjaxReturnJson("-1"));
        }

        #region private members
        /// <summary>
        /// 获取表单信息
        /// </summary>
        /// <returns></returns>
        MPlanBaseInfo GetFormInfo()
        {
            var anPaiLeiXing = (PlanProject?)Utils.GetEnumValueNull(typeof(PlanProject), Utils.GetFormValue("type"));
            string tourId = Utils.GetFormValue("TourID");
            var uinfo = EyouSoft.Security.Membership.UserProvider.GetUserInfo();

            if (!anPaiLeiXing.HasValue) Utils.RCWE(UtilsCommons.AjaxReturnJson("-1"));
            if (string.IsNullOrEmpty(tourId)) Utils.RCWE(UtilsCommons.AjaxReturnJson("-1"));

            MPlanBaseInfo info = null;
            if (!string.IsNullOrEmpty(AnPaiId)) info = new BPlan().GetModel(PlanProject.导游, AnPaiId);
            if (info == null) info = new MPlanBaseInfo();

            info.CompanyId = uinfo.CompanyId;
            info.DeptId = uinfo.DeptId;
            info.OperatorId = uinfo.UserId;
            info.OperatorName = uinfo.Name;
            info.Confirmation = Utils.GetDecimal(Utils.GetFormValue("txt_confirmation"));
            info.CostDetail = Utils.GetFormValue("txt_costDetail").Trim();
            info.IssueTime = DateTime.Now;
            info.Num = Utils.GetInt(Utils.GetFormValue("txt_num"));
            info.DNum = Utils.GetDecimal(Utils.GetFormValue("txt_num"));
            info.PaymentType = (Payment)Utils.GetInt(Utils.GetFormValue("sel_payment"));
            info.ContactName = Utils.GetFormValue("ContactName");
            info.ContactPhone = Utils.GetFormValue("ContactPhone");
            info.SourceName = Utils.GetFormValue("txt_sourceName").Trim();
            info.SourceId = Utils.GetFormValue("hd_sourceId");
            info.TourId = tourId;
            info.Type = anPaiLeiXing.Value;
            info.Status = PlanState.已落实;
            switch (info.Type)
            {
                case PlanProject.领料:
                    info.SourceName = Utils.GetFormValue("txt_sourceName");
                    info.ContactName = Utils.GetFormValue("txt_contactName");
                    info.PlanGood = new EyouSoft.Model.GovStructure.MGovGoodUse();
                    info.PlanGood.GoodId = info.SourceId;
                    info.PlanGood.CompanyId = info.CompanyId;
                    info.PlanGood.DeptId = info.DeptId;
                    info.PlanGood.GoodName = Utils.GetFormValue("txt_sourceName");
                    info.PlanGood.IssueTime = DateTime.Now;
                    info.PlanGood.Number = info.Num;
                    info.PlanGood.Operator = info.OperatorName;
                    info.PlanGood.OperatorId = info.OperatorId;
                    info.PlanGood.PlanId = info.PlanId;
                    info.PlanGood.Price = Utils.GetDecimal(info.CostDetail);
                    info.PlanGood.UserId = Utils.GetFormValue("txt_UserId");
                    info.PlanGood.UserName = Utils.GetFormValue("txt_contactName");
                    break;
                case PlanProject.火车:
                    info.PlanLargeTime = new List<MPlanLargeTime>();
                    var item = new MPlanLargeTime();
                    item.PlanId = info.PlanId;
                    item.PayNumber = info.Num;
                    item.FreeNumber = Utils.GetInt(Utils.GetFormValue("txt_freeNumber"));
                    info.PlanLargeTime.Add(item);
                    break;
                case PlanProject.景点:
                    info.PlanAttractions = new MPlanAttractions();
                    info.PlanAttractions.PlanId = info.PlanId;
                    info.PlanAttractions.AdultNumber = Utils.GetInt(Utils.GetFormValue("txt_adultNumber"));
                    info.PlanAttractions.ChildNumber = Utils.GetInt(Utils.GetFormValue("txt_childNumber"));
                    break;
                //case PlanProject.涉外游轮:
                case PlanProject.轮船:
                    info.PlanShip = new MPlanShip();
                    info.PlanShip.PlanId = info.PlanId;
                    info.PlanShip.PlanShipPriceList = new List<MPlanShipPrice>();
                    var item1 = new MPlanShipPrice();
                    item1.PlanId = info.PlanId;
                    item1.DNum = Utils.GetDecimal(Utils.GetFormValue("txt_adultNumber"));
                    item1.AdultNumber = Utils.GetInt(Utils.GetFormValue("txt_adultNumber"));
                    item1.AdultNumber = Convert.ToInt32(item1.DNum);
                    item1.ChildNumber = Utils.GetInt(Utils.GetFormValue("txt_childNumber"));
                    info.PlanShip.PlanShipPriceList.Add(item1);
                    info.Num = Convert.ToInt32(info.DNum);
                    break;
                case PlanProject.酒店:
                    info.PlanHotel = new MPlanHotel();
                    info.PlanHotel.PlanId = info.PlanId;
                    info.PlanHotel.FreeNumber = Utils.GetInt(Utils.GetFormValue("txt_freeNumber"));
                    break;
            }

            return info;
        }

        /// <summary>
        /// 写入计调安排
        /// </summary>
        void InsertAnPai()
        {
            var info = GetFormInfo();
            info.PlanCost = 0;
            info.AddStatus = (PlanAddStatus)Utils.GetInt(Utils.GetFormValue("addStatusPlan"));

            int renShu = info.Num;
            decimal dRenShu = info.DNum;
            decimal jinE = info.Confirmation;

            info.Num = 0;
            info.DNum = 0;
            info.Confirmation = 0;

            int bllRetCode = new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(info);

            if (bllRetCode != 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("-1"));

            //写入变更
            var info1 = new EyouSoft.Model.PlanStructure.MPlanCostChange();
            if (jinE < 0) info1.ChangeCost = jinE * -1;
            else info1.ChangeCost = jinE;
            //info1.ChangeType = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.导游报账;
            //if (info.AddStatus == PlanAddStatus.销售报账时添加) info1.ChangeType = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.销售报账;
            if (info.AddStatus == PlanAddStatus.计调报账时添加) info1.ChangeType = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.计调报账;

            info1.IssueTime = DateTime.Now;
            info1.PeopleNumber = renShu;
            info1.PlanId = info.PlanId;
            info1.Remark = string.Empty;
            info1.Type = jinE >= 0;
            info1.FeiYongMingXi = string.Empty;

            if (info.Type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船)
            {
                info1.DNum = dRenShu;
                info1.PeopleNumber = Convert.ToInt32(dRenShu);
            }
            else
            {
                info1.PeopleNumber = renShu;
                info1.DNum = renShu;
            }

            bool bllRetCode1 = new EyouSoft.BLL.PlanStructure.BPlan().AddOrUpdPlanCostChange(info1);

            if (bllRetCode1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "操作失败"));
        }

        /// <summary>
        /// 删除计调安排
        /// </summary>
        void DeleteAnPai()
        {
            if (string.IsNullOrEmpty(AnPaiId)) Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "操作失败"));
            var bllRetCode = new BPlan().DelPlan(AnPaiId);
            Utils.RCWE(UtilsCommons.AjaxReturnJson(bllRetCode ? "1" : "-1", "操作失败"));
        }

        /// <summary>
        /// 更新计调安排
        /// </summary>
        void UpdateAnPai()
        {
            var info = GetFormInfo();
            if (string.IsNullOrEmpty(info.PlanId)) Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "操作失败"));
            var bllRetCode = new BPlan().UpdPlan(info);
            Utils.RCWE(UtilsCommons.AjaxReturnJson(bllRetCode == 1 ? "1" : "-1", "操作失败"));
        }
        #endregion

        #region IsReusable
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
