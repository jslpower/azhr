using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class TuanXiaoFei : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置控件的数据源
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MTourTip> SetXiaoFei { get; set; }



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
            this.rpQuote.DataSource = this.SetXiaoFei;
            this.rpQuote.DataBind();
        }


        /// <summary>
        /// 获取控件的数据
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MTourTip> GetDataList()
        {

            string[] Tips = Utils.GetFormValues("txt_Quote_Tip");
            string[] Prices = Utils.GetFormValues("txt_Quote_Price");
            string[] Days = Utils.GetFormValues("txt_Quote_Days");
            string[] SumPrices = Utils.GetFormValues("txt_Quote_SumPrice");
            string[] Remarks = Utils.GetFormValues("txt_Quote_Remark");
            IList<EyouSoft.Model.HTourStructure.MTourTip> XiaoFei = new List<EyouSoft.Model.HTourStructure.MTourTip>();
            if (Tips.Length > 0 && Prices.Length > 0 && Days.Length > 0)
            {
                XiaoFei = new List<EyouSoft.Model.HTourStructure.MTourTip>();

                for (int i = 0; i < Tips.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Tips[i]) && Utils.GetDecimal(Prices[i]) != 0)
                    {
                        EyouSoft.Model.HTourStructure.MTourTip model = new EyouSoft.Model.HTourStructure.MTourTip();
                        model.Tip = Tips[i];
                        model.Price = Utils.GetDecimal(Prices[i]);
                        model.Days = Utils.GetInt(Days[i]);
                        model.SumPrice = Utils.GetDecimal(SumPrices[i]);
                        model.Remark = Remarks[i];

                        XiaoFei.Add(model);
                    }
                }
            }
            return XiaoFei;
        }
    }
}