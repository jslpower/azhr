using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.ComponentModel;

namespace EyouSoft.Web.UserControl
{
    public partial class SelectCity : System.Web.UI.UserControl
    {
        protected string NoticeHTML = "valid=\"required\"errmsg=\"选用不能为空！\"";
        /// <summary>
        /// 单选设置 1，多选设置为2;默认单选
        /// </summary>
        private int _sMode;
        /// <summary>
        /// 单选设置 1，多选设置为2;默认单选
        /// </summary>
        public int SMode
        {
            get { return _sMode; }
            set { _sMode = value; }
        }
        /// <summary>
        /// 城市编号
        /// </summary>
        private string _cityID;
        /// <summary>
        /// 城市编号
        /// </summary>
        public string CityID
        {
            get { return _cityID; }
            set { _cityID = value; }
        }
        /// <summary>
        /// 是否强制选中和失去焦点选择
        /// </summary>
        private bool _selectFirst = true;
        [Bindable(true)]
        public bool SelectFrist
        {
            get { return _selectFirst; }
            set { _selectFirst = value; }
        }
        /// <summary>
        /// 选用名称
        /// </summary>
        private string _name;
        /// <summary>
        /// 城市名称
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
        /// 获取或设置回调函数方法名
        /// </summary>
        public string CallBack { get; set; }
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
        private string _clienttext;
        /// <summary>
        /// 获取城市名称
        /// </summary>
        public string ClientText { get { return "txt_" + this.ClientID + "_Name"; } set { _clienttext = value; } }

        private string _clientvalue;
        /// <summary>
        /// 获取城市编号
        /// </summary>
        public string ClientValue { get { return "hd_" + this.ClientID + "_ID"; } set { _clientvalue = value; } }

        /// <summary>
        /// 获取选用按钮ID
        /// </summary>
        public string btnID { get { return "btn_" + this.ClientID + "_ID"; } }
    }
}