using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Sys
{
    public partial class YuYanGuanLi : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            List<EnumObj> list = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SysStructure.LngType));
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    str.AppendFormat("<tr><td width=\"30\" align=\"center\">{0}</td><td align=\"center\">{1}</td></tr>", i + 1, list[i].Text);
                }
            }

            lit_list.Text = str.ToString();

        }
    }
}
