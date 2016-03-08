using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.WebFX.CommonPage
{
    public partial class UserSupplier : EyouSoft.Common.Page.FrontPage
    {
        /// <summary>
        /// 计调枚举
        /// </summary>
        protected EyouSoft.Model.EnumType.GysStructure.GysLeiXing type;
        protected void Page_Load(object sender, EventArgs e)
        {
            //国家
            string dplcountry = Utils.GetQueryStringValue("ddlCountry");
            //省份
            string dplProvince = Utils.GetQueryStringValue("ddlProvice");
            //城市
            string dplCity = Utils.GetQueryStringValue("ddlCity");
            //县区
            string dplArea = Utils.GetQueryStringValue("ddlArea");
            //名称
            string Name = Utils.GetQueryStringValue("txtName");
            type = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)Enum.Parse(typeof(EyouSoft.Model.EnumType.GysStructure.GysLeiXing), (Utils.GetQueryStringValue("suppliertype")));
        }
    }
}
