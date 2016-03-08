using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 团号生成接口
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public interface ITourNoOptionCode
    {
        /// <summary>
        /// 添加团号生成规则内容
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool Add(IList<MTourNoOptionCode> list);
       
        /// <summary>
        /// 修改团号生成规则内容
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Update(IList<MTourNoOptionCode> list);

        /// <summary>
        /// 获取团号生成规则信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>团号生成规则实体</returns>
        IList<MTourNoOptionCode> GetModel(string CompanyId);
    }
}
