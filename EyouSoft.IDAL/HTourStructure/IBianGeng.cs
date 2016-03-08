using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TourStructure
{
    public interface IBianGeng
    {

        bool InsertBianGeng(EyouSoft.Model.TourStructure.MBianGeng model);

        IList<EyouSoft.Model.TourStructure.MBianGeng> GetBianGengList(string bianId, EyouSoft.Model.EnumType.TourStructure.BianType? bianType);

        /// <summary>
        /// 获取第一次变更信息
        /// </summary>
        /// <param name="bianId"></param>
        /// <param name="bianType"></param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MBianGeng GetFirstBianGeng(
            string bianId, EyouSoft.Model.EnumType.TourStructure.BianType? bianType);

    }
}
