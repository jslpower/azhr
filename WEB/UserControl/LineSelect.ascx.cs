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
    public partial class LineSelect : System.Web.UI.UserControl
    {

        /// <summary>
        /// 部门ID
        /// </summary>
        public string LineID { get; set; }
        /// <summary>
        /// 部门名
        /// </summary>
        public string LineName { get; set; }
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
            get { return parentIframeID ?? Utils.GetQueryStringValue("iframeId"); }
            set { parentIframeID = value; }
        }
        /// <summary>
        /// 设置回调方法
        /// </summary>
        public string CallBackFun { get; set; }
        /// <summary>
        /// 弹出模式，"1":单选 "2":多选
        /// </summary>
        public string SModel { get; set; }
        /// <summary>
        /// 隐藏域客户端ID和Name
        /// </summary>
        public string LineIDClient
        {
            get { return SetPriv + "_hideLineID";}
        }
        /// <summary>
        /// 显示文本框客户端ID和Name
        /// </summary>
        public string LineNameClient
        {
            get { return SetPriv + "_txtLineName"; }
        }
        /// <summary>
        /// 设置控件只读,默认为可以修改
        /// </summary>
        private bool _readOnly = false;
        [Bindable(true)]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }
        private bool _hideSelect = false;
        /// <summary>
        /// 隐藏选用按钮
        /// </summary>
        [Bindable(true)]
        public bool HideSelect {
            get { return _hideSelect; }
            set { _hideSelect = value; }
        }

        private string _setPriv = string.Empty;
        /// <summary>
        /// 指定控件Name前缀，默认为控件ClientID
        /// </summary>
        public string SetPriv
        {
            get { return string.IsNullOrEmpty(_setPriv) ? this.ClientID : _setPriv; }
            set { _setPriv = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}