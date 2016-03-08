using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// 系统公司及配置用到的枚举
namespace EyouSoft.Model.EnumType.ComStructure
{
    #region 附件信息关联类型
    /// <summary>
    /// 附件信息关联类型
    /// </summary>
    public enum AttachItemType
    {
        /// <summary>
        /// 供应商
        /// </summary>
        供应商 = 0,//供应商图片
        /// <summary>
        /// 线路库
        /// </summary>
        线路库 = 1,
        /// <summary>
        /// 通知公告
        /// </summary>
        通知公告 = 2,
        /// <summary>
        /// 规章制度
        /// </summary>
        规章制度 = 3,
        /// <summary>
        /// 人事档案
        /// </summary>
        人事档案 = 4,
        /// <summary>
        /// 意见建议
        /// </summary>
        意见建议 = 5,
        /// <summary>
        /// 公司合同
        /// </summary>
        公司合同 = 6,
        /// <summary>
        /// 培训管理
        /// </summary>
        培训管理 = 7,
        /// <summary>
        /// 发布计划
        /// </summary>
        发布计划 = 8,
        /// <summary>
        /// 供应商合同附件
        /// </summary>
        供应商合同附件 = 9,
        /// <summary>
        /// 游轮旗下游轮图片
        /// </summary>
        游轮旗下游轮图片 = 10,
        /// <summary>
        /// 线路附件
        /// </summary>
        线路附件 = 11,
        /// <summary>
        /// 线路签证资料
        /// </summary>
        线路签证资料 = 12,
        /// <summary>
        /// 线路行程图片
        /// </summary>
        线路行程图片 = 13,
        /// <summary>
        /// 供应商合作协议
        /// </summary>
        供应商合作协议 = 14,
        /// <summary>
        /// 客户合作协议
        /// </summary>
        客户合作协议 = 15,//同行，直客合作协议
        /// <summary>
        /// 文件管理
        /// </summary>
        文件管理 = 16,
        /// <summary>
        /// 报价签证资料=17
        /// </summary>
        报价签证资料 = 17,
        /// <summary>
        /// 计划签证资料=18
        /// </summary>
        计划签证资料 = 18,
        /// <summary>
        /// 质检中心——车队质检
        /// </summary>
        车队质检 = 19,
        /// <summary>
        /// 质检中心——团队服务
        /// </summary>
        团队服务 = 20,
        /// <summary>
        /// 报账的时候计调费用增减变更=21
        /// </summary>
        费用变更=21

    }

    #endregion

    #region 线路类型

    /// <summary>
    /// 线路类型
    /// </summary>
    public enum AreaType
    {
        /// <summary>
        /// 省内线
        /// </summary>
        省内线 = 1,
        /// <summary>
        /// 国内线
        /// </summary>
        国内线 = 2,
        /// <summary>
        /// 出境线
        /// </summary>
        出境线 = 3
    }

    #endregion

    #region 客户等级类型

    /// <summary>
    /// 客户等级类型
    /// </summary>
    public enum LevType
    {
        /// <summary>
        /// 门市价（直客价）
        /// </summary>
        门市价 = 1,
        /// <summary>
        /// 内部结算价
        /// </summary>
        内部结算价 = 4,
        /// <summary>
        /// 同行价
        /// </summary>
        同行价 = 5,
        /// <summary>
        /// 其他（客户自定义）
        /// </summary>
        其他 = 3,
    }

    #endregion

