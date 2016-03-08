using System.Collections.Generic;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.IndStructure;
namespace EyouSoft.IDAL.IndStructure
{
    /// <summary>
    /// 个人中心
    /// 创建者：郑知远
    /// 创建时间：2011/09/06
    /// </summary>
    public interface IIndividual
    {
        #region 备忘录

        /// <summary>
        /// 添加备忘录
        /// </summary>
        /// <param name="mdl">备忘录实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddMemo(MMemo mdl);

          /// 修改备忘录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateMemo(MMemo model);

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        bool DelMemo(int Id, string CompanyId);
        
        /// <summary>
        /// 根据备忘录编号获取备忘录详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>备忘录详细信息</returns>
        MMemo GetMemo(int id);

        /// <summary>
        /// 根据操作者编号获取指定数备忘录
        /// </summary>
        /// <param name="top">前几条记录,0表示全部</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="StartDate">备忘时间开始</param>
        /// <param name="EndDate">备忘时间结束</param>
        /// <returns>备忘录列表</returns>
        IList<MMemo> GetMemoLst(int top, string operatorId, System.DateTime? StartDate, System.DateTime? EndDate);

        #endregion

        #region 事务提醒

        /// <summary>
        /// 订单提醒
        /// 说明：用于分销商下单提醒，提醒计划的销售员或供应商发布计划的审核人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="operatorId">用户编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>订单列表</returns>
        IList<MOrderRemind> GetOrderRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId, string companyId);
       
