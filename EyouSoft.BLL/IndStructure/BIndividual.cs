using System;
using System.Collections.Generic;
using System.Linq;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL.IndStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.EnumType.IndStructure;
using EyouSoft.Model.IndStructure;
using EyouSoft.Toolkit;
namespace EyouSoft.BLL.IndStructure
{
    /// <summary>
    /// 个人中心业务逻辑层
    /// 创建者：郑知远
    /// 创建时间：2011/09/07
    /// </summary>
    public class BIndividual
    {
        private readonly IIndividual _idal = ComponentFactory.CreateDAL<IIndividual>();

        //private readonly EyouSoft.IDAL.SourceStructure.ISourceControl Sourcedal =
        //   EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceControl>();

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BIndividual() { }

        #endregion

        #region 备忘录

        /// <summary>
        /// 添加备忘录
        /// </summary>
        /// <param name="mdl">备忘录实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddMemo(MMemo mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.CompanyId) || string.IsNullOrEmpty(mdl.MemoTitle) || string.IsNullOrEmpty(mdl.OperatorId))
            {
                return false;
            }
            var isOk = this._idal.AddMemo(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("新增了备忘录数据");
            }

            return isOk;
        }
        /// 修改备忘录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMemo(MMemo model)
        {
            return _idal.UpdateMemo(model);
        }

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public bool DelMemo(int Id, string CompanyId)
        {
            return _idal.DelMemo(Id, CompanyId);
        }
        /// <summary>
        /// 根据备忘录编号获取备忘录详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>备忘录详细信息</returns>
        public MMemo GetMemo(int id)
        {
            return id <= 0 ? null : this._idal.GetMemo(id);
        }

