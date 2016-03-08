using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.PlanStructure;

namespace EyouSoft.BLL.ComStructure
{
    /*/// <summary>
    /// 游轮线路业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/26
    /// </summary>
    public class BComShipRoute
    {
        private readonly EyouSoft.IDAL.ComStructure.IComShip dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComShip>();

        #region

        /// <summary>
        /// 添加游轮线路
        /// </summary>
        /// <param name="item">游轮线路实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(MComShip item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加游轮线路,线路名称为:{0}", item.Name));
                }
            }
            return result;
        }
        /// <summary>
        /// 修改游轮线路
        /// </summary>
        /// <param name="item">游轮线路实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComShip item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改游轮线路,编号为:{0}", item.Id));
                }
            }
            return result;
        }
        /// <summary>
        /// 游轮线路列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">游轮类型</param>
        /// <returns>游轮线路集合</returns>
        public IList<MComShip> GetList(string companyId,PlanShipType? type)
        {
            IList<MComShip> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(companyId, type);
            }
            return list;
        }
        #endregion
    }

    public class BComShipOwnCost
    {
        private readonly EyouSoft.IDAL.ComStructure.IComShip dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComShip>();
        #region 
        /// <summary>
        /// 添加游轮自费项目
        /// </summary>
        /// <param name="item">游轮自费项目实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(MComShip item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加游轮自费项目,项目名称为:{0}", item.Name));
                }
            }
            return result;
        }
        /// <summary>
        /// 修改游轮自费项目
        /// </summary>
        /// <param name="item">游轮自费项目实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComShip item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改游轮自费项目,编号为:{0}", item.Id));
                }
            }
            return result;
        }
        /// <summary>
        /// 游轮自费项目列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">游轮类型</param>
        /// <returns>游轮自费项目集合</returns>
        public IList<MComShip> GetList(string companyId,PlanShipType? type)
        {
            IList<MComShip> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(companyId,type);
            }
            return list;
        }

        #endregion
    }*/
}
