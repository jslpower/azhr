using System;

namespace EyouSoft.Web.Guide
{
    using EyouSoft.BLL.TourStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.TourStructure;

    public partial class DaoYouBianGengEdit : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utils.GetFormValue("doType") == "Save")
            {
                Save();
            }
            PageInit();

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            var model = new EyouSoft.BLL.TourStructure.BTour().GetTourChangeModel(CurrentUserCompanyID, Utils.GetInt(Utils.GetQueryStringValue("Id")));

            ////发生时间初始化
            //this.txtDate.Value = DateTime.Now.ToShortDateString();

            //if (model != null)
            //{
            //    this.txtDao.SellsID = model.GuideId;
            //    this.txtDao.SellsName = model.GuideNm;
            //    this.txtBian.SellsID = model.OperatorId;
            //    this.txtBian.SellsName = model.Operator;
            //    this.txtDate.Value = model.IssueTime.ToShortDateString();
            //    this.txtContent.Value = model.Content;
            //    this.txtTitle.Value = model.Title;
            //}
            this.ltlBian.Text = this.SiteUserInfo.Name;
            var s = "var DaoYouBianGengEdit={0};";
            s = string.Format(s, model != null ? Newtonsoft.Json.JsonConvert.SerializeObject(model) : "null");

            RegisterScript(s);
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            switch (new EyouSoft.BLL.TourStructure.BTour().TourChangeAddOrUpd(this.GetPageVal()))
            {
                case 0:
                    PageResponse(UtilsCommons.AjaxReturnJson("-1","保存失败！"));
                    break;
                case 1:
                    PageResponse(UtilsCommons.AjaxReturnJson("1"));
                    break;
            }
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
        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <returns></returns>
        MTourPlanChange GetPageVal()
        {
            var tourId = Utils.GetFormValue("tourid");
            if (string.IsNullOrEmpty(tourId)) return null;
            var m = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(tourId);
            if (m==null) return null;
            return new MTourPlanChange()
                {
                    Id = Utils.GetInt(Utils.GetFormValue("id")),
                    CompanyId = CurrentUserCompanyID,
                    TourId = tourId,
                    TourCode = Utils.GetFormValue("tourcode"),
                    RouteName = m.RouteName,
                    AreaName = m.AreaName,
                    Title = Utils.GetFormValue("txttitle"),
                    Content = Utils.GetFormValue("txtcontent"),
                    SaleId = m.SellerId,
                    SellerName = m.SellerName,
                    GuideId = Utils.GetFormValue(this.txtDao.SellsIDClient),
                    GuideNm = Utils.GetFormValue(this.txtDao.SellsNameClient),
                    ChangeType = ChangeType.导游变更,
                    OperatorDeptId = this.SiteUserInfo.DeptId,
                    OperatorId = this.SiteUserInfo.UserId,
                    Operator = this.SiteUserInfo.Name,
                    IssueTime = DateTime.Now,
                };
        }
    }
}
