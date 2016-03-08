using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 线路区域
    /// </summary>
    public interface IComArea
    {
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="item">线路区域实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(MComArea item);
        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="item">线路区域实体</param>
        /// <returns>返回线路区域编号</returns>
        bool Update(MComArea item);
        /// <summary>
        /// 批量删除线路区域
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaids">线路区域编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(string companyId, params int[] areaids);
        /// <summary>
        /// 根据线路区域编号获取线路区域实体
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域实体</returns>
        MComArea GetModel(int areaId, string companyId);

        /// <summary>
        /// 根据线路区域编号获取线路区域实体
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="typ">语种</param>
        /// <returns>线路区域实体</returns>
        MComArea GetModel(int areaId, string companyId, EyouSoft.Model.EnumType.SysStructure.LngType typ);

        /// <summary>
        /// 分页获取线路区域信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">搜索实体</param>
        /// <returns>线路区域集合</returns>
        IList<MComArea> GetList(int pageCurrent, int pageSize, ref int pageCount, string companyId, MComAreaSearch model);

        /// <summary>
        /// 根据公司编号获取线路区域信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="keyword">关键字</param>
        /// <returns>线路区域集合</returns>
        IList<MComArea> GetAreaByCID(string companyId,string keyword);
        
        /// <summary>
        /// 是否存在线路区域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        bool isEsistsArea(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType);

    }
}
