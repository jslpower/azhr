using System;

namespace EyouSoft.Web.Fin
{
    using EyouSoft.Common.Page;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Common;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.BLL.ComStructure;
    
    public partial class JieKuanZhiFu : BackPage
    {
        /// <summary>
        /// 借款编号
        /// </summary>
        protected int debitID = 0;
        protected bool IsEnableKis;
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
            //系统配置实体
            MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID) ?? new MComSetting();
            IsEnableKis = comModel.IsEnableKis;
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
                    //实借金额
                    txt_realAmount.Text = UtilsCommons.GetMoneyString(model.RealAmount, ProviderToMoney);
                    //实领签单数
                    txt_relSignNum.Text = model.RelSignNum.ToString();
                    //预领签单数
                    lbl_preSignNum.Text = model.PreSignNum.ToString();
                    //审批人
                    lbl_approver.Text = model.Approver;
                    //审批时间
                    txt_approveDate.Text = UtilsCommons.GetDateString(model.ApproveDate, ProviderToDate);
                    txt_approveDate.Enabled = false;
                    //审批意见
                    txt_approval.Text = model.Approval;
                    //出纳
                    txt_lender.SellsName = SiteUserInfo.Name;
                    txt_lender.SellsID = SiteUserInfo.UserId;
                    //支付日期
                    txt_lendDate.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
                    if (Utils.GetInt(Utils.GetQueryStringValue("intstatus")) == ((int)EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付))
                    {
                        txt_lender.SellsName = model.Lender;
                        txt_lender.SellsID = model.LenderId;
                        txt_lendDate.Text = UtilsCommons.GetDateString(model.LendDate, ProviderToDate);
                        txt_lender.ReadOnly = true;
                        txt_lendDate.Enabled = false;
                    }
                }

            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            MDebit model = new MDebit();
            //借款编号
            model.Id = debitID;
            //付款人ID
            model.LenderId = Utils.GetFormValue("txt_lenderID");
            //付款人Name
            model.Lender = Utils.GetFormValue("txt_lenderName");
            //支付
            model.LendDate = DateTime.Now;
            model.CompanyId = CurrentUserCompanyID;
            model.Status = EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付;
            AjaxResponse(UtilsCommons.AjaxReturnJson(new BFinance().PayDebit(model) ? "1" : "-1", "支付失败!"));
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
            if (!CheckGrant(Privs.财务管理_借款管理_财务支付))
            {
                Utils.ResponseNoPermit(Privs.财务管理_借款管理_财务支付, true);
                return;
            }
        }
    }
}
