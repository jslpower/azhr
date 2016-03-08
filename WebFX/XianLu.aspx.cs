using System;

namespace EyouSoft.WebFX
{
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.SysStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.HTourStructure;

    public partial class XianLu : FrontPage
    {
        #region 分页参数

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
        /// <summary>
        /// 总条数
        /// </summary>
        private int recordCount = 0;
        /// <summary>
        /// 打印单链接
        /// </summary>
        protected string PrintPageSp = string.Empty;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //EyouSoft.BLL.ComStructure.BComSetting comSettingBll = new EyouSoft.BLL.ComStructure.BComSetting();
            //PrintPageSp = comSettingBll.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台散拼线路行程单);

            if (!IsPostBack)
            {
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                this.HeadDistributorControl1.IsPubLogin =
    System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == this.SiteUserInfo.Username;
                //绑定数据、分页
                BindSource();
            }
        }

        /// <summary>
        /// 绑定数据、分页控件
        /// </summary>
        public void BindSource()
        {
            var list = new EyouSoft.BLL.HTourStructure.BTour().GetTourInfoList(
                pageSize, pageIndex, ref recordCount, new MTourSearch()
                {
                    CompanyId = this.SiteUserInfo.CompanyId,
                    AreaId = Utils.GetInt(Utils.GetQueryStringValue("a")),
                    StartEffectTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sd")),
                    EndEffectTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ed")),
                    Days = Utils.GetIntNull(Utils.GetQueryStringValue("d")),
                    TourType = TourType.自由行,
                    LngType = (LngType?)Utils.GetInt(Utils.GetQueryStringValue("LgType"))
                });
            this.rptList.DataSource = list;
            this.rptList.DataBind();

            BindPage();

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            if (recordCount == 0)
            {
                this.PhPage.Visible = false;
                this.litMsg.Visible = true;
            }
            else
            {
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
        }

        /// <summary>
        /// 截取AreaName
        /// </summary>
        /// <param name="areaName"></param>
        /// <returns></returns>
        protected string GetAreaName(object obj)
        {
            int areaid = Utils.GetInt(obj.ToString());
            string areaName = string.Empty;
            if (areaid > 0)
            {
                var _bllComArea = new EyouSoft.BLL.ComStructure.BComArea();
                var model = _bllComArea.GetModel(areaid, SiteUserInfo.CompanyId, (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(Utils.GetQueryStringValue("LgType")));
                if (model != null)
                {
                    areaName = model.AreaName;
                }
            }

            if (!string.IsNullOrEmpty(areaName))
            {
                if (areaName.Length > 10)
                {
                    areaName = areaName.Substring(0, 10) + "...";
                }
            }
            return areaName;
        }
    }
}
