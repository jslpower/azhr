using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.QC
{
    public class MCarTeamQC
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
        /// 车队名称
        /// </summary>
        public string CarTeamName { get; set; }
        /// <summary>
        /// 车号
        /// </summary>
        public string CarCode { get; set; }
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
        /// 司机服务
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.DriverService DriverService { get; set; }
        /// <summary>
        /// 认路情况
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.FindWay FindWay { get; set; }
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
        /// 附件集合
        /// </summary>
        public IList<EyouSoft.Model.ComStructure.MComAttach> FileList { get; set; }

    }


    /// <summary>
    /// 查询实体
    /// </summary>
    public class MCarTeamQCSearch
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 车队名称
        /// </summary>
        public string CarTeamName { get; set; }
    }
}
