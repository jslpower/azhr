using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    #region 公司配置信息业务实体
    /// <summary>
    /// 公司配置信息业务实体
    /// </summary>
    [Serializable]
    public class MComSetting
    {
        private string _TourNoSetting = "35";
        private int _ShowBeforeMonth = 12;
        private int _ShowAfterMonth = 12;
        private int _SaveTime = 60;
        private int _CountryArea = 3;
        private int _ProvinceArea = 3;
        private int _ExitArea = 15;
        private int _IntegralProportion = 1;
        private int _ContractRemind = 15;
        private int _SContractRemind = 15;
        private int _ComPanyContractRemind = 15;
        private int _HotelControlRemind = 10;
        private int _CarControlRemind = 10;
        private int _ShipControlRemind = 10;
        private bool _FinancialExpensesReview = true;
        private bool _FinancialIncomeReview = true;

        /// <summary>
        /// default constructor
        /// </summary>
        public MComSetting() { }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团号生成配置
        /// </summary>
        public string TourNoSetting
        {
            get { return _TourNoSetting; }
            set { _TourNoSetting = value; }
        }
        /// <summary>
        /// 列表显示控制(自定义显示-显示当前时间前几个月数据)
        /// </summary>
        public int ShowBeforeMonth
        {
            get { return _ShowBeforeMonth; }
            set { _ShowBeforeMonth = value; }
        }
        /// <summary>
        /// 列表显示控制(自定义显示-显示当前时间后几个月数据)
        /// </summary>
        public int ShowAfterMonth
        {
            get { return _ShowAfterMonth; }
            set { _ShowAfterMonth = value; }
        }
        /// <summary>
        /// 订单最长留位时间，单位分钟
        /// </summary>
        public int SaveTime
        {
            get { return _SaveTime; }
            set { _SaveTime = value; }
        }
        /*/// <summary>
        /// 提醒时间配置(单位：分钟)
        /// </summary>
        public int RemindTime { get; set; }*/
        /// <summary>
        /// 计划停收配置(国内线提前N天停收)单位：天
        /// </summary>
        public int CountryArea
        {
            get { return _CountryArea; }
            set { _CountryArea = value; }
        }
        /// <summary>
        /// 计划停收配置(省内线提前N天停收)单位：天
        /// </summary>
        public int ProvinceArea
        {
            get { return _ProvinceArea; }
            set { _ProvinceArea = value; }
        }
        /// <summary>
        /// 计划停收配置(出境线提前N天停收)单位：天
        /// </summary>
        public int ExitArea
        {
            get { return _ExitArea; }
            set { _ExitArea = value; }
        }
        /// <summary>
        /// 个人会员报名积分比例
        /// </summary>
        public int IntegralProportion
        {
            get { return _IntegralProportion; }
            set { _IntegralProportion = value; }
        }
        /// <summary>
        /// 是否跳过导游报账
        /// </summary>
        public bool SkipGuide { get; set; }
        /// <summary>
        /// 是否跳过销售报账
        /// </summary>
        public bool SkipSale { get; set; }
        /// <summary>
        /// 是否跳过计调终审
        /// </summary>
        public bool SkipFinalJudgment { get; set; }
        /*/// <summary>
        /// 出团提醒
        /// </summary>
        public int GroupRemind { get; set; }
        /// <summary>
        /// 回团提醒
        /// </summary>
        public int BackRemind { get; set; }
        /// <summary>
        /// 收款提醒
        /// </summary>
        public int CollectionRemind { get; set; }
        /// <summary>
        /// 付款提醒
        /// </summary>
        public int PaymnetRemind { get; set; }
        /// <summary>
        /// 变更提醒
        /// </summary>
        public int ChangeRemind { get; set; }*/
        /// <summary>
        /// 劳动合同到期提前N天提醒
        /// </summary>
        public int ContractRemind
        {
            get { return _ContractRemind; }
            set { _ContractRemind = value; }
        }
        /// <summary>
        /// 供应商合同到期在到期前N天提醒
        /// </summary>
        public int SContractRemind
        {
            get { return _SContractRemind; }
            set { _SContractRemind = value; }
        }
        /// <summary>
        /// 公司合同到期在到期前N天提醒
        /// </summary>
        public int ComPanyContractRemind
        {
            get { return _ComPanyContractRemind; }
            set { _ComPanyContractRemind = value; }
        }
        /// <summary>
        /// 洒店预控到期在最后保留日前N天提醒
        /// </summary>
        public int HotelControlRemind
        {
            get { return _HotelControlRemind; }
            set { _HotelControlRemind = value; }
        }
        /// <summary>
        /// 车辆预控到期在最后保留日前N天提醒
        /// </summary>
        public int CarControlRemind
        {
            get { return _CarControlRemind; }
            set { _CarControlRemind = value; }
        }
        /// <summary>
        /// 游船预控到期在最后保留日前N天提醒
        /// </summary>
        public int ShipControlRemind
        {
            get { return _ShipControlRemind; }
            set { _ShipControlRemind = value; }
        }
        /// <summary>
        /// 景点预控到期在最后保留日前N天提醒
        /// </summary>
        public int SightControlRemind { get; set; }
        /// <summary>
        /// 其他预控到期在最后保留日前N天提醒
        /// </summary>
        public int OtherControlRemind { get; set; }
        /*/// <summary>
        /// 物品借阅到期提醒
        /// </summary>
        public int BorrowingRemind { get; set; }
        /// <summary>
        /// 生日提醒
        /// </summary>
        public int BirthdayRemind { get; set; }
        /// <summary>
        /// 公告提醒
        /// </summary>
        public int AnnouncementRemind { get; set; }
        /// <summary>
        /// 弹窗提醒
        /// </summary>
        public int PopRemind { get; set; }
        /// <summary>
        /// 询价提醒
        /// </summary>
        public int InquiryRemind { get; set; }*/
        /// <summary>
        /// 财务付款登记是否需要审核
        /// </summary>
        public bool FinancialExpensesReview
        {
            get { return _FinancialExpensesReview; }
            set { _FinancialExpensesReview = value; }
        }
        /// <summary>
        /// 财务收款登记是否需要审核
        /// </summary>
        public bool FinancialIncomeReview
        {
            get { return _FinancialIncomeReview; }
            set { _FinancialIncomeReview = value; }
        }
        /// <summary>
        /// 是否做欠款额度控制
        /// </summary>
        public bool ArrearsRangeControl { get; set; }
        /// <summary>
        /// 打印单据页眉
        /// </summary>
        public string PagePath { get; set; }
        /// <summary>
        /// 打印单据页脚
        /// </summary>
        public string FooterPath { get; set; }
        /// <summary>
        /// 打印单据模板
        /// </summary>
        public string WordTemplate { get; set; }
        /// <summary>
        /// 打印单据公章
        /// </summary>
        public string CompanyChapter { get; set; }
        /*/// <summary>
        /// 订单毛利配置
        /// </summary>
        public bool OrderCost { get; set; }
        /// <summary>
        /// 下计调任务跳过
        /// </summary>
        public bool PlanTaskJump { get; set; }*/
        /*/// <summary>
        /// 报账配置
        /// </summary>
        public EnumType.ComStructure.PlanGuideSetting PlanGuide { get; set; }
        /// <summary>
        /// 封团配置
        /// </summary>
        public EnumType.ComStructure.TourCloseSetting TourClose { get; set; }*/
        /// <summary>
        /// 打印模版配置
        /// </summary>
        public IList<PrintDocument> PrintDocument { get; set; }
        /// <summary>
        /// 短信配置信息
        /// </summary>
        public MSmsConfigInfo SmsConfig { get; set; }
        /// <summary>
        /// 是否开启KIS整合
        /// </summary>
        public bool IsEnableKis { get; set; }
        /// <summary>
        /// 手机端Logo
        /// </summary>
        public string MLogo { get; set; }
        /// <summary>
        /// 外Logo
        /// </summary>
        public string WLogo { get; set; }
        /// <summary>
        /// 内Logo
        /// </summary>
        public string NLogo { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 用户数量上限
        /// </summary>
        public int MaxUserNumber { get; set; }
        /// <summary>
        /// 用户登录限制类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserLoginLimitType UserLoginLimitType { get; set; }
        /// <summary>
        /// 供应商LOGO
        /// </summary>
        public string GYSLogo { get; set; }
        /// <summary>
        /// 分销商LOGO
        /// </summary>
        public string FXSLogo { get; set; }
        /// <summary>
        /// 是否开启短线
        /// </summary>
        public bool IsEnableDuanXian { get; set; }
        /// <summary>
        /// 是否自动删除出团时间超过5天，销售未派计划，没有订单的散拼计划
        /// </summary>
        public bool IsZiDongShanChuSanPinJiHua { get; set; }
    }
    #endregion

    #region 打印模版配置信息业务实体
    /// <summary>
    /// 打印模版配置信息业务实体
    /// </summary>
    [Serializable]
    public class PrintDocument
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public PrintDocument() { }

        /// <summary>
        /// 打印模版类型
        /// </summary>
        public EnumType.ComStructure.PrintTemplateType PrintTemplateType { get; set; }
        /// <summary>
        /// 打印模版文件路径
        /// </summary>
        public string PrintTemplate { get; set; }
    }
    #endregion

    #region 公司短信配置信息业务实体
    /// <summary>
    /// 公司短信配置信息业务实体
    /// </summary>
    [Serializable]
    public class MSmsConfigInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsConfigInfo() { }

        /// <summary>
        /// 短信账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 短信账户AppKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 短信账户AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 短信账户配置信息是否完整
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return !(string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(AppKey) || string.IsNullOrEmpty(AppSecret));
            }
        }
    }
    #endregion

    #region 金蝶默认科目配置信息业务实体
    /// <summary>
    /// 金蝶默认科目配置信息业务实体
    /// </summary>
    public class MKisConfigInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MKisConfigInfo() { }

        public string Kis1000 { get; set; }
        public string Kis1999 { get; set; }
        public string Kis2000 { get; set; }
        public string Kis2999 { get; set; }
        public string Kis3000 { get; set; }
        public string Kis3999 { get; set; }
        public string Kis4000 { get; set; }
        public string Kis4500 { get; set; }
        public string Kis4501 { get; set; }
        public string Kis4999 { get; set; }
        public string Kis5000 { get; set; }
        public string Kis5001 { get; set; }
        public string Kis5002 { get; set; }
        public string Kis5003 { get; set; }
        public string Kis5004 { get; set; }
        public string Kis5005 { get; set; }
        public string Kis5006 { get; set; }
        public string Kis5007 { get; set; }
        public string Kis5008 { get; set; }
        public string Kis5009 { get; set; }
        public string Kis5999 { get; set; }
        public string Kis6000 { get; set; }
        public string Kis6001 { get; set; }
        public string Kis6002 { get; set; }
        public string Kis6003 { get; set; }
        public string Kis6500 { get; set; }
        public string Kis6501 { get; set; }
        public string Kis6999 { get; set; }
    }
    #endregion
}
