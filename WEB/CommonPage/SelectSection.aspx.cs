using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.ComStructure;
using EyouSoft.Common.Function;
using System.Text;
namespace Web.CommonPage
{
    /// <summary>
    /// 部门选取
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-15
    public partial class SelectSection : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                this.DataInit();
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            EyouSoft.BLL.ComStructure.BComDepartment BLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            IList<MComDepartment> lst = BLL.GetList(this.SiteUserInfo.CompanyId);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst.Where(i => (i.PrevDepartId == 0)).ToList();
                this.RepList.DataBind();
            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<li style='text-align:center;'>对不起，没有相关数据！</li>" });
            }
        }

        protected string GetDepart(object o, int flag)
        {
            int deptId = (int)o;
            EyouSoft.BLL.ComStructure.BComDepartment BLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            IList<MComDepartment> lst = BLL.GetList(deptId.ToString(), this.SiteUserInfo.CompanyId);
            StringBuilder sb = new StringBuilder();
            if (lst != null && lst.Count > 0)
            {
                sb.AppendFormat("<ul class=\"ulborder\" style=\"width: 100%;float: left\">");
                for (int i = 0; i < lst.Count; i++)
                {
                    
                    sb.AppendFormat("<li style=\"width: 23%;display:block;float:left; text-align: left; line-height: 20px;\">");
                    sb.AppendFormat("<input id=\"select_{1}{2}\" type=\"checkbox\" value=\"{0}\" data-pid='{3}' name=\"Operator3\" /><input id=\"select_{1}{2}\" type=\"radio\" value=\"{0}\" name=\"Operator3\" data-pid='{3}' />", lst[i].DepartId, i, flag, lst[i].PrevDepartId);
                    sb.AppendFormat("<label for=\"select_{1}{2}\">{0}</label>", lst[i].DepartName, i, flag);
                    sb.AppendFormat("</li>");
                }
                sb.AppendFormat("</ul>");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }
        #endregion
    }
}