using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.UserControl
{
    /// <summary>
    /// 财务筛选用户控件
    /// </summary>
    /// 汪奇志 2012-03-29
    public partial class CaiWuShaiXuan : System.Web.UI.UserControl
    {
        #region attributes
        /// <summary>
        /// 获取查询操作符ClientID及UniqueID
        /// </summary>
        public string ClientUniqueIDOperator { get { return "txt" + ClientID + "Operator"; } }
        /// <summary>
        /// 获取查询操作数ClientID及UniqueID
        /// </summary>
        public string ClientUniqueIDOperatorNumber { get { return "txt" + ClientID + "OperatorNumber"; } }
        /// <summary>
        /// 获取用户控件UniqueID
        /// </summary>
        public string ClientUniqueID { get { return ClientID; } }
        /// <summary>
        /// 筛选标题
        /// </summary>
        public string ShaiXuanBiaoTi { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //javascript code:var uniqueID = { "ClientUniqueIDOperator": "txt1", "ClientUniqueIDOperatorNumber": "txt2" };
            //string s = string.Format("var {0}={{ \"ClientUniqueIDOperator\":\"{1}\", \"ClientUniqueIDOperatorNumber\":\"{2}\"}};", ClientUniqueID, ClientUniqueIDOperator, ClientUniqueIDOperatorNumber);
           
            //this.Page.ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), s, true);
            //Utils.RegisterClientScript(s, ltrScripts);
        }
    }
}