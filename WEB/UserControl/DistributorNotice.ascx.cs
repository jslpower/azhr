using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControl
{
    public partial class DistributorNotice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSource();
            }
        }

        /// <summary>
        /// 所属公司编号
        /// </summary>
        public string CompanyId
        {
            set;
            get;
        }
        /// <summary>
        /// 接收对象
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.ItemType ItemType
        {
            set;
            get;
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        public void BindSource()
        {
            int recordCount = 0;
            EyouSoft.BLL.GovStructure.BNotice bNotice = new EyouSoft.BLL.GovStructure.BNotice();
            IList<EyouSoft.Model.GovStructure.MGovNotice> list = bNotice.GetGovNoticeList(CompanyId, ItemType, 10, 0, ref recordCount);
            if (list != null)
            {
                if (list.Count != 0)
                {
                    this.Repeater1.DataSource = list;
                    this.Repeater1.DataBind();
                }
            }

        }
    }
}