using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HGysStructure
{
    /// <summary>
    /// 供应商预存款相关数据访问类接口
    /// </summary>
    public interface IYuCunKuan
    {
        /// <summary>
        /// 新增预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Insert(EyouSoft.Model.HGysStructure.MYuCunKuanInfo info);
        /// <summary>
        /// 更新预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Update(EyouSoft.Model.HGysStructure.MYuCunKuanInfo info);
        /// <summary>
        /// 删除预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="yuCunId">预存款编号</param>
        /// <returns></returns>
        int Delete(string companyId, string gysId, string yuCunId);
        /// <summary>
        /// 获取预存款信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MYuCunKuanInfo> GetYuCunKuans(string gysId);
        /// <summary>
        /// 重置供应商预存款余额，返回1成功，其它失败
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        int ResetYuCunKuanYuE(string gysId);
    }
}
