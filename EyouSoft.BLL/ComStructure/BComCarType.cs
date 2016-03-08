using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 车型业务层
    /// 创建者：李晓欢
    /// 创建时间：2012/8/14
    /// </summary>
    public class BComCarType
    {
        private readonly EyouSoft.IDAL.ComStructure.IComCarType dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComCarType>();

        /// <summary>
        /// 查询该车型是否有计划预设
        /// </summary>
        /// <param name="CarTypeId"></param>
        /// <returns></returns>
        public bool IsExistsTourCarType(string CarTypeId)
        {

            if (string.IsNullOrEmpty(CarTypeId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.IsExistsTourCarType(CarTypeId);
        }


        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="model">车型实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddComCarType(MComCarType model)
        {
            model.CarTypeId = Guid.NewGuid().ToString();
            bool flg = dal.AddComCarType(model);
            if (flg)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("添加车型,车型编号:{0}", model.CarTypeId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }


        /// <summary>
        /// 修改车型
        /// </summary>
        /// <param name="model">车型实体</param>
        /// <returns>0:失败 1:成功 2:计划有使用</returns>
        public int UpdateComCarType(MComCarType model)
        {
            if (string.IsNullOrEmpty(model.CarTypeId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            int flg = dal.UpdateComCarType(model);
            if (flg == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("修改车型,车型编号:{0}", model.CarTypeId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());

            }
            return flg;
        }


        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="CarTypeId">车型编号</param>
        /// <returns>0:失败 1:成功 2:计划有使用</returns>
        public int DelComCarType(string CarTypeId)
        {
            if (string.IsNullOrEmpty(CarTypeId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            int flg = dal.DelComCarType(CarTypeId);
            if (flg == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("删除车型,车型编号:{0}", CarTypeId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 获取系统车型
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MComCarType> GetList(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetList(companyId);
        }

        /// <summary>
        /// 分页获取车型
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>车型集合</returns>
        public IList<EyouSoft.Model.ComStructure.MComCarType> GetList(int pageIndex, int pageSize, ref int recordCount, string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetList(pageIndex, pageSize, ref recordCount, companyId);
        }
        /// <summary>
        /// 获取上车地点实体
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <returns>上车地点实体</returns>
        public EyouSoft.Model.ComStructure.MComCarType GetModel(string CarTypeId)
        {
            if (string.IsNullOrEmpty(CarTypeId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetModel(CarTypeId);
        }
    }
}
