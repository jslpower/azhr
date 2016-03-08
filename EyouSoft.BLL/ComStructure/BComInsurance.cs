using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 保险业务层
    /// 修改记录:
    /// 1、2012-04-23 曹胡生 创建
    /// </summary>
    public class BComInsurance
    {
         private readonly EyouSoft.IDAL.ComStructure.IComInsurance dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComInsurance>();
       
         public BComInsurance() { }

         #region BComInsurance 成员
         /// <summary>
        /// 添加保险
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComInsurance item)
        {
            return dal.Add(item);
        }

        /// <summary>
        /// 修改保险
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComInsurance item)
        {
            return dal.Update(item);
        }

        /// <summary>
        /// 删除保险
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(string ids, string CompanyId)
        {
            return dal.Delete(ids, CompanyId);
        }

          /// <summary>
        /// 获取保险实体
        /// </summary>
        /// <param name="InsuranceId">保险编号</param>
        /// <returns></returns>
        public MComInsurance GetModel(string InsuranceId)
        {
            return dal.GetModel(InsuranceId);
        }

        /// <summary>
        /// 获取所有保险
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<MComInsurance> GetList(string CompanyId)
        {
            return dal.GetList(CompanyId);
        }

        #endregion
    }
}
