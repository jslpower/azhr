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
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.Web.Sta
{
    public partial class RuJing : EyouSoft.Common.Page.BackPage
    {
        protected int pageSize = 20;
        protected int pageIndex = UtilsCommons.GetPadingIndex();
        protected int recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            if (UtilsCommons.IsToXls()) ToXls();
            InitDept();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.统计分析_入境目录表_栏目))
            {
                RCWE("没有权限");
            }
        }

        /// <summary>
        /// init dept
        /// </summary>
        void InitDept()
        {
            var items = new EyouSoft.BLL.ComStructure.BComDepartment().GetList(SiteUserInfo.CompanyId);

            StringBuilder s = new StringBuilder();

            s.AppendFormat("<option value='{0}'>{1}</option>", string.Empty, "请选择");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("<option value='{0}'>{1}</option>", item.DepartId, item.DepartName);
                }
            }

            ltrDept.Text = s.ToString();
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.TongJiStructure.BRuJing().GetRuJings(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

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
        EyouSoft.Model.TongJiStructure.MRuJingChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.TongJiStructure.MRuJingChaXunInfo();

            info.ETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtETime"));            
            info.STime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSTime"));
            info.DeptId = Utils.GetIntNull(Utils.GetQueryStringValue("txtDeptId"));

            return info;
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
            var items = new EyouSoft.BLL.TongJiStructure.BRuJing().GetRuJings(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            s.Append("日期\t入境国籍\t团队名称\t人数\t行程安排\t游览城市\t全陪\t页码号\t装订序号\n");
            var i = 0;
            foreach (var item in items)
            {
                s.AppendFormat(item.LDate.ToString("yyyy-MM-dd") + "\t");
                s.AppendFormat(item.GuoJi + "\t");
                s.AppendFormat(item.RouteName + "\t");
                s.AppendFormat(item.RJRS + "\t");
                s.AppendFormat(item.XingChengAnPai + "\t");
                s.AppendFormat(item.YouLanChengShi + "\t");
                s.AppendFormat(item.QuanPeiName + "\t");
                //s.AppendFormat(item.YeMaHao + "\t");
                s.AppendFormat(" "+(i/this.pageSize+1)+"-"+item.ZhuangDingXuHao + "\t");
                s.AppendFormat(" " + item.ZhuangDingXuHao + "\n");
                i += 1;
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
