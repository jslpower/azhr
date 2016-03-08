namespace EyouSoft.WebFX
{
    using System;

    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.BLL.GovStructure;
    using EyouSoft.Model.GovStructure;

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 修改人：蔡永辉
    /// 创建时间：2012-4-5
    /// 说明：个人中心：公告通知 查看
    public partial class NoticeInfo : FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //权限验证
            PowerControl();

            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //根据ID初始化页面
                PageInit(id);
            }

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                BNotice bllBNotice = new BNotice();
                MGovNotice modelMGovNotice = bllBNotice.GetGovNoticeModel(id);
                if (modelMGovNotice != null)
                {
                    //浏览人数加1
                    int views = 0;
                    Int32.TryParse(modelMGovNotice.Views.ToString(), out views);
                    modelMGovNotice.Views = views + 1;
                    bllBNotice.UpdateGovNotice(modelMGovNotice);
                    //插入浏览人相关信息
                    MGovNoticeBrowse viewModel = new MGovNoticeBrowse();
                    viewModel.IssueTime = DateTime.Now;
                    viewModel.NoticeId = id;
                    viewModel.OperatorId = this.SiteUserInfo.UserId;
                    viewModel.Operator = this.SiteUserInfo.Name;
                    bllBNotice.AddGovNoticeBrowse(viewModel);

                    //公告标题
                    this.lblTitle.Text = modelMGovNotice.Title;
                    //公告内容
                    this.lblContent.Text = modelMGovNotice.Content;
                    //公告发布人
                    this.lblAddUser.Text = modelMGovNotice.Operator;
                    //公告发布时间
                    this.lblAddDate.Text = modelMGovNotice.IssueTime.ToLongDateString();
                }
                else
                {
                    Response.Clear();
                    Response.Write("没有该数据");
                    Response.End();
                }
            }
            else
            {
                Response.Clear();
                Response.Write("缺少参数");
                Response.End();
            }

        }





        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {

        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

    }
}
