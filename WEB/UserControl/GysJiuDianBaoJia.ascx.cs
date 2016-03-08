using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class GysJiuDianBaoJia : System.Web.UI.UserControl
    {
        IList<EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo> _BaoJias = null;
        public IList<EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo> BaoJias
        {
            get { return GetFormInfo(); }
            set { _BaoJias = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_BaoJias != null && _BaoJias.Count > 0)
            {
                foreach (var item in _BaoJias)
                {
                    if (item.BinKeLeiXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.单人 
                        && item.TuanXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.合同价)
                    {
                        txtJDJGNTP.Value = item.PJiaGe.ToString("F2");
                        txtJDJGNTPBZ.Value = item.PBeiZhu;
                        txtJDJGNTD.Value = item.DJiaGe.ToString("F2");
                        txtJDJGNTDBZ.Value = item.DBeiZhu;
                        txtJDJGNTW.Value = item.WJiaGe.ToString("F2");
                        txtJDJGNTWBZ.Value = item.WBeiZhu;
                    }

                    if (item.BinKeLeiXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.单人
                        && item.TuanXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.零售价)
                    {
                        txtJDJGNSP.Value = item.PJiaGe.ToString("F2");
                        txtJDJGNSPBZ.Value = item.PBeiZhu;
                        txtJDJGNSD.Value = item.DJiaGe.ToString("F2");
                        txtJDJGNSDBZ.Value = item.DBeiZhu;
                        txtJDJGNSW.Value = item.WJiaGe.ToString("F2");
                        txtJDJGNSWBZ.Value = item.WBeiZhu;
                    }

                    if (item.BinKeLeiXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.双人
                        && item.TuanXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.合同价)
                    {
                        txtJDJGWTP.Value = item.PJiaGe.ToString("F2");
                        txtJDJGWTPBZ.Value = item.PBeiZhu;
                        txtJDJGWTD.Value = item.DJiaGe.ToString("F2");
                        txtJDJGWTDBZ.Value = item.DBeiZhu;
                        txtJDJGWTW.Value = item.WJiaGe.ToString("F2");
                        txtJDJGWTWBZ.Value = item.WBeiZhu;
                    }

                    if (item.BinKeLeiXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.双人
                        && item.TuanXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.零售价)
                    {
                        txtJDJGWSP.Value = item.PJiaGe.ToString("F2");
                        txtJDJGWSPBZ.Value = item.PBeiZhu;
                        txtJDJGWSD.Value = item.DJiaGe.ToString("F2");
                        txtJDJGWSDBZ.Value = item.DBeiZhu;
                        txtJDJGWSW.Value = item.WJiaGe.ToString("F2");
                        txtJDJGWSWBZ.Value = item.WBeiZhu;
                    }

                    if (item.BinKeLeiXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.三人
                        && item.TuanXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.合同价)
                    {
                        txtJDJGSHP.Value = item.PJiaGe.ToString("F2");
                        txtJDJGSHPBZ.Value = item.PBeiZhu;
                        txtJDJGSHD.Value = item.DJiaGe.ToString("F2");
                        txtJDJGSHDBZ.Value = item.DBeiZhu;
                        txtJDJGSHW.Value = item.WJiaGe.ToString("F2");
                        txtJDJGSHWBZ.Value = item.WBeiZhu;
                    }

                    if (item.BinKeLeiXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.三人
                        && item.TuanXing == EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.零售价)
                    {
                        txtJDJGSLP.Value = item.PJiaGe.ToString("F2");
                        txtJDJGSLPBZ.Value = item.PBeiZhu;
                        txtJDJGSLD.Value = item.DJiaGe.ToString("F2");
                        txtJDJGSLDBZ.Value = item.DBeiZhu;
                        txtJDJGSLW.Value = item.WJiaGe.ToString("F2");
                        txtJDJGSLWBZ.Value = item.WBeiZhu;
                    }
                }
            }
        }

        public IList<EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo> GetFormInfo()
        {
            IList<EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo> items = new List<EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo>();
            var item1 = new EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo();
            item1.BinKeLeiXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.单人;
            item1.TuanXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.合同价;
            item1.PJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGNTP.UniqueID));
            item1.PBeiZhu = Utils.GetFormValue(txtJDJGNTPBZ.UniqueID);
            item1.DJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGNTD.UniqueID));
            item1.DBeiZhu = Utils.GetFormValue(txtJDJGNTDBZ.UniqueID);
            item1.WJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGNTW.UniqueID));
            item1.WBeiZhu = Utils.GetFormValue(txtJDJGNTWBZ.UniqueID);

            var item2 = new EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo();
            item2.BinKeLeiXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.单人;
            item2.TuanXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.零售价;
            item2.PJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGNSP.UniqueID));
            item2.PBeiZhu = Utils.GetFormValue(txtJDJGNSPBZ.UniqueID);
            item2.DJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGNSD.UniqueID));
            item2.DBeiZhu = Utils.GetFormValue(txtJDJGNSDBZ.UniqueID);
            item2.WJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGNSW.UniqueID));
            item2.WBeiZhu = Utils.GetFormValue(txtJDJGNSWBZ.UniqueID);

            var item3 = new EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo();
            item3.BinKeLeiXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.双人;
            item3.TuanXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.合同价;
            item3.PJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGWTP.UniqueID));
            item3.PBeiZhu = Utils.GetFormValue(txtJDJGWTPBZ.UniqueID);
            item3.DJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGWTD.UniqueID));
            item3.DBeiZhu = Utils.GetFormValue(txtJDJGWTDBZ.UniqueID);
            item3.WJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGWTW.UniqueID));
            item3.WBeiZhu = Utils.GetFormValue(txtJDJGWTWBZ.UniqueID);

            var item4 = new EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo();
            item4.BinKeLeiXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.双人;
            item4.TuanXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.零售价;
            item4.PJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGWSP.UniqueID));
            item4.PBeiZhu = Utils.GetFormValue(txtJDJGWSPBZ.UniqueID);
            item4.DJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGWSD.UniqueID));
            item4.DBeiZhu = Utils.GetFormValue(txtJDJGWSDBZ.UniqueID);
            item4.WJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGWSW.UniqueID));
            item4.WBeiZhu = Utils.GetFormValue(txtJDJGWSWBZ.UniqueID);

            var item5 = new EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo();
            item5.BinKeLeiXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.三人;
            item5.TuanXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.合同价;
            item5.PJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGSHP.UniqueID));
            item5.PBeiZhu = Utils.GetFormValue(txtJDJGSHPBZ.UniqueID);
            item5.DJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGSHD.UniqueID));
            item5.DBeiZhu = Utils.GetFormValue(txtJDJGSHDBZ.UniqueID);
            item5.WJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGSHW.UniqueID));
            item5.WBeiZhu = Utils.GetFormValue(txtJDJGSHWBZ.UniqueID);

            var item6 = new EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo();
            item6.BinKeLeiXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType.三人;
            item6.TuanXing = EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType.零售价;
            item6.PJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGSLP.UniqueID));
            item6.PBeiZhu = Utils.GetFormValue(txtJDJGSLPBZ.UniqueID);
            item6.DJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGSLD.UniqueID));
            item6.DBeiZhu = Utils.GetFormValue(txtJDJGSLDBZ.UniqueID);
            item6.WJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtJDJGSLW.UniqueID));
            item6.WBeiZhu = Utils.GetFormValue(txtJDJGSLWBZ.UniqueID);

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);
            items.Add(item6);

            return items;
        }
    }
}