using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 保险接口层
    /// 修改记录:
    /// 1、2012-04-23 曹胡生 创建
    /// </summary>
    public interface IComInsurance
    {
        #region IComInsurance 成员
        /// <summary>
        /// 添加保险
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true：成功 false：失败</returns>
        bool Add(MComInsurance item);

        /// <summary>
        /// 修改保险
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true：成功 false：失败</returns>
        bool Update(MComInsurance item);
        
        /// <summary>
        /// 删除保险
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        bool Delete(string ids, string CompanyId);

          /// <summary>
        /// 获取保险实体
        /// </summary>
        /// <param name="InsuranceId">保险编号</param>
        /// <returns></returns>
        MComInsurance GetModel(string InsuranceId);

        /// <summary>
        /// 获取所有保险
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<MComInsurance> GetList(string CompanyId);

        #endregion
    }
}
