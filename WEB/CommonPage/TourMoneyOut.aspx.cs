using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.HPlanStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.BLL.PlanStructure;
using EyouSoft.Common.Page;
using System.Text;
namespace Web.CommonPage
{
    /// <summary>
    /// 各种报账页面 团队支出栏目
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-4-5
    /// 页面备注
    /// 必传参数
    /// tourID:团队编号
    /// parentType:调用页面类型PlanChangeChangeClass?
    /// parentType = null表示财务
    /// 该页面 用户控件枚举为写死的枚举值 枚举改变需维护
    public partial class TourMoneyOut : BackPage
    {
        /// <summary>
        /// 支付方式类型下拉
        /// </summary>
        protected string PaymentStr = string.Empty;
        /// <summary>
        /// 调用页面类型
        ///     财务传null
        ///     导游报账
        ///     销售报账
        ///     计调报账
        /// </summary>
        protected PlanChangeChangeClass? ParentType = null;
        /// <summary>
        /// 是否显示操作
        /// </summary>
        /// -1 = 不能操作
        ///  1 = 能操作
        ///  2 = 仅能操作本人添加的(暂无)
        protected int IsShowOperate = -1;

        /// <summary>
        /// 是否可以修改导游现收、现付
        /// </summary>
        protected bool IsChangeDaoYou;
        /// <summary>
        /// 允许修改数据的枚举
        /// </summary>
        protected PlanAddStatus PlanAddStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            string tourID = Utils.GetQueryStringValue("tourID");
            if (Utils.GetIntSign(Utils.GetQueryStringValue("parentType"), -1) > 0)
            {
                IsShowOperate = Utils.GetQueryStringValue("isop") == "1" ? 1 : -1;
                IsChangeDaoYou = Utils.GetQueryStringValue("ischangedaoyou") == "1" ? true : false;
                ParentType = (PlanChangeChangeClass)Utils.GetInt(Utils.GetQueryStringValue("parentType"));
            }

            DataInit(tourID);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string tourID)
        {
            PlanAddStatus? planAddStatus = null;

            IList<EnumObj> paymentList = EnumObj.GetList(typeof(Payment));
            if (paymentList != null && paymentList.Count > 0)
            {
                for (int i = 0; i < paymentList.Count; i++)
                {
                    if (IsChangeDaoYou == false && (Payment)Utils.GetInt(paymentList[i].Value) == Payment.现付)
                    {
                        //PaymentStr += "<option value='" + paymentList[i].Value + "'>" + paymentList[i].Text + "</option>";
                    }
                    else
                    {
                        PaymentStr += "<option value='" + paymentList[i].Value + "'>" + paymentList[i].Text + "</option>";
                    }
                }
            }

            var bll = new EyouSoft.BLL.HPlanStructure.BPlan();
            bool isShowMsg = true;

            planAddStatus = null;
            isShowMsg = Bind(rpt_CheDui, bll.GetList(PlanProject.用车, null, planAddStatus, true, null, tourID), pan_CheDui) && isShowMsg;
            isShowMsg = Bind(rpt_DaoYou, bll.GetList(PlanProject.导游, null, planAddStatus, true, null, tourID), pan_DaoYou) && isShowMsg;
            isShowMsg = Bind(rpt_DiJie, bll.GetList(PlanProject.地接, null, planAddStatus, true, null, tourID), pan_DiJie) && isShowMsg;
            isShowMsg = Bind(rpt_FeiJi, bll.GetList(PlanProject.飞机, null, planAddStatus, true, null, tourID), pan_FeiJi) && isShowMsg;
            isShowMsg = Bind(rpt_GuoNeiYouLun, bll.GetList(PlanProject.轮船, null, planAddStatus, true, null, tourID), pan_GuoNeiYouLun) && isShowMsg;
            isShowMsg = Bind(rpt_HuoChe, bll.GetList(PlanProject.火车, null, planAddStatus, true, null, tourID), pan_HuoChe) && isShowMsg;
            isShowMsg = Bind(rpt_JinDian, bll.GetList(PlanProject.景点, null, planAddStatus, true, null, tourID), pan_JinDian) && isShowMsg;
            isShowMsg = Bind(rpt_JiuDian, bll.GetList(PlanProject.酒店, null, planAddStatus, true, null, tourID), pan_JiuDian) && isShowMsg;
            isShowMsg = Bind(rpt_LingLiao, bll.GetList(PlanProject.领料, null, planAddStatus, true, null, tourID), pan_LinLiao) && isShowMsg;
            isShowMsg = Bind(rpt_QiChe, bll.GetList(PlanProject.汽车, null, planAddStatus, true, null, tourID), pan_QiChe) && isShowMsg;
            isShowMsg = Bind(rpt_QiTa, bll.GetList(PlanProject.其它, null, planAddStatus, true, null, tourID), pan_QiTa) && isShowMsg;
            //isShowMsg = Bind(rpt_SheWaiYouLun, bll.GetList(PlanProject.涉外游轮, null, planAddStatus, true, null, tourID), pan_SheWaiYouLun) && isShowMsg;
            isShowMsg = Bind(rpt_YongCan, bll.GetList(PlanProject.用餐, null, planAddStatus, true, null, tourID), pan_YongCan) && isShowMsg;

            pan_Msg.Visible = isShowMsg && IsShowOperate == -1 && ParentType == null;

        }


