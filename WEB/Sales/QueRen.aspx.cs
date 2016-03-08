using System;

namespace EyouSoft.Web.Sales
{
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.TourStructure;

    public partial class QueRen : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetFormValue("doType");
            if (doType != "" && doType == "Save")
            {
                this.Save();
            } 
            this.DataInit();
        }

        void DataInit()
        {
            var m = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(Utils.GetQueryStringValue("tourid"));
            if (m!=null)
            {
                this.txtAdult.Value = m.Adults.ToString();
                this.txtChild.Value = m.Childs.ToString();
                this.txtLeader.Value = m.Leaders.ToString();
                this.txtAmout.Value = m.SalerIncome.ToString("F2");
                this.txtNeiBu.Value = m.InsideInformation;
            }
        }

        void Save()
        {
            switch (new BLL.HTourStructure.BTour().TourSure(this.GetFormInfo()))
            {
                case 0:
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("false", "确认失败！"));
                    break;
                case 1:
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("true", "确认成功！"));
                    break;
                case -1:
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("false", "该计划已确认！"));
                    break;
            }
        }

        EyouSoft.Model.HTourStructure.MTourSure GetFormInfo()
        {
            var m = new EyouSoft.Model.HTourStructure.MTourSure()
                {
                    TourId = Utils.GetFormValue("tourid"),
                    Adults = Utils.GetInt(Utils.GetFormValue("txtadult")),
                    Childs = Utils.GetInt(Utils.GetFormValue("txtchild")),
                    Leaders = Utils.GetInt(Utils.GetFormValue("txtleader")),
                    SalerIncome = Utils.GetDecimal(Utils.GetFormValue("txtAmout")),
                    InsideInformation = Utils.GetFormValue("txtneibu"),
                    TourSureStatus = TourSureStatus.已确认
                };
            return m;
        }
    }
}
