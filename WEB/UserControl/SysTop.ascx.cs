using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControl
{
    public partial class SysTop : System.Web.UI.UserControl
    {
        protected string str = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            str = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
            switch (str)
            {
                case "/smscenter/sendmessage.aspx":
                    str = "sendmessage";
                    break;
                case "/smscenter/sendhistory.aspx":
                    str = "sendhistory";
                    break;
                case "/smscenter/smslist.aspx":
                    str = "smslist";
                    break;
                case "/smscenter/smssetting.aspx":
                    str = "smssetting";
                    break;
                case "/smscenter/accountinfo.aspx":
                    str = "accountinfo";
                    break;
                case "/smscenter/renwu.aspx": str = "renwu"; break;
            }
        }
    }
}