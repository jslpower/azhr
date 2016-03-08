using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.BLL.ComStructure;
    
    public partial class QiTaShenHe : BackPage
    {
        /// <summary>
        /// 父级页面
        /// </summary>
        protected ItemType ParentItemType;
        /// <summary>
        /// 审核状态
        /// </summary>
        protected int Status = 0;
        /// <summary>
        /// 审核Id
        /// </summary>
        protected string OtherFeeID = string.Empty;
        protected bool IsEnableKis;
        protected void Page_Load(object sender, EventArgs e)
        {
            ParentItemType = (ItemType)Utils.GetInt(Utils.GetQueryStringValue("parent"));
            if (Utils.GetFormValue("doType").Length > 0)
            {
                Save();
            }
            OtherFeeID = Utils.GetQueryStringValue("OtherFeeID");
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //系统配置实体
            MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID);
            IsEnableKis = comModel == null ? false : comModel.IsEnableKis;
            IList<MOtherFeeInOut> sl = new BFinance().GetOtherFeeInOutLst(ParentItemType, Utils.ConvertToIntArray(OtherFeeID.Split(',')));
            if (sl != null && sl.Count > 0)
            {
                Status = (int)sl[0].Status;
                rpt_list.DataSource = sl;
                rpt_list.DataBind();
                if (sl[0].Status != FinStatus.财务待审批)
                {
                    lbl_audit.Text = sl[0].Audit;
                    lbl_auditTime.Text = UtilsCommons.GetDateString(sl[0].AuditTime, ProviderToDate);
                    lbl_auditRemark.Text = sl[0].AuditRemark;
                    pan_ExamineVBtn.Visible = false;
                    txt_auditRemark.Visible = false;
                    pan_InMoney.Visible = true;
                }
                else
                {
                    lbl_audit.Text = SiteUserInfo.Name;
                    lbl_auditTime.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
                    lbl_auditRemark.Visible = false;
                }
            }
            else
            {
                pan_ExamineVBtn.Visible = false;
                txt_auditRemark.Visible = false;
            }
        }
        /// <summary>
        /// 审核提交
        /// </summary>
        private void Save()
        {
            ParentItemType = (ItemType)Utils.GetInt(Utils.GetFormValue("parent"));
            bool retBool = new BFinance().SetOtherFeeInOutAudit(
                  ParentItemType,
                  SiteUserInfo.DeptId,
                  SiteUserInfo.UserId,
                  SiteUserInfo.Name,
                  Utils.GetFormValue("auditRemark"),
                  DateTime.Now,
                  FinStatus.账务待支付,
                  Utils.ConvertToIntArray(Utils.GetFormValue("OtherFeeID").Split(','))
                  ) > 0;
            if (retBool)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "审核失败!"));
            }


        }
    }
}
