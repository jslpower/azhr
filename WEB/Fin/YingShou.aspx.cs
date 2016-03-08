namespace EyouSoft.Web.Fin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;

    public partial class YingShou : BackPage
    {
        #region attributes
        /// <summary>
        /// 单项业务游客确认单路径
        /// </summary>
        protected string PrintPage_DanXiangYeWuYouKeQueRenDan = string.Empty;
        /// <summary>
        /// 二级栏目枚举
        /// </summary>
        protected Menu2 _sl;
        /// <summary>
        /// 单元格合并数
        /// </summary>
        protected int _span;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            PrintPage_DanXiangYeWuYouKeQueRenDan = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.单项业务游客确认单);

            if (UtilsCommons.IsToXls())
            {
                ToXls();
            }
            //初始化
            DataInit();

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            #region 查询实体
            MReceivableBase queryModel = GetChaXunInfo();
            #endregion
            //金额汇总信息
            var sum = new object[6];
            IList<MReceivableInfo> ls = new BFinance().GetReceivableInfoLst(
                pageSize,
                pageIndex,
                ref  recordCount,
                ref sum,
                queryModel);

            if (sum!=null&&sum.Length>=5)
            {
                lbl_totalReceived.Text = UtilsCommons.GetMoneyString(sum[1], ProviderToMoney);
                //lbl_totalReturned.Text = UtilsCommons.GetMoneyString(sum[4], ProviderToMoney);
                lbl_totalSumPrice.Text = UtilsCommons.GetMoneyString(sum[0], ProviderToMoney);
                lbl_totalUnchecked.Text = UtilsCommons.GetMoneyString(sum[2], ProviderToMoney);
                lbl_totalUnReceived.Text = UtilsCommons.GetMoneyString(sum[3], ProviderToMoney);
                //lbl_totalUnChkReturn.Text = UtilsCommons.GetMoneyString(sum[5], ProviderToMoney);
            }

            if (ls != null && ls.Count > 0)
            {
                pan_sum.Visible = true;
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            this._sl = (Menu2)Utils.GetInt(this.SL);
            switch (_sl)
            {
                case Menu2.销售中心_销售收款:
                    if (!CheckGrant(Privs.销售中心_销售收款_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.销售中心_销售收款_栏目, true);
                        return;
                    }
                    else
                    {
                        pan_plshoukuan.Visible = CheckGrant(Privs.销售中心_销售收款_收款登记);

                        pan_plshenhe.Visible = false;

                        //pan_plkaipiao.Visible = CheckGrant(Privs.财务管理_应收管理_开票登记);

                        pan_DRSK.Visible = false;
                    }
                    this._span = 2;
                    break;
                case Menu2.财务管理_应收管理:
                    if (!CheckGrant(Privs.财务管理_应收管理_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.财务管理_应收管理_栏目, true);
                        return;
                    }
                    else
                    {
                        pan_plshoukuan.Visible = CheckGrant(Privs.财务管理_应收管理_收款登记);

                        pan_plshenhe.Visible = CheckGrant(Privs.财务管理_应收管理_收款审核);

                        //pan_plkaipiao.Visible = CheckGrant(Privs.财务管理_应收管理_开票登记);

                        pan_DRSK.Visible = CheckGrant(Privs.财务管理_应收管理_查看当日收款);
                    }
                    this._span = 1;
                    break;
                default:
                    Utils.ResponseGoBack();
                    break;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void ToXls()
        {
            int recordCount = 0;
            //金额汇总信息
            var sum = new object[6];
            MReceivableBase queryModel = GetChaXunInfo();
            IList<MReceivableInfo> ls = new BFinance().GetReceivableInfoLst(
                UtilsCommons.GetToXlsRecordCount(),
                1,
                ref  recordCount,
                ref sum,
                queryModel);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Date\tFILE NO.\tINVOICE No.\tDetail\t$\t$\t$\t$\n");

                foreach (MReceivableInfo item in ls)
                {
                    sb.Append(item.IssueTime.ToShortDateString() + "\t");
                    sb.Append(item.OrderCode + "\t");
                    sb.Append(string.Empty+ "\t");
                    sb.Append(item.Customer + "\t");
                    sb.Append(item.Receivable.ToString("F2") + "\t");
                    var l = new BLL.TourStructure.BTourOrder().GetTourOrderSalesListByOrderId(item.OrderId, CollectionRefundState.收款);
                    if (l!=null&&l.Count>0)
                    {
                        var isFirst = true;
                        foreach (var m in l)
                        {
                            if (!isFirst)
                            {
                                sb.Append(string.Empty + "\t");
                                sb.Append(string.Empty + "\t");
                                sb.Append(string.Empty + "\t");
                                sb.Append(string.Empty + "\t");
                                sb.Append(string.Empty + "\t");
                            }
                            sb.Append("PAID "+m.CollectionRefundDate.Value.ToString("dd-MM-yy") + "\t");
                            sb.Append(string.Empty + "\t");
                            sb.Append(m.CollectionRefundModeName + "\t");
                            sb.Append(m.CollectionRefundAmount.ToString("F2") + "\n");
                            isFirst = false;
                        }
                    }
                    else
                    {
                        sb.Append(string.Empty + "\t");
                        sb.Append(string.Empty + "\t");
                        sb.Append(string.Empty + "\t");
                        sb.Append(string.Empty + "\n");
                    }
                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);

        }

        /// <summary>
        /// 获取查询信息
        /// </summary>
        /// <returns></returns>
        MReceivableBase GetChaXunInfo()
        {
            MReceivableBase info = new MReceivableBase();
            info.CompanyId = CurrentUserCompanyID;
            info.OrderCode = Utils.GetQueryStringValue("orderId");//订单号
            info.Customer = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);//客户单位
            info.CustomerId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            info.IsClean = Utils.GetQueryStringValue("isReceived") == "1";//是否已结清
            info.Salesman = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);//销售员
            info.SalesmanId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);//销售员
            info.LDateStart = Utils.GetQueryStringValue("SDate");
            info.LDateEnd = Utils.GetQueryStringValue("EDate");
           
            //已收待审金额
            info.SignUnChecked = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator));
            info.UnChecked = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber));
            //未收金额
            info.SignUnReceived = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));
            info.UnReceived = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));

            info.OperatorId = txtXiaDanRen.SellsID = Utils.GetQueryStringValue(txtXiaDanRen.SellsIDClient);
            info.OperatorName = txtXiaDanRen.SellsName = Utils.GetQueryStringValue(txtXiaDanRen.SellsNameClient);

            return info;
        }
        #endregion
    }
}
