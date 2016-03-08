using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;
using System.Text;

namespace EyouSoft.Web.Sys
{
    public partial class BuMenGuanLiBJ : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            string save = Utils.GetQueryStringValue("save");
            if (save == "save")
            {
                PageSave(doType);
            }
            PowerControl();
            string id = Utils.GetQueryStringValue("id");
            PageInit(id, doType);
            if (doType == "add") add.Visible = true;
            edit.Visible = true;

        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id, string doType)
        {
            #region 用户控件初始化
            this.HrSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
            this.HrSelect1.SetTitle = "部门主管";


            #endregion
            BComDepartment BLL = new BComDepartment();
            //根据编辑传过来的编号获取部门信息实体
            MComDepartment Model = BLL.GetModel(Utils.GetInt(id), this.SiteUserInfo.CompanyId);
            if (doType == "update")
            {
                if (null != Model)
                {
                    //部门名称
                    this.txtDepartName.Text = Model.DepartName;
                    //部门编号
                    this.hidDepartId.Value = Model.DepartId.ToString();
                    //联系电话
                    this.txtContact.Text = Model.Contact;
                    //传真
                    this.txtFaxa.Text = Model.Fax;
                    //备注
                    this.txtRemark.Text = Model.Remarks;
                    this.HrSelectJD.SellsID = Model.DepartPlanId;

                    this.HrSelectJD.SellsName = Model.DepartPlan;

                    this.chk_isDefault.Checked = Model.IsDefaultConfig;
                    if (Model.PrintFiles != null && Model.PrintFiles.Count > 0)
                    {
                        CustomRepeater1.DataSource = Model.PrintFiles;
                        CustomRepeater1.DataBind();
                    }
                    else
                    {
                        add.Visible = true;
                    }

                    //string strDel = "<span  class='upload_filename'><a href='{0}' target='_blank'>查看</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\"  title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hide_{1}\" value=\"{0}\"/></span>";


                    //部门主管编号
                    this.HrSelect1.SellsID = Model.DepartHead;
                    var HRmodel = new EyouSoft.BLL.ComStructure.BComUser().GetModel(Model.DepartHead, SiteUserInfo.CompanyId);
                    if (HRmodel != null)
                    {
                        this.HrSelect1.SellsName = HRmodel.ContactName;
                    }

                    MComDepartment ModelPart = BLL.GetModel(Model.PrevDepartId, Model.CompanyId);
                    if (ModelPart != null)
                    {
                        //上级部门名称和编号
                        this.txtUpSection.Text = ModelPart.DepartName;
                        this.hidupsectionId.Value = ModelPart.DepartId.ToString();
                    }
                    else
                    {
                        this.txtUpSection.Text = "股东会";
                    }


                    ////通过部门主管编号获取人事档案信息实体
                    //EyouSoft.BLL.GovStructure.BArchives BLLManager = new EyouSoft.BLL.GovStructure.BArchives();
                    //EyouSoft.Model.GovStructure.MGovFile ModelManager;
                    //ModelManager = BLLManager.GetArchivesModel(Model.DepartHead);
                    //if (ModelManager != null)
                    //{
                    //    this.HrSelect1.SellsName = ModelManager.Name;
                    //    this.HrSelect1.SellsID = ModelManager.ID;
                    //}
                }
                else
                {
                    this.txtUpSection.Text = "股东会";
                }
            }
            else
            {
                if (Model != null)
                {
                    this.txtUpSection.Text = Model.DepartName;
                    this.hidupsectionId.Value = Model.DepartId.ToString();
                }
                else
                {
                    this.txtUpSection.Text = "股东会";
                    this.hidupsectionId.Value = "0";
                }
            }
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            //部门名称
            string deptname = Utils.GetFormValue(txtDepartName.UniqueID);
            //部门编号
            string deptid = Utils.GetFormValue(hidDepartId.UniqueID);
            //部门主管编号
            string depthead = Utils.GetFormValue(this.HrSelect1.SellsIDClient);
            //上级部门编号
            string upsection = Utils.GetFormValue(this.hidupsectionId.UniqueID);
            //联系电话
            string contact = Utils.GetFormValue(txtContact.UniqueID);
            //传真
            string fax = Utils.GetFormValue(txtFaxa.UniqueID);
            string remark = Utils.GetFormValue(txtRemark.UniqueID);
            #endregion

            #region 数据验证
            string msg = "";
            bool result = false;
            if (string.IsNullOrEmpty(deptname))
            {
                msg += "-请输入部门名称！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
                Response.End();
            }
            #endregion

            #region 实体赋值
            MComDepartment model = new MComDepartment();
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.Contact = contact;
            model.DepartHead = depthead;
            //model.DepartId = 
            model.DepartName = deptname;
            model.DepartId = Utils.GetInt(deptid, 0);
            model.Fax = fax;
            model.IssueTime = DateTime.Now;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.PrevDepartId = Utils.GetInt(upsection);
            model.DepartPlanId = Utils.GetFormValue(this.HrSelectJD.SellsIDClient);
            model.DepartPlan = Utils.GetFormValue(this.HrSelectJD.SellsNameClient);
            model.IsDefaultConfig = chk_isDefault.Checked;
            model.Remarks = remark;
            model.PrintFiles = this.GetDataList();
            #endregion

            #region 提交保存
            Response.Clear();
            BComDepartment BLL = new BComDepartment();
            if (doType == "add")
            {
                result = BLL.Add(model);
                msg = result ? "添加成功！" : "添加失败！";
                Response.Write("{\"result\":\"" + (result ? "1" : "0") + "\",\"msg\":\"" + msg + "\",\"id\":\"" + model.PrevDepartId + "\"}");
            }
            else
            {
                result = BLL.Update(model);
                msg = result ? "修改成功！" : "修改失败！";
                Response.Write("{\"result\":\"" + (result ? "1" : "0") + "\",\"msg\":\"" + msg + "\",\"id\":\"" + model.DepartId + "\",\"nowName\":\"" + model.DepartName + "\"}");
            }
            Response.End();
            #endregion

        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_部门管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_部门管理_栏目, false);
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
        /// 获取控件的数据
        /// </summary>
        protected IList<MComDepartmentFiles> GetDataList()
        {
            //打印名称
            string[] printName = Utils.GetFormValues("PrintName");
            //页眉
            string[] printHead = Utils.GetFormValues("hide_head_file");
            string[] oldprintHead = Utils.GetFormValues("oldhide_head_file");
            //页脚
            string[] printFoot = Utils.GetFormValues("hide_Foot_file");
            string[] oldprintFoot = Utils.GetFormValues("oldhide_Foot_file");

            //模板 
            string[] printTemp = Utils.GetFormValues("hide_Temp_file");
            string[] oldprintTemp = Utils.GetFormValues("oldhide_Temp_file");

            //是否默认
            string[] isDefaluts = Utils.GetFormValues("hdremind");


            IList<MComDepartmentFiles> list = new List<MComDepartmentFiles>();
            if (printName.Length > 0)
            {

                for (int i = 0; i < printName.Length; i++)
                {
                    MComDepartmentFiles model = new MComDepartmentFiles();
                    if (printName[i] != "")
                    {
                        model.PrintName = printName[i];
                        if (printHead[i].Split('|').Length > 1 && printHead[i].Length > 1)
                        {
                            model.PrintHeader = printHead[i].Split('|')[1].ToString();
                        }
                        else
                        {
                            model.PrintHeader = oldprintHead != null && oldprintHead.Length>i && oldprintHead[i].Length > 0 ? oldprintHead[i] : "";
                        }
                        if (printFoot[i].Split('|').Length > 1 && printFoot[i].Length > 1)
                        {
                            model.PrintFooter = printFoot[i].Split('|')[1].ToString();
                        }
                        else
                        {
                            model.PrintFooter = oldprintFoot != null && oldprintFoot.Length>i && oldprintFoot[i].Length > 0 ? oldprintFoot[i] : "";
                        }
                        if (printTemp[i].Split('|').Length > 1 && printTemp[i].Length > 1)
                        {
                            model.PrintTemplates = printTemp[i].Split('|')[1].ToString();
                        }
                        else
                        {
                            model.PrintTemplates = oldprintTemp != null && oldprintTemp.Length>i && oldprintTemp[i].Length > 0 ? oldprintTemp[i] : "";
                        }
                        model.IsDefault = isDefaluts[i] == "1" ? true : false;
                        list.Add(model);
                    }

                }

            }

            return list;


        }


        protected string OutPutHTML(string str, int i)
        {

            StringBuilder strbu = new StringBuilder();
            switch (i)
            {
                case 1: strbu.AppendFormat("<input type=\"hidden\" name=\"oldhide_head_file\" value=\"{1}\">", str, str == "" ? "1" : str);
                    break;
                case 2: strbu.AppendFormat("<input type=\"hidden\" name=\"oldhide_Foot_file\" value=\"{1}\">", str, str == "" ? "1" : str);
                    break;
                case 3: strbu.AppendFormat("<input type=\"hidden\" name=\"oldhide_Temp_file\" value=\"{1}\">", str, str == "" ? "1" : str);
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(str) || str == "1") return strbu.ToString();


            strbu.AppendFormat("<span class=\"upload_filename\"><a href=\"{0}\" target=\"_blank\">查看</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\" title=\"删除附件\"><img style=\"vertical-align: middle\"     src=\"/images/cha.gif\"></a></span>", str);




            return strbu.ToString();
        }

    }
}
