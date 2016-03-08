using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.WebFX.UserControl
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-15
    /// 说明：旅客添加：非出境 
    public partial class TravelControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置控件的数据源
        /// </summary>
        private IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> _setTravelList;

        public IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> SetTravelList
        {
            get { return _setTravelList; }
            set { _setTravelList = value; }
        }
        /// <summary>
        /// 获得控件内的数据
        /// </summary>
        private IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> _getTravelList;

        public IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTravelList
        {
            get { return _getTravelList; }
            set { _getTravelList = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            SetDataList();
            base.OnPreRender(e);
        }


        /// <summary>
        /// 页面初始化时绑定数据
        /// </summary>
        private void SetDataList()
        {
            this.rptList.DataSource = this.SetTravelList;
            this.rptList.DataBind();
        }

        protected string GetPeopleType(int selectIndex)
        {
            string sb = "<option value=\"1\" {0} >成人</option><option value=\"2\" {1} >儿童</option><option value=\"3\" {2}>其它</option>";
            switch (selectIndex)
            {
                case 1: sb = string.Format(sb, "selected='selected'", "", ""); break;
                case 2: sb = string.Format(sb, "", "selected='selected'", ""); break;
                case 3: sb = string.Format(sb, "", "", "selected='selected'"); break;
            }
            return sb;
        }

        protected string GetProveType(int selectIndex)
        {
            string sb = "<option value=\"0\" {0}>请选择证件</option><option value=\"1\" {1}>身份证</option><option value=\"2\" {2}>护照</option><option value=\"3\" {3}>军官证</option><option value=\"4\" {4}>台胞证</option><option value=\"5\" {5}>港澳通行证</option>";

            switch (selectIndex)
            {
                case 1: sb = string.Format(sb, "", "selected='selected'", "", "", "", ""); break;
                case 2: string.Format(sb, "", "", "selected='selected'", "", "", ""); break;
                case 3: string.Format(sb, "", "", "", "selected='selected'", "", ""); break;
                case 4: string.Format(sb, "", "", "", "", "selected='selected'", ""); break;
                case 5: string.Format(sb, "", "", "", "", "", "selected='selected'"); break;
                default: string.Format(sb, "selected='selected'", "", "", "", "", ""); break;
            }

            return sb;
        }

        /// <summary>
        /// 获得保险编号
        /// </summary>
        /// <param name="insuranceList"></param>
        /// <returns></returns>
        protected string GetInsuranceID(object list)
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> insuranceList = (List<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance>)list;
            string insuranceID = "";
            if (insuranceList != null && insuranceList.Count > 0)
            {
                for (int i = 0; i < insuranceList.Count; i++)
                {
                    insuranceID += "," + insuranceList[i].InsuranceId;
                }
                insuranceID = insuranceID.Substring(1, insuranceID.Length - 1);
            }
            return insuranceID;
        }

        /// <summary>
        /// 获得保险名称
        /// </summary>
        /// <param name="insuranceList"></param>
        /// <returns></returns>
        protected string GetInsuranceName(object list)
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> insuranceList = (List<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance>)list;
            string insuranceName = "";
            if (insuranceList != null && insuranceList.Count > 0)
            {
                for (int i = 0; i < insuranceList.Count; i++)
                {
                    insuranceName += "," + insuranceList[i].InsuranceId;
                }
                insuranceName = insuranceName.Substring(1, insuranceName.Length - 1);
            }
            return insuranceName;
        }

        /// <summary>
        /// 获得保险单价
        /// </summary>
        /// <param name="insuranceList"></param>
        /// <returns></returns>
        protected string GetInsurancePrice(object list)
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> insuranceList = (List<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance>)list;
            string insurancePrice = "";
            if (insuranceList != null && insuranceList.Count > 0)
            {
                for (int i = 0; i < insuranceList.Count; i++)
                {
                    insurancePrice += "," + insuranceList[i].UnitPrice.ToString("0.00");
                }
                insurancePrice = insurancePrice.Substring(1, insurancePrice.Length - 1);
            }
            return insurancePrice;
        }

        /// <summary>
        /// 获得保险数量
        /// </summary>
        /// <param name="insuranceList"></param>
        /// <returns></returns>
        protected string GetInsuranceCount(object list)
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> insuranceList = (List<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance>)list;
            string insuranceCount = "";
            if (insuranceList != null && insuranceList.Count > 0)
            {
                for (int i = 0; i < insuranceList.Count; i++)
                {
                    insuranceCount += "," + insuranceList[i].BuyNum;
                }
                insuranceCount = insuranceCount.Substring(1, insuranceCount.Length - 1);
            }
            return insuranceCount;
        }

        /// <summary>
        /// 获得保险数量
        /// </summary>
        /// <param name="insuranceList"></param>
        /// <returns></returns>
        protected string GetInsuranceImage(object list)
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> insuranceList = (List<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance>)list;
            string imgShow = "";
            if (insuranceList != null && insuranceList.Count > 0)
            {
                imgShow = "<img src=\"/images/y-duihao.gif\" border=\"0\">";
            }
            else
            {
                imgShow = "<img src=\"/images/y-cuohao.gif\" border=\"0\">";
            }
            return imgShow;
        }


    }
}
