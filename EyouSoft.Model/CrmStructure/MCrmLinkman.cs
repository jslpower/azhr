using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.CrmStructure
{
   /// <summary>
	/// 联系人实体
	/// </summary>
	[Serializable]
    [Table(Name = "tbl_CrmLinkman")]
	public class MCrmLinkman
	{
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmLinkman() {}

		#region Model
		private string _linkmanid;
		private string _companyid;
		private string _name;
        private Model.EnumType.GovStructure.Gender _gender;
		private DateTime? _birthday;
		private string _department;
		private string _post;
		private string _mobilephone;
		private string _telephone;
		private string _email;
		private string _fax;
		private string _qq;
		private string _typeid;
		private string _userid;
        private string _msnskype;
		/// <summary>
		/// 联系人编号(客户单位部门编号)
		/// </summary>
        [Column(IsPrimaryKey = true, Name = "Id",DbType="char(36)")]
		public string Id
		{
			set{ _linkmanid=value;}
			get{return _linkmanid;}
		}
        /// <summary>
        /// 排序编号(自增Id)
        /// </summary>
        [Column(Name="SortId",DbType="int",IsDbGenerated=true)]
        public int SortId
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
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
        [Column(Name = "Name", DbType = "nvarchar(50)")]
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 性别
		/// </summary>
        [Column(Name = "Gender", DbType = "tinyint")]
        public Model.EnumType.GovStructure.Gender Gender
		{
			set{ _gender=value;}
			get{return _gender;}
		}
		/// <summary>
		/// 生日
		/// </summary>
        [Column(Name = "Birthday", DbType = "datetime")]
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 部门
		/// </summary>
        [Column(Name = "Department", DbType = "nvarchar(50)")]
		public string Department
		{
			set{ _department=value;}
			get{return _department;}
		}
		/// <summary>
		/// 职务
		/// </summary>
        [Column(Name = "Post", DbType = "nvarchar(50)")]
		public string Post
		{
			set{ _post=value;}
			get{return _post;}
		}
		/// <summary>
		/// 手机
		/// </summary>
        [Column(Name = "MobilePhone", DbType = "varchar(50)")]
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// 电话
		/// </summary>
        [Column(Name = "Telephone", DbType = "varchar(50)")]
		public string Telephone
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		/// <summary>
		/// E-Mail
		/// </summary>
        [Column(Name = "EMail", DbType = "varchar(50)")]
		public string EMail
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 传真
		/// </summary>
        [Column(Name = "Fax", DbType = "varchar(50)")]
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		/// <summary>
		/// QQ
		/// </summary>
        [Column(Name = "QQ", DbType = "varchar(50)")]
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// 类型编号
		/// </summary>
        [Column(Name = "TypeId", DbType = "char(36)")]
		public string TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 联系人类型
		/// </summary>
        [Column(Name = "Type", DbType = "tinyint")]
        public Model.EnumType.ComStructure.LxrType Type { get; set; }
		/// <summary>
		/// 用户编号
		/// </summary>
        [Column(Name = "UserId", DbType = "char(36)")]
		public string UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}

        /// <summary>
        /// 地址
        /// </summary>
        [Column(Name = "Address", DbType = "nvarchar(255)")]
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// MSN-SKYPE
        /// </summary>
        [Column(Name = "MSN", DbType = "nvarchar(255)")]
        public string MSN
        {
            get;
            set;
        }

        /// <summary>
        /// 是否生日提醒
        /// </summary>
        [Column(Name = "IsRemind", DbType = "char(1)")]
        public bool IsRemind
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Column(Name = "IssueTime", DbType = "datetime")]
        public DateTime IssueTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column(Name = "IsDeleted", DbType = "char(1)")]
        public bool IsDeleted
        {
            get;
            set;
        }
		#endregion Model

	}
}
