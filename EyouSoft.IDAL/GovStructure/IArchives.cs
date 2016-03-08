using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 档案信息业务接口
    /// 邵权江 2011-09-05
    /// </summary>
    public interface IArchives
    {
        /// <summary>
        /// 判断身份证号是否存在
        /// </summary>
        /// <param name="IDNumber">身份证号</param>
        /// <param name="Id">档案Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        bool ExistsIDNumber(string IDNumber, string Id, string CompanyId);
        /// <summary>
        /// 判断档案编号是否存在
        /// </summary>
        /// <param name="FileNumber">档案编号</param>
        /// <param name="Id">档案Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        bool ExistsFileNumber(string FileNumber, string Id, string CompanyId);
        /// <summary>
        /// 添加档案信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddArchives(EyouSoft.Model.GovStructure.MGovFile model);
        /// <summary>
        /// 修改档案信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="IsUser">是否用户</param>
        /// <param name="IsGuide">是否导游</param>
        /// <returns></returns>
        int UpdateArchives(EyouSoft.Model.GovStructure.MGovFile model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, bool IsUser, bool IsGuide);
        /// <summary>
        /// 根据档案id获取档案实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovFile GetArchivesModel(string ID, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);
        /// <summary>
        /// 根据用户id获取档案实体
        /// </summary>
        /// <param name="UserId">用户UserId</param>
        /// <returns>true:成功，false:失败</returns>
        EyouSoft.Model.GovStructure.MGovFile GetArchivesModelByUserId(string UserId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);
        /// <summary>
        /// 获取档案列表
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GovStructure.MGovFile> GetSearchArchivesList(EyouSoft.Model.GovStructure.MSearchGovFile model, string CompanyId, int PageSize, int PageIndex, ref int RecordCount);
        /*/// <summary>
        /// 删除档案信息
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns>0或负值：失败，1成功，2档案正在使用</returns>
        int DeleteArchives(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] Ids);*/
        /// <summary>
        /// 删除档案信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="dangAnId">档案编号</param>
        /// <returns></returns>
        int DeleteArchives(string companyId, string dangAnId);
        /// <summary>
        /// 获取内部通讯录信息
        /// </summary>
        /// <param name="UserName">姓名</param>
        /// <param name="DepartIds">部门编号集合（如：1,2,3）</param>
        /// <param name="Department">部门</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="ReCordCount"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.GovStructure.MGovFile> GetSearchArchivesList(string UserName, string DepartIds, string Department, string CompanyId, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据部门编号判断该部门是否存在员工
        /// </summary>
        /// <param name="deptId">部门编号</param>
        /// <returns>True：存在 False：不存在</returns>
        //bool IsExistStaffByDetpId(int deptId);
    }
}
