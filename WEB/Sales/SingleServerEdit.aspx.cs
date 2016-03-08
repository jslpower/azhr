using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.TourStructure;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.Common.Page;
using System.Text;
using EyouSoft.Model.CrmStructure;

namespace EyouSoft.WEB.Sales
{
    /// <summary>
    /// 单项业务
    /// 创建人：刘飞
    /// 时间：2011-10-21
    /// </summary>
    public partial class SingleServerEdit : BackPage
    {
        #region attributes
        protected string Col = string.Empty;
        private IList<MTourOrderTraveller> _setCustomList;
        /// <summary>
        /// 设置客户名单数据源
        /// </summary>
        private IList<MTourOrderTraveller> SetCustomList
        {
            get { return _setCustomList; }
            set { _setCustomList = value; }
        }
        protected int CustomListCount = 0;

        /// <summary>
        /// 设置客户要求数据源
        /// </summary>
        private IList<MTourTeamPrice> _setCustomrequire;
        private IList<MTourTeamPrice> SetCusttomrequire
        {
            get { return _setCustomrequire; }
            set { _setCustomrequire = value; }
        }
        protected int CustomRequireCount = 0;

        /// <summary>
        /// 设置供应商安排数据源
        /// </summary>
        private IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> _setSourcePlan;
        private IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> SetSourcePlan
        {
            get { return _setSourcePlan; }
            set { _setSourcePlan = value; }
        }
        protected int SourcePlanCount = 0;
        /// <summary>
        /// 计调项枚举
        /// </summary>
        protected List<EnumObj> EnumSource = null;
        /// <summary>
        /// 供应商安排权限
        /// </summary>
        protected bool Privs_JiDiaoAnPai = false;
        /// <summary>
        /// 新增权限
        /// </summary>
        protected bool Privs_Insert = false;
        /// <summary>
        /// 修改权限
        /// </summary>
        protected bool Privs_Update = false;

        string DanXiangYeWuId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.SellsSelect1.ClientDeptID = this.hideDeptID.ClientID;
            this.SellsSelect1.ClientDeptName = this.hideDeptName.ClientID;
            this.SellsSelect1.SellsID = this.SiteUserInfo.UserId;
            this.SellsSelect1.SellsName = this.SiteUserInfo.Name;
            this.SellsSelect1.CallBackFun = "sellCallBack";
            this.SellsSelect2.CallBackFun = "planerCallBack";

