using System;
using System.Collections.Generic;

namespace EyouSoft.IDAL.TourStructure
{
    using EyouSoft.Model.TourStructure;

    /// <summary>
    /// 描述：计划接口类
    /// 修改记录：
    /// 1、2011-09-05 PM 曹胡生 创建
    /// </summary>
    public interface ITour
    {
        /// <summary>
        /// 获得团队计划列表
        /// </summary>
        /// <param name="CompanyId">计划所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="ModuleType">模块类型</param>  
        /// <param name="TourTeamSearch">搜索实体</param>
        /// <param name="DeptIds">部门集合</param>
        /// <param name="isOnlySeft">是否仅查看自己的数据</param>
        /// <param name="LoginUserId">当前登录的用户编号</param> 
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourTeamInfo> GetTourTeamList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourTeamSearch TourTeamSearch, int[] DeptIds, bool isOnlySeft, string LoginUserId);

        /// <summary>
        /// 获得散拼计划列表
        /// </summary>
        /// <param name="CompanyId">计划所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="ModuleType">模块类型</param>  
        /// <param name="TourSanPinSearch">搜索实体</param>
        /// <param name="DeptIds">部门集合</param>
        /// <param name="isOnlySeft">是否仅查看自己的数据</param>
        /// <param name="LoginUserId">当前登录的用户编号</param>  
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSanPinList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSanPinSearch TourSanPinSearch, int[] DeptIds, bool isOnlySeft, string LoginUserId);

        /// <summary>
        /// 获得分销商平台计划列表
        /// </summary>
        /// <param name="CompanyId">分销商所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MTourSaleSearch"></param>
        /// <param name="LevelId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSaleList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSaleSearch MTourSaleSearch, string LevelId);

        /// <summary>
        /// 获得供应商平台计划列表
        /// </summary>
        /// <param name="SourceId">供应商编号</param> 
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="TourSanPinSearch">搜索实体</param>         
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSupplierList(string SourceId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSupplierSearch MTourSupplierSearch);

        /// <summary>
        /// 获得计调列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="ModuleType"></param>
        /// <param name="MPlanListSearch"></param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MPlanList> GetPlanList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MPlanListSearch MPlanListSearch, int[] DetpIds, bool isOnlySeft, string LoginUserId);

        /// <summary>
        /// 获得计划报账列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="BZList"></param>
        /// <param name="DeptIds">部门编号</param>  
        /// <param name="isOnlySelf">是否仅查看自己的数据</param>  
        /// <param name="LoginUserId">当前登录用户编号</param> 
        /// <param name="MBZSearch"></param>
        /// <param name="setting">系统配置信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MBZInfo> GetBZList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.EnumType.TourStructure.BZList BZList, int[] DeptIds, bool isOnlySelf, string LoginUserId, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, EyouSoft.Model.ComStructure.MComSetting setting);

        /// <summary>
        /// 获得计划签证文件
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.ComStructure.MComAttach> GetVisaFileList(string TourId, int pageSize, int pageIndex, ref int recordCount);

        /// <summary>
        /// 派团给计调的未出团计划列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="RouteName"></param>
        /// <param name="TourCode"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourBaseInfo> GetSendWCTTour(string CompanyId, int pageSize, int pageIndex, ref int recordCount, string RouteName, string TourCode, string LoginUserId);

        /// <summary>
        /// 获得计划实体
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MTourBaseInfo GetTourInfo(string TourId);

        /// <summary>
        /// 获得计划基础信息实体
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MTourBaseInfo GetBasicTourInfo(string TourId);

        /// <summary>
        /// 新增团队计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int AddTourTeam(EyouSoft.Model.TourStructure.MTourTeamInfo info);

        /// <summary>
        /// 新增散拼计划
        /// </summary>
        /// <param name="info"></param>
        /// <param name="SendDate">出团日期</param>
        /// <returns></returns>
        int AddTourSanPin(EyouSoft.Model.TourStructure.MTourSanPinInfo info);

        /// <summary>
        /// 修改团队计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool UpdateTourTeam(EyouSoft.Model.TourStructure.MTourTeamInfo info);

        /// <summary>
        /// 修改散拼计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int UpdateTourSanPin(EyouSoft.Model.TourStructure.MTourSanPinInfo info);

        /// <summary>
        /// 派团给计调
        /// </summary>
        /// <param name="SendTour">派团给计调实体</param>
        /// <returns></returns>
        bool SendTour(EyouSoft.Model.TourStructure.MSendTour SendTour);

        /// <summary>
        /// 获取线路区域计划数
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MAreaTour> GetAreaTour(string CompanyId);

        /// <summary>
        /// 获取关键字计划数
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MKeyTour> GetKeyTour(string CompanyId);

        /// <summary>
        /// 获得同行分销散拼计划
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="TourSanPinSearch"></param>
        /// <param name="DeptIds">部门集合</param>
        /// <param name="isOnlySeft">是否仅查看自己的数据</param>
        /// <param name="LoginUserId">当前登录的用户编号</param> 
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourTongHanInfo> GetTYFXTourSanPinList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSanPinSearch TourSanPinSearch, int[] DeptIds, bool isOnlySeft, string LoginUserId);

        /// <summary>
        /// 获取计划行程变更列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourPlanChange> GetTourPlanChange(MTourPlanChangeBase mSearch, int pageSize, int pageIndex, ref int recordCount, int[] DetpIds, bool isOnlySeft, string LoginUserId);

        /// <summary>
        /// 获取计划变更实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MTourPlanChange GetTourChangeModel(string CompanyId, int Id);

        /// <summary>
        /// 计划确认变更
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <returns></returns>
        int TourChangeSure(EyouSoft.Model.TourStructure.MTourPlanChangeConfirm model);

        /// <summary>
        /// 业务变更新增/修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int TourChangeAddOrUpd(EyouSoft.Model.TourStructure.MTourPlanChange model);

        /// <summary>
        /// 取消计划
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <param name="CancelReson">取消原因</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="OperatorId">当前操作人编号</param>
        /// <returns></returns>
        bool CancelTour(string TourId, string CancelReson, string CompanyId, string OperatorId);

        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SuccessDelTourIds">成功删除的计划编号列表</param>
        /// <param name="TourIds">计划编号列表</param>
        /// <returns></returns>
        void DeleteTour(string CompanyId, ref List<string> SuccessDelTourIds, string[] TourIds);

        /// <summary>
        /// 根据计划编号得到行程
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> GetTourPlan(string TourId);

        /// <summary>
        /// 供应商发布的计划审核(成功进同行分销与分销商平台)
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <param name="ShowPublisher">供应商计划在分销商显示的发布人</param> 
        /// <param name="SaleInfo">审核人信息</param> 
        ///<param name="list">价格标准</param> 
        /// <returns></returns>
        bool Review(string TourId, EyouSoft.Model.EnumType.TourStructure.ShowPublisher ShowPublisher, EyouSoft.Model.TourStructure.MSaleInfo SaleInfo, IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list);

        /// <summary>
        /// 改变计划状态
        /// </summary>
        /// <param name="TourStatusChange">状态实体</param>
        /// <returns></returns>
        bool UpdateTourStatus(EyouSoft.Model.TourStructure.MTourStatusChange TourStatusChange);

        /// <summary>
        /// 手工设置收客状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        bool SetHandStatus(string TourId, EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus Status);

        /// <summary>
        /// 根据团号得到计划编号
        /// </summary>
        /// <param name="TourCode"></param>
        /// <returns></returns>
        string GetTourId(string TourCode);

        /// <summary>
        /// 报销报账报销
        /// </summary>
        /// <param name="approverDeptId">报销完成人员部门编号</param>
        /// <param name="approverId">报销完成人员编号</param>
        /// <param name="approver">报销完成人员名</param>
        /// <param name="TourId">计划编号</param>
        /// <param name="CompanyId">系统公司编号</param>
        /// <returns></returns>
        bool Apply(int approverDeptId, string approverId, string approver, string TourId, string CompanyId);

        /// <summary>
        /// 获得计划弹出信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MTourBaoInfo GetTourBaoInfo(string TourId);

        /// <summary>
        /// 资源预控团号选择
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="serach"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MControlTour> GetControlTourList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MControlTourSearch serach);

        /// <summary>
        /// 获得散拼价格
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourPriceStandard> GetTourSanPinPrice(string TourId);

        /// <summary>
        /// 根据计划编号获得所有游客
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTourTravellerList(string TourId);

        /// <summary>
        /// 派团时的订单列表
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MSendTourOrderList> GetSendTourOrderList(string TourId);

        /// <summary>
        /// 获取计划数量
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        string GetTourNum(string CompanyId);

        /// <summary>
        /// 得到计划发布人信息
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="OperatorId"></param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MPublisherInfo GetPublisherInfo(EyouSoft.Model.EnumType.ComStructure.UserType UserType, string OperatorId);

        /// <summary>
        /// 获得供应商发布的价格
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MSupplierPublishPrice GetSupplyPrice(string TourId);

        /// <summary>
        /// 获取计划状态
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        EyouSoft.Model.EnumType.TourStructure.TourStatus GetTourStatus(string CompanyId, string TourId);

        /// <summary>
        /// 添加计划原始信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddOriginalTourInfo(EyouSoft.Model.TourStructure.MTourOriginalInfo model);

        /// <summary>
        /// 获取计划原始信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MTourOriginalInfo GetOriginalTourInfo(string TourId, string CompanyId);

        /// <summary>
        /// 获得订单利润分配订单列表
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourOrderDisInfo> GetTourOrderDisList(string TourId);

        /// <summary>
        /// 得到发布人信息
        /// </summary>
        /// <param name="SourceId"></param>
        /// <param name="OperatorId"></param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MPersonInfo GetPersonInfo(string SourceId, string OperatorId);

        /// <summary>
        /// 判断供应商发布的计划是否审核，审核后，供应商平台不能修改计划
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="SourceId"></param>
        /// <returns></returns>
        bool GetSupplierTourCheckStatus(string TourId, string SourceId);

        /// <summary>
        /// 得到计划的成本确认状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        bool GetCostStatus(string TourId);

        /// <summary>
        /// 根据计划编号获得计划类型
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        EyouSoft.Model.EnumType.TourStructure.TourType GetTourType(string TourId);

        /// <summary>
        /// 得到计划合同金额确认状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        bool GetConfirmMoneyStatus(string TourId);
        /// <summary>
        /// 获取计划价格备注信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        string GetTourJiaGeBeiZhu(string tourId);

        /// <summary>
        /// 添加垫付申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns>0:失败，1：成功，2：销售员或客户单位不存在欠款</returns>
        int AddAdvanceApp(EyouSoft.Model.TourStructure.MAdvanceApp model);




        #region--2012-08-20 短线功能添加的方法-----------------------------------------
        /// <summary>
        /// 获取计划预设车型
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourCarType> GetTourCarType(string TourId);

        /// <summary>
        /// 获取计划上车地点
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourCarLocation> GetTourCarLocation(string TourId);

        /// <summary>
        /// 修改散拼短线的预设车型
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        bool UpdateTourCarType(string TourId, string Operator, string OperatorId, IList<EyouSoft.Model.TourStructure.MTourCarType> list);


        /// <summary>
        /// 将车型、座次变更的信息变为已读状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool IsReadCarTypeSeatChange(string Id);


        /// <summary>
        /// 获取分销商订单座次变更的提示消息
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="SourceId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MCarTypeSeatChange> GetCarTypeSeatChangeList(string CompanyId, string SourceId, int top, EyouSoft.Model.EnumType.TourStructure.CarChangeType? CarChangeType);

        #endregion

        /// <summary>
        /// 统计分析-状态查询表：自行设定计划状态，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int SetTourStatus(EyouSoft.Model.TourStructure.MSetTourStatusInfo info);
    }
}
