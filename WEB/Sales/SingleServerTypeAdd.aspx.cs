using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.EnumType.PlanStructure;
using System.Text;
namespace EyouSoft.Web.Sales
{
    public partial class SingleServerTypeAdd : BackPage
    {
        #region attributes
        //支付方式
        protected string panyMent = string.Empty;
        //状态
        protected string states = string.Empty;
        /// <summary>
        /// 列表操作
        /// </summary>
        protected bool ListPower = false;
        /// <summary>
        /// 安排权限
        /// </summary>
        bool Privs_AnPai = false;
        /// <summary>
        /// 计划编号
        /// </summary>
        string TourId = string.Empty;
        bool isCanOPT = false;
        int ObjType = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            if (Utils.GetQueryStringValue("doType").Length > 0)
            {
                if (Utils.GetQueryStringValue("doType") != "Delete")
                {
                    PageSave();
                }
                else
                {
                    deleteByPlanId();
                }
            }
            DataInit();
        }






        #region private members
        /// <summary>
        /// 绑定安排的地接计调项
        /// </summary>
        /// <param name="tourId">团号</param>
        void DataInit()
        {
            ListPower = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(TourId, SiteUserInfo.UserId);
            //if (ListPower) ListPower = panView.Visible = Privs_AnPai;
            ObjType = Utils.GetInt(Utils.GetQueryStringValue("tp"));
            TourId = Utils.GetQueryStringValue("tourid");
            var model = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(TourId);
            if (model != null)
            {
                isCanOPT = (int)model.TourStatus < 6 ? true : false;
            }

            IList<EyouSoft.Model.HPlanStructure.MPlan> AyencyList = new EyouSoft.BLL.HPlanStructure.BPlan().GetList((EyouSoft.Model.EnumType.PlanStructure.PlanProject)ObjType, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, TourId);


            if (AyencyList != null && AyencyList.Count > 0)
            {
                this.repAycentylist.DataSource = AyencyList;
                this.repAycentylist.DataBind();
            }


        }


        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(TourId);

            //switch (tourType)
            //{
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
            //        Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排地接);
            //        break;
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
            //        Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排地接);
            //        break;
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
            //        Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排地接);
            //        break;
            //}
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        void PageSave()
        {
            var model = getModel();
            EyouSoft.BLL.HPlanStructure.BPlan bll = new EyouSoft.BLL.HPlanStructure.BPlan();

            if (model.PlanId == "")
            {
                int i = bll.AddPlan(model);
                if (i == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "添加成功"));
                RCWE(UtilsCommons.AjaxReturnJson("0", "添加失败"));

            }
            else
            {
                int i = bll.UpdPlan(model);
                if (i == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                RCWE(UtilsCommons.AjaxReturnJson("0", "修改失败"));

            }



        }

