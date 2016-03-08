using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class CanGuanCaiDanEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        string GysId = string.Empty;
        string CaiDanId = string.Empty;

        bool Privs_Insert = false;
        bool Privs_Update = false;

        protected string txtIsShowOrHidden = "0";
        protected EyouSoft.Model.EnumType.SysStructure.LngType LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GysId = Utils.GetQueryStringValue("gysid");
            CaiDanId = Utils.GetQueryStringValue("caidanid");
            LngType = Utils.GetEnumValue<EyouSoft.Model.EnumType.SysStructure.LngType>(Utils.GetQueryStringValue("lng"), EyouSoft.Model.EnumType.SysStructure.LngType.中文);

            if (string.IsNullOrEmpty(GysId)) RCWE("异常请求");

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
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_新增);
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(CaiDanId)) return;
            var info = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCanGuanCaiDanInfo(CaiDanId, LngType);

            if (info == null) info = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCanGuanCaiDanInfo(CaiDanId);
            if (info == null) RCWE("异常请求");

            phLng.Visible = true;

            txtName.Value = info.Name;
            txtNeiRong.Value = info.NeiRong;
            txtZMS.Value = info.JiaGeZMS.ToString("F2");
            txtZTH.Value = info.JiaGeZTH.ToString("F2");
            txtZJS.Value = info.JiaGeZJS.ToString("F2");
            txtRMS.Value = info.JiaGeRMS.ToString("F2");
            txtRTH.Value = info.JiaGeRTH.ToString("F2");
            txtRJS.Value = info.JiaGeRJS.ToString("F2");
            txtTMianYiM.Value = info.TMianM.ToString();
            txtTMianYiN.Value = info.TMianN.ToString();
            if (info.IsDisplay) txtIsShowOrHidden = "1";
        }

        /// <summary>
        /// bao cun
        /// </summary>
        void BaoCun()
        {
            if (string.IsNullOrEmpty(CaiDanId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            var info = GetFormInfo();
            int bllRetCode = 0;

            if (string.IsNullOrEmpty(CaiDanId))
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BJiaGe().InsertCanGuanCaiDan(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BJiaGe().UpdateCanGuanCaiDan(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo();
            info.CaiDanId = CaiDanId;
            info.GysId = GysId;
            info.IssueTime = DateTime.Now;
            info.JiaGeRJS = Utils.GetDecimal(Utils.GetFormValue(txtRJS.UniqueID));
            info.JiaGeRMS = Utils.GetDecimal(Utils.GetFormValue(txtRMS.UniqueID));
            info.JiaGeRTH = Utils.GetDecimal(Utils.GetFormValue(txtRTH.UniqueID));
            info.JiaGeZJS = Utils.GetDecimal(Utils.GetFormValue(txtZJS.UniqueID));
            info.JiaGeZMS = Utils.GetDecimal(Utils.GetFormValue(txtZMS.UniqueID));
            info.JiaGeZTH = Utils.GetDecimal(Utils.GetFormValue(txtZTH.UniqueID));
            info.Name = Utils.GetFormValue(txtName.UniqueID);
            info.NeiRong = Utils.GetFormValue(txtNeiRong.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.LngType = LngType;
            info.TMianM = Utils.GetInt(Utils.GetFormValue(txtTMianYiM.UniqueID));
            info.TMianN = Utils.GetInt(Utils.GetFormValue(txtTMianYiN.UniqueID));
            int result = Utils.GetInt(Utils.GetFormValue("txtIsShowOrHidden"));
            info.IsDisplay = Convert.ToBoolean(result);

            return info;
        }
        #endregion
    }
}
