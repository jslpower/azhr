using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Web.UserControl
{
    public partial class HeadDistributorControl : System.Web.UI.UserControl
    {
        private string _ProcductClass = "Producticon";

        public string ProcductClass
        {
            get { return _ProcductClass; }
            set { _ProcductClass = value; }
        }


        private string _OrderClass = "orderformicon";

        public string OrderClass
        {
            get { return _OrderClass; }
            set { _OrderClass = value; }
        }


        public string _FinanceClass = "cawuglicon";

        public string FinanceClass
        {
            get { return _FinanceClass; }
            set { _FinanceClass = value; }
        }


        public string _SystemClass = "xtglicon";

        public string SystemClass
        {
            get { return _SystemClass; }
            set { _SystemClass = value; }
        }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CompanyId))
            {
                var domain= EyouSoft.Security.Membership.UserProvider.GetDomain();
                if (domain != null)
                {
                    CompanyId = domain.CompanyId;                    
                }                
            }

            if (string.IsNullOrEmpty(CompanyId))
            {
                Response.Clear();
                Response.Write("异常请求，系统域名配置错误。");
                Response.End();
            }


            if (!IsPostBack)
            {
                this.Notice1.CompanyId = CompanyId;
                this.Notice1.ItemType = EyouSoft.Model.EnumType.GovStructure.ItemType.分销商;
            }            

            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);

            if (setting != null&&!string.IsNullOrEmpty(setting.FXSLogo))
            {
                this.ltrLogo.Text = "<img src=\"" + setting.FXSLogo + "\" style=\"width:371px;height:64px;\" />";
            }
        }
    }
}