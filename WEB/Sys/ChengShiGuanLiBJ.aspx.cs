using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.SysStructure;
using EyouSoft.BLL.SysStructure;

namespace EyouSoft.Web.Sys
{
    public partial class ChengShiGuanLiBJ : BackPage
    {
        int getid = 0, mark = 0;
        string dotype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // getid = Utils.GetInt(Utils.GetQueryStringValue("id"));
            string isVisable = Utils.GetQueryStringValue("isPro");
            switch (isVisable)
            {
                case "2": plc_isAddCity.Visible = true;
                    break;
                case "3": plc_isAddCity.Visible = true; plc_isAddDistry.Visible = true;
                    break;
                default:
                    break;
            }

            mark = Utils.GetInt(Utils.GetQueryStringValue("mark"));
            dotype = Utils.GetQueryStringValue("type");
            if (!string.IsNullOrEmpty(dotype))
            {
                save(mark, 0, dotype);
            }
            // initPage(getid, mark);

        }
        /// <summary>
        /// 初始化页面 
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="type">操作标识 1添加省份,2添加城市</param>
        protected void initPage(int id, int type)
        {
            BGeography bll = new BGeography();
            if (type == 1)
            {
                plc_isAddCity.Visible = false;
                MSysProvince MProvince = bll.GetProvince(SiteUserInfo.CompanyId, id);
                if (MProvince != null)
                {
                    txt_cnName.Text = MProvince.Name;
                    txt_enName.Text = MProvince.EnName;
                    //txt_tnName.Text = MProvince.ThName;
                    txtJP.Text = MProvince.JP;
                    txtQP.Text = MProvince.QP;
                }
            }
            if (type == 2)
            {
                MSysCity Mcity = bll.GetCity(SiteUserInfo.CompanyId, id);
                if (Mcity != null)
                {
                    txt_cnName.Text = Mcity.Name;
                    txt_enName.Text = Mcity.EnName;
                    //txt_tnName.Text = Mcity.ThName;
                    txtJP.Text = Mcity.JP;
                    txtQP.Text = Mcity.QP;
                }
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="mark">操作标识 1添加省份,2添加城市</param>
        /// <param name="id">信息编号</param>
        /// <param name="dotype">操作标识 修改，添加</param>
        protected void save(int mark, int id, string dotype)
        {
            int result = 0;
            BGeography bll = new BGeography();
            string cnName = Utils.GetFormValue(txt_cnName.UniqueID);
            string enName = Utils.GetFormValue(txt_enName.UniqueID);
            //string tnName = Utils.GetFormValue(txt_tnName.UniqueID);
            var jp = Utils.GetFormValue(txtJP.UniqueID);
            var qp = Utils.GetFormValue(txtQP.UniqueID);

            if (mark == 1)
            {
                int countryID = Utils.GetInt(Utils.GetFormValue("sltCountry"));
                MSysProvince MProvince = new MSysProvince();
                MProvince.CountryId = countryID;
                MProvince.Name = cnName;
                MProvince.EnName = enName;
                //MProvince.ThName = tnName;
                MProvince.JP = jp;
                MProvince.QP = qp;
                switch (dotype)
                {
                    case "add":
                        result = bll.InsertProvince(SiteUserInfo.CompanyId, MProvince);
                        break;
                    //case "update":
                    //    MProvince.ProvinceId = id;
                    //    result = bll.UpdateProvince(SiteUserInfo.CompanyId, MProvince);
                    //    break;
                    default:
                        break;
                }
            }
            if (mark == 2)
            {
                int proID = Utils.GetInt(Utils.GetFormValue("sltProvince"));
                MSysCity Mcity = new MSysCity();
                Mcity.ProvinceId = proID;
                Mcity.Name = cnName;
                Mcity.EnName = enName;
                //Mcity.ThName = tnName;
                Mcity.JP = jp;
                Mcity.QP = qp;
                switch (dotype)
                {
                    case "add":
                        result = bll.InsertCity(SiteUserInfo.CompanyId, Mcity);
                        break;
                    //case "update":
                    //    Mcity.ProvinceId = id;
                    //    result = bll.UpdateCity(SiteUserInfo.CompanyId, Mcity);
                    //    break;
                    default:
                        break;
                }
            }

            if (mark == 3)
            {
                int cityid = Utils.GetInt(Utils.GetFormValue("sltCity"));
                MSysDistrict Mcity = new MSysDistrict();
                Mcity.CityId = cityid;
                Mcity.Name = cnName;
                Mcity.EnName = enName;
                //Mcity.ThName = tnName;
                Mcity.JP = jp;
                Mcity.QP = qp;
                switch (dotype)
                {
                    case "add":
                        result = bll.InsertDistrict(SiteUserInfo.CompanyId, Mcity);
                        break;
                    //case "update":
                    //    Mcity.CityId = id;
                    //    result = bll.InsertDistrict(SiteUserInfo.CompanyId, Mcity);
                    //    break;
                    default:
                        break;
                }
            }

            AjaxResponse(UtilsCommons.AjaxReturnJson(result.ToString(), result == 1 ? "操作成功" : "操作失败"));

        }
    }
}
