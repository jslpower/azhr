using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.SmsCenter
{
    public partial class notify : System.Web.UI.Page
    {
        #region attributes
        string CompanyId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            var domain = EyouSoft.Security.Membership.UserProvider.GetDomain();
            if (domain == null) Utils.RCWE("异常请求:未知的域名信息");

            CompanyId = domain.CompanyId;

            var sxinfo = GetShangXingInfo();

            int retCode1 = new EyouSoft.BLL.SmsStructure.BSmsRenWu().InsertShangXing(sxinfo);

            if (retCode1 != 1) Utils.RCWE("异常:写入短信上行信息失败");

            var rwinfo = GetRenWuInfo(sxinfo);

            if (rwinfo.LeiXing == EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.行程变化)
            {
                int retCode2 = XCBHHandler(rwinfo);
                if (retCode2 != 1) Utils.RCWE("异常:行程变化信息处理失败");
                else
                {
                    int retCode3 = new EyouSoft.BLL.SmsStructure.BSmsRenWu().Insert(rwinfo);
                    if (retCode3 != 1) Utils.RCWE("异常:写入行程变化任务信息失败");
                    else Utils.RCWE("行程变化短信处理成功");
                }
            }
            else if (rwinfo.LeiXing == EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.进店报账)
            {
                int retCode4 = JDBZHandler(rwinfo);
                if (retCode4 != 1) Utils.RCWE("异常:进店报账信息处理失败");
                else
                {
                    int retCode5 = new EyouSoft.BLL.SmsStructure.BSmsRenWu().Insert(rwinfo);
                    if (retCode5 != 1) Utils.RCWE("异常:写入进店报账任务信息失败");
                    else Utils.RCWE("进店报账短信处理成功");
                }
            }
            else
            {
                int retCode6 = new EyouSoft.BLL.SmsStructure.BSmsRenWu().Insert(rwinfo);
                if (retCode6 != 1) Utils.RCWE("异常:写入短信任务信息失败");
            }
        }

        #region private members
        /// <summary>
        /// get shangxing info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.SmsStructure.MSmsShangXingInfo GetShangXingInfo()
        {
            var info = new EyouSoft.Model.SmsStructure.MSmsShangXingInfo();

            info.ApiSmsId = Utils.GetQueryStringValue("smsid");
            info.HaoMa = Utils.GetQueryStringValue("mobile");
            info.IssueTime = DateTime.Now;
            info.NeiRong = Utils.GetQueryStringValue("content");
            info.CompanyId = CompanyId;

            return info;
        }

        /// <summary>
        /// get renwu info
        /// </summary>
        /// <param name="sxinfo"></param>
        /// <returns></returns>
        EyouSoft.Model.SmsStructure.MSmsRenWuInfo GetRenWuInfo(EyouSoft.Model.SmsStructure.MSmsShangXingInfo sxinfo)
        {
            var info = new EyouSoft.Model.SmsStructure.MSmsRenWuInfo();
            info.CompanyId = CompanyId;
            info.HandlerStatus = EyouSoft.Model.EnumType.SmsStructure.RenWuHandlerStatus.未处理;
            info.IssueTime = DateTime.Now;
            info.JieShouStatus = EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus.未接收;
            info.ShangXingId = sxinfo.ShangXingId;
            info.LeiXing = GetRenWuLeiXing(sxinfo.NeiRong);
            info.NeiRong = sxinfo.NeiRong;

            return info;
        }

        /// <summary>
        /// get renwuleixing
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing GetRenWuLeiXing(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length < 4) return EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.None;

            var items = s.Split(',');
            if (items == null || items.Length == 0) return EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.None;

            if (items[0].ToUpper().Contains("JDBZ")) return EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.进店报账;
            if (items[0].ToUpper().Contains("XCBH")) return EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.行程变化;

            return EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.None;
        }

        /// <summary>
        /// 行程变化处理程序
        /// </summary>
        /// <param name="rwinfo">任务信息</param>
        /// <returns></returns>
        int XCBHHandler(EyouSoft.Model.SmsStructure.MSmsRenWuInfo rwinfo)
        {
            var info = ParseXCBH(rwinfo.NeiRong);

            if (info == null) return 0;

            int retCode = new EyouSoft.BLL.SmsStructure.BSmsRenWu().XCBH(info);

            if (retCode == 1)
            {
                rwinfo.FaQiRenId = info.FaQiRenId;
                rwinfo.JieShouRenId = info.JieShouRenId;
                rwinfo.HandlerStatus = EyouSoft.Model.EnumType.SmsStructure.RenWuHandlerStatus.处理成功;
            }

            return retCode;
        }

        /// <summary>
        /// 进店报账处理程序
        /// </summary>
        /// <param name="rwinfo">任务信息</param>
        /// <returns></returns>
        int JDBZHandler(EyouSoft.Model.SmsStructure.MSmsRenWuInfo rwinfo)
        {
            var info = ParseJDBZ(rwinfo.NeiRong);

            if (info == null) return 0;

            int retCode = new EyouSoft.BLL.SmsStructure.BSmsRenWu().JDBZ(info);

            if (retCode == 1)
            {
                rwinfo.FaQiRenId = info.FaQiRenId;
                rwinfo.HandlerStatus = EyouSoft.Model.EnumType.SmsStructure.RenWuHandlerStatus.处理成功;
            }

            return retCode;
        }

        /// <summary>
        /// 解析行程变化
        /// </summary>
        /// <param name="s">短信内容</param>
        /// <returns></returns>
        EyouSoft.Model.SmsStructure.MSmsXCBHInfo ParseXCBH(string s)
        {
            int f1 = s.IndexOf('(');

            if (f1 < 0) return null;

            string s1 = s.Substring(0, f1);
            string[] arr1 = s1.Split(',');

            if (arr1.Length < 3) return null;

            var info = new EyouSoft.Model.SmsStructure.MSmsXCBHInfo();
            info.BianHuaNeiRong = s.Substring(f1);
            info.CompanyId = CompanyId;
            info.DaoYouBH = arr1[2];
            info.TourCode = arr1[1];

            return info;
        }

        /// <summary>
        /// 解析进店报账
        /// </summary>
        /// <param name="s">短信内容</param>
        /// <returns></returns>
        EyouSoft.Model.SmsStructure.MSmsJDBZInfo ParseJDBZ(string s)
        {
            string[] arr1 = s.Split(',');
            if (arr1.Length < 8) return null;

            var info = new EyouSoft.Model.SmsStructure.MSmsJDBZInfo();

            info.CompanyId = CompanyId;
            info.DaoYouBH = arr1[2];
            info.GWCR = Utils.GetInt(arr1[4]);
            info.GWET = Utils.GetInt(arr1[5]);
            info.GysBH = arr1[3];
            info.AnPaiBH = arr1[3];
            info.LiuShui = Utils.GetDecimal(arr1[6]);
            info.TourCode = arr1[1];
            info.XXs = new List<EyouSoft.Model.SmsStructure.MSmsJDBZXXInfo>();

            int f1 = s.IndexOf('(');
            if (f1 < 0) return null;

            string s1 = s.Substring(f1);
            if (string.IsNullOrEmpty(s1)) return null;
            s1 = s1.TrimStart('(').TrimEnd(')');

            string[] arr2 = s1.Split(';');
            if (arr2 != null && arr2.Length > 0)
            {
                foreach (var s3 in arr2)
                {
                    if (string.IsNullOrEmpty(s3)) continue;
                    string[] arr3 = s3.Split(',');
                    if (arr3 == null || arr3.Length < 2) continue;

                    var xx = new EyouSoft.Model.SmsStructure.MSmsJDBZXXInfo();
                    xx.CPBH = arr3[0];
                    xx.CPSL = Utils.GetInt(arr3[1]);

                    info.XXs.Add(xx);
                }
            }

            return info;
        }
        #endregion
    }
}
