using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 团号生成规则业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BTourNoOptionCode
    {
        private readonly EyouSoft.IDAL.ComStructure.ITourNoOptionCode dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.ITourNoOptionCode>();

        public BTourNoOptionCode() { }

        /// <summary>
        /// 添加团号生成规则内容
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Add(IList<MTourNoOptionCode> list)
        {
            return dal.Add(list);
        }

        /// <summary>
        /// 修改团号生成规则内容
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(IList<MTourNoOptionCode> list)
        {
            return dal.Update(list);
        }

        /// <summary>
        /// 获取团号生成规则信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>团号生成规则实体</returns>
        public IList<MTourNoOptionCode> GetModel(string CompanyId)
        {
            return dal.GetModel(CompanyId);
        }
    }
}
