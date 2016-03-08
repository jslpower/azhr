using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 用户业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComUser
    {
        private readonly EyouSoft.IDAL.ComStructure.IComUser dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComUser>();
        /// <summary>
        /// 附件类型
        /// </summary>
        EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.ComStructure.AttachItemType), "人事档案");

        public BComUser() { }

        #region
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="item">用户信息实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComUser item)
        {
            bool result = false;
            if (item != null)
            {
                item.UserId = Guid.NewGuid().ToString();

                if (item.ComAttachList != null && item.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < item.ComAttachList.Count; i++)
                    {
                        item.ComAttachList[i].ItemId = item.UserId;
                        item.ComAttachList[i].ItemType = ItemType;
                    }
                }

                item.MD5Password = new EyouSoft.Toolkit.DataProtection.HashCrypto().MD5Encrypt(item.Password);
                result = dal.Add(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加用户,编号为:{0}", item.UserId));
                }
            }
            return result;
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="item">用户信息实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComUser item)
        {
            bool result = false;
            if (item != null)
            {
                item.MD5Password = new EyouSoft.Toolkit.DataProtection.HashCrypto().MD5Encrypt(item.Password);

                if (item.ComAttachList != null && item.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < item.ComAttachList.Count; i++)
                    {
                        item.ComAttachList[i].ItemId = item.UserId;
                        item.ComAttachList[i].ItemType = ItemType;
                    }
                }

                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改用户,编号为:{0}", item.UserId));

                    string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComUser, item.CompanyId, item.UserId);
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);
                }
            }
            return result;
        }

        /*/// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户编号</param> 
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool Delete( string ids,string CompanyId)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(ids))
            {
                result = dal.Delete(ids,CompanyId);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("删除用户,编号为:{0}", ids));
                }
            }
            return result;
        }*/

        /// <summary>
        /// 删除用户，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string userId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(userId)) return 0;

            int dalRetCode = dal.Delete(ItemType, userId);

            if (dalRetCode == 1)
            {
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert("删除用户信息，用户编号：" + userId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="ids">用户编号(以逗号分隔)</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="UserStatus">用户状态</param>
        /// <returns></returns>
        public bool SetUserStatus(string ids, string CompanyId, EyouSoft.Model.EnumType.ComStructure.UserStatus UserStatus)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(ids))
            {
                result = dal.SetUserStatus(ids, CompanyId, UserStatus);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("设置用户状态,状态为{0},编号为:{1}", UserStatus, ids));
                }
            }
            return result;
        }
        /// <summary>
        /// 修改用户权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="roleId">角色编号</param>
        /// <param name="privs">权限集合</param>
        /// <returns>true：成功 false：失败</returns>
        public bool UpdatePrivs(string userId, string companyId, int? roleId, string privs)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId))
            {
                result = dal.UpdatePrivs(userId, companyId, roleId, privs);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改用户权限,编号为:{0}", userId));
                }
            }
            return result;
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns>true：存在 false：不存在</returns>
        public bool IsExistsUserName(string userName, string companyId, string userId)
        {
            bool result = true;
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(companyId))
            {
                result = dal.IsExistsUserName(userName, companyId, userId);
            }
            return result;
        }

        /// <summary>
        /// 获取用户实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>用户实体</returns>
        public MComUser GetModel(string userId, string companyId)
        {
            MComUser item = null;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(userId, companyId, ItemType);
            }
            return item;
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>用户权限</returns>
        public string GetPrivs(string userId, string companyId)
        {
            string privs = string.Empty;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId))
            {
                privs = dal.GetPrivs(userId, companyId);
            }
            return privs;
        }
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>用户信息集合</returns>
        public IList<MComUser> GetList(int pageCurrent, int pageSize, ref int pageCount,
            string companyId, MComUserSearch search)
        {
            IList<MComUser> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                pageCurrent = pageCurrent < 1 ? 1 : pageCurrent;
                list = dal.GetList(pageCurrent, pageSize, ref pageCount, companyId, search);
            }
            return list;
        }

        /// <summary>
        /// 个人密码修改
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="userNm">用户名</param>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="NewPwd">新密码</param>
        /// <returns></returns>
        public bool PwdModify(string UserId, string userNm, string OldPwd, string NewPwd)
        {
            string MD5Pwd = new EyouSoft.Toolkit.DataProtection.HashCrypto().MD5Encrypt(NewPwd);
            return dal.PwdModify(UserId,userNm, OldPwd, NewPwd, MD5Pwd);
        }

        /// <summary>
        /// 获取系统用户数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetYongHuShuLiang(string companyId)
        {
            return dal.GetYongHuShuLiang(companyId);
        }

        #endregion
    }
}
