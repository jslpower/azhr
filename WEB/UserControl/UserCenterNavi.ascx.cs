using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;

namespace Web.UserControl
{
    public partial class UserCenterNavi : System.Web.UI.UserControl
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-20
        /// 说明：用户中心导航


        private int _navIndex;
        /// <summary>
        /// 设置界面选中
        /// </summary>
        public int NavIndex
        {
            get { return _navIndex; }
            set { _navIndex = value; }
        }


        private int[] _privsList;
        /// <summary>
        /// 当前用户的权限列表
        /// </summary>
        public int[] PrivsList
        {
            get { return _privsList; }
            set { _privsList = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (!IsPostBack)
            {
                switch (this.NavIndex)
                {
                    //case 1: this.nav_1.Attributes.Add("class", "warmdefault"); break;
                    case 2: this.nav_2.Attributes.Add("class", "warmdefault"); break;
                    //case 3: this.nav_3.Attributes.Add("class", "warmdefault"); break;
                    //case 4: this.nav_4.Attributes.Add("class", "warmdefault"); break;
                    case 5: this.nav_5.Attributes.Add("class", "warmdefault"); break;
                    //case 6: this.nav_6.Attributes.Add("class", "warmdefault"); break;
                    case 7: this.nav_7.Attributes.Add("class", "warmdefault"); break;
                    //case 8: this.nav_8.Attributes.Add("class", "warmdefault"); break;
                    //case 9: this.nav_9.Attributes.Add("class", "warmdefault"); break;
                    //case 10: this.nav_10.Attributes.Add("class", "warmdefault"); break;
                    //case 10: this.nav_10.Attributes.Add("class", "warmdefault"); break;
                    //case 11: this.nav_11.Attributes.Add("class", "warmdefault"); break;
                    //case 12: this.nav_12.Attributes.Add("class", "warmdefault"); break;
                    default: this.nav_2.Attributes.Add("class", "warmdefault"); break;
                }
            }
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            //if (!PrivsList.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_订单提醒栏目))
            //    this.phnav_1.Visible = false;
            //else
            //    this.nav_1.Attributes.Add("href", "/UserCenter/WorkAwake/OrderRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒);
            if (!PrivsList.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_计调提醒栏目))
                this.phnav_2.Visible = false;
            else
                this.nav_2.Attributes.Add("href", "/UserCenter/WorkAwake/OperaterRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒);
            if (!PrivsList.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_收款提醒栏目))
                this.phnav_5.Visible = false;
            else
                this.nav_5.Attributes.Add("href", "/UserCenter/WorkAwake/CollectionRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒);
            if (!PrivsList.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目))
                this.phnav_7.Visible = false;
            else
                this.nav_7.Attributes.Add("href", "/UserCenter/WorkAwake/PlanChangeRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒);
            //if (!PrivsList.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_合同到期提醒栏目))
            //    this.phnav_9.Visible = false;
            //else
            //    this.nav_9.Attributes.Add("href", "/UserCenter/WorkAwake/LaborRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒);
            //if (!PrivsList.Contains((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_询价提醒栏目))
            //    this.phnav_10.Visible = false;
            //else
            //    this.nav_10.Attributes.Add("href", "/UserCenter/WorkAwake/InquiryRemind.aspx?sl=" + (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒);
        }
    }
}