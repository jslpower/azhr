using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class GysLxr : System.Web.UI.UserControl
    {
        IList<EyouSoft.Model.HGysStructure.MLxrInfo> _Lxrs = null;
        public IList<EyouSoft.Model.HGysStructure.MLxrInfo> Lxrs
        {
            get { return GetFormInfo(); }
            set { _Lxrs = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_Lxrs != null && _Lxrs.Count > 0)
            {
                rpt.DataSource = _Lxrs;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        IList<EyouSoft.Model.HGysStructure.MLxrInfo> GetFormInfo()
        {
            IList<EyouSoft.Model.HGysStructure.MLxrInfo> items = new List<EyouSoft.Model.HGysStructure.MLxrInfo>();

            string[] txtLxrId = Utils.GetFormValues("txtLxrId");
            string[] txtLxrName = Utils.GetFormValues("txtLxrName");
            string[] txtLxrZhiWu = Utils.GetFormValues("txtLxrZhiWu");
            string[] txtLxrTelephone = Utils.GetFormValues("txtLxrTelephone");
            string[] txtLxrMobile = Utils.GetFormValues("txtLxrMobile");
            string[] txtLxrBirthday = Utils.GetFormValues("txtLxrBirthday");
            string[] txtLxrBitrhdayTiXing = Utils.GetFormValues("txtLxrBitrhdayTiXing1");
            string[] txtLxrQQ = Utils.GetFormValues("txtLxrQQ");
            string[] txtLxrEmail = Utils.GetFormValues("txtLxrEmail");
            string[] txtLxrMSN = Utils.GetFormValues("txtLxrMSN");
            string[] txtLxrFax = Utils.GetFormValues("txtLxrFax");

            if (txtLxrId == null || txtLxrId.Length == 0) return items;

            for (int i = 0; i < txtLxrId.Length; i++)
            {
                if (string.IsNullOrEmpty(txtLxrName[i])) continue;
                var item = new EyouSoft.Model.HGysStructure.MLxrInfo();
                item.Birthday = Utils.GetDateTimeNullable(txtLxrBirthday[i]);
                item.Email = txtLxrEmail[i];
                item.Fax = txtLxrFax[i];
                item.IsTiXing = txtLxrBitrhdayTiXing[i] == "1";
                item.LxrId = txtLxrId[i];
                item.Mobile = txtLxrMobile[i];
                item.MSN = txtLxrMSN[i];
                item.Name = txtLxrName[i];
                item.QQ = txtLxrQQ[i];
                item.Telephone = txtLxrTelephone[i];
                item.ZhiWu = txtLxrZhiWu[i];

                items.Add(item);
            }

            return items;
        }

        protected string IsTiXing(object isTiXing)
        {
            if (isTiXing == null) return string.Empty;
            if ((bool)isTiXing) return "checked='checked'";
            return string.Empty;
        }
    }
}