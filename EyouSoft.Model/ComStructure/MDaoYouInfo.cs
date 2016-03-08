using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    #region 导游信息业务实体
    /// <summary>
    /// 导游信息业务实体
    /// </summary>
    public class MDaoYouInfo
    {
        /// <summary>
        /// 导游编号
        /// </summary>
        public string DaoYouId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public EyouSoft.Model.ComStructure.MPasswordInfo Pwd { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XingMing { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender Gender { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie JiBie { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public IList<EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie> LeiBies { get; set; }
        /// <summary>
        /// 语种
        /// </summary>
        public string YuZhong { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string ShenFenZhengHao { get; set; }
        /// <summary>
        /// 导游证号
        /// </summary>
        public string DaoYouZhengHao { get; set; }
        /// <summary>
        /// 领队证号
        /// </summary>
        public string LingDuiZhengHao { get; set; }
        /// <summary>
        /// 挂靠单位
        /// </summary>
        public string GuaKaoDanWei { get; set; }
        /// <summary>
        /// 是否年审
        /// </summary>
        public bool IsNianShen { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string ShouJiHao { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// MSN SKYPE
        /// </summary>
        public string MSN { get; set; }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string JiaTingTelephone { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string JiaTingDiZhi { get; set; }
        /// <summary>
        /// 照片路径
        /// </summary>
        public string ZhaoPianFilePath { get; set; }
        /// <summary>
        /// 性格特点
        /// </summary>
        public string XingGeTeDian { get; set; }
        /// <summary>
        /// 擅长线路
        /// </summary>
        public string ShanChangXianLu { get; set; }
        /// <summary>
        /// 客户评价
        /// </summary>
        public string KeHuPingJia { get; set; }
        /// <summary>
        /// 特长
        /// </summary>
        public string TeChang { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 职位类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.ZhiWeiLeiXing ZhiWeiLeiXing { get; set; }

        /// <summary>
        /// 导游类别(GET)
        /// </summary>
        public string LeiBie
        {
            get
            {
                if (LeiBies == null || LeiBies.Count == 0) return string.Empty;
                string s = string.Empty;

                foreach (var item in LeiBies)
                {
                    s += item.ToString()+",";
                }

                if (!string.IsNullOrEmpty(s))
                {
                    return s.TrimEnd(',');
                }

                return string.Empty;
            }
        }
        /// <summary>
        /// 带团次数
        /// </summary>
        public int DaiTuanCiShu { get; set; }
        /// <summary>
        /// 带团天数
        /// </summary>
        public int DaiTuanTianShu { get; set; }
        /// <summary>
        /// 导游编号
        /// </summary>
        public int IdentityId { get; set; }

    }
    #endregion

    #region 导游信息查询业务实体
    /// <summary>
    /// 导游信息查询业务实体
    /// </summary>
    public class MDaoYouChaXunInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string XingMing { get; set; }
        /// <summary>
        /// 语种
        /// </summary>
        public string YuZhong { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender? Gender { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie? LeiBie {get;set;}
        /// <summary>
        /// 级别
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie? JiBie { get; set; } 
    }
    #endregion

    #region 导游带团明细信息业务实体
    /// <summary>
    /// 导游带团明细信息业务实体
    /// </summary>
    public class MDaoYouDaiTuanXXInfo
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 抵达日期
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 行程天数
        /// </summary>
        public int XCTS { get; set; }
        /// <summary>
        /// 入境成人数
        /// </summary>
        public int RJCR { get; set; }
        /// <summary>
        /// 入境儿童数
        /// </summary>
        public int RJET { get; set; }
        /// <summary>
        /// 入境领队数
        /// </summary>
        public int RJLD { get; set; }
        /// <summary>
        /// 入境人数
        /// </summary>
        public int RJRS { get { return RJCR + RJET + RJLD; } }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 带团起始时间
        /// </summary>
        public DateTime DTSTime { get; set; }
        /// <summary>
        /// 带团截止时间
        /// </summary>
        public DateTime DTETime { get; set; }
    }
    #endregion

    #region 导游带团明细信息查询业务实体
    /// <summary>
    /// 导游带团明细信息查询业务实体
    /// </summary>
    public class MDaoYouDaiTuanXXChaXunInfo
    {
        /// <summary>
        /// 出团起始时间
        /// </summary>
        public DateTime? SLDate { get; set; }
        /// <summary>
        /// 出团截止时间
        /// </summary>
        public DateTime? ELDate { get; set; }
        /// <summary>
        /// 带团起始时间
        /// </summary>
        public DateTime? SDTTime { get; set; }
        /// <summary>
        /// 带团截止时间
        /// </summary>
        public DateTime? EDTTime { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
    }
    #endregion

    #region 导游上团统计信息业务实体
    /// <summary>
    /// 导游上团统计信息业务实体
    /// </summary>
    public class MDaoYouShangTuanInfo
    {
        /// <summary>
        /// 导游编号
        /// </summary>
        public string DaoYouId { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
        /// <summary>
        /// 团队数
        /// </summary>
        public int TuanDuiShu { get; set; }
        /// <summary>
        /// 带团天数
        /// </summary>
        public int TianShu { get; set; }
        /// <summary>
        /// 团天数
        /// </summary>
        public int TuanTianShu { get { return TuanDuiShu * TianShu; } }
    }
    #endregion

    #region 导游上团统计信息查询业务实体
    /// <summary>
    /// 导游上团统计信息查询业务实体
    /// </summary>
    public class MDaoYouShangTuanChaXunInfo
    {
        /// <summary>
        /// 导游编号
        /// </summary>
        public string DaoYouId { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
        /// <summary>
        /// 带团起始时间
        /// </summary>
        public DateTime? SDTTime { get; set; }
        /// <summary>
        /// 带团截止时间
        /// </summary>
        public DateTime? EDTTime { get; set; }
        /// <summary>
        /// 排序 0:团队数DESC 1:团队数ASC 2:团天数DESC 3:团天数ASC
        /// </summary>
        public int PaiXu { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
    }
    #endregion

    #region 导游排班显示Model
    /// <summary>
    /// 导游排班显示 邵权江 2011-10-08
    /// </summary>
    [Serializable]
    public class MGuidePlanWork
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId
        {
            set;
            get;
        }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuideId
        {
            set;
            get;
        }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string Name
        {
            set;
            get;
        }
        /// <summary>
        /// 员工状态(在职/离职/兼职/挂靠)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.StaffStatus StaffStatus { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 团编号
        /// </summary>
        public string TourId
        {
            get;
            set;
        }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode
        {
            get;
            set;
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName
        {
            get;
            set;
        }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate
        {
            set;
            get;
        }
        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime RDate
        {
            set;
            get;
        }
        /// <summary>
        /// 出团天数
        /// </summary>
        public int TourDays
        {
            set;
            get;
        }
        /// <summary>
        /// 上团时间
        /// </summary>
        public DateTime OnTime
        {
            set;
            get;
        }
        /// <summary>
        /// 下团时间
        /// </summary>
        public DateTime NextTime
        {
            set;
            get;
        }
        /// <summary>
        /// 结算费用
        /// </summary>
        public decimal PlanCost
        {
            set;
            get;
        }
        /// <summary>
        /// 带团天数
        /// </summary>
        public int GuideDays
        {
            set;
            get;
        }
        /// <summary>
        /// 上团地点
        /// </summary>
        public string OnLocation
        {
            get;
            set;
        }
        /// <summary>
        /// 下团地点
        /// </summary>
        public string NextLocation
        {
            get;
            set;
        }
        /// <summary>
        /// 安排时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        /// <summary>
        /// 计调状态
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState PlanState { get; set; }
        /// <summary>
        /// 导游安排类型业务实体集合
        /// </summary>
        public IList<MGuidePlanWorkType> TypeList { get; set; }
        /// <summary>
        /// 团态标识：例一女、一两、一九
        /// </summary>
        public string TourMark { get; set; }
        /// <summary>
        /// 销售标识：黄橙绿青蓝紫
        /// </summary>
        public string SaleMark { get; set; }
        /// <summary>
        /// 抵达城市
        /// </summary>
        public string ArriveCity { get; set; }
        /// <summary>
        /// 抵达航班/时间
        /// </summary>
        public string ArriveCityFlight { get; set; }
        /// <summary>
        /// 离开城市
        /// </summary>
        public string LeaveCity { get; set; }
        /// <summary>
        /// 离开航班/时间
        /// </summary>
        public string LeaveCityFlight { get; set; }
    }
    #endregion

    #region 导游安排类型业务实体集合
    /// <summary>
    /// 导游安排类型业务实体集合
    /// </summary>
    public class MGuidePlanWorkType
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? OnTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? NextTime { get; set; }
        /// <summary>
        /// 类型：已安排，假期，停职
        /// </summary>
        public string Type { get; set; }
    }
    #endregion
}
