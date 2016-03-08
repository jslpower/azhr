using System;
using System.Collections.Generic;

namespace EyouSoft.Model.CrmStructure
{
    #region 个人会员信息业务实体
    /// <summary>
    /// 个人会员信息业务实体
    /// </summary>
    /// 汪奇志 2012-04-27
    public class MCrmPersonalInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmPersonalInfo() { }

        /// <summary>
        /// 个人会员编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 区县编号
        /// </summary>
        public int DistrictId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 简码
        /// </summary>
        public string BriefCode { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender Gender { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCardCode { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string National { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string HomeAddress { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string DanWei { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary>
        public string DanWeiAddress { get; set; }
        /// <summary>
        /// 会员类型编号
        /// </summary>
        public int MemberTypeId { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string MemberCardCode { get; set; }
        /// <summary>
        /// 会员状态
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.CrmMemberState MemberStatus { get; set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime? JoinTime { get; set; }
        /// <summary>
        /// 报名类型
        /// </summary>
        public string JoinType { get; set; }
        /// <summary>
        /// 会费
        /// </summary>
        public decimal HuiFei { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 是否生日提醒
        /// </summary>
        public bool IsTiXing { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.CrmType CrmType { get { return EyouSoft.Model.EnumType.CrmStructure.CrmType.个人会员; } }
        /// <summary>
        /// 责任销售编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 责任销售姓名
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CardType? CardType { get; set; }
        /// <summary>
        /// 证件有效期
        /// </summary>
        public string CardValidDate { get; set; }
        /// <summary>
        /// 签发日期
        /// </summary>
        public DateTime? QianFaDate { get; set; }
        /// <summary>
        /// 签发地
        /// </summary>
        public string QianFaDi { get; set; }
    }
    #endregion    

    #region 个人会员列表信息业务实体
    /// <summary>
    /// 个人会员列表信息业务实体
    /// </summary>
    /// 汪奇志 2012-04-28
    public class MLBCrmPersonalInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBCrmPersonalInfo() { }

        /// <summary>
        /// 会员编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 会员类型名称
        /// </summary>
        public string MemberTypeName { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string MemberCardCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender Gender { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int DingDanShu { get; set; }
        /// <summary>
        /// 订单人数
        /// </summary>
        public int DingDanRenShu { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal DingDanJinE { get; set; }
        /// <summary>
        /// 拖欠金款
        /// </summary>
        public decimal TuoQianJinE { get; set; }
        /// <summary>
        /// 最后消费时间
        /// </summary>
        public DateTime? LatestTime { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 责任销售编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CardType? CardType { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// 签发日期
        /// </summary>
        public DateTime? QianFaDate { get; set; }
        /// <summary>
        /// 证件有效期
        /// </summary>
        public DateTime? CardValidDate { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 签发地
        /// </summary>
        public string QianFaDi { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion

    #region 个人会员查询信息业务实体
    /// <summary>
    /// 个人会员查询信息业务实体
    /// </summary>
    public class MLBCrmPersonalSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBCrmPersonalSearchInfo() { }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string MemberCardCode { get; set; }
        /// <summary>
        /// 会员类型编号
        /// </summary>
        public int? MemberTypeId { get; set; }
        /// <summary>
        /// 积分查询操作符
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.EqualSign? JiFenOperator { get; set; }
        /// <summary>
        /// 积分查询操作数
        /// </summary>
        public decimal? JiFenOperatorNumber { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string Name { get; set; }
    }
    #endregion

    #region 个人会员积分信息业务实体
    /// <summary>
    /// 个人会员积分信息业务实体
    /// </summary>
    /// 汪奇志 2012-05-02
    public class MJiFenInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiFenInfo() { }

        /// <summary>
        /// 个人会员编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 积分增减类别
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.JiFenZengJianLeiBie ZengJianLeiBie { get; set; }
        /// <summary>
        /// 积分时间
        /// </summary>
        public DateTime JiFenShiJian { get; set; }
        /// <summary>
        /// 积分值
        /// </summary>
        public decimal JiFen { get; set; }
        /// <summary>
        /// 积分说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion
}
