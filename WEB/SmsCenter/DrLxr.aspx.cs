using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.SmsCenter
{
    public partial class DrLxr : EyouSoft.Common.Page.BackPage
    {
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
            int pageSize = 20;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;

            
            var items = new EyouSoft.BLL.SmsStructure.BDaoRuLxr().GetLxrs(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun);
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
            else
            {
                phEmpty.Visible = true;
                paging.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.SmsStructure.MLBDaoRuLxrSearchInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.SmsStructure.MLBDaoRuLxrSearchInfo();
            
            info.CountryId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCountryId"));
            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("txtProvinceId"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("txtCityId"));

            info.DanWeiType = (EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType), Utils.GetQueryStringValue("txtLeiXing"));

            return info;
        }
        #endregion
    }
}
