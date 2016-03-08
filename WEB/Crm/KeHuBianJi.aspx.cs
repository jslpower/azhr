using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL;
using EyouSoft.Model;

namespace Web.CrmCenter
{
    /// <summary>
    /// 客户资料添加修改
    /// 创建者:刘树超
    /// 时间 :2013-6-5
    /// </summary>
    public partial class KeHuBianJi : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected string Province = "0";
        protected string City = "0";
        protected string Country = "0";
        protected string County = "0";

        /// <summary>
        /// 是否有常用设置权限
        /// </summary>
        protected bool IsSetChangYong = false;
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

            #region 初始化上传控件
            this.UploadYM.CompanyID = this.UploadYJ.CompanyID = this.UploadWordTemp.CompanyID = this.UploadControl1.CompanyID = SiteUserInfo.CompanyId;
            this.UploadYM.IsUploadMore = this.UploadYJ.IsUploadMore = this.UploadWordTemp.IsUploadMore = false;
            this.UploadYM.IsUploadSelf = this.UploadYJ.IsUploadSelf = this.UploadWordTemp.IsUploadSelf = true;

            this.UploadYM.FileTypes = "*.jpg;*.jpeg;*.gif;*.png;*.bmp;";
            this.UploadYJ.FileTypes = "*.jpg;*.jpeg;*.gif;*.png;*.bmp;";
            this.UploadWordTemp.FileTypes = "*.dot;";
            #endregion


            #region ajaxrequest
            string doType = Utils.GetQueryStringValue("dotype");

            switch (doType)
            {
                case "save": Save(); break;
                case "isexists": IsExists(); break;
                case "savechangyongshezhi": SaveChangYongSheZhi(); break;
                default: break;
            }
            #endregion

