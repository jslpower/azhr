using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class JiuDianJiaGeEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected string IsHanZao = "0";
        protected string FangXingId = "";
        protected string BinKeLeiXing = "";
        protected string XingQi = "";
        string GysId = string.Empty;
        string JiaGeId = string.Empty;

        bool Privs_Insert = false;
        bool Privs_Update = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GysId = Utils.GetQueryStringValue("gysid");
            JiaGeId = Utils.GetQueryStringValue("jiageid");

            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "submit") BaoCun();

            InitInfo();

            InitFangXing();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_新增);
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(JiaGeId)) return;
            var info = new EyouSoft.BLL.HGysStructure.BJiaGe().GetJiuDianJiaGeInfo(JiaGeId);
            if (info == null) RCWE("异常请求");

            if (info.IsHanZao) IsHanZao = "1";
            BinKeLeiXing = ((int)info.BinKeLeiXing).ToString();
            FangXingId = info.FangXingId;
            txtJGZC.Value = info.JiaGeZC.ToString("F2");
            txtJGMS.Value = info.JiaGeMS.ToString("F2");
            if (info.STime.HasValue) txtSTime.Value = info.STime.Value.ToString("yyyy-MM-dd");
            if (info.ETime.HasValue) txtETime.Value = info.ETime.Value.ToString("yyyy-MM-dd");
            txtJGTJS.Value = info.JiaGeTJS.ToString("F2");
            txtJGTFW.Value = info.JiaGeTFW.ToString("F2");
            txtTMianYiM.Value = info.TMianM.ToString();
            txtTMianYiN.Value = info.TMianN.ToString();
            txtJGSJS.Value = info.JiaGeSJS.ToString("F2");
            txtJGSFW.Value = info.JiaGeSFW.ToString("F2");
            txtBZ.Value = info.BeiZhu;

            if (info.XingQi != null && info.XingQi.Count > 0)
            {
                foreach (var item in info.XingQi)
                {
                    XingQi += (int)item + ",";
                }
            }

            if (!string.IsNullOrEmpty(XingQi)) XingQi = XingQi.TrimEnd(',');
        }

        /// <summary>
        /// init fangxing
        /// </summary>
        void InitFangXing()
        {
            var items = new EyouSoft.BLL.SysStructure.BSysRoom().GetRooms(SiteUserInfo.CompanyId);
            StringBuilder s = new StringBuilder();

            s.Append("<option value=''>请选择</option>");

            if (items == null || items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("<option value='{0}'>{1}</option>", item.RoomId, item.TypeName);
                }
            }

            ltrFangXing.Text = s.ToString();
        }

        /// <summary>
        /// bao cun
        /// </summary>
        void BaoCun()
        {
            if (string.IsNullOrEmpty(JiaGeId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            var info = GetFormInfo();
            int bllRetCode = 0;

            if (string.IsNullOrEmpty(JiaGeId))
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BJiaGe().InsertJiuDianJiaGe(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BJiaGe().UpdateJiuDianJiaGe(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo();
            info.BeiZhu = Utils.GetFormValue(txtBZ.UniqueID);
            info.BinKeLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.CrmStructure.CustomType>(Utils.GetFormValue("txtBinKeLeiXing"), EyouSoft.Model.EnumType.CrmStructure.CustomType.内宾);
            info.ETime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtETime.UniqueID));
            info.FangXingId = Utils.GetFormValue("txtFangXing");
            info.GysId = GysId;
            info.IsHanZao = Utils.GetFormValue("txtIsHanZao") == "1";
            info.IssueTime = DateTime.Now;
            info.JiaGeId = JiaGeId;
            info.JiaGeMS = Utils.GetDecimal(Utils.GetFormValue(txtJGMS.UniqueID));
            info.JiaGeSFW = Utils.GetDecimal(Utils.GetFormValue(txtJGSFW.UniqueID));
            info.JiaGeSJS= Utils.GetDecimal(Utils.GetFormValue(txtJGSJS.UniqueID));
            info.JiaGeTFW = Utils.GetDecimal(Utils.GetFormValue(txtJGTFW.UniqueID));
            info.JiaGeTJS = Utils.GetDecimal(Utils.GetFormValue(txtJGTJS.UniqueID));
            info.JiaGeZC = Utils.GetDecimal(Utils.GetFormValue(txtJGZC.UniqueID));
            info.OperatorId = SiteUserInfo.UserId;
            info.STime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtSTime.UniqueID));
            info.TMianM = Utils.GetInt(Utils.GetFormValue(txtTMianYiM.UniqueID));
            info.TMianN = Utils.GetDecimal(Utils.GetFormValue(txtTMianYiN.UniqueID));
            info.XingQi = new List<DayOfWeek>();

            string xingQi = Utils.GetFormValue("txtXQ");
            if (!string.IsNullOrEmpty(xingQi))
            {
                string[] items = xingQi.Split(',');
                if (items != null && items.Length > 0)
                {
                    foreach (var item in items)
                    {
                        var xq = (DayOfWeek?)Utils.GetEnumValueNull(typeof(DayOfWeek), item);
                        if (xq.HasValue) info.XingQi.Add(xq.Value);
                    }
                }
            }

            return info;
        }
        #endregion
    }
}
