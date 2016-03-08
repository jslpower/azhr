using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.EnumType.IndStructure;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.UserControl
{
    public partial class HeadOrderControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-27
        /// 说明：模板页头部 订单提醒


        private MUserInfo _userInfo = null;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public MUserInfo SiteUserInfo
        {
            get
            {
                return _userInfo;
            }
            set { _userInfo = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SiteUserInfo != null)
                {

                }
            }
        }

        /// <summary>
        /// 判断当前用户是否有权限
        /// </summary>
        /// <param name="permissionId">权限ID</param>
        /// <returns></returns>
        private bool CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs permission)
        {
            if (_userInfo == null) return false;
            return _userInfo.Privs.Contains((int)permission);
        }

        /// <summary>
        /// 根据类型获取提醒
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetRemindCountByType()
        {
            string result = "";
            BIndividual bllBIndividual = new BIndividual();
            foreach (EnumObj item in EnumObj.GetList(typeof(RemindType)))
            {
                int count = bllBIndividual.GetRemindCountByType(SiteUserInfo.CompanyId, SiteUserInfo.UserId, (RemindType)Utils.GetInt(item.Value));
                if (count > 0)
                {
                    string url = "";
                    switch ((RemindType)Utils.GetInt(item.Value))
                    {
                        case RemindType.变更提醒:
                            if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目))
                            {
                                url = "/UserCenter/WorkAwake/PlanChangeRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目;
                                result += "<li><a href=\"" + url + "\">最新提醒:" + item.Text + "<b class=\"fontred\">" + count + "件</b></a></li>";
                            }

                            break;
                        //case RemindType.订单提醒:
                        //    if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_订单提醒栏目))
                        //    {
                        //        url = "/UserCenter/WorkAwake/OrderRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目;
                        //        result += "<li><a href=\"" + url + "\">最新提醒:" + item.Text + "<b class=\"fontred\">" + count + "件</b></a></li>";
                        //    }

                        //    break;
                        //case RemindType.合同到期提醒:
                        //    if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_合同到期提醒栏目))
                        //    {
                        //        url = "/UserCenter/WorkAwake/LaborRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目;
                        //        result += "<li><a href=\"" + url + "\">最新提醒:" + item.Text + "<b class=\"fontred\">" + count + "件</b></a></li>";
                        //    }

                        //    break;
                        case RemindType.计调提醒:
                            if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_计调提醒栏目))
                            {
                                url = "/UserCenter/WorkAwake/OperaterRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目;
                                result += "<li><a href=\"" + url + "\">最新提醒:" + item.Text + "<b class=\"fontred\">" + count + "件</b></a></li>";
                            }

                            break;
                        case RemindType.收款提醒:
                            if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_收款提醒栏目))
                            {
                                url = "/UserCenter/WorkAwake/CollectionRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目;
                                result += "<li><a href=\"" + url + "\">最新提醒:" + item.Text + "<b class=\"fontred\">" + count + "件</b></a></li>";
                            }

                            break;
                        //case RemindType.预控到期提醒:
                        //    if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_预控到期提醒栏目))
                        //    {
                        //        url = "/UserCenter/WorkAwake/Preview/PreviewHotel.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目;
                        //        result += "<li><a href=\"" + url + "\">最新提醒:" + item.Text + "<b class=\"fontred\">" + count + "件</b></a></li>";
                        //    }

                        //    break;
                        //case RemindType.询价提醒:
                        //    if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_询价提醒栏目))
                        //    {
                        //        url = "/UserCenter/WorkAwake/InquiryRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目;
                        //        result += "<li><a href=\"" + url + "\">最新提醒:" + item.Text + "<b class=\"fontred\">" + count + "件</b></a></li>";
                        //    }

                        //    break;
                    }

                }

            }
            if (string.IsNullOrEmpty(result))
                result = "";
            return result;
        }
    }
}