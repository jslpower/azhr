using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using Adpost.Common.ExporPage;
using System.Text;
using EyouSoft.Model.CrmStructure;

namespace Web.CommonPage
{
    /// <summary>
    /// 客户单位选用页面
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-20
    /// 页面参数说明：
    /// callBack  回调函数名 
    /// CustomerUnitName 客户单位
    /// CustomerUnitId 客户单位id
    /// CustomerUnitType 客户单位类型
    /// IframeID 自身IframeID
    /// pIframeID 上级弹窗的IframeID 若上级页面不是弹窗可省略
    /// rc 单选或者多选 c表示多选,r表示单选(默认为多选)
    public partial class CustomerUnitSelect : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("doType") == "GetCustomerUnit") GetCustomerUnit();
            pan_titleBtn.Visible = Utils.GetIntSign(Utils.GetQueryStringValue("IsUniqueness")) < 0;
        }

        #region private members
        void GetCustomerUnit()
        {
            int recordCount = 0;
            MLBCrmSearchInfo queryString = new MLBCrmSearchInfo();
            queryString.CrmName = Utils.GetQueryStringValue("q");
            StringBuilder sb = new StringBuilder();
            IList<EyouSoft.Model.CrmStructure.MLBCrmXuanYongInfo> ls = new EyouSoft.BLL.CrmStructure.BCrm().GetCrmsXuanYong(CurrentUserCompanyID, 35, 1, ref recordCount, null, queryString);
            if (ls != null && ls.Count > 0)
            {
                foreach (MLBCrmXuanYongInfo item in ls)
                {
                    sb.Append(item.Name + "|" + item.CrmId + "|" + (int)item.CrmType + "|" + item.KeHuDengJiBH);
                    if (item.Lxrs != null && item.Lxrs.Count > 0)
                    {
                        sb.Append("|" + item.Lxrs[0].Id + "|" + item.Lxrs[0].Name + "|" + item.Lxrs[0].MobilePhone + "|" + item.Lxrs[0].Telephone + "\n");

                    }
                    else
                    {

                        sb.Append("|-1|无联系人|-1|-1 \n");
                    }
                }
            }
            else
            {
                sb.Append("无匹配项|-1|-1|无联系人|-1|-1|");
            }
            AjaxResponse(sb.ToString());
        }
        #endregion

        #region protected members
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