    #region 支付方式-款项来源
    /// <summary>
    /// 款项来源
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// 现金
        /// </summary>
        现金 = 1,
        /// <summary>
        /// 银行
        /// </summary>
        银行 = 2
    }
    #endregion

    #region 支付方式-类型
    /// <summary>
    /// 支付方式类型
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 收入=1
        /// </summary>
        收入 = 1,
        /// <summary>
        /// 支付=2
        /// </summary>
        支出 = 2
    }
    #endregion

    #region 公司服务标准模版-类型

    /// <summary>
    /// 公司服务标准模版-类型
    /// </summary>
    public enum ProjectType
    {
        /// <summary>
        /// 不含项目
        /// </summary>
        不含项目 = 1,
        /// <summary>
        /// 购物安排
        /// </summary>
        购物安排 = 2,
        /// <summary>
        /// 儿童安排
        /// </summary>
        儿童安排 = 3,
        /// <summary>
        /// 自费项目
        /// </summary>
        自费项目 = 4,
        /// <summary>
        /// 注意事项
        /// </summary>
        注意事项 = 5,
        /// <summary>
        /// 温馨提醒
        /// </summary>
        温馨提醒 = 6,
        /// <summary>
        /// 包含项目
        /// </summary>
        包含项目 = 7,
        /// <summary>
        /// 服务标准
        /// </summary>
        服务标准 = 8
    }
    #endregion

    #region 包含项目类型
    /// <summary>
    /// 包含项目类型
    /// </summary>
    public enum ContainProjectType
    {
        /// <summary>
        /// 大交通
        /// </summary>
        机票 = 1,
        /// <summary>
        /// 用车
        /// </summary>
        用车 = 2,
        /// <summary>
        /// 房
        /// </summary>
        酒店 = 3,
        /// <summary>
        /// 餐
        /// </summary>
        餐 = 4,
        /// <summary>
        /// 导服
        /// </summary>
        //导服 = 5,
        /// <summary>
        /// 景点
        /// </summary>
        景点 = 6,
        ///// <summary>
        ///// 保险
        ///// </summary>
        //保险 = 7,
        /// <summary>
        /// 小交通
        /// </summary>
        //小交通 = 8,
        ///// <summary>
        ///// 综费
        ///// </summary>
        //综费 = 9,
        /// <summary>
        /// 其它
        /// </summary>
        其它 = 10,
        ///// <summary>
        ///// 国内段
        ///// </summary>
        //国内段 = 11,
        ///// <summary>
        ///// 签证
        ///// </summary>
        //签证 = 12,
        ///// <summary>
        ///// 游船
        ///// </summary>
        //游船 = 13
    }
    #endregion

    #region 包含项目单位
    /// <summary>
    /// 包含项目单位
    /// </summary>
    public enum ContainProjectUnit
    {
        /// <summary>
        /// 人 = 0
        /// </summary>
        人 = 0,
        /// <summary>
        /// 团 = 1
        /// </summary>
        团 = 1
    }
    #endregion

    #region 自动生成团号规则-规则项目类型

    /// <summary>
    /// 自动生成团号规则-规则项目类型
    /// </summary>
    public enum OptionItemType
    {
        /// <summary>
        /// 公司简称=0
        /// </summary>
        公司简称 = 0,
        /// <summary>
        /// 部门简称=1
        /// </summary>
        部门简称 = 1,
        /// <summary>
        /// 团队类型=2
        /// </summary>
        团队类型 = 2,
        /// <summary>
        /// 出团日期=3
        /// </summary>
        出团日期 = 3,
        /// <summary>
        /// 分隔符=4
        /// </summary>
        分隔符 = 4,
        /// <summary>
        /// 序列号=5
        /// </summary>
        序列号 = 5,
        /// <summary>
        /// 客户简码=6
        /// </summary>
        客户简码=6
    }
    #endregion

    #region 团号的出团日期格式
    /// <summary>
    /// 出团日期格式
    /// </summary>
    public enum OptionItemTypeLDateFormat
    {
        /// <summary>
        /// 格式1=0(年月日)
        /// </summary>
        格式1 = 0,
        /// <summary>
        /// 格式2=1(年-月-日)
        /// </summary>
        格式2 = 1
    }
    #endregion

    #region 团号的序列号格式
    /// <summary>
    /// 序列号格式
    /// </summary>
    public enum OptionItemTypeSeriesFormat
    {
        /// <summary>
        /// 流水号=0
        /// </summary>
        流水号 = 0,
        /// <summary>
        /// 字母=1
        /// </summary>
        字母 = 1
    }
    #endregion

    #region 用户类型

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 内部员工
        /// </summary>
        内部员工 = 1,
        /// <summary>
        /// 供应商
        /// </summary>
        供应商,
        /// <summary>
        /// 组团社(分销商)
        /// </summary>
        组团社,
        /// <summary>
        /// 导游
        /// </summary>
        导游
    }

    #endregion

    /*#region 系统配置类型
    /// <summary>
    /// 系统配置类型
    /// </summary>
    public enum ConfigurationType
    {
        /// <summary>
        /// 打印配置
        /// </summary>
        打印配置 = 1,
        /// <summary>
        /// 业务配置
        /// </summary>
        业务配置 = 2
    }
    #endregion*/

    #region 系统配置

    /// <summary>
    /// 系统配置
    /// </summary>
    public enum SysConfiguration
    {
        /// <summary>
        /// 团号配置（生成团号的规则字符串）
        /// </summary>
        团号配置 = 1,
        /// <summary>
        /// 列表显示控制_前几个月
        /// </summary>
        列表显示控制_前几个月 = 2,
        /// <summary>
        /// 列表显示控制_后几个月
        /// </summary>
        列表显示控制_后几个月 = 3,
        /// <summary>
        /// 订单最长留位时间，单位分钟
        /// </summary>
        留位时间控制 = 4,
        //提醒时间配置 = 5,
        /// <summary>
        /// 国内线提前N天停收
        /// </summary>
        计划停收国内线 = 6,
        /// <summary>
        /// 省内线提前N天停收
        /// </summary>
        计划停收省内线 = 7,
        /// <summary>
        /// 出境线提前N天停收
        /// </summary>
        计划停收出境线 = 8,
        /// <summary>
        /// 是否跳过导游报账
        /// </summary>
        跳过导游报账 = 9,
        /// <summary>
        /// 是否跳过销售报账
        /// </summary>
        跳过销售报账 = 10,
        /// <summary>
        /// 是否路过计调终审
        /// </summary>
        跳过报账终审 = 11,
        /// <summary>
        /// 个人会员报名积分比例
        /// </summary>
        积分比例 = 12,
        //出团提醒 = 13,
        //回团提醒 = 14,
        //收款提醒 = 15,
        //付款提醒 = 16,
        //变更提醒 = 17,
        /// <summary>
        /// 劳动合同到期提前N天提醒
        /// </summary>
        劳动合同到期提醒 = 18,
        /// <summary>
        /// 供应商合同到期在到期前N天提醒
        /// </summary>
        供应商合同到期提醒 = 19,
        /// <summary>
        /// 公司合同到期在到期前N天提醒
        /// </summary>
        公司合同到期提醒 = 20,
        /// <summary>
        /// 洒店预控到期在最后保留日前N天提醒
        /// </summary>
        洒店预控到期提醒 = 21,
        /// <summary>
        /// 车辆预控到期在最后保留日前N天提醒
        /// </summary>
        车辆预控到期提醒 = 22,
        /// <summary>
        /// 游船预控到期在最后保留日前N天提醒
        /// </summary>
        游船预控到期提醒 = 23,
        //物品借阅到期提醒 = 24,
        //生日提醒 = 25,
        //公告提醒 = 26,
        //弹窗提醒 = 27,
        //询价提醒 = 28,
        /// <summary>
        /// 是否做欠款额度控制
        /// </summary>
        欠款额度控制 = 29,
        /// <summary>
        /// 财务付款登记是否需要审核
        /// </summary>
        财务支出审核 = 30,
        /// <summary>
        /// 财务收款登记是否需要审核
        /// </summary>
        财务收入审核 = 31,
        /// <summary>
        /// 打印单据页眉
        /// </summary>
        页眉 = 32,
        /// <summary>
        /// 打印单据页脚
        /// </summary>
        页脚 = 33,
        /// <summary>
        /// 打印单据模板
        /// </summary>
        Word模版 = 34,
        /// <summary>
        /// 打印单据公章
        /// </summary>
        公司章 = 35,
        //订单毛利配置 = 36,
        //下计调任务跳过 = 37,
        //报账配置 = 38,
        //封团配置 = 39,
        单据配置 = 40,
        /// <summary>
        /// 是否开启KIS整合
        /// </summary>
        是否开启KIS整合 = 41,
        /// <summary>
        /// 景点预控到期在最后保留日前N天提醒
        /// </summary>
        景点预控到期提醒 = 42,
        /// <summary>
        /// 其他预控到期在最后保留日前N天提醒
        /// </summary>
        其他预控到期提醒 = 43,
    }

    #endregion

    #region 系统配置属性枚举
    /*/// <summary>
    /// 团号生成配置
    /// </summary>
    public enum TourNoSetting
    {
        发布计划自动生成 = 1,
        下计调任务时自动生成 = 2,
        手动生成 = 3
    }

    /// <summary>
    /// 报账配置
    /// </summary>
    public enum PlanGuideSetting
    {
        /// <summary>
        /// 只显示导游现付
        /// </summary>
        只显示导游现付 = 0,
        /// <summary>
        /// 显示所有计调
        /// </summary>
        显示所有计调 = 1
    }

    /// <summary>
    /// 封团配置
    /// </summary>
    public enum TourCloseSetting
    {
        /// <summary>
        /// 单团核算结束后封团
        /// </summary>
        单团核算结束后封团 = 0,
        /// <summary>
        /// 导游报账结束后封团
        /// </summary>
        导游报账结束后封团 = 1
    }*/

    /// <summary>
    /// 打印模版配置类型
    /// </summary>
    public enum PrintTemplateType
    {
        /// <summary>
        /// none
        /// </summary>
        None = 0,
        /// <summary>
        /// 团队行程单
        /// </summary>
        团队行程单,
        /// <summary>
        /// 散拼行程单
        /// </summary>
        散拼行程单,
        /// <summary>
        /// 结算单
        /// </summary>
        结算单,
        /// <summary>
        /// 订单信息汇总表
        /// </summary>
        订单信息汇总表,
        /// <summary>
        /// 游客名单
        /// </summary>
        游客名单,
        /// <summary>
        /// 出境游客名单
        /// </summary>
        出境游客名单,
        /// <summary>
        /// 导游任务单
        /// </summary>
        导游任务单,
        /// <summary>
        /// 地接确认单
        /// </summary>
        地接确认单,
        /// <summary>
        /// 酒店确认单
        /// </summary>
        酒店确认单,
        /// <summary>
        /// 用车确认单
        /// </summary>
        用车确认单,
        /// <summary>
        /// 机票确认单
        /// </summary>
        机票确认单,
        /// <summary>
        /// 火车确认单
        /// </summary>
        火车确认单,
        /// <summary>
        /// 汽车确认单
        /// </summary>
        汽车确认单,
        /// <summary>
        /// 景点确认单
        /// </summary>
        景点确认单,
        /// <summary>
        /// 涉外游轮确认单
        /// </summary>
        涉外游轮确认单,
        /// <summary>
        /// 国内游轮确认单
        /// </summary>
        国内游轮确认单,
        /// <summary>
        /// 订餐确认单
        /// </summary>
        订餐确认单,
        /// <summary>
        /// 购物确认单
        /// </summary>
        购物确认单,
        /// <summary>
        /// 其它安排确认单
        /// </summary>
        其它安排确认单,
        /// <summary>
        /// 核算单
        /// </summary>
        核算单,
        /// <summary>
        /// 酒店预控确认单
        /// </summary>
        酒店预控确认单,
        /// <summary>
        /// 游轮预控确认单
        /// </summary>
        游轮预控确认单,
        /// <summary>
        /// 车辆预控确认单
        /// </summary>
        车辆预控确认单,
        /// <summary>
        /// 分销商平台订单信息汇总单
        /// </summary>
        分销商平台订单信息汇总单,
        /// <summary>
        /// 分销商平台散拼线路行程单
        /// </summary>
        分销商平台散拼线路行程单,
        /// <summary>
        /// 分销商平台游客信息打印单
        /// </summary>
        分销商平台游客信息打印单,
        /// <summary>
        /// 景点预控确认单
        /// </summary>
        景点预控确认单,
        /// <summary>
        /// 其他预控确认单
        /// </summary>
        其他预控确认单,
        /// <summary>
        /// 单项业务游客确认单
        /// </summary>
        单项业务游客确认单,
        /// <summary>
        /// 单项业务游客预订单
        /// </summary>
        单项业务供应商确认单
    }

    #endregion

    #region 用户状态
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 未启用=0
        /// </summary>
        未启用 = 0,
        /// <summary>
        /// 正常=1
        /// </summary>
        正常 = 1,
        /// <summary>
        /// 黑名单=2
        /// </summary>
        黑名单 = 2,
        /// <summary>
        /// 已停用=3
        /// </summary>
        已停用 = 3
    }
    #endregion

    /*
    #region 国家省份城市县区获取名称查询类型
    /// <summary>
    /// 国家省份城市县区获取名称查询类型
    /// </summary>
    public enum SelectNameType
    {
        /// <summary>
        /// 国家
        /// </summary>
        国家 = 1,
        /// <summary>
        /// 省份
        /// </summary>
        省份 = 2,
        /// <summary>
        /// 城市
        /// </summary>
        城市 = 3,
        /// <summary>
        /// 县区
        /// </summary>
        县区 = 4
    }
    #endregion*/

    #region 用户登录类型
    /// <summary>
    /// 用户登录类型
    /// </summary>
    public enum UserLoginType
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        用户登录 = 0,
        /// <summary>
        /// 客服登录
        /// </summary>
        客服登录,
        /// <summary>
        /// 自动登录
        /// </summary>
        自动登录,
        /// <summary>
        /// 公共登录
        /// </summary>
        公共登录
    }
    #endregion

    #region 联系人类型
    /// <summary>
    /// 联系人类型
    /// </summary>
    public enum LxrType
    {
        /// <summary>
        /// 客户单位
        /// </summary>
        客户单位 = 0,
        /// <summary>
        /// 供应商
        /// </summary>
        供应商
    }
    #endregion

    #region 用户在线状态
    /// <summary>
    /// 用户在线状态
    /// </summary>
    public enum UserOnlineStatus
    {
        /// <summary>
        /// 离线
        /// </summary>
        Offline = 0,
        /// <summary>
        /// 在线
        /// </summary>
        Online
    }
    #endregion

    #region 用户登录限制类型
    /// <summary>
    /// 用户登录限制类型
    /// </summary>
    public enum UserLoginLimitType
    {
        /// <summary>
        /// 所有登录有效
        /// </summary>
        None,
        /// <summary>
        /// 最早登录有效
        /// </summary>
        Earliest,
        /// <summary>
        /// 最近登录有效
        /// </summary>
        Latest
    }
    #endregion

    #region 支付方式类型
    /// <summary>
    /// 支付方式类型
    /// </summary>
    public enum ZhiFuFangShiLeiXing
    {
        /// <summary>
        /// 用户自定的支付方式
        /// </summary>
        用户自定 = 0,
        /// <summary>
        /// 收入类-现金-系统默认的支付方式
        /// </summary>
        导游现收 = 1,
        /// <summary>
        /// 支出类-现金-系统默认的支付方式
        /// </summary>
        导游现付 = 2,
        /// <summary>
        /// 收入类-现金-系统默认的支付方式
        /// </summary>
        供应商代收 = 3,
        /// <summary>
        /// 支出类-现金-系统默认的支付方式
        /// </summary>
        预付款支付 = 4
    }
    #endregion

    #region 导游级别
    /// <summary>
    /// 导游级别
    /// </summary>
    public enum DaoYouJiBie
    {
        /// <summary>
        /// 初级
        /// </summary>
        初级 = 0,
        /// <summary>
        /// 中级
        /// </summary>
        中级 = 1,
        /// <summary>
        /// 高级
        /// </summary>
        高级 = 2,
        /// <summary>
        /// 见习
        /// </summary>
        见习 = 3,
        /// <summary>
        /// 实习
        /// </summary>
        实习 = 4
    }
    #endregion

    #region 导游类别
    /// <summary>
    /// 导游类别
    /// </summary>
    public enum DaoYouLeiBie
    {
        /// <summary>
        /// 领队
        /// </summary>
        领队 = 0,
        /// <summary>
        /// 全陪
        /// </summary>
        全陪,
        /// <summary>
        /// 地陪
        /// </summary>
        地陪,
        /// <summary>
        /// 景区导游
        /// </summary>
        景区导游,
        /// <summary>
        /// 导游兼领队
        /// </summary>
        导游兼领队 
    }
    #endregion

    #region 职位类型
    /// <summary>
    /// 职位类型
    /// </summary>
    public enum ZhiWeiLeiXing
    {
        /// <summary>
        /// 全职
        /// </summary>
        全职=0,
        /// <summary>
        /// 兼职
        /// </summary>
        兼职
    }
    #endregion
}
