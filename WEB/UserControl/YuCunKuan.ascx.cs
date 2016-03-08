using System;

namespace EyouSoft.Web.UserControl
{
    using System.Collections.Generic;

    using EyouSoft.Common;

    public partial class YuCunKuan : System.Web.UI.UserControl
    {
        IList<EyouSoft.Model.HGysStructure.MYuCunKuanInfo> _YuCunKuans = null;
        public IList<EyouSoft.Model.HGysStructure.MYuCunKuanInfo> YuCunKuans
        {
            get { return GetFormInfo(); }
            set { _YuCunKuans = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_YuCunKuans != null && _YuCunKuans.Count > 0)
            {
                rpt.DataSource = _YuCunKuans;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        IList<EyouSoft.Model.HGysStructure.MYuCunKuanInfo> GetFormInfo()
        {
            var items = new List<EyouSoft.Model.HGysStructure.MYuCunKuanInfo>();

            string[] txtYuCunId = Utils.GetFormValues("txtYuCunId");
            string[] txtTime = Utils.GetFormValues("txtTime");
            string[] txtJinE = Utils.GetFormValues("txtJinE");
            string[] txtBeiZhu = Utils.GetFormValues("txtBeiZhu");

            if (txtYuCunId == null || txtYuCunId.Length == 0) return items;

            for (int i = 0; i < txtYuCunId.Length; i++)
            {
                var item = new EyouSoft.Model.HGysStructure.MYuCunKuanInfo();

                item.JinE = Utils.GetDecimal(txtJinE[i]);
                item.BeiZhu = txtBeiZhu[i];
                item.YuCunId = txtYuCunId[i];
                item.Time = Utils.GetDateTime(txtTime[i]);

                if (item.JinE == 0 || item.Time==DateTime.MinValue) continue;

                items.Add(item);
            }

            return items;
        }
    }
}