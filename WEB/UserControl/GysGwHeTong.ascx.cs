using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class GysGwHeTong : System.Web.UI.UserControl
    {
        IList<EyouSoft.Model.HGysStructure.MGouWuHeTongInfo> _HeTongs = null;
        public IList<EyouSoft.Model.HGysStructure.MGouWuHeTongInfo> HeTongs
        {
            get { return GetFormInfo(); }
            set { _HeTongs = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_HeTongs != null && _HeTongs.Count > 0)
            {
                rpt.DataSource = _HeTongs;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        IList<EyouSoft.Model.HGysStructure.MGouWuHeTongInfo> GetFormInfo()
        {
            IList<EyouSoft.Model.HGysStructure.MGouWuHeTongInfo> items = new List<EyouSoft.Model.HGysStructure.MGouWuHeTongInfo>();

            string[] txtHeTongId = Utils.GetFormValues("txtHeTongId");
            string[] txtHeTongETime = Utils.GetFormValues("txtHeTongETime");
            string[] txtHeTongGuoJi = Utils.GetFormValues("txtHeTongGuoJi");
            string[] txtHeTongLS = Utils.GetFormValues("txtHeTongLS");
            string[] txtHeTongBD = Utils.GetFormValues("txtHeTongBD");
            string[] txtHeTongCR = Utils.GetFormValues("txtHeTongCR");
            string[] txtHeTongET = Utils.GetFormValues("txtHeTongET");
            string[] txtHeTongIsQiYong = Utils.GetFormValues("txtHeTongIsQiYong1");

            string[] txtFile1 = Utils.GetFormValues("i_gwhetong_upload_hidden");
            string[] txtFile2 = Utils.GetFormValues("i_gwhetong_upload_hidden_y");

            if (txtHeTongId == null || txtHeTongId.Length == 0) return items;

            for (int i = 0; i < txtHeTongId.Length; i++)
            {
                var item = new EyouSoft.Model.HGysStructure.MGouWuHeTongInfo();

                item.BaoDiJinE = Utils.GetDecimal(txtHeTongBD[i]);
                item.ETime = Utils.GetDateTimeNullable(txtHeTongETime[i]);
                item.GuoJi = txtHeTongGuoJi[i];
                item.HeTongId = txtHeTongId[i];
                item.IsQiYong = txtHeTongIsQiYong[i] == "1";
                item.LiuShui = Utils.GetDecimal(txtHeTongLS[i]);
                item.RenTouCR = Utils.GetDecimal(txtHeTongCR[i]);
                item.RenTouET = Utils.GetDecimal(txtHeTongET[i]);               

                if (!string.IsNullOrEmpty(txtFile1[i]) || !string.IsNullOrEmpty(txtFile2[i]))
                {
                    if (!string.IsNullOrEmpty(txtFile1[i]))
                    {
                        string[] arr = txtFile1[i].Split('|');
                        if (arr != null && arr.Length == 2)
                            item.FilePath = arr[1];
                    }
                    else if (!string.IsNullOrEmpty(txtFile2[i]))
                    {
                        item.FilePath = txtFile2[i];
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

            return "<div class='chakangwhetongfujian'><a href='" + _filepath + "' target='_blank'>查看附件</a></div>";
        }
    }
}