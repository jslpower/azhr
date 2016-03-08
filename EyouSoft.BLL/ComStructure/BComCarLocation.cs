using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.ComStructure
{
    public class BComCarLocation
    {
        private readonly EyouSoft.IDAL.ComStructure.IComCarLocation dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComCarLocation>();



        /// <summary>
        /// 查询该上车地点是否有计划预设
        /// </summary>
        /// <param name="CarLocationId"></param>
        /// <returns></returns>
        public bool IsExistsTourCarLocation(string CarLocationId)
        {
            if (string.IsNullOrEmpty(CarLocationId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.IsExistsTourCarLocation(CarLocationId);

        }


        /// <summary>
        /// 添加上车地点
        /// </summary>
        /// <param name="model">上车地点实体</param>
        /// <returns></returns>
        public bool AddCarLocation(EyouSoft.Model.ComStructure.MComCarLocation model)
        {
            model.CarLocationId = Guid.NewGuid().ToString();
            bool flg = dal.AddCarLocation(model);
            if (flg)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("添加上车地点,上车地点编号:{0}", model.CarLocationId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());

            }
            return flg;

        }


        /// <summary>
        /// 获取上车地点实体
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <returns>上车地点实体</returns>
        public EyouSoft.Model.ComStructure.MComCarLocation GetModel(string CarLocationId)
        {
            if (string.IsNullOrEmpty(CarLocationId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetModel(CarLocationId);
        }

        /// <summary>
        /// 删除上车地点
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <returns>1:成功，0：失败,2:该上车地点已被计划使用</returns>
        public int DelCarLocation(string CarLocationId)
        {
            if (string.IsNullOrEmpty(CarLocationId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            int flg = dal.DelCarLocation(CarLocationId);
            if (flg == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("删除上车地点,上车地点编号:{0}", CarLocationId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());

            }
            return flg;
        }

        /// <summary>
        /// 修改上车地点
        /// </summary>
        /// <param name="model">上车地点实体</param>
        /// <returns>1:成功，0：失败,2:该上车地点已被计划使用</returns>
        public int UpdateCarLocation(EyouSoft.Model.ComStructure.MComCarLocation model)
        {
            if (string.IsNullOrEmpty(model.CarLocationId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            int flg = dal.UpdateCarLocation(model);
            if (flg == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("修改上车地点,上车地点编号:{0}", model.CarLocationId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;

        }

        /// <summary>
        /// 设置上车地点的启用禁用
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public bool UpdateCarLocation(string CarLocationId, bool Status)
        {
            if (string.IsNullOrEmpty(CarLocationId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            int flg = dal.UpdateCarLocation(CarLocationId, Status);
            if (flg == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("{0}上车地点,上车地点编号:{1}", Status == true ? "启用" : "禁用", CarLocationId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }


        /// <summary>
        /// 获取上车地点列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        ///<param name="status">启用or 禁用</param>
        ///<param name="location">上车地点</param>
        /// <returns>结果集</returns>
        public IList<EyouSoft.Model.ComStructure.MComCarLocation> GetList(int pageIndex, int pageSize, ref int recordCount, string companyId, bool? status, string location)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetList(pageIndex, pageSize, ref recordCount, companyId, status, location);
        }



    }
}
