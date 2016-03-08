using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Text;

namespace Web.Sys
{
    /// <summary>
    /// 系统设置-通知公告-列表
    /// </summary>
    /// 修改人：刘树超
    /// 修改时间：2013-6-5
    public partial class TongZhiGongGao : BackPage
    {
        #region 页面参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;
        #endregion

        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType == "delete")
            {
                AJAX(doType);
            }
            #endregion

            //初始化
            DataInit();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            string title = Utils.GetQueryStringValue("txtTitle");
            //用户控件的取值和赋值
            string operatorId = Utils.GetQueryStringValue(this.SellsSelect1.SellsIDClient);
            string operatorname = Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);
            this.SellsSelect1.SellsID = operatorId;
            this.SellsSelect1.SellsName = operatorname;
            EyouSoft.BLL.GovStructure.BNotice BLL = new EyouSoft.BLL.GovStructure.BNotice();
            IList<EyouSoft.Model.GovStructure.MGovNotice> lst = BLL.GetGovNoticeList(this.SiteUserInfo.CompanyId, title, operatorId, operatorname, this.pageSize, this.pageIndex, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                if (recordCount <= pageSize)
                {
                    this.ExporPageInfoSelect1.Visible = false;

                }
                else
                {
                    BindPage();
                }
            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='6' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;

            }
        }
        #endregion

        #region 绑定分页
        /// <summary> 
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        #endregion

        #region ajax操作
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string id = Utils.GetQueryStringValue("id");
            bool result = DeleteData(id);
            string msg = result == true ? "删除成功！" : "删除失败！";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
        }
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private bool DeleteData(string id)
        {
            bool b = false;
            if (!String.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BNotice BLL = new EyouSoft.BLL.GovStructure.BNotice();
                b = BLL.DeleteGovNotice(id.Split(','));
            }
            return b;
        }
        #endregion

        #region 获取附件链接
        /// <summary>
        /// 获取附件链接，默认取集合里的第一个附件
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetAttach(object obj)
        {
            if (obj == null) return string.Empty;

            var items = obj as IList<EyouSoft.Model.ComStructure.MComAttach>;

            if (items != null && items.Count > 0)
            {
                return string.Format("<a target=\"_blank\" title=\"附件\" href=\"/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}\" ><img src=\"/Images/fujian.gif\" /></a>", items[0].FilePath, HttpUtility.UrlEncode(items[0].Name));
            }

            return string.Empty;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_栏目, false);
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_新增))
            {
                ph_Add.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_修改))
            {
                ph_Update.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_删除))
            {
                ph_Del.Visible = false;
            }

        }
        #endregion
    }
}