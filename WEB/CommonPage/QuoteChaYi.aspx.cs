using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.CommonPage
{
    public partial class QuoteChaYi : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ParentId = Utils.GetQueryStringValue("ParentId");
            if (string.IsNullOrEmpty(ParentId))
            {
                RCWE("异常请求");
            }
            else
            {
                PageInit(ParentId);
            }
        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="QuoteId"></param>
        private void PageInit(string ParentId)
        {
            string[] ids = ParentId.Split(',');
            if (ids.Length == 1)
            {
                EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
                IList<EyouSoft.Model.HTourStructure.MQuoteCompare> list = bll.GetQuoteCompareList(ParentId);
                if (list != null && list.Count != 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        EyouSoft.Model.HTourStructure.MQuoteCompare model = list[i];
                        ListItem item = new ListItem();
                        item.Text = string.Format("第{0}次报价", model.TimeCount);
                        item.Value = model.QuoteId;

                        this.ddlFrist.Items.Add(item);
                        this.ddlSecond.Items.Add(item);
                    }
                }
            }
            else
            {
                EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
                IList<EyouSoft.Model.HTourStructure.MQuoteCompare> listfrist = bll.GetQuoteCompareList(ids[0]);
                if (listfrist != null && listfrist.Count != 0)
                {
                    for (int i = 0; i < listfrist.Count; i++)
                    {
                        EyouSoft.Model.HTourStructure.MQuoteCompare modelfrist = listfrist[i];
                        ListItem item = new ListItem();
                        item.Text = string.Format("第{0}次报价", modelfrist.TimeCount);
                        item.Value = modelfrist.QuoteId;

                        this.ddlFrist.Items.Add(item);
                    }
                }


                IList<EyouSoft.Model.HTourStructure.MQuoteCompare> listsecond = bll.GetQuoteCompareList(ids[1]);
                if (listsecond != null && listsecond.Count != 0)
                {
                    for (int i = 0; i < listsecond.Count; i++)
                    {
                        EyouSoft.Model.HTourStructure.MQuoteCompare modelsecond = listsecond[i];
                        ListItem item = new ListItem();
                        item.Text = string.Format("第{0}次报价", modelsecond.TimeCount);
                        item.Value = modelsecond.QuoteId;

                        this.ddlSecond.Items.Add(item);
                    }
                }

            }




        }
    }
}
