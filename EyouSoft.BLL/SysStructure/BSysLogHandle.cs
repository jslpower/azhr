//汪奇志 2011-09-20
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.BLL.SysStructure
{
    /// <summary>
    /// 操作日志业务逻辑类
    /// </summary>
    public class BSysLogHandle:EyouSoft.BLL.BLLBase
    {
        private readonly EyouSoft.IDAL.SysStructure.ISysLogHandle dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SysStructure.ISysLogHandle>();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BSysLogHandle() { }
        #endregion

        #region public members
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="item">操作日志信息业务实体</param>
        /// <returns></returns>
        public bool Insert(EyouSoft.Model.SysStructure.MSysLogHandleInfo info)
        {
            if (info == null) return false;

            return dal.Insert(info);
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="content">操作内容</param>
        public static void Insert(string content)
        {
            MUserInfo uInfo = null;
            bool isLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out uInfo);

            if (!isLogin) return;

            MComLocationInfo locationInfo = EyouSoft.Security.Membership.UserProvider.GetLocation(uInfo.SysId);

            EyouSoft.Model.SysStructure.MSysLogHandleInfo info = new EyouSoft.Model.SysStructure.MSysLogHandleInfo();
            
            info.CompanyId = uInfo.CompanyId;
            info.Content = content;
            info.DeptId = uInfo.DeptId;
            info.DeptName = string.Empty;
            info.IssueTime = DateTime.Now;
            info.LogId = Guid.NewGuid().ToString();
            info.Menu1Id = locationInfo.Menu1Id;
            info.Menu2Id = locationInfo.Menu2Id;
            info.Operator = uInfo.Name;
            info.OperatorId = uInfo.UserId;
            info.RemoteIp = EyouSoft.Toolkit.Utils.GetRemoteIP();

            new BSysLogHandle().Insert(info);
        }

        /// <summary>
        /// 获取操作日志信息
        /// </summary>
        /// <param name="logId">日志编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SysStructure.MSysLogHandleInfo GetInfo(string logId)
        {
            return dal.GetInfo(logId);
        }

         /// <summary>
        /// 获取操作日志信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>        
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询信息</param>
        /// <returns></returns>
        public IList<MSysLogHandleInfo> GetLogs(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysLogHandleSearch search)
        {
            if (string.IsNullOrEmpty(companyId) || !ValidPaging(pageSize, pageIndex)) return null;

            bool isOnlySelf = false;
            int[] depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.系统设置_操作日志, out  isOnlySelf);

            return dal.GetLogs(companyId, LoginUserId, depts, pageSize, pageIndex, ref recordCount, search);
        }

        /// <summary>
        /// 获取登录日志信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<MSysLogLoginInfo> GetLoginLogs(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysLogLoginSearchInfo searchInfo)
        {
            if (string.IsNullOrEmpty(companyId) || !ValidPaging(pageSize, pageIndex)) return null;

            return dal.GetLoginLogs(companyId, pageSize, pageIndex, ref recordCount, searchInfo);
        }
        #endregion
    }
}
