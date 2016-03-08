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
    /// 计调中心-用餐安排
    /// </summary>
    public partial class PlanDiningList : EyouSoft.Common.Page.BackPage
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

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.订餐确认单);

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
                        Response.Write(DelOperatCar());
                        Response.End();
                        break;
                    case "update":
                        GetCarModel();
                        break;
                    case "getdata":
                        Response.Clear();
                        Response.Write("{\"tolist\":" + GetCarType(Utils.GetQueryStringValue("suppId"), Utils.GetQueryStringValue("rid")) + "}");
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
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排用餐);
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
                //预定方式
                this.litDueToway.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.DueToway)), "-1", false);
                //支付方式
                this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "-1", false);
                //状态
                this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), "-1", false);

                IList<EyouSoft.Model.HPlanStructure.MPlan> List = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, null, null, false, null, tourId);
                if (List != null && List.Count > 0)
                {
                    this.repCarList.DataSource = List;
                    this.repCarList.DataBind();
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

        #region 获取菜单，用餐类型
        /// <summary>
        /// 异步获取菜单
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetCarType(string suppId, string rid)
        {
            IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> list = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCanGuanCaiDans(suppId);
            if (list != null && list.Count > 0)
            {
                if (!string.IsNullOrEmpty(rid))
                {
                    list = list.Where(c => c.CaiDanId == rid).ToList();
                }
                string pri = !string.IsNullOrEmpty(Utils.GetQueryStringValue("type")) ? Utils.GetQueryStringValue("type") : "ren";
                string CarModelList = string.Empty;
                CarModelList += "[";
                for (int i = 0; i < list.Count; i++)
                {
                    CarModelList += "{\"id\":\"" + list[i].CaiDanId + "\",\"text\":\"" + list[i].Name + "\",\"con\":\"" + Utils.GetText2(list[i].NeiRong, 10, true) + "\",\"js\":\"" + Utils.FilterEndOfTheZeroDecimal(pri == "ren" ? list[i].JiaGeRJS : list[i].JiaGeZJS) + "\",\"js0\":\"" + (pri == "ren" ? list[i].JiaGeRJS : list[i].JiaGeZJS).ToString("C2") + "\"},";
                }
                CarModelList = CarModelList.TrimEnd(',');
                CarModelList += "]";
                return CarModelList;
            }
            return "[{id:\"\",text:\"\",con:\"\",js:\"\",js0:\"\"}]";
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetCarTypeList(string Id)
        {
            var sbCarType = new StringBuilder();
            string suppId = Utils.GetQueryStringValue("suppId");
            if (!string.IsNullOrEmpty(suppId))
            {
                IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> list = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCanGuanCaiDans(suppId);
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        sbCarType.AppendFormat("<option value='{0},{1}' " + (list[i].CaiDanId == Id.Trim() ? "selected='selected'" : "") + " >{1}</option>", list[i].CaiDanId, list[i].Name);
                    }
                }
            }
            return sbCarType.ToString();
        }
        //用餐类型 早中晚
        protected string DiningTypeHtml(string Selected,string t)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<EnumObj> PassengerType = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanDiningType));
            if (PassengerType != null && PassengerType.Count > 0)
            {
                sb.Append("<select name=\"DiningType" + t + "\" class=\"inputselect\" >");
                for (int i = 0; i < PassengerType.Count; i++)
                {
                    if (PassengerType[i].Value == Selected)
                    {
                        sb.Append("<option value='" + PassengerType[i].Value + "' selected='selected'>" + PassengerType[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + PassengerType[i].Value + "'>" + PassengerType[i].Text + "</option>");
                    }
                }
                sb.Append("</select>");
            }
            return sb.ToString();
        }
        #endregion

        #region 删除已安排
        /// <summary>
        /// 删除已安排
        /// </summary>
        /// <returns></returns>
        protected string DelOperatCar()
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

        #region 获取实体
        /// <summary>
        /// 获取实体
        /// </summary>
        protected void GetCarModel()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, planId);
                if (baseinfo != null)
                {
                    this.SupplierControl1.HideID = baseinfo.SourceId;
                    this.SupplierControl1.Name = baseinfo.SourceName;
                    this.txtContectName.Text = baseinfo.ContactName;
                    this.txtContectPhone.Text = baseinfo.ContactPhone;
                    this.txtContectFax.Text = baseinfo.ContactFax;
                    if (baseinfo.PlanDiningList != null)
                    {
                        if (Utils.GetQueryStringValue("PriceType") == "2") //桌
                        {
                            this.tabViewTS.Visible = false;
                            this.repPricesListTS.DataSource = baseinfo.PlanDiningList;
                            this.repPricesListTS.DataBind();
                        }
                        else  //人
                        {
                            this.tabViewCG.Visible = false;
                            this.repPricesListCG.DataSource = baseinfo.PlanDiningList;
                            this.repPricesListCG.DataBind();
                        }
                    }
                    this.txttotalMoney.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    this.txtGuidNotes.Text = baseinfo.GuideNotes;
                    this.txtOtherRemark.Text = baseinfo.Remarks;
                    this.txtStartTime.Text = UtilsCommons.GetDateString(baseinfo.StartDate, ProviderToDate);
                    //预定方式
                    this.litDueToway.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.DueToway)), ((int)baseinfo.DueToway).ToString(), false);
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
            //餐馆
            string dName = Utils.GetFormValue(this.SupplierControl1.ClientText);
            string dId = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            //联系人 联系电话 联系传真 用餐时间
            string contectName = Utils.GetFormValue(this.txtContectName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContectPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContectFax.UniqueID);
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtStartTime.UniqueID));
            //结算费用
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txttotalMoney.UniqueID));
            //签单数
            string signingCount = Utils.GetFormValue(this.txtSigningCount.UniqueID);
            //导游需知 备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherRemark.UniqueID);
            //线路类型
            string routeType = Utils.GetFormValue("radshipType");
            //人 
            string[] selRoomTypeListR = Utils.GetFormValues("selRoomTypeListR");
            string[] txtAdultNumberR = Utils.GetFormValues("txtAdultNumberR");
            string[] txtChildNumberR = Utils.GetFormValues("txtChildNumberR");
            string[] txtLeaderNumberR = Utils.GetFormValues("txtLeaderNumberR");
            string[] txtGuideNumberR = Utils.GetFormValues("txtGuideNumberR");
            string[] txtDriverNumberR = Utils.GetFormValues("txtDriverNumberR");
            string[] txtAdultUnitPriceR = Utils.GetFormValues("txtAdultUnitPriceR");
            string[] txtChildPriceR = Utils.GetFormValues("txtChildPriceR");
            string[] DiningTypeR = Utils.GetFormValues("DiningTypeR");
            string[] txtTableNumberR = Utils.GetFormValues("txtTableNumberR");
            string[] txtFreeNumber = Utils.GetFormValues("txtFreeNumber");
            string[] txtFreePrice = Utils.GetFormValues("txtFreePrice");
            string[] txtTotalPriceR = Utils.GetFormValues("txtTotalPriceR");
            //桌
            string[] selRoomTypeListZ = Utils.GetFormValues("selRoomTypeListZ");
            string[] txtAdultNumberZ = Utils.GetFormValues("txtAdultNumberZ");
            string[] txtChildNumberZ = Utils.GetFormValues("txtChildNumberZ");
            string[] txtLeaderNumberZ = Utils.GetFormValues("txtLeaderNumberZ");
            string[] txtGuideNumberZ = Utils.GetFormValues("txtGuideNumberZ");
            string[] txtDriverNumberZ = Utils.GetFormValues("txtDriverNumberZ");
            string[] txtAdultUnitPriceZ = Utils.GetFormValues("txtAdultUnitPriceZ");
            string[] DiningTypeZ = Utils.GetFormValues("DiningTypeZ");
            string[] txtTableNumberZ = Utils.GetFormValues("txtTableNumberZ");
            string[] txtTotalPriceZ = Utils.GetFormValues("txtTotalPriceZ");
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(dId) && string.IsNullOrEmpty(dName))
            {
                msg += "请选择餐厅!<br/>";
            }
            if (string.IsNullOrEmpty(startTime.ToString()))
            {
                msg += "请填写用餐时间!<br/>";
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
            baseinfo.DueToway = (EyouSoft.Model.EnumType.PlanStructure.DueToway)Utils.GetInt(Utils.GetFormValue("dueToway"));
            baseinfo.SourceId = dId;
            baseinfo.SourceName = dName;
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐;
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.PlanDiningList = new List<EyouSoft.Model.HPlanStructure.MPlanDining>();
            if (routeType.Trim() == "2")
            {
                for (int i = 0; i < txtTotalPriceZ.Length; i++)
                {
                    var pd = new EyouSoft.Model.HPlanStructure.MPlanDining();
                    pd.MenuId = selRoomTypeListZ[i].Split(',')[0];
                    pd.MenuName = selRoomTypeListZ[i].Split(',')[1];
                    pd.AdultNumber = Utils.GetInt(txtAdultNumberZ[i]);
                    pd.ChildNumber = Utils.GetInt(txtChildNumberZ[i]);
                    pd.LeaderNumber = Utils.GetInt(txtLeaderNumberZ[i]);
                    pd.GuideNumber = Utils.GetInt(txtGuideNumberZ[i]);
                    pd.DriverNumber = Utils.GetInt(txtDriverNumberZ[i]);
                    pd.AdultUnitPrice = Utils.GetDecimal(txtAdultUnitPriceZ[i]);
                    pd.TableNumber = Utils.GetInt(txtTableNumberZ[i]);
                    pd.DiningType = (EyouSoft.Model.EnumType.PlanStructure.PlanDiningType)Utils.GetInt(DiningTypeZ[i]);
                    pd.SumPrice = Utils.GetDecimal(txtTotalPriceZ[i]);
                    pd.PriceType = (EyouSoft.Model.EnumType.PlanStructure.PlanDiningPriceType)2;
                    baseinfo.PlanDiningList.Add(pd);
                    baseinfo.Num += (pd.AdultNumber + pd.ChildNumber);
                }
            }
            else
            {
                for (int i = 0; i < txtTotalPriceR.Length; i++)
                {
                    var pd = new EyouSoft.Model.HPlanStructure.MPlanDining();
                    pd.MenuId = selRoomTypeListR[i].Split(',')[0];
                    pd.MenuName = selRoomTypeListR[i].Split(',')[1];
                    pd.AdultNumber = Utils.GetInt(txtAdultNumberR[i]);
                    pd.ChildNumber = Utils.GetInt(txtChildNumberR[i]);
                    pd.LeaderNumber = Utils.GetInt(txtLeaderNumberR[i]);
                    pd.GuideNumber = Utils.GetInt(txtGuideNumberR[i]);
                    pd.DriverNumber = Utils.GetInt(txtDriverNumberR[i]);
                    pd.AdultUnitPrice = Utils.GetDecimal(txtAdultUnitPriceR[i]);
                    pd.ChildPrice = Utils.GetDecimal(txtChildPriceR[i]);
                    pd.TableNumber = Utils.GetInt(txtTableNumberR[i]);
                    pd.DiningType = (EyouSoft.Model.EnumType.PlanStructure.PlanDiningType)Utils.GetInt(DiningTypeR[i]);
                    pd.SumPrice = Utils.GetDecimal(txtTotalPriceR[i]);
                    pd.FreeNumber = Utils.GetInt(txtFreeNumber[i]);
                    pd.FreePrice = Utils.GetDecimal(txtFreePrice[i]);
                    pd.PriceType = (EyouSoft.Model.EnumType.PlanStructure.PlanDiningPriceType)1;
                    baseinfo.PlanDiningList.Add(pd);
                    baseinfo.Num += (pd.AdultNumber + pd.ChildNumber);
                }
            }
            baseinfo.StartDate = startTime;
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
                for (int i = 0; i < baseinfo.PlanDiningList.Count; i++)
                {
                    baseinfo.PlanDiningList[i].PlanId = editid;
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