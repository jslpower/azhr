using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class TuanDiJie : System.Web.UI.UserControl
    {
        public IList<EyouSoft.Model.HTourStructure.MTourDiJie> DiJies;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DiJies != null)
            {
                this.rptList.DataSource = DiJies;
                this.rptList.DataBind();
                this.PHDefault.Visible = false;
            }
        }

        public IList<EyouSoft.Model.HTourStructure.MTourDiJie> GetFormInfo()
        {
            IList<EyouSoft.Model.HTourStructure.MTourDiJie> items = new List<EyouSoft.Model.HTourStructure.MTourDiJie>();
            string[] cityid = Utils.GetFormValues("hiddijiecityid");
            string[] cityname = Utils.GetFormValues("txtdijiecity");
            string[] dijieid = Utils.GetFormValues("hiddijieid");
            string[] dijiename = Utils.GetFormValues("txtdijiename");
            string[] contact = Utils.GetFormValues("dijie_contact");
            string[] tel = Utils.GetFormValues("dijie_tel");
            string[] remark = Utils.GetFormValues("dijie_remark");
            if (cityid.Length > 0 && cityid.Length == cityname.Length && cityname.Length == dijieid.Length && dijieid.Length == dijiename.Length && dijiename.Length == contact.Length && contact.Length == tel.Length && tel.Length == remark.Length)
            {
                for (int i = 0; i < dijieid.Length; i++)
                {
                    if (!string.IsNullOrEmpty(dijieid[i]))
                    {
                        EyouSoft.Model.HTourStructure.MTourDiJie model = new EyouSoft.Model.HTourStructure.MTourDiJie();
                        model.CityId = Utils.GetInt(cityid[i]);
                        model.CityName = cityname[i];
                        model.Contact = contact[i];
                        model.DiJieId = dijieid[i];
                        model.DiJieName = dijiename[i];
                        model.Phone = tel[i];
                        model.Remark = remark[i];
                        items.Add(model);
                    }
                }
            }
            return items;
        }
    }
}