using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.ComStructure;
using System.Text;
using EyouSoft.Model.SSOStructure;
using Newtonsoft.Json;
using EyouSoft.Common;

namespace EyouSoft.WebFX.UserControl
{
    /// <summary>
    /// 价格等级标准
    /// 编辑人：DYZ  创建日期：2011.10.28
    /// </summary>
    public partial class PriceStand : System.Web.UI.UserControl
    {

        private IList<EyouSoft.Model.TourStructure.MTourPriceStandard> _setPriceStandard;
        /// <summary>
        /// 绑定控件数据
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MTourPriceStandard> SetPriceStandard
        {
            get { return _setPriceStandard; }
            set { _setPriceStandard = value; }
        }

        /// <summary>
        /// 标准的Options
        /// </summary>
        protected string[] StandardNameOptions;

        private string _companyID;
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }

        /// <summary>
        /// 显示模式默认 文本框
        /// </summary>
        private bool _showModel = false;

        public bool ShowModel
        {
            get { return _showModel; }
            set { _showModel = value; }
        }

        /// <summary>
        /// 默认通过系统定义初始化
        /// </summary>
        private bool _initMode = true;

        public bool InitMode
        {
            get { return _initMode; }
            set { _initMode = value; }
        }
        /// <summary>
        /// 修改团队时需要
        /// </summary>
        private bool _initTour = true;

        public bool InitTour
        {
            get { return _initTour; }
            set { _initTour = value; }
        }

