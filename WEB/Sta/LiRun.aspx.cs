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
    public partial class LiRun : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (UtilsCommons.IsToXls()) ToXls();

            InitArea();
            InitDept();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.统计分析_利润统计_栏目))
            {
                RCWE("没有权限");
            }
        }

        /// <summary>
        /// init area
        /// </summary>
        void InitArea()
        {
            var items = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(SiteUserInfo.CompanyId);
            StringBuilder s = new StringBuilder();

            s.AppendFormat("<option value='{0}'>{1}</option>", string.Empty, "请选择");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.LngType != EyouSoft.Model.EnumType.SysStructure.LngType.中文) continue;
                    s.AppendFormat("<option value='{0}'>{1}</option>", item.AreaId, item.AreaName);
                }
            }

            ltrArea.Text = s.ToString();
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
            int pageSize = 20;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;
            EyouSoft.Model.TongJiStructure.MLiRunHeJiInfo heJi;
            var items = new EyouSoft.BLL.TongJiStructure.BLiRun().GetLiRuns(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun, out heJi);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;

                ltrYSHJ.Text = ToMoneyString(heJi.YingShouJinE);
                ltrYFHJ.Text = ToMoneyString(heJi.YingFuJinE);
                ltrMLHJ.Text = ToMoneyString(heJi.MaoLi);
                ltrGWFLHJ.Text = ToMoneyString(heJi.GWFanLi);
            }
            else
            {
                phEmpty.Visible = true;
                paging.Visible = false;
                phHeJi.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.TongJiStructure.MLiRunChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.TongJiStructure.MLiRunChaXunInfo();

            info.ETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtETime"));
            info.KeHuId = Utils.GetQueryStringValue(txtKeHu.ClientNameKHBH);
            info.KeHuName = Utils.GetQueryStringValue(txtKeHu.ClientNameKHMC);
            info.MaoLi1 = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue("txtMaoLi1"));
            info.MaoLi2 = Utils.GetDecimalNull(Utils.GetQueryStringValue("txtMaoLi2"));
            info.STime=Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSTime"));
            info.XiaoShouYuanDeptId = Utils.GetIntNull(Utils.GetQueryStringValue("txtDeptId"));
            info.XiaoShouYuanId = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient);
            info.XiaoShouYuanName = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient);
            info.AreaId = Utils.GetIntNull(Utils.GetQueryStringValue("txtAreaId"));

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
            EyouSoft.Model.TongJiStructure.MLiRunHeJiInfo heJi;
            var items = new EyouSoft.BLL.TongJiStructure.BLiRun().GetLiRuns(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun, out heJi);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            s.Append("团号\t线路区域\t线路名称\t入境成人\t入境儿童\t入境领队\t签单成人\t签单儿童\t客户单位\t业务员\t应收款\t应付款\t毛利\t人均毛利\tGST\n");
            foreach (var item in items)
            {
                s.AppendFormat(item.TourCode + "\t");
                s.AppendFormat(item.AreaName + "\t");
                s.AppendFormat(item.RouteName + "\t");
                s.AppendFormat(item.RJCR + "\t");
                s.AppendFormat(item.RJET + "\t");
                s.AppendFormat(item.RJLD + "\t");
                s.AppendFormat(item.GWCR + "\t");
                s.AppendFormat(item.GWET + "\t");
                s.AppendFormat(item.KeHuName + "\t");
                s.AppendFormat(item.XiaoShouYuanName + "\t");
                s.AppendFormat(item.YingShouJinE.ToString("F2") + "\t");
                s.AppendFormat(item.YingFuJinE.ToString("F2") + "\t");
                s.AppendFormat(item.MaoLi.ToString("F2") + "\t");
                s.AppendFormat(item.RenJunMaoLi.ToString("F2") + "\t");
                s.AppendFormat((item.IsTax?"√":"×") + "\n");
            }

            //合计
            if (heJi!=null)
            {
                s.AppendFormat("\t\t\t\t\t\t\t\t\t合计：\t{0}\t{1}\t{2}\t\t{3}\n", heJi.YingShouJinE.ToString("F2"), heJi.YingFuJinE.ToString("F2"), heJi.MaoLi.ToString("F2"), "");
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
