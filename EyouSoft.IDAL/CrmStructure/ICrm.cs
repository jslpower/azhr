using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CrmStructure
{
    /// <summary>
    /// 客户关系数据访问类接口
    /// </summary>
    public interface ICrm
    {
        /// <summary>
        /// 添加客户单位,返回1成功
        /// </summary>
        /// <param name="info">客户单位信息业务实体</param>
        /// <returns></returns>
        int Insert(Model.CrmStructure.MCrm info);
        /// <summary>
        /// 修改客户单位,返回1成功
        /// </summary>
        /// <param name="info">客户单位信息业务实体</param>
        /// <returns></returns>
        int Update(Model.CrmStructure.MCrm info);
        /// <summary>
        /// 删除客户单位信息，返回1成功
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        int Delete(string crmId, string operatorId);
        /// <summary>
        /// 是否存在相同的客户单位名称
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmName">客户单位名称</param>
        /// <param name="crmId">客户单位编号</param>
        /// <returns></returns>
        bool IsExistsCrmName(string companyId, string crmName, string crmId);
        /// <summary>
        /// 获取客户单位信息业务实体
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <returns></returns>
        Model.CrmStructure.MCrm GetInfo(string crmId);
        /// <summary>
        /// 设置客户单位用户账号状态
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="status">用户状态</param>
        /// <returns></returns>
        bool SetCrmUserStatus(string companyId, string crmId, string userId, EyouSoft.Model.EnumType.ComStructure.UserStatus status);
        /// <summary>
        /// 设置客户单位联系人账号信息，已经分配过账号的做密码修改操作，返回1成功
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operator">操作人编号</param>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="lxrId">联系人编号</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        int SetCrmUser(string companyId, string operatorId, string crmId, string lxrId, string username, EyouSoft.Model.ComStructure.MPasswordInfo pwd);
        /// <summary>
        /// 获得客户单位联系人列表(含账号信息)
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <returns></returns>
        IList<Model.CrmStructure.MCrmUserInfo> GetCrmUsers(string crmId);
        /// <summary>
        /// 修改客户单位的打印配置
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <param name="printHead">打印头</param>
        /// <param name="printFoot">打印尾</param>
        /// <param name="printTemplates">打印模板</param>
        /// <param name="seal">公章</param>
        /// <returns></returns>
        bool UpdatePrintSet(string crmId, string printHead, string printFoot, string printTemplates, string seal);
        /// <summary>
        /// 获取客户单位信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>        
        /// <param name="depts">数据级浏览权限控制-部门集合</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="crmType">客户单位类型</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<Model.CrmStructure.MLBCrmInfo> GetCrms(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.EnumType.CrmStructure.CrmType? crmType, EyouSoft.Model.CrmStructure.MLBCrmSearchInfo searchInfo);
        /// <summary>
        /// 获取客户单位信息集合(选用时使用)
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="crmType">客户单位类型</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CrmStructure.MLBCrmXuanYongInfo> GetCrmsXuanYong(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.EnumType.CrmStructure.CrmType? crmType, EyouSoft.Model.CrmStructure.MLBCrmSearchInfo searchInfo);
        /// <summary>
        /// 获取个人会员信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CrmStructure.MLBCrmPersonalInfo> GetCrmsPersonal(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CrmStructure.MLBCrmPersonalSearchInfo searchInfo);
        /// <summary>
        /// 验证客户单位责任销售，返回真匹配，返回假不匹配
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="sellerId">销售员编号</param>
        /// <returns></returns>
        bool YanZhengZeRenXiaoShou(string crmId, string sellerId);
    }
}
