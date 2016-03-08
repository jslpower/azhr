using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;
namespace EyouSoft.Web.Webmaster
{
    public partial class setcarseat : System.Web.UI.Page
    {
        protected string strSeat = string.Empty;
        protected string tempID = string.Empty;
        protected int strLeft = 57;
        protected int strTop = 45;

        protected void Page_Load(object sender, EventArgs e)
        {
            tempID = Utils.GetQueryStringValue("tempId");
            if (!IsPostBack)
            {
                if (tempID != "")
                {
                    Bindrpt_busBoxList(tempID);
                }
            }

            string type = Utils.GetQueryStringValue("type");
            if (type == "post")
            {
                PageSave();
            }
        }
        #region 绑定数据
        /// <summary>
        /// 根据模板编号获取座位列表
        /// </summary>
        /// <param name="strID">模板编号</param>
        public void Bindrpt_busBoxList(string strID)
        {
            EyouSoft.BLL.SysStructure.BSysCarType bll = new EyouSoft.BLL.SysStructure.BSysCarType();
            EyouSoft.Model.SysStructure.MSysCarType model = bll.GetCarTypeById(strID);
            if (model != null)
            {
                if (model.list != null && model.list.Count > 0)
                {
                    rpt_busBoxList.DataSource = model.list;
                    rpt_busBoxList.DataBind();
                }
                else
                {
                    ret_SeatHtml(model.SeatNum);
                }
            }

        }
        #endregion

        #region 返回座位坐标  默认情况下
        /// <summary>
        /// 根据模板编号输出座位
        /// </summary>
        /// <param name="id">模板编号</param>
        /// <returns></returns>
        protected string ret_SeatHtml(int seatNum)
        {
            int seat = 0;
            #region
            if (seatNum > 10)
            {
                for (int i = 1; i <= seatNum; i++)
                {
                    for (int j = 1; j <= 5; j++)
                    {
                        if (seat < seatNum - 5)
                        {
                            seat++;
                            if (j == 3)
                            {
                                strSeat += "<div class='blackdiv' style='display:none;left: " + strLeft * i + "px; top: " + strTop * j + "px;'><a class='graybtn'>" + seat.ToString() + "</a></div>";
                                seat--;
                            }
                            else
                            {
                                strSeat += "<div class='movediv' style='left: " + strLeft * i + "px; top: " + strTop * j + "px;'><a class='graybtn'>" + seat.ToString() + "</a></div>";
                            }
                        }
                    }
                }
                for (int i = 1; i <= 5; i++)
                {
                    seat++;
                    strSeat += "<div class='movediv' style='left:860px; top: " + strTop * i + "px;'><a class='graybtn'>" + seat.ToString() + "</a></div>";
                }
            }
            else if (seatNum <= 10)
            {
                for (int i = 1; i <= seatNum; i++)
                {
                    for (int j = 1; j <= 5; j++)
                    {
                        if (seat < seatNum)
                        {
                            seat++;
                            if (j == 3)
                            {
                                strSeat += "<div class='blackdiv' style='display:none;left: " + strLeft * i + "px; top: " + strTop * j + "px;'><a class='graybtn'>" + seat.ToString() + "</a></div>";
                                seat--;
                            }
                            else
                            {
                                strSeat += "<div class='movediv' style='left: " + strLeft * i + "px; top: " + strTop * j + "px;'><a class='graybtn'>" + seat.ToString() + "</a></div>";
                            }
                        }
                    }
                }
            }
            #endregion

            return strSeat;
        }
        #endregion

        #region 表单提交
        /// <summary>
        /// 表单提交
        /// </summary>
        protected void PageSave()
        {
            string tempId = Utils.GetFormValue("tempId");
            string data = Utils.GetFormValue("data");
            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat>>(data);


            Response.Clear();
            if (list != null && list.Count > 0)
            {
                EyouSoft.BLL.SysStructure.BSysCarType bll = new EyouSoft.BLL.SysStructure.BSysCarType();
                bool result = bll.UpdateSysCarType(tempId, list);
                if (result)
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "操作失败!"));
                }
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "操作失败!"));
            }
            Response.End();
        }
        #endregion
    }
}
