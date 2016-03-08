using System;
using System.Linq;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Common;
    
    public partial class YingFuBatchShen : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("doType") == "Save")
            {
                Save();
            }
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            IList<MRegister> ls = new BFinance().GetRegisterById(
                CurrentUserCompanyID,
                Utils.ConvertToIntArray(Utils.GetQueryStringValue("registerIds").Split(','))
                );

            if (ls != null && ls.Count > 0)
            {
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                lbl_amount.Text = UtilsCommons.GetMoneyString(ls.Sum(item => item.PaymentAmount), ProviderToMoney);
            }
            lbl_EAPerson.Text = SiteUserInfo.Name;
            txt_EADate.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string eARemark = Utils.GetFormValue(txt_EARemark.UniqueID);
            int[] registerIds = Utils.ConvertToIntArray(Utils.GetFormValue(hd_registerIds.UniqueID).Split(','));
            string msg = string.Empty;
            if (registerIds.Length <= 0)
            {
                msg += "审批失败!";
            }
            else
            {
                bool strBool = new BFinance().SetRegisterApprove(
                        SiteUserInfo.UserId, //审核人ID
                        SiteUserInfo.Name,//审核人
                        DateTime.Now,//审核时间(当前时间)
                        eARemark,//审核意见
                        EyouSoft.Model.EnumType.FinStructure.FinStatus.账务待支付,//审核状态
                        CurrentUserCompanyID,//公司ID
                        registerIds//登记编号集合
                        ) > 0;
                if (strBool)
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "审批失败!"));
                }

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
    }
}
