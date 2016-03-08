using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class GysHeTong : System.Web.UI.UserControl
    {
        IList<EyouSoft.Model.HGysStructure.MHeTongInfo> _HeTongs=null;
        public IList<EyouSoft.Model.HGysStructure.MHeTongInfo> HeTongs
        {
            get { return GetFormInfo(); }
            set { _HeTongs = value; }
        }

        public bool IsBaoDi { get; set; }
        public bool IsJiangLi { get; set; }

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

        IList<EyouSoft.Model.HGysStructure.MHeTongInfo> GetFormInfo()
        {
            IList<EyouSoft.Model.HGysStructure.MHeTongInfo> items = new List<EyouSoft.Model.HGysStructure.MHeTongInfo>();

            string[] txtHeTongId = Utils.GetFormValues("txtHeTongId");
            string[] txtHeTongSTime = Utils.GetFormValues("txtHeTongSTime");
            string[] txtHeTongETime = Utils.GetFormValues("txtHeTongETime");
            string[] txtHeTongBaoDi = Utils.GetFormValues("txtHeTongBaoDi");
            string[] txtHeTongJiangLi = Utils.GetFormValues("txtHeTongJiangLi");
            string[] txtHeTongBeiZhu = Utils.GetFormValues("txtHeTongBeiZhu");
            string[] txtFile1 = Utils.GetFormValues("i_hetong_upload_hidden");
            string[] txtFile2 = Utils.GetFormValues("i_hetong_upload_hidden_y");

            if (txtHeTongId == null || txtHeTongId.Length == 0) return items;

            for (int i = 0; i < txtHeTongId.Length; i++)
            {
                var item = new EyouSoft.Model.HGysStructure.MHeTongInfo();

                item.BaoDi = Utils.GetInt(txtHeTongBaoDi[i]);
                item.BeiZhu = txtHeTongBeiZhu[i];
                item.ETime = Utils.GetDateTimeNullable(txtHeTongETime[i]);
                item.FilePath = string.Empty;
                item.HeTongId = txtHeTongId[i];
                item.JiangLi = Utils.GetInt(txtHeTongJiangLi[i]);
                item.STime = Utils.GetDateTimeNullable(txtHeTongSTime[i]);

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

                if (item.BaoDi == 0 
                    && string.IsNullOrEmpty(item.BeiZhu) 
                    && !item.ETime.HasValue 
                    && string.IsNullOrEmpty(item.FilePath) 
                    && item.JiangLi == 0 
                    && !item.STime.HasValue) continue;

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