using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.WebFX.CommonPage
{
    public partial class selectCaiDan : FrontPage
    {
        protected int pageSize = 24;
        protected int pageIndex = 0;
        protected int recordCount = 0;
        protected int listCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //供应商编号
            string gysid = EyouSoft.Common.Utils.GetQueryStringValue("hideID");
            if (!IsPostBack)
            {
                PageInit(gysid);
            }
        }

        private void PageInit(string GysId)
        {
            EyouSoft.BLL.HGysStructure.BJiaGe bll = new EyouSoft.BLL.HGysStructure.BJiaGe();
            EyouSoft.Model.EnumType.SysStructure.LngType LgType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
            int lgType = Utils.GetInt(Utils.GetQueryStringValue("LgType")) == 0 ? 1 : Utils.GetInt(Utils.GetQueryStringValue("LgType"));
            if (lgType > 1)
            {
                LgType = (EyouSoft.Model.EnumType.SysStructure.LngType)lgType;
            }
            IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> list = bll.GetCanGuanCaiDan(GysId,LgType);
            if (list != null && list.Count > 0)
            {
                this.RepList.DataSource = list;
                this.RepList.DataBind();
                BindPage();
            }
            else
            {
                this.lbemptymsg.Text = "<tr class='old'><td colspan='4' align='center'>" + (String)GetGlobalResourceObject("string", "暂无数据") + "</td></tr>";
            }

        }

        #region 设置分页
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        #endregion
    }
}
