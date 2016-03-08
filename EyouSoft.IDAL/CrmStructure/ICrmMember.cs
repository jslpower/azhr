using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CrmStructure
{
    /// <summary>
    /// 个人会员数据访问类接口
    /// </summary>
    public interface ICrmMember
    {
        /// <summary>
        /// 写入个人会员信息，返回1成功
        /// </summary>
        /// <param name="info">个人会员信息业务实体</param>
        /// <returns></returns>
        int Insert(EyouSoft.Model.CrmStructure.MCrmPersonalInfo info);
        /// <summary>
        /// 修改个人会员信息，返回1成功
        /// </summary>
        /// <param name="info">个人会员信息业务实体</param>
        /// <returns></returns>
        int Update(EyouSoft.Model.CrmStructure.MCrmPersonalInfo info);
        /// <summary>
        /// 获取个人会员信息业务实体
        /// </summary>
        /// <param name="crmId">个人会员编号</param>
        /// <returns></returns>
        EyouSoft.Model.CrmStructure.MCrmPersonalInfo GetInfo(string crmId);
        /// <summary>
        /// 获取个人会员积分明细集合
        /// </summary>
        /// <param name="crmId">个人会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CrmStructure.MJiFenInfo> GetJiFens(string crmId, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 设置个人会员积分
        /// </summary>
        /// <param name="info">积分信息业务实体</param>
        /// <returns></returns>
        bool SetJiFen(EyouSoft.Model.CrmStructure.MJiFenInfo info);
        /// <summary>
        /// 判断是否存在相同的个人会员身份证号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">个人会员编号</param>
        /// <param name="idCardCode">身份证号</param>
        /// <returns></returns>
        bool IsExistsIdCardCode(string companyId, string crmId, string idCardCode);
        /// <summary>
        /// 判断是否存在相同的个人会员
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">个人会员编号</param>
        /// <returns></returns>
        bool IsExists(string companyId, string crmId);
    }
}