        /// <summary>
        /// 获得支付方式
        /// </summary>
        /// <returns></returns>
        protected string GetPaymentStr(int pType)
        {
            var p = Utils.GetEnumText(typeof(Payment), (Payment)pType);
            if (IsShowOperate == -1 || ((Payment)pType == Payment.现付 && IsChangeDaoYou == false))
            {
                return p.ToString();
            }
            string str = string.Empty;
            str = "<select class='inputselect'  name='sel_payment'  data-paymenttype='" + pType + "'>";
            str += PaymentStr;
            str += "</select>";
            return str;
        }

        /// <summary>
        /// 获取导游,计调,销售变更
        /// </summary>
        /// <param name="ls">变更列表</param>
        /// <returns></returns>
        protected string GetBianGengHtml(object ls)
        {
            StringBuilder s = new StringBuilder();
            var items = (IList<MPlanCostChange>)ls;

            //MPlanCostChange info1 = null;//daoyou jia
            //MPlanCostChange info2 = null;//daoyou jian
            //MPlanCostChange info3 = null;//xiaoshou jia
            //MPlanCostChange info4 = null;//xiaoshou jian
            MPlanCostChange info5 = null;//jidiao jia
            MPlanCostChange info6 = null;//jidiao jian

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    switch (item.ChangeType)
                    {
                        //case PlanChangeChangeClass.导游报账:
                        //    if (item.Type) info1 = item;
                        //    else info2 = item;
                        //    break;
                        //case PlanChangeChangeClass.销售报账:
                        //    if (item.Type) info3 = item;
                        //    else info4 = item;
                        //    break;
                        case PlanChangeChangeClass.计调报账:
                            if (item.Type) info5 = item;
                            else info6 = item;
                            break;
                    }
                }
            }

            //if (info1 == null)
            //{
            //    s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_daoyoujia'>—</a></td>");
            //}
            //else
            //{
            //    s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_daoyoujia'>" + info1.ChangeCost.ToString("F2") + "</a></td>");
            //}

            //if (info2 == null)
            //{
            //    s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_daoyoujian'>—</a></td>");
            //}
            //else
            //{
            //    s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_daoyoujian'>" + info2.ChangeCost.ToString("F2") + "</a></td>");
            //}

            //if (info3 == null)
            //{
            //    s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_xiaoshoujia'>—</a></td>");
            //}
            //else
            //{
            //    s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_xiaoshoujia'>" + info3.ChangeCost.ToString("F2") + "</a></td>");
            //}

            //if (info4 == null)
            //{
            //    s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_xiaoshoujian'>—</a></td>");
            //}
            //else
            //{
            //    s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_xiaoshoujian'>" + info4.ChangeCost.ToString("F2") + "</a></td>");
            //}

            if (info5 == null)
            {
                s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_jidiaojia'>—</a></td>");
            }
            else
            {
                s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_jidiaojia'>" + info5.ChangeCost.ToString("F2") + "</a></td>");
            }

            if (info6 == null)
            {
                s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_jidiaojian'>—</a></td>");
            }
            else
            {
                s.Append("<td style='text-align:center;'><a href='javascript:void(0)' class='i_jidiaojian'>" + info6.ChangeCost.ToString("F2") + "</a></td>");
            }

            return s.ToString();
        }
        
        /// <summary>
        /// 获取后台操作按钮
        /// </summary>
        /// <param name="addStatus"></param>
        /// <returns></returns>
        protected string GetOperate(int addStatus, int payType)
        {

            if (IsShowOperate == -1)
            {
                return string.Empty;
            }
            else
            {

                string isShow = IsChangeDaoYou == false && (Payment)payType == Payment.现付 ? "none" : "";

                EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus _addStatus = (EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus)(int)addStatus;
                switch (_addStatus)
                {
                    case EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加:
                        return string.Empty;
                    //case EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.导游报账时添加:
                    //    if (ParentType == PlanChangeChangeClass.导游报账)
                    //    {
                    //        return "<a href=javascript:void(0); style='display:" + isShow + "' data-class=a_Updata class=addbtn><img src=/images/updateimg.gif border=0 /></a>&nbsp;&nbsp;<a href=javascript:void(0); style='display:" + isShow + "' data-class=a_Del class=addbtn><img src=/images/delimg.gif border=0 /></a>";
                    //    }
                    //    return string.Empty;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调报账时添加:
                        if (ParentType == PlanChangeChangeClass.计调报账)
                        {
                            return "<a href=javascript:void(0); style='display:" + isShow + "' data-class=a_Updata class=addbtn><img src=/images/updateimg.gif border=0 /></a>&nbsp;&nbsp;<a href=javascript:void(0); style='display:" + isShow + "' data-class=a_Del class=addbtn><img src=/images/delimg.gif border=0 /></a>";
                        }
                        return string.Empty;
                    //case EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.销售报账时添加:
                    //    if (ParentType == PlanChangeChangeClass.销售报账)
                    //    {
                    //        return "<a href=javascript:void(0); style='display:" + isShow + "' data-class=a_Updata class=addbtn><img src=/images/updateimg.gif border=0 /></a>&nbsp;&nbsp;<a href=javascript:void(0); style='display:" + isShow + "' data-class=a_Del class=addbtn><img src=/images/delimg.gif border=0 /></a>";
                    //    }
                    //    return string.Empty;
                }
                return string.Empty;

            }
        }
        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="rpt"></param>
        /// <param name="items"></param>
        /// <param name="p"></param>
        private bool Bind(Repeater rpt, IList<EyouSoft.Model.HPlanStructure.MPlan> items, Panel p)
        {
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
                return false;
            }
            else
            {
                if (ParentType == null)
                {
                    p.Visible = false;
                }
                return true;
            }

        }

    }
}
