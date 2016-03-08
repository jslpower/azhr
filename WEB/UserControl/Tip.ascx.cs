using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class Tip : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置控件的数据源
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MQuoteTip> SetQuoteTipList { get; set; }



        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            SetDataList();
            base.OnPreRender(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack)
            {
                base.OnLoad(e);
            }
        }

        /// <summary>
        /// 页面初始化时绑定数据
        /// </summary>
        private void SetDataList()
        {
            this.rpQuote.DataSource = this.SetQuoteTipList;
            this.rpQuote.DataBind();
        }


        /// <summary>
        /// 获取控件的数据
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MQuoteTip> GetDataList()
        {

            string[] Tips = Utils.GetFormValues("txt_Quote_Tip");
            string[] Prices = Utils.GetFormValues("txt_Quote_Price");
            string[] Days = Utils.GetFormValues("txt_Quote_Days");
            string[] SumPrices = Utils.GetFormValues("txt_Quote_SumPrice");
            string[] Remarks = Utils.GetFormValues("txt_Quote_Remark");
            IList<EyouSoft.Model.HTourStructure.MQuoteTip> GetQuoteTipList = new List<EyouSoft.Model.HTourStructure.MQuoteTip>();
            if (Tips.Length > 0 && Prices.Length > 0 && Days.Length > 0)
            {
                GetQuoteTipList = new List<EyouSoft.Model.HTourStructure.MQuoteTip>();

                for (int i = 0; i < Tips.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Tips[i]) && Utils.GetDecimal(Prices[i]) != 0)
                    {
                        EyouSoft.Model.HTourStructure.MQuoteTip model = new EyouSoft.Model.HTourStructure.MQuoteTip();
                        model.Tip = Tips[i];
                        model.Price = Utils.GetDecimal(Prices[i]);
                        model.Days = Utils.GetInt(Days[i]);
                        model.SumPrice = Utils.GetDecimal(SumPrices[i]);
                        model.Remark = Remarks[i];
                        
                        GetQuoteTipList.Add(model);
                    }
                }
            }
            return GetQuoteTipList;
        }
    }
}