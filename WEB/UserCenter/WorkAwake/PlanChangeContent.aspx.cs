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
using EyouSoft.Common.Page;

namespace Web.UserCenter.WorkAwake
{
    public partial class PlanChangeContent : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //团队编号
            string TourId = string.Empty;
            if (!IsPostBack)
            {
                GetTourDataInfo(TourId);
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="tourID"></param>
        private void GetTourDataInfo(string tourID)
        {

        }
    }
}
