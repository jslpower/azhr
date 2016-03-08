using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.HTourStructure;

namespace EyouSoft.Web.UserControl
{
    public partial class TuanFengWeiCan : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置风味餐数据源（用作初始化表单）
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MTourFoot> SetFengWeiCan { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDateList();
            }
        }
        private void SetDateList()
        {
            if (SetFengWeiCan != null && SetFengWeiCan.Count > 0)
            {
                this.rptFengWeiList.DataSource = SetFengWeiCan;
                this.rptFengWeiList.DataBind();
                ph_showorhide.Visible = false;
            }
        }
        /// <summary>
        /// 获取风味餐信息（用于实体赋值）
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MTourFoot> GetFengWeiList()
        {
            string[] gysid = Utils.GetFormValues("hid_fcanguanid");
            string[] caidanid = Utils.GetFormValues("hid_fcaidanid");
            string[] caidanname = Utils.GetFormValues("txtfcaidanname");
            string[] caidanprice = Utils.GetFormValues("txtfprice");//销售价
            string[] remark = Utils.GetFormValues("txtfremark");
            string[] pricejs = Utils.GetFormValues("hid_fpricejs");//结算价
            string[] footid = Utils.GetFormValues("hidfootid");//唯一编号，跟菜单一一对应
            IList<EyouSoft.Model.HTourStructure.MTourFoot> list = new List<MTourFoot>();
            if (gysid.Length > 0 && caidanid.Length > 0 && caidanname.Length > 0 && caidanprice.Length > 0 && pricejs.Length > 0 && footid.Length > 0)
            {
                for (int i = 0; i < gysid.Length; i++)
                {
                    if (!string.IsNullOrEmpty(gysid[i]))
                    {
                        EyouSoft.Model.HTourStructure.MTourFoot model = new EyouSoft.Model.HTourStructure.MTourFoot();
                        model.Menu = caidanname[i].ToString();
                        model.MenuId = caidanid[i].ToString();
                        model.Price = Utils.GetDecimal(caidanprice[i].ToString());
                        model.Remark = remark[i];
                        model.RestaurantId = gysid[i];
                        model.SettlementPrice = Utils.GetDecimal(pricejs[i].ToString());
                        model.FootId = footid[i].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取是否显示删除按钮
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string getIsDelBtn(object obj)
        {
            string str = string.Empty;
            string footid = obj.ToString().Trim();
            if (footid == "")
            {
                str = "<a href='javascript:void(0)' class='delbtnfwei'> <img width='48' height='20' src='/images/delimg.gif'></a>";
            }
            return str;
        }
    }
}