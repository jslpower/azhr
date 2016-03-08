using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class selectWuPin : System.Web.UI.UserControl
    {
        protected string NoticeHTML = "valid=\"required\"errmsg=\"选用不能为空！\"";
       
        /// <summary>
        /// ID
        /// </summary>
        private string _hideID;
        /// <summary>
        /// 物品ID
        /// </summary>
        public string HideID
        {
            get { return _hideID; }
            set { _hideID = value; }
        }
        /// <summary>
        /// 选用名称
        /// </summary>
        private string _name;
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 是否必选默认值
        /// </summary>
        private bool _ismust = true;
        /// <summary>
        /// 获取或设置是否必选（默认：true）
        /// </summary>
        public bool IsMust
        {
            get { return _ismust; }
            set { _ismust = value; }
        }
        /// <summary>
        /// 获取IframeID
        /// </summary>
        protected string IframeID
        {
            get { return Utils.GetQueryStringValue("iframeId"); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack)
            {

                base.OnLoad(e);
            }
        }
        string _callback;
        /// <summary>
        /// 获取或设置回调函数方法名
        /// </summary>
        public string CallBack
        {
            get
            {
                if (string.IsNullOrEmpty(_callback)) return ClientID + "._callBack";

                return _callback;
            }
            set { _callback = value; }
        }
        private string _clienttext;
        /// <summary>
        /// 获取物品名称input name
        /// </summary>
        public string ClientText { get { return "txt_" + this.ClientID + "_Name"; } set { _clienttext = value; } }

        private string _clientvalue;
        /// <summary>
        /// 获取物品编号input value
        /// </summary>
        public string ClientValue { get { return "hd_" + this.ClientID + "_ID"; } set { _clientvalue = value; } }


        /// <summary>
        /// 获取选用按钮ID
        /// </summary>
        public string btnID { get { return "btn_" + this.ClientID + "_ID"; } }
    }
}