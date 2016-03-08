using System;

namespace EyouSoft.Web.Fin
{
    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.BLL.HTourStructure;
    using EyouSoft.Model.HTourStructure;
    
    public partial class LiRunFenPei : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            if (Utils.GetQueryStringValue("doType") == "Save")
            {
                Save();
            }
            //根据ID初始化页面
            PageInit();
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            var model = new BTour().GetTourModel(Utils.GetQueryStringValue("TourId"));
            if (model != null)
            {
                lbl_uName.Text = model.SellerName;
                txt_Price.Text = Utils.FilterEndOfTheZeroDecimal(model.SumPrice);
                //pan_tourTye.Visible =
                //    model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼 ||
                //    model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼 ||
                //    model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼;

            }
            string distributeId = Utils.GetQueryStringValue("id");
            if (distributeId.Length > 0)
            {
                MProfitDistribute DistributeModel = new BFinance().GetProfitDistribute(Utils.GetInt(distributeId), CurrentUserCompanyID);
                txt_distributeType.Text = DistributeModel.DistributeType;
                lbl_uName.Text = DistributeModel.Staff;

                txt_amount.Text = Utils.FilterEndOfTheZeroDecimal(DistributeModel.Amount);
                txt_remark.Text = DistributeModel.Remark;
                txt_OrderList.Text = DistributeModel.OrderCode;
                DistributeModel.OrderId = DistributeModel.OrderId ?? string.Empty;
                pan_tourTye.Visible = pan_tourTye.Visible || DistributeModel.OrderId.Length > 0;
            }
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {

        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string doType = Utils.GetQueryStringValue("doType");
            MProfitDistribute model = new MProfitDistribute();
            string msg = string.Empty;

            if (GetPageVal(model, ref msg))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson((model.Id > 0 ? new BFinance().UpdProfitDistribute(model) : new BFinance().AddProfitDistribute(model)) ? "1" : "-1", "提交失败!"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", msg));
            }
        }
        /// <summary>
        /// 获取页面数据
        /// </summary>
        private bool GetPageVal(MProfitDistribute model, ref string msg)
        {
            model.Id = Utils.GetInt(Utils.GetFormValue("Id"));
            if (Utils.GetFormValue("radio") == "order")
            {
                model.OrderId = Utils.GetFormValue("Ids");
                model.OrderCode = Utils.GetFormValue("OrderList");
                msg += model.OrderId.Length > 0 ? string.Empty : "请选择订单";
            }
            model.TourId = Utils.GetFormValue("TourId");
            model.TourCode = Utils.GetFormValue("TourCode");
            //分配类型
            model.DistributeType = Utils.GetFormValue(txt_distributeType.ClientID);
            //分配金额
            model.Amount = Utils.GetDecimal(Utils.GetFormValue(txt_amount.ClientID));
            //公司编号
            model.CompanyId = CurrentUserCompanyID;
            //操作时间
            model.IssueTime = DateTime.Now;
            //操作人编号
            model.OperatorId = SiteUserInfo.UserId;
            model.Staff = Utils.GetFormValue("Staffs");
            model.Gross = Utils.GetDecimal(Utils.GetFormValue("Price"));
            //备注
            model.Remark = Utils.GetFormValue(txt_remark.ClientID);
            return msg.Length <= 0;


        }
    }
}
