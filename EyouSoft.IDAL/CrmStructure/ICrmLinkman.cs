using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CrmStructure
{
    /// <summary>
    /// 联系人
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface ICrmLinkman
    {

        /// <summary>
        /// 获得相关联的联系人
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="TypeId">类型编号</param>
        /// <param name="Type">类型</param>
        /// <returns></returns>
        IList<Model.CrmStructure.MCrmLinkman> GetCrmLinkManModel(string companyId, string TypeId, Model.EnumType.ComStructure.LxrType Type);

        /// <summary>
        /// 获得联系人Model
        /// </summary>
        /// <param name="linkManId">联系人编号</param>
        /// <returns></returns>
        Model.CrmStructure.MCrmLinkman GetLinkManModel(string linkManId);
    }
}
