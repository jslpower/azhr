using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.HTourStructure;

namespace EyouSoft.WebFX.UserControl
{
    public partial class Give : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置控件的数据源
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MQuoteGive> SetQuoteGiveList { get; set; }


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
            this.rpGive.DataSource = this.SetQuoteGiveList;
            this.rpGive.DataBind();
        }


        /// <summary>
        /// 获取控件的数据
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MQuoteGive> GetDataList()
        {
            IList<EyouSoft.Model.HTourStructure.MQuoteGive> GetQuoteGiveList = new List<MQuoteGive>();
            string[] ItemIds = Utils.GetFormValues("hidWuPinId");
            string[] Items = Utils.GetFormValues("txtWuPin");
            string[] Prices = Utils.GetFormValues("txt_WuPinPrice");
            string[] Remarks = Utils.GetFormValues("txt_WuPinRemark");

            if (ItemIds.Length > 0 && Items.Length > 0 && Prices.Length > 0)
            {
                GetQuoteGiveList = new List<EyouSoft.Model.HTourStructure.MQuoteGive>();

                for (int i = 0; i < ItemIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Items[i]) && !string.IsNullOrEmpty(Items[i]))
                    {
                        EyouSoft.Model.HTourStructure.MQuoteGive model = new EyouSoft.Model.HTourStructure.MQuoteGive();
                        model.ItemId = ItemIds[i];
                        model.Item = Items[i];
                        model.Price = Utils.GetDecimal(Prices[i]);
                        model.Remark = Remarks[i];

                        GetQuoteGiveList.Add(model);
                    }
                }
            }
            return GetQuoteGiveList;
        }
    }
}