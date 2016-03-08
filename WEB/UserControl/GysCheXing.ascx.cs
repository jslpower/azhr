using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class GysCheXing : System.Web.UI.UserControl
    {
        IList<EyouSoft.Model.HGysStructure.MCheXingInfo> _CheXings = null;
        public IList<EyouSoft.Model.HGysStructure.MCheXingInfo> CheXings
        {
            get { return GetFormInfo(); }
            set { _CheXings = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_CheXings != null && _CheXings.Count > 0)
            {
                rpt.DataSource = _CheXings;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        IList<EyouSoft.Model.HGysStructure.MCheXingInfo> GetFormInfo()
        {
            IList<EyouSoft.Model.HGysStructure.MCheXingInfo> items = new List<EyouSoft.Model.HGysStructure.MCheXingInfo>();

            string[] txtCheXingId = Utils.GetFormValues("txtCheXingId");
            string[] txtCheXingName = Utils.GetFormValues("txtCheXingName");
            string[] txtCheXingZuoWeiShu = Utils.GetFormValues("txtCheXingZuoWeiShu");
            string[] txtCheXingDanJiaJiShu = Utils.GetFormValues("txtCheXingDanJiaJiShu");
            string[] txtCheXingBeiZhu = Utils.GetFormValues("txtCheXingBeiZhu");
            string[] txtFile1 = Utils.GetFormValues("i_chexing_upload_hidden");
            string[] txtFile2 = Utils.GetFormValues("i_chexing_upload_hidden_y");

            if (txtCheXingId == null || txtCheXingId.Length == 0) return items;

            for (int i = 0; i < txtCheXingId.Length; i++)
            {
                if (string.IsNullOrEmpty(txtCheXingName[i])) continue;
                var item = new EyouSoft.Model.HGysStructure.MCheXingInfo();

                item.BeiZhu = txtCheXingBeiZhu[i];
                item.CheXingId = txtCheXingId[i];
                item.DanJiaJiShu = Utils.GetDecimal(txtCheXingDanJiaJiShu[i]);
                item.FuJian = new EyouSoft.Model.HGysStructure.MFuJianInfo();
                item.Name = txtCheXingName[i];
                item.ZuoWeiShu = Utils.GetInt(txtCheXingZuoWeiShu[i]);

                if (!string.IsNullOrEmpty(txtFile1[i]) || !string.IsNullOrEmpty(txtFile2[i]))
                {
                    if (!string.IsNullOrEmpty(txtFile1[i]))
                    {
                        string[] arr = txtFile1[i].Split('|');
                        if (arr != null && arr.Length == 2)
                            item.FuJian.FilePath = arr[1];
                    }
                    else if (!string.IsNullOrEmpty(txtFile2[i]))
                    {
                        item.FuJian.FilePath = txtFile2[i];
                    }
                }

                items.Add(item);
            }

            return items;
        }

        protected string GetChaKanFuJian(object filepath)
        {
            if (filepath == null) return string.Empty;

            string _filepath = filepath.ToString();

            if (string.IsNullOrEmpty(_filepath)) return string.Empty;

            return "<div class='chakanhetongfujian'><a href='" + _filepath + "' target='_blank'>查看附件</a></div>";
        }
    }
}