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
using EyouSoft.Common.Page;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Common;
using EyouSoft.Model.IndStructure;

namespace EyouSoft.Web.UserCenter.Memo
{
    public partial class MemoInfo : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //备忘录id
                string memoid = Utils.GetQueryStringValue("Id");
                IntoDate(memoid);
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void IntoDate(string id)
        {

            BIndividual bllBIndividual = new BIndividual();
            #region 初始化修改实体
            if (!string.IsNullOrEmpty(id))
            {
                MMemo modelMMemo = new MMemo();
                modelMMemo = bllBIndividual.GetMemo(Utils.GetInt(id));
                if (modelMMemo != null)
                {
                    //时间
                    litTime.Text = modelMMemo.IssueTime.ToString();
                    //备忘录状态
                    litMemoState.Text = modelMMemo.MemoState.ToString();
                    //备忘录内容
                    litMemoText.Text = modelMMemo.MemoText;
                    //备忘录标题
                    littitle.Text = modelMMemo.MemoTitle;
                    //备忘录紧急程度
                    litMemoUrgent.Text = modelMMemo.UrgentType.ToString();
                }
                else
                {
                    Response.Clear();
                    Response.Write("数据为空");
                    Response.End();
                }
            }
            else
            {
                Response.Clear();
                Response.Write("参数为空");
                Response.End();
            }
            #endregion
        }
    }
}
