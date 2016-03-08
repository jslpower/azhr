using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
namespace Web.SmsCenter
{
    /// <summary>
    /// 常用短信编辑页
    /// 修改记录：
    /// 1、2012-04-18 曹胡生 创建
    /// </summary>
    public partial class SMSEdit : BackPage
    {
        //常用短信类型JSON字符串
        protected string TypeList = "1";
        //当前短信所选的短信类型
        protected string TypeSelValue = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "TypeAdd")
            {
                AddType();
            }
            else if (Utils.GetQueryStringValue("dotype") == "TypeDel")
            {
                DelType();
            }
            else if (Utils.GetQueryStringValue("dotype") == "btnSave")
            {
                Save();
            }
            PageInit();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void PageInit()
        {
            InitType();
            int Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (!Id.Equals(0))
            {
                var item = new EyouSoft.BLL.SmsStructure.BSmsPhrase().GetSmsPhrase(Id);
                if (item != null)
                {
                    TypeSelValue = item.SmsPhraseType.TypeId.ToString();
                    txtContent.Text = item.Content;
                }
            }
        }

        /// <summary>
        /// 添加类型
        /// </summary>
        private void AddType()
        {
            string TypeText = Utils.GetFormValue("txtTypeName");
            int Result = new EyouSoft.BLL.SmsStructure.BSmsPhraseType().AddSmsPhraseType(new EyouSoft.Model.SmsStructure.MSmsPhraseType()
            {
                CompanyId = CurrentUserCompanyID,
                IssueTime = DateTime.Now,
                OperatorId = SiteUserInfo.UserId,
                TypeName = TypeText
            });
            Response.Clear();
            if (Result == 1)
            {
                InitType();
                Response.Write(UtilsCommons.AjaxReturnJson("1", "类型添加成功!", InitType()));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "类型添加失败!"));
            }
            Response.End();
        }

        /// <summary>
        /// 删除类型
        /// </summary>
        private void DelType()
        {
            int TypeValue = Utils.GetInt(Utils.GetFormValue("selType"));
            int Result = new EyouSoft.BLL.SmsStructure.BSmsPhraseType().DelSmsPhraseType(TypeValue);
            Response.Clear();
            if (Result == 1)
            {
                InitType();
                Response.Write(UtilsCommons.AjaxReturnJson("1", "类型删除成功!", InitType()));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "类型删除失败!"));
            }
            Response.End();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string msg = string.Empty;
            int TypeValue = Utils.GetInt(Utils.GetFormValue("selType"));
            string Content = Utils.GetFormValue(txtContent.UniqueID);
            if (TypeValue == 0)
            {
                msg += "短信类型不能为空！</br>";
            }
            if (string.IsNullOrEmpty(Content))
            {
                msg += "短信内容不能为空";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            int Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            int Result = 0;
            if (Id == 0)
            {
                Result = new EyouSoft.BLL.SmsStructure.BSmsPhrase().AddSmsPhrase(new EyouSoft.Model.SmsStructure.MSmsPhrase()
                {
                    CompanyId = CurrentUserCompanyID,
                    Content = Content,
                    IssueTime = DateTime.Now,
                    OperatorId = SiteUserInfo.UserId,
                    SmsPhraseType = new EyouSoft.Model.SmsStructure.MSmsPhraseTypeBase() { TypeId = TypeValue }
                });

            }
            else
            {
                Result = new EyouSoft.BLL.SmsStructure.BSmsPhrase().UpdateSmsPhrase(new EyouSoft.Model.SmsStructure.MSmsPhrase()
                {
                    CompanyId = CurrentUserCompanyID,
                    Content = Content,
                    OperatorId = SiteUserInfo.UserId,
                    PhraseId = Id,
                    SmsPhraseType = new EyouSoft.Model.SmsStructure.MSmsPhraseTypeBase() { TypeId = TypeValue }
                });
            }
            Response.Clear();
            if (Result == 1)
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "保存成功!"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "保存失败!"));
            }
            Response.End();
        }

        /// <summary>
        /// 初始化短信类型
        /// </summary>
        private object InitType()
        {
            var list = new EyouSoft.BLL.SmsStructure.BSmsPhraseType().GetSmsPhraseTypeList(CurrentUserCompanyID);
            if (list != null && list.Count > 0)
            {
                TypeList = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            }
            return list;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_常用短信栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_常用短信栏目, false);
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
    }
}
