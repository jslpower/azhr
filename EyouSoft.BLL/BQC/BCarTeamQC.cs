using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.BQC
{
    /// <summary>
    /// 车队质检信息
    /// </summary>
    public class BCarTeamQC
    {
        private EyouSoft.IDAL.IQC.ICarTeamQC dal = new EyouSoft.DAL.DQC.DCarTeamQC();

        /// <summary>
        /// 添加车队质检
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCarTeamQC(EyouSoft.Model.QC.MCarTeamQC model)
        {
            if (string.IsNullOrEmpty(model.CompanyId)
               || string.IsNullOrEmpty(model.TourId)
               || string.IsNullOrEmpty(model.OperatorId)) return false;

            model.QCID = Guid.NewGuid().ToString();
            model.IssueTime = DateTime.Now;

            return dal.AddCarTeamQC(model);
        }

        /// <summary>
        /// 修改车队质检
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCarTeamQC(EyouSoft.Model.QC.MCarTeamQC model)
        {
            if (string.IsNullOrEmpty(model.QCID)
              || string.IsNullOrEmpty(model.CompanyId)
              || string.IsNullOrEmpty(model.TourId)
              || string.IsNullOrEmpty(model.OperatorId)) return false;

            return dal.UpdateCarTeamQC(model);
        }

        /// <summary>
        /// 删除车队质检
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public bool DeleteCarTeamQC(string QCID)
        {
            if (string.IsNullOrEmpty(QCID)) return false;

            return dal.DeleteCarTeamQC(QCID);
        }

        /// <summary>
        /// 获取车队质检实体
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public EyouSoft.Model.QC.MCarTeamQC GetCarTeamQCModel(string QCID)
        {
            if (string.IsNullOrEmpty(QCID)) return null;

            return dal.GetCarTeamQCModel(QCID);
        }

        /// <summary>
        /// 分页列表信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="reocrdCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.QC.MCarTeamQC> GetCarTeamQCList(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.QC.MCarTeamQCSearch search)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetCarTeamQCList(companyId, pageSize, pageIndex, ref recordCount, search);
        }
    }
}
