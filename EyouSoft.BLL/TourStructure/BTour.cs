using System;
using System.Linq;
using System.Collections.Generic;
namespace EyouSoft.BLL.TourStructure
{
    using EyouSoft.Model.TourStructure;

    /// <summary>
    /// 描述：计划业务层
    /// 修改记录：
    /// 1、2011-09-05 PM 曹胡生 创建
    /// </summary>
    public class BTour : EyouSoft.BLL.BLLBase
    {
        private readonly EyouSoft.IDAL.TourStructure.ITour dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.ITour>();

        #region 构造
        public BTour()
        {

        }
        #endregion

        #region 成员方法
        ///// <summary>
        ///// 获得团队计划列表
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="recordCount"></param>
        ///// <param name="ModuleType"></param>
        ///// <param name="TourTeamSearch"></param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.TourStructure.MTourTeamInfo> GetTourTeamList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourTeamSearch TourTeamSearch)
        //{
        //    bool isOnlySeft = false;
        //    int[] DeptIds = null;
        //    //switch (ModuleType)
        //    //{
        //        //case EyouSoft.Model.EnumType.TourStructure.ModuleType.组团:
        //        //    {
        //        //        DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.组团团队_派团计划, out isOnlySeft);
        //        //        break;
        //        //    }
        //        //case EyouSoft.Model.EnumType.TourStructure.ModuleType.出境:
        //        //    {
        //        //        DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.出境团队_派团计划, out isOnlySeft);
        //        //        break;
        //        //    }
        //        //case EyouSoft.Model.EnumType.TourStructure.ModuleType.地接:
        //        //    {
        //        //        DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.地接团队_派团计划, out isOnlySeft);
        //        //        break;
        //        //    }
        //    //}
        //    if (!TourTeamSearch.LDateStart.HasValue && !TourTeamSearch.LDateEnd.HasValue && !TourTeamSearch.RDateStart.HasValue && !TourTeamSearch.RDateEnd.HasValue)
        //    {
        //        EyouSoft.Model.ComStructure.MComSetting MComSet = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
        //        if (MComSet.ShowBeforeMonth != 0)
        //        {
        //            TourTeamSearch.LDateStart = DateTime.Now.AddMonths(-MComSet.ShowBeforeMonth);
        //        }
        //        if (MComSet.ShowAfterMonth != 0)
        //        {
        //            TourTeamSearch.LDateEnd = DateTime.Now.AddMonths(MComSet.ShowAfterMonth);
        //        }
        //    }

        //    return dal.GetTourTeamList(CompanyId, pageSize, pageIndex, ref recordCount, TourTeamSearch, DeptIds, isOnlySeft, LoginUserId);
        //}

        ///// <summary>
        ///// 获得散拼计划列表
        ///// </summary>
        ///// <param name="CompanyId">计划所属公司编号</param>
        ///// <param name="pageSize"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="recordCount"></param>
        ///// <param name="ModuleType">模块类型</param>  
        ///// <param name="TourSanPinSearch">搜索实体</param>
        ///// <param name="IsShowAll">是否显示全部</param>
        ///// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSanPinList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSanPinSearch TourSanPinSearch, bool IsShowAll)
        {
            bool isOnlySeft = false;
            int[] DeptIds = null;
            //switch (ModuleType)
            //{
            //    case EyouSoft.Model.EnumType.TourStructure.ModuleType.组团:
            //        {
            //            DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.组团团队_组团散拼, out isOnlySeft);
            //            break;
            //        }
            //    case EyouSoft.Model.EnumType.TourStructure.ModuleType.出境:
            //        {
            //            DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.出境团队_组团散拼, out isOnlySeft);
            //            break;
            //        }
            //    case EyouSoft.Model.EnumType.TourStructure.ModuleType.地接:
            //        {
            //            DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.地接团队_组团散拼, out isOnlySeft);
            //            break;
            //        }
            //}
            if (!TourSanPinSearch.SLDate.HasValue && !TourSanPinSearch.LLDate.HasValue && !TourSanPinSearch.SLDate.HasValue && !TourSanPinSearch.SRDate.HasValue&&!string.IsNullOrEmpty(TourSanPinSearch.ParentId))
            {
                EyouSoft.Model.ComStructure.MComSetting MComSet = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
                if (MComSet.ShowBeforeMonth != 0)
                {
                    TourSanPinSearch.SLDate = DateTime.Now.AddMonths(-MComSet.ShowBeforeMonth);
                }
                if (MComSet.ShowAfterMonth != 0)
                {
                    TourSanPinSearch.LLDate = DateTime.Now.AddMonths(MComSet.ShowAfterMonth);
                }
            }
            return dal.GetTourSanPinList(CompanyId, pageSize, pageIndex, ref recordCount, TourSanPinSearch, DeptIds, isOnlySeft, LoginUserId);
        }

