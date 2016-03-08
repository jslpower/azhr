using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class SelfPay : System.Web.UI.UserControl
    {

        /// <summary>
        /// 设置控件的数据源
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MQuoteSelfPay> SetSelfPayList { get; set; }




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
            this.rpSelfPay.DataSource = this.SetSelfPayList;
            this.rpSelfPay.DataBind();
        }


        /// <summary>
        /// 获取控件的数据
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MQuoteSelfPay> GetDataList()
        {
            string[] CityIds = Utils.GetFormValues("hidselfcityid");
            string[] CityNames = Utils.GetFormValues("txtselfcity");
            string[] ScenicSpotIds = Utils.GetFormValues("hidselfscenicid");
            string[] ScenicSpotNames = Utils.GetFormValues("txtselfscenicname");
            string[] Prices = Utils.GetFormValues("txt_SelfPayPrice");//对外
            string[] Costs = Utils.GetFormValues("txt_SelfPayCost");//减少
            string[] Remarks = Utils.GetFormValues("txt_SelfPayRemark");
            string[] SettlementPrice = Utils.GetFormValues("hidselfpricejs");
            IList<EyouSoft.Model.HTourStructure.MQuoteSelfPay> GetSelfPayList = new List<EyouSoft.Model.HTourStructure.MQuoteSelfPay>();
            if (CityIds.Length > 0 && CityNames.Length > 0 && ScenicSpotIds.Length > 0 && ScenicSpotNames.Length > 0 && Prices.Length > 0)
            {
                GetSelfPayList = new List<EyouSoft.Model.HTourStructure.MQuoteSelfPay>();

                for (int i = 0; i < CityIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(CityIds[i]) && !string.IsNullOrEmpty(CityNames[i]) && !string.IsNullOrEmpty(ScenicSpotIds[i]) && !string.IsNullOrEmpty(ScenicSpotNames[i]))
                    {
                        EyouSoft.Model.HTourStructure.MQuoteSelfPay model = new EyouSoft.Model.HTourStructure.MQuoteSelfPay();
                        model.CityId = Utils.GetInt(CityIds[i]);
                        model.CityName = CityNames[i];
                        model.ScenicSpotId = ScenicSpotIds[i];
                        model.ScenicSpotName = ScenicSpotNames[i];
                        model.Price = Utils.GetDecimal(Prices[i]);
                        model.Cost = Utils.GetDecimal(Costs[i]);
                        model.Remark = Remarks[i];
                        model.SettlementPrice = Utils.GetDecimal(SettlementPrice[i]);
                        GetSelfPayList.Add(model);
                    }
                }
            }
            return GetSelfPayList;
        }
    }
}