        /// <summary>
        /// 计调提醒
        /// 说明:提醒派团给计调时指定的计调员，未接收的均做提醒
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="operatorId">用户编号</param>
        /// <returns></returns>
        IList<MPlanRemind> GetPlanRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId);

        /// <summary>
        /// 收款提醒
        /// 说明:提醒客户单位责任销售员
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="operatorId">用户编号</param>
        /// <returns>欠款客户单位列表</returns>
        IList<MReceivablesRemind> GetReceivablesRemindLst(int pageSize, int pageIndex, ref int recordCount
 , string companyId, string operatorId);

        /// <summary>
        /// 计划变更
        /// 说明:提醒相应的团队计调
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="operatorId">用户编号</param>
        /// <returns>变更信息列表</returns>
        IList<MTourChangeRemind> GetTourChangeRemindLst(int pageSize, int pageIndex, ref int recordCount
 , string companyId, string operatorId);

        /// <summary>
        /// 获取计划变更实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        MTourChangeRemind GetTourChangeModel(string CompanyId, int Id);

        /// <summary>
        /// 计划确认变更
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <returns></returns>
        bool TourChangeSure(MTourChangeRemind model);

        /// <summary>
        /// 订单变更
        /// 说明:提醒相应的团队计调
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="operatorId">用户编号</param>
        /// <returns>变更信息列表</returns>
        IList<MOrderChangeRemind> GetOrderChangeRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId);

        /// <summary>
        /// 获取订单变更实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        MOrderChangeRemind GetOrderChangeModel(string CompanyId, string Id);

        /// <summary>
        /// 订单确认变更
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <returns></returns>
        bool OrderChangeSure(MOrderChangeRemind model);

        ///// <summary>
        ///// 酒店预控到期提醒
        ///// 说明:提醒预控登记人
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <param name="operatorId">用户编号</param>
        ///// <param name="EarlyDays">提前天数提醒</param>
        ///// <returns>酒店预控到期酒店列表</returns>
        //IList<MHotelcontrolRemindHotel> GetHotelcontrolHotelUsedLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId, int EarlyDays);

        ///// <summary>
        ///// 车辆预控到期提醒
        ///// 说明：提醒预控登记人
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <param name="operatorId">用户编号</param>
        ///// <param name="EarlyDays">提前天数提醒</param> 
        ///// <returns></returns>
        //IList<MCarcontrolRemindVehicle> GetCarcontrolHotelUsedLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId, int EarlyDays);
        
        ///// <summary>
        ///// 游船预控到期提醒
        ///// 说明：提醒预控登记人
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <param name="operatorId">用户编号</param>
        /////  <param name="EarlyDays">提前天数提醒</param>
        ///// <returns></returns>
        //IList<MShipcontrolRemindCruise> GetShipcontrolHotelUsedLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId, int EarlyDays);
        
        /// <summary>
        /// 劳动合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="EarlyDays">提前天数提醒</param>
        /// <returns>劳动合同信息列表</returns>
        IList<MLaborContractExpireRemind> GetLaborContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, int EarlyDays);

        /// <summary>
        /// 供应商合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="EarlyDays">提前天数提醒</param> 
        /// <returns>劳动合同信息列表</returns>
        IList<MSourceContractExpireRemind> GetSourceContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, int EarlyDays);

        /// <summary>
        /// 公司合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="EarlyDays">提前天数提醒</param> 
        /// <returns>劳动合同信息列表</returns>
        IList<MCompanyContractExpireRemind> GetCompanyContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, int EarlyDays);

        /// <summary>
        /// 公告通知：根据用户编号和系统公司编号获取公告列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="operatorId">用户编号</param>
        /// <param name="deptid">用户部门编号</param> 
        /// <param name="companyId">系统公司编号</param>
        /// <returns>公告列表</returns>
        IList<MNoticeRemind> GetNoticeRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId,int deptid,string companyId);

        ///// <summary>
        ///// 根据键名获取值
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="SettingType"></param>
        ///// <returns></returns>
        //string GetSettingByType(string CompanyId, SysConfiguration SettingType);
        
        ///// <summary>
        ///// 修改公司配置项
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <param name="Value"></param>
        ///// <param name="CompanyId"></param>
        ///// <returns></returns>
        //bool UpdateComSetting(SysConfiguration Key, string Value, string CompanyId);

         /// <summary>
        /// 询价提醒
        /// 提醒询价时指定的计调员
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="operatorId">用户编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns></returns>
        IList<MInquiryRemind> GetInquiryRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId, string companyId);

         /// <summary>
        /// 根据类型得到提醒数
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OperatorId"></param>
        /// <param name="type"></param>
        /// <param name="setting">系统配置信息业务实体</param>
        /// <returns></returns>
        int GetRemindCountByType(string CompanyId, string OperatorId, EyouSoft.Model.EnumType.IndStructure.RemindType type, EyouSoft.Model.ComStructure.MComSetting setting);
       
        #endregion

        #region 工作汇报

        /// <summary>
        /// 添加工作汇报
        /// </summary>
        /// <param name="mdl">汇报实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddWorkReport(MWorkReport mdl);

        /// <summary>
        /// 修改工作汇报
        /// </summary>
        /// <param name="mdl">汇报实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool UpdWorkReport(MWorkReport mdl);

        /// <summary>
        /// 删除工作汇报
        /// </summary>
        /// <param name="ids">汇报编号集合</param>
        /// <returns>True：成功 False：失败</returns>
        bool DelWorkReport(params int[] ids);

        /// <summary>
        /// 根据汇报编号获取工作汇报实体
        /// </summary>
        /// <param name="id">汇报编号</param>
        /// <returns>工作汇报实体</returns>
        MWorkReport GetWorkReport(int id);

        /// <summary>
        /// 根据工作汇报搜索实体获取工作汇报列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="mSearch"></param>
        /// <returns></returns>
        IList<MWorkReport> GetWorkReportLst(string CompanyId,int pageSize
                                            , int pageIndex
                                            , ref int recordCount
                                            , MWorkReportSearch mSearch);

          /// <summary>
        /// 审批工作汇报
        /// </summary>
        /// <param name="model">工作汇报实体</param>
        /// <returns></returns>
        bool SetWorkReportStatus(MWorkReportCheck model);

        #endregion

        #region 工作计划

        /// <summary>
        /// 添加工作计划
        /// </summary>
        /// <param name="mdl">计划实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddWorkPlan(MWorkPlan mdl);

        /// <summary>
        /// 修改工作计划
        /// </summary>
        /// <param name="mdl">计划实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool UpdWorkPlan(MWorkPlan mdl);

        /// <summary>
        /// 删除工作汇报
        /// </summary>
        /// <param name="workPlanIds">工作计划编号集合</param>
        /// <returns>True：成功 False：失败</returns>
        bool DelWorkPlan(params string[] workPlanIds);

        /// <summary>
        /// 根据工作计划编号获取工作计划实体
        /// </summary>
        /// <param name="workPlanId">工作计划编号</param>
        /// <returns>工作计划实体</returns>
        MWorkPlan GetWorkPlan(string workPlanId);

       /// <summary>
        /// 根据工作计划搜索实体获取工作计划列表
       /// </summary>
       /// <param name="CompanyId"></param>
       /// <param name="pageSize"></param>
       /// <param name="pageIndex"></param>
       /// <param name="recordCount"></param>
       /// <param name="mSearch"></param>
       /// <returns></returns>
        IList<MWorkPlan> GetWorkPlanLst(string CompanyId,int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , MWorkPlanSearch mSearch);

        /// <summary>
        /// 审核工作计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SetWorkPlanStatus(MWorkPlanCheck model);

          /// <summary>
        /// 工作计划结束
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SetWorkPlanEnd(MWorkPlan model);

        #endregion

        #region 个人密码修改
        /// <summary>
        /// 个人密码修改
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="NewPwd">新密码</param>
        /// <param name="MD5Pwd">MD5密码</param>
        /// <returns></returns>
        bool PwdModify(string UserId, string OldPwd, string NewPwd, string MD5Pwd);
        
        #endregion
    }
}
