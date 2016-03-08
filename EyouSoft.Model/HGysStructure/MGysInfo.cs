using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HGysStructure
{
    #region 供应商联系人信息业务实体
    /// <summary>
    /// 供应商联系人信息业务实体
    /// </summary>
    public class MLxrInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLxrInfo() { }
        /// <summary>
        /// 联系人编号
        /// </summary>
        public string LxrId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string ZhiWu { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 生日提醒
        /// </summary>
        public bool IsTiXing { get; set; }
        /// <summary>
        /// 电子信箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// MSN
        /// </summary>
        public string MSN { get; set; }
    }
    #endregion

    #region 供应商合同信息业务实体
    /// <summary>
    /// 供应商合同信息业务实体
    /// </summary>
    public class MHeTongInfo
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HeTongId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 合同附件
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 保底量
        /// </summary>
        public int BaoDi { get;set; }
        /// <summary>
        /// 奖励量
        /// </summary>
        public int JiangLi { get; set; }
    }
    #endregion

    #region 供应商附件信息业务实体
    /// <summary>
    /// 供应商附件信息业务实体
    /// </summary>
    public class MFuJianInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MFuJianInfo() { }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string FilePath { get; set; }
    }
    #endregion

    #region 供应商景点景点信息业务实体
    /// <summary>
    /// 供应商景点景点信息业务实体
    /// </summary>
    public class MJingDianJingDianInfo
    {
        /// <summary>
        /// 景点编号
        /// </summary>
        public string JingDianId { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JingDianXingJi XingJi { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string WangZhi { get; set; }
        /// <summary>
        /// 浏览时间
        /// </summary>
        public string YouLanShiJian { get; set; }
        /// <summary>
        /// 景点电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 景点简介
        /// </summary>
        public string JianJie { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsTuiJian { get; set; }
        /// <summary>
        /// 景点附件
        /// </summary>
        public MFuJianInfo FuJian { get; set; }
        /// <summary>
        /// 是否秀
        /// </summary>
        public bool IsXiu { get; set; }
    }
    #endregion

    #region 供应商购物店合同信息业务实体
    /// <summary>
    /// 供应商购物店合同信息业务实体
    /// </summary>
    public class MGouWuHeTongInfo
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HeTongId { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string GuoJi { get; set; }
        /// <summary>
        /// 流水
        /// </summary>
        public decimal LiuShui { get; set; }
        /// <summary>
        /// 人头-成人
        /// </summary>
        public decimal RenTouCR { get; set; }
        /// <summary>
        /// 人头-儿童
        /// </summary>
        public decimal RenTouET { get; set; }
        /// <summary>
        /// 合同附件
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 保底金额
        /// </summary>
        public decimal BaoDiJinE { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsQiYong { get; set; }
    }
    #endregion

    #region 供应商购物店产品信息业务实体
    /// <summary>
    /// 供应商购物店产品信息业务实体
    /// </summary>
    public class MGouWuChanPinInfo
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ChanPinId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal XiaoShouJinE { get; set; }
        /// <summary>
        /// 返点金额
        /// </summary>
        public decimal FanDianJinE { get; set; }
        /// <summary>
        /// 产品短信编号
        /// </summary>
        public int IdentityId { get; set; }
    }
    #endregion

    #region 供应商购物店其他信息业务实体
    /// <summary>
    /// 供应商购物店其他信息业务实体
    /// </summary>
    public class MGouWuInfo
    {
        /// <summary>
        /// 商品类别
        /// </summary>
        public string ShangPinLeiBie { get; set; }
        /// <summary>
        /// 保底金额
        /// </summary>
        public decimal BaoDiJinE { get; set; }
        /// <summary>
        /// 报价-人头-成人
        /// </summary>
        public decimal RenTouCR { get; set; }
        /// <summary>
        /// 报价-人头-儿童
        /// </summary>
        public decimal RenTouET { get; set; }
        /// <summary>
        /// 报价-流水
        /// </summary>
        public decimal LiuShui { get; set; }
        /// <summary>
        /// 购物店产品信息集合
        /// </summary>
        public IList<MGouWuChanPinInfo> ChanPins { get; set; }
        /// <summary>
        /// 购物合同信息集合
        /// </summary>
        public IList<MGouWuHeTongInfo> HeTongs { get; set; }
    }
    #endregion

    #region 供应商-其他供应商信息业务实体
    /// <summary>
    /// 供应商-其他供应商信息业务实体
    /// </summary>
    public class MQiTaInfo
    {
        /// <summary>
        /// 价格
        /// </summary>
        public decimal JiaGe { get; set; }
        /// <summary>
        /// 结算价格
        /// </summary>
        public decimal JieSuanJiaGe { get; set; }
    }
    #endregion

    #region 供应商-酒店信息业务实体
    /// <summary>
    /// 供应商-酒店信息业务实体
    /// </summary>
    public class MJiuDianInfo
    {
        /// <summary>
        /// 酒店前台电话
        /// </summary>
        public string QianTaiTelephone { get; set; }
        /// <summary>
        /// 酒店星级
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi XingJi { get; set; }
        /// <summary>
        /// 酒店图片信息
        /// </summary>
        public IList<MFuJianInfo> FuJians { get; set; }
        /// <summary>
        /// 报价信息集合
        /// </summary>
        public IList<MJiuDianBaoJiaInfo> BaoJias { get; set; }
    }
    #endregion

    #region 供应商-餐馆信息业务实体
    /// <summary>
    /// 供应商-餐馆信息业务实体
    /// </summary>
    public class MCanGuanInfo
    {
        /// <summary>
        /// 餐馆-餐标
        /// </summary>
        public string CanBiao { get; set; }
        /// <summary>
        /// 餐馆-菜系
        /// </summary>
        public string CaiXi { get; set; }
    }
    #endregion

    #region 供应商-车型信息业务实体
    /// <summary>
    /// 供应商-车型信息业务实体
    /// </summary>
    public class MCheXingInfo
    {
        /// <summary>
        /// 车型编号
        /// </summary>
        public string CheXingId { get; set; }
        /// <summary>
        /// 车型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 车型图片
        /// </summary>
        public MFuJianInfo FuJian { get; set; }
        /// <summary>
        /// 座位数
        /// </summary>
        public int ZuoWeiShu { get; set; }
        /// <summary>
        /// 单价基数
        /// </summary>
        public decimal DanJiaJiShu { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
    }
    #endregion

    #region 供应商-酒店报价信息业务实体
    /// <summary>
    /// 供应商-酒店报价信息业务实体
    /// </summary>
    public class MJiuDianBaoJiaInfo
    {
        /// <summary>
        /// 团型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType TuanXing { get; set; }
        /// <summary>
        /// 宾客类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType BinKeLeiXing { get; set; }
        /// <summary>
        /// 平-价格
        /// </summary>
        public decimal PJiaGe { get; set; }
        /// <summary>
        /// 平-备注
        /// </summary>
        public string PBeiZhu { get; set; }
        /// <summary>
        /// 淡-价格
        /// </summary>
        public decimal DJiaGe { get; set; }
        /// <summary>
        /// 淡-备注
        /// </summary>
        public string DBeiZhu { get; set; }
        /// <summary>
        /// 旺-价格
        /// </summary>
        public decimal WJiaGe { get; set; }
        /// <summary>
        /// 旺-备注
        /// </summary>
        public string WBeiZhu { get; set; }
    }
    #endregion    

    #region 供应商信息业务实体
    /// <summary>
    /// 供应商信息业务实体
    /// </summary>
    public class MGysInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGysInfo() { }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing LeiXing { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 国家省份城市县区
        /// </summary>
        public EyouSoft.Model.ComStructure.MCPCC CPCD { get; set; }
        /// <summary>
        /// 许可证号
        /// </summary>
        public string XuKeZhengCode { get; set; }
        /// <summary>
        /// 法人姓名
        /// </summary>
        public string FaRenName { get; set; }
        /// <summary>
        /// 法人电话
        /// </summary>
        public string FaRenTelephone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string JianJie { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 是否签单
        /// </summary>
        public bool IsQianDan { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsTuiJian { get; set; }
        /// <summary>
        /// 是否返单
        /// </summary>
        public bool IsFanDan { get; set; }
        /// <summary>
        /// 是否签订合同
        /// </summary>
        public bool IsHeTong { get; set; }
        /// <summary>
        /// 是否16免1
        /// </summary>
        public bool IsMianYi { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string WangZhi { get; set; }
        /// <summary>
        /// 联系人信息集合
        /// </summary>
        public IList<MLxrInfo> Lxrs { get; set; }
        /// <summary>
        /// 合同信息集合
        /// </summary>
        public IList<MHeTongInfo> HeTongs { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JieSuanFangShi JieSuanFangShi { get; set; }
        /// <summary>
        /// 挂账期限
        /// </summary>
        public string GuaZhangQiXian { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 最后操作人编号(OUTPUT)
        /// </summary>
        public string LatestOperatorId { get; set; }
        /// <summary>
        /// 最后操作人姓名(OUTPUT)
        /// </summary>
        public string LatestOperatorName { get; set; }
        /// <summary>
        /// 最后操作人时间(OUTPUT)
        /// </summary>
        public DateTime LatestTime { get; set; }        
        /// <summary>
        /// 酒店信息
        /// </summary>
        public MJiuDianInfo JiuDian { get; set; }
        /// <summary>
        /// 景点信息集合
        /// </summary>
        public IList<MJingDianJingDianInfo> JingDians { get; set; }
        /// <summary>
        /// 购物店信息
        /// </summary>
        public MGouWuInfo GouWu { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        public MQiTaInfo QiTa { get; set; }
        /// <summary>
        /// 餐馆信息
        /// </summary>
        public MCanGuanInfo CanGuan { get; set; }
        /// <summary>
        /// 车型信息集合
        /// </summary>
        public IList<MCheXingInfo> CheXings { get; set; }
        /// <summary>
        /// 短信编号
        /// </summary>
        public int IdentityId { get; set; }
    }
    #endregion
}
