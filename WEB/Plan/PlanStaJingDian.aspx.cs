using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Plan
{
    public partial class PlanStaJingDian : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (UtilsCommons.IsToXls()) ToXls();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调查询统计_栏目))
            {
                RCWE("没有权限");
            }
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
            var items = new EyouSoft.BLL.HPlanStructure.BPlan().GetPlanTJ(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);
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
        EyouSoft.Model.HPlanStructure.MPlanTJChaXunInfo GetChaXunInfo()
        {
            //供应商id
            string sourceId = Utils.GetQueryStringValue(this.supplierControl1.ClientValue);
            //供应商称
            string sourceName = Utils.GetQueryStringValue(this.supplierControl1.ClientText);

            #region 查询赋值
            if (!string.IsNullOrEmpty(sourceId) && !string.IsNullOrEmpty(sourceName))
            {
                this.supplierControl1.HideID = sourceId;
                this.supplierControl1.Name = sourceName;
            }
            #endregion

            var info = new EyouSoft.Model.HPlanStructure.MPlanTJChaXunInfo();
            info.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点;
            info.Attractions = Utils.GetQueryStringValue("txtJingDian");
            info.SourceId = sourceId;
            info.SourceName = sourceName;
            info.TourCode = Utils.GetQueryStringValue("txtTourCode");
            info.StartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
            info.EndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
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
            var items = new EyouSoft.BLL.HPlanStructure.BPlan().GetPlanTJ(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            s.Append("城市\t景点公司名称\t团号\t安排明细\t导游\t备注\t状态\n");
            foreach (var m in items)
            {
                s.Append(m.CityName+"\t");
                s.Append(m.SourceName+"\t");
                s.Append(m.TourCode+"\t");
                s.Append(this.GetAPMX(m.PlanAttractionsList).Replace("\t", "    ").Replace("\r\n", "    ").Replace("<br/>", "    ") + "\t");
                s.Append(this.GetGuidInfo(m.GuidList,"0")+"\t");
                s.Append(m.GuideNotes.Replace("\t", "    ").Replace("\r\n", "    ").Replace("<br/>", "    ") + "\t");
                s.Append(m.Status+"\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion

        #region protected members
        /// <summary>
        /// 安排明细
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetAPMX(object list)
        {
            StringBuilder str = new StringBuilder();
            if (list != null)
            {
                var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanAttractions>)list;
                for (int i = 0; i < lis.Count; i++)
                {
                    str.AppendFormat("{0}/{1}/成人{2}人/儿童{3}人<br/>", lis[i].VisitTime.Value.ToString("yyyy-MM-dd"), lis[i].Attractions,lis[i].AdultNumber,lis[i].ChildNumber);
                }
            }
            return str.ToString();
        }
        protected string GetGuidInfo(object guid, string type)
        {
            System.Text.StringBuilder bs = new System.Text.StringBuilder();
            IList<EyouSoft.Model.HPlanStructure.MGuidInfo> guidList = (List<EyouSoft.Model.HPlanStructure.MGuidInfo>)guid;
            if (guidList != null && guidList.Count > 0)
            {
                if (type == "0")
                {
                    bs.Append("" + guidList[0].Name + "");
                }
                else
                {
                    bs.Append("姓名：" + guidList[0].Name + "<br />电话：" + guidList[0].Tel + "<br />手机：" + guidList[0].Mobile + "");
                    for (int i = 1; i < guidList.Count; i++)
                    {
                        bs.Append("<br />--------------<br />姓名：" + guidList[i].Name + "<br />电话：" + guidList[i].Tel + "<br />手机：" + guidList[i].Mobile + "");
                    }
                }
            }
            return bs.ToString();
        }
        #endregion
    }
}
