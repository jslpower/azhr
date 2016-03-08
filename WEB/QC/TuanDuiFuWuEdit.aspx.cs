using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.QC
{
    public partial class TuanDuiFuWuEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        string ZhiJianId = string.Empty;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ZhiJianId = Utils.GetQueryStringValue("zhijianid");

            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "submit") BaoCun();

            InitInfo();

        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质检中心_团队服务_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质检中心_团队服务_修改);
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            if (string.IsNullOrEmpty(ZhiJianId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            var info = GetFormInfo();

            bool bllRetCode = false;

            if (string.IsNullOrEmpty(ZhiJianId))
            {
                bllRetCode = new EyouSoft.BLL.BQC.BTourServiceQC().AddTourServiceQC(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.BQC.BTourServiceQC().UpdateTourServiceQC(info);
            }

            if (bllRetCode) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));

        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            List<EnumObj> list = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.SatisfactionType));

            this.ddlTrip.DataSource = list;
            this.ddlTrip.DataValueField = "Value";
            this.ddlTrip.DataTextField = "Text";
            this.ddlTrip.DataBind();


            this.ddlScenic.DataSource = list;
            this.ddlScenic.DataValueField = "Value";
            this.ddlScenic.DataTextField = "Text";
            this.ddlScenic.DataBind();

            this.ddlHotel.DataSource = list;
            this.ddlHotel.DataValueField = "Value";
            this.ddlHotel.DataTextField = "Text";
            this.ddlHotel.DataBind();

            this.ddlFood.DataSource = list;
            this.ddlFood.DataValueField = "Value";
            this.ddlFood.DataTextField = "Text";
            this.ddlFood.DataBind();

            this.ddlGuideOne.DataSource = list;
            this.ddlGuideOne.DataValueField = "Value";
            this.ddlGuideOne.DataTextField = "Text";
            this.ddlGuideOne.DataBind();

            this.ddlGuideTwo.DataSource = list;
            this.ddlGuideTwo.DataValueField = "Value";
            this.ddlGuideTwo.DataTextField = "Text";
            this.ddlGuideTwo.DataBind();

            if (string.IsNullOrEmpty(ZhiJianId)) return;

            var info = new EyouSoft.BLL.BQC.BTourServiceQC().GetTourServiceQCModel(ZhiJianId);

            if (info == null) RCWE("异常请求");

            txtTuanHaoXuanYong.TourId = info.TourId;
            txtTuanHaoXuanYong.TourCode = info.TourCode;

            txtGuideOneName.Text = info.GuideOneName;
            txtGuideTwoName.Text = info.GuideTwoName;
            txtQCTime.Text = info.QCTime.ToString("yyyy-MM-dd");
            txtLeaderName.Text = info.LeaderName;
            txtLeaderTel.Text = info.LeaderTel;
            ddlTrip.SelectedValue = ((int)info.Trip).ToString();
            ddlScenic.SelectedValue = ((int)info.Scenic).ToString();
            ddlHotel.SelectedValue = ((int)info.Hotel).ToString();
            ddlFood.SelectedValue = ((int)info.Food).ToString();
            ddlGuideOne.SelectedValue = ((int)info.GuideOne).ToString();
            ddlGuideTwo.SelectedValue = ((int)info.GuideTwo).ToString();

            txtTripRemark.Text = info.TripRemark;
            txtScenicRemark.Text = info.ScenicRemark;
            txtHotelRemark.Text = info.HotelRemark;
            txtFoodRemark.Text = info.FoodRemark;
            txtGuideOneRemark.Text = info.GuideOneRemark;
            txtGuideTwoRemark.Text = info.GuideTwoRemark;
            txtAdvice.Text = info.Advice;

            if (info.FileList != null && info.FileList.Count > 0)
            {
                IList<global::Web.UserControl.MFileInfo> files = new List<global::Web.UserControl.MFileInfo>();


                foreach (var item in info.FileList)
                {
                    files.Add(new global::Web.UserControl.MFileInfo() { FilePath = item.FilePath });
                }

                txtFuJian.YuanFiles = files;
            }


        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.QC.MTourServiceQC GetFormInfo()
        {
            var info = new EyouSoft.Model.QC.MTourServiceQC();

            info.QCID = ZhiJianId;
            info.CompanyId = CurrentUserCompanyID;
            info.TourCode = Utils.GetFormValue(txtTuanHaoXuanYong.ClientNameTourCode);
            info.TourId = Utils.GetFormValue(txtTuanHaoXuanYong.ClientNameTourId);

            info.GuideOneName = Utils.GetFormValue(this.txtGuideOneName.UniqueID);
            info.GuideTwoName = Utils.GetFormValue(this.txtGuideTwoName.UniqueID);
            info.QCTime = Utils.GetDateTime(txtQCTime.UniqueID, DateTime.Now);
            info.LeaderName = Utils.GetFormValue(txtLeaderName.UniqueID);
            info.LeaderTel = Utils.GetFormValue(txtLeaderTel.UniqueID);
            info.Trip = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)Utils.GetInt(Utils.GetFormValue(ddlTrip.UniqueID));
            info.Scenic = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)Utils.GetInt(Utils.GetFormValue(ddlScenic.UniqueID));
            info.Hotel = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)Utils.GetInt(Utils.GetFormValue(ddlHotel.UniqueID));
            info.Food = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)Utils.GetInt(Utils.GetFormValue(ddlFood.UniqueID));
            info.GuideOne = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)Utils.GetInt(Utils.GetFormValue(ddlGuideOne.UniqueID));
            info.GuideTwo = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)Utils.GetInt(Utils.GetFormValue(ddlGuideTwo.UniqueID));
            info.TripRemark = Utils.GetFormValue(txtTripRemark.UniqueID);
            info.ScenicRemark = Utils.GetFormValue(txtScenicRemark.UniqueID);
            info.HotelRemark = Utils.GetFormValue(txtHotelRemark.UniqueID);
            info.FoodRemark = Utils.GetFormValue(txtFoodRemark.UniqueID);
            info.GuideOneRemark = Utils.GetFormValue(txtGuideOneRemark.UniqueID);
            info.GuideTwoRemark = Utils.GetFormValue(txtGuideTwoRemark.UniqueID);
            info.Advice = Utils.GetFormValue(txtAdvice.UniqueID);

            info.OperatorDeptId = SiteUserInfo.DeptId;
            info.OperatorId = SiteUserInfo.UserId;
            info.Operator = SiteUserInfo.Name;


            info.FileList = GetFuJians();

            return info;
        }

        /// <summary>
        /// get fujians
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.ComStructure.MComAttach> GetFuJians()
        {
            IList<EyouSoft.Model.ComStructure.MComAttach> items = new List<EyouSoft.Model.ComStructure.MComAttach>();

            var files1 = txtFuJian.Files;
            var files2 = txtFuJian.YuanFiles;

            IList<global::Web.UserControl.MFileInfo> files = new List<global::Web.UserControl.MFileInfo>();

            if (files1 != null && files1.Count > 0)
            {
                foreach (var item in files1)
                {
                    files.Add(item);
                }
            }
            if (files2 != null && files2.Count > 0)
            {
                foreach (var item in files2)
                {
                    files.Add(item);
                }
            }

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    items.Add(new EyouSoft.Model.ComStructure.MComAttach() { FilePath = file.FilePath, ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.车队质检 });
                }
            }

            return items;
        }
        #endregion




    }
}
