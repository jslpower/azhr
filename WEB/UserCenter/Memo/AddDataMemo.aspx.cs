using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.IndStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.IndStructure;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.UserCenter.Memo
{
    public partial class AddDataMemo : BackPage
    {
        protected string stardate = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string ajaxtype = Utils.GetQueryStringValue("AjaxType");
            if (!string.IsNullOrEmpty(ajaxtype))
                Ajax(ajaxtype);

            //指定时间
            stardate = Server.UrlDecode(Utils.GetQueryStringValue("stardate"));
            if (!string.IsNullOrEmpty(stardate))
                txtMemoTime.Text = Utils.GetDateTime(stardate).ToShortDateString();
            hidstart.Value = stardate;
            if (!IsPostBack)
            {
                BindDropDownList();
                IntoData();
            }
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        private void IntoData()
        {

            BIndividual bllBIndividual = new BIndividual();
            #region 初始化修改实体
            string memoid = Utils.GetQueryStringValue("Id");
            hidid.Value = memoid;
            if (!string.IsNullOrEmpty(memoid))
            {
                MMemo modelMMemo = new MMemo();
                modelMMemo = bllBIndividual.GetMemo(Utils.GetInt(memoid));
                if (modelMMemo != null)
                {
                    txtMemoTitle.Text = modelMMemo.MemoTitle;
                    txtMemoTime.Text = modelMMemo.MemoTime.ToShortDateString();
                    txtMemoText.Text = modelMMemo.MemoText;
                    dropMemoState.SelectedValue = ((int)modelMMemo.MemoState).ToString();
                    dropMemoUrgent.SelectedValue = ((int)modelMMemo.UrgentType).ToString();
                }
            }
            #endregion

            #region 绑定列表
            IList<MMemo> list = bllBIndividual.GetMemoLst(0, SiteUserInfo.UserId, Utils.GetDateTimeNullable(hidstart.Value), Utils.GetDateTimeNullable(hidstart.Value));
            if (list != null && list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            #endregion
        }

        /// <summary>
        /// ajax 操作
        /// </summary>
        /// <param name="type"></param>
        private void Ajax(string type)
        {
            switch (type)
            {
                case "ajaxAddMemo":
                    AddMemo();
                    break;
                case "DeleteMemo":
                    DeleteMemo();
                    break;
            }
        }

        private void DeleteMemo()
        {
            string result = "";
            string memoid = Utils.GetQueryStringValue("Id");
            if (!string.IsNullOrEmpty(memoid))
            {
                BIndividual bllBIndividual = new BIndividual();
                if (bllBIndividual.DelMemo(Utils.GetInt(memoid), SiteUserInfo.CompanyId))//删除方法
                {
                    result = UtilsCommons.AjaxReturnJson("true", "删除成功");
                }
                else
                    result = UtilsCommons.AjaxReturnJson("false", "删除失败");
            }
            else
                result = UtilsCommons.AjaxReturnJson("false", "缺少参数");
            Response.Clear();
            Response.Write(result);
            Response.End();

        }

        private void AddMemo()
        {
            string result = "";
            BIndividual bllBIndividual = new BIndividual();
            MMemo modelMMemo = new MMemo();
            if (!string.IsNullOrEmpty(hidid.Value))
            {
                modelMMemo = bllBIndividual.GetMemo(Utils.GetInt(hidid.Value));
            }
            #region 备忘录属性
            //备忘时间
            string txtMemoTime = Utils.GetFormValue("txtMemoTime");
            //事件紧急程度
            string dropUrgentType = Utils.GetFormValue("dropMemoUrgent");
            //完成状态
            string dropMemoState = Utils.GetFormValue("dropMemoState");
            //事件标题
            string txtMemoTitle = Utils.GetFormValue("txtMemoTitle");
            //事件详细
            string txtMemoText = Utils.GetFormValue("txtMemoText");
            #endregion
            modelMMemo.MemoTime = Utils.GetDateTime(txtMemoTime);
            modelMMemo.UrgentType = (MemoUrgent)Utils.GetInt(dropUrgentType);
            modelMMemo.MemoState = (MemoState)Utils.GetInt(dropMemoState);
            modelMMemo.MemoTitle = txtMemoTitle;
            modelMMemo.MemoText = txtMemoText;
            if (!string.IsNullOrEmpty(hidid.Value))
            {
                if (bllBIndividual.UpdateMemo(modelMMemo))
                {
                    result = UtilsCommons.AjaxReturnJson("true", "修改成功");
                }
                else
                {
                    result = UtilsCommons.AjaxReturnJson("false", "修改失败");
                }
            }
            else
            {
                modelMMemo.OperatorId = SiteUserInfo.UserId;
                modelMMemo.IssueTime = DateTime.Now;
                modelMMemo.CompanyId = SiteUserInfo.CompanyId;
                if (bllBIndividual.AddMemo(modelMMemo))
                {
                    result = UtilsCommons.AjaxReturnJson("true", "添加成功");
                }
                else
                {
                    result = UtilsCommons.AjaxReturnJson("false", "添加失败");
                }
            }

            Response.Clear();
            Response.Write(result);
            Response.End();
        }


        private void BindDropDownList()
        {
            this.dropMemoUrgent.DataSource = EnumObj.GetList(typeof(MemoUrgent));
            this.dropMemoUrgent.DataTextField = "Text";
            this.dropMemoUrgent.DataValueField = "Value";
            this.dropMemoUrgent.DataBind();
            this.dropMemoUrgent.Items.Insert(0, new ListItem("--请选择--", "-1"));

            this.dropMemoState.DataSource = EnumObj.GetList(typeof(MemoState));
            this.dropMemoState.DataTextField = "Text";
            this.dropMemoState.DataValueField = "Value";
            this.dropMemoState.DataBind();
            this.dropMemoState.Items.Insert(0, new ListItem("--请选择--", "-1"));
        }

        #region 前台调用方法

        /// <summary>
        /// 获得线路区域的Options
        /// </summary>
        /// <param name="selectIndex">设置选择项</param>
        /// <returns></returns>
        public static string GetSelect(string selectIndex, string type)
        {
            StringBuilder sb = new StringBuilder();
            List<EnumObj> list = null;
            switch (type)
            {
                case "MemoUrgent":
                    list = EnumObj.GetList(typeof(MemoUrgent));
                    break;
                case "MemoState":
                    list = EnumObj.GetList(typeof(MemoState));
                    break;
            }
            sb.Append("<option value=\"-1\">-请选择-</option>");
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Value != selectIndex)
                    {
                        sb.Append("<option  value=\"" + list[i].Value + "\">" + list[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option  value=\"" + list[i].Value + "\" selected=\"selected\">" + list[i].Text + "</option>");
                    }
                }
            }

            return sb.ToString();
        }


        #endregion
    }
}
