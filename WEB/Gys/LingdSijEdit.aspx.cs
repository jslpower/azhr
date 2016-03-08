using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class LingdSijEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        string GysId = string.Empty;
        bool Privs_Insert = false;
        bool Privs_Update = false;
        bool Privs_Delete = false;
        EyouSoft.Model.EnumType.GysStructure.GysLeiXing LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.领队;

        protected string Gender = "0";
        protected string PingJiaLeiXing = "1";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GysId = Utils.GetQueryStringValue("gysid");
            if (SL == "43") LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.司机;

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
            if (LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.领队)
            {
                Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_领队管理_新增);
                Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_领队管理_修改);
                Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_领队管理_删除);
            }

            if (LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.司机)
            {
                Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_司机管理_新增);
                Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_司机管理_修改);
                Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_司机管理_删除);
            }
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(GysId)) return;

            var info = new EyouSoft.BLL.HGysStructure.BSiJi().GetInfo(GysId);
            if (info == null) RCWE("异常请求");

            txtName.Value = info.Name;
            txtTelephone.Value = info.Telephone;
            txtMobile.Value = info.Mobile;
            txtAddress.Value = info.Address;
            txtBeiZhu.Value = info.BeiZhu;
            txtPingJiaNeiRong.Value = info.PingJiaNeiRong;

            Gender = ((int)info.Gender).ToString();
            PingJiaLeiXing = ((int)info.PingJiaLeiXing).ToString();
        }

        /// <summary>
        /// bao cun
        /// </summary>
        void BaoCun()
        {
            if (string.IsNullOrEmpty(GysId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            var info = GetFormInfo();

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(GysId))
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BSiJi().Insert(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BSiJi().Update(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MSiJiInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MSiJiInfo();
            info.Address = Utils.GetFormValue(txtAddress.UniqueID);
            info.BeiZhu = Utils.GetFormValue(txtBeiZhu.UniqueID);
            info.CompanyId = SiteUserInfo.CompanyId;
            info.Gender = Utils.GetEnumValue<EyouSoft.Model.EnumType.GovStructure.Gender>(Utils.GetFormValue("txtGender"), EyouSoft.Model.EnumType.GovStructure.Gender.男);
            info.GysId = GysId;
            info.IssueTime = DateTime.Now;
            info.LeiXing = LeiXing;
            info.Mobile = Utils.GetFormValue(txtMobile.UniqueID);
            info.Name = Utils.GetFormValue(txtName.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.PingJiaLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.GysStructure.SiJiPingJiaLeiXing>(Utils.GetFormValue("txtPingJiaLeiXing"), EyouSoft.Model.EnumType.GysStructure.SiJiPingJiaLeiXing.好);
            info.PingJiaNeiRong = Utils.GetFormValue(txtPingJiaNeiRong.UniqueID);
            info.Telephone = Utils.GetFormValue(txtTelephone.UniqueID);

            return info;
        }
        #endregion

    }
}
