using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.UserCenter.DocManage
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-20
    /// 说明：个人中心：文档管理 新增，修改
    public partial class DocEdit : BackPage
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
            this.txtFileName.Text = "";
            this.txtRemarks.Text = "";
            this.txtAddUser.Text = "";
            this.txtAddDate.Text = "";
        }


        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {


            bool result = true;
            string fileTitle = this.txtFileName.Text.Trim();
            string remarks = this.txtRemarks.Text.Trim();
            string addUser = this.txtAddUser.Text.Trim();
            DateTime addDate = Utils.GetDateTime(this.txtAddDate.Text.Trim());



            if (result)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改成功!');;window.location='目标地址';");
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改失败!');;window.location='目标地址';");
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
