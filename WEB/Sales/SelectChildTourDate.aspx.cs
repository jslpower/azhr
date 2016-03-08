using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.Sales
{
    public partial class SelectChildTourDate : System.Web.UI.Page
    {
        protected string CurrentDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        protected string NextDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today.AddMonths(1));
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
