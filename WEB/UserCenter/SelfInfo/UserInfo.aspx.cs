using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.UserCenter.SelfInfo
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-20
    /// 说明：个人中心：个人信息
    public partial class UserInfo : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //根据ID初始化页面
                PageInit(id);
            }

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            this.txtCompanyName.Text = "";
            this.txtDepartment.Text = "";
            this.txtUserLogin.Text = "";
            this.txtPwd.Text = "";
            this.txtUserName.Text = "";
            if (true)
            {
                this.rdoBoy.Checked = true;
            }
            this.txtPosition.Text = "";
            this.txtPhone.Text = "";
            this.txtFax.Text = "";
            this.txtMobile.Text = "";
            this.txtQQ.Text = "";
            this.txtMsn.Text = "";
            this.txtEmail.Text = "";


        }


        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            string doType = Utils.GetQueryStringValue("doType");

            string companyName = this.txtCompanyName.Text;
            string departName = this.txtDepartment.Text;
            string userLogin = this.txtUserLogin.Text;
            string userPwd = this.txtPwd.Text;
            string userName = this.txtUserName.Text;
            string userSex = this.rdoBoy.Checked ? "男" : "女";
            string userPosition = this.txtPosition.Text;
            string userPhone = this.txtPhone.Text;
            string userFax = this.txtFax.Text;
            string userMobile = this.txtMobile.Text;
            string userQQ = this.txtQQ.Text;
            string userMsn = this.txtMsn.Text;
            string userEmail = this.txtEmail.Text;

            #region 表单验证
            string errorMsg = "";
            if (userPwd == "")
            {
                errorMsg = "请输入密码!<br />";
            }
            if (userName == "")
            {
                errorMsg += "请输入姓名!<br />";
            }
            if (userPhone =="")
            {
                errorMsg += "请输入联系电话!<br />";
            }
            if (errorMsg != "")
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, " tableToolbar._showMsg('" + errorMsg + "');");
                return;
            }

            #endregion
            
            bool result = true;


            if (result)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改成功!');;window.location='目标地址';");
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改失败!');;window.location='目标地址';");
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {

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



        protected void btnSave_Click(object sender, EventArgs e)
        {
            PageSave();
        }
    }
}
