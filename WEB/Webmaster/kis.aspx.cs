//汪奇志 2012-05-08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.Webmaster
{
    /// <summary>
    /// 金蝶默认科目配置
    /// </summary>
    public partial class _kis : WebmasterPageBase
    {
        #region attributes
        /// <summary>
        /// 系统编号
        /// </summary>
        string SysId = string.Empty;
        /// <summary>
        /// 公司编号
        /// </summary>
        string CompanyId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SysId = Utils.GetQueryStringValue("sysid");
            CompanyId = Utils.GetQueryStringValue("cid");

            if (!IsPostBack)
            {
                InitSysInfo();
                InitAccountGroups();
                InitKisConfig();
            }
        }

        #region private members
        /// <summary>
        /// init sys info
        /// </summary>
        void InitSysInfo()
        {
            var sysInfo = new EyouSoft.BLL.SysStructure.BSys().GetSysInfo(SysId);

            if (sysInfo == null)
            {
                RegisterAlertAndRedirectScript("请求异常。", "systems.aspx");
            }

            ltrSysName.Text = sysInfo.SysName;
        }

        /// <summary>
        /// 初始化金蝶科目
        /// </summary>
        void InitAccountGroups()
        {
            var items = new EyouSoft.BLL.FinStructure.BKis().GetAccountGroups(CompanyId);
            string s = "var kisAccountGroups={0};";

            if (items != null && items.Count > 0)
            {
                s = string.Format(s, Newtonsoft.Json.JsonConvert.SerializeObject(items));
            }
            else
            {
                s = string.Format(s, "[]");
            }

            RegisterScript(s);
        }

        /// <summary>
        /// 初始化金蝶默认科目配置
        /// </summary>
        void InitKisConfig()
        {
            var info = new EyouSoft.BLL.ComStructure.BComSetting().GetKisConfigInfo(CompanyId);

            string s = "var kisConfig={0};";

            if (info != null)
            {
                s = string.Format(s, Newtonsoft.Json.JsonConvert.SerializeObject(info));
            }
            else
            {
                s = string.Format(s, "{}");
            }

            RegisterScript(s);
        }
        #endregion

        #region protected members
        /// <summary>
        /// btnSubmit click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //get form value
            var info = new EyouSoft.Model.ComStructure.MKisConfigInfo();
            info.Kis1000 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_贷));
            info.Kis1999 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_借));
            info.Kis2000 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_贷));
            info.Kis2999 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_借));
            info.Kis3000 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_贷));
            info.Kis3999 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_借));
            info.Kis4000 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预付账款_借));
            info.Kis4500 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预收账款_贷));
            info.Kis4501 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_团队借款_贷));
            info.Kis4999 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_现金_贷));
            info.Kis5000 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务收入_贷));
            info.Kis5001 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队借款_贷));
            info.Kis5002 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队预支_贷));
            info.Kis5003 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队支出_贷));
            info.Kis5004 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应付账款_贷));
            info.Kis5005 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_贷));
            info.Kis5006 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务成本_借));
            info.Kis5007 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_预收账款_借));
            info.Kis5008 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_借));
            info.Kis5009 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_银行存款_借));
            info.Kis5999 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应收帐款_借));
            //info.Kis6000 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_主营业务收入_贷));
            //info.Kis6001 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_团队预支_贷));
            //info.Kis6002 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_应付账款_贷));
            //info.Kis6003 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_团队支出_贷));
            //info.Kis6500 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_预收账款_借));
            //info.Kis6501 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_应收账款_借));
            //info.Kis6999 = Utils.GetFormValue(string.Format("txt{0}", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_主营业务成本_借));

            if (new EyouSoft.BLL.ComStructure.BComSetting().SetKisConfigInfo(CompanyId, info))
            {
                this.RegisterAlertAndReloadScript("设置成功");
            }
            else
            {
                this.RegisterAlertAndReloadScript("设置失败");
            }
        }
        #endregion

    }
}
