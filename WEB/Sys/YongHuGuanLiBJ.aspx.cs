using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.ComStructure;
using System.Text;

namespace EyouSoft.Web.Sys
{
    public partial class YongHuGuanLiBJ : BackPage
    {
        public string Pwd = string.Empty;
        protected bool isGuide = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            #region  初始化上传控件
            this.uc_Photo.CompanyID = SiteUserInfo.CompanyId;
            this.uc_Photo.IsUploadMore = false;
            this.uc_Photo.IsUploadSelf = true;
            this.uc_Photo.FileTypes = "*.jpg;*.jpeg;*.gif;*.png;*.bmp;";
            this.uc_Files.CompanyID = SiteUserInfo.CompanyId;
            this.uc_Files.IsUploadMore = true;
            this.uc_Files.IsUploadSelf = true;
            #endregion



            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            else if (Utils.GetQueryStringValue("dotype") == "getGovInfo")
            {
                Response.Clear();
                Response.Write(GetGovFileInfo(Utils.GetQueryStringValue("id")));
                Response.End();
            }
            else
            {
                PageInit();
            }
        }

        protected void Save()
        {
            EyouSoft.Model.ComStructure.MComUser model = new EyouSoft.Model.ComStructure.MComUser();
            //内部用户为string.Empty,客户单位或供应商就是相就的公司编号
            model.TourCompanyId = string.Empty;
            model.CompanyId = SiteUserInfo.CompanyId;
            model.UserName = txtUserName.Text;
            model.Password = Utils.GetFormValue("txtPwd");
            model.ContactName = Utils.GetFormValue(txtName.UniqueID);
            model.ContactSex = Utils.GetEnumValue<EyouSoft.Model.EnumType.GovStructure.Gender>(Utils.GetFormValue(ddlSex.UniqueID), EyouSoft.Model.EnumType.GovStructure.Gender.男);
            model.DeptId = Utils.GetInt(Utils.GetFormValue(BelongDepart.SelectIDClient));
            model.DeptIdJG = Utils.GetInt(Utils.GetFormValue(ManageDepart.SelectIDClient));
            //model.Arrears = Utils.GetDecimal(txtDebt.Value);
            model.ContactTel = txtPhone.Value;
            model.ContactFax = txtFax.Value;
            model.ContactMobile = txtMobile.Value;
            model.QQ = txtQQ.Value;
            model.MSN = txtMSN.Value;
            model.ContactEmail = txtEmail.Value;
            model.PeopProfile = txtIntroduction.Text;
            model.Remark = txtRemark.Text;
            model.RoleId = Utils.GetInt(Utils.GetFormValue(ddlRoleList.ClientID));
            model.Operator = SiteUserInfo.Name;
            model.OperatorId = SiteUserInfo.UserId;
            model.OperDeptId = SiteUserInfo.DeptId;
            //model.UserStatus = chkState.Checked;

            #region 新添加属性
            model.ComAttachList = NewGetAttach();
            model.BirthDate = Utils.GetDateTimeNullable(txtBirth.Value);
            model.IDcardId = txtDebt.Value;
            model.IsGuide = Utils.GetFormValue(ddlisGuide.UniqueID) == "1" ? true : false;
            model.ContactShortTel = txtShortPhone.Value;
            model.ContactShortMobile = txtShortMobile.Value;
            string filePageMei = Utils.GetFormValue(uc_Photo.ClientHideID);
            if (!string.IsNullOrEmpty(filePageMei))
                model.UserImg = filePageMei.Split('|')[1];
            else
                model.UserImg = Utils.GetFormValue("hideFileInfo_photo");

            #endregion
            model.UserType = EyouSoft.Model.EnumType.ComStructure.UserType.内部员工;


            Response.Clear();
            if (string.IsNullOrEmpty(UserID))
            {

                if (!new EyouSoft.BLL.ComStructure.BComUser().IsExistsUserName(model.UserName, SiteUserInfo.CompanyId, string.Empty))
                {
                    if (new EyouSoft.BLL.ComStructure.BComUser().Add(model))
                    {
                        Response.Write(UtilsCommons.AjaxReturnJson("1", "添加成功!"));
                    }
                    else
                    {
                        Response.Write(UtilsCommons.AjaxReturnJson("0", "添加失败!"));
                    }
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "用户名已存在!"));
                }
            }
            else
            {
                model.UserId = UserID;
                if (new EyouSoft.BLL.ComStructure.BComUser().Update(model))
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功!"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败!"));
                }
            }
            Response.End();
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            //this.HrSelect1.IsShowSelect = false;
            //string iframeId = Utils.GetQueryStringValue("iframeId");
            //this.HrSelect1.SetTitle = "姓名";
            ////this.HrSelect1..SModel = "1";
            //this.HrSelect1.ParentIframeID = iframeId;
            //this.HrSelect1.CallBackFun = "UserEdit.BindGovFile";
            this.ddlSex.DataTextField = "Text";
            this.ddlSex.DataValueField = "Value";
            this.ddlSex.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), new string[] { "2" });
            this.ddlSex.DataBind();
            this.ddlSex.Items.Insert(0, new ListItem("请选择", ""));
            IList<EyouSoft.Model.ComStructure.MComRole> rolelist = new EyouSoft.BLL.ComStructure.BComRole().GetList(SiteUserInfo.CompanyId);
            this.ddlRoleList.DataTextField = "RoleName";
            this.ddlRoleList.DataValueField = "Id";
            this.ddlRoleList.DataSource = rolelist;
            this.ddlRoleList.DataBind();
            this.ddlRoleList.Items.Insert(0, new ListItem("请选择", ""));
            this.ManageDepart.IsNotValid = false;
            if (!string.IsNullOrEmpty(UserID))
            {
                EyouSoft.Model.ComStructure.MComUser model = new EyouSoft.BLL.ComStructure.BComUser().GetModel(UserID, SiteUserInfo.CompanyId);
                txtUserName.Text = model.UserName;
                txtUserName.ReadOnly = true;

                txtName.Text = model.ContactName;
                //HrSelect1.SellsName = model.ContactName;
                //HrSelect1.SellsID = model.GovFileId;
                // HrSelect1.IsDisplay = true;
                if (this.ddlSex.Items.FindByValue(((int)model.ContactSex).ToString()) != null)
                {
                    this.ddlSex.SelectedValue = ((int)model.ContactSex).ToString();
                }
                this.BelongDepart.SectionID = model.DeptId.ToString();
                this.BelongDepart.SectionName = model.DeptName;
                this.ManageDepart.SectionID = model.DeptIdJG.ToString();
                this.ManageDepart.SectionName = model.JGDeptName;
                if (this.ddlRoleList.Items.FindByValue(model.RoleId.ToString()) != null)
                {
                    this.ddlRoleList.SelectedValue = model.RoleId.ToString();
                }
                //txtDebt.Value = Utils.FilterEndOfTheZeroString(model.Arrears.ToString());
                txtPhone.Value = model.ContactTel;
                txtFax.Value = model.ContactFax;
                txtMobile.Value = model.ContactMobile;
                txtQQ.Value = model.QQ;
                txtMSN.Value = model.MSN;
                txtEmail.Value = model.ContactEmail;
                txtIntroduction.Text = model.PeopProfile;
                txtRemark.Text = model.Remark;
                //chkState.Checked = model.IsEnable;
                txtUserName.Enabled = false;
                Pwd = model.Password;
                if (this.ddlisGuide.Items.FindByValue(model.IsGuide ? "1" : "0") != null)
                {
                    if (model.IsGuide) this.ddlisGuide.Items.Remove(this.ddlisGuide.Items.FindByValue("0"));
                    this.ddlisGuide.SelectedValue = model.IsGuide ? "1" : "0";
                }
                txtDebt.Value = model.IDcardId;
                if (model.BirthDate != null)
                {
                    txtBirth.Value =Utils.GetDateTime(model.BirthDate.ToString()).ToString("yyyy-MM-dd");
                }
                txtShortPhone.Value = model.ContactShortTel;
                txtShortMobile.Value = model.ContactShortMobile;


                #region 附件处理
                if (!string.IsNullOrEmpty(model.UserImg))
                {
                    lbTxtphoto.Text = string.Format("<span  class='upload_filename'><a href='{0}' target='_blank'>查看</a><a href=\"javascript:void(0)\"  onclick=\"UserEdit.RemoveFile(this)\" title='删除照片'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" id=\"hideFileInfo_photo\" name=\"hideFileInfo_photo\" value=\"{0}\"/></span>", model.UserImg);
                }
                //附件
                StringBuilder strFile = new StringBuilder();
                IList<EyouSoft.Model.ComStructure.MComAttach> lstFile = model.ComAttachList;
                if (null != lstFile && lstFile.Count > 0)
                {
                    for (int i = 0; i < lstFile.Count; i++)
                    {
                        strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={2}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"UserEdit.RemoveFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", lstFile[i].FilePath, lstFile[i].Name, HttpUtility.UrlEncode(lstFile[i].Name));
                    }
                }
                this.lbTxtFiles.Text = strFile.ToString();//附件
                #endregion



            }
            else
            {
                ddlSex.Items[0].Selected = true;
                txtUserName.Enabled = true;
                //chkState.Checked = false;
            }
        }

        /// <summary>
        /// 获得人事档案信息
        /// </summary>
        /// <param name="GovId"></param>
        /// <returns></returns>
        public string GetGovFileInfo(string GovId)
        {
            if (!string.IsNullOrEmpty(GovId))
            {
                var model = new EyouSoft.BLL.GovStructure.BArchives().GetArchivesModel(GovId);
                return Newtonsoft.Json.JsonConvert.SerializeObject(model);
            }
            return "";
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_栏目, false);
                return;
            }
            if (string.IsNullOrEmpty(UserID))
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_新增))
                {
                    placeSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_修改))
                {
                    placeSave.Visible = false;
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

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID { get { return Utils.GetQueryStringValue("id"); } }


        private IList<MComAttach> NewGetAttach()
        {
            //之前上传的附件
            string stroldupload = Utils.GetFormValue("hideFileInfo");
            IList<MComAttach> lst = new List<MComAttach>();
            if (!string.IsNullOrEmpty(stroldupload))
            {
                string[] oldupload = stroldupload.Split(',');
                if (oldupload != null && oldupload.Length > 0)
                {
                    for (int i = 0; i < oldupload.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(oldupload[i]))
                        {
                            string[] uploaditem = oldupload[i].Split('|');
                            MComAttach attModel = new MComAttach();
                            attModel.Name = uploaditem[0];
                            attModel.FilePath = uploaditem[1];
                            lst.Add(attModel);
                        }
                    }
                }
            }
            //新上传附件
            string[] upload = Utils.GetFormValues(this.uc_Files.ClientHideID);
            for (int i = 0; i < upload.Length; i++)
            {
                string[] newupload = upload[i].Split('|');
                if (newupload != null && newupload.Length > 1)
                {
                    MComAttach attModel = new MComAttach();
                    attModel.FilePath = newupload[1];
                    attModel.Name = newupload[0];
                    lst.Add(attModel);
                }
            }
            return lst;
        }
    }
}
