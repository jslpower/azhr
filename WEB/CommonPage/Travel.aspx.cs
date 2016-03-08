using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.CommonPage
{
    public partial class Travel : System.Web.UI.Page
    {
        EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();

        protected void Page_Load(object sender, EventArgs e)
        {
            //ajax
            string type = Utils.GetQueryStringValue("doType");

            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("submit"))
                {

                    Response.Clear();

                    EyouSoft.Model.HTourStructure.MQuote model = bll.GetQuoteModel("e2fad51d-8ca2-46e8-844c-d8fe71fa58a4");
                    model.TravelNote = Request.Form[this.txtJourney.ClientID];
                    model.QuoteJourneyList = this.SelectJourney1.GetQuoteJourneyList();


                    var a1 = this.SelectJourneySpot1.GetQuoteJourneyList();
                    foreach (var item in a1)
                    {
                        model.QuoteJourneyList.Add(item);
                    }

                    var a2 = this.SelectPriceRemark1.GetQuoteJourneyList();  
                    foreach (var item in a2) 
                    {
                         model.QuoteJourneyList.Add(item);
                    }



                    if (bll.UpdateQuote(model) == 1)
                    {
                        Response.Write(1);
                    }
                    else
                    {
                        Response.Write(0);
                    }
                    Response.End();
                }
            }


            if (!IsPostBack)
            {
                EyouSoft.Model.HTourStructure.MQuote model = bll.GetQuoteModel("e2fad51d-8ca2-46e8-844c-d8fe71fa58a4");
                this.SelectJourney1.SetQuoteJourneyList = model.QuoteJourneyList;
                this.txtJourney.Text = model.TravelNote;

                this.SelectJourneySpot1.SetQuoteJourneyList = model.QuoteJourneyList;

                this.SelectPriceRemark1.SetQuoteJourneyList = model.QuoteJourneyList;

                //var a = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel("1f12c422-8660-475a-af8a-50b4a4ba15b9");

            }
        }


    }
}
