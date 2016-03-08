using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class WuPinEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        string WuPinId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            WuPinId = Utils.GetQueryStringValue("wupinid");
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
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_入库登记);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_物品管理_物品修改);

            if (string.IsNullOrEmpty(WuPinId))
            {
                phSubmit.Visible = Privs_Insert;
            }
            else
            {
                phSubmit.Visible = Privs_Update;
            }
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(WuPinId))
            {
                ltrOperatorName.Text = SiteUserInfo.Name;
                return;
            }

            var info = new EyouSoft.BLL.HGysStructure.BWuPin().GetInfo(WuPinId);
            if (info == null) RCWE("异常请求");

            txtName.Value = info.Name;
            txtShuLiang.Value = info.ShuLiangRK.ToString();
            txtDanJia.Value = info.DanJia.ToString("F2");
            txtTime.Value = info.RuKuTime.ToString("yyyy-MM-dd");
            txtYongTu.Value = info.YongTu;
            txtBeiZhu.Value = info.BeiZhu;
            ltrOperatorName.Text = info.OperatorName;
        }

        /// <summary>
        /// bao cun
        /// </summary>
        void BaoCun()
        {
            if (string.IsNullOrEmpty(WuPinId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            var info = GetFormInfo();

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(WuPinId))
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BWuPin().Insert(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BWuPin().Update(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -1) RCWE(UtilsCommons.AjaxReturnJson("0", "物品数量不能小于已使用数量"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MWuPinInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MWuPinInfo();

            info.BeiZhu = Utils.GetFormValue(txtBeiZhu.UniqueID);
            info.CompanyId = SiteUserInfo.CompanyId;
            info.DanJia = Utils.GetDecimal(Utils.GetFormValue(txtDanJia.UniqueID));
            info.IssueTime = DateTime.Now;
            info.Name = Utils.GetFormValue(txtName.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.RuKuTime = Utils.GetDateTime(Utils.GetFormValue(txtTime.UniqueID), DateTime.Now);
            info.ShuLiangRK = Utils.GetInt(Utils.GetFormValue(txtShuLiang.UniqueID));
            info.WuPinId = WuPinId;
            info.YongTu = Utils.GetFormValue(txtYongTu.UniqueID);

            return info;
        }

        #endregion
    }
}
