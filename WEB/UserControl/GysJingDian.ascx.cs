using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class GysJingDian : System.Web.UI.UserControl
    {
        IList<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo> _JingDians = null;
        public IList<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo> JingDians
        {
            get { return GetFormInfo(); }
            set { _JingDians = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (_JingDians != null && _JingDians.Count > 0)
            {
                rpt.DataSource = _JingDians;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }
        }

        IList<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo> GetFormInfo()
        {
            IList<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo> items = new List<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo>();
            string[] txtJingDianId = Utils.GetFormValues("txtJingDianId");
            string[] txtJingDianName = Utils.GetFormValues("txtJingDianName");
            string[] txtJingDianXingJi = Utils.GetFormValues("txtJingDianXingJi");
            string[] txtJingDianWangZhi = Utils.GetFormValues("txtJingDianWangZhi");
            string[] txtJingDianYouLanShiJian = Utils.GetFormValues("txtJingDianYouLanShiJian");
            string[] txtJingDianTelephone = Utils.GetFormValues("txtJingDianTelephone");
            string[] txtJingDianMiaoShu = Utils.GetFormValues("txtJingDianMiaoShu");
            string[] txtFile1 = Utils.GetFormValues("i_jingdian_upload_hidden");
            string[] txtFile2 = Utils.GetFormValues("i_jingdian_upload_hidden_y");
            string[] txtIsTuiJian = Utils.GetFormValues("txtJingDianTuiJian1");
            string[] txtIsXiu = Utils.GetFormValues("txtIsXiu1");

            if (txtJingDianId == null || txtJingDianId.Length == 0) return items;

            for (int i = 0; i < txtJingDianId.Length; i++)
            {
                if (string.IsNullOrEmpty(txtJingDianName[i])) continue;

                var item = new EyouSoft.Model.HGysStructure.MJingDianJingDianInfo();
                item.FuJian = new EyouSoft.Model.HGysStructure.MFuJianInfo();
                item.IsTuiJian = txtIsTuiJian[i] == "1";
                item.IsXiu = txtIsXiu[i] == "1";
                item.JianJie = txtJingDianMiaoShu[i];
                item.JingDianId = txtJingDianId[i];
                item.Name = txtJingDianName[i];
                item.Telephone = txtJingDianTelephone[i];
                item.WangZhi = txtJingDianWangZhi[i];
                item.XingJi = Utils.GetEnumValue<EyouSoft.Model.EnumType.GysStructure.JingDianXingJi>(txtJingDianXingJi[i], EyouSoft.Model.EnumType.GysStructure.JingDianXingJi.None);
                item.YouLanShiJian = txtJingDianYouLanShiJian[i];

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

            return "<div class='chakanjingdianfujian'><a href='" + _filepath + "' target='_blank'>查看附件</a></div>";
        }
    }
}