        /// <summary>
        /// 绑定支付状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string getPayState(int state)
        {
            IList<EyouSoft.Model.ComStructure.MComPayment> ls = new EyouSoft.BLL.ComStructure.BComPayment().GetList(SiteUserInfo.CompanyId);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ls)
                {
                    if (item.PaymentId == state)
                    {
                        sb.Append("<option  selected=\"selected\"  value=" + item.PaymentId + ">" + item.Name + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value=" + item.PaymentId + ">" + item.Name + "</option>");
                    }
                }
                return sb.ToString();
            }
            return "<option value=-1>-无支付方式-</option>";
        }
        /// <summary>
        /// 计调状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string getPlanState(EyouSoft.Model.EnumType.PlanStructure.PlanState state)
        {

            return UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState)).Where(t => t.Text != EyouSoft.Model.EnumType.PlanStructure.PlanState.待确认.ToString() && t.Text != EyouSoft.Model.EnumType.PlanStructure.PlanState.无计调任务.ToString() && t.Text != EyouSoft.Model.EnumType.PlanStructure.PlanState.未安排.ToString()).ToList(), (int)state);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HPlanStructure.MPlanBaseInfo getModel()
        {
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo model = new EyouSoft.Model.HPlanStructure.MPlanBaseInfo();


            model.TourId = Utils.GetQueryStringValue("tourid");
            model.CompanyId = SiteUserInfo.CompanyId;
            model.Operator = SiteUserInfo.Username;
            model.OperatorId = SiteUserInfo.UserId;
            model.Type = (PlanProject)Utils.GetInt(Utils.GetQueryStringValue("tp"));
            model.GuideNotes = Utils.GetFormValue("txtinfo");
            //model.PlanCost = Utils.GetDecimal(Utils.GetFormValue("txtcost"));
            model.Remarks = Utils.GetFormValue("txtremark");
            model.SourceName = Request.Form[CustomerUnitSelect1.ClientNameKHMC] ?? Utils.GetFormValue("txt_" + Utils.GetFormValue("sellsFormKey") + CustomerUnitSelect1.ClientNameKHMC.Substring(4));
            //Utils.GetFormValue(CustomerUnitSelect1.ClientNameKHMC) == "" ? Utils.GetFormValue("hidsourNM") : "";
            model.Confirmation = Utils.GetDecimal(Utils.GetFormValue("txtcost"));
            model.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)(Utils.GetInt(Utils.GetFormValue("payStateDDL")));
            model.Num = Utils.GetInt(Utils.GetFormValue("txtnum"));


            model.AddStatus = PlanAddStatus.计调安排时添加;
            // model.ContactName = Utils.GetFormValue("");
            // model.ContactPhone = Utils.GetFormValue("");
            //model.ContactFax = Utils.GetFormValue("");
            model.SourceId = Request.Form[CustomerUnitSelect1.ClientNameKHBH] ?? Utils.GetFormValue("hd_" + Utils.GetFormValue("sellsFormKey") + CustomerUnitSelect1.ClientNameKHBH.Substring(3));
            model.PlanId = Utils.GetFormValue("PLanId");
            model.Status = (PlanState)Utils.GetInt(Utils.GetFormValue("planStateDDL"));
            return model;
        }


        void deleteByPlanId()
        {
            bool result = new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(Utils.GetFormValue("planID"));
            AjaxResponse(UtilsCommons.AjaxReturnJson(result ? "1" : "-1", result ? "删除成功!" : "删除失败!"));
        }

        protected string GetPrintURL(string id)
        {
            string str = string.Empty;
            EyouSoft.Model.EnumType.PlanStructure.PlanProject tp = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)ObjType;

            switch (tp)
            {
                case PlanProject.酒店:
                    //订房
                    str = "/PrintPage/singledingfangdan.aspx?planId=" + id;
                    break;
                case PlanProject.用车:
                    str = "/PrintPage/singleyongche.aspx?planId=" + id;
                    break;
                case PlanProject.景点:
                    str = "/PrintPage/singlejingdian.aspx?planId=" + id;
                    break;
                //case PlanProject.导游:
                //    break;
                //case PlanProject.地接:
                //    break;
                case PlanProject.用餐:
                    str = "/PrintPage/singlecanguan.aspx?planId=" + id;
                    break;
                //case PlanProject.购物:
                //    break;
                //case PlanProject.领料:
                //    break;
                case PlanProject.飞机:
                    str = "/PrintPage/singlefeiji.aspx?planId=" + id;
                    break;
                case PlanProject.火车:
                    str = "/PrintPage/singlehuoche.aspx?planId=" + id;
                    break;
                //case PlanProject.汽车:
                //    break;
                //case PlanProject.轮船:
                //    break;
                case PlanProject.其它:
                    str = "/PrintPage/singleqita.aspx?planId=" + id;
                    break;
                default:
                    break;
            }

            return str;
        }

        protected string GetOPThtml(string pid)
        {
            StringBuilder strbu = new StringBuilder();

            if (isCanOPT)
            {
                strbu.AppendFormat("<a class=\"a_Updata\" href=\"javascript:void(0);\" data-class=\"updateAyency\"><img src=\"/images/y-delicon.gif\" border=\"0\" alt=\"\" data-id=\"{0}\" />修改</a>&nbsp;", pid);
                strbu.AppendFormat("<a class=\"a_Delete\" href=\"javascript:void(0);\" data-class=\"delAyency\"><img src=\"/images/y-delicon.gif\" border=\"0\" alt=\"\" data-id=\"{0}\" />删除</a>", pid);
            }
            else
            {
                strbu.AppendFormat("修改&nbsp;");
                strbu.AppendFormat("删除&nbsp;  ");

            }


            return strbu.ToString();
        }

        #endregion
    }
}
