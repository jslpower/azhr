using System;

namespace EyouSoft.Web.Fin
{
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.FinStructure;
    
    public partial class QianDanGuaShiEdit : BackPage
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
            var model = new BFinance().GetGuaShiMdl(CurrentUserCompanyID, Utils.GetInt(Utils.GetQueryStringValue("Id")));

            //签单类别初始化
            seltyp.DataTextField = "Text";
            seltyp.DataValueField = "Value";
            seltyp.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanProject));
            seltyp.DataBind();

            //领用时间初始化
            txtApplyTime.Value = DateTime.Now.ToShortDateString();

            if (model != null)
            {
                TuanHaoXuanYong.TourId = model.TourId;
                TuanHaoXuanYong.TourCode = model.TourCode;

                //财务类型
                seltyp.SelectedValue = ((int)model.Typ).ToString();

                txtCode.Value = model.SignCode;
                txtL.SellsID = model.ApplierId;
                txtL.SellsName = model.Applier;
                txtApplyTime.Value = model.ApplyTime.ToShortDateString();
            }
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
            var msg = string.Empty;
            var mdl = new MQianDanGuaShi();

            //获取页面数据并验证
            if (GetPageVal(mdl, ref msg))
            {
                PageResponse(UtilsCommons.AjaxReturnJson(new BFinance().AddOrUpdGuaShi(mdl) ? "1" : "提交失败!"));
            }
            else
            {
                PageResponse(UtilsCommons.AjaxReturnJson("-1", msg));
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
        /// <param name="model">提交实体</param>
        /// <param name="msg">验证语</param>
        /// <returns></returns>
        private bool GetPageVal(MQianDanGuaShi model, ref string msg)
        {
            model.Id = Utils.GetInt(Utils.GetFormValue("Id")); ;
            model.CompanyId = CurrentUserCompanyID;
            model.TourCode = Utils.GetFormValue(TuanHaoXuanYong.ClientNameTourCode);
            model.TourId = Utils.GetFormValue(TuanHaoXuanYong.ClientNameTourId);
            if (string.IsNullOrEmpty(model.TourId))
            {
                msg += "请选择团号！<br/>";                
            }
            model.Typ = (PlanProject)Utils.GetInt(Utils.GetFormValue(this.seltyp.ClientID));
            model.SignCode = txtCode.Value;
            if (string.IsNullOrEmpty(model.SignCode))
            {
                msg += "请填写签单号！<br/>";
            }
            model.ApplierId = Utils.GetFormValue(txtL.SellsIDClient);
            model.Applier = Utils.GetFormValue(txtL.SellsNameClient);
            if (string.IsNullOrEmpty(model.ApplierId))
            {
                msg += "请选择领用人！<br/>";
            }
            model.ApplyTime = Utils.GetDateTime(txtApplyTime.Value);
            if (model.ApplyTime == DateTime.MinValue)
            {
                msg += "请选择领用时间！<br/>";
            }
            model.OperatorDeptId = this.SiteUserInfo.DeptId;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.Operator = this.SiteUserInfo.Name;
            model.IssueTime = DateTime.Now;
            return msg.Length <= 0;
        }
    }
}
