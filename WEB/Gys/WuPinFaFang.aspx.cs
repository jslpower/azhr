using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common; 

namespace EyouSoft.Web.Gys
{
    public partial class WuPinFaFang : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_FaFang = false;
        string WuPinId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            WuPinId = Utils.GetQueryStringValue("wupinid");
            if (string.IsNullOrEmpty(WuPinId)) RCWE("异常请求");

            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "submit") BaoCun();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_FaFang = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_发放登记);
            phSubmit.Visible = Privs_FaFang;
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var info = new EyouSoft.BLL.HGysStructure.BWuPin().GetInfo(WuPinId);
            if (info == null) RCWE("异常请求");

            ltrName.Text = info.Name;
            ltrOperatorName.Text = SiteUserInfo.Name;
        }

        /// <summary>
        /// baocun 
        /// </summary>
        void BaoCun()
        {
            if (!Privs_FaFang) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            var info = GetFormInfo();
            int bllRetCode = new EyouSoft.BLL.HGysStructure.BWuPin().InsertLingYong(info);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -1) RCWE(UtilsCommons.AjaxReturnJson("0", "物品数量不足"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MWuPinLingYongInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MWuPinLingYongInfo();

            info.IssueTime = DateTime.Now;
            info.LingYongLeiXing = EyouSoft.Model.EnumType.GysStructure.WuPinLingYongLeiXing.发放;
            info.LingYongRenId = Utils.GetFormValue(txtRen.SellsIDClient);
            info.OperatorId = SiteUserInfo.UserId;
            info.ShiJian = Utils.GetDateTime(Utils.GetFormValue(txtTime.UniqueID));
            info.ShuLiang = Utils.GetInt(Utils.GetFormValue(txtShuLiang.UniqueID));
            info.WuPinId = WuPinId;
            info.YongTu = Utils.GetFormValue(txtYongTu.UniqueID);

            return info;
        }
        #endregion
    }
}
