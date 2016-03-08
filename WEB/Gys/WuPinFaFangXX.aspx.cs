using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class WuPinFaFangXX : EyouSoft.Common.Page.BackPage
    {
        string WuPinId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            WuPinId = Utils.GetQueryStringValue("wupinid");
            if (string.IsNullOrEmpty(WuPinId)) RCWE("异常请求");

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var items = new EyouSoft.BLL.HGysStructure.BWuPin().GetLingYongs(WuPinId, EyouSoft.Model.EnumType.GysStructure.WuPinLingYongLeiXing.发放);

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
        #endregion
    }
}
