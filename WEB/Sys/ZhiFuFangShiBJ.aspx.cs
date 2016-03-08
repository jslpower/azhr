using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;


namespace EyouSoft.Web.Sys
{
    public partial class ZhiFuFangShiBJ : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            PageInit();
        }

        protected void Save()
        {
            string msg = string.Empty;
            EyouSoft.Model.ComStructure.MComPayment model = new EyouSoft.Model.ComStructure.MComPayment();
            model.Name = Utils.GetFormValue(txtPayStyleName.UniqueID);
            model.CompanyId = SiteUserInfo.CompanyId;
            model.OperatorId = SiteUserInfo.UserId;
            model.ItemType = (EyouSoft.Model.EnumType.ComStructure.ItemType)Utils.GetInt(Utils.GetFormValue(ddlPayType.UniqueID));
            if (string.IsNullOrEmpty(model.Name))
            {
                msg += "请输入支付方式名称!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue(this.RbtnSourceType.UniqueID)))
            {
                msg += "请选择支付方式!<br/>";
            }
            else
            {
                model.SourceType = (EyouSoft.Model.EnumType.ComStructure.SourceType)Utils.GetInt(Utils.GetFormValue(this.RbtnSourceType.UniqueID));
            }
            if (model.SourceType == EyouSoft.Model.EnumType.ComStructure.SourceType.银行)
            {
                model.AccountId = Utils.GetInt(Utils.GetFormValue(this.ddlBankList.UniqueID));
                if (model.AccountId == 0)
                {
                    msg += "请选择银行!<br/>";
                }
            }
            Response.Clear();
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            if (Id == 0)
            {
                if (new EyouSoft.BLL.ComStructure.BComPayment().Add(model))
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "添加成功"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "添加失败"));
                }
            }
            else
            {
                model.PaymentId = Id;
                if (new EyouSoft.BLL.ComStructure.BComPayment().Update(model))
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败"));
                }
            }
            Response.End();
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            this.ddlPayType.DataTextField = "Text";
            this.ddlPayType.DataValueField = "Value";
            this.ddlPayType.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.ItemType));
            this.ddlPayType.DataBind();

            this.RbtnSourceType.DataTextField = "Text";
            this.RbtnSourceType.DataValueField = "Value";
            this.RbtnSourceType.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.SourceType));
            this.RbtnSourceType.DataBind();

            System.Collections.Generic.List<EnumObj> list = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.ItemType));
            this.ddlBankList.DataTextField = "BankName";
            this.ddlBankList.DataValueField = "AccountId";
            this.ddlBankList.DataSource = new EyouSoft.BLL.ComStructure.BComAccount().GetList(CurrentUserCompanyID);
            this.ddlBankList.DataBind();
            if (Id != 0)
            {
                EyouSoft.Model.ComStructure.MComPayment model = new EyouSoft.BLL.ComStructure.BComPayment().GetModel(Id, SiteUserInfo.CompanyId);
                this.txtPayStyleName.Text = model.Name;
                this.ddlPayType.SelectedValue = ((int)model.ItemType).ToString();
                if (model.AccountId != 0)
                {
                    ddlBankList.SelectedValue = ((int)model.AccountId).ToString();
                }
                this.RbtnSourceType.SelectedValue = ((int)model.SourceType).ToString();
            }
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目, false);
                return;
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
        /// 支付方式编号
        /// </summary>
        public int Id { get { return Utils.GetInt(Utils.GetQueryStringValue("id")); } }
    }
}
