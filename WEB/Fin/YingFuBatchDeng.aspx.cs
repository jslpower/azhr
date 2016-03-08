using System;
using System.Collections;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Common;
    using EyouSoft.BLL.ComStructure;
    
    public partial class YingFuBatchDeng : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("doType") == "Save")
            {
                Save();
            }
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            IList<MPayRegister> ls = new BFinance().GetPayRegisterBaseByPlanId(Utils.GetQueryStringValue("planIDs").Split(','));
            if (ls != null && ls.Count > 0)
            {
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                this.litMsg.Visible = false;
            }
            else
            {
                this.phdSave.Visible = false;
                this.litMsg.Visible = true;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            IList<MRegister> ls = new List<MRegister>();
            MRegister model = null;
            string[] data = Utils.GetFormValue("data").Split(',');
            int i = data.Length;
            while (i-- > 0)
            {
                model = new MRegister();
                string[] datas = data[i].Split('|');
                if (datas.Length == 9)
                {
                    model.CompanyId = CurrentUserCompanyID;
                    model.Operator = SiteUserInfo.Name;
                    model.OperatorId = SiteUserInfo.UserId;
                    model.DeptId = SiteUserInfo.DeptId;
                    model.IssueTime = DateTime.Now;
                    //付款日期
                    model.PaymentDate = Utils.GetDateTimeNullable(datas[0]);
                    //付款人
                    model.Dealer = datas[1].Trim();
                    //付款金额
                    model.PaymentAmount = Utils.GetDecimal(datas[2]);
                    //付款方式
                    model.PaymentType = Utils.GetInt(datas[3]);
                    //最晚付款时间
                    model.Deadline = Utils.GetDateTimeNullable(datas[4]);
                    //备注
                    model.Remark = datas[5].Trim();
                    model.TourId = datas[6].Trim();
                    model.PlanId = datas[7].Trim();
                    model.DealerId = datas[8].Trim();
                }
                ls.Add(model);
            }
            var intRtn = new BFinance().AddRegister(ls);
            switch (intRtn)
            {
                case 0:
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "添加失败!"));
                    break;
                case -1:
                    AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "超额付款!"));
                    break;
                default:
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                    break;
            }

            //if (new BFinance().AddRegister(ls) > 0)
            //{
            //    AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
            //}
            //else
            //{
            //    AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "批量登记失败!"));
            //}
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
    }
}
