using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class JingDianJiaGe : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected string GysId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GysId = Utils.GetQueryStringValue("gysid");

            if (string.IsNullOrEmpty(GysId)) RCWE("异常请求");

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var info = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(GysId);
            if (info == null) RCWE("异常请求");

            if (info.JingDians != null && info.JingDians.Count > 0)
            {
                rpt.DataSource = info.JingDians;
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
