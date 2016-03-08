using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.BLL.SysStructure
{
    /// <summary>
    /// 系统菜单业务逻辑类
    /// </summary>
    public class BSysMenu
    {
        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BSysMenu() { }
        #endregion

        #region public members
        /// <summary>
        /// 获取公司菜单信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public IList<MComMenu1Info> GetComMenus(string sysId)
        {
            if (string.IsNullOrEmpty(sysId)) return null;

            return EyouSoft.Security.Membership.UserProvider.GetComMenus(sysId);
        }
        #endregion
    }
}
