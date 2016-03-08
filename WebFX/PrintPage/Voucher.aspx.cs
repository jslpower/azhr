using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EyouSoft.WebFX.PrintPage
{
    using EyouSoft.Common;
    
    public partial class Voucher : EyouSoft.Common.Page.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var orderid = Utils.GetQueryStringValue("OrderId");
            var m = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderExpandByOrderId(orderid);
            if (m != null)
            {
                this.litCustomer.Text = m.DCompanyName;
                this.litIssueTime.Text = m.IssueTime.HasValue ? m.IssueTime.Value.ToShortDateString() : string.Empty;
                this.litContact.Text = m.DContactTel;
                this.litOrderCode.Text = m.OrderCode;
                var mt = new EyouSoft.BLL.HTourStructure.BTour().GetTourInfoModel(m.TourId);
                if (mt != null)
                {
                    this.litRouteName.Text = mt.RouteName;
                    this.litLeaveDate.Text = mt.LDate.ToShortDateString();
                    this.litArrive.Text = mt.ArriveCityFlight + mt.ArriveCity;
                }
                this.litAdult.Text = m.Adults.ToString();
                this.litChild.Text = m.Childs.ToString();
                this.litInfant.Text = m.Others.ToString();
                this.litAgent.Text = m.BuyCompanyName;
            }
        }
    }
}
