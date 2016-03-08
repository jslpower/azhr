using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.ComStructure;
using System.Text;

namespace EyouSoft.WebFX.CommonPage
{
    public partial class OrderSells : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int recordCount = 0;

        protected int listCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                string userName = Utils.GetQueryStringValue("userName");
                //初始化
                GetDepart();
                DataInit(userName);
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string userName)
        {

            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            string deptId = Utils.GetQueryStringValue("deptId");
            EyouSoft.Model.ComStructure.MComUserSearch searchModel = new EyouSoft.Model.ComStructure.MComUserSearch();
            searchModel.ContactName = userName;
            searchModel.UserStatus = EyouSoft.Model.EnumType.ComStructure.UserStatus.正常;
            if (deptId != "")
            {
                searchModel.DeptId = Utils.GetInt(deptId);
            }
            IList<EyouSoft.Model.ComStructure.MComUser> userList = new EyouSoft.BLL.ComStructure.BComUser().GetList(pageIndex, pageSize, ref recordCount, SiteUserInfo.CompanyId, searchModel);

            if (userList != null && userList.Count > 0)
            {
                listCount = userList.Count;
                this.rptList.DataSource = userList;
                this.rptList.DataBind();
                BindPage();
            }
            else
            {
                this.lblMsg.Text = "没有相关数据!";
                this.ExporPageInfoSelect1.Visible = false;
            }

            //绑定分页
            BindPage();
        }


        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {

            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }

        /// <summary>  获得所有的公司
        /// 获得所有的公司顶级ID
        /// </summary>
        protected void GetDepart()
        {
            EyouSoft.BLL.ComStructure.BComDepartment BLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            IList<MComDepartment> lst = BLL.GetList(this.SiteUserInfo.CompanyId);
            if (null != lst && lst.Count > 0)
            {
                this.rep_depart.DataSource = lst.Where(i => (i.PrevDepartId == 0)).ToList();
                this.rep_depart.DataBind();
            }
            else
            {
                this.rep_depart.Controls.Add(new Label() { Text = "<li style='text-align:center;'>对不起，没有相关数据！</li>" });
            }
        }


        /// <summary>
        /// 获得所有部门
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetAllDepart(object o)
        {
            StringBuilder sb = new StringBuilder();
            EyouSoft.BLL.ComStructure.BComDepartment BLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            IList<MComDepartment> lst = BLL.GetList(this.SiteUserInfo.CompanyId);
            if (lst != null && lst.Count > 0)
            {
                GetSecondDepart(lst, (int)o, sb);
            }
            return sb.ToString();
        }

        /// <summary>  获得公司下面的部门
        /// 获得对应的公司下面的部门
        /// </summary>
        /// <param name="o">对应的公司ID</param>
        /// <returns></returns>
        protected void GetSecondDepart(IList<MComDepartment> lst, int deptId,StringBuilder sb)
        {
            var childList = lst.Where(p => p.PrevDepartId == deptId).ToList();
            if (childList != null && childList.Count > 0)
            {
                for (int i = 0; i < childList.Count; i++)
                {
                 
                    //输出公司下面的部门，绑定查询公司对应的人员方法
                    sb.Append("<a href='javascript:void(0);' onclick=OrderSellsPage.SearchFun(''," + childList[i].DepartId + ")>" + childList[i].DepartName + "</a> | ");
                    GetSecondDepart(lst, childList[i].DepartId, sb);
                }
            }
        }

        #endregion

    }
}
