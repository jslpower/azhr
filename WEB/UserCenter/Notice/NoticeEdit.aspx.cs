using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;
using EyouSoft.Model.EnumType.GovStructure;

namespace Web.UserCenter.Notice
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-28
    /// 说明：个人中心：公告通知 修改，新增
    public partial class NoticeEdit : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //保存列表页面传过来的id
                HidNoticeID.Value = id;
                //判断此次请求是不是ajax==true为ajax请求

                //根据ID初始化页面
                PageInit(id);
            }
            else
                PageSave();

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            this.SelectSection1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
            //通知公告实体类
            MGovNotice modelMGovNotice = new MGovNotice();
            //通知公告业务
            BNotice bllBNotice = new BNotice();
            modelMGovNotice = bllBNotice.GetGovNoticeModel(id);
            if (modelMGovNotice != null)
            {
                this.txtTitle.Text = modelMGovNotice.Title;
                this.txtContent.Text = modelMGovNotice.Content;
                this.txtAddUser.Text = modelMGovNotice.Operator;
                this.txtAddDate.Text = modelMGovNotice.IssueTime.ToShortDateString();
            }
            else
                this.txtAddUser.Text = SiteUserInfo.Name;
        }

        #region 保存数据
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            //通知公告实体类
            MGovNotice modelMGovNotice = new MGovNotice();
            //通知公告业务
            BNotice bllBNotice = new BNotice();
            //操作类型（添加  修改）
            string doType = "Add";
            //获取实体
            if (!string.IsNullOrEmpty(HidNoticeID.Value))
            {
                modelMGovNotice = bllBNotice.GetGovNoticeModel(HidNoticeID.Value);
                doType = "Update";
            }

            #region 通知公告实体属性赋值

            #region 通知公告实体显性属性

            //标题
            string title = Utils.GetFormValue(txtTitle.UniqueID);
            //内容
            string content = Utils.GetFormValue(txtContent.UniqueID);
            //发布人
            string addUser = Utils.GetFormValue(txtAddUser.UniqueID);
            //发布时间
            string addDate = Utils.GetFormValue(txtAddDate.UniqueID);

            modelMGovNotice.Title = title;
            modelMGovNotice.Content = content;
            modelMGovNotice.Operator = addUser;
            modelMGovNotice.IssueTime = Utils.GetDateTime(addDate);
            //附件实体
            modelMGovNotice.ComAttachList = null;

            #region 接收人员实体
            MGovNoticeReceiver modelMGovNoticeReceiver = null;
            List<MGovNoticeReceiver> MGovNoticeReceiverlist = new List<MGovNoticeReceiver>();

            #region 公司内部（选择所有部门）
            if (cbxAll.Checked)
            {
                modelMGovNoticeReceiver = new MGovNoticeReceiver();
                modelMGovNoticeReceiver.ItemType = ItemType.公司内部;
                modelMGovNoticeReceiver.NoticeId = modelMGovNotice.NoticeId;
                MGovNoticeReceiverlist.Add(modelMGovNoticeReceiver);
                //释放实体资源
                modelMGovNoticeReceiver = null;
            }

            #endregion

            #region 指定部门
            if (cbxSelect.Checked)
            {
                //指定部门ids值
                string selectDepartlist = Utils.GetFormValue("SectionHideID");
                if (!string.IsNullOrEmpty(selectDepartlist))
                {
                    if (selectDepartlist.Contains(","))
                    {
                        //循环指定部门存入数据库
                        foreach (string strid in selectDepartlist.Split(','))
                        {
                            MGovNoticeReceiver modelselectReceiver = new MGovNoticeReceiver();
                            modelselectReceiver.ItemId = strid;
                            modelselectReceiver.ItemType = ItemType.指定部门;
                            modelselectReceiver.NoticeId = modelMGovNotice.NoticeId;
                            MGovNoticeReceiverlist.Add(modelselectReceiver);
                            //释放实体资源
                            modelselectReceiver = null;
                        }
                    }
                    else
                    {
                        modelMGovNoticeReceiver.ItemId = selectDepartlist;
                        modelMGovNoticeReceiver.ItemType = ItemType.指定部门;
                        modelMGovNoticeReceiver.NoticeId = modelMGovNotice.NoticeId;
                        MGovNoticeReceiverlist.Add(modelMGovNoticeReceiver);
                        //释放实体资源
                        modelMGovNoticeReceiver = null;
                    }
                }
            }
            #endregion

            #region 同行社
            if (cbxPeer.Checked)
            {
                modelMGovNoticeReceiver = new MGovNoticeReceiver();
                modelMGovNoticeReceiver.ItemType = ItemType.同行社;
                modelMGovNoticeReceiver.NoticeId = modelMGovNotice.NoticeId;
                MGovNoticeReceiverlist.Add(modelMGovNoticeReceiver);
                //释放实体资源
                modelMGovNoticeReceiver = null;
            }

            #endregion

            #region 组团社
            if (cbxGrounp.Checked)
            {
                modelMGovNoticeReceiver = new MGovNoticeReceiver();
                modelMGovNoticeReceiver.ItemType = ItemType.组团社;
                modelMGovNoticeReceiver.NoticeId = modelMGovNotice.NoticeId;
                MGovNoticeReceiverlist.Add(modelMGovNoticeReceiver);
                //释放实体资源
                modelMGovNoticeReceiver = null;
            }

            #endregion

            #endregion

            modelMGovNotice.MGovNoticeReceiverList = MGovNoticeReceiverlist;
            #endregion

            #region 通知公告实体隐形属性
            //操作人id
            modelMGovNotice.OperatorId = SiteUserInfo.UserId;
            //短信内容
            modelMGovNotice.MsgContent = "";
            //是否提醒
            modelMGovNotice.IsRemind = false;
            //是否发送短信
            modelMGovNotice.IsMsg = false;
            //公司编号
            modelMGovNotice.CompanyId = SiteUserInfo.CompanyId;

            #endregion

            #endregion

            bool result = false;
            //新增公告
            if (doType == "Add")
            {
                modelMGovNotice.NoticeId = Guid.NewGuid().ToString();
                result = bllBNotice.AddGovNotice(modelMGovNotice);
                if (result)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('新增成功!');;window.location='NoticeList.aspx?sl=1';");
                }
                else
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('新增失败!');;window.location='NoticeList.aspx?sl=1';");
            }
            else//修改公告
            {
                result = bllBNotice.UpdateGovNotice(modelMGovNotice);
                if (result)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改成功!');;window.location='NoticeList.aspx?sl=1';");
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改失败!');;window.location='NoticeList.aspx?sl=1';");
                }
            }
        }
        #endregion

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
