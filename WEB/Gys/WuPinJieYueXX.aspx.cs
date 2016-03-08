using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class WuPinJieYueXX : EyouSoft.Common.Page.BackPage
    {
        string WuPinId = string.Empty;
        bool Privs_GuiHuan = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            WuPinId = Utils.GetQueryStringValue("wupinid");
            if (string.IsNullOrEmpty(WuPinId)) RCWE("异常请求");

            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "guihuan") GuiHuan();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_GuiHuan = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_借阅管理);
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var items = new EyouSoft.BLL.HGysStructure.BWuPin().GetLingYongs(WuPinId, EyouSoft.Model.EnumType.GysStructure.WuPinLingYongLeiXing.借阅);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        /// <summary>
        /// gui huan
        /// </summary>
        void GuiHuan()
        {
            if (!Privs_GuiHuan) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));

            string s = Utils.GetQueryStringValue("jieyueid");

            int bllRetCode = new EyouSoft.BLL.HGysStructure.BWuPin().GuiHuan(WuPinId, s, SiteUserInfo.UserId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion

        #region protected members
        /// <summary>
        /// get status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetStatus(object status)
        {
            if (status == null) return string.Empty;
            var _status = (EyouSoft.Model.EnumType.GysStructure.WuPinJieYueStatus)status;

            if (_status == EyouSoft.Model.EnumType.GysStructure.WuPinJieYueStatus.已归还)
            {
                return "已归还";
            }

            return "借阅中&nbsp;<a href='javascript:void(0)' class='i_guihuan'>归还</a>";
        }
        #endregion
    }
}
