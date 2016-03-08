using System;

namespace EyouSoft.Web.Fin
{
    using EyouSoft.Common.Page;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.PrivsStructure;
    
    public partial class JieKuanShenhe : BackPage
    {
        /// <summary>
        /// 借款编号
        /// </summary>
        protected int debitID = 0;
        protected string verificated = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            debitID = Utils.GetInt(Utils.GetFormValue("debitID"));
            verificated = Utils.GetQueryStringValue("verificated");
            if (debitID > 0)
            {
                Save();
            }
            DataInit();
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void DataInit()
        {
            debitID = Utils.GetInt(Utils.GetQueryStringValue("debitID"));
            if (debitID > 0)
            {
                MDebit model = new BFinance().GetDebit(debitID);
                if (model != null && model.Id > 0)
                {
                    //借款人
                    lbl_borrower.Text = model.Borrower;
                    //预借金额
                    lbl_borrowAmount.Text = UtilsCommons.GetMoneyString(model.BorrowAmount, ProviderToMoney);
                    //预领签单数
                    lbl_preSignNum.Text = model.PreSignNum.ToString();
                    //审批人
                    lbl_approver.Text = SiteUserInfo.Name;
                    //审批时间
                    txt_approveDate.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
                }
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            MDebit model = new MDebit();
            model.CompanyId = CurrentUserCompanyID;
            //实借金额
            model.RealAmount = Utils.GetDecimal(Utils.GetFormValue(txt_realAmount.ClientID));
            //实领签单数
            model.RelSignNum = Utils.GetInt(Utils.GetFormValue(txt_relSignNum.ClientID));
            //审批意见
            model.Approval = Utils.GetFormValue(txt_approval.ClientID);
            //借款编号
            model.Id = debitID;
            //审批人ID
            model.ApproverId = SiteUserInfo.UserId;
            //审批人Name
            model.Approver = SiteUserInfo.Name;
            //审批时间
            model.ApproveDate = DateTime.Now;
            model.Status = EyouSoft.Model.EnumType.FinStructure.FinStatus.账务待支付;
            if (new BFinance().SetDebitApprove(model))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1"));
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
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_借款管理_借款审批))
            {
                Utils.ResponseNoPermit(Privs.财务管理_借款管理_借款审批, true);
                return;
            }
        }
    }
}
