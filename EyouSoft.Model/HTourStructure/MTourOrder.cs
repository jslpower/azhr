using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HTourStructure
{

    #region 订单游客信息
    /// <summary>
    /// 订单游客信息
    /// </summary>
    [Serializable]
    public class MTourOrderTraveller
    {
        /// <summary>
        /// 客户单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 游客编号
        /// </summary>
        public string TravellerId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 游客中文姓名
        /// </summary>
        public string CnName { get; set; }
        /// <summary>
        /// 游客英文姓名
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 游客类型（成人,儿童，其它）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.VisitorType? VisitorType { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CardType? CardType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 证件有效期
        /// </summary>
        public string CardValidDate { get; set; }

        /// <summary>
        /// 签证状态（办理中，通过，未通过）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.VisaStatus? VisaStatus { get; set; }

        /// <summary>
        /// 证件是否已办理（办理，未办理）
        /// </summary>
        public bool IsCardTransact { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender? Gender { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 出团通知
        /// </summary>
        public bool LNotice { get; set; }
        /// <summary>
        /// 回团通知
        /// </summary>
        public bool RNotice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 游客状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TravellerStatus TravellerStatus { get; set; }
        /// <summary>
        /// 退团金额
        /// </summary>
        public decimal RAmount { get; set; }
        /// <summary>
        /// 退团金额说明
        /// </summary>
        public string RAmountRemark { get; set; }
        /// <summary>
        /// 退团时间
        /// </summary>
        public DateTime? RTime { get; set; }
        /// <summary>
        /// 退团原因
        /// </summary>
        public string RRemark { get; set; }

    }
    #endregion
}