        /// <summary>
        /// 获得分销商平台计划列表
        /// </summary>
        /// <param name="CompanyId">分销商所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MTourSaleSearch"></param>
        /// <param name="LevelId">客户等级编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSaleList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSaleSearch MTourSaleSearch, string LevelId)
        {
            return dal.GetTourSaleList(CompanyId, pageSize, pageIndex, ref recordCount, MTourSaleSearch, LevelId);
           
        }

        /// <summary>
        /// 获得供应商平台计划列表
        /// </summary>
        /// <param name="SourceId">供应商编号</param> 
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MTourSupplierSearch">搜索实体，无需搜索传null</param>        
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSupplierList(string SourceId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSupplierSearch MTourSupplierSearch)
        {
            if (string.IsNullOrEmpty(SourceId))
            {
                return null;
            }
            return dal.GetTourSupplierList(SourceId, pageSize, pageIndex, ref recordCount, MTourSupplierSearch);
        }

        /// <summary>
        /// 获得计划签证文件
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MComAttach> GetVisaFileList(string TourId, int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetVisaFileList(TourId, pageSize, pageIndex, ref recordCount);
        }

        /// <summary>
        /// 派团给计调的未出团计划列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="RouteName"></param>
        /// <param name="TourCode"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourBaseInfo> GetSendWCTTour(string CompanyId, int pageSize, int pageIndex, ref int recordCount, string RouteName, string TourCode)
        {
            return dal.GetSendWCTTour(CompanyId, pageSize, pageIndex, ref recordCount, RouteName, TourCode, LoginUserId);
        }

        /// <summary>
        /// 获得计划实体
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourBaseInfo GetTourInfo(string TourId)
        {
            if (string.IsNullOrEmpty(TourId))
            {
                return null;
            }
            return dal.GetTourInfo(TourId);
        }

        /// <summary>
        /// 获得计划基础信息实体
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourBaseInfo GetBasicTourInfo(string TourId)
        {
            if (string.IsNullOrEmpty(TourId))
            {
                return null;
            }
            return dal.GetBasicTourInfo(TourId);
        }