            this.CustomerUnitSelect1.CallBack = "CallBackFun";
            DanXiangYeWuId = Utils.GetQueryStringValue("id");
            string type = Utils.GetQueryStringValue("type");
            EnumSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanProject)).Where(m => m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料.ToString()
                && m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物.ToString()
                && m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船.ToString()
                && m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车.ToString()
                && m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接.ToString()
                && m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐.ToString()
                && m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游.ToString()).ToList();
            //EnumSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GysStructure.GysLeiXing)).Where(m => m.Text != EyouSoft.Model.EnumType.GysStructure.GysLeiXing.司机.ToString() && m.Text != EyouSoft.Model.EnumType.GysStructure.GysLeiXing.区间交通.ToString() && m.Text != EyouSoft.Model.EnumType.GysStructure.GysLeiXing.物品.ToString()).ToList();
            #region 处理AJAX请求
            switch (type)
            {
                case "save": BaoCun(); break;
            }
            #endregion

            //     EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕 

            //权限验证
            this.PowerControl();

            if (!IsPostBack)
            {
                InitInfo();
                SetSourcePlanList();
                SetCustomRequireList();
                SetCustomDataList();
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void InitInfo()
        {
            this.UploadControl1.CompanyID = this.SiteUserInfo.CompanyId;
            if (string.IsNullOrEmpty(DanXiangYeWuId))
            {
                this.txtOpeator.Text = this.SiteUserInfo.Name;
                this.txtTotalIn.ReadOnly = true;
                return;
            }

            EyouSoft.BLL.TourStructure.BSingleService bll = new EyouSoft.BLL.TourStructure.BSingleService();
            MSingleServiceExtend model = bll.GetSingleServiceExtendByTourId(DanXiangYeWuId);
            if (model == null) RCWE("异常请求");
            PlaceHolder1.Visible = true;
            this.txtWeiTuoRiQi.Text = model.WeiTuoRiQi.ToShortDateString();
            this.status.Value = ((int)model.TourStatus).ToString();
            this.txtContactName.Text = model.ContactName;
            this.txtContactTel.Text = model.ContactTel;
            this.CustomerUnitSelect1.CustomerUnitName = model.BuyCompanyName;
            this.CustomerUnitSelect1.CustomerUnitId = model.BuyCompanyId;
            this.SellsSelect1.SellsName = model.SellerName;
            this.SellsSelect1.SellsID = model.SellerId;
            this.SellsSelect2.SetTitle = "OP";
            if (model.TourPlanersList != null && model.TourPlanersList.Count > 0)
            {
                //this.SellsSelect2.SellsName = model.TourPlanersList[0].Planer;
                string planidlist = string.Empty;
                string plannamelist = string.Empty;
                string plandeptlist = string.Empty;
                for (int i = 0; i < model.TourPlanersList.Count; i++)
                {
                    if (i == model.TourPlanersList.Count - 1)
                    {
                        planidlist += model.TourPlanersList[i].PlanerId;
                        plannamelist += model.TourPlanersList[i].Planer;
                        plandeptlist += model.TourPlanersList[i].DeptId;
                    }
                    else
                    {
                        planidlist += model.TourPlanersList[i].PlanerId + ",";
                        plannamelist += model.TourPlanersList[i].Planer + ",";
                        plandeptlist += model.TourPlanersList[i].DeptId + ",";
                    }
                }
                this.SellsSelect2.SellsID = planidlist;
                this.SellsSelect2.SellsName = plannamelist;
                this.hidePlanerDeptID.Value = plandeptlist;
            }
            this.hideDeptID.Value = model.DeptId.ToString();
            if (model.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 && model.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置)
            {
                this.ddlopeaterStatus.Items.Add(new ListItem { Value = ((int)model.TourStatus).ToString(), Text = model.TourStatus.ToString(), Selected = true });
            }
            else
            {
                this.ddlopeaterStatus.Items.FindByValue(((int)model.TourStatus).ToString()).Selected = true; ;
            }

            if (model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团
                || model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.单团核算
                || model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.财务待审
                || model.TourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.导游报销)
            {
                this.ddlopeaterStatus.Enabled = false;
            }

            this.txtOrderCode.Text = model.OrderCode;
            this.txtOpeator.Text = model.Operator;
            this.txtTotalIn.Text = Utils.FilterEndOfTheZeroDecimal(model.TourIncome);
            this.txtAdultCount.Text = model.Adults.ToString();
            this.txtTotalIn.ReadOnly = true;

            if (model.TravellerFile != "")
            {
                StringBuilder agreement = new StringBuilder();
                agreement.AppendFormat("<span class='upload_filename'><a href='{0}' target='_blank'>游客附件</a><a href=\"javascript:void(0)\" onclick=\"SingleEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideagreement\" value='附件|{0}'/></span>", model.TravellerFile);
                this.lbFiles.Text = agreement.ToString();
            }
            if (model.TourOrderTravellerList != null && model.TourOrderTravellerList.Count > 0)
            {
                this.SetCustomList = model.TourOrderTravellerList;
                CustomListCount = SetCustomList.Count;
            }
            if (model.TourTeamPriceList != null && model.TourTeamPriceList.Count > 0)
            {
                this.SetCusttomrequire = model.TourTeamPriceList;
                CustomRequireCount = SetCusttomrequire.Count;
            }

            if (model.PlanBaseInfoList != null && model.PlanBaseInfoList.Count > 0)
            {
                this.SetSourcePlan = model.PlanBaseInfoList;
                SourcePlanCount = SetSourcePlan.Count;
            }

            hdContactdepartid.Value = model.ContactDepartId;
            //phOrderCode.Visible = true;


            string printPageHSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);
            string heSuanDanLink = string.Format("<a target='_blank' href='{0}?referertype=2&tourid={1}' class='unbtn'>查看核算单</a>", printPageHSD, model.TourId);
            switch (model.TourStatus)
            {
                //case EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审:
                //    ltrCaoZuoTiShi.Text = heSuanDanLink + "当前业务状态为<b>待终审</b>。";
                //    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.单团核算:
                    ltrCaoZuoTiShi.Text = heSuanDanLink + "当前业务状态为<b>财务核算</b>。";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.封团:
                    ltrCaoZuoTiShi.Text = heSuanDanLink + "当前业务状态为<b>核算结束</b>。";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消:
                    ltrCaoZuoTiShi.Text = "当前业务已取消。";
                    break;
                default: break;
            }

            bool _isCaoZuo = model.OperatorId == SiteUserInfo.UserId || model.SellerId == SiteUserInfo.UserId || SiteUserInfo.IsHandleElse || SiteUserInfo.IsAdmin;
            if (!_isCaoZuo && model.TourPlanersList != null && model.TourPlanersList.Count > 0)
            {
                foreach (var item in model.TourPlanersList)
                {
                    if (item.PlanerId == SiteUserInfo.UserId) { _isCaoZuo = true; break; }
                }
            }

            if (!_isCaoZuo)
            {
                phCaoZuo.Visible = false;

                if (string.IsNullOrEmpty(ltrCaoZuoTiShi.Text)) ltrCaoZuoTiShi.Text = "你没有当前业务的操作权限。";
            }
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        void BaoCun()
        {
            string msg = string.Empty;
            //EyouSoft.Model.SourceStructure.MSource source = new EyouSoft.Model.SourceStructure.MSource();
            //EyouSoft.BLL.SourceStructure.BSource bllsource = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.BLL.TourStructure.BSingleService bll = new EyouSoft.BLL.TourStructure.BSingleService();
            MSingleServiceExtend model = new MSingleServiceExtend();
            if (!String.IsNullOrEmpty(DanXiangYeWuId))
            {
                model = bll.GetSingleServiceExtendByTourId(DanXiangYeWuId);
                model.TourId = DanXiangYeWuId;
            }

            if (Utils.GetQueryStringValue("submitplan") == "submit")
            {
                //model.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审;
                if (model.PlanBaseInfoList != null && model.PlanBaseInfoList.Count > 0)
                {
                    for (int i = 0; i < model.PlanBaseInfoList.Count; i++)
                    {
                        model.PlanBaseInfoList[i].Status = EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实;
                    }
                }
            }
            else
            {
                //联系人
                string ContactName = Utils.GetFormValue(this.txtContactName.UniqueID);
                //订单编号
                //string ordercode = Utils.GetFormValue(this.txtOrdercode.UniqueID);
                //客户电话
                string ContactTel = Utils.GetFormValue(this.txtContactTel.UniqueID);
                //客户单位
                string CustomUnit = Utils.GetFormValue(this.CustomerUnitSelect1.ClientNameKHMC);
                //客户单位编号
                string CustomUnitID = Utils.GetFormValue(this.CustomerUnitSelect1.ClientNameKHBH);

                //总收入
                string TotalIn = Utils.GetFormValue(this.txtTotalIn.UniqueID);
                //人数
                string AdultCount = Utils.GetFormValue(this.txtAdultCount.UniqueID);
                //操作状态
                string opeateStatu = Utils.GetFormValue(this.ddlopeaterStatus.UniqueID);

                if (opeateStatu != "")
                {
                    model.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)EyouSoft.Common.Utils.GetInt(opeateStatu);
                }
                else
                {
                    RCWE(UtilsCommons.AjaxReturnJson("0", "无法修改已落实的数据!"));
                }

                #region 游客信息附件
                string[] agrUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
                string[] oldagrUpload = Utils.GetFormValues("hideagreement");
                string agreement = string.Empty;
                if (oldagrUpload.Length > 0)
                {
                    for (int i = 0; i < oldagrUpload.Length; i++)
                    {
                        agreement = oldagrUpload[i].Split('|')[1];
                    }
                }
                if (agrUpload.Length > 0)
                {
                    for (int i = 0; i < agrUpload.Length; i++)
                    {
                        if (agrUpload[i].Trim() != "")
                        {
                            if (agrUpload[i].Split('|').Length > 1)
                            {
                                if (agrUpload[i].Length > 1)
                                {
                                    agreement = agrUpload[i].Split('|')[1];
                                }
                            }
                        }
                    }
                }

                #endregion





                #region  单项业务添加

                #endregion




                model.WeiTuoRiQi = Utils.GetDateTime(this.txtWeiTuoRiQi.Text);
                model.TravellerFile = agreement;
                model.CompanyId = this.SiteUserInfo.CompanyId;
                model.ContactName = ContactName;
                model.ContactTel = ContactTel;
                model.ContactDepartId = Utils.GetFormValue(this.CustomerUnitSelect1.ClientNameKHBH);
                model.Operator = this.SiteUserInfo.Name;
                model.SellerName = Utils.GetFormValue(SellsSelect1.SellsNameClient);
                model.SellerId = Utils.GetFormValue(SellsSelect1.SellsIDClient);
                model.DeptId = Utils.GetInt(Utils.GetFormValue(this.hideDeptID.ClientID));
                model.BuyCompanyId = CustomUnitID;
                model.BuyCompanyName = CustomUnit;
                model.OperatorId = this.SiteUserInfo.UserId;

                IList<MTourPlaner> planerlist = new List<MTourPlaner>();
                MTourPlaner planer = null;
                string planernamestr = Utils.GetFormValue(SellsSelect2.SellsNameClient);
                string planerIdstr = Utils.GetFormValue(SellsSelect2.SellsIDClient);
                string planerptIdstr = Utils.GetFormValue(this.hidePlanerDeptID.ClientID);

                for (int i = 0; i < planerIdstr.Split(',').Length; i++)
                {
                    planer = new MTourPlaner();
                    planer.PlanerId = planerIdstr.Split(',')[i];
                    planer.Planer = planernamestr.Split(',')[i];
                    planer.DeptId = Utils.GetInt(planerptIdstr.Split(',')[i]);
                    planerlist.Add(planer);
                }
                model.TourPlanersList = planerlist;
                model.Adults = Utils.GetInt(AdultCount);
                model.ContactDepartId = this.hdContactdepartid.Value;
                //客户要求
                model.TourTeamPriceList = GetCustomRequireList();
                //供应商安排
                model.PlanBaseInfoList = GetSourcePlanList();
                //客户信息
                model.TourOrderTravellerList = GetCustomList();
                model.TourIncome = Utils.GetDecimal(TotalIn);
                if (model.TourOrderTravellerList.Count == 0)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "请至少填写一条客户信息");
                    RCWE(msg);
                }
            }

            model.OperatorDeptId = this.SiteUserInfo.DeptId;
            //model.OrderCode = Utils.GetFormValue(txtOrderCode.UniqueID);
            //model.HeTongCode = Utils.GetFormValue(txtHeTongHao.HeTongCodeClientID);
            //model.HeTongId = Utils.GetFormValue(txtHeTongHao.HeTongIdClientID);

            bool result = false;
            if (string.IsNullOrEmpty(DanXiangYeWuId))
            {
                result = bll.AddSingleService(model);
           }
            else
            {
                result = bll.UpdateSingleService(model);
            }

            //添加个人会员
            if (result && model.TourOrderTravellerList != null && model.TourOrderTravellerList.Count > 0)
            {
                var bm = new BLL.CrmStructure.BCrmMember();
                foreach (var m in model.TourOrderTravellerList)
                {
                    var md = new MCrmPersonalInfo()
                    {
                        CompanyId = SiteUserInfo.CompanyId,
                        Name = m.CnName,
                        Gender = m.Gender.HasValue ? m.Gender.Value : EyouSoft.Model.EnumType.GovStructure.Gender.其他,
                        IdCardCode = m.CardNumber,
                        Birthday = m.Birthday,
                        Telephone = m.Contact,
                        Remark = m.Remark,
                        JiFen = Utils.GetDecimal(m.LiCheng),
                        OperatorId = SiteUserInfo.UserId,
                        CardType = m.CardType,
                        CardValidDate = m.CardValidDate,
                        QianFaDate = m.QianFaDate,
                        QianFaDi = m.QianFaDi,
                        JoinType = "游客名单导入",
                        CrmId = m.TravellerId
                    };

                    //判断是否存在该会员
                    if (!bm.IsExists(md.CompanyId, md.CrmId))
                        bm.Insert(md);
                    else
                        bm.Update(md);
                }
            }

            string type = string.IsNullOrEmpty(DanXiangYeWuId) ? "新增" : "修改";
            if (Utils.GetQueryStringValue("submitplan") == "submit")
            {
                EyouSoft.Model.HTourStructure.MTourStatusChange statusChange = new EyouSoft.Model.HTourStructure.MTourStatusChange();

                statusChange.CompanyId = this.SiteUserInfo.CompanyId;
                statusChange.TourId = model.TourId;
                statusChange.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.单团核算;
                statusChange.Operator = this.SiteUserInfo.Name;
                statusChange.OperatorId = this.SiteUserInfo.UserId;
                statusChange.OperatorDeptId = this.SiteUserInfo.DeptId;

                if (new EyouSoft.BLL.HTourStructure.BTour().UpdateTourStatus(statusChange) == 1)
                {
                    type = "提交";
                }
            }

            if (result)
            {
                msg = UtilsCommons.AjaxReturnJson("1", type + "成功！");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", type + "失败！");
            }

            RCWE(msg);
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            Privs_Insert = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_新增);
            Privs_JiDiaoAnPai = Privs_Update = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_修改);

            if (!string.IsNullOrEmpty(DanXiangYeWuId))
            {
                if (!Privs_Update)
                {
                    phCaoZuo.Visible = false;

                    ltrCaoZuoTiShi.Text = "你没有当前业务的操作权限。";
                }
            }
            else
            {
                if (!Privs_Insert)
                {
                    phCaoZuo.Visible = false;

                    ltrCaoZuoTiShi.Text = "你没有当前业务的操作权限。";
                }
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

        /// <summary>
        /// 获取客户要求数据源
        /// </summary>
        /// <returns></returns>
        private IList<MTourTeamPrice> GetCustomRequireList()
        {
            string[] ServerType = Utils.GetFormValues("ServerType");
            string[] ServiceStandard = Utils.GetFormValues("ServiceStandard");
            string[] dj = Utils.GetFormValues("DanJia");
            string[] sl = Utils.GetFormValues("ShuLiang");
            string[] Price = Utils.GetFormValues("Price");
            string[] remarkCustom = Utils.GetFormValues("remarkCustom");
            var IsTax = Utils.GetFormValues("hidIsTax");
            MTourTeamPrice model;
            IList<MTourTeamPrice> list = new List<MTourTeamPrice>();
            if (ServerType.Length > 0 && ServiceStandard.Length > 0 && Price.Length > 0 && remarkCustom.Length > 0)
            {
                string errorMsg = string.Empty;
                for (int i = 0; i < ServerType.Length; i++)
                {
                    model = new MTourTeamPrice();
                    if (!String.IsNullOrEmpty(Price[i].ToString()) && Price[i].ToString() != "0")
                    {
                        model.ServiceType = Utils.GetEnumValue<EyouSoft.Model.EnumType.ComStructure.ContainProjectType>(ServerType[i], EyouSoft.Model.EnumType.ComStructure.ContainProjectType.其它);
                        model.DanJia = Utils.GetDecimal(dj[i]);
                        model.ShuLiang = float.Parse(Utils.GetDecimal(sl[i]).ToString());
                        model.Quote = Utils.GetDecimal(Price[i].ToString());
                        model.Remark = remarkCustom[i];
                        model.ServiceStandard = ServiceStandard[i];
                        model.IsTax = bool.Parse(IsTax[i]);
                        list.Add(model);
                    }
                }
                if (!String.IsNullOrEmpty(errorMsg))
                    return null;
                else
                {
                    this.SetCusttomrequire = list;
                    return list;
                }
            }
            else
                return null;
        }

        /// <summary>
        /// 获取客户信息数据源
        /// </summary>
        /// <returns></returns>
        private IList<MTourOrderTraveller> GetCustomList()
        {
            var TravellerId = Utils.GetFormEditorValues("hidTravellerId");
            string[] CustomName = Utils.GetFormValues("CustomName");
            string[] CustomType = Utils.GetFormValues("CustomType");
            string[] Certificates = Utils.GetFormValues("Certificates");
            string[] CertificatesNum = Utils.GetFormValues("CertificatesNum");
            string[] Sex = Utils.GetFormValues("Sex");
            string[] ContactTel = Utils.GetFormValues("ContactTel");
            var licheng = Utils.GetFormEditorValues("LiCheng");
            var remark = Utils.GetFormEditorValues("Remark");
            var QianFaDate = Utils.GetFormEditorValues("QianFaDate");
            var CardValidDate = Utils.GetFormEditorValues("CardValidDate");
            var Birthday = Utils.GetFormEditorValues("Birthday");
            var QianFaDi = Utils.GetFormEditorValues("QianFaDi");
            IList<MTourOrderTraveller> list = new List<MTourOrderTraveller>();
            MTourOrderTraveller model;
            if (CustomName.Length > 0 && CustomType.Length > 0 && Certificates.Length > 0 && CertificatesNum.Length > 0 && Sex.Length > 0 && ContactTel.Length > 0)
            {
                string errorMsg = string.Empty;
                for (int i = 0; i < CustomName.Length; i++)
                {
                    model = new MTourOrderTraveller();
                    if (!String.IsNullOrEmpty(CustomName[i]))
                    {
                        model.TravellerId = string.IsNullOrEmpty(TravellerId[i]) ? Guid.NewGuid().ToString() : TravellerId[i];
                        model.CnName = CustomName[i];
                        model.CardNumber = CertificatesNum[i];
                        model.CardType = Utils.GetEnumValue<EyouSoft.Model.EnumType.TourStructure.CardType>(Certificates[i], EyouSoft.Model.EnumType.TourStructure.CardType.身份证);
                        model.Contact = ContactTel[i];
                        model.OrderId = string.Empty;
                        model.IsInsurance = false;
                        model.Gender = Utils.GetEnumValue<EyouSoft.Model.EnumType.GovStructure.Gender>(Sex[i], EyouSoft.Model.EnumType.GovStructure.Gender.其他);
                        model.VisitorType = Utils.GetEnumValue<EyouSoft.Model.EnumType.TourStructure.VisitorType>(CustomType[i], EyouSoft.Model.EnumType.TourStructure.VisitorType.成人);
                        model.LiCheng = licheng[i];
                        model.Remark = remark[i];
                        model.QianFaDate = Utils.GetDateTimeNullable(QianFaDate[i]);
                        model.CardValidDate = CardValidDate[i];
                        model.Birthday = Utils.GetDateTimeNullable(Birthday[i]);
                        model.QianFaDi = QianFaDi[i];
                        list.Add(model);
                    }
                }
                if (String.IsNullOrEmpty(errorMsg))
                {
                    this.SetCustomList = list;
                    return list;
                }
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取供应商安排数据源
        /// </summary>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> GetSourcePlanList()
        {
            string[] SourceId = Utils.GetFormValues("ShowID");
            string[] Sourcetype = Utils.GetFormValues("Sourcetype");
            string[] SourceName = Utils.GetFormValues("SourceName");
            string[] GuideNotes = Utils.GetFormValues("GuideNotes");
            string[] PlanCost = Utils.GetFormValues("PlanCost");
            string[] remarkSource = Utils.GetFormValues("remarkSource");
            string[] PayType = Utils.GetFormValues("PayType");
            string[] Count = Utils.GetFormValues("Count");
            string[] ContactName = Utils.GetFormValues("ContactName");
            string[] ContactTel = Utils.GetFormValues("ContactPhone");
            string[] ContactFax = Utils.GetFormValues("ContactFax");
            string[] PlanId = Utils.GetFormValues("PlanId");
            EyouSoft.Model.PlanStructure.MPlanBaseInfo model;
            IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> list = new List<EyouSoft.Model.PlanStructure.MPlanBaseInfo>();
            if (Sourcetype.Length > 0 && SourceName.Length > 0 && GuideNotes.Length > 0 && PlanCost.Length > 0 && remarkSource.Length > 0)
            {
                string errorMsg = string.Empty;
                for (int i = 0; i < SourceName.Length; i++)
                {
                    model = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
                    if (!String.IsNullOrEmpty(PlanCost[i]) && !String.IsNullOrEmpty(SourceName[i]) && !string.IsNullOrEmpty(SourceId[i]))
                    {
                        model.GuideNotes = GuideNotes[i];
                        model.PlanCost = Utils.GetDecimal(PlanCost[i]);
                        // model.Remarks = remarkSource[i];
                        model.CostDetail = remarkSource[i];
                        model.SourceName = SourceName[i];
                        model.Confirmation = Utils.GetDecimal(PlanCost[i]);
                        model.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)(Utils.GetInt(PayType[i]));
                        model.Num = Utils.GetInt(Count[i]);
                        EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)Utils.GetInt(Utils.GetFormValue(status.UniqueID));
                        model.ContactName = ContactName[i];
                        model.ContactPhone = ContactTel[i];
                        model.ContactFax = ContactFax[i];
                        model.SourceId = SourceId[i];
                        model.PlanId = PlanId[i];
                        model.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
                        if (tourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划)
                        {
                            model.Status = EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实;
                        }
                        if (tourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置 || tourStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.财务待审)
                        {
                            model.Status = EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实;
                        }
                        model.Type = Utils.GetEnumValue<EyouSoft.Model.EnumType.PlanStructure.PlanProject>(Sourcetype[i], EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它);
                        list.Add(model);
                        SourcePlanCount = list.Count;
                    }
                }
                if (String.IsNullOrEmpty(errorMsg))
                {
                    this.SetSourcePlan = list;
                    return list;
                }
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// 页面初始化时绑定数据
        /// </summary>
        private void SetCustomDataList()
        {
            this.rptCustomList.DataSource = this.SetCustomList;
            this.rptCustomList.DataBind();
        }

        private void SetCustomRequireList()
        {
            this.rptRequire.DataSource = this.SetCusttomrequire;
            this.rptRequire.DataBind();
        }
        private void SetSourcePlanList()
        {
            this.rptsourceplan.DataSource = this.SetSourcePlan;
            this.rptsourceplan.DataBind();
        }
    }
}
