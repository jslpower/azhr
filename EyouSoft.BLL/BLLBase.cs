using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 业务逻辑基类
    /// </summary>
    public class BLLBase
    {
        #region attributes
        /// <summary>
        /// 获取组织机构用户(同级及下级部门用户)信息集合
        /// </summary>
        protected IList<string> Users { get { return null; } }
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        protected MUserInfo LoginUserInfo { get { return GetLoginUserInfo(); } }
        /// <summary>
        /// 获取当前登录用户编号
        /// </summary>
        protected string LoginUserId { get { return LoginUserInfo!=null?this.LoginUserInfo.UserId:string.Empty; } }
        #endregion

        #region private members
        /// <summary>
        /// get login user info
        /// </summary>
        /// <returns></returns>
        private MUserInfo GetLoginUserInfo()
        {
            MUserInfo info = null;
            bool isLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out info);

            if (!isLogin) return null;

            return info;
        }

        /// <summary>
        /// 获取指定二级栏目栏目下所有权限信息集合
        /// </summary>
        /// <param name="menu2">二级栏目</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.SysStructure.MSysPrivsInfo> GetMenu2Privs(EyouSoft.Model.EnumType.PrivsStructure.Menu2 menu2)
        {
            var items = EyouSoft.Security.Membership.UserProvider.GetSysMenu2s();

            if (items != null && items.Count > 0)
            {
                foreach (var item in items) if (item.MenuId == (int)menu2) return item.Privs;
            }

            return null;
        }

        /// <summary>
        /// 按指定权限类别获取权限编号
        /// </summary>
        /// <param name="privs">权限集合</param>
        /// <param name="privsType">权限类别</param>
        /// <returns></returns>
        private int GetPrivsId(IList<EyouSoft.Model.SysStructure.MSysPrivsInfo> privs, EyouSoft.Model.EnumType.SysStructure.PrivsType privsType)
        {
            if (privs != null && privs.Count > 0)
            {
                foreach (var item in privs)
                {
                    if (item.PrivsType == privsType) return item.PrivsId;
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取指定部门的下级部门集合（含部门编号）
        /// </summary>
        /// <param name="depts">公司部门信息集合</param>
        /// <param name="deptId">当前所在部门编号</param>
        /// <returns></returns>
        private int[] GetDeptChildrens(List<EyouSoft.Model.ComStructure.MCacheDeptInfo> depts, int deptId)
        {
            if (depts == null || depts.Count == 0) return new int[] { -1 };

            foreach (var item in depts)
            {
                if (item.DeptId == deptId)
                {
                    return item.Depts.ToArray();
                }
            }

            return new int[] { -1 };
        }

        /// <summary>
        /// 获取指定部门所在的一级部门的下级部门集合（含部门编号）
        /// </summary>
        /// <param name="depts">公司部门信息集合</param>
        /// <param name="deptId">当前所在部门编号</param>
        /// <returns></returns>
        private int[] GetFirstDeptChildrens(List<EyouSoft.Model.ComStructure.MCacheDeptInfo> depts, int deptId)
        {
            if(depts==null||depts.Count==0) return new int[] { -1 };

            foreach (var item in depts)
            {
                if (item.DeptId == deptId)
                {
                    return GetDeptChildrens(depts, item.FirstId);
                }
            }

            return new int[] { -1 };
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取当前登录用户指定二级栏目的数据级浏览权限。返回可以浏览到的部门集合。return null时是所有部门。isOnlySelf==true时返回值为int[]{-1}，只能查看自己的数据。
        /// </summary>
        /// <param name="menu2">要获取数据级浏览权限的二级栏目</param>
        /// <param name="isOnlySelf">是否仅查看自己的数据</param>
        /// <returns>int[deptids]返回可以浏览到的部门集合，return null时是所有部门</returns>
        protected int[] GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2 menu2, out bool isOnlySelf)
        {
            isOnlySelf = true;

            MUserInfo info = EyouSoft.Security.Membership.UserProvider.GetUserInfo();
            if (info == null) return new int[] { -1 };

            var privs = GetMenu2Privs(menu2);
            var depts = EyouSoft.Security.Membership.UserProvider.GetDepts(info.CompanyId);

            if (depts == null || depts.Count == 0) return new int[] { -1 };

            //统计分析默认有内部浏览权限
            EyouSoft.Model.EnumType.PrivsStructure.Menu2[] tongJiFenXi ={
                                                                             EyouSoft.Model.EnumType.PrivsStructure.Menu2.统计分析_单团核算
                                                                             ,EyouSoft.Model.EnumType.PrivsStructure.Menu2.统计分析_导游业绩统计
                                                                             ,EyouSoft.Model.EnumType.PrivsStructure.Menu2.统计分析_利润统计
                                                                             ,EyouSoft.Model.EnumType.PrivsStructure.Menu2.统计分析_人天数统计
                                                                             ,EyouSoft.Model.EnumType.PrivsStructure.Menu2.统计分析_人数统计表
                                                                             ,EyouSoft.Model.EnumType.PrivsStructure.Menu2.统计分析_入境目录表
                                                                        };

            //数据级权限有优先级：查看全部、内部浏览、部门浏览、本部浏览从高到底。
            if (EyouSoft.Security.Membership.UserProvider.IsGrant(info.Privs, GetPrivsId(privs, EyouSoft.Model.EnumType.SysStructure.PrivsType.查看全部)))
            {
                isOnlySelf = false;
                return null;
            }
            else if (EyouSoft.Security.Membership.UserProvider.IsGrant(info.Privs, GetPrivsId(privs, EyouSoft.Model.EnumType.SysStructure.PrivsType.内部浏览)) || tongJiFenXi.Contains(menu2))
            {
                isOnlySelf = false;
                return GetFirstDeptChildrens(depts, info.DeptId);
            }
            else if (EyouSoft.Security.Membership.UserProvider.IsGrant(info.Privs, GetPrivsId(privs, EyouSoft.Model.EnumType.SysStructure.PrivsType.部门浏览)))
            {
                isOnlySelf = false;
                return GetDeptChildrens(depts, info.DeptId);
            }
            else if (EyouSoft.Security.Membership.UserProvider.IsGrant(info.Privs, GetPrivsId(privs, EyouSoft.Model.EnumType.SysStructure.PrivsType.本部浏览)))
            {
                isOnlySelf = false;
                return new int[] { info.DeptId };
            }

            return new int[] { -1 };
        }

        /// <summary>
        /// 获取指定部门的下级部门集合(含指定的部门编号)
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        protected int[] GetDepts(string companyId, int deptId)
        {
            var depts = EyouSoft.Security.Membership.UserProvider.GetDepts(companyId);
            if (depts == null || depts.Count == 0) return new int[] { -1 };

            return GetDeptChildrens(depts, deptId);
        }

        /// <summary>
        /// 分页参数验证
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">面索引</param>
        /// <returns></returns>
        protected bool ValidPaging(int pageSize, int pageIndex)
        {
            if (pageSize <= 0) return false;
            if (pageIndex < 1) return false;

            return true;
        }
        #endregion
    }
}
