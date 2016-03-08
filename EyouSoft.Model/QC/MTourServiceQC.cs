using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.QC
{
    public class MTourServiceQC
    {
        /// <summary>
        /// 质检编号
        /// </summary>
        public string QCID { get; set; }
        /// <summary>
        /// 系统公司ID
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 导游1姓名
        /// </summary>
        public string GuideOneName { get; set; }
        /// <summary>
        /// 导游2姓名
        /// </summary>
        public string GuideTwoName { get; set; }
        /// <summary>
        /// 质检日期
        /// </summary>
        public DateTime QCTime { get; set; }
        /// <summary>
        /// 领队姓名
        /// </summary>
        public string LeaderName { get; set; }
        /// <summary>
        /// 领队电话
        /// </summary>
        public string LeaderTel { get; set; }
        /// <summary>
        /// 行程
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.SatisfactionType Trip { get; set; }
        /// <summary>
        /// 景点
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.SatisfactionType Scenic { get; set; }
        /// <summary>
        /// 各地酒店
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.SatisfactionType Hotel { get; set; }
        /// <summary>
        /// 餐饮
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.SatisfactionType Food { get; set; }
        /// <summary>
        /// 导游1
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.SatisfactionType GuideOne { get; set; }
        /// <summary>
        /// 导游2
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.SatisfactionType GuideTwo { get; set; }
        /// <summary>
        /// 行程备注
        /// </summary>
        public string TripRemark { get; set; }
        /// <summary>
        /// 景点备注
        /// </summary>
        public string ScenicRemark { get; set; }
        /// <summary>
        /// 各地酒店备注
        /// </summary>
        public string HotelRemark { get; set; }
        /// <summary>
        /// 餐饮备注
        /// </summary>
        public string FoodRemark { get; set; }
        /// <summary>
        /// 导游1备注
        /// </summary>
        public string GuideOneRemark { get; set; }
        /// <summary>
        /// 导游2备注
        /// </summary>
        public string GuideTwoRemark { get; set; }
        /// <summary>
        /// 建议
        /// </summary>
        public string Advice { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public IList<EyouSoft.Model.ComStructure.MComAttach> FileList { get; set; }
    }

    /// <summary>
    /// 查询实体
    /// </summary>
    public class MTourServiceQCSearch
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 导游名称
        /// </summary>
        public string GuideName { get; set; }
    }
}
