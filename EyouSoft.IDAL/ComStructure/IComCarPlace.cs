using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 公司上车地点加价实体
    /// </summary>
    public interface IComCarPlace
    {
        /// <summary>
        /// 添加上车地点
        /// </summary>
        /// <param name="item">上传地点实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(MComCarPlace item);
        /// <summary>
        /// 修改上车地点
        /// </summary>
        /// <param name="item">上传地点实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(MComCarPlace item);
        /// <summary>
        /// 获取加价金额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="place">上车地点</param>
        /// <returns>加价金额</returns>
        decimal GetAmountByPlace(string companyId, string place);
        /// <summary>
        /// 获取所有上车地点信息
        /// </summary>
        /// <returns>上车地点集合</returns>
        IList<MComCarPlace> GetList(string companyId);
        /// <summary>
        /// 分页获取上车地点列表
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns>上车地点集合</returns>
        IList<MComCarPlace> GetList(int pageCurrent, int pageSize, ref int pageCount,string companyId);
    }
}
