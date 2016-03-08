using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.Model.TourStructure
{


    #region  单项服务基础表
    /// <summary>
    /// 单项服务基础表
    /// </summary>
    public class MSingleService
    {
        /// <summary>
        /// 团队编号(附：添加时为guid，修改时将订单所属的TourId赋值)
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 客源单位编号
        /// </summary>
        public string BuyCompanyId { get; set; }


        /// <summary>
        ///客源单位联系人部门编号
        /// </summary>
        public string ContactDepartId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int Adults { get; set; }


        /// <summary>
        /// 服务类别(订房，订票，租车，景点，签证，游轮，导游，保险，其它)
        /// </summary>
        public string PlanProjectType { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 销售员部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { set; get; }

        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { set; get; }

        /// <summary>
        /// 操作人部门编号
        /// </summary>
        public int OperatorDeptId { set; get; }

        /// <summary>
        /// 合计收入
        /// </summary>
        public decimal TourIncome { get; set; }

        /// <summary>
        /// 合计支出
        /// </summary>
        public decimal TourPay { get; set; }

        /// <summary>
        /// 毛利
        /// </summary>
        public decimal TourProfit { get; set; }

        /// <summary>
        /// 绑定列表时的状态
        /// </summary>
        public TourStatus TourStatus { get; set; }

        /// <summary>
        /// 游客信息的附件
        /// </summary>
        public string TravellerFile { get; set; }

        /// <summary>
        /// 计调员(用于列表显示)
        /// </summary>
        public string Planers { get; set; }
        /// <summary>
        /// 委托日期
        /// </summary>
        public DateTime WeiTuoRiQi { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HeTongId { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string HeTongCode { get; set; }
    }
    #endregion


    #region 单项业务信息表
    /// <summary>
    /// 单项业务信息表
    /// </summary>
    public class MSingleServiceExtend : MSingleService
    {

        /// <summary>
        /// 游客信息
        /// </summary>
        public IList<MTourOrderTraveller> TourOrderTravellerList { get; set; }

        /// <summary>
        /// 游客要求(团队分项报价)
        /// </summary>
        public IList<MTourTeamPrice> TourTeamPriceList { get; set; }

        /// <summary>
        /// 供应商安排（计调信息）
        /// </summary>
        public IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> PlanBaseInfoList { get; set; }


        /// <summary>
        /// 指定计调员集合
        /// </summary>
        public IList<MTourPlaner> TourPlanersList { get; set; }


    }
    #endregion


    #region 单项业务查询的实体
    /// <summary>
    /// 单项业务查询的实体
    /// </summary>
    public class MSeachSingleService
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 订单编号 
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 客户单位
        /// </summary>
        public string BuyCompanyName { get; set; }

        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string BuyCompanyId { get; set; }

        /// <summary>
        /// 下单起始时间
        /// </summary>
        public DateTime? BeginLDate { get; set; }

        /// <summary>
        /// 下单截止时间
        /// </summary>
        public DateTime? EndLDate { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 单项业务的状态
        /// </summary>
        public TourStatus? TourStatus { get; set; }
        /// <summary>
        /// 委托起始时间
        /// </summary>
        public DateTime? SWeiTuoRiQi { get; set; }
        /// <summary>
        /// 委托截止时间
        /// </summary>
        public DateTime? EWeiTuoRiQi { get; set; }
    }
    #endregion



}
