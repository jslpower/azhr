using System.Collections.Generic;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 会员类型接口层
    /// 修改记录:
    /// 1、 2021-03-22 曹胡生 创建
    /// </summary>
    public interface IComMemberType
    {
        /// <summary>
        /// 添加会员类型
        /// </summary>
        /// <param name="item">会员类型实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Add(MComMemberType item);

        /// <summary>
        /// 修改会员类型
        /// </summary>
        /// <param name="item">会员类型实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Update(MComMemberType item);
       
        /// <summary>
        /// 删除会员类型
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Ids">会员类型编号</param>
        /// <returns>true：成功 false：失败</returns>
        bool Delete(string CompanyId, params int[] Ids);

        /// <summary>
        /// 获取会员类型实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        MComMemberType GetModel(string CompanyId, int Id);
       
        /// <summary>
        /// 获取所有会员类型
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>会员类型集合</returns>
        IList<MComMemberType> GetList(string CompanyId);
       
    }
}
