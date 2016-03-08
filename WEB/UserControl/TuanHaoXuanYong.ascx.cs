using System;

namespace EyouSoft.Web.UserControl
{
    using EyouSoft.Common;

    public partial class TuanHaoXuanYong : System.Web.UI.UserControl
    {
        #region attribute

        /// <summary>
        /// 弹窗标题默认值
        /// </summary>
        private string _boxytitle = "团号";
        /// <summary>
        /// 用户控件本身标题
        /// </summary>
        private string _thistitle = string.Empty;
        /// <summary>
        /// 是否多选默认值
        /// </summary>
        private int _ismultiple = 1;
        /// <summary>
        /// 用户控件默认class
        /// </summary>
        private string _txtcssclass = "inputtext formsize120 ";
        /// <summary>
        /// 失去焦点是否强制匹配第一个
        /// </summary>
        private bool _selectfrist = true;
        /// <summary>
        /// 获取或设置团队Id
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 获取或设置团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 获取或设置回调函数方法名
        /// </summary>
        public string CallBack { get; set; }
        /// <summary>
        /// 失去焦点是否强制匹配第一个
        /// </summary>
        public bool SelectFrist
        {
            get { return _selectfrist; }
            set { _selectfrist = value; }
        }
        /// <summary>
        /// 获取或设置弹窗标题
        /// </summary>
        public string BoxyTitle
        {
            get { return _boxytitle; }
            set { _boxytitle = value; }
        }
        /// <summary>
        /// 获取或设置本身标题
        /// </summary>
        public string ThisTitle
        {
            get
            {
                if (_thistitle.Length > 0 &&
                    _thistitle.Substring(_thistitle.Length - 1) != ":" &&
                    _thistitle.Substring(_thistitle.Length - 1) != "：")
                {
                    return _thistitle + "：";
                }
                return _thistitle;
            }
            set { _thistitle = value; }
        }
        /// <summary>
        /// 获取或设置是否多选（单选设置 1，多选设置为2;默认1）
        /// </summary>
        public int IsMultiple
        {
            get { return _ismultiple; }
            set { _ismultiple = value; }
        }

        /// <summary>
        /// 获取或设置用户控件样式（默认：inputtext formsize120）
        /// </summary>
        public string TxtCssClass
        {
            get { return _txtcssclass; }
            set { _txtcssclass = value; }
        }
        /// <summary>
        /// 获取IframeID
        /// </summary>
        protected string IframeID
        {
            get { return Utils.GetQueryStringValue("iframeId"); }
        }
        /// <summary>
        /// 获取团队编号input name
        /// </summary>
        public string ClientNameTourId { get { return "hd_" + this.ClientID + "_TourId"; } }
        /// <summary>
        /// 获取团号input name
        /// </summary>
        public string ClientNameTourCode { get { return "txt_" + this.ClientID + "_TourCode"; } }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = TourId ?? Request.QueryString[ClientNameTourId] ?? Request.Form[ClientNameTourId] ?? string.Empty;

            TourCode = TourCode ?? Request.QueryString[ClientNameTourCode] ?? Request.Form[ClientNameTourCode] ?? string.Empty;
        }
    }
}