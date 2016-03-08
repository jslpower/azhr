//短信中心-短信任务相关信息业务实体
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SmsStructure
{
    #region 短信任务信息业务实体
    /// <summary>
    /// 短信任务信息业务实体
    /// </summary>
    public class MSmsRenWuInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsRenWuInfo() { }

        /// <summary>
        /// 任务编号
        /// </summary>
        public string RenWuId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing LeiXing { get; set; }
        /// <summary>
        /// 发起人编号
        /// </summary>
        public string FaQiRenId { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public string JieShouRenId { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime? JieShouTime { get; set; }
        /// <summary>
        /// 接收状态
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus JieShouStatus { get; set; }
        /// <summary>
        /// 状态(OUTPUT)
        /// </summary>
        public string Status
        {
            get
            {
                if (LeiXing == EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.行程变化)
                {
                    if (JieShouStatus == EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus.未接收)
                    {
                        return "<a href='javascript:void(0)' class='i_jieshou'>未接收</a>";
                    }

                    return JieShouStatus.ToString();
                }

                if (LeiXing == EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.进店提醒)
                {
                    return "已发送";
                }

                if (LeiXing == EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.进店报账)
                {
                    return HandlerStatus.ToString();
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.RenWuHandlerStatus HandlerStatus { get; set; }
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 上行编号
        /// </summary>
        public string ShangXingId { get; set; }

        string _FaQiRenName = string.Empty;
        /// <summary>
        /// 发起人姓名
        /// </summary>
        public string FaQiRenName
        {
            get
            {
                if (LeiXing == EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.进店提醒) return "系统";

                return _FaQiRenName;
            }

            set
            {
                _FaQiRenName = value;
            }
        }
        string _JieShouRenName = string.Empty;
        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string JieShouRenName
        {
            get
            {
                if (LeiXing == EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.进店报账) return string.Empty;

                return _JieShouRenName;
            }
            set
            {
                _JieShouRenName = value;
            }
        }
        string _JieShouRenDeptName = string.Empty;
        /// <summary>
        /// 接收人部门名称
        /// </summary>
        public string JieShouRenDeptName
        {
            get
            {
                if (LeiXing == EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.进店报账) return string.Empty;

                return _JieShouRenDeptName;
            }
            set
            {
                _JieShouRenDeptName = value;
            }
        }
    }
    #endregion

    #region 短信任务信息查询业务实体
    /// <summary>
    /// 短信任务信息查询业务实体
    /// </summary>
    public class MSmsRenWuChaXunInfo
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 发起人编号
        /// </summary>
        public string FaQiRenId { get; set; }
        /// <summary>
        /// 发起人姓名
        /// </summary>
        public string FaQiRenName { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public string JieShouRenId { get; set; }
        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string JieShouRenName { get; set; }
        /// <summary>
        /// 接收人部门编号
        /// </summary>
        public int? JieShouRenDeptId { get; set; }
        /// <summary>
        /// 接收状态
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus? JieShouStatus { get; set; }
    }
    #endregion

    #region 短信任务接收信息业务实体
    /// <summary>
    /// 短信任务接收信息业务实体
    /// </summary>
    public class MSmsRenWuJieShouInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsRenWuJieShouInfo() { }
        /// <summary>
        /// 任务编号
        /// </summary>
        public string RenWuId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public string JieShouRenId { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime JieShouTime { get; set; }
        /// <summary>
        /// 接收状态
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus JieShouStatus { get; set; }
    }
    #endregion

    #region 短信上行信息业务实体
    /// <summary>
    /// 短信上行信息业务实体
    /// </summary>
    public class MSmsShangXingInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsShangXingInfo() { }

        /// <summary>
        /// 上行编号
        /// </summary>
        public string ShangXingId { get; set; }
        /// <summary>
        /// 号码
        /// </summary>
        public string HaoMa { get; set; }
        /// <summary>
        /// api sms id
        /// </summary>
        public string ApiSmsId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 上行时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
    }
    #endregion

    #region 进店报账信息业务实体
    /// <summary>
    /// 进店报账信息业务实体
    /// </summary>
    public class MSmsJDBZInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string DaoYouBH { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysBH { get; set; }
        /// <summary>
        /// 购物安排编号
        /// </summary>
        public string AnPaiBH { get; set; }
        /// <summary>
        /// 购物成人数
        /// </summary>
        public int GWCR { get; set; }
        /// <summary>
        /// 购物儿童数
        /// </summary>
        public int GWET { get; set; }
        /// <summary>
        /// 购物流水
        /// </summary>
        public decimal LiuShui { get; set; }
        /// <summary>
        /// 购物明细
        /// </summary>
        public IList<MSmsJDBZXXInfo> XXs { get; set; }
        /// <summary>
        /// 发起人编号(OUTPUT)
        /// </summary>
        public string FaQiRenId { get; set; }
    }
    #endregion

    #region 进店报账购物信息业务实体
    /// <summary>
    /// 进店报账购物信息业务实体
    /// </summary>
    public class MSmsJDBZXXInfo
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string CPBH { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int CPSL { get; set; }
    }
    #endregion

    #region 行程变化信息业务实体
    /// <summary>
    /// 行程变化信息业务实体
    /// </summary>
    public class MSmsXCBHInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string DaoYouBH { get; set; }
        /// <summary>
        /// 变化内容
        /// </summary>
        public string BianHuaNeiRong { get; set; }
        /// <summary>
        /// 发起人编号(OUTPUT)
        /// </summary>
        public string FaQiRenId { get; set; }
        /// <summary>
        /// 接收人编号(OUTPUT)
        /// </summary>
        public string JieShouRenId { get; set; }
    }
    #endregion
}
