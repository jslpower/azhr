using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class TravelControlS : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置控件的数据源
        /// </summary>
        private IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> _YouKes;

        public IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> YouKes
        {
            get { return GetFormInfo(); }
            set { _YouKes = value; }
        }

        IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetFormInfo()
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> items = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();

            string[] txt_TravelControl_TreavelID = Utils.GetFormValues("txt_TravelControl_TreavelID");
            string[] txt_TravelControl_Name = Utils.GetFormValues("txt_TravelControl_Name");
            string[] slt_TravelControl_UserSex = Utils.GetFormValues("slt_TravelControl_UserSex");
            string[] slt_TravelControl_PeopleType = Utils.GetFormValues("slt_TravelControl_PeopleType");
            string[] slt_TravelControl_ProveType = Utils.GetFormValues("slt_TravelControl_ProveType");
            string[] txt_TravelControl_CardNum = Utils.GetFormValues("txt_TravelControl_CardNum");
            string[] txt_TravelControl_EffectTime = Utils.GetFormValues("txt_TravelControl_EffectTime");
            string[] txt_TravelControl_IDCard = Utils.GetFormValues("txt_TravelControl_IDCard");
            string[] txt_TravelControl_Phone = Utils.GetFormValues("txt_TravelControl_Phone");
            string[] txt_TravelControl_Brithday = Utils.GetFormValues("txt_TravelControl_Brithday");
            string[] txt_TravelControl_Remarks = Utils.GetFormValues("txt_TravelControl_Remarks");
            string[] cbx_TravelControl_LeaveMsg = Utils.GetFormValues("cbx_TravelControl_LeaveMsg");
            string[] cbx_TravelControl_BackMsg = Utils.GetFormValues("cbx_TravelControl_BackMsg");

            if (txt_TravelControl_TreavelID == null || txt_TravelControl_TreavelID.Length == 0) return items;

            for (int i = 0; i < txt_TravelControl_TreavelID.Length; i++)
            {
                if (string.IsNullOrEmpty(txt_TravelControl_Name[i])) continue;
                var item = new EyouSoft.Model.TourStructure.MTourOrderTraveller();

                item.Birthday = Utils.GetDateTimeNullable(txt_TravelControl_Brithday[i]);
                item.CardId = txt_TravelControl_IDCard[i];
                item.CardNumber = txt_TravelControl_CardNum[i];
                item.CardType = (EyouSoft.Model.EnumType.TourStructure.CardType?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.TourStructure.CardType), slt_TravelControl_ProveType[i]);
                item.CardValidDate = txt_TravelControl_EffectTime[i];
                item.CnName = txt_TravelControl_Name[i];
                item.Contact = txt_TravelControl_Phone[i];
                item.EnName = string.Empty;
                item.Gender = Utils.GetEnumValue<EyouSoft.Model.EnumType.GovStructure.Gender>(slt_TravelControl_UserSex[i], EyouSoft.Model.EnumType.GovStructure.Gender.男);
                item.IsCardTransact = false;
                item.LNotice = cbx_TravelControl_LeaveMsg[i] == "1";
                item.OrderId = string.Empty;
                item.RAmount = 0;
                item.RAmountRemark = string.Empty;
                item.Remark = txt_TravelControl_Remarks[i];
                item.RNotice = cbx_TravelControl_BackMsg[i] == "1";
                item.RRemark = string.Empty;
                item.RTime = null;
                item.TourId = "" ;
                item.TravellerId = txt_TravelControl_TreavelID[i];
                item.TravellerStatus = EyouSoft.Model.EnumType.TourStructure.TravellerStatus.在团;
                item.VisaStatus = EyouSoft.Model.EnumType.TourStructure.VisaStatus.材料收集中;
                item.VisitorType = Utils.GetEnumValue<EyouSoft.Model.EnumType.TourStructure.VisitorType>(slt_TravelControl_PeopleType[i], EyouSoft.Model.EnumType.TourStructure.VisitorType.成人);

                items.Add(item);
            }

            return items;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_YouKes != null && _YouKes.Count > 0)
            {
                this.rptList.DataSource = _YouKes;
                this.rptList.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
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
    }
}