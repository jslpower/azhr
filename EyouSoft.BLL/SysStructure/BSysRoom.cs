using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.BLL.SysStructure
{
    public class BSysRoom
    {
        private readonly EyouSoft.IDAL.SysStructure.ISysRoom dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SysStructure.ISysRoom>();

        /// <summary>
        /// 添加房型
        /// </summary>
        /// <param name="RouteName"></param>
        /// <returns>1:成功、0:失败、2:该房型已存在</returns>
        public int AddRoom(MSysRoom model)
        {
            if (string.IsNullOrEmpty(model.CompanyId)) return 0;
            return dal.AddRoom(model);
        }
        /// <summary>
        /// 修改房型
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RouteName"></param>
        /// <returns></returns>
        public int UpdateRoom(MSysRoom model)
        {
            if (string.IsNullOrEmpty(model.RoomId) || string.IsNullOrEmpty(model.CompanyId)) return 0;
            return dal.UpdateRoom(model);
        }
        /// <summary>
        /// 删除房型
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1:成功、0:失败、2:该房型已被使用</returns>
        public int DeleteRoom(string id, string comPanyID)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(comPanyID)) return 0;
            return dal.DeleteRoom(id, comPanyID);
        }

        /// <summary>
        /// 获取房型集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<MSysRoom> GetRooms(string companyId, int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetRoomList(companyId, pageSize, pageIndex, ref recordCount, null);
        }

        /// <summary>
        /// 获取房型信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MSysRoom> GetRooms(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetRooms(companyId);
        }
        /// <summary>
        /// 获取房型
        /// </summary>
        /// <param name="id">房型编号</param>
        /// <param name="comID">公司编号</param>
        /// <returns></returns>
        public MSysRoom getRoom(string id,string comID)
        {
            return dal.getModel(id, comID);
        }
    }
}
