using System;

namespace EyouSoft.Web.Fin
{
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.Model.FinStructure;

    public partial class CaiWuDengJiEdit : BackPage
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
            var model = new BFinance().GetDengJiMdl(CurrentUserCompanyID, Utils.GetInt(Utils.GetQueryStringValue("Id")));

            //财务类型初始化
            seltyp.DataTextField = "Text";
            seltyp.DataValueField = "Value";
            seltyp.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.FinStructure.CaiWuDengJi));
            seltyp.DataBind();

            //发生时间初始化
            txtApplyDate.Value = DateTime.Now.ToShortDateString();

            if (model != null)
            {
                //财务类型
                seltyp.SelectedValue = ((int)model.Typ).ToString();
                //发生时间
                txtApplyDate.Value = model.ApplyDate.ToShortDateString();
                //标题
                txtTitle.Value = model.Title;
                //单位名称
                txtDanWeiNm.Value = model.DanWeiNm;
                //金额
                txtFeeAmount.Value = model.FeeAmount.ToString("0.00");
                //备注
                txtRemark.Text = model.Remark;
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
            var mdl = new MCaiWuDengJi();

            //获取页面数据并验证
            if (GetPageVal(mdl, ref msg))
            {
                PageResponse(UtilsCommons.AjaxReturnJson(new BFinance().AddOrUpdDengJi(mdl) ? "1" : "提交失败!"));
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
        private bool GetPageVal(MCaiWuDengJi model, ref string msg)
        {
            model.Id = Utils.GetInt(Utils.GetFormValue("Id"));
            model.CompanyId = CurrentUserCompanyID;
            model.Typ = (Model.EnumType.FinStructure.CaiWuDengJi)Utils.GetInt(seltyp.SelectedValue);
            model.ApplyDate = Utils.GetDateTime(txtApplyDate.Value);
            if (model.ApplyDate==DateTime.MinValue)
            {
                msg += "请选择发生时间！<br/>";
            }
            model.Title = txtTitle.Value;
            if (string.IsNullOrEmpty(model.Title))
            {
                msg += "请输入财务标题！<br/>";
            }
            model.DanWeiNm = txtDanWeiNm.Value;
            model.FeeAmount = Utils.GetDecimal(txtFeeAmount.Value);
            if (model.FeeAmount==0)
            {
                msg += "请输入金额！<br/>";
            }
            model.Remark = txtRemark.Text;
            model.OperatorDeptId = this.SiteUserInfo.DeptId;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.Operator = this.SiteUserInfo.Name;
            model.IssueTime = DateTime.Now;
            return msg.Length <= 0;
        }
    }
}
