using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Guide
{
    public partial class DaoYou : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        bool Privs_Delete = false;
        bool Privs_LanMu = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (UtilsCommons.IsToXls()) ToXls();
            if (Utils.GetQueryStringValue("dotype") == "Delete") Delete();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_LanMu = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_栏目);
            Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_删除);
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_修改);

            if (!Privs_LanMu) RCWE("无权限");
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

            var items = new EyouSoft.BLL.ComStructure.BDaoYou().GetDaoYous(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.ComStructure.MDaoYouChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.ComStructure.MDaoYouChaXunInfo();

            info.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), Utils.GetQueryStringValue("txtGender"));
            info.JiBie = (EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie), Utils.GetQueryStringValue("txtJiBie"));
            info.LeiBie = (EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie), Utils.GetQueryStringValue("txtLeiBie"));
            info.XingMing = Utils.GetQueryStringValue("txtXingMing");
            info.YuZhong = Utils.GetQueryStringValue("txtYuZhong");

            return info;
        }

        /// <summary>
        /// delete
        /// </summary>
        void Delete()
        {
            string s = Utils.GetQueryStringValue("deleteids");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));

            string[] items = s.Split(',');
            if (items == null || items.Length == 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));


            if (!Privs_Delete) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "没有删除权限！"));

            int f1 = 0;
            int f2 = 0;

            var bll = new EyouSoft.BLL.ComStructure.BDaoYou();

            foreach (var item in items)
            {
                if (bll.Delete(SiteUserInfo.CompanyId, item) == 1)
                {
                    f1++;
                }
                else
                {
                    f2++;
                }
            }

            bll = null;

            if (f1 > 0 && f2 == 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
            if (f1 > 0 && f2 > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", f1 + "个信息删除成功，" + f2 + "个信息不允许删除。"));
            if (f1 == 0 && f2 > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", f2 + "个信息不允许删除。"));

            Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败"));
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.ComStructure.BDaoYou().GetDaoYous(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            s.AppendFormat("姓名\t性别\t类别\t级别\t语种\t手机\t带团次数\t带团天数\t挂靠单位\t擅长线路\t年审状态\n");

            foreach (var item in items)
            {
                s.Append(item.XingMing.Replace("\t", "    ").Replace("\r\n", "    ") + "\t");
                s.Append(item.Gender + "\t");
                s.Append(item.LeiBie + "\t");
                s.Append(item.JiBie + "\t");
                s.Append(item.YuZhong.Replace("\t", "    ").Replace("\r\n", "    ") + "\t");
                s.Append(item.ShouJiHao.Replace("\t", "    ").Replace("\r\n", "    ") + "\t");
                s.Append(item.DaiTuanCiShu + "\t");
                s.Append(item.DaiTuanTianShu + "\t");
                s.Append(item.GuaKaoDanWei.Replace("\t", "    ").Replace("\r\n", "    ") + "\t");
                s.Append(item.ShanChangXianLu.Replace("\t", "    ").Replace("\r\n", "    ") + "\t");
                s.Append((item.IsNianShen ? "已审" : "未审") + "\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
