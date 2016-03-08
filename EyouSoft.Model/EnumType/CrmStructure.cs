using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.CrmStructure
{
    #region 客户类型
    /// <summary>
    /// 客户类型
    /// </summary>
    public enum CrmType
    {
        /// <summary>
        /// 单位直客
        /// </summary>
        单位直客 = 0,
        /// <summary>
        /// 同行客户
        /// </summary>
        同行客户 = 1,
        /// <summary>
        /// 个人会员
        /// </summary>
        个人会员 = 2
    }
    #endregion

    #region 个人会员状态
    /// <summary>
    /// 个人会员状态
    /// </summary>
    public enum CrmMemberState
    {
        /// <summary>
        /// 普通
        /// </summary>
        普通 = 1,
        /// <summary>
        /// 活跃
        /// </summary>
        活跃 = 2,
        /// <summary>
        /// 休眠
        /// </summary>
        休眠 = 3,
        /// <summary>
        /// 作废
        /// </summary>
        作废 = 4
    }
    #endregion

    #region 团队被访人身份
    /// <summary>
    /// 团队被访人身份
    /// </summary>
    public enum CrmIdentity
    {
        /// <summary>
        /// 游客
        /// </summary>
        游客 = 1,
        /// <summary>
        /// 领队
        /// </summary>
        领队 = 2,
        /// <summary>
        /// 全陪
        /// </summary>
        全陪 = 3
    }
    #endregion

    #region 团队回访类型
    /// <summary>
    /// 团队回访类型
    /// </summary>
    public enum CrmReturnType
    {
        /// <summary>
        /// 质检回访
        /// </summary>
        质检回访 = 1,
        /// <summary>
        /// 电话回访
        /// </summary>
        电话回访 = 2,
        /// <summary>
        /// 游客意见表
        /// </summary>
        游客意见表 = 3
    }
    #endregion

    #region 积分增减类别
    /// <summary>
    /// 积分增减类别
    /// </summary>
    public enum JiFenZengJianLeiBie
    {
        /// <summary>
        /// 增加
        /// </summary>
        增加,
        /// <summary>
        /// 减少
        /// </summary>
        减少
    }
    #endregion

    #region 满意度类型
    /// <summary>
    /// 满意度类型
    /// </summary>
    public enum SatisfactionType
    {
        /// <summary>
        /// 非常满意=1
        /// </summary>
        非常满意 = 1,
        /// <summary>
        /// 满意=2
        /// </summary>
        满意 = 2,
        /// 需改进=3
        /// </summary>
        需改进 = 3,
        /// 备注=4
        /// </summary>
        备注 = 4
    }
    #endregion

    #region 司机服务
    /// <summary>
    /// 司机服务
    /// </summary>
    public enum DriverService
    {
        /// <summary>
        /// 非常满意=1
        /// </summary>
        非常满意 = 1,
        /// <summary>
        /// 满意=2
        /// </summary>
        满意 = 2,
        /// 需改进=3
        /// </summary>
        需改进 = 3
    }
    #endregion

    #region 认路情况
    /// <summary>
    /// 认路情况
    /// </summary>
    public enum FindWay
    {
        /// <summary>
        /// 认路=1
        /// </summary>
        认路 = 1,
        /// <summary>
        /// 不认路=2
        /// </summary>
        不认路 = 2
    }
    #endregion

    #region 支付方式
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayType
    {    
        /// <summary>
        /// 现付=1
        /// </summary>
        现付 = 1,
        /// <summary>
        /// 挂账=2
        /// </summary>
        挂账 = 2

    }
    #endregion

    #region 宾客类型
    /// <summary>
    /// 宾客类型
    /// </summary>
    public enum CustomType
    {
        /// <summary>
        /// 内宾=1
        /// </summary>
        内宾 = 1,
        /// <summary>
        /// 外宾=2
        /// </summary>
        外宾 = 2
    }
    #endregion


}
