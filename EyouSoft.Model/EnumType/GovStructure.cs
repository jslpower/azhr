//行政中心中心枚举 2011-09-05 邵权江 创建
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.GovStructure
{
    /// <summary>
    /// 通知公告发布对象枚举
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 公司内部
        /// </summary>
        公司内部 = 0,
        /// <summary>
        /// 指定部门
        /// </summary>
        指定部门,
        /// <summary>
        /// 组团社
        /// </summary>
        组团社,
        /// <summary>
        /// 同行社
        /// </summary>
        同行社,
        /// <summary>
        /// 分销商
        /// </summary>
        分销商,
        /// <summary>
        /// 供应商
        /// </summary>
        供应商
    }

    /// <summary>
    /// 会议类别枚举
    /// </summary>
    public enum Category
    {
        /// <summary>
        /// 行政会议
        /// </summary>
        行政会议 = 0,
        /// <summary>
        /// 员工会议
        /// </summary>
        员工会议
    }

    /// <summary>
    /// 学历枚举
    /// </summary>
    public enum Education
    {
        /// <summary>
        /// 大专
        /// </summary>
        大专= 0,
        /// <summary>
        /// 本科
        /// </summary>
        本科
    }

    /// <summary>
    /// 考勤类型枚举
    /// </summary>
    public enum AttendanceType
    {
        /// <summary>
        /// 全勤
        /// </summary>
        全勤 = 0,
        /// <summary>
        /// 迟到
        /// </summary>
        迟到,
        /// <summary>
        /// 早退
        /// </summary>
        早退,
        /// <summary>
        /// 旷工
        /// </summary>
        旷工,
        /// <summary>
        /// 请假
        /// </summary>
        请假,
        /// <summary>
        /// 加班
        /// </summary>
        加班,
        /// <summary>
        /// 出差
        /// </summary>
        出差,
        /// <summary>
        /// 休假
        /// </summary>
        休假,
        /// <summary>
        /// 停职
        /// </summary>
        停职
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        男 = 0,
        /// <summary>
        /// 女
        /// </summary>
        女=1,
        /// <summary>
        /// 其他
        /// </summary>
        其他=2
    }

    /// <summary>
    /// 档案员工类型
    /// </summary>
    public enum StaffType
    {
        /// <summary>
        /// 正式员工
        /// </summary>
        正式员工 = 1,
        /// <summary>
        /// 试用期
        /// </summary>
        试用期,
        /// <summary>
        /// 学徒期
        /// </summary>
        学徒期
    }

    /// <summary>
    /// 员工状态
    /// </summary>
    public enum StaffStatus
    {
        /// <summary>
        /// 在职
        /// </summary>
        在职 = 1,
        /// <summary>
        /// 离职
        /// </summary>
        离职,
        /// <summary>
        /// 兼职
        /// </summary>
        兼职,
        /// <summary>
        /// 挂靠
        /// </summary>
        挂靠
    }

    /// <summary>
    /// 学历状态
    /// </summary>
    public enum Statue
    {
        /// <summary>
        /// 毕业
        /// </summary>
        毕业 = 1,
        /// <summary>
        /// 在读
        /// </summary>
        在读
    }

    /// <summary>
    /// 人事交接状态
    /// </summary>
    public enum TransitionState
    {
        /// <summary>
        /// 未完成
        /// </summary>
        未完成 = 1,
        /// <summary>
        /// 已完成
        /// </summary>
        已完成
    }

    /// <summary>
    /// 人事审核状态
    /// </summary>
    public enum ApprovalStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        待审核 = 1,
        /// <summary>
        /// 已审核
        /// </summary>
        审核通过,
        /// <summary>
        /// 审核未通过
        /// </summary>
        审核未通过
    }

    /// <summary>
    /// 劳动合同状态
    /// </summary>
    public enum FileContractStatus
    {
        /// <summary>
        /// 未到期
        /// </summary>
        未到期 = 1,
        /// <summary>
        /// 到期未处理
        /// </summary>
        到期未处理,
        /// <summary>
        /// 到期已处理
        /// </summary>
        到期已处理
    }

    /// <summary>
    /// 物品领用/发放/借阅类型
    /// </summary>
    public enum GoodUseType
    {
        /// <summary>
        /// 领用
        /// </summary>
        领用 = 0,
        /// <summary>
        /// 发放
        /// </summary>
        发放,
        /// <summary>
        /// 借阅
        /// </summary>
        借阅
    }

    /// <summary>
    /// 物品借阅状态
    /// </summary>
    public enum BorrowStatus
    {
        /// <summary>
        /// 借阅中
        /// </summary>
        借阅中=0,
        /// <summary>
        /// 已归还
        /// </summary>
        已归还
    }

    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 审批
        /// </summary>
        审批 = 0,
        /// <summary>
        /// 传阅
        /// </summary>
        传阅
    }
}
