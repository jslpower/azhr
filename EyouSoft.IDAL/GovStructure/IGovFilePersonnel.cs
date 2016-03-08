using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 离职申请业务接口
    /// 邵权江 2011-09-06
    /// </summary>
    public interface IGovFilePersonnel 
    {
        /// <summary>
        /// 添加离职信息/人事变动信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddGovFilePersonnel(EyouSoft.Model.GovStructure.MGovFilePersonnel model);
        /// <summary>
        /// 修改离职申请信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateGovFilePersonnel(EyouSoft.Model.GovStructure.MGovFilePersonnel model);
        /// <summary>
        /// 根据 离职ID 获取离职申请实体信息
        /// </summary>
        /// <param name="ID">离职ID</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovFilePersonnel GetGovFilePersonnelModel(string ID);
        /// <summary>
        /// 根据 档案ID 获取离职申请实体信息
        /// </summary>
        /// <param name="ID">档案ID</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovFilePersonnel GetGovFilePersonnelModelByFileId(string ID);
        /// <summary>
        /// 修改离职审批信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddGovFilePersonnel(EyouSoft.Model.GovStructure.MGovPersonnelApprove model);
        /// <summary>
        /// 更新离职状态
        /// </summary>
        /// <param name="DepartureTime">离职时间</param>
        /// <param name="Id">人事编号</param>
        /// <returns></returns>
        bool UpdateIsLeft(DateTime DepartureTime, string Id);
        /// <summary>
        /// 删除离职申请信息
        /// </summary>
        /// <param name="Ids">编号</param>
        /// <returns></returns>
        bool DeleteGovFilePersonnel(params string[] Ids);
        /// <summary>
        /// 获取离职信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Name">员工姓名</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GovStructure.MGovFilePersonnel> GetGovFilePersonnelList(string CompanyId, string Name, int PageSize, int PageIndex, ref int RecordCount);
    }
}
