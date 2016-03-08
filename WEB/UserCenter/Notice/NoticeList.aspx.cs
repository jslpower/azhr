using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.GovStructure;
using EyouSoft.BLL.GovStructure;
using System.Text;

namespace Web.UserCenter.Notice
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-28
    /// 说明：个人中心：公告通知 列表
    public partial class NoticeList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            if (!string.IsNullOrEmpty(doType))
                AJAX(doType);
            #endregion
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();

                //初始化
                DataInit();
            }

        }
        #region  分页绑定列表
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            EyouSoft.BLL.IndStructure.BIndividual bllBNotice = new EyouSoft.BLL.IndStructure.BIndividual();
            IList<EyouSoft.Model.IndStructure.MNoticeRemind> listMGovNotice = bllBNotice.GetNoticeRemindLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.UserId, SiteUserInfo.DeptId, SiteUserInfo.CompanyId);
            if (listMGovNotice != null && listMGovNotice.Count > 0)
            {
                this.rptList.DataSource = listMGovNotice;
                this.rptList.DataBind();
                //绑定分页
                BindPage();
            }
        }
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

            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }

        #region ajax操作
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            //ajax操作后返回的结果
            string msg = "";
            string noticeids = Utils.GetQueryStringValue("idList");
            switch (doType)
            {
                case "delete":
                    msg = DeleteData("");
                    break;
                case "IsRemind":
                    msg = SetIsRemind("");
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }

        /// <summary>
        /// 设置是否提醒
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        private string SetIsRemind(string id)
        {
            //返回结果
            string msg = string.Empty;
            return msg;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        private string DeleteData(string id)
        {
            BNotice bllBNotice = new BNotice();
            //返回结果
            string msg = string.Empty;
            string[] noticeids = { "" };
            //判断是不是多条数据删除
            if (id.Contains(","))
                noticeids = id.Split(',');
            else
                noticeids[0] = id;
            //批量删除数据
            if (bllBNotice.DeleteGovNotice(noticeids))
                msg = "true";
            else
                msg = "false";
            return msg;
        }

        #endregion

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }
        #endregion

        #region 前台调用方法
        #region 获取附件链接
        /// <summary>
        /// 获取附件链接  默认取集合里的第一个附件
        /// </summary>
        /// <param name="id">公告编号</param>
        /// <returns></returns>
        protected string GetUrl(string id)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BNotice bll = new EyouSoft.BLL.GovStructure.BNotice();
                EyouSoft.Model.ComStructure.MComAttach attachModel = new EyouSoft.Model.ComStructure.MComAttach();
                EyouSoft.Model.GovStructure.MGovNotice noticeModel = new EyouSoft.Model.GovStructure.MGovNotice();
                noticeModel = bll.GetGovNoticeModel(id);
                if (noticeModel != null)
                {
                    if (noticeModel.ComAttachList != null && noticeModel.ComAttachList.Count > 0)
                    {
                        attachModel = noticeModel.ComAttachList.First();
                        sb.AppendFormat("<a target=\"_blank\" title=\"附件\" href=\"{0}\" ><img src=\"/Images/fujian.gif\" /></a>", attachModel.FilePath);
                        return sb.ToString();
                    }
                }
            }
            return string.Empty;
        }
        #endregion
        #endregion
    }
}
