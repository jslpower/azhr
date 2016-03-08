using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;


namespace Web.Crm
{
    /// <summary>
    /// 客户中心 个人会员列表
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public partial class List : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;

        /// <summary>
        /// 二级栏目
        /// </summary>
        EyouSoft.Model.EnumType.PrivsStructure.Menu2 Menu2Type = EyouSoft.Model.EnumType.PrivsStructure.Menu2.None;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitMenu2Type();
            PowerControl();

            #region ajax request
            switch (Utils.GetQueryStringValue("doType"))
            {
                case "delete": Delete(); break;
                default: break;
            }
            #endregion

            DataInit();
        }

        #region private members
        /// <summary>
        /// 初始化二级栏目
        /// </summary>
        void InitMenu2Type()
        {
            Menu2Type = Utils.GetEnumValue<EyouSoft.Model.EnumType.PrivsStructure.Menu2>(Utils.GetQueryStringValue("sl"), EyouSoft.Model.EnumType.PrivsStructure.Menu2.None);

            switch (Menu2Type)
            {
                case EyouSoft.Model.EnumType.PrivsStructure.Menu2.客户中心_个人会员: break;
                default: AjaxResponse("错误的请求"); break;
            }
        }

        /// <summary>
        /// get search info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.CrmStructure.MLBCrmPersonalSearchInfo GetSearchInfo()
        {
            var info = new EyouSoft.Model.CrmStructure.MLBCrmPersonalSearchInfo();

            info.JiFenOperator = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(txtJiFen.ClientUniqueIDOperator));
            info.JiFenOperatorNumber = Utils.GetIntNull(Utils.GetQueryStringValue(txtJiFen.ClientUniqueIDOperatorNumber), 0);
            info.MemberCardCode = Utils.GetQueryStringValue("txtCardCode");
            info.MemberTypeId = Utils.GetIntNull(Utils.GetQueryStringValue("txtMemberTypeId"));
            info.Name = Utils.GetQueryStringValue("txtName");
            //info.Telephone = Utils.GetQueryStringValue("txtTelephone");

            return info;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            pageIndex = UtilsCommons.GetPadingIndex();

            var items = new EyouSoft.BLL.CrmStructure.BCrm().GetCrmsPersonal(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, GetSearchInfo());
            if (items != null && items.Count > 0)
            {
                rptList.DataSource = items;
                rptList.DataBind();
            }

            BindPage();
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            paging.UrlParams = Request.QueryString;
            paging.intPageSize = pageSize;
            paging.CurrencyPage = pageIndex;
            paging.intRecordCount = recordCount;

            if (paging.intRecordCount == 0)
            {
                paging.Visible = false;
                phEmpty.Visible = true;
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_栏目, false);
                return;
            }

            if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_新增)) phXinZeng.Visible = true;
            if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_修改)) phXiuGai.Visible = true;
            if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_删除)) phShanChu.Visible = true;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <returns></returns>
        void Delete()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_个人会员_删除))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("0"));
            }

            string retCode = "0";
            string[] ids = Utils.GetQueryStringValue("deleteids").Split(',');

            if (ids != null && ids.Length > 0)
            {
                foreach (var s in ids)
                {
                    new EyouSoft.BLL.CrmStructure.BCrm().Delete(s, SiteUserInfo.UserId);
                }
            }

            retCode = "1";

            AjaxResponse(UtilsCommons.AjaxReturnJson(retCode));
        }
        #endregion        

        #region protected members
        /// <summary>
        /// 绑定会员类型
        /// </summary>
        /// <returns></returns>
        protected string GetMemberTypesHTML()
        {
            StringBuilder s = new StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComMemberType> items = new EyouSoft.BLL.ComStructure.BComMemberType().GetList(base.SiteUserInfo.CompanyId);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("<option value=\"{0}\">{1}</option>", item.Id, item.TypeName);
                }
            }

            return s.ToString();
        }
        #endregion
    }
}
