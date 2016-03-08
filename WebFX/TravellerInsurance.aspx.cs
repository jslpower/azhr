using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.WebFX
{
    public partial class TravellerInsurance : EyouSoft.Common.Page.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = EyouSoft.Common.Utils.GetQueryStringValue("Type");
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Do"))
                {
                    Response.Clear();
                    Response.Write(GetTravellerInsurance());
                    Response.End();
                }
            }

            if (!IsPostBack)
            {
                this.TravellerId.Value = EyouSoft.Common.Utils.GetQueryStringValue("TravellerId");
                PageInit();


            }
        }

        /// <summary>
        /// 绑定页面
        /// </summary>
        private void PageInit()
        {
            EyouSoft.BLL.ComStructure.BComInsurance bInsurance = new EyouSoft.BLL.ComStructure.BComInsurance();
            IList<EyouSoft.Model.ComStructure.MComInsurance> list = bInsurance.GetList(SiteUserInfo.CompanyId);
            this.RpInsurance.DataSource = list;
            this.RpInsurance.DataBind();


        }


        /// <summary>
        /// 获取游客信息的Json数据
        /// </summary>
        /// <returns></returns>
        private string GetTravellerInsurance()
        {
            string json = string.Empty;
            string travellerId = EyouSoft.Common.Utils.GetQueryStringValue("TravellerId");
            if (!string.IsNullOrEmpty(travellerId))
            {
                EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
                IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> list = bOrder.GetTravellerInsuranceListByTravellerId(travellerId);
                if (list != null && list.Count != 0)
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(list);

                    
                }
            }
            return json;
        }
    }
}
