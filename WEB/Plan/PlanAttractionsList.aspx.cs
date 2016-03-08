using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Plan
{
    /// <summary>
    /// 计调中心-景点安排
    /// </summary>
    public partial class PlanAttractionsList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //确认单
        protected string querenUrl = string.Empty;
        /// <summary>
        /// 列表操作
        /// </summary>
        protected bool ListPower = false;
        /// <summary>
        /// 安排权限
        /// </summary>
        bool Privs_AnPai = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.景点确认单);

            PageInit();

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            if (doType != "")
            {
                //存在ajax请求
                switch (doType)
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                    case "delete":
                        Response.Clear();
                        Response.Write(DelOperat());
                        Response.End();
                        break;
                    case "update":
                        GetModel();
                        break;
                    case "getdata":
                        Response.Clear();
                        Response.Write("{\"tolist\":" + GetSelList(Utils.GetQueryStringValue("suppId")) + "}");
                        Response.End();
                        break;
                    case "getprice":
                        Response.Clear();
                        Response.Write(GetPriceList(Utils.GetQueryStringValue("suppId"), Utils.GetQueryStringValue("rid"), Utils.GetQueryStringValue("tourMode")));
                        Response.End();
                        break;
                    case "quanbuluoshi":
                        Response.Clear();
                        this.QuanBuLuoShi((EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(Utils.GetQueryStringValue("type")), Utils.GetQueryStringValue("tourid"));
                        Response.End();
                        break;
                    default: break;
                }
            }
            #endregion
        }


        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排景点);
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                //支付方式
                this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "-1", false);
                //状态
                this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), "-1", false);

                IList<EyouSoft.Model.HPlanStructure.MPlan> List = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, null,null, false, null, tourId);
                if (List != null && List.Count > 0)
                {
                    this.repList.DataSource = List;
                    this.repList.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }

                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;
            }
            else
            {
                RCWE("异常请求");
            }
        }
        #endregion

        #region 获取景点
        /// <summary>
        /// 异步获取景点
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetSelList(string suppId)
        {
            EyouSoft.Model.HGysStructure.MGysInfo model = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(suppId);
            if (model != null && model.JingDians != null && model.JingDians.Count > 0)
            {
                string modelList = string.Empty;
                modelList += "[";
                for (int i = 0; i < model.JingDians.Count; i++)
                {
                    modelList += "{\"id\":\"" + model.JingDians[i].JingDianId + "\",\"text\":\"" + model.JingDians[i].Name + "\"},";
                }
                modelList = modelList.TrimEnd(',');
                modelList += "]";
                return modelList;
            }
            return "[{id:\"\",text:\"\"}]";
        }
        /// <summary>
        /// 异步获取景点价格
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetPriceList(string suppId, string rid, string tourMode)
        {
            IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> list = new EyouSoft.BLL.HGysStructure.BJiaGe().GetJingDianJiaGes(rid);
            if (list != null && list.Count > 0)
            {
                if (!string.IsNullOrEmpty(tourMode))
                {
                    if (tourMode.Trim() == "2")
                    {
                        list = list.Where(c => c.TuanXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaTuanXing.团).ToList();
                    }
                    else
                    {
                        list = list.Where(c => c.TuanXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaTuanXing.散).ToList();
                    }
                }
                return "{\"tolist\":" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + "}";
            }
            else
            {
                return "{\"tolist\":\"\"}";
            }
            //return "{\"tolist\":" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + ",\"name\":\"" + tourMode + "\"}";
        }

        /// <summary>
        /// 获取景点
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetList(string id)
        {
            var sb = new StringBuilder();
            string suppId = Utils.GetQueryStringValue("suppId");
            if (!string.IsNullOrEmpty(suppId))
            {
                EyouSoft.Model.HGysStructure.MGysInfo model = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(suppId);
                if (model != null && model.JingDians != null && model.JingDians.Count > 0)
                {
                    for (int i = 0; i < model.JingDians.Count; i++)
                    {
                        sb.AppendFormat("<option value='{0},{1}' " + (model.JingDians[i].JingDianId == id.Trim() ? "selected='selected'" : "") + " >{1}</option>", model.JingDians[i].JingDianId, model.JingDians[i].Name);
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 删除已安排的计调项
        /// <summary>
        /// 删除已安排的计调项
        /// </summary>
        /// <returns></returns>
        protected string DelOperat()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(planId))
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planId))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                }
            }
            return msg;
        }
        #endregion

        #region 获取计调项实体
        /// <summary>
        /// 获取计调项实体
        /// </summary>
        protected void GetModel()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, planId);
                if (baseinfo != null)
                {
                    this.SupplierControl1.HideID = baseinfo.SourceId;
                    this.SupplierControl1.Name = baseinfo.SourceName;
                    this.txtContentName.Text = baseinfo.ContactName;
                    this.txtContentPhone.Text = baseinfo.ContactPhone;
                    this.txtContentFax.Text = baseinfo.ContactFax;
                    if (baseinfo.PlanAttractionsList != null)
                    {
                        this.tabHolderView.Visible = false;
                        this.repAPList.DataSource = baseinfo.PlanAttractionsList;
                        this.repAPList.DataBind();
                    }
                    else
                    {
                        this.tabHolderView.Visible = true;
                    }
                    this.txtCostAccount.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    this.txtGuidNotes.Text = baseinfo.GuideNotes;
                    this.txtOtherMark.Text = baseinfo.Remarks;
                    //支付方式
                    this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), ((int)baseinfo.PaymentType).ToString(), false);
                    if (((int)baseinfo.PaymentType) == 3)
                    {
                        this.txtSigningCount.Text = baseinfo.SigningCount.ToString();
                    }
                    //状态
                    this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), ((int)baseinfo.Status).ToString(), false);
                }
            }
        }
        #endregion

        #region 全部落实
        void QuanBuLuoShi(EyouSoft.Model.EnumType.PlanStructure.PlanProject typ,string tourid)
        {
            switch (new EyouSoft.BLL.HPlanStructure.BPlan().DoGlobal(tourid,EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实,EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点))
            {
                case 0:
                    Context.Response.Write("景点全部落实失败！");
                    break;
                case 1:
                    Context.Response.Write("景点全部落实成功！");
                    break;
            }
        }
        #endregion

        public string GetAPMX(object list)
        {
            StringBuilder str = new StringBuilder();
            if (list != null)
            {
                var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanAttractions>)list;
                for (int i = 0; i < lis.Count; i++)
                {
                    str.AppendFormat("{0}:{1}/{2}+{3}/{4}+{5}/{6}<br/>", lis[i].VisitTime.Value.ToString("yyyy-MM-dd"), lis[i].Attractions, lis[i].AdultNumber, lis[i].ChildNumber, lis[i].AdultPrice.ToString("C2"), lis[i].ChildPrice.ToString("C2"), lis[i].SumPrice.ToString("C2"));
                }
            }
            return str.ToString();
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected string PageSave()
        {
            #region 表单赋值
            string msg = string.Empty;
            string seterrorMsg = string.Empty;
            //车队
            string Name = Utils.GetFormValue(this.SupplierControl1.ClientText);
            string Id = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            //联系人 联系电话 联系传真
            string contectName = Utils.GetFormValue(this.txtContentName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContentPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContentFax.UniqueID);
            //结算费用
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtCostAccount.UniqueID));
            //签单数
            string signingCount = Utils.GetFormValue(this.txtSigningCount.UniqueID);
            //导游需知 备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherMark.UniqueID);
            //安排信息 
            string[] selList = Utils.GetFormValues("selList");
            string[] txtvisitTime = Utils.GetFormValues("txtvisitTime");
            string[] txtadultNums = Utils.GetFormValues("txtadultNums");
            string[] txtchildNums = Utils.GetFormValues("txtchildNums");
            string[] txtadultPrices = Utils.GetFormValues("txtadultPrices");
            string[] txtchildPrices = Utils.GetFormValues("txtchildPrices");
            string[] txtseats = Utils.GetFormValues("txtseats");
            string[] txtbeiZhu = Utils.GetFormValues("txtbeiZhu");
            string[] txtXiaoJi = Utils.GetFormValues("txtXiaoJi");
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(Name))
            {
                msg += "请选择景点公司名称!<br/>";
            }
            if (totalMoney <= 0)
            {
                msg += "请填写结算费用!<br/>";
            }

            if (string.IsNullOrEmpty(Utils.GetFormValue("states")))
            {
                msg += "请选择状态！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("panyMent")))
            {
                msg += "请选择支付方式！<br/>";
            }
            if (msg != "")
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return seterrorMsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.HPlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.IsDuePlan = false;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalMoney;
            baseinfo.PlanCost = totalMoney;
            baseinfo.ContactFax = contectFax;
            baseinfo.ContactName = contectName;
            baseinfo.ContactPhone = contectPhone;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.Num = 0;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("panyMent"));
            baseinfo.SigningCount = Utils.GetInt(Utils.GetFormValue("panyMent")) == 3 ? Utils.GetInt(signingCount) : 0;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("states"));
            baseinfo.SourceId = Id;
            baseinfo.SourceName = Name;
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点;
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.PlanAttractionsList = new List<EyouSoft.Model.HPlanStructure.MPlanAttractions>();
            for (int i = 0; i < selList.Length; i++)
            {
                var mod = new EyouSoft.Model.HPlanStructure.MPlanAttractions();
                mod.AttractionsId = selList[i].Split(',')[0];
                mod.Attractions = selList[i].Split(',')[1];
                mod.VisitTime = Utils.GetDateTimeNullable(txtvisitTime[i]);
                mod.AdultNumber = Utils.GetInt(txtadultNums[i]);
                mod.ChildNumber = Utils.GetInt(txtchildNums[i]);
                mod.AdultPrice = Utils.GetDecimal(txtadultPrices[i]);
                mod.ChildPrice = Utils.GetDecimal(txtchildPrices[i]);
                mod.Seats = txtseats[i];
                mod.SumPrice = Utils.GetDecimal(txtXiaoJi[i]);
                mod.BeiZhu = txtbeiZhu[i];
                baseinfo.PlanAttractionsList.Add(mod);
                baseinfo.Num += (mod.AdultNumber + mod.ChildNumber);
                baseinfo.AdultNumber += mod.AdultNumber;
                baseinfo.ChildNumber += mod.ChildNumber;
            }
            baseinfo.Remarks = otherRemark;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorDeptId = this.SiteUserInfo.DeptId;
            baseinfo.Operator = this.SiteUserInfo.Name;
            #endregion

            #region 提交操作
            string editid = Utils.GetQueryStringValue("planId");
            EyouSoft.BLL.HPlanStructure.BPlan bll = new EyouSoft.BLL.HPlanStructure.BPlan();
            int result = 0;
            if (editid != "" && editid != null)
            {
                baseinfo.PlanId = editid;
                for (int i = 0; i < baseinfo.PlanAttractionsList.Count; i++)
                {
                    baseinfo.PlanAttractionsList[i].PlanId = editid;
                }
                result = bll.UpdPlan(baseinfo);
                if (result == 1)
                {
                    msg += "修改成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                result = bll.AddPlan(baseinfo);
                if (result == 1)
                {
                    msg += "添加成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "添加失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,添加失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            #endregion

            return seterrorMsg;
        }
        #endregion
    }
}
