using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Gys
{
    public partial class JiuDianEdit : EyouSoft.Common.Page.BackPage
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
        protected string IsMianYi = "0";
        protected string XingJi = "0";
        protected EyouSoft.Model.EnumType.SysStructure.LngType LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            GysId = Utils.GetQueryStringValue("gysid");
            LngType = Utils.GetEnumValue<EyouSoft.Model.EnumType.SysStructure.LngType>(Utils.GetQueryStringValue("lng"), EyouSoft.Model.EnumType.SysStructure.LngType.中文);

            if (Utils.GetQueryStringValue("dotype") == "submit") BaoCun();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_修改);

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

            var info = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(GysId, LngType);
            if (info == null) info = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(GysId);
            if (info == null) RCWE("异常请求");

            phLng.Visible = true;

            CountryId = info.CPCD.CountryId;
            ProvinceId = info.CPCD.ProvinceId;
            CityId = info.CPCD.CityId;
            DistrictId = info.CPCD.DistrictId;
            txtName.Value = info.GysName;
            txtDiZhi.Value = info.Address;
            txtQianTaiTelephone.Value = info.JiuDian.QianTaiTelephone;
            txtWangZhi.Value = info.WangZhi;
            txtJianJie.Value = info.JianJie;
            txtBeiZhu.Value = info.BeiZhu;
            txtGuaZhangQiXian.Value = info.GuaZhangQiXian;
            txtWeiHuRen.Value = info.LatestOperatorName;
            txtWeiHuTime.Value = info.LatestTime.ToString("yyyy-MM-dd");

            GysHeTong1.HeTongs = info.HeTongs;
            GysLxr1.Lxrs = info.Lxrs;
            GysJiuDianBaoJia1.BaoJias = info.JiuDian.BaoJias;
            YuCunKuan.YuCunKuans = new EyouSoft.BLL.HGysStructure.BYuCunKuan().GetYuCunKuans(info.GysId);

            if (info.IsTuiJian) IsTuiJian = "1";
            if (info.IsHeTong) IsHeTong = "1";            
            if (info.IsQianDan) IsQianDan = "1";
            if (info.IsMianYi) IsMianYi = "1";
            JieSuanFangShi = ((int)info.JieSuanFangShi).ToString();
            XingJi = ((int)info.JiuDian.XingJi).ToString();

            if (info.JiuDian.FuJians != null && info.JiuDian.FuJians.Count > 0)
            {
                IList<global::Web.UserControl.MFileInfo> files = new List<global::Web.UserControl.MFileInfo>();
                

                foreach (var item in info.JiuDian.FuJians)
                {
                    files.Add(new global::Web.UserControl.MFileInfo() { FilePath = item.FilePath });
                }

                txtFuJian.YuanFiles = files;
            }
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

            if (bllRetCode == 1)
            {
                if (YuCunKuan.YuCunKuans!=null&&YuCunKuan.YuCunKuans.Count>0)
                {
                    var b = new EyouSoft.BLL.HGysStructure.BYuCunKuan();
                    var gysid = Utils.GetQueryStringValue("gysid");
                    foreach (var m in YuCunKuan.YuCunKuans)
                    {
                        m.GysId = gysid;
                        m.OperatorId = SiteUserInfo.UserId;
                        m.IssueTime = DateTime.Now;
                        if (string.IsNullOrEmpty(m.YuCunId))
                        {
                            b.Insert(m);
                        }
                        else
                        {
                            b.Update(m);
                        }
                    }
                    var l = b.GetYuCunKuans(gysid);
                    foreach (var mdl in YuCunKuan.YuCunKuans)
                    {
                        if (l != null && l.Count > 0)
                        {
                            l = l.Where(m => m.YuCunId != mdl.YuCunId && !string.IsNullOrEmpty(mdl.YuCunId)).ToList();
                        }
                    }
                    if (l != null && l.Count > 0)
                    {
                        foreach (var m in l)
                        {
                            b.Delete(SiteUserInfo.CompanyId, gysid, m.YuCunId);
                        }
                    }
                }
                RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
            }
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
            info.WangZhi = Utils.GetFormValue(txtWangZhi.UniqueID);
            info.JiuDian = new EyouSoft.Model.HGysStructure.MJiuDianInfo();
            info.JiuDian.QianTaiTelephone = Utils.GetFormValue(txtQianTaiTelephone.UniqueID);
            info.JiuDian.XingJi = Utils.GetEnumValue<EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi>(Utils.GetFormValue("txtJiuDianXingJi"), EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi.None);
            info.JianJie = Utils.GetFormValue(txtJianJie.UniqueID);
            info.BeiZhu = Utils.GetFormValue(txtBeiZhu.UniqueID);
            info.IsTuiJian = Utils.GetFormValue("txtIsTuiJian") == "1";
            info.IsHeTong = Utils.GetFormValue("txtIsHeTong") == "1";
            info.IsQianDan = Utils.GetFormValue("txtIsQianDan") == "1";
            info.IsMianYi = Utils.GetFormValue("txtIsMianYi") == "1";
            info.JieSuanFangShi = Utils.GetEnumValue<EyouSoft.Model.EnumType.GysStructure.JieSuanFangShi>(Utils.GetFormValue("txtJieSuanFangShi"), EyouSoft.Model.EnumType.GysStructure.JieSuanFangShi.现付);
            
            info.GuaZhangQiXian = Utils.GetFormValue(txtGuaZhangQiXian.UniqueID);
            info.Lxrs = GysLxr1.Lxrs;
            info.HeTongs = GysHeTong1.HeTongs;
            info.JiuDian.BaoJias = GysJiuDianBaoJia1.BaoJias;
            info.OperatorId = SiteUserInfo.UserId;
            info.CompanyId = SiteUserInfo.CompanyId;
            info.LeiXing = EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店;
            info.LngType = LngType;

            info.JiuDian.FuJians = GetFuJians();

            return info;
        }

        /// <summary>
        /// get fujians
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MFuJianInfo> GetFuJians()
        {
            IList<EyouSoft.Model.HGysStructure.MFuJianInfo> items = new List<EyouSoft.Model.HGysStructure.MFuJianInfo>();

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
                    items.Add(new EyouSoft.Model.HGysStructure.MFuJianInfo() { FilePath = file.FilePath });
                }
            }

            return items;
        }
        #endregion
    }
}
