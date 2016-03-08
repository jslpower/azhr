using System;
using System.Collections;

namespace EyouSoft.Web.Sales
{
    using System.Collections.Generic;

    using EyouSoft.BLL.HTourStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.HTourStructure;

    public partial class PaiTuan : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string doType = Utils.GetQueryStringValue("doType");
            if (doType != "" && doType == "save")
            {
                Response.Clear();
                Response.Write(PageSave());
                Response.End();
            }
            if (!IsPostBack)
            {
                this.SellsSelect1.SMode = true;//多选
                this.SellsSelect1.ReadOnly = true;
                this.SellsSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
                this.SellsSelect1.ClientDeptID = this.hideDeptId.ClientID;
                string id = Utils.GetQueryStringValue("id");
                if (id != "")
                {
                    PageInit(id);
                }
            }
        }

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id"></param>
        private void PageInit(string id)
        {
            var bll = new BTour();
            var model = bll.GetTourModel(id);
            if (model == null)
            {
                return;
            }
            //绑定内部信息
            this.txtInsiderInfor.Text = model.InsideInformation;
            var l = bll.GetTourToPlaner(id);
            if (l!=null && l.TourPlanItemList != null)
            {
                foreach (var t in l.TourPlanItemList)
                {
                    //绑定原计调安排项
                    this.hidePlanItem.Value += ((int)t.PlanType).ToString() + ",";
                }
            }
            if (l!=null && l.TourPlanerList != null && l.TourPlanerList.Count > 0)
            {
                foreach (var t in l.TourPlanerList)
                {
                    this.SellsSelect1.SellsID += t.PlanerId + ",";
                    this.SellsSelect1.SellsName += t.Planer + ",";
                    this.hideDeptId.Value += t.PlanerDeptId + ",";
                }
                //绑定 计调员
                this.SellsSelect1.SellsID = this.SellsSelect1.SellsID.Trim(',');
                this.SellsSelect1.SellsName = this.SellsSelect1.SellsName.Trim(',');
                this.hideDeptId.Value = this.hideDeptId.Value.Trim(',');
            }
            else if (model.TourPlanerList != null && model.TourPlanerList.Count > 0)
            {
                foreach (var t in model.TourPlanerList)
                {
                    this.SellsSelect1.SellsID += t.PlanerId + ",";
                    this.SellsSelect1.SellsName += t.Planer + ",";
                    this.hideDeptId.Value += t.PlanerDeptId + ",";
                }
                //绑定 计调员
                this.SellsSelect1.SellsID = this.SellsSelect1.SellsID.Trim(',');
                this.SellsSelect1.SellsName = this.SellsSelect1.SellsName.Trim(',');
                this.hideDeptId.Value = this.hideDeptId.Value.Trim(',');
            }
            else
            {
                //获取部门计调
                var mDept = new EyouSoft.BLL.ComStructure.BComDepartment().GetModel(
                    this.SiteUserInfo.DeptId, this.CurrentUserCompanyID);
                if (mDept != null)
                {
                    this.SellsSelect1.SellsID = mDept.DepartPlanId;
                    this.SellsSelect1.SellsName = mDept.DepartPlan;
                }
            }
            if (l == null)
            {
                return;
            }
            this.chk_isfenche.Checked = l.IsCar;
            this.txtChe.Value = l.CarNum.ToString();
            this.txtCheRemark.Value = l.CarRemark;
            this.chk_isfenzuo.Checked = l.IsDesk;
            this.txtZuo.Value = l.DeskNum.ToString();
            this.txtZuoRemark.Value = l.DeskRemark;
        }
        #endregion

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

        #region 保存 计调安排
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        private string PageSave()
        {
            string msg = string.Empty;
            //获取表单
            string[] sellsArray = Utils.GetFormValue(this.SellsSelect1.SellsIDClient).Split(',');
            string[] sellsNameArray = Utils.GetFormValue(this.SellsSelect1.SellsNameClient).Split(',');
            string[] sellsDeptArray = Utils.GetFormValue(this.hideDeptId.UniqueID).Split(',');
            string[] item = Utils.GetFormValues("chk_item");
            string tourId = Utils.GetQueryStringValue("tourID");
            string insiderInfor = Utils.GetFormValue(this.txtInsiderInfor.UniqueID);

            //验证
            if (sellsArray.Length == 0)
            {
                return UtilsCommons.AjaxReturnJson("0", "请选择OP!");
            }
            if (item.Length == 0)
            {
                return UtilsCommons.AjaxReturnJson("0", "请选择计调安排项目!");
            }

            BTour bll = new BTour();

            IList<EyouSoft.Model.HTourStructure.MTourPlaner> TourPlaner = new List<EyouSoft.Model.HTourStructure.MTourPlaner>();
            for (int i = 0; i < sellsArray.Length; i++)
            {
                EyouSoft.Model.HTourStructure.MTourPlaner planerModel = new EyouSoft.Model.HTourStructure.MTourPlaner();
                planerModel.PlanerId = sellsArray[i];
                planerModel.Planer = sellsNameArray[i];
                planerModel.PlanerDeptId = Utils.GetInt(sellsDeptArray[i]);
                planerModel.TourId = tourId;
                TourPlaner.Add(planerModel);



            }

            IList<EyouSoft.Model.HTourStructure.MTourPlanItem> TourPlanItem = new List<EyouSoft.Model.HTourStructure.MTourPlanItem>();
            for (int i = 0; i < item.Length; i++)
            {
                EyouSoft.Model.HTourStructure.MTourPlanItem tpiModel = new EyouSoft.Model.HTourStructure.MTourPlanItem();
                //tpiModel.TourId = tourId;
                tpiModel.PlanType = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(item[i]);
                TourPlanItem.Add(tpiModel);
            }



            var tourModel = bll.GetPaituanModel(tourId);
            if (tourModel != null)
            {
                var sendTourModel = new EyouSoft.Model.HTourStructure.MTourToPlaner();
                sendTourModel.CompanyId = SiteUserInfo.CompanyId;
                sendTourModel.OperatorDeptId = SiteUserInfo.DeptId;
                sendTourModel.InsideInformation = insiderInfor;
                //sendTourModel.LDate = Convert.ToDateTime(tourModel.LDate);
                sendTourModel.Operator = SiteUserInfo.Name;
                sendTourModel.OperatorId = SiteUserInfo.UserId;

                sendTourModel.TourPlanerList = TourPlaner;
                sendTourModel.TourPlanItemList = TourPlanItem;
                //sendTourModel.TourCode = bll.GenerateTourNo(SiteUserInfo.DeptId, SiteUserInfo.CompanyId, tourModel.TourType, tourModel.LDate);
                sendTourModel.TourId = tourId;
                //sendTourModel.TourType = tourModel.TourType;
                sendTourModel.IsCar = this.chk_isfenche.Checked;
                sendTourModel.CarNum = Utils.GetInt(this.txtChe.Value);
                sendTourModel.CarRemark = this.txtCheRemark.Value;
                sendTourModel.IsDesk = this.chk_isfenzuo.Checked;
                sendTourModel.DeskNum = Utils.GetInt(this.txtZuo.Value);
                sendTourModel.DeskRemark = this.txtZuoRemark.Value;

                switch (bll.SendTourToPlaner(sendTourModel))
                {
                    case 0:
                        msg = UtilsCommons.AjaxReturnJson("0", "派团失败!");
                        break;
                    case 1:
                        msg = UtilsCommons.AjaxReturnJson("1", "派团成功!");
                        break;
                    case -1:
                        msg = UtilsCommons.AjaxReturnJson("-1", "计划已派团给计调!");
                        break;
                }
            }



            return msg;
        }
        #endregion
    }
}
