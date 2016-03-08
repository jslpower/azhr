using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.ImportSource
{
    public partial class ImportPage : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;

        ///// <summary>
        ///// 二级栏目
        ///// </summary>
        //EyouSoft.Model.EnumType.PrivsStructure.Menu2 Menu2Type = EyouSoft.Model.EnumType.PrivsStructure.Menu2.None;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Utils.GetQueryStringValue("type"))
            {
                case "1": //非出境游客列表
                    break;
                case "2": //出境游客列表
                    break;
                case "3": //生成短信发送的手机号码列表
                    break;
                case "4": //生成客户名单列表
                    break;
                case "5": //生成单项客户名单
                    DataInit();
                    break;
            }
        }

        /// <summary>
        /// get search info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.CrmStructure.MLBCrmPersonalSearchInfo GetSearchInfo()
        {
            var info = new EyouSoft.Model.CrmStructure.MLBCrmPersonalSearchInfo();

            //info.JiFenOperator = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(txtJiFen.ClientUniqueIDOperator));
            //info.JiFenOperatorNumber = Utils.GetIntNull(Utils.GetQueryStringValue(txtJiFen.ClientUniqueIDOperatorNumber), 0);
            //info.MemberCardCode = Utils.GetQueryStringValue("txtCardCode");
            //info.MemberTypeId = Utils.GetIntNull(Utils.GetQueryStringValue("txtMemberTypeId"));
            //info.Name = Utils.GetQueryStringValue("txtName");
            //info.Telephone = Utils.GetQueryStringValue("txtTelephone");

            return info;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            pageIndex = UtilsCommons.GetPadingIndex();

            var items = new EyouSoft.BLL.CrmStructure.BCrm().GetCrmsPersonal(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, GetSearchInfo());
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }

            BindPage();
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            paging.UrlParams = Request.QueryString;
            paging.intPageSize = pageSize;
            paging.CurrencyPage = pageIndex;
            paging.intRecordCount = recordCount;

            if (paging.intRecordCount == 0)
            {
                paging.Visible = false;
                phd.Visible = true;
            }
        }

    }
}
