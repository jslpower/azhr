using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.SmsCenter
{
    public partial class RenWu : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_DX_LanMu = false;
        bool Privs_RW_LanMu = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "JieShou") JieShouRenWu();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_DX_LanMu = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目);
            Privs_RW_LanMu = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目);

            if (!Privs_DX_LanMu) RCWE("无权限");
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            int pageSize = 20;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;

            var items = new EyouSoft.BLL.SmsStructure.BSmsRenWu().GetRenWus(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
            else
            {
                phEmpty.Visible = true;
                paging.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.SmsStructure.MSmsRenWuChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.SmsStructure.MSmsRenWuChaXunInfo();
            info.FaQiRenName = Utils.GetQueryStringValue("txtFaQiRenName");
            info.JieShouRenName = Utils.GetQueryStringValue("txtJieShouRenName");
            info.JieShouStatus = (EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus), Utils.GetQueryStringValue("txtJieShouStatus"));
            info.LeiXing = (EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing), Utils.GetQueryStringValue("txtRenWuLeiXing"));
            
            return info;
        }

        /// <summary>
        /// jieshou renwu
        /// </summary>
        void JieShouRenWu()
        {
            var info = new EyouSoft.Model.SmsStructure.MSmsRenWuJieShouInfo();
            info.CompanyId = SiteUserInfo.CompanyId;
            info.JieShouRenId = SiteUserInfo.UserId;
            info.JieShouStatus = EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus.已接收;
            info.JieShouTime = DateTime.Now;
            info.RenWuId = Utils.GetQueryStringValue("renwuid");

            int bllRetCode = new EyouSoft.BLL.SmsStructure.BSmsRenWu().JieShouRenWu(info);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion

        #region protected members
        #endregion
    }
}
