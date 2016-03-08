using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.WebFX
{
    public partial class SetSeat : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置标题
        /// </summary>
        public string SetTitle { get; set; }

        /// <summary>
        /// 弹窗父级iframeID
        /// </summary>
        private string parentIframeID;
        public string ParentIframeID
        {
            get { return parentIframeID ?? EyouSoft.Common.Utils.GetQueryStringValue("iframeId"); }
            set { parentIframeID = value; }
        }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 回调函数
        /// </summary>
        public string CallBackFun { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string LabMsgText { get; set; }
        /// <summary>
        /// 提示信息ClientID
        /// </summary>
        public string LabMsgClientID { get { return this.SetPriv + "_labMsgClientID"; } }

        /// <summary>
        /// 人数
        /// </summary>
        public string PeoNum { get; set; }

        /// <summary>
        /// 订单原人数
        /// </summary>
        public string OldPeoNum { get; set; }

        public string PeoNumClientID { get { return this.SetPriv + "_peoNumHidCilentID"; } }

        private string _setPriv = string.Empty;
        /// <summary>
        /// 指定控件Name前缀，默认为控件ClientID
        /// </summary>
        public string SetPriv
        {
            get { return string.IsNullOrEmpty(_setPriv) ? this.ClientID : _setPriv; }
            set { _setPriv = value; }
        }
        /// <summary>
        /// 隐藏域的客户端ID
        /// </summary>
        public string setSeatHidClientID { get { return this.SetPriv + "_setSeatHidClientID"; } }
        /// <summary>
        /// 隐藏域的值
        /// </summary>
        public string setSeatHidValue { get; set; }

        /// <summary>
        /// 已被安拍的座位
        /// </summary>
        public string HidSeatedData { get; set; }
        /// <summary>
        /// 已被安拍的座位ClientID 
        /// </summary>
        public string HidSeatedDataClientID { get { return this.SetPriv + "_hidSeatedDataClientID"; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}