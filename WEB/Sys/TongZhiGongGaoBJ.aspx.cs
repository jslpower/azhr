using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.Model.GovStructure;
using EyouSoft.BLL.GovStructure;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.GovStructure;
using EyouSoft.Model.EnumType.ComStructure;
namespace EyouSoft.Web.Sys
{
    /// <summary>
    /// 系统设置-通知公告-添加
    /// </summary>
    /// 修改人：刘树超
    /// 修改时间：2013-6-5

    public partial class TongZhiGongGaoBJ : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("id");
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (save == "save")
            {
                PageSave(doType);
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id);
            }
        }

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            #region 初始化用户控件
            this.SelectSection1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
            this.SelectSection1.SModel = "2";
            this.SelectSection1.SetTitle = "指定部门";
            this.SingleFileUpload1.CompanyID = this.SiteUserInfo.CompanyId;
            #endregion

            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BNotice BLL = new EyouSoft.BLL.GovStructure.BNotice();
                MGovNotice Model = BLL.GetGovNoticeModel(id);
                if (null != Model)
                {
                    #region 普通赋值
                    //标题
                    this.txtTitle.Text = Model.Title;
                    this.hidNotiecID.Value = Model.NoticeId;
                    //内容
                    this.txtContent.Text = Model.Content;
                    //消息内容
                    //this.msgContent.Text = Model.MsgContent;
                    //发布人
                    this.lbSender.Text = Model.Operator;
                    //发布时间       
                    this.lbTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", Model.IssueTime);
                    #endregion

                    #region 发布对象
                    IList<MGovNoticeReceiver> lst = Model.MGovNoticeReceiverList;
                    if (lst != null && lst.Count > 0)
                    {
                        string sectionID = "";
                        foreach (MGovNoticeReceiver item in lst)
                        {
                            switch (item.ItemType)
                            {
                                case EyouSoft.Model.EnumType.GovStructure.ItemType.公司内部:
                                    innerSection.Checked = true;
                                    break;
                                case EyouSoft.Model.EnumType.GovStructure.ItemType.指定部门:
                                    PointDept.Checked = true;
                                    sectionID += item.ItemId.Trim() + ",";
                                    break;
                                case EyouSoft.Model.EnumType.GovStructure.ItemType.分销商:
                                    fenxiao.Checked = true;
                                    break;
                                case EyouSoft.Model.EnumType.GovStructure.ItemType.供应商:
                                    gongying.Checked = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        sectionID = sectionID.Length > 0 ? sectionID.Substring(0, sectionID.Length - 1) : "";
                        this.SelectSection1.SectionID = sectionID;
                        this.SelectSection1.SectionName = GetDeptName(sectionID);
                    }

                    #endregion

                    #region 是否提醒
                    if (Model.IsRemind)
                    {
                        warn.Checked = true;
                        nowarn.Checked = false;
                    }
                    else
                    {
                        nowarn.Checked = true;
                        warn.Checked = false;
                    }
                    #endregion

                    //#region 是否发送短信
                    //if (Model.IsMsg == true)
                    //{
                    //    sendMsg.Checked = true;
                    //    noSendMsg.Checked = false;
                    //    msgContent.Text = Model.MsgContent;
                    //}
                    //if (Model.IsMsg == false)
                    //{
                    //    sendMsg.Checked = false;
                    //    noSendMsg.Checked = true;
                    //}
                    //#endregion

                    #region 附件处理
                    //附件
                    StringBuilder strFile = new StringBuilder();
                    IList<EyouSoft.Model.ComStructure.MComAttach> lstFile = Model.ComAttachList;
                    if (null != lstFile && lstFile.Count > 0)
                    {
                        for (int i = 0; i < lstFile.Count; i++)
                        {
                            strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={2}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", lstFile[i].FilePath, lstFile[i].Name,HttpUtility.UrlEncode(lstFile[i].Name));
                        }
                    }
                    this.lbFiles.Text = strFile.ToString();//附件
                    #endregion

                    #region 权限判断
                    //权限判断
                    //if (!this.SiteUserInfo.IsHandleElse && Model.OperatorId != this.SiteUserInfo.UserId)
                    //{
                    //this.ph_Save.Visible = false;
                    //}
                    #endregion

                }
            }
            else
            {
                this.lbSender.Text = this.SiteUserInfo.Name;
                this.lbTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now);
            }

        }
        #endregion

        #region 页面保存
        /// <summary>
        /// 页面保存
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            Response.Clear();
            string msg = "";
            bool result = false;
            string title = Utils.GetFormValue(txtTitle.UniqueID);
            string id = Utils.GetFormValue(hidNotiecID.UniqueID);
            string content = Utils.GetFormValue(txtContent.UniqueID);
            //string msgcontent = Utils.GetFormValue(msgContent.UniqueID);
            //string ismsg = Utils.GetFormValue("isSendMsg");
            string sectionhideid = Utils.GetFormValue(this.SelectSection1.SelectIDClient);
            string pointdept = Utils.GetFormValue("PointDept");
            string fenxiao = Utils.GetFormValue("fenxiao");
            string gongying = Utils.GetFormValue("gongying");
            string innersection = Utils.GetFormValue("innerSection");

            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(title))
            {
                msg += "-请输入公告标题！+<br/>";
            }
            if (string.IsNullOrEmpty(content))
            {
                msg += "-请输入公告内容！<br/>";
            }
            //if (ismsg == "sendMsg" && string.IsNullOrEmpty(msgcontent))
            //{
            //    msg += "-请输入短信内容！<br/>";
            //}
            if (!string.IsNullOrEmpty(pointdept) && string.IsNullOrEmpty(sectionhideid))
            {
                msg += "-请选择指定部门！<br/>";
            }
            if (string.IsNullOrEmpty(pointdept) && string.IsNullOrEmpty(fenxiao) && string.IsNullOrEmpty(gongying) && string.IsNullOrEmpty(innersection))
            {
                msg += "-请选择发布对象！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
                return;
            }
            #endregion

            #region 实体赋值
            EyouSoft.BLL.GovStructure.BNotice NoticeBLL = new BNotice();
            MGovNotice noticeModel = new MGovNotice();
            //公司编号
            noticeModel.CompanyId = this.SiteUserInfo.CompanyId;
            //公告标题
            noticeModel.Title = title;
            noticeModel.NoticeId = id;
            //公告内容
            noticeModel.Content = content;
            //是否提醒
            noticeModel.IsRemind = Utils.GetFormValue("isTixing") == "warn" ? true : false;
            //是否发送短信
            //noticeModel.IsMsg = ismsg == "sendMsg";
            noticeModel.IsMsg = false;
            //短信内容
            //noticeModel.MsgContent = msgcontent;
            //浏览量
            //noticeModel.Views = 0;
            //部门编号
            noticeModel.DepartId = this.SiteUserInfo.DeptId;
            //操作人编号
            noticeModel.OperatorId = this.SiteUserInfo.UserId;
            //操作时间
            noticeModel.IssueTime = DateTime.Now;
            //操作人
            noticeModel.Operator = this.SiteUserInfo.Name;// Username;
            //发布对象实体
            noticeModel.MGovNoticeReceiverList = GetList(sectionhideid, pointdept, fenxiao, gongying, innersection);

            noticeModel.ComAttachList = NewGetAttach();
            #endregion

            #region 提交回应
            if (doType == "update")
            {

                //如果公告编号不为空则为修改
                result = NoticeBLL.UpdateGovNotice(noticeModel);
                msg = result ? "修改成功!" : "修改失败！";
            }
            else
            {
                //公告编号为空则新增
                result = NoticeBLL.AddGovNotice(noticeModel);
                msg = result ? "添加成功！" : "添加失败！";
            }
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion

        }
        #endregion

        private IList<MComAttach> NewGetAttach()
        {
            //之前上传的附件
            string stroldupload = Utils.GetFormValue("hideFileInfo");
            IList<MComAttach> lst = new List<MComAttach>();
            if (!string.IsNullOrEmpty(stroldupload))
            {
                string[] oldupload = stroldupload.Split(',');
                if (oldupload != null && oldupload.Length > 0)
                {
                    for (int i = 0; i < oldupload.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(oldupload[i]))
                        {
                            string[] uploaditem = oldupload[i].Split('|');
                            MComAttach attModel = new MComAttach();
                            attModel.Name = uploaditem[0];
                            attModel.FilePath = uploaditem[1];
                            lst.Add(attModel);
                        }
                    }
                }
            }
            //新上传附件
            string[] upload = Utils.GetFormValues(this.SingleFileUpload1.ClientHideID);
            for (int i = 0; i < upload.Length; i++)
            {
                string[] newupload = upload[i].Split('|');
                if (newupload != null && newupload.Length > 1)
                {
                    MComAttach attModel = new MComAttach();
                    attModel.FilePath = newupload[1];
                    attModel.Name = newupload[0];
                    lst.Add(attModel);
                }
            }
            return lst;
        }

        #region 获取部门名称
        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="DeptId">部门编号</param>
        /// <returns></returns>
        protected string GetDeptName(string DeptId)
        {
            EyouSoft.BLL.ComStructure.BComDepartment secBLL = new EyouSoft.BLL.ComStructure.BComDepartment();
            string departName = "";
            if (!string.IsNullOrEmpty(DeptId))
            {
                string[] deptArry = DeptId.Split(',');
                if (deptArry.Length > 0)
                {
                    for (int i = 0; i < deptArry.Length; i++)
                    {
                        EyouSoft.Model.ComStructure.MComDepartment secModel = secBLL.GetModel(Utils.GetInt(deptArry[i]), this.SiteUserInfo.CompanyId);
                        if (secModel != null)
                        {
                            departName += secModel.DepartName + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(departName))
                    {
                        departName = departName.Substring(0, departName.Length - 1);
                    }
                }
            }
            return departName;
        }
        #endregion

        #region 获取接收人员列表
        /// <summary>
        /// 获取接收人员列表
        /// </summary>
        /// <param name="SectionHideID">隐藏域知</param>
        /// <param name="PointDept">指定部门</param>
        /// <param name="fenxiao">分销商</param>
        /// <param name="gongying">供应商</param>
        /// <param name="innerSection">公司内部</param>
        /// <returns></returns>
        protected IList<MGovNoticeReceiver> GetList(string SectionHideID, string PointDept, string fenxiao, string gongying, string innerSection)
        {
            IList<MGovNoticeReceiver> List = new List<MGovNoticeReceiver>();
            MGovNoticeReceiver model = null;
            if (!string.IsNullOrEmpty(PointDept) && !string.IsNullOrEmpty(SectionHideID))
            {
                string[] deptId = SectionHideID.Split(',');
                for (int i = 0; i < deptId.Length; i++)
                {
                    model = new MGovNoticeReceiver();
                    model.ItemId = deptId[i];
                    model.ItemType = EyouSoft.Model.EnumType.GovStructure.ItemType.指定部门;
                    List.Add(model);
                }
            }
            if (!string.IsNullOrEmpty(fenxiao))
            {
                model = new MGovNoticeReceiver();
                model.ItemType = EyouSoft.Model.EnumType.GovStructure.ItemType.分销商;
                List.Add(model);
            }
            if (!string.IsNullOrEmpty(gongying))
            {
                model = new MGovNoticeReceiver();
                model.ItemType = EyouSoft.Model.EnumType.GovStructure.ItemType.供应商;
                List.Add(model);
            }
            if (!string.IsNullOrEmpty(innerSection))
            {
                model = new MGovNoticeReceiver();
                model.ItemType = EyouSoft.Model.EnumType.GovStructure.ItemType.公司内部;
                List.Add(model);
            }
            return List;
        }

        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "add")
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_新增))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_新增, false);
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_通知公告_修改, false);
                    }
                }
            }
        }
        #endregion

        #region 重写OnPreInit
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #endregion

    }
}