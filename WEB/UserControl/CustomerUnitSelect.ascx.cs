using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.CrmStructure;
using EyouSoft.Model.EnumType.PlanStructure;

namespace Web.UserControl
{
    /// <summary>
    /// 客户单位选用用户控件
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-20
    /// 用户控件备注:
    /// 
    /// 1、不管是否配置回调方法
    ///     客户单位或者个人会员 ID，名称，类型必然会被赋值
    ///     
    /// 2、自定义回调方法参数说明:
    ///     客户单位或者个人会员名称     CustomerUnitName
    ///     客户单位或者个人会员Id       CustomerUnitId
    ///     客户单位或者个人会员类型     CustomerUnitType
    ///     客户单位或者个人会员联系人Id CustomerUnitContactId
    ///     客户单位或者个人会员联系人   CustomerUnitContactName
    ///     客户单位或者个人会员联系手机 CustomerUnitMobilePhone
    ///     客户单位或者个人会员联系电话 CustomerUnitContactPhone
    ///     客户单位或者个人会员部门     CustomerUnitDepartment
    ///     客户单位或者个人会员客户等级 CustomerUnitLV
    /// 3、多选情况下
    ///    多个公司之间用           ,(逗号)   隔开
    ///    同公司多个联系人信息用   |(竖线)    隔开
    public partial class CustomerUnitSelect : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CustomerUnitId = CustomerUnitId ?? Request.QueryString[ClientNameKHBH] ?? Request.Form[ClientNameKHBH] ?? string.Empty;

            CustomerUnitName = CustomerUnitName ?? Request.QueryString[ClientNameKHMC] ?? Request.Form[ClientNameKHMC] ?? string.Empty;

            CustomerUnitType = (CrmType)Utils.GetInt(((Request.QueryString[ClientNameKHLX] ?? Request.Form[ClientNameKHLX]) ?? ((int)CustomerUnitType).ToString()));

        }
        #region 用户控件属性
        private bool _isuniqueness = true;
        /// <summary>
        /// 弹窗标题默认值
        /// </summary>
        private string _boxytitle = "客户单位";
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
        /// 客户类型默认值
        /// </summary>
        private CrmType _customerunittype = CrmType.单位直客;
        /// <summary>
        /// 是否报名时选用
        /// </summary>
        private bool _isapply = false;
        /// <summary>
        /// 默认选项卡
        /// </summary>
        private PlanProject? _defaulttab = null;
        /// <summary>
        /// 失去焦点是否强制匹配第一个
        /// </summary>
        private bool _selectfrist = true;
        /// <summary>
        /// 获取或设置客户单位Id
        /// </summary>
        public string CustomerUnitId { get; set; }
        /// <summary>
        /// 获取或设置客户单位名称
        /// </summary>
        public string CustomerUnitName { get; set; }
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
        /// 获取或设置默认选项卡(null表示默认客户单位,其他枚举表示供应商)
        /// </summary>
        public PlanProject? DefaultTab
        {
            get { return _defaulttab; }
            set { _defaulttab = value; }
        }
        /// <summary>
        /// 是否报名时选用 默认 flalse不显示(报名时选用 必须选择明确的联系人)
        /// </summary>
        public bool IsApply
        {
            get { return _isapply; }
            set { _isapply = value; }
        }
        /// <summary>
        /// 是否只选择客户单位(默认为只选择客户单位)
        /// </summary>
        public bool IsUniqueness
        {
            get { return _isuniqueness; }
            set { _isuniqueness = value; }
        }
        /// <summary>
        /// 获取或设置客户单位类型（默认：）
        /// </summary>
        public CrmType CustomerUnitType
        {
            get { return _customerunittype; }
            set { _customerunittype = value; }
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
        /// 获取客户单位编号input name
        /// </summary>
        public string ClientNameKHBH { get { return "hd_" + this.ClientID + "_customerUnitId"; } }
        /// <summary>
        /// 获取客户单位名称input name
        /// </summary>
        public string ClientNameKHMC { get { return "txt_" + this.ClientID + "_customerUnitName"; } }
        /// <summary>
        /// 获取客户单位类型input name
        /// </summary>
        public string ClientNameKHLX { get { return "hd_" + this.ClientID + "_customerUnitType"; } }
        #endregion
    }
}