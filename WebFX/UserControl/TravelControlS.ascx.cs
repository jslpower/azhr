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
    /// 说明：旅客添加：出境
    public partial class TravelControlS : System.Web.UI.UserControl
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

        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack)
            {
                GetDataList();
                base.OnLoad(e);
            }
        }

        /// <summary>
        /// 获取控件的数据
        /// </summary>
        protected void GetDataList()
        {
            //旅客编号
            string[] travellerId = Utils.GetFormValues("txt_TravelControl_TreavelID");
            //旅客姓名Cn
            string[] userNameCn = Utils.GetFormValues("txt_TravelControlS_TreavelID_NameCn");
            //旅客姓名En
            string[] userNameEn = Utils.GetFormValues("txt_TravelControlS_TreavelID_NameEn");
            //证件类型
            string[] proveType = Utils.GetFormValues("slt_TravelControlS_ProveType");
            //身份证
            string[] userSfz = Utils.GetFormValues("slt_TravelControlS_Sfz");
            //旅客类型
            string[] peopleType = Utils.GetFormValues("slt_TravelControlS_PeoType");

            //证件号码
            string[] txtProveNumber = Utils.GetFormValues("txt_TravelControlS_CardNum");
            //性别
            string[] txtUserSex = Utils.GetFormValues("slt_TravelControlS_Sex");
            //电话
            string[] txtPhone = Utils.GetFormValues("txt_TravelControlS_ContactTel");
            //备注
            string[] txtRemarks = Utils.GetFormValues("txt_TravelControlS_Remarks");
            //出团提醒
            string[] cbxLeaveMsg = Utils.GetFormValues("cbx_TravelControlS_LeaveMsg");
            //回团提醒
            string[] cbxBackMsg = Utils.GetFormValues("cbx_TravelControlS_BackMsg");
            //证件有效期
            string[] txtValidDate = Utils.GetFormValues("txt_TravelControlS_ValidDate");
            //是否办理
            string[] cbxHandle = Utils.GetFormValues("cbx__TravelControlS_IsBan");
            //签证状体
            string[] visaStatus = Utils.GetFormValues("slt__TravelControlS_State");

            if (userNameCn.Length > 0 && peopleType.Length > 0 && proveType.Length > 0 && txtProveNumber.Length > 0 && txtUserSex.Length > 0 && txtPhone.Length > 0 && txtRemarks.Length > 0 && cbxBackMsg.Length > 0 && cbxLeaveMsg.Length > 0)
            {
                IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();

                for (int i = 0; i < userNameCn.Length; i++)
                {

                    EyouSoft.Model.TourStructure.MTourOrderTraveller model = new EyouSoft.Model.TourStructure.MTourOrderTraveller();

                    //旅客姓名
                    model.CnName = userNameCn[i];
                    model.EnName = userNameEn[i];
                    //证件号码
                    model.CardNumber = txtProveNumber[i];
                    //身份证
                    model.CardId = userSfz[i];
                    //联系电话
                    model.Contact = txtPhone[i];
                    //编号
                    model.TravellerId = travellerId[i];
                    //游客类型
                    model.VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)Utils.GetInt(peopleType[i]);
                    //证件类型
                    model.CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)Utils.GetInt(proveType[i]);
                    //旅客性别
                    model.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(txtUserSex[i]);
                    //备注
                    model.Remark = txtRemarks[i];
                    //出团提醒
                    model.LNotice = cbxLeaveMsg[i] == "1" ? true : false;
                    //回团提醒
                    model.RNotice = cbxBackMsg[i] == "1" ? true : false;
                    //证件有效期
                    model.CardValidDate = txtValidDate[i];
                    //是否办理
                    model.IsCardTransact = cbxHandle[i] == "1" ? true : false;
                    //签证状态
                    model.VisaStatus = (EyouSoft.Model.EnumType.TourStructure.VisaStatus)Utils.GetInt(visaStatus[i]);
                    list.Add(model);

                }
                this.GetTravelList = list;
            }



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
                insuranceID = insuranceID.Substring(0, 1);
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
                insuranceName = insuranceName.Substring(0, 1);
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
                insurancePrice = insurancePrice.Substring(0, 1);
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
                insuranceCount = insuranceCount.Substring(0, 1);
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
