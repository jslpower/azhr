using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//金蝶相关枚举 创建人:陈志仁 创建时间:2011-10-11
namespace EyouSoft.Model.EnumType.KingDee
{
    using EyouSoft.Model.EnumType.CrmStructure;

    #region 金蝶相关枚举
    /// <summary>
    /// 科目类型
    /// </summary>
    public enum FinanceAccountItem
    {
        /// <summary>
        /// 为 科目类别时无须关联相应的系统资源(如客户,部门,员工,供应商)
        /// </summary>
        科目类别 = 6,
        /// <summary>
        /// 同行客户 = 1
        /// </summary>
        客户 = 1,
        /*/// <summary>
        /// 单位直客 = 0
        /// </summary>
        单位直客 = 0,
        /// <summary>
        /// 个人会员 = 3
        /// </summary>
        个人会员 = 3,*/
        /// <summary>
        /// 部门 = 2
        /// </summary>
        部门 = 2,
        /// <summary>
        /// 职员（档案信息） = 5
        /// </summary>
        职员 = 5,
        /// <summary>
        /// 指资源管理中的 地接,酒店,餐厅,景点等供应商
        /// </summary>
        供应商 = 4,
        /// <summary>
        /// 团号 = 7
        /// </summary>
        团号 = 7,
        /// <summary>
        /// 支付方式（现金、银行）=8
        /// </summary>
        支付方式=8,
        ///// <summary>
        ///// 导游（导游信息）=9
        ///// </summary>
        //导游=9
    }

    /// <summary>
    /// 凭证录入弹出框的默认类型
    /// </summary>
    public enum DefaultProofType
    {
        /// <summary>
        /// 订单收款（封团前）=0
        /// </summary>
        订单收款=0,
        /// <summary>
        /// 计调付款（封团前）=1
        /// </summary>
        计调付款=1,
        /// <summary>
        /// 杂费收入=2
        /// </summary>
        杂费收入=2,
        /// <summary>
        /// 杂费支出=3
        /// </summary>
        杂费支出=3,
        /// <summary>
        /// 导游借款（导游备用金）=4
        /// </summary>
        导游借款=4,
        /// <summary>
        /// 单团核算（单团核算时报销完成没有财务入账的场合）=5
        /// </summary>
        单团核算=5,
        /// <summary>
        /// 团未完导游先报账（报销完成的财务入账）=6
        /// </summary>
        团未完导游先报账=6,
        /// <summary>
        /// 后期收款（单团核算时报销完成已入帐的场合）=7
        /// </summary>
        后期收款=7,
        ///// <summary>
        ///// 订单封团后收款=8
        ///// </summary>
        //订单封团后收款=8,
        ///// <summary>
        ///// 计调封团后付款=9
        ///// </summary>
        //计调封团后付款=9
    }

    /// <summary>
    /// 金蝶默认科目配置
    /// </summary>
    public enum SysConfigDefaultSubject
    {
        #region 金蝶默认科目配置

        #region 订单收款

        订单收款_贷 = 1000,
        订单收款_借=1999 ,

        #endregion

        #region 计调预付款

        计调预付款_贷=2000,
        计调预付款_借=2999,

        #endregion

        #region 导游备用金

        导游备用金_贷=3000,
        导游备用金_借=3999,

        #endregion

        #region 团未完导游先报账

        团未完导游先报账_预付账款_借 = 4000,
        团未完导游先报账_预收账款_贷 = 4500,
        团未完导游先报账_团队借款_贷 = 4501,
        团未完导游先报账_现金_贷 = 4999,

        #endregion

        #region 单团核算

        单团核算_主营业务收入_贷=5000,
        单团核算_团队借款_贷=5001,
        单团核算_团队预支_贷=5002,
        单团核算_团队支出_贷=5003,
        单团核算_应付账款_贷=5004,
        单团核算_现金_贷=5005,
        单团核算_主营业务成本_借=5006,
        单团核算_预收账款_借=5007,
        单团核算_现金_借=5008,
        单团核算_银行存款_借=5009,
        单团核算_应收帐款_借=5999,

        #endregion

        //#region 后期收款

        //后期收款_主营业务收入_贷 = 6000,
        //后期收款_团队预支_贷 = 6001,
        //后期收款_应付账款_贷 = 6002,
        //后期收款_团队支出_贷 = 6003,
        //后期收款_预收账款_借=6500,
        //后期收款_应收账款_借=6501,
        //后期收款_主营业务成本_借=6999,

        //#endregion

        #endregion
    }
    #endregion
}
