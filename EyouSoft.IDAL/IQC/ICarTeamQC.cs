using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.IQC
{
    public interface ICarTeamQC
    {

        /// <summary>
        /// 添加车队质检
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddCarTeamQC(EyouSoft.Model.QC.MCarTeamQC model);

        /// <summary>
        /// 修改车队质检
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCarTeamQC(EyouSoft.Model.QC.MCarTeamQC model);

        /// <summary>
        /// 删除车队质检
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        bool DeleteCarTeamQC(string QCID);

        /// <summary>
        /// 获取车队质检实体
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        EyouSoft.Model.QC.MCarTeamQC GetCarTeamQCModel(string QCID);

        /// <summary>
        /// 分页列表信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="reocrdCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.QC.MCarTeamQC> GetCarTeamQCList(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.QC.MCarTeamQCSearch search);


    }
}
