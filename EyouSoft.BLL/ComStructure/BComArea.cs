using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 线路区域业务层
    /// </summary>
    public class BComArea
    {
        private readonly EyouSoft.IDAL.ComStructure.IComArea dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComArea>();

        public BComArea() { }
        #region
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="item">线路区域实体</param>
        /// <returns>true 成功 false 失败</returns>
        public bool Add(MComArea item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("新增线路区域,区域名称为:{0}", item.AreaName));
                }
            }

            return result;
        }
        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="item">线路区域实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(MComArea item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改线路区域,区域编号为:{0}", item.AreaId));
                }
            }

            return result;
        }

        /// <summary>
        /// 批量删除线路区域
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaIds">线路区域编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(string companyId, params int[] areaIds)
        {
            if (string.IsNullOrEmpty(companyId) || areaIds == null || areaIds.Length == 0) return false;

            bool result = dal.Delete(companyId, areaIds);

            if (result)
            {
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("删除线路区域,区域编号为:{0}", EyouSoft.Toolkit.Utils.GetSqlIdStrByArray(areaIds)));
            }

            return result;
        }

        /// <summary>
        /// 根据线路区域编号获取线路区域实体
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域实体</returns>
        public MComArea GetModel(int areaId, string companyId)
        {
            MComArea item = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(areaId, companyId);
            }

            return item;
        }

        /// <summary>
        /// 根据线路区域编号获取线路区域实体
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="typ">语种</param>
        /// <returns>线路区域实体</returns>
        public MComArea GetModel(int areaId, string companyId,EyouSoft.Model.EnumType.SysStructure.LngType typ)
        {
            MComArea item = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(areaId, companyId, typ);
            }

            return item;
        }

        /// <summary>
        /// 分页获取线路区域信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域集合</returns>
        public IList<MComArea> GetList(int pageCurrent, int pageSize, ref int pageCount, string companyId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetList(pageCurrent, pageSize, ref pageCount, companyId, null);
            }
            return null;
        }

        /// <summary>
        /// 分页获取线路区域信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">搜索实体</param>
        /// <returns>线路区域集合</returns>
        public IList<MComArea> GetList(int pageCurrent, int pageSize, ref int pageCount, string companyId, MComAreaSearch model)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetList(pageCurrent, pageSize, ref pageCount, companyId, model);
            }
            return null;
        }
        /// <summary>
        /// 根据公司编号获取线路区域信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域集合</returns>
        public IList<MComArea> GetAreaByCID(string companyId)
        {
            IList<MComArea> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetAreaByCID(companyId,null);
            }
            return list;
        }

        /// <summary>
        /// 根据公司编号获取线路区域信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域集合</returns>
        public IList<MComArea> GetAreaByCID(string companyId,string keyword)
        {
            IList<MComArea> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetAreaByCID(companyId, keyword);
            }
            return list;
        }

        /// <summary>
        /// 是否存在线路区域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        public bool isEsistsArea(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            return dal.isEsistsArea(id, lngType);
        }
        #endregion
    }
}
