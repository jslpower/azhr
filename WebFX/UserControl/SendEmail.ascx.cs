namespace EyouSoft.WebFX.UserControl
{
    using System;

    /// <summary>
    /// 邮件发送控件
    /// </summary>
	public partial class SendEmail : System.Web.UI.UserControl
	{
        #region Attribute
        /// <summary>
        /// 当前公司ID
        /// </summary>
        public string CurrCompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 当前公司名称
        /// </summary>
        public string CurrCompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 打印类型[1:行程单;2:报价单;3:计调安排确认单;4:出团通知书;5:出团确认单;6:费用结算单]
        /// </summary>
        public int PrintType
        {
            get;
            set;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
	}
}