using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.WebFX.CommonPage
{
    public partial class Journey : EyouSoft.Common.Page.FrontPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private int pageSize = 5;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 100;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax for save
            string type = Utils.GetQueryStringValue("type");
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("save"))
                {
                    Response.Write(Save());
                    Response.End();
                    Response.Clear();
                }
            }

            if (!IsPostBack)
            {
                PageInit();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void PageInit()
        {
            //IList<MSysJourneyMark> GetMSysJourneyMarkList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysJourneyMarkSearch searchInfo)

            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);

            EyouSoft.Model.SysStructure.MSysJourneyMarkSearch search = new EyouSoft.Model.SysStructure.MSysJourneyMarkSearch();
            string LngType = Utils.GetQueryStringValue("LngType");
            if (!string.IsNullOrEmpty(LngType) && Utils.GetInt(LngType) != 0)
            {
                search.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(LngType);
            }
            else
            {
                search.LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
            }



            EyouSoft.BLL.SysStructure.BSysOptionConfig bll = new EyouSoft.BLL.SysStructure.BSysOptionConfig();
            IList<EyouSoft.Model.SysStructure.MSysJourneyMark> list = bll.GetMSysJourneyMarkList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, search);


            if (list != null && list.Count > 0)
            {
                this.rpJourney.DataSource = list;
                this.rpJourney.DataBind();

                //绑定分页
                BindPage();
                this.litMsg.Visible = false;
            }
            else
            {
                this.litMsg.Text = "<tr><td align='center' colspan='2'>'" + (String)GetGlobalResourceObject("string", "暂无数据") + "'</td></tr>";
                this.litMsg.Visible = true;
                this.ExporPageInfoSelect1.Visible = false;
            }

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;

        }


        /// <summary>
        /// 添加行程备注
        /// </summary>
        /// <returns></returns>
        private string Save()
        {
            string msg = string.Empty;

            EyouSoft.Model.EnumType.SysStructure.LngType LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;

            int strLngType = Utils.GetInt(Utils.GetQueryStringValue("LngType"));

            if (strLngType > 0)
            {
                LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)(strLngType);
            }

            EyouSoft.Model.SysStructure.MSysJourneyMark model = new EyouSoft.Model.SysStructure.MSysJourneyMark();
            model.CompanyId = SiteUserInfo.CompanyId;
            model.BackMark = Request.Form["txtNewInfo"];
            model.LngType = LngType;
            model.OperatorDeptId = SiteUserInfo.DeptId;
            model.Operator = SiteUserInfo.Name;
            model.OperatorId = SiteUserInfo.UserId;
            model.IssueTime = DateTime.Now;

            EyouSoft.BLL.SysStructure.BSysOptionConfig bll = new EyouSoft.BLL.SysStructure.BSysOptionConfig();
            if (bll.AddMSysJourneyMark(model) == 1)
            {
                msg = UtilsCommons.AjaxReturnJson("1", "行程备注添加成功!");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "行程备注添加失败!");
            }
            return msg;
        }
    }
}
