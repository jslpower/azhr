using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HTourStructure
{
    public class BTourOrder : EyouSoft.BLL.BLLBase
    {
        private readonly EyouSoft.IDAL.HTourStructure.ITourOrder dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HTourStructure.ITourOrder>();


        /// <summary>
        /// 添加团队游客
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns>0:失败 1：成功 -1:不能删除退团的游客</returns>
        public int AddOrUpdateTourTraveller(IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> list, string TourId)
        {
            if (string.IsNullOrEmpty(TourId)) return 0;
            int flg= dal.AddOrUpdateTourTraveller(list, TourId);

            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("添加团队游客,团队编号:{0}", TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }



        /// <summary>
        /// 获取游客信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> GetTourOrderTraveller(string TourId)
        {
            if (string.IsNullOrEmpty(TourId)) return null;

            return dal.GetTourOrderTraveller(TourId);
        }

    }
}
