using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Guide
{
    public partial class DaoYouEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        string DaoYouId = string.Empty;

        protected string Gender = "0";
        protected string JiBie = "0";
        protected string LeiBie = "";
        protected string ZhiWeiLeiXing = "0";
        protected string NianShen = "0";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region  初始化上传控件
            this.uc_Photo.CompanyID = SiteUserInfo.CompanyId;
            this.uc_Photo.IsUploadMore = false;
            this.uc_Photo.IsUploadSelf = true;
            this.uc_Photo.FileTypes = "*.jpg;*.jpeg;*.gif;*.png;*.bmp;";
            #endregion

            DaoYouId = Utils.GetQueryStringValue("daoyouid");

            if (string.IsNullOrEmpty(DaoYouId)) RCWE("异常请求");

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
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_修改);
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            if (string.IsNullOrEmpty(DaoYouId)) return;

            var info = new EyouSoft.BLL.ComStructure.BDaoYou().GetInfo(DaoYouId);
            if (info == null) RCWE("异常请求");

            txtXingMing.Value = info.XingMing;
            txtYuZhong.Value = info.YuZhong;
            txtShenFenZhengHao.Value = info.ShenFenZhengHao;
            txtDaoYouZhengHao.Value = info.DaoYouZhengHao;
            txtLingDuiZhengHao.Value = info.LingDuiZhengHao;
            txtGuaKaoDanWei.Value = info.GuaKaoDanWei;
            txtTelephone.Value = info.Telephone;
            txtShouJiHao.Value = info.ShouJiHao;
            txtQQ.Value = info.QQ;
            txtEmail.Value = info.Email;
            txtMSN.Value = info.MSN;
            txtJiaTingDiZhi.Value = info.JiaTingDiZhi;
            txtJiaTingTelephone.Value = info.JiaTingTelephone;
            txtXingGeTeDian.Value = info.XingGeTeDian;
            txtShanChangXianLu.Value = info.ShanChangXianLu;
            txtKeHuPingJia.Value = info.KeHuPingJia;
            txtTeChang.Value = info.TeChang;
            txtBeiZhu.Value = info.BeiZhu;

            Gender = ((int)info.Gender).ToString();
            JiBie = ((int)info.JiBie).ToString();
            ZhiWeiLeiXing = ((int)info.ZhiWeiLeiXing).ToString();
            NianShen = info.IsNianShen ? "1" : "0";
            if (!string.IsNullOrEmpty(info.ZhaoPianFilePath))
            {
                lbTxtphoto.Text = string.Format("<span  class='upload_filename'><a href='{0}' target='_blank'>查看</a><a href=\"javascript:void(0)\"  onclick=\"UserEdit.RemoveFile(this)\" title='删除照片'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" id=\"hideFileInfo_photo\" name=\"hideFileInfo_photo\" value=\"{0}\"/></span>", info.ZhaoPianFilePath);
            }

            if (info.LeiBies != null && info.LeiBies.Count > 0)
            {
                string s = string.Empty;
                foreach (var item in info.LeiBies)
                {
                    s += string.Format("{0},", (int)item);
                }
                if (!string.IsNullOrEmpty(s)) LeiBie = s.TrimEnd(',');
            }
        }

        /// <summary>
        /// baocun 
        /// </summary>
        void BaoCun()
        {
            if (string.IsNullOrEmpty(DaoYouId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            var info = GetFormInfo();

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(DaoYouId))
            {
                //bllRetCode = new EyouSoft.BLL.ComStructure.BDaoYou().Insert(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.ComStructure.BDaoYou().Update(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.ComStructure.MDaoYouInfo GetFormInfo()
        {
            var info = new EyouSoft.Model.ComStructure.MDaoYouInfo();

            info.BeiZhu = Utils.GetFormValue(txtBeiZhu.UniqueID);
            info.CompanyId = SiteUserInfo.CompanyId;
            info.DaoYouId = DaoYouId;
            info.DaoYouZhengHao = Utils.GetFormValue(txtDaoYouZhengHao.UniqueID);
            info.Email = Utils.GetFormValue(txtEmail.UniqueID);
            info.Gender = Utils.GetEnumValue<EyouSoft.Model.EnumType.GovStructure.Gender>(Utils.GetFormValue("txtGender"), EyouSoft.Model.EnumType.GovStructure.Gender.男);
            info.GuaKaoDanWei = Utils.GetFormValue(txtGuaKaoDanWei.UniqueID);
            info.IsNianShen = Utils.GetFormValue("txtIsNianShen") == "1";
            info.IssueTime = DateTime.Now;
            info.JiaTingDiZhi = Utils.GetFormValue(txtJiaTingDiZhi.UniqueID);
            info.JiaTingTelephone = Utils.GetFormValue(txtJiaTingTelephone.UniqueID);
            info.JiBie = Utils.GetEnumValue<EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie>(Utils.GetFormValue("txtJiBie"), EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie.初级);
            info.KeHuPingJia = Utils.GetFormValue(txtKeHuPingJia.UniqueID);
            info.LeiBies = new List<EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie>() ;
            info.LingDuiZhengHao = Utils.GetFormValue(txtLingDuiZhengHao.UniqueID);
            info.MSN = Utils.GetFormValue(txtMSN.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.Pwd = null;
            info.QQ = Utils.GetFormValue(txtQQ.UniqueID);
            info.ShanChangXianLu = Utils.GetFormValue(txtShanChangXianLu.UniqueID);
            info.ShenFenZhengHao = Utils.GetFormValue(txtShenFenZhengHao.UniqueID);
            info.ShouJiHao = Utils.GetFormValue(txtShouJiHao.UniqueID);
            info.TeChang = Utils.GetFormValue(txtTeChang.UniqueID);
            info.Telephone = Utils.GetFormValue(txtTelephone.UniqueID);
            info.Username = Utils.GetFormValue("txtUsername");
            info.XingGeTeDian = Utils.GetFormValue(txtXingGeTeDian.UniqueID);
            info.XingMing = Utils.GetFormValue(txtXingMing.UniqueID);
            info.YuZhong = Utils.GetFormValue(txtYuZhong.UniqueID);
            info.ZhiWeiLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.ComStructure.ZhiWeiLeiXing>(Utils.GetFormValue("txtZhiWeiLeiXing"), EyouSoft.Model.EnumType.ComStructure.ZhiWeiLeiXing.全职);
            string filePageMei = Utils.GetFormValue(uc_Photo.ClientHideID);
            if (!string.IsNullOrEmpty(filePageMei))
                info.ZhaoPianFilePath = filePageMei.Split('|')[1];
            else
                info.ZhaoPianFilePath = Utils.GetFormValue("hideFileInfo_photo");

            string s = Utils.GetFormValue("txtLeiBie");
            if (!string.IsNullOrEmpty(s))
            {
                var arr = s.Split(',');
                if (arr != null && arr.Length > 0)
                {
                    foreach (var item in arr)
                    {
                        info.LeiBies.Add(Utils.GetEnumValue<EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie>(item, EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie.全陪));
                    }
                }
            }

            /*var arr = Utils.GetFormValues("txtLeiBie");
            if (arr != null && arr.Length > 0)
            {
                foreach (var item in arr)
                {
                    info.LeiBies.Add(Utils.GetEnumValue<EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie>(item, EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie.全陪));
                }
            }*/

            return info;
        }
        #endregion
    }
}
