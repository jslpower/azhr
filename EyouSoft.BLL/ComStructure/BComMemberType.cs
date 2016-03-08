using System.Collections.Generic;

using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 会员类型业务层
    /// 修改记录:
    /// 1、2012-03-22 曹胡生 创建
    /// </summary>
    public class BComMemberType : EyouSoft.BLL.BLLBase
    {
         private readonly EyouSoft.IDAL.ComStructure.IComMemberType dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComMemberType>();
        /// <summary>
        /// 构造函数
        /// </summary>
         public BComMemberType() { }
        /// <summary>
        /// 添加会员类型
        /// </summary>
        /// <param name="item">会员类型实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComMemberType item)
        {
            if (item != null)
            {
                return dal.Add(item);
            }
            return false;
        }

        /// <summary>
        /// 修改会员类型
        /// </summary>
        /// <param name="item">会员类型实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComMemberType item)
        {
            if (item != null)
            {
                return dal.Update(item);
            }
            return false;
        }

        /// <summary>
        /// 删除会员类型
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Ids">会员类型编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(string CompanyId, params int[] Ids)
        {
            if (!string.IsNullOrEmpty(CompanyId))
            {
                return dal.Delete(CompanyId, Ids);
            }
            return false;
        }

        /// <summary>
        /// 获取会员类型实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public MComMemberType GetModel(string CompanyId, int Id)
        {
            if (Id != 0 && !string.IsNullOrEmpty(CompanyId))
            {
                return dal.GetModel(CompanyId, Id);
            }
            return null;
        }

        /// <summary>
        /// 获取所有会员类型
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>会员类型集合</returns>
        public IList<MComMemberType> GetList(string CompanyId)
        {
            if (!string.IsNullOrEmpty(CompanyId))
            {
                return dal.GetList(CompanyId);
            }
            return null;
        }
    }
}
