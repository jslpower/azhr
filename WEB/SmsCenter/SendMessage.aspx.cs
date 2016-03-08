using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace Web.SmsCenter
{
    /// <summary>
    /// 短信发送页
    /// 修改记录:
    /// 1 2012-4-16 曹胡生 创建
    /// </summary>
    public partial class SendMessage : EyouSoft.Common.Page.BackPage
    {
        protected string showPay = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Send();
            }
            PageInit();
        }


        private void PageInit()
        {
            #region 初次加载
            //获取账户余额，如果少于等于0则显示充值按钮和短信提示信息
            decimal accountMoney = new EyouSoft.BLL.SmsStructure.BSmsAccount().GetSmsAccountYuE(CurrentUserCompanyID);
            if (accountMoney <= 0)
            {
                showPay = "style='display:block'";
                remainNum.Text = "你的短信剩余条数为0！";
                remainNum.Visible = true;
                PSendBtn.Visible = false;

            }
            else
            {
                showPay = "style='display:none'";
                remainNum.Visible = false;
            }
            txtSender.Text = SiteUserInfo.CompanyName;
            InitSendChannel();
            #endregion
        }

        //保存
        private void Send()
        {
            #region 获取发送手机号并验证
            List<EyouSoft.Model.SmsStructure.MSmsNumber> MobileList = new List<EyouSoft.Model.SmsStructure.MSmsNumber>();
            string[] moibleArr = Utils.InputText(Utils.GetFormValue("txtSendMobile")).Split(',', '，');
            System.Text.StringBuilder errBuilder = new System.Text.StringBuilder();
            string resultMess = string.Empty;
            if (string.IsNullOrEmpty(Utils.GetFormValue("txtSendContent")))
            {
                errBuilder.Append("发送内容不能为空!<br/>");
            }
            else if (string.IsNullOrEmpty(Utils.GetFormValue("txtSendMobile")))
            {
                errBuilder.Append("手机号不能为空!<br/>");
            }
            foreach (string mobile in moibleArr)
            {
                if (!Utils.IsMobile(mobile) && !Utils.IsPhone(mobile))//验证手机格式如果不正确则输出错误手机
                {
                    errBuilder.AppendFormat("{0}，", mobile);
                }
                else
                {
                    EyouSoft.Model.SmsStructure.MSmsNumber Mobile = new EyouSoft.Model.SmsStructure.MSmsNumber()
                    {
                        Code = mobile.Trim(),
                        Type = EyouSoft.Model.EnumType.SmsStructure.MobileType.Mobiel
                    };
                    MobileList.Add(Mobile);
                }
            }
            resultMess = errBuilder.ToString();

            #endregion

            //如果验证手机格式都通过则发送
            if (resultMess == "")
            {
                #region 构造短息信息实体
                //发送实体
                EyouSoft.Model.SmsStructure.MSendMessage SendMessage = new EyouSoft.Model.SmsStructure.MSendMessage();
                //发信人公司ID
                SendMessage.CompanyId = CurrentUserCompanyID;
                //发送通道
                SendMessage.SendChannel = Utils.GetInt(Utils.GetFormValue(this.ddlSelChannel.UniqueID));
                //发送类型
                SendMessage.SendType = (EyouSoft.Model.EnumType.SmsStructure.SendType)Utils.GetInt(Utils.GetFormValue("selSendType"));
                //发送时间
                SendMessage.SendTime = DateTime.Now;
                if (SendMessage.SendType == EyouSoft.Model.EnumType.SmsStructure.SendType.定时发送)
                {
                    SendMessage.SendTime = Utils.GetDateTime(Utils.GetFormValue("txtSendTime"));
                }
                else
                {
                    SendMessage.LeiXing = EyouSoft.Model.EnumType.SmsStructure.LeiXing.直接发送;
                }
                //短信内容
                SendMessage.SmsContent = Utils.GetFormValue("txtSendContent");
                //号码集合
                SendMessage.Mobiles = MobileList;
                //发送人ID
                SendMessage.UserId = SiteUserInfo.UserId;
                //发信人姓名(如果勾选了发信人)
                //if (chkSender.Checked)
                //{
                //    SendMessage.UserFullName = Utils.GetFormValue(txtSender.UniqueID);
                //}
                SendMessage.UserFullName = Utils.GetFormValue(txtSender.UniqueID);
                #endregion
                #region 执行发送
                //执行发送，返回发送结果实体
                EyouSoft.Model.SmsStructure.MSendResult resultInfo = new EyouSoft.BLL.SmsStructure.BSendMessage().Send(SendMessage);
                if (resultInfo != null)
                {
                    if (resultInfo.IsSucceed == true)
                    {
                        resultMess = string.Format("您本次共发送短信{0}个字{1}!<br/>发送移动、联通共{2}个号码、<br/>发送小灵通共{3}个号码、<br/>实际共消费金额为:{4}、<br/>实际发送短信{5}条!",
                        SendMessage.SmsContentSendComplete.Length,
                        !string.IsNullOrEmpty(SendMessage.UserFullName) ? "（包含发信人）" : "",
                        resultInfo.SuccessCount,
                        resultInfo.PhsSuccessCount,
                        resultInfo.CountFee.ToString("C2"),
                        resultInfo.SuccessCount + resultInfo.PhsSuccessCount);
                        Response.Clear();
                        Response.Write(UtilsCommons.AjaxReturnJson("1", resultMess));
                        Response.End();

                    }
                    else
                    {   //其他错误
                        Response.Clear();
                        Response.Write(UtilsCommons.AjaxReturnJson("0", resultInfo.ErrorMessage));
                        Response.End();
                    }
                }
                #endregion
            }
            else
            {
                //手机格式未通过输出消息
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", "数据输入有误!<br/>" + resultMess));
                Response.End();
            }
        }

        /// <summary>
        /// 初始化短信通道
        /// </summary>
        private void InitSendChannel()
        {
            var list = new EyouSoft.BLL.SmsStructure.BSmsAccount().GetSmsChannels();
            this.ddlSelChannel.DataTextField = "Name";
            this.ddlSelChannel.DataValueField = "Index";
            this.ddlSelChannel.DataSource = list;
            this.ddlSelChannel.DataBind();
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        private void PowerControl()
        {
            //判断权限
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_常用短信栏目))
            {
                EyouSoft.Common.Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目, true);
                return;
            }
            else if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_发送短信栏目))
            {
                EyouSoft.Common.Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_发送短信栏目, true);
                return;
            }
        }
    }
}