        private IList<EyouSoft.Model.ComStructure.MComLev> sysComLev = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CompanyID))
            {
                throw new Exception("PriceStand控件CompanyID未赋值");
            }

            if (!IsPostBack)
            {
                PriceStandInit();
            }
        }

        private void PriceStandInit()
        {
            BComStand bll = new BComStand();

            IList<EyouSoft.Model.ComStructure.MComStand> list = bll.GetList(this.CompanyID);

            if (list != null && list.Count > 0)
            {
                this.StandardNameOptions = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    this.StandardNameOptions[i] = list[i].Id + "|##|" + list[i].Name;
                }
            }

            //获得系统的报价等级
            sysComLev = new EyouSoft.BLL.ComStructure.BComLev().GetList(this.CompanyID);
            if (this.InitMode && (SetPriceStandard == null || SetPriceStandard.Count == 0))
            {
                if (sysComLev != null && sysComLev.Count > 0)
                {
                    //绑定表格头部
                    this.rptTableHeadSys.DataSource = sysComLev;
                    this.rptTableHeadSys.DataBind();
                    //生成成人 儿童列
                    this.rptTableHeadCol.DataSource = sysComLev;
                    this.rptTableHeadCol.DataBind();

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < sysComLev.Count; i++)
                    {
                        sb.Append("<td style='text-align:center;'>");
                        sb.Append("<input type='text' errmsg='请输入成人 " + sysComLev[i].Name.ToString() + "!|请输入正确的成人 " + sysComLev[i].Name.ToString() + "!|" + sysComLev[i].Name.ToString() + " 必须大于0' valid='required|isMoney|range' min='1' class='inputtext formsize50' name='txt_PriceStand_Adult_" + sysComLev[i].Id + "_0' value=''>");
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:center;'>");
                        sb.Append("<input type='text' errmsg='请输入儿童 " + sysComLev[i].Name.ToString() + "!|请输入正确的" + sysComLev[i].Name.ToString() + "!|" + sysComLev[i].Name.ToString() + " 必须大于0!' valid='required|isMoney|range' min='1' class='inputtext formsize50' name='txt_PriceStand_Child_" + sysComLev[i].Id + "_0' value=''>");
                        sb.Append("</td>");
                    }
                    this.litTableBody.Text = sb.ToString();
                }
            }
            else
            {
                if (SetPriceStandard != null && SetPriceStandard.Count > 0)
                {
                    if (this.InitTour)
                    {
                        //绑定表格头部
                        this.rptTableHead.DataSource = SetPriceStandard[0].PriceLevel;
                        this.rptTableHead.DataBind();
                        //生成成人 儿童列
                        this.rptTableHeadCol.DataSource = SetPriceStandard[0].PriceLevel;
                        this.rptTableHeadCol.DataBind();
                    }
                    else
                    {
                        //绑定表格头部
                        this.rptTableHeadSys.DataSource = sysComLev;
                        this.rptTableHeadSys.DataBind();
                        //生成成人 儿童列
                        this.rptTableHeadCol.DataSource = sysComLev;
                        this.rptTableHeadCol.DataBind();
                    }

                    this.rptList.DataSource = SetPriceStandard;
                    this.rptList.DataBind();
                }
            }
        }

        /// <summary>
        /// 获取标准
        /// </summary>
        /// <param name="selval"></param>
        /// <returns></returns>
        protected string GetPriceStandard(string selectValue)
        {
            StringBuilder sb = new StringBuilder();
            if (this.StandardNameOptions != null)
            {
                for (int i = 0; i < this.StandardNameOptions.Length; i++)
                {
                    string[] standardList = StandardNameOptions[i].Split(new string[] { "|##|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (standardList.Length > 1)
                    {
                        if (standardList[0] == selectValue)
                        {
                            sb.Append("<option selected='selected' value='" + standardList[0] + "'>" + standardList[1] + "</option>");
                        }
                        else
                        {
                            sb.Append("<option value='" + standardList[0] + "'>" + standardList[1] + "</option>");
                        }
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 根据集合生成表体
        /// </summary>
        /// <param name="o">数据源</param>
        /// <returns></returns>
        protected string GetTableBody(object o, int rowIndex)
        {
            int refCount = 0;

            StringBuilder sb = new StringBuilder();
            if (o != null)
            {
                List<EyouSoft.Model.TourStructure.MTourPriceLevel> priceLevelList = (List<EyouSoft.Model.TourStructure.MTourPriceLevel>)o;

                if (sysComLev != null)
                {
                    refCount = sysComLev.Count - priceLevelList.Count;
                }
                if (priceLevelList.Count > 0)
                {
                    if (this.InitMode)
                    {
                        for (int i = 0; i < sysComLev.Count; i++)
                        {
                            if (priceLevelList.Find(p => (p.LevelId == sysComLev[i].Id)) == null)
                            {
                                priceLevelList.Add(new EyouSoft.Model.TourStructure.MTourPriceLevel() { AdultPrice = 0, LevelId = sysComLev[i].Id, ChildPrice = 0, LevelName = sysComLev[i].Name, LevType = sysComLev[i].LevType });
                            }
                        }
                    }

                    //当系统等级类型数量 超过 数据源中的等级数量时
                    for (int j = 0; j < priceLevelList.Count; j++)
                    {
                        sb.Append(GetLevelHtml(priceLevelList[j].LevelId, priceLevelList[j].LevelName, priceLevelList[j].AdultPrice, priceLevelList[j].ChildPrice, rowIndex));
                    }
                }
            }
            return sb.ToString();
        }

        private string GetLevelHtml(int levelId, string levelName, decimal adultPrice, decimal childPrice, int rowIndex)
        {
            StringBuilder sb = new StringBuilder();
            if (this.ShowModel)
            {
                sb.Append("<td style='text-align:center;'>");
                sb.Append("<input type='radio' name='txt_PriceStand_radio_price' value='" + levelId.ToString() + "|" + Utils.FilterEndOfTheZeroDecimal(adultPrice) + "|" + Utils.FilterEndOfTheZeroDecimal(childPrice) + "' />");
                sb.Append("</td>");
            }

            sb.Append("<td style='text-align:center;'>");
            if (this.ShowModel)
            {
                sb.Append("<label>" + Utils.FilterEndOfTheZeroDecimal(adultPrice) + "</label>");
            }
            else
            {
                sb.Append("<input type='text' errmsg='请输入成人" + levelName + " !|请输入正确的成人 " + levelName + "!|成人" + levelName + " 必须大于0' valid='required|isMoney|range' min='1'  class='inputtext formsize50' name='txt_PriceStand_Adult_" + levelId.ToString() + "_" + rowIndex.ToString() + "' value='" + Utils.FilterEndOfTheZeroDecimal(adultPrice) + "'>");
            }

            sb.Append("</td>");
            sb.Append("<td style='text-align:center;'>");

            if (this.ShowModel)
            {
                sb.Append("<label>" + Utils.FilterEndOfTheZeroDecimal(childPrice) + "</label>");
            }
            else
            {
                sb.Append("<input type='text' errmsg='请输入儿童 " + levelName + "!|请输入正确的儿童 " + levelName + "!|儿童" + levelName + " 必须大于0' valid='required|isMoney|range' min='1' class='inputtext formsize50' name='txt_PriceStand_Child_" + levelId.ToString() + "_" + rowIndex.ToString() + "' value='" + Utils.FilterEndOfTheZeroDecimal(childPrice) + "'>");
            }
            sb.Append("</td>");

            return sb.ToString();
        }
    }
}