using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.IQC
{
    public interface ITourServiceQC
    {


        /// <summary>
        /// 添加团队服务质检信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddTourServiceQC(EyouSoft.Model.QC.MTourServiceQC model);
        /// <summary>
        /// 修改团队服务质检信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateTourServiceQC(EyouSoft.Model.QC.MTourServiceQC model);

        /// <summary>
        /// 删除团队服务质检信息
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        bool DeleteTourServiceQC(string QCID);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        EyouSoft.Model.QC.MTourServiceQC GetTourServiceQCModel(string QCID);

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="seach"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.QC.MTourServiceQC> GetTourServiceQCList(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.QC.MTourServiceQCSearch search);


    }
}
