using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.CrmStructure
{
    /// <summary>
    /// 团队回访
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_CrmVisit")]
    public class MCrmVisit
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmVisit() { }
        #region Model
        private string _visitid;
        private string _companyid;
        
        private string _tourid;
        
        private string _name;
        private string _telephone;
        
        
        
        private string _operatorid;
        private DateTime _issuetime;
        /// <summary>
        /// 回访编号
        /// </summary>
        [Column(Name = "VisitId", IsPrimaryKey = true, DbType = "char(36)")]
        public string VisitId
        {
            set { _visitid = value; }
            get { return _visitid; }
        }
        /// <summary>
        /// 系统公司ID
        /// </summary>
        [Column(Name = "CompanyId", DbType = "char(36)")]
        public string CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        
        /// <summary>
        /// 团队编号
        /// </summary>
        [Column(Name = "TourId", DbType = "char(36)")]
        public string TourId
        {
            set { _tourid = value; }
            get { return _tourid; }
        }
        /// <summary>
        /// 被访人身份
        /// </summary>
        [Column(Name = "Identity", DbType = "tinyint")]
        public Model.EnumType.CrmStructure.CrmIdentity? Identity
        {
            get;
            set;
        }
        /// <summary>
        /// 被访人
        /// </summary>
        [Column(Name = "Name", DbType = "nvarchar(50)")]
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 被访人电话
        /// </summary>
        [Column(Name = "Telephone", DbType = "varchar(50)")]
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        /// <summary>
        /// 回访类型
        /// </summary>
        [Column(Name = "ReturnType", DbType = "tinyint")]
        public Model.EnumType.CrmStructure.CrmReturnType? ReturnType
        {
            get;
            set;
        }
       
        /// <summary>
        /// 总评
        /// </summary>
        [Column(Name = "Total", DbType = "nvarchar(max)")]
        public string Total
        {
            get;
            set;
        }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column(Name = "OperatorId", DbType = "char(36)")]
        public string OperatorId
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column(Name = "IssueTime", DbType = "datetime")]
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }


        /// <summary>
        /// 团队均分
        /// </summary>
        [Column(Name = "QualityScore", DbType = "float(8,2)")]
        public float QualityScore
        {
            get;
            set;
        }

        /// <summary>
        /// 团队计调安排项
        /// </summary>
        public IList<Model.CrmStructure.MCrmVisitDetail> VisitDetailList
        {
            get;
            set;
        }

        
        #endregion Model

    }

    #region 团队回访列表页面Model
    /// <summary>
    /// 团队回访列表页面显示Model
    /// </summary>
    [Serializable]
    public class MVisitListModel
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId
        {
            get;
            set;
        }

        public int Id
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
        /// 线路编号
        /// </summary>
        public string RouteId
        {
            get;
            set;
        }

        /// <summary>
        /// 回团日期
        /// </summary>
        public DateTime? RDate
        {
            get;
            set;
        }

        /// <summary>
        /// 客户单位
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }

        /// <summary>
        /// 销售员名称
        /// </summary>
        public string SalesmanName
        {
            get;
            set;
        }

        /// <summary>
        /// 导游名称
        /// </summary>
        public string[] GuideName
        {
            get;
            set;
        }

        /// <summary>
        /// 计调员名称
        /// </summary>
        public string PlanerName
        {
            get;
            set;
        }

        /// <summary>
        /// 团队状态
        /// </summary>
        public Model.EnumType.TourStructure.TourStatus TourStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// 团队均分
        /// </summary>
        public float QualityScore
        {
            get;
            set;
        }

        /// <summary>
        /// 总评
        /// </summary>
        public string Total
        {
            get;
            set;
        }

        /// <summary>
        /// 回访类型
        /// </summary>
        public Model.EnumType.CrmStructure.CrmReturnType ReturnType
        {
            get;
            set;
        }
    }
    #endregion

    #region 每日汇总表Model
    /// <summary>
    /// 每日汇总表Model
    /// </summary>
    [Serializable]
    public class MDayTotalModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int Id
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
        /// 团队编号
        /// </summary>
        public string TourId
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
        /// 线路编号
        /// </summary>
        public string RouteId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售员
        /// </summary>
        public string Seller
        {
            get;
            set;
        }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer
        {
            get;
            set;
        }


        /// <summary>
        /// 回访类型
        /// </summary>
        public Model.EnumType.CrmStructure.CrmReturnType ReturnType
        {
            get;
            set;
        }

        /// <summary>
        /// 团队均分
        /// </summary>
        public float QualityScore
        {
            get;
            set;
        }

        /// <summary>
        /// 导游名称
        /// </summary>
        public string[] GuideName
        {
            get;
            set;
        }

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
    }
    #endregion
}
