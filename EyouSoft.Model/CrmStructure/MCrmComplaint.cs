using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.CrmStructure
{
    /// <summary>
    /// 投诉管理
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_CrmComplaint")]
    public class MCrmComplaint
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmComplaint() { }
        #region Model

        private string _companyid;
        private string _name;
        private string _tel;


        private string _operatorid;
        private DateTime _issuetime;
        /// <summary>
        /// 投诉编号
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "ComplaintsId", DbType = "char(36)")]
        public string ComplaintsId
        {
            get;
            set;
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
        /// 投诉人
        /// </summary>
        [Column(Name = "ComplaintsName", DbType = "nvarchar(50)")]
        public string ComplaintsName
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 投诉人电话
        /// </summary>
        [Column(Name = "ComplaintsTel", DbType = "varchar(50)")]
        public string ComplaintsTel
        {
            set { _tel = value; }
            get { return _tel; }
        }

        /// <summary>
        /// 投诉类型
        /// </summary>
        [Column(Name = "ComplaintsType", DbType = "nvarchar(50)")]
        public string ComplaintsType
        {
            get;
            set;
        }

        /// <summary>
        /// 投诉人身份
        /// </summary>
        [Column(Name = "ComplaintsIdentity", DbType = "nvarchar(50)")]
        public string ComplaintsIdentity
        {
            get;
            set;
        }

        /// <summary>
        /// 投诉意见
        /// </summary>
        [Column(Name = "ComplaintsOpinion", DbType = "nvarchar(max)")]
        public string ComplaintsOpinion
        {
            get;
            set;
        }

        /// <summary>
        /// 投诉时间
        /// </summary>
        [Column(Name = "ComplaintsTime", DbType = "datetime")]
        public DateTime? ComplaintsTime
        {
            get;
            set;
        }

        /// <summary>
        /// 处理人
        /// </summary>
        [Column(Name = "HandleName", DbType = "nvarchar(50)")]
        public string HandleName
        {
            get;
            set;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        [Column(Name = "HandleOpinion", DbType = "nvarchar(max)")]
        public string HandleOpinion
        {
            get;
            set;
        }

        /// <summary>
        /// 是否处理
        /// </summary>
        [Column(Name = "IsHandle", DbType = "char(1)")]
        public bool IsHandle
        {
            get;
            set;
        }

        /// <summary>
        /// 处理时间
        /// </summary>
        [Column(Name = "HandleTime", DbType = "datetime")]
        public DateTime? HandleTime
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
        /// 团号
        /// </summary>
        [Column(Name = "TourCode", DbType = "varchar(50)")]
        public string TourCode
        {
            get;
            set;
        }

        /// <summary>
        /// 团队编号
        /// </summary>
        [Column(Name = "TourId", DbType = "char(36)")]
        public string TourId
        {
            get;
            set;
        }


        #endregion Model

    }
    #region 每日汇总表Model
    /// <summary>
    /// 每日汇总表Model
    /// </summary>
    [Serializable]
    public class MCrmDayTotalModel
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
        /// 总团数
        /// </summary>
        public int TotalTourCount
        {
            get;
            set;
        }

        /// <summary>
        /// 被投诉数
        /// </summary>
        public int ComplaintCount
        {
            get;
            set;
        }

        /// <summary>
        /// 已解决数
        /// </summary>
        public int SolveCout
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

    #region 投诉列表Model
    /// <summary>
    /// 投诉列表Model
    /// </summary>
    [Serializable]
    public class MComplaintsListModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 投诉编号
        /// </summary>
        public string ComplaintsId
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
        /// 团号
        /// </summary>
        public string TourCode
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
        /// 线路编号
        /// </summary>
        public string RouteName
        {
            get;
            set;
        }


        /// <summary>
        /// 投诉时间
        /// </summary>
        public DateTime? ComplaintsTime
        {
            get;
            set;
        }


        /// <summary>
        /// 投诉人
        /// </summary>
        public string ComplaintsName
        {
            get;
            set;
        }


        /// <summary>
        /// 投诉类型
        /// </summary>
        public string ComplaintsType
        {
            get;
            set;
        }

        /// <summary>
        /// 状态(已处理显示已处理,未处理显示未处理并且可以点击进行处理投诉)
        /// </summary>
        public bool IsHandle
        {
            get;
            set;
        }

        /// <summary>
        /// 处理人
        /// </summary>
        public string HandleName
        {
            get;
            set;
        }


        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandleTime
        {
            get;
            set;
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string HandleResult
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
    }
    #endregion

    #region  投诉列表搜索Model
    /// <summary>
    /// 投诉列表搜索Model
    /// </summary>
    [Serializable]
    public class MComplaintsSearchModel
    {
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
        /// 投诉日期(开始)
        /// </summary>
        public DateTime? StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 投诉日期(结束)
        /// </summary>
        public DateTime? EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 投诉人
        /// </summary>
        public string ComplaintsName
        {
            get;
            set;
        }

        /// <summary>
        /// 投诉类型
        /// </summary>
        public string ComplaintsType
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
    }
    #endregion
}
