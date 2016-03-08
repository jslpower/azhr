using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.WebFX.UserControl
{
    public partial class SelectPriceRemark : System.Web.UI.UserControl
    {
        /// <summary>
        /// 行程备注的集合
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MQuoteJourney> SetQuoteJourneyList { get; set; }

        /// <summary>
        /// 行程备注的集合(用于计划报价)
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MTourJourney> SetTourJourneyList { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            SetData();
            base.OnPreRender(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack)
            {
                base.OnLoad(e);
            }
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        private void SetData()
        {
            string Ids = string.Empty;

            if (SetQuoteJourneyList != null && SetQuoteJourneyList.Count != 0)
            {
                var items = SetQuoteJourneyList.Where(c => c.JourneyType == EyouSoft.Model.EnumType.TourStructure.JourneyType.报价备注).ToList();
                if (items.Count != 0)
                {
                    foreach (var item in items)
                    {
                        Ids += item.SourceId.ToString() + ',';
                    }

                    this.hd_SelectPriceRemark_Id.Value = Ids.Substring(0, Ids.Length - 1);
                }
            }

            if (SetTourJourneyList != null && SetTourJourneyList.Count != 0)
            {

                var items = SetTourJourneyList.Where(c => c.JourneyType == EyouSoft.Model.EnumType.TourStructure.JourneyType.报价备注).ToList();
                if (items.Count != 0)
                {
                    foreach (var item in items)
                    {
                        Ids += item.SourceId.ToString() + ',';
                    }

                    this.hd_SelectPriceRemark_Id.Value = Ids.Substring(0, Ids.Length - 1);
                }
            }

        }


        /// <summary>
        /// 获取行程备注
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MQuoteJourney> GetQuoteJourneyList()
        {
            IList<EyouSoft.Model.HTourStructure.MQuoteJourney> list = new List<EyouSoft.Model.HTourStructure.MQuoteJourney>();


            string strOldId = Utils.GetFormValue(this.hd_SelectPriceRemark_Id.UniqueID);

            //原始的编号
            if (!string.IsNullOrEmpty(strOldId))
            {
                string[] ids = strOldId.Split(',');
                if (ids.Length >= 0)
                {

                    foreach (var s in ids)
                    {
                        if (!string.IsNullOrEmpty(s) && Utils.GetInt(s, 0) != 0)
                        {
                            EyouSoft.Model.HTourStructure.MQuoteJourney model = new EyouSoft.Model.HTourStructure.MQuoteJourney();
                            model.SourceId = Utils.GetInt(s);
                            model.JourneyType = EyouSoft.Model.EnumType.TourStructure.JourneyType.报价备注;
                            list.Add(model);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 获取计划备注
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MTourJourney> GetTourJourneyList()
        {
            IList<EyouSoft.Model.HTourStructure.MTourJourney> list = new List<EyouSoft.Model.HTourStructure.MTourJourney>();


            string strOldId = Utils.GetFormValue(this.hd_SelectPriceRemark_Id.UniqueID);

            //原始的编号
            if (!string.IsNullOrEmpty(strOldId))
            {
                string[] ids = strOldId.Split(',');
                if (ids.Length >= 0)
                {

                    foreach (var s in ids)
                    {
                        if (!string.IsNullOrEmpty(s) && Utils.GetInt(s, 0) != 0)
                        {
                            EyouSoft.Model.HTourStructure.MTourJourney model = new EyouSoft.Model.HTourStructure.MTourJourney();
                            model.SourceId = Utils.GetInt(s);
                            model.JourneyType = EyouSoft.Model.EnumType.TourStructure.JourneyType.报价备注;
                            list.Add(model);
                        }
                    }
                }
            }

            return list;
        }
    }
}