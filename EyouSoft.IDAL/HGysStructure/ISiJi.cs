using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HGysStructure
{
    /// <summary>
    /// 领队、司机相关数据访问类接口
    /// </summary>
    public interface ISiJi
    {
        /// <summary>
        /// 新增领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Insert(EyouSoft.Model.HGysStructure.MSiJiInfo info);
        /// <summary>
        /// 更新领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Update(EyouSoft.Model.HGysStructure.MSiJiInfo info);
        /// <summary>
        /// 删除领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">领队、司机编号</param>
        /// <returns></returns>
        int Delete(string companyId, string gysId);
        /// <summary>
        /// 获取领队、司机信息业务实体
        /// </summary>
        /// <param name="gysId">领队、司机编号</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MSiJiInfo GetInfo(string gysId);
        /// <summary>
        /// 获取领队、司机信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MSiJiInfo> GetSiJis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MSiJiChaXunInfo chaXun);
    }
}
