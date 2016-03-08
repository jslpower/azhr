using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CrmStructure
{
    /// <summary>
    /// 客户关系业务逻辑类
    /// </summary>
    public class BCrm : BLLBase
    {
        readonly EyouSoft.IDAL.CrmStructure.ICrm dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CrmStructure.ICrm>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BCrm() { }
        #endregion

        #region public members
        /// <summary>
        /// 添加客户单位信息,返回1成功
        /// </summary>
        /// <param name="info">客户单位信息业务实体</param>
        /// <returns></returns>
        public int Insert(Model.CrmStructure.MCrm info)
        {
            if (info == null) return 0;
            if (string.IsNullOrEmpty(info.CompanyId)) return -1;
            //if (info.LinkManList == null || info.LinkManList.Count == 0) return -2;

            info.CrmId = Guid.NewGuid().ToString();

            if (info.BankList != null && info.BankList.Count > 0)
            {
                foreach (var item in info.BankList)
                {
                    item.BankId = Guid.NewGuid().ToString();
                }
            }

            foreach (var item in info.LinkManList)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.Insert(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("添加客户单位信息，客户单位编号：" + info.CrmId + "，单位名称：" + info.Name + "。");
                return 1;
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改客户单位,返回1成功
        /// </summary>
        /// <param name="info">客户单位信息业务实体</param>
        /// <returns></returns>
        public int Update(Model.CrmStructure.MCrm info)
        {
            if (info == null || string.IsNullOrEmpty(info.CrmId)) return 0;
            if (string.IsNullOrEmpty(info.CompanyId)) return -1;
            //if (info.LinkManList == null || info.LinkManList.Count == 0) return -2;

            if (info.BankList != null && info.BankList.Count > 0)
            {
                foreach (var item in info.BankList)
                {
                    if (!string.IsNullOrEmpty(item.BankId)) continue;
                    item.BankId = Guid.NewGuid().ToString();
                }
            }

            foreach (var item in info.LinkManList)
            {
                if (!string.IsNullOrEmpty(item.Id)) continue;
                item.Id = Guid.NewGuid().ToString();
            }

            int dalRetCode = dal.Update(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改客户单位信息，客户单位编号：" + info.CrmId + "，单位名称：" + info.Name + "。");
                return 1;
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除客户单位信息，返回1成功
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        public int Delete(string crmId, string operatorId)
        {
            if (string.IsNullOrEmpty(crmId) || string.IsNullOrEmpty(operatorId)) return 0;

            int dalRetCode = dal.Delete(crmId, operatorId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除客户单位信息，客户单位编号：" + crmId + "。");
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取客户单位信息业务实体
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrm GetInfo(string crmId)
        {
            if (string.IsNullOrEmpty(crmId)) return null;

            return dal.GetInfo(crmId);
        }

        /// <summary>
        /// 是否存在相同的客户单位名称
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmName">客户单位名称</param>
        /// <param name="crmId">客户单位编号</param>
        /// <returns></returns>
        public bool IsExistsCrmName(string companyId, string crmName, string crmId)
        {
            if (string.IsNullOrEmpty(crmName) || string.IsNullOrEmpty(companyId)) return true;

            return dal.IsExistsCrmName(companyId, crmName, crmId);
        }

        /// <summary>
        /// 设置客户单位用户账号状态
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="status">用户状态</param>
        /// <returns></returns>
        public bool SetCrmUserStatus(string companyId, string crmId, string userId, EyouSoft.Model.EnumType.ComStructure.UserStatus status)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(crmId) || string.IsNullOrEmpty(userId)) return false;

            if (dal.SetCrmUserStatus(companyId, crmId, userId, status))
            {
                SysStructure.BSysLogHandle.Insert("设置客户单位用户账号状态，用户编号：" + userId + "，状态：" + status + "。");

                return true;
            }

            return false;
        }

        /// <summary>
        /// 设置客户单位联系人账号信息，已经分配过账号的做密码修改操作，返回1成功
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatroId">操作人编号</param>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="lxrId">联系人编号</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        public int SetCrmUser(string companyId, string operatroId, string crmId, string lxrId, string username, EyouSoft.Model.ComStructure.MPasswordInfo pwd)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(crmId) || string.IsNullOrEmpty(lxrId) || string.IsNullOrEmpty(username)) return 0;
            if (pwd == null || string.IsNullOrEmpty(pwd.NoEncryptPassword)) return 0;

            if (dal.SetCrmUser(companyId, operatroId, crmId, lxrId, username, pwd) == 1)
            {
                SysStructure.BSysLogHandle.Insert("设置客户单位用户账号，联系人编号：" + lxrId + "，用户名：" + username + "。");
            }

            return 1;
        }

        /// <summary>
        /// 获得客户单位联系人列表(含账号信息)
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmUserInfo> GetCrmUsers(string crmId)
        {
            if (string.IsNullOrEmpty(crmId)) return null;

            return dal.GetCrmUsers(crmId);
        }

        /// <summary>
        /// 修改客户单位的打印配置
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <param name="printHead">打印头</param>
        /// <param name="printFoot">打印尾</param>
        /// <param name="printTemplates">打印模板</param>
        /// <param name="seal">公章</param>
        /// <returns></returns>
        public bool UpdatePrintSet(string crmId, string printHead, string printFoot, string printTemplates, string seal)
        {
            if (string.IsNullOrEmpty(crmId)) return false;

            return dal.UpdatePrintSet(crmId, printHead, printFoot, printTemplates, seal);
        }

        /// <summary>
        /// 获取客户单位信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="crmType">客户单位类型</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MLBCrmInfo> GetCrms(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.EnumType.CrmStructure.CrmType crmType, EyouSoft.Model.CrmStructure.MLBCrmSearchInfo searchInfo)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            if (crmType == EyouSoft.Model.EnumType.CrmStructure.CrmType.个人会员) return null;

            bool isOnlySelf = false;
            int[] depts = null;


            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.客户中心_分销商, out  isOnlySelf);


            var items = dal.GetCrms(companyId, LoginUserId, depts, pageSize, pageIndex, ref recordCount, crmType, searchInfo);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.SysStructure.BGeography();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CountryId, item.ProvinceId, item.CityId, item.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取客户单位信息集合(选用时使用)
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="crmType">客户单位类型</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CrmStructure.MLBCrmXuanYongInfo> GetCrmsXuanYong(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.EnumType.CrmStructure.CrmType? crmType, EyouSoft.Model.CrmStructure.MLBCrmSearchInfo searchInfo)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetCrmsXuanYong(companyId, null, null, pageSize, pageIndex, ref recordCount, crmType, searchInfo);
        }

        /// <summary>
        /// 获取个人会员信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CrmStructure.MLBCrmPersonalInfo> GetCrmsPersonal(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CrmStructure.MLBCrmPersonalSearchInfo searchInfo)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            //depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.客户管理_个人会员, out  isOnlySelf);

            return dal.GetCrmsPersonal(companyId, LoginUserId, depts, pageSize, pageIndex, ref recordCount, searchInfo);
        }

        /// <summary>
        /// 验证客户单位责任销售，返回真匹配，返回假不匹配
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="sellerId">销售员编号</param>
        /// <returns></returns>
        public bool YanZhengZeRenXiaoShou(string crmId, string sellerId)
        {
            if (string.IsNullOrEmpty(crmId) || string.IsNullOrEmpty(sellerId)) return false;

            return dal.YanZhengZeRenXiaoShou(crmId, sellerId);
        }
        #endregion
    }
}
