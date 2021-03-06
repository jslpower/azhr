﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class DiJieEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        string GysId = string.Empty;
        protected int CountryId = 0;
        protected int ProvinceId = 0;
        protected int CityId = 0;
        protected int DistrictId = 0;
        protected string IsTuiJian = "0";
        protected string IsHeTong = "0";
        protected string JieSuanFangShi = "0";
        protected string IsQianDan = "0";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            GysId = Utils.GetQueryStringValue("gysid");

            if (Utils.GetQueryStringValue("dotype") == "submit") BaoCun();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_修改);

            if (string.IsNullOrEmpty(GysId))
            {
                phSubmit.Visible = Privs_Insert;
            }
            else
            {
                phSubmit.Visible = Privs_Update;
            }
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(GysId))
            {
                txtWeiHuRen.Value = SiteUserInfo.Name;
                txtWeiHuTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
                return;
            }

            var info = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(GysId);
            if (info == null) RCWE("异常请求");

            CountryId = info.CPCD.CountryId;
            ProvinceId = info.CPCD.ProvinceId;
            CityId = info.CPCD.CityId;
            DistrictId = info.CPCD.DistrictId;
            txtName.Value = info.GysName;
            txtDiZhi.Value = info.Address;
            txtFaRenName.Value = info.FaRenName;
            txtFaRenTelephone.Value = info.FaRenTelephone;
            txtJianJie.Value = info.JianJie;
            txtBeiZhu.Value = info.BeiZhu;
            txtGuaZhangQiXian.Value = info.GuaZhangQiXian;
            txtWeiHuRen.Value = info.LatestOperatorName;
            txtWeiHuTime.Value = info.LatestTime.ToString("yyyy-MM-dd");
            txtXuKeZhengHao.Value = info.XuKeZhengCode;

            GysHeTong1.HeTongs = info.HeTongs;
            GysLxr1.Lxrs = info.Lxrs;

            if (info.IsTuiJian) IsTuiJian = "1";
            if (info.IsHeTong) IsHeTong = "1";
            JieSuanFangShi = ((int)info.JieSuanFangShi).ToString();
            if (info.IsQianDan) IsQianDan = "1";
        }

        /// <summary>
        /// bao cun
        /// </summary>
        void BaoCun()
        {
            if (string.IsNullOrEmpty(GysId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            var info = GetFormInfo();

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(GysId))
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BGys().Insert(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.HGysStructure.BGys().Update(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MGysInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MGysInfo();
            info.GysId = GysId;
            info.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
            info.CPCD.CountryId = Utils.GetInt(Utils.GetFormValue("txtCountryId"));
            info.CPCD.ProvinceId = Utils.GetInt(Utils.GetFormValue("txtProvinceId"));
            info.CPCD.CityId = Utils.GetInt(Utils.GetFormValue("txtCityId"));
            info.CPCD.DistrictId = Utils.GetInt(Utils.GetFormValue("txtDistrictId"));
            info.GysName = Utils.GetFormValue(txtName.UniqueID);
            info.Address = Utils.GetFormValue(txtDiZhi.UniqueID);
            info.FaRenName = Utils.GetFormValue(txtFaRenName.UniqueID);
            info.FaRenTelephone = Utils.GetFormValue(txtFaRenTelephone.UniqueID);
            info.JianJie = Utils.GetFormValue(txtJianJie.UniqueID);
            info.BeiZhu = Utils.GetFormValue(txtBeiZhu.UniqueID);
            info.IsTuiJian = Utils.GetFormValue("txtIsTuiJian") == "1";
            info.IsHeTong = Utils.GetFormValue("txtIsHeTong") == "1";
            info.IsQianDan = Utils.GetFormValue("txtIsQianDan") == "1";
            info.JieSuanFangShi = Utils.GetEnumValue<EyouSoft.Model.EnumType.GysStructure.JieSuanFangShi>(Utils.GetFormValue("txtJieSuanFangShi"), EyouSoft.Model.EnumType.GysStructure.JieSuanFangShi.现付);
            info.GuaZhangQiXian = Utils.GetFormValue(txtGuaZhangQiXian.UniqueID);
            info.XuKeZhengCode = Utils.GetFormValue(txtXuKeZhengHao.UniqueID);
            info.Lxrs = GysLxr1.Lxrs;
            info.HeTongs = GysHeTong1.HeTongs;
            info.OperatorId = SiteUserInfo.UserId;
            info.CompanyId = SiteUserInfo.CompanyId;
            info.LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.地接社;
            info.LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;

            return info;
        }
        #endregion
    }
}
