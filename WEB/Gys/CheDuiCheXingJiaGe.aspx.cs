using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class CheDuiCheXingJiaGe : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected string GysId = string.Empty;
        protected string CheXingId = string.Empty;

        bool Privs_Insert = false;
        bool Privs_Update = false;
        bool Privs_Delete = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GysId = Utils.GetQueryStringValue("gysid");
            CheXingId = Utils.GetQueryStringValue("chexingid");

            if (string.IsNullOrEmpty(GysId) || string.IsNullOrEmpty(CheXingId)) RCWE("异常请求");

            InitPrivs();

            switch (Utils.GetQueryStringValue("dotype"))
            {
                case "submit": BaoCun(); break;
                case "delete": Delete(); break;
                default: break;
            }

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_车队_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_车队_新增);
            Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_车队_新增);
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var items = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCheXingJiaGes(CheXingId);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        void Delete()
        {
            if (!Privs_Delete) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));

            string s = Utils.GetFormValue("txtJiaGeId");
            int bllRetCode = new EyouSoft.BLL.HGysStructure.BJiaGe().DeleteCheXingJiaGe(SiteUserInfo.CompanyId, GysId, CheXingId, s);
            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// bao cun
        /// </summary>
        void BaoCun()
        {
            var info = GetFormInfo();
            if (string.IsNullOrEmpty(info.JiaGeId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(info.JiaGeId))
                bllRetCode = new EyouSoft.BLL.HGysStructure.BJiaGe().InsertCheXingJiaGe(info);
            else
                bllRetCode = new EyouSoft.BLL.HGysStructure.BJiaGe().UpdateCheXingJiaGe(info);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo();

            info.BinKeLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.CrmStructure.CustomType>(Utils.GetFormValue("txtBinKeLeiXing"), EyouSoft.Model.EnumType.CrmStructure.CustomType.内宾);
            info.CheXingId = CheXingId;
            info.ETime = Utils.GetDateTimeNullable(Utils.GetFormValue("txtETime"));
            info.IssueTime = DateTime.Now;
            info.JiaGeId = Utils.GetFormValue("txtJiaGeId");
            info.JiaGeJS = Utils.GetDecimal(Utils.GetFormValue("txtJSJ"));
            info.OperatorId = SiteUserInfo.UserId;
            info.STime = Utils.GetDateTimeNullable(Utils.GetFormValue("txtSTime"));

            return info;
        }
        #endregion
    }
}

