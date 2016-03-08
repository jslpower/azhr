using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HTourStructure
{
    public interface ITourOrder
    {
        /// <summary>
        /// 添加团队游客
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns>0:失败 1：成功 -1:不能删除退团的游客</returns>
        int AddOrUpdateTourTraveller(IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> list, string TourId);



        /// <summary>
        /// 获取游客信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> GetTourOrderTraveller(string TourId);

    }
}
