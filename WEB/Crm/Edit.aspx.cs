using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.Crm
{
    /// <summary>
    /// 客户管理 个人会员 添加修改
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public partial class Edit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected int Country = 1;
        protected int Province = 0;
        protected int City = 0;
        protected int County = 0;

        /// <summary>
        /// 二级栏目
        /// </summary>
        EyouSoft.Model.EnumType.PrivsStructure.Menu2 Menu2Type = EyouSoft.Model.EnumType.PrivsStructure.Menu2.None;
        /// <summary>
        /// 是否修改
        /// </summary>
        bool IsEdit = false;
        /// <summary>
        /// 是否新增
        /// </summary>
        bool IsAdd = true;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitMenu2Type();
            PowerControl();

            #region ajax request
            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "save": Save(); break;
                case "isexistsidcardcode": IsExistsIdCardCode(); break;
                default: break;
            }
            #endregion

            InitMemberTypes();
            InitMemberStatus();
            InitInfo();
        }

        #region private members
        /// <summary>
        /// 初始化二级栏目
        /// </summary>
        void InitMenu2Type()
        {
            Menu2Type = Utils.GetEnumValue<EyouSoft.Model.EnumType.PrivsStructure.Menu2>(Utils.GetQueryStringValue("sl"), EyouSoft.Model.EnumType.PrivsStructure.Menu2.None);

            switch (Menu2Type)
            {
                case EyouSoft.Model.EnumType.PrivsStructure.Menu2.客户中心_个人会员: break;
                default: AjaxResponse("错误的请求"); break;
            }

            IsAdd = string.IsNullOrEmpty(Utils.GetQueryStringValue("crmid"));
            IsEdit = !IsAdd;
        }

        /// <summary>
        /// init member type
        /// </summary>
        /// <returns></returns>
        void InitMemberTypes()
        {
            ddlMemberType.Items.Add(new ListItem("--请选择--", "0"));
            IList<EyouSoft.Model.ComStructure.MComMemberType> items = new EyouSoft.BLL.ComStructure.BComMemberType().GetList(CurrentUserCompanyID);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    ddlMemberType.Items.Add(new ListItem(item.TypeName, item.Id.ToString()));
                }
            }
        }

        /// <summary>
        /// init member status
        /// </summary>
        void InitMemberStatus()
        {
            ddlMemberState.Items.Add(new ListItem("--请选择--", "0"));
            var items = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.CrmMemberState));

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    ddlMemberState.Items.Add(new ListItem(item.Text, item.Value));
                }
            }
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        void Save()
        {
            EyouSoft.Model.CrmStructure.MCrmPersonalInfo info = new EyouSoft.Model.CrmStructure.MCrmPersonalInfo();

            #region get form value
            if (IsEdit) info.CrmId = Utils.GetQueryStringValue("crmid");
            info.Birthday = Utils.GetDateTimeNullable(Utils.GetFormValue(txtBirthday.UniqueID));
            info.BriefCode = Utils.GetFormValue(txtBrevityCode.UniqueID);
            info.CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            info.CompanyId = CurrentUserCompanyID;
            info.CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            info.DanWei = Utils.GetFormValue(txtWorkUnit.UniqueID);
            info.DanWeiAddress = Utils.GetFormValue(txtUnitAddress.UniqueID);
            info.DistrictId = Utils.GetInt(Utils.GetFormValue(ddlCounty.UniqueID));
            info.Email = Utils.GetFormValue(txtEMail.UniqueID);
            info.Gender = Utils.GetEnumValue<EyouSoft.Model.EnumType.GovStructure.Gender>(Utils.GetFormValue(ddlGender.UniqueID), EyouSoft.Model.EnumType.GovStructure.Gender.男);
            info.HomeAddress = Utils.GetFormValue(txtHomeAddress.UniqueID);
            info.HuiFei = Utils.GetDecimal(Utils.GetFormValue(txtMemberDues.UniqueID));
            info.IdCardCode = Utils.GetFormValue(txtIdNumber.UniqueID);
            info.IsTiXing = Utils.GetFormValue(this.chbIsRemind.UniqueID) == "on";
            info.JoinTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtJoinTime.UniqueID));
            info.JoinType = Utils.GetFormValue(txtApplicationType.UniqueID);
            info.MemberCardCode = Utils.GetFormValue(txtMemberCardNumber.UniqueID);
            info.MemberStatus = Utils.GetEnumValue<EyouSoft.Model.EnumType.CrmStructure.CrmMemberState>(Utils.GetFormValue(ddlMemberState.UniqueID), EyouSoft.Model.EnumType.CrmStructure.CrmMemberState.普通);
            info.MemberTypeId = Utils.GetInt(Utils.GetFormValue(ddlMemberType.UniqueID));
            info.Mobile = Utils.GetFormValue(txtMobilePhone.UniqueID);
            info.Name = Utils.GetFormValue(txtName.UniqueID);
            info.National = Utils.GetFormValue(txtNational.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            info.QQ = Utils.GetFormValue(txtQQ.UniqueID);
            info.Remark = Utils.GetFormValue(txtRemark.UniqueID);
            info.Telephone = Utils.GetFormValue(txtContactPhone.UniqueID);
            info.ZipCode = Utils.GetFormValue(txtZipCode.UniqueID);
            info.SellerId = Utils.GetFormValue(Seller1.SellsIDClient);
            #endregion

            //validate form

            int retcode = 0;

            if (IsAdd) retcode = new EyouSoft.BLL.CrmStructure.BCrmMember().Insert(info);
            if (IsEdit) retcode = new EyouSoft.BLL.CrmStructure.BCrmMember().Update(info);

            AjaxResponse(UtilsCommons.AjaxReturnJson(retcode == 1 ? "1" : "0"));
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (IsAdd) return;
            string crmId = Utils.GetQueryStringValue("crmid");
            var info = new EyouSoft.BLL.CrmStructure.BCrmMember().GetInfo(crmId);
            if (info == null) return;

            if (info.Birthday.HasValue) txtBirthday.Text = info.Birthday.Value.ToString("yyyy-MM-dd");
            txtContactPhone.Text = info.Telephone;
            txtEMail.Text = info.Email;
            txtHomeAddress.Text = info.HomeAddress;
            txtIdNumber.Text = info.IdCardCode;
            if (info.JoinTime.HasValue) txtJoinTime.Text = info.JoinTime.Value.ToString("yyyy-MM-dd");
            txtMemberCardNumber.Text = info.MemberCardCode;
            txtMemberDues.Text = info.HuiFei.ToString("F2");
            txtMobilePhone.Text = info.Mobile;
            txtName.Text = info.Name;
            txtNational.Text = info.National;
            txtQQ.Text = info.QQ;
            txtRemark.Text = info.Remark;
            txtUnitAddress.Text = info.DanWeiAddress;
            txtWorkUnit.Text = info.DanWei;
            txtZipCode.Text = info.ZipCode;
            ddlGender.SelectedValue = ((int)info.Gender).ToString();
            ddlMemberState.SelectedValue = ((int)info.MemberStatus).ToString();
            ddlMemberType.SelectedValue = info.MemberTypeId.ToString();
            chbIsRemind.Checked = info.IsTiXing;
            txtApplicationType.Text = info.JoinType;
            txtBrevityCode.Text = info.BriefCode;
            this.Seller1.SellsID = info.SellerId;
            this.Seller1.SellsName = info.SellerName;
            Country = info.CountryId;
            Province = info.ProvinceId;
            City = info.CityId;
            County = info.DistrictId;


            if (Utils.GetQueryStringValue("edittype") == "details")
            {
                this.phdSave.Visible = false;
            }

            if (SiteUserInfo.IsHandleElse == false)
            {
                if (info.SellerId != SiteUserInfo.UserId)
                {
                    this.phdSave.Visible = false;
                }
            }

        }

        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (IsEdit)
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_修改))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_修改, false);
                    return;
                }
            }

            if (IsAdd)
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_新增))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_新增, false);
                    return;
                }
            }
        }

        /// <summary>
        /// 是否存在相同的身份证号码
        /// </summary>
        void IsExistsIdCardCode()
        {
            string crmId = Utils.GetQueryStringValue("crmid");
            string idCardCode = Utils.GetQueryStringValue("idcardcode");

            string retCode = "1";

            if (new EyouSoft.BLL.CrmStructure.BCrmMember().IsExistsIdCardCode(CurrentUserCompanyID, crmId, idCardCode))
            {
                retCode = "0";
            }

            AjaxResponse(UtilsCommons.AjaxReturnJson(retCode));
        }
        #endregion

        #region protected members
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #endregion

    }
}
