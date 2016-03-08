using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//档案实体 2011-09-26 邵权江
namespace EyouSoft.Model.GovStructure
{
    #region 人事档案业务实体
    /// <summary>
    /// 人事档案业务实体
    /// </summary>
    public class MGovFile
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 档案编号
        /// </summary>
        public string FileNumber { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender Sex { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// 所属部门编号
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 员工照片
        /// </summary>
        public string StaffPhoto { get; set; }
        /// <summary>
        /// 类型(正式员工/试用期/学徒期)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.StaffType StaffType { get; set; }
        /// <summary>
        /// 员工状态(在职/离职/兼职/挂靠)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.StaffStatus StaffStatus { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime? EntryTime { get; set; }
        /// <summary>
        /// 工龄
        /// </summary>
        public int LengthService { get; set; }
        /// <summary>
        /// 婚姻状态(1是/0否)
        /// </summary>
        public bool IsMarriage { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string Birthplace { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticalFace { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 电话短号
        /// </summary>
        public string ContactShort { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 手机短号
        /// </summary>
        public string MobileShort { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string qq { get; set; }
        /// <summary>
        /// MSN
        /// </summary>
        public string Msn { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 当分配账号的话，关联用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 同步导游的话，关联导游ID
        /// </summary>
        public string GuideId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人部门编号
        /// </summary>
        public int OperDeptId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 附件实体集合
        /// </summary>
        public IList<Model.ComStructure.MComAttach> ComAttachList { get; set; }
        /// <summary>
        /// 所属部门业
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 合同到期时间
        /// </summary>
        public DateTime? MaturityTime { get; set; }
        /// <summary>
        /// 合同是否签订
        /// </summary>
        public bool IsSignContract { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 明文密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// MD5密码
        /// </summary>
        public string MD5Password { get; set; }
        /*/// <summary>
        /// 所属部门业务实体集合
        /// </summary>
        //public IList<MGovFileDept> GovFileDeptList { get; set; }*/
        /// <summary>
        /// 职务关系业务实体集合
        /// </summary>
        public IList<MGovFilePosition> GovFilePositionList { get; set; }
        /// <summary>
        /// 学历信息业务实体集合
        /// </summary>
        public IList<MGovFileEducation> GovFileEducationList { get; set; }
        /// <summary>
        /// 履历业务实体集合
        /// </summary>
        public IList<MGovFileCurriculum> GovFileCurriculumList { get; set; }
        /// <summary>
        /// 家庭关系业务实体集合
        /// </summary>
        public IList<MGovFilehome> GovFilehomeList { get; set; }
        /// <summary>
        /// 劳动合同表业务实体集合
        /// </summary>
        public IList<MGovFileContract> GovFileContractList { get; set; }
    }
    #endregion

    #region 所属部门业务实体
    /// <summary>
    /// 所属部门业务实体
    /// </summary>
    public class MGovFileDept
    {
        /// <summary>
        /// 档案ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
    }
    #endregion

    #region 职务关系业务实体
    /// <summary>
    /// 职务关系业务实体
    /// </summary>
    public class MGovFilePosition
    {
        /// <summary>
        /// 档案ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 职务ID
        /// </summary>
        public int PositionId { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        public string Title { get; set; }
    }
    #endregion

    #region 学历信息业务实体
    /// <summary>
    /// 学历信息业务实体
    /// </summary>
    public class MGovFileEducation
    {
        /// <summary>
        /// 档案ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        //public EyouSoft.Model.EnumType.GovStructure.Education Education { get; set; }
        public string Education { get; set; }
        /// <summary>
        /// 所学专业
        /// </summary>
        public string Profession { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string Graduated { get; set; }
        /// <summary>
        /// 状态 (毕业/在读)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Statue Statue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion

    #region 履历业务实体
    /// <summary>
    /// 履历业务实体
    /// </summary>
    public class MGovFileCurriculum
    {
        /// <summary>
        /// 档案ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string WorkUnit { get; set; }
        /// <summary>
        /// 从事职业
        /// </summary>
        public string Occupation { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion

    #region 家庭关系业务实体
    /// <summary>
    /// 家庭关系业务实体
    /// </summary>
    public class MGovFilehome
    {
        /// <summary>
        /// 档案ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 关系
        /// </summary>
        public string Relationship { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string WorkUnit { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
    }
    #endregion

    #region 劳动合同表业务实体
    /// <summary>
    /// 劳动合同表业务实体
    /// </summary>
    public class MGovFileContract
    {
        /// <summary>
        /// 档案ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNumber { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 签订时间
        /// </summary>
        public DateTime? SignedTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? MaturityTime { get; set; }
        /// <summary>
        /// 状态(未到期/到期位处理/到期已处理)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.FileContractStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion

    #region 列表查询业务实体
    /// <summary>
    /// 列表查询业务实体
    /// </summary>
    public class MSearchGovFile
    {
        /// <summary>
        /// 档案编号
        /// </summary>
        public string FileNumber { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别(男/女)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender? Sex { get; set; }
        /// <summary>
        /// 出生日期开始
        /// </summary>
        public DateTime? BirthDateBegin { get; set; }
        /// <summary>
        /// 出生日期结束
        /// </summary>
        public DateTime? BirthDateEnd { get; set; }
        /// <summary>
        /// 工龄
        /// </summary>
        public int? LengthService { get; set; }
        /// <summary>
        /// 职务Id
        /// </summary>
        public int? PositionId { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 类型(正式员工/试用期/学徒期)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.StaffType? StaffType { get; set; }
        /// <summary>
        /// 员工状态(在职/离职/兼职/挂靠)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.StaffStatus? StaffStatus { get; set; }
        /// <summary>
        /// 非离职(true：非离职，false：离职)
        /// </summary>
        public bool? NoLeft { get; set; }
        /// <summary>
        /// 婚姻状态(是/否)
        /// </summary>
        public bool? IsMarriage { get; set; }
        /// <summary>
        /// 是否分配账号(是：true；否：false)
        /// </summary>
        public bool? IsAccount { get; set; }
    }
    #endregion

    #region 内部通讯录实体
    /// <summary>
    /// 内部通讯录实体
    /// </summary>
    public class MInternalContacts
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 档案编号
        /// </summary>
        public string FileNumber { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string qq { get; set; }
        /// <summary>
        /// MSN
        /// </summary>
        public string Msn { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 所属部门实体集合
        /// </summary>
        public IList<Model.GovStructure.MGovMeetingStaff> MGovMeetingStaff { get; set; }
    }
    #endregion
}
