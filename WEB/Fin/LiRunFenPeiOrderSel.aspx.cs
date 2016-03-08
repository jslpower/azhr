using System;

namespace EyouSoft.Web.Fin
{
    using EyouSoft.BLL.TourStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;

    public partial class LiRunFenPeiOrderSel : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ls = new BTour().GetTourOrderDisList(Utils.GetQueryStringValue("tourId"));
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
            }
        }
    }
}
