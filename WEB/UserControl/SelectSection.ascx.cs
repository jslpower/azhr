using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.ComponentModel;

namespace Web.UserControl
{
    /// <summary>
    /// 选择部门
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-26
    public partial class SelectSection : System.Web.UI.UserControl
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string SectionID { get; set; }
        /// <summary>
        /// 部门名
        /// </summary>
        public string SectionName { get; set; }
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
        /// 弹出模式，"1":单选 "2":多选
        /// </summary>
        public string SModel { get; set; }
        /// <summary>
        /// 隐藏域客户端ID和Name
        /// </summary>
        public string SelectIDClient
        {
            get { return SetPriv + "_hideSellID"; }
        }
        /// <summary>
        /// 设置回调方法
        /// </summary>
        public string CallBackFun { get; set; }
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

        /// <summary>
        /// 设置是否为必填
        /// </summary>
        private bool _isNotValid = true;
        [Bindable(true)]
        public bool IsNotValid
        {
            get { return _isNotValid; }
            set { _isNotValid = value; }
        }
        /// <summary>
        /// 显示文本框客户端ID和Name
        /// </summary>
        public string SelectNameClient
        {
            get { return SetPriv + "_txtSellName"; }
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