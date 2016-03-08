using System;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common;
    using System.Text;

    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PrivsStructure;
    
    public partial class DangRiShouKuan : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 页记录数
        /// </summary>
        int pageSize = 20;
        /// <summary>
        /// 页索引
        /// </summary>
        int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //判断权限
                PowerControl();
                //初始化
                DataInit();
            }
            if (UtilsCommons.IsToXls()) ToXls();
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            pageIndex = UtilsCommons.GetPadingIndex();
            decimal xmlSum = 0;
            var searchInfo = GetSearchInfo();
            var items = new EyouSoft.BLL.FinStructure.BFinance().GetDayReceivablesChkLst(pageSize, pageIndex, ref recordCount, ref xmlSum, null, false, this.SiteUserInfo.CompanyId, searchInfo);
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }
            BindPage();
        }

        /// <summary>
        /// 枚举集合
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.EnumType.TourStructure.TourType> GetTourTypes()
        {
            IList<EyouSoft.Model.EnumType.TourStructure.TourType> lis = new List<EyouSoft.Model.EnumType.TourStructure.TourType>();
            List<EnumObj> ls = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourType));
            IList<EyouSoft.Model.EnumType.TourStructure.TourType> tourTypes = new List<EyouSoft.Model.EnumType.TourStructure.TourType>();
            for (int i = 0; i < ls.Count; i++)
            {
                tourTypes.Add((EyouSoft.Model.EnumType.TourStructure.TourType)i);
            }
            return tourTypes;
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            paging.UrlParams = Request.QueryString;
            paging.intPageSize = pageSize;
            paging.CurrencyPage = pageIndex;
            paging.intRecordCount = recordCount;

            phEmpty.Visible = paging.intRecordCount == 0 ? true : false;
            paging.Visible = paging.intRecordCount > pageSize ? true : false;
        }

        /// <summary>
        /// 导出
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            decimal xmlSum = 0;
            StringBuilder s = new StringBuilder();

            var searchInfo = GetSearchInfo();
            var items = new EyouSoft.BLL.FinStructure.BFinance().GetDayReceivablesChkLst(toXlsRecordCount, 1, ref _recordCount, ref xmlSum, null, false, this.SiteUserInfo.CompanyId, searchInfo);

            if (items != null && items.Count > 0)
            {
                s.Append("<table border=1>");
                s.Append("<tr><th align='center'>团号</th>");
                s.Append("<th align='center'>线路名称</th>");
                s.Append("<th align='center'>订单号</th>");
                s.Append("<th align='center'>客户单位/游客姓名</th>");
                s.Append("<th align='center'>人数</th>");
                s.Append("<th align='center'>销售员</th>");
                s.Append("<th align='center'>收款金额</th>");
                s.Append("<th align='center'>状态</th></tr>");
                foreach (var item in items)
                {
                    s.AppendFormat("<tr><td align='center'>{0}</td>", item.TourCode);
                    s.AppendFormat("<td align='center'>{0}</td>", item.RouteName);
                    s.AppendFormat("<td align='center'>{0}</td>", item.OrderCode);
                    s.AppendFormat("<td align='center'>{0}</td>", item.Customer);
                    s.AppendFormat("<td align='center'><b>{0}</b><sup>+{1}</sup></td>", item.Adults, item.Childs);
                    s.AppendFormat("<td align='center'>{0}</td>", item.Salesman);
                    s.AppendFormat("<td align='right'>{0}</td>", UtilsCommons.GetMoneyString(item.ReceivableAmount, ProviderToMoney));
                    s.AppendFormat("<td align='center'>{0}</td>", item.Status);
                    s.Append("</tr>");
                }
            }

            ResponseToXls(s.ToString());
        }

        /// <summary>
        /// 获取查询参数
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.FinStructure.MDayReceivablesChkBase GetSearchInfo()
        {
            var info = new EyouSoft.Model.FinStructure.MDayReceivablesChkBase();
            info.Customer = Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHMC);
            info.CustomerId = Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHBH);
            info.Salesman = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient);
            info.SalesmanId = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient);
            return info;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        void PowerControl()
        {
            if (((Menu2)Utils.GetInt(Utils.GetQueryStringValue("sl"))) == Menu2.财务管理_应收管理)
            {
                if (!CheckGrant(Privs.财务管理_应收管理_查看当日收款))
                {
                    Utils.ResponseNoPermit(Privs.财务管理_应收管理_查看当日收款, true);
                    return;
                }
            }
        }
        #endregion
    }
}
