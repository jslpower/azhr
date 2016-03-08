using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SmsStructure
{
    /// <summary>
    /// 短信中心导入客户管理号码数据访问类接口
    /// </summary>
    public interface IDaoRuLxr
    {
        /// <summary>
        /// 获取导入客户管理号码集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SmsStructure.MLBDaoRuLxrInfo> GetLxrs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SmsStructure.MLBDaoRuLxrSearchInfo searchInfo);
    }
}