        /// <summary>
        /// 根据操作者编号获取指定数备忘录
        /// </summary>
        /// <param name="top">前几条记录,0表示全部</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="StartDate">备忘时间开始</param>
        /// <param name="EndDate">备忘时间结束</param>
        /// <returns>备忘录列表</returns>
        public IList<MMemo> GetMemoLst(int top, string operatorId, System.DateTime? StartDate, System.DateTime? EndDate)
        {
            if (string.IsNullOrEmpty(operatorId))
            {
                return null;
            }
            return this._idal.GetMemoLst(top, operatorId, StartDate, EndDate);
        }

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
        public IList<MOrderRemind> GetOrderRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId, string companyId)
        {
            return _idal.GetOrderRemindLst(pageSize, pageIndex, ref recordCount, operatorId, companyId);
        }

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
        public IList<MPlanRemind> GetPlanRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        {
            return _idal.GetPlanRemindLst(pageSize, pageIndex, ref recordCount, companyId, operatorId);
        }

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
        public IList<MReceivablesRemind> GetReceivablesRemindLst(int pageSize, int pageIndex, ref int recordCount
 , string companyId, string operatorId)
        {
            return _idal.GetReceivablesRemindLst(pageSize, pageIndex, ref recordCount, companyId, operatorId);
        }

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
        public IList<MTourChangeRemind> GetTourChangeRemindLst(int pageSize, int pageIndex, ref int recordCount
 , string companyId, string operatorId)
        {
            return _idal.GetTourChangeRemindLst(pageSize, pageIndex, ref recordCount, companyId, operatorId);
        }

        /// <summary>
        /// 获取计划变更实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public MTourChangeRemind GetTourChangeModel(string CompanyId, int Id)
        {
            return _idal.GetTourChangeModel(CompanyId, Id);
        }

        /// <summary>
        /// 计划确认变更
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <returns></returns>
        public bool TourChangeSure(MTourChangeRemind model)
        {
            return _idal.TourChangeSure(model);
        }

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
        public IList<MOrderChangeRemind> GetOrderChangeRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        {
            return _idal.GetOrderChangeRemindLst(pageSize, pageIndex, ref recordCount, companyId, operatorId);
        }

        /// <summary>
        /// 获取订单变更实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public MOrderChangeRemind GetOrderChangeModel(string CompanyId, string Id)
        {
            return _idal.GetOrderChangeModel(CompanyId, Id);
        }

        /// <summary>
        /// 订单确认变更
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <returns></returns>
        public bool OrderChangeSure(MOrderChangeRemind model)
        {
            return _idal.OrderChangeSure(model);
        }

        ///// <summary>
        ///// 酒店预控到期提醒
        ///// 说明:提醒预控登记人
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <param name="operatorId">用户编号</param>
        ///// <returns>酒店预控到期酒店列表</returns>
        //public IList<EyouSoft.Model.SourceStructure.MSourceSueHotel> GetHotelcontrolHotelUsedLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        //{
        //    int EarlyDays = 10;
        //    var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
        //    if (setting != null) EarlyDays = setting.HotelControlRemind;

        //    EyouSoft.Model.SourceStructure.MSourceSueHotelSearch search = new EyouSoft.Model.SourceStructure.MSourceSueHotelSearch()
        //    {
        //        CompanyId = companyId,
        //        OperatorId = operatorId,
        //        IsTranTip = true,
        //        EarlyDays = EarlyDays
        //    };

        //    return Sourcedal.GetListSueHotel(pageIndex, pageSize, ref recordCount, search);
        //}

        ///// <summary>
        ///// 车辆预控到期提醒
        ///// 说明：提醒预控登记人
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <param name="operatorId">用户编号</param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.SourceStructure.MSourceSueCar> GetCarcontrolHotelUsedLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        //{
        //    int EarlyDays = 10;
        //    var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
        //    if (setting != null) EarlyDays = setting.CarControlRemind;

        //    EyouSoft.Model.SourceStructure.MSourceSueCarSearch search = new EyouSoft.Model.SourceStructure.MSourceSueCarSearch()
        //    {
        //        CompanyId = companyId,
        //        OperatorId = operatorId,
        //        IsTranTip = true,
        //        EarlyDays = EarlyDays
        //    };
        //    return Sourcedal.GetListSueCar(pageIndex, pageSize, ref recordCount, search);

        //}

        ///// <summary>
        ///// 游船预控到期提醒
        ///// 说明：提醒预控登记人
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <param name="operatorId">用户编号</param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.SourceStructure.MSourceSueShip> GetShipcontrolHotelUsedLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        //{
        //    int EarlyDays = 10;
        //    var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
        //    if (setting != null) EarlyDays = setting.ShipControlRemind;

        //    EyouSoft.Model.SourceStructure.MSourceSueShipSearch search = new EyouSoft.Model.SourceStructure.MSourceSueShipSearch()
        //    {
        //        CompanyId = companyId,
        //        OperatorId = operatorId,
        //        IsTranTip = true,
        //        EarlyDays = EarlyDays
        //    };
        //    return Sourcedal.GetListSueShip(pageIndex, pageSize, ref recordCount, search);

        //}

        ///// <summary>
        ///// 景点预控到期提醒
        ///// 说明：提醒预控登记人
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <param name="operatorId">用户编号</param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.SourceStructure.MSourceSueSight> GetPreControlSightUsedLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        //{
        //    int EarlyDays = 10;
        //    var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
        //    if (setting != null) EarlyDays = setting.SightControlRemind;

        //    var search = new EyouSoft.Model.SourceStructure.MSourceSueSightSearch()
        //    {
        //        CompanyId = companyId,
        //        OperatorId = operatorId,
        //        IsTranTip = true,
        //        EarlyDays = EarlyDays
        //    };
        //    return Sourcedal.GetListSueSight(pageIndex, pageSize, ref recordCount, search);

        //}

        ///// <summary>
        ///// 其他预控到期提醒
        ///// 说明：提醒预控登记人
        ///// </summary>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="pageIndex">当前页码</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <param name="companyId">系统公司编号</param>
        ///// <param name="operatorId">用户编号</param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.SourceStructure.MSourceSueOther> GetPreControlOtherUsedLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        //{
        //    int EarlyDays = 10;
        //    var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
        //    if (setting != null) EarlyDays = setting.OtherControlRemind;

        //    var search = new EyouSoft.Model.SourceStructure.MSourceSueOtherSearch()
        //    {
        //        CompanyId = companyId,
        //        OperatorId = operatorId,
        //        IsTranTip = true,
        //        EarlyDays = EarlyDays
        //    };
        //    return Sourcedal.GetListSueOther(pageIndex, pageSize, ref recordCount, search);

        //}

        /// <summary>
        /// 劳动合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>劳动合同信息列表</returns>
        public IList<MLaborContractExpireRemind> GetLaborContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            int EarlyDays = 10;
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
            if (setting != null) EarlyDays = setting.ContractRemind;

            return _idal.GetLaborContractExpireRemindLst(pageSize, pageIndex, ref recordCount, companyId, EarlyDays);

        }

        /// <summary>
        /// 供应商合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>劳动合同信息列表</returns>
        public IList<MSourceContractExpireRemind> GetSourceContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            int EarlyDays = 10;
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
            if (setting != null) EarlyDays = setting.SContractRemind;

            return _idal.GetSourceContractExpireRemindLst(pageSize, pageIndex, ref recordCount, companyId, EarlyDays);
        }

        /// <summary>
        /// 公司合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>劳动合同信息列表</returns>
        public IList<MCompanyContractExpireRemind> GetCompanyContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            int EarlyDays = 10;
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);
            if (setting != null) EarlyDays = setting.ComPanyContractRemind;

            return _idal.GetCompanyContractExpireRemindLst(pageSize, pageIndex, ref recordCount, companyId, EarlyDays);
        }

        /// <summary>
        /// 根据类型得到提醒数
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OperatorId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetRemindCountByType(string CompanyId, string OperatorId, RemindType type)
        {
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
            if (setting == null) setting = new EyouSoft.Model.ComStructure.MComSetting();

            return _idal.GetRemindCountByType(CompanyId, OperatorId, type, setting);
            //int recordCount = 0;
            //switch (type)
            //{
            //    case RemindType.变更提醒:
            //        {
            //            int temp = 0;
            //            GetOrderChangeRemindLst(20, 1, ref temp, CompanyId, OperatorId);
            //            GetTourChangeRemindLst(20, 1, ref recordCount, CompanyId, OperatorId);
            //            recordCount += temp;
            //            break;
            //        }
            //    case RemindType.订单提醒:
            //        GetOrderRemindLst(20, 1, ref recordCount, OperatorId, CompanyId);
            //        break;
            //    case RemindType.合同到期提醒:
            //        {
            //            int temp1 = 0;
            //            int temp2 = 0;
            //            GetLaborContractExpireRemindLst(20, 1, ref temp1, CompanyId);
            //            GetCompanyContractExpireRemindLst(20, 1, ref temp2, CompanyId);
            //            GetSourceContractExpireRemindLst(20, 1, ref recordCount, CompanyId);
            //            recordCount = recordCount + temp1 + temp2;
            //            break;
            //        }
            //    case RemindType.计调提醒:
            //        GetPlanRemindLst(20, 1, ref recordCount, CompanyId, OperatorId);
            //        break;
            //    case RemindType.收款提醒:
            //        GetReceivablesRemindLst(20, 1, ref recordCount, CompanyId, OperatorId);
            //        break;
            //    case RemindType.预控到期提醒:
            //        {
            //            int temp1 = 0;
            //            int temp2 = 0;
            //            GetHotelcontrolHotelUsedLst(20, 1, ref temp1, CompanyId, OperatorId);
            //            GetShipcontrolHotelUsedLst(20, 1, ref temp2, CompanyId, OperatorId);
            //            GetCarcontrolHotelUsedLst(20, 1, ref recordCount, CompanyId, OperatorId);
            //            recordCount = recordCount + temp1 + temp2;
            //            break;
            //        }
            //    case RemindType.询价提醒:
            //        GetInquiryRemindLst(20, 1, ref recordCount, OperatorId, CompanyId);
            //        break;
            //}
            //return recordCount;
        }

        ///// <summary>
        ///// 根据键名获取值
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="SettingType"></param>
        ///// <returns></returns>
        //public string GetSettingByType(string CompanyId, SysConfiguration SettingType)
        //{
        //    return _idal.GetSettingByType(CompanyId, SettingType);
        //}

        ///// <summary>
        ///// 修改公司配置项
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <param name="Value"></param>
        ///// <param name="CompanyId"></param>
        ///// <returns></returns>
        //public bool UpdateComSetting(SysConfiguration Key, string Value, string CompanyId)
        //{
        //    return _idal.UpdateComSetting(Key, Value, CompanyId);
        //}

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
        public IList<MInquiryRemind> GetInquiryRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId, string companyId)
        {
            return _idal.GetInquiryRemindLst(pageSize, pageIndex, ref recordCount, operatorId, companyId);
        }
        #endregion

        #region 公告通知
        /// <summary>
        /// 公告通知
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="operatorId">用户编号</param>
        /// <param name="DeptId">用户部门编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>公告列表</returns>
        public IList<MNoticeRemind> GetNoticeRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId, int DeptId, string companyId)
        {
            return _idal.GetNoticeRemindLst(pageSize, pageIndex, ref recordCount, operatorId, DeptId, companyId);
        }
        #endregion

        #region 工作汇报

        /// <summary>
        /// 添加工作汇报
        /// </summary>
        /// <param name="mdl">汇报实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddWorkReport(MWorkReport mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.CompanyId) || string.IsNullOrEmpty(mdl.Title)
                || mdl.DepartmentId <= 0 || string.IsNullOrEmpty(mdl.OperatorId) || string.IsNullOrEmpty(mdl.OperatorName))
            {
                return false;
            }
            var isOk = this._idal.AddWorkReport(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("新增了工作汇报标题：{0}数据", mdl.Title));
            }

            return isOk;
        }

        /// <summary>
        /// 修改工作汇报
        /// </summary>
        /// <param name="mdl">汇报实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdWorkReport(MWorkReport mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.Title) || mdl.DepartmentId == 0 ||
                mdl.Id <= 0 || string.IsNullOrEmpty(mdl.OperatorId) || string.IsNullOrEmpty(mdl.OperatorName))
            {
                return false;
            }
            var isOk = this._idal.UpdWorkReport(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了工作汇报编号：{0}数据", mdl.Id));
            }

            return isOk;
        }

        /// <summary>
        /// 删除工作汇报
        /// </summary>
        /// <param name="ids">汇报编号集合</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelWorkReport(params int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return false;
            }
            var isOk = this._idal.DelWorkReport(ids);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了工作汇报编号：{0}数据", Utils.GetSqlIdStrByArray(ids)));
            }

            return isOk;
        }

        /// <summary>
        /// 根据汇报编号获取工作汇报实体
        /// </summary>
        /// <param name="id">汇报编号</param>
        /// <returns>工作汇报实体</returns>
        public MWorkReport GetWorkReport(int id)
        {
            return id == 0 ? null : this._idal.GetWorkReport(id);
        }

        /// <summary>
        /// 根据工作汇报搜索实体获取工作汇报列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="mSearch"></param>
        /// <returns></returns>
        public IList<MWorkReport> GetWorkReportLst(string CompanyId, int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , MWorkReportSearch mSearch)
        {

            return this._idal.GetWorkReportLst(CompanyId, pageSize, pageIndex, ref recordCount, mSearch);
        }

        /// <summary>
        /// 审批工作汇报
        /// </summary>
        /// <param name="model">工作汇报实体</param>
        /// <returns></returns>
        public bool SetWorkReportStatus(MWorkReportCheck model)
        {
            if (_idal.SetWorkReportStatus(model))
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(
                    string.Format("审核人编号：{0}修改了工作汇报编号：{1}的审核状态数据", model.Approver, model.Id));
                return true;
            }
            return false;
        }

        #endregion

        #region 工作计划

        /// <summary>
        /// 添加工作计划
        /// </summary>
        /// <param name="mdl">计划实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddWorkPlan(MWorkPlan mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.CompanyId) || string.IsNullOrEmpty(mdl.Title)
                || string.IsNullOrEmpty(mdl.OperatorId) || string.IsNullOrEmpty(mdl.OperatorName))
            {
                return false;
            }
            var isOk = this._idal.AddWorkPlan(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("新增了工作计划编号：{0}数据", mdl.WorkPlanId));
            }

            return isOk;
        }

        /// <summary>
        /// 修改工作计划
        /// </summary>
        /// <param name="mdl">计划实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdWorkPlan(MWorkPlan mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.CompanyId) || string.IsNullOrEmpty(mdl.Title)
                || mdl.WorkPlanId == 0 || string.IsNullOrEmpty(mdl.OperatorId) || string.IsNullOrEmpty(mdl.OperatorName))
            {
                return false;
            }
            var isOk = this._idal.UpdWorkPlan(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了工作计划编号：{0}数据", mdl.WorkPlanId));
            }

            return isOk;
        }

        /// <summary>
        /// 删除工作汇报
        /// </summary>
        /// <param name="workPlanIds">工作计划编号集合</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelWorkPlan(params string[] workPlanIds)
        {
            if (workPlanIds == null || workPlanIds.Length == 0)
            {
                return false;
            }
            var isOk = this._idal.DelWorkPlan(workPlanIds);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了工作计划编号：{0}数据", workPlanIds.Aggregate((current, workPlanId) => current + string.Format("{0},", workPlanIds)).TrimEnd(',')));
            }

            return isOk;
        }

        /// <summary>
        /// 根据工作计划编号获取工作计划实体
        /// </summary>
        /// <param name="workPlanId">工作计划编号</param>
        /// <returns>工作计划实体</returns>
        public MWorkPlan GetWorkPlan(string workPlanId)
        {
            return String.IsNullOrEmpty(workPlanId) ? null : this._idal.GetWorkPlan(workPlanId);
        }

        /// <summary>
        /// 根据工作计划搜索实体获取工作计划列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="mSearch"></param>
        /// <returns></returns>
        public IList<MWorkPlan> GetWorkPlanLst(string CompanyId, int pageSize
                                            , int pageIndex
                                            , ref int recordCount
                                            , MWorkPlanSearch mSearch)
        {
            if (mSearch == null || string.IsNullOrEmpty(CompanyId))
            {
                return null;
            }
            return this._idal.GetWorkPlanLst(CompanyId, pageSize, pageIndex, ref recordCount, mSearch);
        }

        /// <summary>
        /// 审核工作计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SetWorkPlanStatus(MWorkPlanCheck model)
        {
            if (_idal.SetWorkPlanStatus(model))
            {
                SysStructure.BSysLogHandle.Insert(string.Format("审核人：{0}修改了工作计划编号：{1}审核状态为{2}", model.Approver, model.Id, model.Status));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 工作计划结束
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SetWorkPlanEnd(MWorkPlan model)
        {
            if (_idal.SetWorkPlanEnd(model))
            {
                SysStructure.BSysLogHandle.Insert(string.Format("审核人：{0}修改了工作计划编号：{1}审核状态为{2}", model.OperatorName, model.WorkPlanId, model.Status));
                return true;
            }
            return false;
        }


        #endregion

        #region 个人密码修改
        /// <summary>
        /// 个人密码修改
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="NewPwd">新密码</param>
        /// <returns></returns>
        public bool PwdModify(string UserId, string OldPwd, string NewPwd)
        {
            string MD5Pwd = new EyouSoft.Toolkit.DataProtection.HashCrypto().MD5Encrypt(NewPwd);
            return _idal.PwdModify(UserId, OldPwd, NewPwd, MD5Pwd);
        }
        #endregion
    }
}
