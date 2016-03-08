using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.QC
{
    public partial class CheDuiZhiJianEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        string ZhiJianId = string.Empty;

        protected string DriverService = string.Empty;
        protected string FindWay = string.Empty;
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
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质检中心_车队质检_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质检中心_车队质检_修改);
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
                bllRetCode = new EyouSoft.BLL.BQC.BCarTeamQC().AddCarTeamQC(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.BQC.BCarTeamQC().UpdateCarTeamQC(info);
            }

            if (bllRetCode) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));

        }
        private void BindRadio(string findway, string service)
        {
            List<EnumObj> list = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.FindWay));
            List<EnumObj> listservice = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.DriverService));

            System.Text.StringBuilder findwayStr = new System.Text.StringBuilder();
            System.Text.StringBuilder driverServiceStr = new System.Text.StringBuilder();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (findway == list[i].Text)
                    {
                        findwayStr.AppendFormat("<label><input type='radio' name='radiofindway' checked='checked' value='{0}' />{1}</label>", list[i].Value, list[i].Text);
                    }
                    else
                    {
                        findwayStr.AppendFormat("<label><input type='radio' name='radiofindway' value='{0}' />{1}</label>", list[i].Value, list[i].Text);
                    }
                }
            }
            if (listservice != null && listservice.Count > 0)
            {
                for (int i = 0; i < listservice.Count; i++)
                {
                    if (service == listservice[i].Text)
                    {
                        driverServiceStr.AppendFormat("<label><input type='radio' name='radioservice' checked='checked' value='{0}' />{1}</label>", listservice[i].Value, listservice[i].Text);
                    }
                    else
                    {
                        driverServiceStr.AppendFormat("<label><input type='radio' name='radioservice' value='{0}' />{1}</label>", listservice[i].Value, listservice[i].Text);
                    }
                }
            }
            this.FindWay = findwayStr.ToString();
            this.DriverService = driverServiceStr.ToString();

        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {

            if (string.IsNullOrEmpty(ZhiJianId))
            {
                BindRadio("", "");
                return;
            }
            else
            {

                var info = new EyouSoft.BLL.BQC.BCarTeamQC().GetCarTeamQCModel(ZhiJianId);

                if (info == null) RCWE("异常请求");

                BindRadio(info.FindWay.ToString(), info.DriverService.ToString());
                txtTuanHaoXuanYong.TourId = info.TourId;
                txtTuanHaoXuanYong.TourCode = info.TourCode;
                txtCheDuiName.Value = info.CarTeamName;
                txtCheHao.Value = info.CarCode;
                txtRiQi.Value = info.QCTime.ToString("yyyy-MM-dd");
                txtLingDuiName.Value = info.LeaderName;
                txtLingDuiTelephone.Value = info.LeaderTel;
                txtJianYi.Value = info.Advice;

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
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.QC.MCarTeamQC GetFormInfo()
        {
            var info = new EyouSoft.Model.QC.MCarTeamQC();

            info.QCID = ZhiJianId;
            info.Advice = Utils.GetFormValue(txtJianYi.UniqueID);
            info.CarCode = Utils.GetFormValue(txtCheHao.UniqueID);
            info.CarTeamName = Utils.GetFormValue(txtCheDuiName.UniqueID);
            info.CompanyId = SiteUserInfo.CompanyId;
            info.DriverService = Utils.GetEnumValue<EyouSoft.Model.EnumType.CrmStructure.DriverService>(Utils.GetFormValue("radioservice"), EyouSoft.Model.EnumType.CrmStructure.DriverService.满意);
            info.FileList = GetFuJians();
            info.FindWay = Utils.GetEnumValue<EyouSoft.Model.EnumType.CrmStructure.FindWay>(Utils.GetFormValue("radiofindway"), EyouSoft.Model.EnumType.CrmStructure.FindWay.认路);
            info.IssueTime = DateTime.Now;
            info.LeaderName = Utils.GetFormValue(txtLingDuiName.UniqueID);
            info.LeaderTel = Utils.GetFormValue(txtLingDuiTelephone.UniqueID);
            info.Operator = SiteUserInfo.Name;
            info.OperatorDeptId = SiteUserInfo.DeptId;
            info.OperatorId = SiteUserInfo.UserId;
            info.QCTime = Utils.GetDateTime(txtRiQi.UniqueID, DateTime.Now);
            info.TourCode = Utils.GetFormValue(txtTuanHaoXuanYong.ClientNameTourCode);
            info.TourId = Utils.GetFormValue(txtTuanHaoXuanYong.ClientNameTourId);

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