            InitWUC();
            InitKeHuDengJi();
            InitPayType();
            InitInfo();
            string id = Utils.GetQueryStringValue("crmId");
            string type = Utils.GetQueryStringValue("type");
            if (type == "chk") phdSave.Visible = false;

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
                case EyouSoft.Model.EnumType.PrivsStructure.Menu2.客户中心_分销商: break;
                default: AjaxResponse("错误的请求"); break;
            }

            IsAdd = string.IsNullOrEmpty(Utils.GetQueryStringValue("crmid"));
            IsEdit = !IsAdd;
        }

        /// <summary>
        /// init wuc
        /// </summary>
        void InitWUC()
        {
            UploadControl1.CompanyID = SiteUserInfo.CompanyId;
            UploadControl1.IsUploadSelf = false;

            Seller1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
        }

        /// <summary>
        /// get attach
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.ComStructure.MComAttach GetAttach()
        {
            EyouSoft.Model.ComStructure.MComAttach info = null;

            string attach = Utils.GetFormValue(this.UploadControl1.ClientHideID);
            if (string.IsNullOrEmpty(attach))
            {
                attach = Utils.GetFormValue("txtLatestAttach");
            }

            if (!string.IsNullOrEmpty(attach) && attach.IndexOf('|') > -1)
            {
                string[] attachs = attach.Split('|');

                if (attachs != null && attachs.Length == 2)
                {
                    info = new EyouSoft.Model.ComStructure.MComAttach();
                    info.FilePath = attachs[1];
                    info.Name = attachs[0];
                    info.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.客户合作协议;
                    info.ItemId = string.Empty; ;
                }
            }

            return info;
        }

        /// <summary>
        /// get banks
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.CrmStructure.MCrmBank> GetBanks()
        {
            IList<EyouSoft.Model.CrmStructure.MCrmBank> items = new List<EyouSoft.Model.CrmStructure.MCrmBank>();
            string[] BankAccountArray = Utils.GetFormValues("txtBankAccount");
            string[] BankNameArray = Utils.GetFormValues("txtBankName");
            string[] BankId = Utils.GetFormValues("hidBankId");

            if (BankAccountArray == null || BankAccountArray.Length == 0) return null;

            for (int i = 0; i < BankNameArray.Length; i++)
            {
                var item = new EyouSoft.Model.CrmStructure.MCrmBank();
                item.BankAccount = BankAccountArray[i];
                item.BankName = BankNameArray[i];
                item.BankId = BankId[i];

                if (string.IsNullOrEmpty(item.BankId)
                    && string.IsNullOrEmpty(item.BankAccount)
                    && string.IsNullOrEmpty(item.BankName)) continue;

                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// get lxrs
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.CrmStructure.MCrmLinkman> GetLxrs()
        {
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> items = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
            string[] BirthdayArray = Utils.GetFormValues("txtlBirthday");
            string[] DepartmentArray = Utils.GetFormValues("txtlDepartment");
            string[] FaxArray = Utils.GetFormValues("txtlFax");
            string[] MobilePhoneArray = Utils.GetFormValues("txtlMobilePhone");
            string[] NameArray = Utils.GetFormValues("txtllinkManName");
            string[] QQArray = Utils.GetFormValues("txtlQQ");
            string[] TelephoneArray = Utils.GetFormValues("txtlTel");
            // string[] AddressArray = Utils.GetFormValues("txtlAddress");
            string[] IsRemindArray = Utils.GetFormValues("txtIsTiXing");
            string[] LinkManId = Utils.GetFormValues("hidlLinkManId");
            string[] Email = Utils.GetFormValues("txtEmail");
            string[] msn = Utils.GetFormValues("txtMSN");
            if (DepartmentArray == null || DepartmentArray.Length == 0) return null;

            for (int i = 0; i < DepartmentArray.Length; i++)
            {
                if (string.IsNullOrEmpty(DepartmentArray[i])) continue;

                var item = new EyouSoft.Model.CrmStructure.MCrmLinkman();
                item.Id = LinkManId[i];
                item.Birthday = Utils.GetDateTimeNullable(BirthdayArray[i]);
                item.CompanyId = CurrentUserCompanyID;
                item.Department = DepartmentArray[i];
                item.Fax = FaxArray[i];
                item.MobilePhone = MobilePhoneArray[i];
                item.Name = NameArray[i];
                item.QQ = QQArray[i];
                item.Telephone = TelephoneArray[i];
                item.Type = EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位;
                item.IsRemind = IsRemindArray[i] == "1";
                // item.Address = AddressArray[i];
                item.EMail = Email[i];
                item.MSN = msn[i];

                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        void Save()
        {
            string editid = Utils.GetQueryStringValue("crmId");

            EyouSoft.Model.CrmStructure.MCrm info = new EyouSoft.Model.CrmStructure.MCrm();
            if (IsEdit) info = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(editid);

            #region 客户信息
            //客户信息
            info.Address = Utils.GetFormValue(txtAddress.UniqueID);//地址
            info.CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID), 0);//城市
            info.CompanyId = CurrentUserCompanyID;//公司编号
            info.IssueTime = DateTime.Now;//添加时间
            info.LegalRepresentative = Utils.GetFormValue(txtLegalRepresentative.UniqueID);//法人代表
            info.LegalRepresentativePhone = Utils.GetFormValue(txtLegalRepresentativePhone.UniqueID);//法人代表电话
            info.LevId = Utils.GetInt(Utils.GetFormValue(ddlLevId.UniqueID), 0);//客户等级
            info.License = Utils.GetFormValue(txtLicense.UniqueID);//许可证号
            info.Name = Utils.GetFormValue(txtName.UniqueID);//单位名称
            info.OperatorId = SiteUserInfo.UserId;//操作人
            info.OrganizationCode = Utils.GetFormValue(txtOrganizationCode.UniqueID);//机构代码
            info.ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvice.UniqueID), 0);//省份
            //info.RebatePolicy = Utils.GetFormValue(txtRebatePolicy.UniqueID);//返利政策      
            EyouSoft.Model.EnumType.CrmStructure.CrmType crmType = EyouSoft.Model.EnumType.CrmStructure.CrmType.同行客户;
            info.Type = crmType;
            info.BrevityCode = Utils.GetFormValue(txtBrevityCode.UniqueID);//简码
            info.CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID), 0);//国家
            //info.CountyId = Utils.GetInt(Utils.GetFormValue(ddlCounty.UniqueID), 0);//县区
            info.DeptId = SiteUserInfo.DeptId;//操作人部门
            // info.LegalRepresentativeMobile = Utils.GetFormValue(txtLegalRepresentativeMobile.UniqueID);//法人代表手机 
            #endregion

            #region 常用设置
            info.IsSignContract = Utils.GetFormValue("IsSignContract") == "rbtnIsSignContractYes";


            info.FinancialMobile = Utils.GetFormValue(txtFinancialMobile.UniqueID);
            info.FinancialName = Utils.GetFormValue(txtFinancialName.UniqueID);
            info.FinancialPhone = Utils.GetFormValue(txtFinancialPhone.UniqueID);

            info.FinancialFax = Utils.GetFormValue(txtFinancialFax.UniqueID);
            info.ForeignName = Utils.GetFormValue(txtForeignName.UniqueID);
            info.FinancialFax = Utils.GetFormValue(txtFinancialFax.UniqueID);

            info.PayType = (EyouSoft.Model.EnumType.CrmStructure.PayType)Utils.GetInt(Utils.GetFormValue(ddlPayType.UniqueID), 1);
            info.Limit = Utils.GetInt(Utils.GetFormValue(txtLimit.UniqueID), 0);
            info.SaleName = ""; //Utils.GetFormValue(txtSaleName.UniqueID);
            info.SalePwd = "";//Utils.GetFormValue(txtSalePwd.UniqueID);
            info.SaleState = Utils.GetFormValue("IsSaleState") == "radYes";
            string filePageMei = Utils.GetFormValue(UploadYM.ClientHideID);
            string filePageFoot = Utils.GetFormValue(UploadYJ.ClientHideID);
            string fileWordTemplate = Utils.GetFormValue(UploadWordTemp.ClientHideID);

            if (!string.IsNullOrEmpty(filePageMei))
                info.PrintHeader = filePageMei.Split('|')[1];
            else
                info.PrintHeader = Utils.GetFormValue("hidYM");

            if (!string.IsNullOrEmpty(filePageFoot))
                info.PrintFooter = filePageFoot.Split('|')[1];
            else
                info.PrintFooter = Utils.GetFormValue("hidYJ");

            if (!string.IsNullOrEmpty(fileWordTemplate))
                info.PrintTemplates = fileWordTemplate.Split('|')[1];
            else
                info.PrintTemplates = Utils.GetFormValue("hidWordTemp");
            #endregion


            #region 客户信息 特殊权限控制部分
            //客户信息 特殊权限控制部分
            if (IsEdit)
            {



                if (IsSetChangYong)
                {
                    info.SellerId = Utils.GetFormValue(Seller1.SellsIDClient);
                    // info.AmountOwed = Utils.GetDecimal(Utils.GetFormValue("txtAmountOwed"), 5000);
                    // info.Deadline = Utils.GetInt(Utils.GetFormValue("txtDeadline"), 10);
                    info.IsSignContract = Utils.GetFormValue(rbtnIsSignContractYes.GroupName) == "rbtnIsSignContractYes";
                    info.AttachModel = GetAttach();
                    if (info.AttachModel != null) info.AttachModel.ItemId = info.CrmId;
                    info.BankList = GetBanks();

                    info.FinancialMobile = Utils.GetFormValue(txtFinancialMobile.UniqueID);
                    info.FinancialName = Utils.GetFormValue(txtFinancialName.UniqueID);
                    info.FinancialPhone = Utils.GetFormValue(txtFinancialPhone.UniqueID);
                }
            }

            if (IsAdd)
            {


                info.SellerId = Utils.GetFormValue(Seller1.SellsIDClient);
                if (IsSetChangYong)
                {

                    // info.AmountOwed = Utils.GetDecimal(Utils.GetFormValue("txtAmountOwed"), 5000);
                    // info.Deadline = Utils.GetInt(Utils.GetFormValue("txtDeadline"), 10);
                    info.AttachModel = GetAttach();
                    if (info.AttachModel != null) info.AttachModel.ItemId = info.CrmId;
                    info.BankList = GetBanks();
                }
                else
                {
                    //info.AmountOwed = 5000;
                    //info.Deadline = 10;
                    info.IsSignContract = false;
                    info.AttachModel = null;
                    info.BankList = null;
                }

            }

            #endregion

            info.LinkManList = GetLxrs();//联系人

            //表单验证

            int retcode = 0;
            if (IsEdit) retcode = new EyouSoft.BLL.CrmStructure.BCrm().Update(info);
            if (IsAdd) retcode = new EyouSoft.BLL.CrmStructure.BCrm().Insert(info);

            AjaxResponse(UtilsCommons.AjaxReturnJson(retcode == 1 ? "1" : "0"));
        }

        /// <summary>
        /// 绑定客户等级
        /// </summary>
        /// <returns></returns>
        void InitKeHuDengJi()
        {
            var items = new EyouSoft.BLL.ComStructure.BComLev().GetList(base.SiteUserInfo.CompanyId);
            ddlLevId.Items.Insert(0, new ListItem("--未选择--", "0"));

            if (items == null || items.Count == 0) return;

            foreach (var item in items)
            {
                if (item.LevType == EyouSoft.Model.EnumType.ComStructure.LevType.内部结算价) continue;

                ddlLevId.Items.Add(new ListItem(item.Name, item.Id.ToString()));
            }
        }        /// <summary>
        /// 绑定支付方式
        /// </summary>
        /// <returns></returns>
        void InitPayType()
        {
            this.ddlPayType.DataTextField = "Text";
            this.ddlPayType.DataValueField = "Value";
            this.ddlPayType.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.PayType));
            this.ddlPayType.DataBind();
        }

        /// <summary>
        /// init add info
        /// </summary>
        void InitAddInfo()
        {
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> linkManList = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
            linkManList.Add(new EyouSoft.Model.CrmStructure.MCrmLinkman() { Department = string.Empty, Name = string.Empty, Telephone = string.Empty, MobilePhone = string.Empty, Birthday = null, QQ = string.Empty, Fax = string.Empty, Address = string.Empty, IsRemind = false });
            rptCYLXRList.DataSource = linkManList;
            rptCYLXRList.DataBind();
            IList<EyouSoft.Model.CrmStructure.MCrmBank> bankList = new List<EyouSoft.Model.CrmStructure.MCrmBank>();
            bankList.Add(new EyouSoft.Model.CrmStructure.MCrmBank() { BankAccount = string.Empty, BankName = string.Empty });
            rptJSZHList.DataSource = bankList;
            rptJSZHList.DataBind();

            Seller1.SellsID = SiteUserInfo.UserId;
            Seller1.SellsName = SiteUserInfo.Name;

            rbtnIsSignContractNo.Checked = true;
        }

        /// <summary>
        /// init edit info
        /// </summary>
        void InitEditInfo()
        {
            string editid = Utils.GetQueryStringValue("crmId");
            EyouSoft.Model.CrmStructure.MCrm model = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(editid);
            if (model == null) return;

            txtAddress.Text = model.Address;
            txtLegalRepresentative.Text = model.LegalRepresentative;
            txtLegalRepresentativePhone.Text = model.LegalRepresentativePhone;
            txtLicense.Text = model.License;
            txtName.Text = model.Name;
            txtOrganizationCode.Text = model.OrganizationCode;
            //txtRebatePolicy.Text = model.RebatePolicy;
            Seller1.SellsID = model.SellerId;
            Seller1.SellsName = model.SellerName;
            ddlCity.SelectedValue = model.CityId.ToString();
            ddlLevId.SelectedValue = model.LevId.ToString();
            ddlProvice.SelectedValue = model.ProvinceId.ToString();
            Province = model.ProvinceId.ToString();
            City = model.CityId.ToString();
            Country = model.CountryId.ToString();
            County = model.CountyId.ToString();
            txtBrevityCode.Text = model.BrevityCode;
            txtFinancialMobile.Text = model.FinancialMobile;
            txtFinancialName.Text = model.FinancialName;
            txtFinancialPhone.Text = model.FinancialPhone;
            //txtLegalRepresentativeMobile.Text = model.LegalRepresentativeMobile;
            #region 添加属性
            txtForeignName.Text = model.ForeignName;
            ddlPayType.SelectedValue = ((int)model.PayType).ToString();
            txtLimit.Text = model.Limit.ToString();
            txtFinancialFax.Text = model.FinancialFax;
            //txtSaleName.Text = model.SaleName;

            if (model.SaleState)
            {
                radYes.Checked = true;
            }
            else
            {
                radNo.Checked = true;
            }


            if (model.PrintHeader != "")
            {
                lblYM.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='CustomerEdit.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hidYM' value='{0}'/></span>", model.PrintHeader);
            }
            if (model.PrintFooter != "")
            { lblYJ.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='CustomerEdit.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hidYJ' value='{0}'/></span>", model.PrintFooter); }
            if (model.PrintTemplates != "")
            { lblWordTemp.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='CustomerEdit.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hidWordTemp' value='{0}'/></span>", model.PrintTemplates); }
            #endregion

            if (model.IsSignContract)
            {
                rbtnIsSignContractYes.Checked = true;
            }
            else
            {
                rbtnIsSignContractNo.Checked = true;
            }

            if (model.AttachModel != null)
            {
                lblFile.Text = "<span class='upload_filename' id=\"spanLatestAttach\">&nbsp;<a href='" + model.AttachModel.FilePath + "' target='_blank'>查看附件</a><a href='javascript:void(0);' onclick=\"CustomerEdit.delLatestAttach()\"> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='txtLatestAttach' value='" + model.AttachModel.Name + "|" + model.AttachModel.FilePath + "'></span>";
            }

            if (model.BankList != null && model.BankList.Count > 0)
            {
                rptJSZHList.DataSource = model.BankList;
                rptJSZHList.DataBind();
            }
            else
            {
                IList<EyouSoft.Model.CrmStructure.MCrmBank> list = new List<EyouSoft.Model.CrmStructure.MCrmBank>();
                list.Add(new EyouSoft.Model.CrmStructure.MCrmBank() { BankAccount = string.Empty, BankName = string.Empty });
                rptJSZHList.DataSource = list;
                rptJSZHList.DataBind();
            }

            if (model.LinkManList != null && model.LinkManList.Count > 0)
            {
                rptCYLXRList.DataSource = model.LinkManList;
                rptCYLXRList.DataBind();
            }
            else
            {
                IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
                list.Add(new EyouSoft.Model.CrmStructure.MCrmLinkman() { Department = string.Empty, Name = string.Empty, Telephone = string.Empty, MobilePhone = string.Empty, Birthday = null, QQ = string.Empty, Fax = string.Empty, Address = string.Empty, IsRemind = false, UserId = string.Empty });
                rptCYLXRList.DataSource = list;
                rptCYLXRList.DataBind();
            }

            //如果没有修改别人数据的权限
            if (!SiteUserInfo.IsHandleElse)
            {
                if (model.SellerId != SiteUserInfo.UserId)
                {
                    this.phdSave.Visible = false;
                }
            }


            //  if (IsSetChangYong) phChangYongSheZhi.Visible = true;


        }

        /// <summary>
        /// 初始化
        /// </summary>
        void InitInfo()
        {
            if (IsAdd) InitAddInfo();
            if (IsEdit) InitEditInfo();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (IsEdit)
            {
                if (Menu2Type == EyouSoft.Model.EnumType.PrivsStructure.Menu2.客户中心_分销商
                    && !this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_分销商_修改))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_分销商_修改, false);
                }

            }

            if (IsAdd)
            {
                if (Menu2Type == EyouSoft.Model.EnumType.PrivsStructure.Menu2.客户中心_分销商
                    && !this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_分销商_新增))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_分销商_新增, false);
                }

            }

            if (Menu2Type == EyouSoft.Model.EnumType.PrivsStructure.Menu2.客户中心_分销商)
            {
                IsSetChangYong = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_分销商_常用设置);

            }


        }

        /// <summary>
        /// 验证客户单位名称是否重复
        /// </summary>
        void IsExists()
        {
            string crmId = Utils.GetFormValue("crmid");
            string crmName = Utils.GetFormValue("crmname");

            string retCode = "1";

            if (new EyouSoft.BLL.CrmStructure.BCrm().IsExistsCrmName(CurrentUserCompanyID, crmName, crmId))
            {
                retCode = "0";
            }

            AjaxResponse(UtilsCommons.AjaxReturnJson(retCode));
        }

        /// <summary>
        /// 保存常用设置
        /// </summary>
        void SaveChangYongSheZhi()
        {
            if (!IsSetChangYong) AjaxResponse(UtilsCommons.AjaxReturnJson("-1"));
            if (IsAdd) AjaxResponse(UtilsCommons.AjaxReturnJson("-2"));
            string editid = Utils.GetQueryStringValue("crmId");
            if (string.IsNullOrEmpty(editid)) AjaxResponse(UtilsCommons.AjaxReturnJson("-3"));
            var info = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(editid);
            if (info == null) AjaxResponse(UtilsCommons.AjaxReturnJson("-4"));

            // info.AmountOwed = Utils.GetDecimal(Utils.GetFormValue("txtAmountOwed"), 5000);
            // info.Deadline = Utils.GetInt(Utils.GetFormValue("txtDeadline"), 10);
            info.IsSignContract = Utils.GetFormValue(rbtnIsSignContractYes.UniqueID) == "rbtnIsSignContractYes";
            info.AttachModel = GetAttach();
            if (info.AttachModel != null) info.AttachModel.ItemId = info.CrmId;
            info.BankList = GetBanks();
            info.FinancialMobile = Utils.GetFormValue(txtFinancialMobile.UniqueID);
            info.FinancialName = Utils.GetFormValue(txtFinancialName.UniqueID);
            info.FinancialPhone = Utils.GetFormValue(txtFinancialPhone.UniqueID);

            info.FinancialFax = Utils.GetFormValue(txtFinancialFax.UniqueID);
            info.ForeignName = Utils.GetFormValue(txtForeignName.UniqueID);
            info.FinancialFax = Utils.GetFormValue(txtFinancialFax.UniqueID);

            info.PayType = (EyouSoft.Model.EnumType.CrmStructure.PayType)Utils.GetInt(Utils.GetFormValue(ddlPayType.UniqueID), 1);
            info.Limit = Utils.GetInt(Utils.GetFormValue(txtLimit.UniqueID), 0);
            info.SaleName = ""; //Utils.GetFormValue(txtSaleName.UniqueID);
            //if (Utils.GetFormValue(txtSalePwd.UniqueID) != "")
            //{
            info.SalePwd = "";//Utils.GetFormValue(txtSalePwd.UniqueID);
            //}
            info.SaleState = Utils.GetFormValue(radYes.GroupName) == "radYes";
            string filePageMei = Utils.GetFormValue(UploadYM.ClientHideID);
            string filePageFoot = Utils.GetFormValue(UploadYJ.ClientHideID);
            string fileWordTemplate = Utils.GetFormValue(UploadWordTemp.ClientHideID);

            if (!string.IsNullOrEmpty(filePageMei))
                info.PrintHeader = filePageMei.Split('|')[1];
            else
                info.PrintHeader = Utils.GetFormValue("hidYM");

            if (!string.IsNullOrEmpty(filePageFoot))
                info.PrintFooter = filePageFoot.Split('|')[1];
            else
                info.PrintFooter = Utils.GetFormValue("hidYJ");

            if (!string.IsNullOrEmpty(fileWordTemplate))
                info.PrintTemplates = fileWordTemplate.Split('|')[1];
            else
                info.PrintTemplates = Utils.GetFormValue("hidWordTemp");



            int bllRetCode = new EyouSoft.BLL.CrmStructure.BCrm().Update(info);

            AjaxResponse(UtilsCommons.AjaxReturnJson(bllRetCode == 1 ? "1" : "0"));
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

            PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion
    }
}
