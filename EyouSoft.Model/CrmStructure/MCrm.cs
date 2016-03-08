using System;
using System.Collections.Generic;

namespace EyouSoft.Model.CrmStructure
{
    #region 客户信息业务实体
    /// <summary>
    /// 客户信息业务实体
    /// </summary>
    public class MCrm
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrm() { }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        public int CountyId { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        public Model.EnumType.CrmStructure.CrmType Type { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 客户等级
        /// </summary>
        public int LevId { get; set; }
        /// <summary>
        /// 许可证号
        /// </summary>
        public string License { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string LegalRepresentative { get; set; }
        /// <summary>
        /// 法人代表电话
        /// </summary>
        public string LegalRepresentativePhone { get; set; }
        /// <summary>
        /// 法人手机
        /// </summary>
        public string LegalRepresentativeMobile { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 财务姓名
        /// </summary>
        public string FinancialName { get; set; }
        /// <summary>
        /// 财务电话
        /// </summary>
        public string FinancialPhone { get; set; }
        /// <summary>
        /// 财务手机
        /// </summary>
        public string FinancialMobile { get; set; }
        /// <summary>
        /// 财务传真
        /// </summary>
        public string FinancialFax { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.PayType PayType { get; set; }
        /// <summary>
        /// 挂账期限
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 简码
        /// </summary>
        public string BrevityCode { get; set; }
        /// <summary>
        /// 欠款额度
        /// </summary>
        public decimal AmountOwed { get; set; }
        /// <summary>
        /// 返利政策
        /// </summary>
        public string RebatePolicy { get; set; }
        /// <summary>
        /// 是否签订协议
        /// </summary>
        public bool IsSignContract { get; set; }
        /// <summary>
        /// 协议附件
        /// </summary>
        public Model.ComStructure.MComAttach AttachModel { get; set; }
        /// <summary>
        /// 责任销售编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 单团账龄期限(单位天)
        /// </summary>
        public int Deadline { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public IList<Model.CrmStructure.MCrmLinkman> LinkManList { get; set; }
        /// <summary>
        /// 结算账户
        /// </summary>
        public IList<Model.CrmStructure.MCrmBank> BankList { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 打印页眉
        /// </summary>
        public string PrintHeader { get; set; }
        /// <summary>
        /// 打印页脚
        /// </summary>
        public string PrintFooter { get; set; }
        /// <summary>
        /// 打印模板
        /// </summary>
        public string PrintTemplates { get; set; }
        /// <summary>
        /// 打印公章
        /// </summary>
        public string Seal { get; set; }
        /// <summary>
        /// 责任销售姓名
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 外语名称
        /// </summary>
        public string ForeignName { get; set; }
        /// <summary>
        /// 分销状态
        /// </summary>
        public bool SaleState { get; set; }
        /// <summary>
        /// 分销账号
        /// </summary>
        public string SaleName { get; set; }
        /// <summary>
        /// 分销密码
        /// </summary>
        public string SalePwd { get; set; }


    }
    #endregion

    #region 客户帐号管理列表信息业务实体
    /// <summary>
    /// 客户帐号管理列表信息业务实体
    /// </summary>
    public class MCrmUserInfo
    {
        /// <summary>
        /// 联系人编号
        /// </summary>
        public string LinkManId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系人名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 帐号状态
        /// </summary>
        public Model.EnumType.ComStructure.UserStatus Status { get; set; }
    }
    #endregion

    #region 客户单位列表信息业务实体
    /// <summary>
    /// 客户单位列表信息业务实体
    /// </summary>
    [Serializable]
    public class MLBCrmInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBCrmInfo() { }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }        
        /// <summary>
        /// 是否签订合同
        /// </summary>
        public bool IsXieYi { get; set; }        
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
        /// 责任销售编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 责任销售名称
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        public Model.EnumType.CrmStructure.CrmType Type { get; set; }
        /// <summary>
        /// 客户等级
        /// </summary>
        public int LevId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 县区编号
        /// </summary>
        public int DistrictId{get;set;}
        /// <summary>
        /// 最后消费时间
        /// </summary>
        public DateTime? LastTime { get; set; }
        /// <summary>
        /// 联系人信息集合
        /// </summary>
        public IList<EyouSoft.Model.CrmStructure.MCrmLinkman> Lxrs { get; set; }
        /// <summary>
        /// 国家省份城市县区名称
        /// </summary>
        public EyouSoft.Model.ComStructure.MCPCC CPCD { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
    }
    #endregion

    #region 客户单位查询信息业务实体
    /// <summary>
    /// 客户单位查询信息业务实体
    /// </summary>
    /// 汪奇志 2012-04-26
    public class MLBCrmSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBCrmSearchInfo() { }

        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string CrmName { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int? DengJiBH { get; set; }
        /// <summary>
        /// 责任销售名称
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 责任销售编号
        /// </summary>
        public string SellerId { get; set; }
        /*/// <summary>
        /// 简码
        /// </summary>
        public string BriefCode { get; set; }*/
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LxrName { get; set; }
    }
    #endregion

    #region 客户单位选用列表信息业务实体
    /// <summary>
    /// 客户单位选用列表信息业务实体
    /// </summary>
    /// 汪奇志 2012-04-26
    public class MLBCrmXuanYongInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBCrmXuanYongInfo() { }

        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 客户单位类型
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.CrmType CrmType { get; set; }
        /// <summary>
        /// 联系人信息集合
        /// </summary>
        public IList<EyouSoft.Model.CrmStructure.MCrmLinkman> Lxrs { get; set; }
        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int KeHuDengJiBH { get; set; }
        /// <summary>
        /// 客户国籍
        /// </summary>
        public int CountryId { get; set; }
    }
    #endregion

    #region 客户单位交易明细信息业务实体
    /// <summary>
    /// 客户单位交易明细信息业务实体
    /// </summary>
    public class MCrmJiaoYiMingXiInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmJiaoYiMingXiInfo() { }

        /// <summary>
        /// 订单数
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
        /// 拖欠金额
        /// </summary>
        public decimal TuoQianJinE { get { return DingDanJinE - YiShouJinE; } }
        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal YiShouJinE { get; set; }
        /// <summary>
        /// 最近下单时间
        /// </summary>
        public DateTime? LatestTime { get; set; }
    }
    #endregion
}