        /// <summary>
        /// 获得原始计划实体
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourBaseInfo GetOldTourInfo(string TourId, string CompanyId)
        {
            EyouSoft.Model.TourStructure.MTourBaseInfo TourBaseInfo = null;
            var model = dal.GetOriginalTourInfo(TourId, CompanyId);
            if (model != null && model.TourContent != "")
            {
                try
                {
                    //switch (model.TourType)
                    //{
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    //        {
                    //            TourBaseInfo = (EyouSoft.Model.TourStructure.MTourTeamInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(model.TourContent, typeof(EyouSoft.Model.TourStructure.MTourTeamInfo));
                    //            break;
                    //        }
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                    //        {
                    //            TourBaseInfo = (EyouSoft.Model.TourStructure.MTourSanPinInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(model.TourContent, typeof(EyouSoft.Model.TourStructure.MTourSanPinInfo));
                    //            break;
                    //        }
                    //}
                    return TourBaseInfo;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 新增团队计划(0:添加失败，1:添加成功,[2:垫付审请中(去掉)] 3:销售员超限 4: 客户超限 5:销售员客户均超限)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddTourTeam(EyouSoft.Model.TourStructure.MTourTeamInfo info)
        {
            info.TourId = System.Guid.NewGuid().ToString();
            info.OrderId = System.Guid.NewGuid().ToString();
            int result = dal.AddTourTeam(info);
            if (result == 1 || result == 2)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("新增{0},计划编号:{1}", info.TourType, info.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return result;
        }

        /// <summary>
        /// 新增散拼计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddTourSanPin(EyouSoft.Model.TourStructure.MTourSanPinInfo model)
        {
            if (string.IsNullOrEmpty(model.CompanyId)
                || model.AreaId == 0
                || string.IsNullOrEmpty(model.RouteName)
                || model.TourDays <= 0
                || string.IsNullOrEmpty(model.OperatorId)
                || string.IsNullOrEmpty(model.Operator)
                || model.OperatorDeptId == 0) return 0;

            model.TourId = Guid.NewGuid().ToString();
            if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品)
            {
                //if (string.IsNullOrEmpty(model.BuyCompanyID)
                //    || string.IsNullOrEmpty(model.BuyCompanyName)
                //    || model.Adults + model.Childs + model.Leaders <= 0) return 0;


                model.TourCode = GenerateTourNo(model.OperatorDeptId, model.CompanyId, model.TourType,model.LDate.Value);
                    
            }

            int flg = dal.AddTourSanPin(model);

            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("添加了计划,计划编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 修改团队计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateTourTeam(EyouSoft.Model.TourStructure.MTourTeamInfo info)
        {
            //if (!Verification(info, EyouSoft.Model.EnumType.TourStructure.TourType.组团团队))
            //{
            //    return false;
            //}
            if (dal.UpdateTourTeam(info))
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("修改{0},计划编号:{1},团号:{2}", info.TourType, info.TourId, info.TourCode);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改散拼计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateTourSanPin(EyouSoft.Model.TourStructure.MTourSanPinInfo model)
        {
            if (string.IsNullOrEmpty(model.CompanyId)
                 || model.AreaId == 0
                 || string.IsNullOrEmpty(model.RouteName)
                //|| string.IsNullOrEmpty(model.SellerId)
                //|| string.IsNullOrEmpty(model.SellerName)
                //|| model.SellerDeptId == 0
                 || model.TourDays <= 0
                 || string.IsNullOrEmpty(model.OperatorId)
                 || string.IsNullOrEmpty(model.Operator)
                 || model.OperatorDeptId == 0) return 0;

            if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品)
            {
                if (string.IsNullOrEmpty(model.BuyCompanyID)
                    || string.IsNullOrEmpty(model.BuyCompanyName)
                    || model.Adults + model.Childs + model.Leaders <= 0) return 0;
            }

            int flg = dal.UpdateTourSanPin(model);
            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("修改了计划,计划编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 派团给计调
        /// </summary>
        /// <param name="SendTour">派团给计调实体
        ///     2012-09-13 王磊 case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线: 
        /// </param>
        /// <returns></returns>
        public bool SendTour(EyouSoft.Model.TourStructure.MSendTour SendTour)
        {
            if (SendTour.DeptId == 0 || string.IsNullOrEmpty(SendTour.OperatorId) || SendTour.Planer == null || SendTour.Planer.Count == 0)
            {
                return false;
            }
            SendTour.TourCode = GenerateTourNo(SendTour.DeptId, SendTour.CompanyId, SendTour.TourType, SendTour.LDate);
            if (dal.SendTour(SendTour))
            {
                {
                    string JsonString = string.Empty;
                    EyouSoft.Model.TourStructure.MTourTeamInfo MTourTeamInfo = null;
                    EyouSoft.Model.TourStructure.MTourSanPinInfo MTourSanPinInfo = null;
                    EyouSoft.Model.TourStructure.MTourBaseInfo MTourBaseInfo = null;
                    MTourBaseInfo = dal.GetTourInfo(SendTour.TourId);
                    //switch (MTourBaseInfo.TourType)
                    //{
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    //        {
                    //            MTourTeamInfo = (EyouSoft.Model.TourStructure.MTourTeamInfo)MTourBaseInfo;
                    //            JsonString = Newtonsoft.Json.JsonConvert.SerializeObject(MTourTeamInfo);
                    //            break;
                    //        }
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                    //    //2012-09-13 王磊 case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线: 
                    //    case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                    //        {
                    //            MTourSanPinInfo = (EyouSoft.Model.TourStructure.MTourSanPinInfo)MTourBaseInfo;
                    //            JsonString = Newtonsoft.Json.JsonConvert.SerializeObject(MTourSanPinInfo);
                    //            break;
                    //        }
                    //}
                    AddOriginalTourInfo(new EyouSoft.Model.TourStructure.MTourOriginalInfo()
                    {
                        CompanyId = SendTour.CompanyId,
                        TourContent = JsonString,
                        TourId = SendTour.TourId,
                        TourType = MTourBaseInfo.TourType
                    });

                    //添加操作日志
                    System.Text.StringBuilder str = new System.Text.StringBuilder();
                    str.AppendFormat("派团给计调,计划编号:{0}", SendTour.TourId);
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                    return true;
                }
            }
            return false;
        }

        ///// <summary>
        ///// 获得同行分销散拼计划
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="recordCount"></param>
        ///// <param name="TourSanPinSearch"></param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.TourStructure.MTourTongHanInfo> GetTYFXTourSanPinList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSanPinSearch TourSanPinSearch)
        //{
        //    bool isOnlySeft=false;
        //    int[] DeptIds = null;// GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.同业分销_收客计划, out isOnlySeft);
        //    return dal.GetTYFXTourSanPinList(CompanyId, pageSize, pageIndex, ref recordCount, TourSanPinSearch, DeptIds, isOnlySeft, LoginUserId);
        //}

        /// <summary>
        /// 获取计划行程变更列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourPlanChange> GetTourPlanChange(int pageSize, int pageIndex, ref int recordCount,MTourPlanChangeBase mSearch)
        {
            bool isOnlySeft = false;
            int[] DeptIds = null;
            DeptIds = GetDataPrivs(mSearch.SL, out isOnlySeft);
            return dal.GetTourPlanChange(mSearch, pageSize, pageIndex, ref recordCount, DeptIds, isOnlySeft, LoginUserId);
        }

        /// <summary>
        /// 获取计划变更列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourPlanChange GetTourChangeModel(string CompanyId, int Id)
        {
            return dal.GetTourChangeModel(CompanyId, Id);
        }

        /// <summary>
        /// 计划确认变更
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <returns>--返回值：0失败 1成功 2已确认</returns>
        public int TourChangeSure(EyouSoft.Model.TourStructure.MTourPlanChangeConfirm model)
        {
            var r = dal.TourChangeSure(model);
            if (r==1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("计划确认变更,计划编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return r;
        }
        /// <summary>
        /// 业务变更新增/修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回值：0失败 1成功</returns>
        public int TourChangeAddOrUpd(EyouSoft.Model.TourStructure.MTourPlanChange model)
        {
            if (model==null)
            {
                return 0;
            }
            var r = dal.TourChangeAddOrUpd(model);
            if (r == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("新增/修改业务变更,变更编号:{0}", model.Id);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return r;
        }

        ///// <summary>
        ///// 获得组团计调列表
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="recordCount"></param>
        ///// <param name="MPlanListSearch"></param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.TourStructure.MPlanList> GetZTPlanList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MPlanListSearch MPlanListSearch)
        //{
        //    bool isOnlySeft = false;
        //    int[] DeptIds = null;
        //    //DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_组团计调, out isOnlySeft);
        //    return dal.GetPlanList(CompanyId, pageSize, pageIndex, ref recordCount, MPlanListSearch, DeptIds, isOnlySeft, LoginUserId);
        //}

        /// <summary>
        /// 获得地接计调列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MPlanListSearch"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MPlanList> GetDJPlanList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MPlanListSearch MPlanListSearch)
        {
            bool isOnlySeft = false;
            int[] DeptIds = null;
            //DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_地接计调, out isOnlySeft);
            return dal.GetPlanList(CompanyId, pageSize, pageIndex, ref recordCount, MPlanListSearch, DeptIds, isOnlySeft, LoginUserId);
        }

        /// <summary>
        /// 获得出境计调列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MPlanListSearch"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MPlanList> GetCJPlanList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MPlanListSearch MPlanListSearch)
        {
            bool isOnlySeft = false;
            int[] DeptIds = null;
            //DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_出境计调, out isOnlySeft);
            return dal.GetPlanList(CompanyId, pageSize, pageIndex, ref recordCount, MPlanListSearch, DeptIds, isOnlySeft, LoginUserId);
        }

        /// <summary>
        /// 获取导游报账、计调报账、报销报账列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MBZSearch"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MBZInfo> GetBXBZList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MBZSearch MBZSearch)
        {
            //是否仅查看自己的数据
            bool isOnlySelf = false;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] DeptIds = GetDataPrivs(MBZSearch.SL, out  isOnlySelf);
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
            return dal.GetBZList(CompanyId, pageSize, pageIndex, ref recordCount, MBZSearch.Type, DeptIds, isOnlySelf, LoginUserId, MBZSearch, setting);
        }

        /// <summary>
        /// 获得导游报账列表
        /// (控制计调安排时指定的导游)
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MBZSearch"></param>
        /// <param name="DeptId">当前操作人部门编号</param> 
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MBZInfo> GetGuidBZList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, int DeptId)
        {
            //是否仅查看自己的数据
            bool isOnlySelf = false;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.导游中心_导游报账, out  isOnlySelf);
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
            return dal.GetBZList(CompanyId, pageSize, pageIndex, ref recordCount, EyouSoft.Model.EnumType.TourStructure.BZList.导游报账, DeptIds, isOnlySelf, LoginUserId, MBZSearch, setting);
        }

        ///// <summary>
        ///// 获得销售报账列表
        ///// (控制计划销售员（供应商计划审核人）)
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="recordCount"></param>
        ///// <param name="MBZSearch"></param>
        ///// <param name="DeptId">当前操作人部门编号</param>
        ///// <returns></returns>
        //public IList<EyouSoft.Model.TourStructure.MBZInfo> GetSaleBZList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, int DeptId)
        //{
        //    //是否仅查看自己的数据
        //    bool isOnlySelf = false;
        //    //能查看到该菜单下面数据的部门编号，NULL为所有部门
        //    int[] DeptIds = null; //GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.销售中心_销售报账, out  isOnlySelf);
        //    var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
        //    return dal.GetBZList(CompanyId, pageSize, pageIndex, ref recordCount, EyouSoft.Model.EnumType.TourStructure.BZList.销售报账, DeptIds, isOnlySelf, LoginUserId, MBZSearch, setting);
        //}

        /// <summary>
        /// 获得计调报账列表
        /// (控制派团给计调时指定的计调员)
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MBZSearch"></param>
        /// <param name="DeptId">当前操作人部门编号</param> 
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MBZInfo> GetPlanBZList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, int DeptId)
        {
            //是否仅查看自己的数据
            bool isOnlySelf = false;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_计调报账, out  isOnlySelf);
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
            return dal.GetBZList(CompanyId, pageSize, pageIndex, ref recordCount, EyouSoft.Model.EnumType.TourStructure.BZList.计调报账, DeptIds, isOnlySelf, LoginUserId, MBZSearch, setting);
        }

        ///// <summary>
        ///// 获得计调终审列表
        ///// (控制派团给计调时指定的计调员)
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="recordCount"></param>
        ///// <param name="MBZSearch"></param>
        ///// <param name="DeptId">当前操作人部门编号</param> 
        ///// <returns></returns>
        //public IList<EyouSoft.Model.TourStructure.MBZInfo> GetPlanEndList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, int DeptId)
        //{
        //    //是否仅查看自己的数据
        //    bool isOnlySelf = false;
        //    //能查看到该菜单下面数据的部门编号，NULL为所有部门
        //    int[] DeptIds = null; //GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_计调终审, out  isOnlySelf);
        //    var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
        //    return dal.GetBZList(CompanyId, pageSize, pageIndex, ref recordCount, EyouSoft.Model.EnumType.TourStructure.BZList.计调终审, DeptIds, isOnlySelf, LoginUserId, MBZSearch, setting);
        //}

        /// <summary>
        /// 获得财务报账列表
        /// 控制提交账务核算的计调
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MBZSearch"></param>
        /// <param name="DeptId">当前操作人部门编号</param>  
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MBZInfo> GetFincBZList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, int DeptId)
        {
            //是否仅查看自己的数据
            bool isOnlySelf = false;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_报销报账, out  isOnlySelf);
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
            return dal.GetBZList(CompanyId, pageSize, pageIndex, ref recordCount, EyouSoft.Model.EnumType.TourStructure.BZList.报账, DeptIds, isOnlySelf, LoginUserId, MBZSearch, setting);
        }

        /// <summary>
        /// 获得财务报销列表
        /// 控制提交导游报账的导游
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MBZSearch"></param>
        /// <param name="DeptId">当前操作人部门编号</param>  
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MBZInfo> GetFincBXList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, int DeptId)
        {
            //是否仅查看自己的数据
            bool isOnlySelf = false;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_报销报账, out  isOnlySelf);
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
            return dal.GetBZList(CompanyId, pageSize, pageIndex, ref recordCount, EyouSoft.Model.EnumType.TourStructure.BZList.报销, DeptIds, isOnlySelf, LoginUserId, MBZSearch, setting);
        }

        /// <summary>
        /// 获得财务单团核算列表
        /// 控制计调终审人
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MBZSearch"></param>
        /// <param name="DeptId">当前操作人部门编号</param>   
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MBZInfo> GetFinHSList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, int DeptId)
        {
            //是否仅查看自己的数据
            bool isOnlySelf = false;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_单团核算, out  isOnlySelf);
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
            return dal.GetBZList(CompanyId, pageSize, pageIndex, ref recordCount, EyouSoft.Model.EnumType.TourStructure.BZList.单团核算, DeptIds, isOnlySelf, LoginUserId, MBZSearch, setting);
        }

