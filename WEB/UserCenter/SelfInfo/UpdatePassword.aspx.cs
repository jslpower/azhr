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
using EyouSoft.Common.Page;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Common;

namespace Web.UserCenter.SelfInfo
{
    public partial class UpdatePassword : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region ajax操作
            //获得ajax类型
            string dotype = Utils.GetQueryStringValue("doType");
            if (!string.IsNullOrEmpty(dotype))
                Ajax(dotype);
            #endregion
            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit();
            }
        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
        }

        #region ajax 操作
        private void Ajax(string type)
        {
            switch (type)
            {
                case "AjaxPwd"://ajax操作修改密码
                    PageSave();
                    break;
            }
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            //ajax返回信息
            string returnmes = "";
            //原始密码
            string pwd = Utils.GetFormValue(txtPwd.UniqueID);
            //新密码
            string newPwd = Utils.GetFormValue(txtNewPwd.UniqueID);
            //确认密码
            string surePwd = Utils.GetFormValue(txtSurePwd.UniqueID);
            //修改操作之后的结果
            bool result = false;
            //实例化业务层
            BIndividual bllBIndividual = new BIndividual();
            result = bllBIndividual.PwdModify(SiteUserInfo.UserId, pwd, newPwd);
            if (result)
            {
                returnmes = EyouSoft.Common.UtilsCommons.AjaxReturnJson("true", "修改成功");
            }
            else
            {
                returnmes = EyouSoft.Common.UtilsCommons.AjaxReturnJson("false", "修改失败,查看原始密码是否正确");
            }
            Response.Clear();
            Response.Write(returnmes);
            Response.End();

        }

        #endregion

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
    }
}
