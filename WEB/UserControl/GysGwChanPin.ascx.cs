using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class GysGwChanPin : System.Web.UI.UserControl
    {
        IList<EyouSoft.Model.HGysStructure.MGouWuChanPinInfo> _ChanPins = null;
        public IList<EyouSoft.Model.HGysStructure.MGouWuChanPinInfo> ChanPins
        {
            get { return GetFormInfo(); }
            set { _ChanPins = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_ChanPins != null && _ChanPins.Count > 0)
            {
                rpt.DataSource = _ChanPins;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        IList<EyouSoft.Model.HGysStructure.MGouWuChanPinInfo> GetFormInfo()
        {
            IList<EyouSoft.Model.HGysStructure.MGouWuChanPinInfo> items = new List<EyouSoft.Model.HGysStructure.MGouWuChanPinInfo>();

            string[] txtChanPinId = Utils.GetFormValues("txtChanPinId");
            string[] txtChanPinName = Utils.GetFormValues("txtChanPinName");
            string[] txtChanPinXSJE = Utils.GetFormValues("txtChanPinXSJE");
            string[] txtChanPinFDJE = Utils.GetFormValues("txtChanPinFDJE");

            if (txtChanPinId == null || txtChanPinId.Length == 0) return items;

            for (int i = 0; i < txtChanPinId.Length; i++)
            {
                if (string.IsNullOrEmpty(txtChanPinName[i])) continue;
                var item = new EyouSoft.Model.HGysStructure.MGouWuChanPinInfo();

                item.ChanPinId = txtChanPinId[i];
                item.FanDianJinE = Utils.GetDecimal(txtChanPinFDJE[i]);
                item.Name = txtChanPinName[i];
                item.XiaoShouJinE = Utils.GetDecimal(txtChanPinXSJE[i]);                

                items.Add(item);
            }

            return items;
        }
    }
}