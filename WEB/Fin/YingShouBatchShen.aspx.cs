using System;
using System.Collections;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.BLL.FinStructure;

    public partial class YingShouBatchShen : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("doType") == "Save")
            {
                Save();
            }

            InitPrivs();

            string s = Utils.GetQueryStringValue("OrderId");
            if (string.IsNullOrEmpty(s)) AjaxResponse("错误的请求");

            string[] orderIds = s.Split(',');
            if (orderIds != null && orderIds.Length > 0)
            {
                IList<MTourOrderSales> items = new BFinance().GetBatchTourOrderSalesCheck(orderIds);
                if (items != null && items.Count > 0)
                {
                    rpt_list.DataSource = items;
                    rpt_list.DataBind();

                    decimal heJiJinE = 0;
                    foreach (var item in items)
                    {
                        heJiJinE += item.CollectionRefundAmount;
                    }
                    ltrHeJiJinE.Text = UtilsCommons.GetMoneyString(heJiJinE, ProviderToMoney);

                    phEmpty.Visible = false;
                    phHeJiJinE.Visible = true;
                }
                else
                {
                    phHeJiJinE.Visible = false;
                    phEmpty.Visible = true;
                    phShengHe.Visible = false;
                }
            }

        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            phShengHe.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_收款审核);
        }

        /// <summary>
        /// 保存审核
        /// </summary>
        void Save()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_收款审核))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "操作失败：没有权限。"));
            }

            IList<MTourOrderSales> ls = new List<MTourOrderSales>();
            string[] datas = Utils.GetFormValue("list").Split(',');
            if (datas.Length > 0)
            {
                foreach (string item in datas)
                {
                    string[] subdatas = item.Split('|');
                    if (subdatas.Length == 2)
                    {
                        ls.Add(new MTourOrderSales
                        {
                            Approver = SiteUserInfo.Name,
                            ApproverDeptId = SiteUserInfo.DeptId,
                            ApproverId = SiteUserInfo.UserId,
                            ApproveTime = DateTime.Now,
                            Id = subdatas[1],
                            OrderId = subdatas[0]
                        });
                    }
                }
            }
            IList<string> retMsg = new BFinance().SetTourOrderSalesCheck(ls);
            if (retMsg == null || retMsg.Count <= 0)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
            }
            else
            {
                string strMsg = string.Empty;
                foreach (string item in retMsg)
                {
                    strMsg += item + "<br/>";
                }
                strMsg += "审核失败!";
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", strMsg));
            }

        }
        #endregion
    }
}
