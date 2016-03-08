using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CrmStructure
{
    /// <summary>
    /// 联系人
    /// 创建者:钱琦
    /// 时间:2011-10-1
    /// </summary>
    public class BCrmLinkMan : EyouSoft.BLL.BLLBase
    {
        #region dal对象
        readonly EyouSoft.IDAL.CrmStructure.ICrmLinkman linkManDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CrmStructure.ICrmLinkman>();
         #endregion

        #region 构造函数

        #endregion


        /// <summary>
        /// 获得相关联的联系人
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="TypeId">类型编号</param>
        /// <param name="Type">类型</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmLinkman> GetCrmLinkManModel(string companyId, string TypeId, Model.EnumType.ComStructure.LxrType Type)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(TypeId))
                return null;
            return linkManDal.GetCrmLinkManModel(companyId, TypeId, Type);
        }



        /// <summary>
        /// 获得联系人Model
        /// </summary>
        /// <param name="linkManId">联系人编号</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrmLinkman GetLinkManModel(string linkManId)
        {
            if (string.IsNullOrEmpty(linkManId))
                return null;
            return linkManDal.GetLinkManModel(linkManId);



        }
    }
}
