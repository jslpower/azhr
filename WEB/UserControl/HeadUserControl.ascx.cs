using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;

namespace Web.UserControl
{
    public partial class HeadUserControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-27
        /// 说明：模板页头部用户信息

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
            set
            {
                _userInfo = value;
            }
        }

        protected string resultPrivs = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (!IsPostBack)
            {
                if (SiteUserInfo != null)
                {

                    this.lblUserName.Text = SiteUserInfo.Name + "    您好!";
                }
            }
        }


        /// <summary>
        /// 判断权限
        /// </summary>
        private void PowerControl()
        {
            IList<EyouSoft.Model.SysStructure.MComMenu1Info> menuList = new EyouSoft.BLL.SysStructure.BSysMenu().GetComMenus(SiteUserInfo.SysId);
            var isGrant = false;

            //个人中心
            IList<EyouSoft.Model.SysStructure.MComMenu1Info> geren = menuList.Where(m => m.Name == "个人中心" && m.Menu2s != null).ToList();
            if (geren.Count > 0 && geren[0].Menu2s != null)
            {
                foreach (var m in geren[0].Menu2s.Where(m => this.SiteUserInfo.Privs.Contains(m.Menu2PrivId)))
                {
                    this.resultPrivs += "<a href=\"" + m.Url + "\"><s></s>" + m.Name + "</a>";
                    isGrant = true;
                }
            }
            if (!isGrant)
            {
                this.phPersonCenter.Visible = false;
            }
            else
            {
                isGrant = false;
            }

            //常用工具
            IList<EyouSoft.Model.SysStructure.MComMenu1Info> gongju = menuList.Where(m => m.Name == "常用工具" && m.Menu2s != null).ToList();
            if (gongju.Count > 0 && gongju[0].Menu2s != null)
            {
                this.resultPrivs += "*";
                foreach (var m in gongju[0].Menu2s.Where(m => this.SiteUserInfo.Privs.Contains(m.Menu2PrivId)))
                {
                    this.resultPrivs += "<a href=\"" + m.Url + "\"><s></s>" + m.Name + "</a>";
                    isGrant = true;
                }
            }
            if (!isGrant)
            {
                this.phTools.Visible = false;
            }
            else
            {
                isGrant = false;
            }

            //系统设置
            IList<EyouSoft.Model.SysStructure.MComMenu1Info> xitong = menuList.Where(m => m.Name == "系统设置" && m.Menu2s != null).ToList();
            if (xitong.Count > 0 && xitong[0].Menu2s != null)
            {
                this.resultPrivs += "*";
                foreach (var m in xitong[0].Menu2s.Where(m => this.SiteUserInfo.Privs.Contains(m.Menu2PrivId)))
                {
                    this.resultPrivs += "<a href=\"" + m.Url + "\"><s></s>" + m.Name + "</a>";
                    isGrant = true;
                }
            }
            if (!isGrant)
            {
                this.phSystemSet.Visible = false;
            }
            else
            {
                isGrant = false;
            }
        }
    }
}