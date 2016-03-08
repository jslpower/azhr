using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.BQC
{
    /// <summary>
    /// 团队服务质检信息
    /// </summary>
    public class BTourServiceQC
    {
        private  EyouSoft.IDAL.IQC.ITourServiceQC dal=new EyouSoft.DAL.DQC.DTourServiceQC();

        /// <summary>
        /// 添加团队服务质检信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddTourServiceQC(EyouSoft.Model.QC.MTourServiceQC model){
            
            if(string.IsNullOrEmpty(model.CompanyId)
                ||string.IsNullOrEmpty(model.TourId)
                ||string.IsNullOrEmpty(model.OperatorId)
                ||model.OperatorDeptId==0) return false;

            model.QCID=Guid.NewGuid().ToString();

            return dal.AddTourServiceQC(model);
        }
        /// <summary>
        /// 修改团队服务质检信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTourServiceQC(EyouSoft.Model.QC.MTourServiceQC model)
        {
            
            if(string.IsNullOrEmpty(model.QCID)
                ||string.IsNullOrEmpty(model.CompanyId)
                ||string.IsNullOrEmpty(model.TourId)) return false;

            return dal.UpdateTourServiceQC(model);
        }

        /// <summary>
        /// 删除团队服务质检信息
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public bool DeleteTourServiceQC(string QCID)
        {
            if(string.IsNullOrEmpty(QCID)) return false;
            return dal.DeleteTourServiceQC(QCID);
            
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public EyouSoft.Model.QC.MTourServiceQC GetTourServiceQCModel(string QCID)
        {
            if(string.IsNullOrEmpty(QCID)) return null;
            return dal.GetTourServiceQCModel(QCID);
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="seach"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.QC.MTourServiceQC> GetTourServiceQCList(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.QC.MTourServiceQCSearch search)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetTourServiceQCList(companyId, pageSize, pageIndex, ref recordCount, search);
        }

    }
}
