using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Common;

namespace EyouSoft.Web.CommonPage
{
    public partial class Supplier : BackPage
    {
        /// <summary>
        /// 供应商枚举类型
        /// </summary>
        protected EyouSoft.Model.EnumType.GysStructure.GysLeiXing type;

        /// <summary>
        /// 供应商枚举
        /// </summary>
        protected List<EnumObj> EnumSource = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //省份
            string Province = Utils.GetQueryStringValue("ddlProvice");
            //城市
            string City = Utils.GetQueryStringValue("ddlCity");
            //县区
            string Area = Utils.GetQueryStringValue("ddlArea");
            //名称
            string Name = Utils.GetQueryStringValue("txtName");
            //供应商类型
            type = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)Utils.GetInt(Utils.GetQueryStringValue("Sourcetype"));
            EnumSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GysStructure.GysLeiXing)).Where(m => m.Text != EyouSoft.Model.EnumType.GysStructure.GysLeiXing.司机.ToString() && m.Text != EyouSoft.Model.EnumType.GysStructure.GysLeiXing.领队.ToString() && m.Text != EyouSoft.Model.EnumType.GysStructure.GysLeiXing.物品.ToString()).ToList();

        }
    }
}