        /// <summary>
        /// 派团时的订单列表
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MSendTourOrderList> GetSendTourOrderList(string TourId)
        {
            return dal.GetSendTourOrderList(TourId);
        }

        /// <summary>
        /// 取消计划
        /// </summary>
        /// <param name="TourIds">计划编号</param>
        /// <param name="CancelReson">取消原因</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool CancelTour(string[] TourIds, string CancelReson, string CompanyId)
        {
            if (TourIds == null || TourIds.Length == 0) return false;
            foreach (string TourId in TourIds)
            {
                if (!string.IsNullOrEmpty(TourId))
                {
                    if (dal.CancelTour(TourId, CancelReson, CompanyId, LoginUserId))
                    {
                        //添加操作日志
                        System.Text.StringBuilder str = new System.Text.StringBuilder();
                        str.AppendFormat("取消了计划,计划编号:{0}", TourId);
                        EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SuccessDelTourIds">成功删除的计划编号列表</param>
        /// <param name="TourIds">计划编号列表</param>
        /// <returns></returns>
        public bool DeleteTour(string CompanyId, string[] TourIds)
        {
            List<string> SuccessDelTourIds = new List<string>();
            dal.DeleteTour(CompanyId, ref SuccessDelTourIds, TourIds);
            if (SuccessDelTourIds.Count > 0)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("删除了计划,计划编号:{0}", String.Join(",", SuccessDelTourIds.ToArray()));
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据计划编号得到行程
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> GetTourPlan(string TourId)
        {
            return dal.GetTourPlan(TourId);
        }

        /// <summary>
        /// 供应商发布的计划审核(成功进同行分销与分销商平台)
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <param name="ShowPublisher">供应商计划在分销商显示的发布人</param> 
        /// <param name="SaleInfo">审核人信息</param> 
        ///<param name="list">价格标准</param> 
        /// <returns></returns>
        public bool Review(string TourId, EyouSoft.Model.EnumType.TourStructure.ShowPublisher ShowPublisher, EyouSoft.Model.TourStructure.MSaleInfo SaleInfo, IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list)
        {
            if (SaleInfo == null || string.IsNullOrEmpty(SaleInfo.SellerId) || string.IsNullOrEmpty(SaleInfo.Name) || SaleInfo.DeptId == 0)
            {
                return false;
            }
            if (dal.Review(TourId, ShowPublisher, SaleInfo, list))
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("计划审核,计划编号:{0},审核人:{1}", TourId, SaleInfo.Name);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 改变计划状态
        /// </summary>
        /// <param name="TourStatusChange">状态实体</param>
        /// <returns></returns>
        public bool UpdateTourStatus(EyouSoft.Model.TourStructure.MTourStatusChange TourStatusChange)
        {
            if (TourStatusChange != null && !string.IsNullOrEmpty(TourStatusChange.TourId) && !string.IsNullOrEmpty(TourStatusChange.CompanyId) &&
                !string.IsNullOrEmpty(TourStatusChange.Operator) && !string.IsNullOrEmpty(TourStatusChange.OperatorId) && TourStatusChange.DeptId != 0 && TourStatusChange.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划)
            {
                if (dal.UpdateTourStatus(TourStatusChange))
                {
                    //添加操作日志
                    System.Text.StringBuilder str = new System.Text.StringBuilder();
                    str.AppendFormat("计划状态变更,计划编号:{0},当前状态:{1}", TourStatusChange.TourId, TourStatusChange.TourStatus);
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 手工设置收客状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool SetHandStatus(string TourId, EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus Status)
        {
            if (dal.SetHandStatus(TourId, Status))
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("手动设置收客状态,计划编号:{0},当前状态:{1}", TourId, Status);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 生成团号
        /// </summary>
        /// <param name="DeptId"></param>
        /// <param name="CompanyId"></param>
        /// <param name="TourType"></param>
        /// <param name="LDate"></param>
        /// <returns></returns>
        public string GenerateTourNo(int DeptId, string CompanyId, EyouSoft.Model.EnumType.TourStructure.TourType TourType, DateTime LDate)
        {
            string TourNoSetting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId).TourNoSetting;
            IList<EyouSoft.Model.ComStructure.MTourNoOptionCode> list = new EyouSoft.BLL.ComStructure.BTourNoOptionCode().GetModel(CompanyId);
            string TourCode = "";

            if (string.IsNullOrEmpty(TourNoSetting))
            {
                TourNoSetting = "35";
                TourCode += LDate.ToString("yyyyMMdd");
                TourCode += dal.GetTourNum(CompanyId).ToString();
            }

            if (list != null && list.Count > 0)
            {
                TourCode = string.Empty;
                for (int i = 0; i < TourNoSetting.Length; i++)
                {
                    switch ((EyouSoft.Model.EnumType.ComStructure.OptionItemType)Convert.ToInt32(TourNoSetting.Substring(i, 1)))
                    {
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称:
                            {
                                TourCode += list.SingleOrDefault(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称).Code;
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称:
                            {
                                var item = list.SingleOrDefault(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称 && p.ItemId == DeptId.ToString());
                                if (item != null)
                                {
                                    TourCode += item.Code;
                                }
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型:
                            {
                                TourCode += list.SingleOrDefault(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型 && p.ItemId == ((int)TourType).ToString()).Code;
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.分隔符:
                            {
                                TourCode += "-";
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.序列号:
                            {
                                TourCode += dal.GetTourNum(CompanyId).ToString();
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期:
                            {
                                EyouSoft.Model.EnumType.ComStructure.OptionItemTypeLDateFormat OptionItemTypeLDateFormat = (EyouSoft.Model.EnumType.ComStructure.OptionItemTypeLDateFormat)Convert.ToInt32(list.SingleOrDefault(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期).Code);
                                if (OptionItemTypeLDateFormat == EyouSoft.Model.EnumType.ComStructure.OptionItemTypeLDateFormat.格式1)
                                {
                                    TourCode += LDate.ToString("yyyyMMdd");
                                }
                                else
                                {
                                    TourCode += LDate.ToString("yyyy-MM-dd");
                                }
                                break;
                            }
                    }
                }
            }

            if (string.IsNullOrEmpty(TourCode))
            {
                TourCode += LDate.ToString("yyyyMMdd");
                TourCode += dal.GetTourNum(CompanyId).ToString();
            }

            return TourCode;
        }

        /// <summary>
        /// 报销报账报销
        /// </summary>
        /// <param name="approverDeptId">报销完成人员部门编号</param>
        /// <param name="approverId">报销完成人员编号</param>
        /// <param name="approver">报销完成人员名</param>
        /// <param name="TourId">计划编号</param>
        /// <param name="CompanyId">系统公司编号</param>
        /// <returns></returns>
        public bool Apply(int approverDeptId, string approverId, string approver, string TourId, string CompanyId)
        {
            if (dal.Apply(approverDeptId, approverId, approver, TourId, CompanyId))
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("报销报账完成,计划编号:{0}", TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得计划弹出信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourBaoInfo GetTourBaoInfo(string TourId)
        {
            return dal.GetTourBaoInfo(TourId);
        }

        /// <summary>
        /// 获取关键字计划数
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MKeyTour> GetKeyTour(string CompanyId)
        {
            return dal.GetKeyTour(CompanyId);
        }

        /// <summary>
        /// 获取线路区域计划数
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MAreaTour> GetAreaTour(string CompanyId)
        {
            return dal.GetAreaTour(CompanyId);
        }

        /// <summary>
        /// 资源预控团号选择
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="serach"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MControlTour> GetControlTourList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MControlTourSearch serach)
        {
            return dal.GetControlTourList(CompanyId, pageSize, pageIndex, ref recordCount, serach);
        }

        /// <summary>
        /// 获得散拼价格
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourPriceStandard> GetTourSanPinPrice(string TourId)
        {
            return dal.GetTourSanPinPrice(TourId);
        }

        /// <summary>
        /// 根据计划编号获得所有游客
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTourTravellerList(string TourId)
        {
            return dal.GetTourTravellerList(TourId);
        }

        /// <summary>
        /// 得到计划发布人信息
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="OperatorId"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MPublisherInfo GetPublisherInfo(EyouSoft.Model.EnumType.ComStructure.UserType UserType, string OperatorId)
        {
            return dal.GetPublisherInfo(UserType, OperatorId);
        }

        /// <summary>
        /// 获得供应商发布的价格
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MSupplierPublishPrice GetSupplyPrice(string TourId)
        {
            return dal.GetSupplyPrice(TourId);
        }

        /// <summary>
        /// 获取计划状态
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus GetTourStatus(string CompanyId, string TourId)
        {
            return dal.GetTourStatus(CompanyId, TourId);
        }

        /// <summary>
        /// 添加计划原始信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddOriginalTourInfo(EyouSoft.Model.TourStructure.MTourOriginalInfo model)
        {
            if (!string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.TourId) && !string.IsNullOrEmpty(model.TourContent))
            {
                if (dal.AddOriginalTourInfo(model))
                {
                    //添加操作日志
                    System.Text.StringBuilder str = new System.Text.StringBuilder();
                    str.AppendFormat("添加计划原始信息,计划编号:{0}", model.TourId);
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 获得订单利润分配订单列表
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourOrderDisInfo> GetTourOrderDisList(string TourId)
        {
            return dal.GetTourOrderDisList(TourId);
        }

        /// <summary>
        /// 得到发布人信息
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <param name="OperatorId">操作人编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MPersonInfo GetPersonInfo(string SourceId, string OperatorId)
        {
            return dal.GetPersonInfo(SourceId, OperatorId);
        }

        /// <summary>
        /// 得到计划的成本确认状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public bool GetCostStatus(string TourId)
        {
            return dal.GetCostStatus(TourId);
        }

        /// <summary>
        /// 根据计划编号获得计划类型
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.TourStructure.TourType GetTourType(string TourId)
        {
            return dal.GetTourType(TourId);
        }

        /// <summary>
        /// 得到计划合同金额确认状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public bool GetConfirmMoneyStatus(string TourId)
        {
            return dal.GetConfirmMoneyStatus(TourId);
        }

        /// <summary>
        /// 获取计划价格备注信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public string GetTourJiaGeBeiZhu(string tourId)
        {
            if (string.IsNullOrEmpty(tourId)) return string.Empty;
            return dal.GetTourJiaGeBeiZhu(tourId);
        }

        /// <summary>
        /// 添加垫付申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns>0:失败，1：成功，2：销售员或客户单位不存在欠款</returns>
        public int AddAdvanceApp(EyouSoft.Model.TourStructure.MAdvanceApp model)
        {
            model.DisburseId = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(model.CompanyId) || string.IsNullOrEmpty(model.ItemId) || string.IsNullOrEmpty(model.ApplierId) || model.DeptId == 0 || string.IsNullOrEmpty(model.OperatorId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            int flg = dal.AddAdvanceApp(model);
            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("添加垫付申请信息,垫付申请编号:{0}", model.DisburseId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());

            }
            return flg;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 判断数据是否完整
        /// </summary>
        /// <param name="o"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool Verification(object o, EyouSoft.Model.EnumType.TourStructure.TourType type)
        {
            //if (type == EyouSoft.Model.EnumType.TourStructure.TourType.组团团队)
            //{
            //    EyouSoft.Model.TourStructure.MTourTeamInfo info = (EyouSoft.Model.TourStructure.MTourTeamInfo)o;
            //    if (string.IsNullOrEmpty(info.RouteId) || string.IsNullOrEmpty(info.RouteName) || info.TourDays == 0 || info.LDate == null || string.IsNullOrEmpty(info.BuyCompanyID) || string.IsNullOrEmpty(info.CompanyInfo.CompanyName) || string.IsNullOrEmpty(info.SaleInfo.SellerId) || string.IsNullOrEmpty(info.SaleInfo.Name) || info.Adults == 0)
            //    {
            //        return false;
            //    }
            //}
            //else if (type == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼)
            //{
            //    return true;
            //}

            return true;
        }

        #endregion


        #region --2012-08-20 短线功能添加的方法-----------------------------------------
        /// <summary>
        /// 获取计划预设车型
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourCarType> GetTourCarType(string TourId)
        {
            if (string.IsNullOrEmpty(TourId)) return null;
            return dal.GetTourCarType(TourId);
        }

        /// <summary>
        /// 获取计划上车地点
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourCarLocation> GetTourCarLocation(string TourId)
        {
            if (string.IsNullOrEmpty(TourId)) return null;
            return dal.GetTourCarLocation(TourId);
        }

        /// <summary>
        /// 修改散拼短线的预设车型
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateTourCarType(string TourId, string Operator, string OperatorId, IList<EyouSoft.Model.TourStructure.MTourCarType> list)
        {
            if (string.IsNullOrEmpty(TourId)) return false;

            bool flg = dal.UpdateTourCarType(TourId, Operator, OperatorId, list);
            if (flg)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("修改计划车型,计划编号:{0}", TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 将车型、座次变更的信息变为已读状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsReadCarTypeSeatChange(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return false;
            bool flg = dal.IsReadCarTypeSeatChange(Id);
            if (flg)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("车型、座次变更变为已读。变更编号:{0}", Id);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }


        /// <summary>
        /// 获取分销商订单座次变更的提示消息
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="SourceId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MCarTypeSeatChange> GetCarTypeSeatChangeList(string CompanyId, string SourceId, int top, EyouSoft.Model.EnumType.TourStructure.CarChangeType? CarChangeType)
        {
            if (string.IsNullOrEmpty(CompanyId) || string.IsNullOrEmpty(SourceId)) return null;
            return dal.GetCarTypeSeatChangeList(CompanyId, SourceId, top, CarChangeType);
        }

        #endregion

        /// <summary>
        /// 统计分析-状态查询表：自行设定计划状态，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SetTourStatus(EyouSoft.Model.TourStructure.MSetTourStatusInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.TourId) 
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            int dalRetCode = dal.SetTourStatus(info);

            if (dalRetCode == 1)
            {
                string s = info.Status.ToString();

                //if (info.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.单项服务)
                //{
                //    switch (info.Status)
                //    {
                //        case EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划: s = "操作中"; break;
                //        case EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕: s = "已落实"; break;
                //        default: break;
                //    }
                //}

                SysStructure.BSysLogHandle.Insert("设置计划状态，计划编号：" + info.TourId + "，状态：" + s + "。（H）。");
            }

            return dalRetCode;
        }
    }
}
