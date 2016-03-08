using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.CommonPage
{
    public partial class DuoXuanGouWuDian : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {            
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            int pageIndex = 1;
            int pageSize = 10000;

            var items = new EyouSoft.BLL.HGysStructure.BGys().GetXuanYongs(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo();

            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCityId"));
            info.CountryId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCountryId"));
            info.GysName = Utils.GetQueryStringValue("txtName");
            info.LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.购物;
            info.ProvinceId= Utils.GetIntNull(Utils.GetQueryStringValue("txtProvinceId"));

            return info;
        }
        #endregion
    }
}
