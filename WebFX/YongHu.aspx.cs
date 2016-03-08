using System;

namespace EyouSoft.WebFX
{
    using EyouSoft.Common.Page;

    public partial class YongHu : FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Update"))
                {
                    Response.Write(DoUpdate());
                    Response.End();
                }
            }

            if (!IsPostBack)
            {
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                this.HeadDistributorControl1.IsPubLogin =
        System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == this.SiteUserInfo.Username;
            }
        }

        /// <summary>
        /// 获取当前用户的明文密码
        /// </summary>
        /// <returns></returns>
        private string GetPassword()
        {
            EyouSoft.BLL.ComStructure.BComUser BUser = new EyouSoft.BLL.ComStructure.BComUser();
            EyouSoft.Model.ComStructure.MComUser User = BUser.GetModel(SiteUserInfo.UserId, SiteUserInfo.CompanyId);
            return User.Password;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        private string DoUpdate()
        {
            string Result = string.Empty;

            string username = EyouSoft.Common.Utils.GetFormValue("txtUserName");
            string newPwd = EyouSoft.Common.Utils.GetFormValue("txtNewPwd");
            string surePwd = EyouSoft.Common.Utils.GetFormValue("txtSurePwd");
            string old = GetPassword();

            if (string.IsNullOrEmpty(username))
            {
                Result += string.Format("{0}</br>", GetGlobalResourceObject("string", "用户名不能为空"));
            }
            if (new EyouSoft.BLL.ComStructure.BComUser().IsExistsUserName(username,SiteUserInfo.CompanyId,SiteUserInfo.UserId))
            {
                Result += string.Format("{0}</br>", GetGlobalResourceObject("string", "用户名已存在"));
            }
            if (string.IsNullOrEmpty(newPwd))
            {
                Result += string.Format("{0}</br>", GetGlobalResourceObject("string", "密码不能为空"));
            }
            if (!newPwd.Equals(surePwd))
            {
                Result += string.Format("{0}</br>", GetGlobalResourceObject("string", "两次输入的密码不一致"));
            }
            if (Result.Length <= 0)
            {
                var BUser = new EyouSoft.BLL.ComStructure.BComUser();

                if (BUser.PwdModify(SiteUserInfo.UserId,username, old, surePwd))
                {
                    Result = EyouSoft.Common.UtilsCommons.AjaxReturnJson("1",(string)GetGlobalResourceObject("string","密码修改成功"));
                }
                else
                {
                    Result = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0",(string)GetGlobalResourceObject("string","密码修改成功")); ;
                }
            }
            else
            {
                Result = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", Result);
            }
            return Result;
        }
    }
}
