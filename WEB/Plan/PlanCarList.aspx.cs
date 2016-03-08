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
    /// 计调中心-用车安排
    /// </summary>
    public partial class PlanCarList : EyouSoft.Common.Page.BackPage
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
        string TourId = string.Empty;
        string PlanId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            PlanId = Utils.GetQueryStringValue("planid");
            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            InitPrivs();

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.用车确认单);

            string doType = Utils.GetQueryStringValue("action");

            switch (doType)
            {
                case "save":
                    Response.Clear();
                    Response.Write(PageSave());
                    Response.End();
                    break;
                case "delete": Delete(); break;
                case "getdata":
                    Response.Clear();
                    Response.Write("{\"tolist\":" + GetCarType(Utils.GetQueryStringValue("suppId")) + "}");
                    Response.End();
                    break;
                case "getprice":
                    Response.Clear();
                    Response.Write(GetPriceList(Utils.GetQueryStringValue("suppId"), Utils.GetQueryStringValue("rid")));
                    Response.End();
                    break;
                default: break;
            }

            InitInfo();
            InitRpt();
        }

        #region 获取车型
        /// <summary>
        /// 异步获取车型
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetCarType(string suppId)
        {
            EyouSoft.Model.HGysStructure.MGysInfo carModel = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(suppId);
            if (carModel != null && carModel.CheXings != null && carModel.CheXings.Count > 0)
            {
                string CarModelList = string.Empty;
                CarModelList += "[";
                for (int i = 0; i < carModel.CheXings.Count; i++)
                {
                    CarModelList += "{\"id\":\"" + carModel.CheXings[i].CheXingId + "\",\"text\":\"" + carModel.CheXings[i].Name + "\"},";
                }
                CarModelList = CarModelList.TrimEnd(',');
                CarModelList += "]";
                return CarModelList;
            }
            return "[{id:\"\",text:\"\"}]";
        }

        /// <summary>
        /// 异步获取车型价格
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetPriceList(string suppId, string rid)
        {
            IList<EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo> list = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCheXingJiaGes(rid);
            if (list != null && list.Count > 0)
            {
                return "{\"tolist\":" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + "}";
            }
            else
            {
                return "{\"tolist\":\"\"}";
            }
        }

        /// <summary>
        /// 获取车型
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetCarTypeList(string CarId)
        {
            var sbCarType = new StringBuilder();
            string suppId = Utils.GetQueryStringValue("suppId");
            if (!string.IsNullOrEmpty(suppId))
            {
                EyouSoft.Model.HGysStructure.MGysInfo carModel = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(suppId);
                if (carModel != null && carModel.CheXings != null && carModel.CheXings.Count > 0)
                {
                    for (int i = 0; i < carModel.CheXings.Count; i++)
                    {
                        sbCarType.AppendFormat("<option value='{0},{1}' " + (carModel.CheXings[i].CheXingId == CarId.Trim() ? "selected='selected'" : "") + " >{1}</option>", carModel.CheXings[i].CheXingId, carModel.CheXings[i].Name);
                    }
                }
            }
            return sbCarType.ToString();
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
            //车队
            string CarName = Utils.GetFormValue(this.SupplierControl1.ClientText);
            string CarId = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            //联系人 联系电话 联系传真
            string contectName = Utils.GetFormValue(this.txtContectName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContectPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContectFax.UniqueID);
            //结算费用
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txttotalMoney.UniqueID));
            //签单数
            string signingCount = Utils.GetFormValue(this.txtSigningCount.UniqueID);
            //导游需知 备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherRemark.UniqueID);
            //线路类型
            string routeType = Utils.GetFormValue("radshipType");
            //常规线路 
            string[] roomTypeList = Utils.GetFormValues("selRoomTypeList");
            string[] carNumber = Utils.GetFormValues("txtCarNumber");
            string[] dirverName = Utils.GetFormValues("txtDirverName");
            string[] dirverPhone = Utils.GetFormValues("txDirverPhone");
            string[] carPriceCG = Utils.GetFormValues("txtCarPriceCG");
            string[] daysCG = Utils.GetFormValues("txtDaysCG");
            string[] totalPriceCG = Utils.GetFormValues("txtTotalPriceCG");
            string[] sheWaiBeiZhu = Utils.GetFormValues("txtSheWaiBeiZhu");
            //特殊线路
            string[] roomTypeListTS = Utils.GetFormValues("selRoomTypeListTS");
            string[] carNumberTS = Utils.GetFormValues("txtCarNumberTS");
            string[] dirverNameTS = Utils.GetFormValues("txtDirverNameTS");
            string[] dirverPhoneTS = Utils.GetFormValues("txDirverPhoneTS");
            string[] carPriceTS = Utils.GetFormValues("txtCarPriceTS");
            string[] daysTS = Utils.GetFormValues("txtDaysTS");
            string[] bridgePrice = Utils.GetFormValues("txtBridgePrice");
            string[] driverPrice = Utils.GetFormValues("txtDriverPrice");
            string[] driverRoomPrice = Utils.GetFormValues("txtDriverRoomPrice");
            string[] driverDiningPrice = Utils.GetFormValues("txtDriverDiningPrice");
            string[] emptyDrivingPrice = Utils.GetFormValues("txtEmptyDrivingPrice");
            string[] otherPrice = Utils.GetFormValues("txtOtherPrice");
            string[] totalPriceTS = Utils.GetFormValues("txtTotalPriceTS");
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(CarId) && string.IsNullOrEmpty(CarName))
            {
                msg += "请选择车队!<br/>";
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
            baseinfo.SourceId = CarId;
            baseinfo.SourceName = CarName;
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车;
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.PlanCarList = new List<EyouSoft.Model.HPlanStructure.MPlanCar>();
            if (routeType.Trim() == "2")
            {
                for (int i = 0; i < carPriceTS.Length; i++)
                {
                    var car = new EyouSoft.Model.HPlanStructure.MPlanCar();
                    car.CarId = roomTypeListTS[i].Split(',')[0];
                    car.Models = roomTypeListTS[i].Split(',')[1];
                    car.CarNumber = carNumberTS[i];
                    car.Driver = dirverNameTS[i];
                    car.DriverPhone = dirverPhoneTS[i];
                    car.CarPrice = Utils.GetDecimal(carPriceTS[i]);
                    car.Days = Utils.GetInt(daysTS[i]);
                    car.BridgePrice = Utils.GetDecimal(bridgePrice[i]);
                    car.DriverPrice = Utils.GetDecimal(driverPrice[i]);
                    car.DriverRoomPrice = Utils.GetDecimal(driverRoomPrice[i]);
                    car.DriverDiningPrice = Utils.GetDecimal(driverDiningPrice[i]);
                    car.EmptyDrivingPrice = Utils.GetDecimal(emptyDrivingPrice[i]);
                    car.OtherPrice = Utils.GetDecimal(otherPrice[i]);
                    car.SumPrice = Utils.GetDecimal(totalPriceTS[i]);
                    car.PriceType = (EyouSoft.Model.EnumType.PlanStructure.PlanCarPriceType)2;
                    baseinfo.PlanCarList.Add(car);
                    baseinfo.Num++;
                }
            }
            else
            {
                for (int i = 0; i < carPriceCG.Length; i++)
                {
                    var car = new EyouSoft.Model.HPlanStructure.MPlanCar();
                    car.CarId = roomTypeList[i].Split(',')[0];
                    car.Models = roomTypeList[i].Split(',')[1];
                    car.CarNumber = carNumber[i];
                    car.Driver = dirverName[i];
                    car.DriverPhone = dirverPhone[i];
                    car.CarPrice = Utils.GetDecimal(carPriceCG[i]);
                    car.Days = Utils.GetInt(daysCG[i]);
                    car.SumPrice = Utils.GetDecimal(totalPriceCG[i]);
                    car.Remark = sheWaiBeiZhu[i];
                    car.PriceType = (EyouSoft.Model.EnumType.PlanStructure.PlanCarPriceType)1;
                    baseinfo.PlanCarList.Add(car);
                    baseinfo.Num++;
                }
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
                for (int i = 0; i < baseinfo.PlanCarList.Count; i++)
                {
                    baseinfo.PlanCarList[i].PlanId = editid;
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

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排用车);
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(TourId, SiteUserInfo.UserId);
            if (ListPower) ListPower = panView.Visible = Privs_AnPai;

            if (string.IsNullOrEmpty(PlanId))
            {
                this.litDueToway.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.DueToway)), "-1", false);
                this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "-1", false);
                this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), "-1", false);
                return;
            }

            EyouSoft.Model.HPlanStructure.MPlanBaseInfo info = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, PlanId);
            if (info == null) RCWE("异常请求");

            this.SupplierControl1.HideID = info.SourceId;
            this.SupplierControl1.Name = info.SourceName;
            this.txtContectName.Text = info.ContactName;
            this.txtContectPhone.Text = info.ContactPhone;
            this.txtContectFax.Text = info.ContactFax;
            this.txttotalMoney.Text = Utils.FilterEndOfTheZeroDecimal(info.Confirmation);
            if (info.PlanCarList != null)
            {
                if (Utils.GetQueryStringValue("PriceType") == "2") 
                {
                    this.tabViewTS.Visible = false;
                    this.repPricesListTS.DataSource = info.PlanCarList;
                    this.repPricesListTS.DataBind();
                }
                else
                {
                    this.tabViewCG.Visible = false;
                    this.repPricesListCG.DataSource = info.PlanCarList;
                    this.repPricesListCG.DataBind();
                }
            }
            this.txtGuidNotes.Text = info.GuideNotes;
            this.txtOtherRemark.Text = info.Remarks;
            this.litDueToway.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.DueToway)), ((int)info.DueToway).ToString(), false);
            this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), ((int)info.PaymentType).ToString(), false);
            if (((int)info.PaymentType) == 3) this.txtSigningCount.Text = info.SigningCount.ToString();
            this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), ((int)info.Status).ToString(), false);
        }

        /// <summary>
        /// init rpt
        /// </summary>
        protected void InitRpt()
        {
            var items = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, null,null, false, null, TourId);
            if (items != null && items.Count > 0)
            {
                this.repCarList.DataSource = items;
                this.repCarList.DataBind();
            }
            else
            {
                this.phdShowList.Visible = false;
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        void Delete()
        {
            string planId = Utils.GetQueryStringValue("planId");

            if (!Privs_AnPai) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));

            if (string.IsNullOrEmpty(planId)) RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));

            bool bllRetCode = new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planId);

            if (bllRetCode) RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功!"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败!"));
        }
        #endregion
    }
}
