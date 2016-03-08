using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 线路区域接口
    /// 1、2013-06-06 PM 刘树超 创建
    /// </summary>
    public interface ISysRoom
    {
        /// <summary>
        /// 添加房型
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:成功、0:失败、2:该房型已存在</returns>
        int AddRoom(MSysRoom model);
        /// <summary>
        /// 修改房型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpdateRoom(MSysRoom model);
        /// <summary>
        /// 删除房型
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:成功、0:失败、2:该房型已被使用</returns>
        int DeleteRoom(string id, string companyID);

        /// <summary>
        /// 获取房型集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<MSysRoom> GetRoomList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysRoomSearchModel searchInfo);
        /// <summary>
        /// 获取房型信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<MSysRoom> GetRooms(string companyId);
        /// <summary>
        /// 获取房型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comID"></param>
        /// <returns></returns>
        MSysRoom getModel(string id,string comID);

    }
}
