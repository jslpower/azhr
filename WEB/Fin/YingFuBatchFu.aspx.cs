using System;
using System.Collections;
using System.Linq;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Common;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.BLL.ComStructure;
    
    public partial class YingFuBatchFu : BackPage
    {
        /// <summary>
        /// 是否开启kis整合
        /// </summary>
        protected bool IsEnableKis;

        /// <summary>
        /// 财务入账 = true ，支付 = false
        /// </summary>
        protected bool IsInAccount = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            //控件初始化
            this.SellsSelect1.ClientDeptID = this.hideDeptID.ClientID;
            this.SellsSelect1.ClientDeptName = this.hideDeptName.ClientID;

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
            IsInAccount = Utils.GetQueryStringValue("isInAccount") == "" ? false : true;
            //系统配置实体
            MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID) ?? new MComSetting();
            IsEnableKis = comModel.IsEnableKis;

            if (Utils.GetQueryStringValue("isInAccount") == "chakan") { IsEnableKis = false; }

            txt_PDate.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
            this.SellsSelect1.SellsID = this.SiteUserInfo.UserId;
            this.SellsSelect1.SellsName = this.SiteUserInfo.Name;
            this.SellsSelect1.ReadOnly = IsInAccount; //财务入账 不能修改出纳
            this.SellsSelect1.IsShowSelect = !IsInAccount; //财务入账 隐藏选用按钮
            this.hideDeptID.Value = this.SiteUserInfo.DeptId.ToString();

            IList<MRegister> ls = new BFinance().GetRegisterById(
                CurrentUserCompanyID,
                Utils.ConvertToIntArray(Utils.GetQueryStringValue("registerIds").Split(','))
                );

            if (ls != null && ls.Count > 0)
            {
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                lbl_amount.Text = UtilsCommons.GetMoneyString(ls.Sum(item => item.PaymentAmount).ToString(), ProviderToMoney);
                if (ls[0].Status == EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付)
                {
                    txt_PDate.Text = UtilsCommons.GetDateString(ls[0].PayTime, ProviderToDate);
                    this.SellsSelect1.SellsID = ls[0].AccountantId;
                    this.SellsSelect1.SellsName = ls[0].Accountant;
                    this.hideDeptID.Value = ls[0].AccountantDeptId.ToString();
                    txt_PDate.Enabled = false;
                }
            }


        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string msg = string.Empty;
            IList<MBatchPay> registers = new List<MBatchPay>();
            #region 获得表单
            string[] data = Utils.GetFormValue("data").Split('|');

            string uID = Utils.GetFormValue("uid");
            string uName = Utils.GetFormValue("uName");
            int deptID = Utils.GetInt(Utils.GetFormValue("deptID"));
            string deptName = Utils.GetFormValue("deptName");

            #endregion

            int i = data.Length;
            while (i-- > 0)
            {
                int[] datas = Utils.GetIntArray(data[i], ",");
                if (datas.Length > 1)
                {
                    registers.Add(new MBatchPay { RegisterId = datas[0], PaymentType = datas[1] });
                }
                else
                {
                    msg += UtilsCommons.AjaxReturnJson("0", "请选择支付方式!");
                    return;
                }

            }
            if (data.Length > 0)
            {
                switch (new BFinance().SetRegisterPay(deptID, uID, uName, Utils.GetDateTime(Utils.GetFormValue("PDate"), DateTime.Now), CurrentUserCompanyID, registers))
                {
                    case 0:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "支付失败!"));
                        break;
                    case 1:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                        break;
                    case 2:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "预存款余额不足!"));
                        break;
                }
            }
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
