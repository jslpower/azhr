using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.HGysStructure;
using EyouSoft.Common;
using EyouSoft.Model.HGysStructure;

namespace EyouSoft.Web.CommonPage
{
    public partial class SelectScenic : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页数量
        /// </summary>
        protected int pageSize = 40;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 1;
        /// <summary>
        /// 一共多少页
        /// </summary>
        int recordCount = 0;
        protected int listCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Rpt_List();
        }

        #region private members
        void Rpt_List()
        {
            pageIndex = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("Page"), 1);
            BGys bScenid_model = new BGys();

            //获取景点所在国家、省份、城市、县区。供应商公司编号
            EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo msource = new EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo();

            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("cityids")))
                msource.CityIds = Utils.GetIntArray(Utils.GetQueryStringValue("cityids"), ",");

            msource.LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.景点;
            msource.JingDianName = Utils.GetQueryStringValue("textfield");
            msource.IsJingDianFuJian = true;
            msource.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)(Utils.GetInt(Utils.GetQueryStringValue("LgType")) == 0 ? 1 : Utils.GetInt(Utils.GetQueryStringValue("LgType")));
            //获取景点列表（获取县区下面的景区）
            IList<EyouSoft.Model.HGysStructure.MXuanYongJingDianInfo> list = bScenid_model.GetXuanYongJingDians(this.SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, msource);

            if (list != null && list.Count > 0)
            {
                listCount = list.Count;
                rpt_ScenicList.DataSource = list;
                rpt_ScenicList.DataBind();
                BindPage();
            }
            else
            {
                litMsg.Text = "<tr class='old'><td colspan='4' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }

        }

        /// <summary>
        /// 判断景点是否包含价格（用来控制要不要弹出价格选择窗口）
        /// </summary>
        /// <param name="JingDianId"></param>
        /// <returns></returns>
        protected string GetIsHavePrice(object JingDianId)
        {
            string result = string.Empty;
            EyouSoft.BLL.HGysStructure.BJiaGe bll = new EyouSoft.BLL.HGysStructure.BJiaGe();
            IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> list = bll.GetJingDianJiaGes(JingDianId.ToString(), null);
            if (list != null && list.Count > 0)
            {
                result = "yes";
            }
            else
            {
                result = "no";
            }
            return result;
        }

        /// <summary>
        /// 获取景点图片路径
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetFujianInfo(object obj)
        {
            string filepath = string.Empty;
            MFuJianInfo fujianinfo = (MFuJianInfo)obj;
            if (fujianinfo != null)
            {
                filepath = fujianinfo.FilePath;
            }
            return filepath;

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        #endregion
    }
}
