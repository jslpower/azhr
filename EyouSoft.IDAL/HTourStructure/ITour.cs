using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EyouSoft.IDAL.HTourStructure
{
    public interface ITour
    {
        /// <summary>
        /// 验证是否存在相同的团号(特价产品)
        /// </summary>
        /// <param name="TourCode">团号</param>
        /// <param name="QuoteId">计划编号</param>
        /// <returns></returns>
        bool isExist(string TourCode, string TourId);


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HTourStructure.MTour GetPaituanModel(string TourId);
        /// <summary> 
        /// 添加散拼计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddSanPin(EyouSoft.Model.HTourStructure.MTour model);
        /// <summary>
        /// 获取散拼实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HTourStructure.MTour GetSanPinModel(string TourId, bool? isparent, DateTime? ldate);

        /// <summary>
        /// 修改散拼计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpdateSanPin(EyouSoft.Model.HTourStructure.MTour model);

        /// <summary>
        /// 添加团队计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:添加成功 0：添加失败</returns>
        int AddTour(EyouSoft.Model.HTourStructure.MTour model);

        /// <summary>
        /// 修改团队计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:修改成功 0：修改失败</returns>
        int UpdateTour(EyouSoft.Model.HTourStructure.MTour model);

        /// <summary>
        /// 修改团队状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:修改成功 0：修改失败</returns>
        int UpdateTourStatus(EyouSoft.Model.HTourStructure.MTourStatusChange model);

        /// <summary>
        /// 修改团态标识、销售标识
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        int UpdateTourMark(EyouSoft.Model.ComStructure.MGuidePlanWork m);

        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SuccessDelTourIds">成功删除的计划编号列表</param>
        /// <param name="TourIds">计划编号列表</param>
        /// <returns></returns>
        void DeleteTour(string CompanyId, ref List<string> SuccessDelTourIds, string[] TourIds);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HTourStructure.MTour GetTourModel(string TourIds);

        EyouSoft.Model.HTourStructure.MTour GetRouteInfoByTourId(string TourId);


        /// <summary>
        /// 获取计划数量
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        string GetTourNum(string CompanyId);

        /// <summary>
        /// 获取计划数量
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="crmId">客户ID</param>
        /// <param name="ldate">出团时间</param>
        /// <param name="format">序列号格式化类型</param>
        /// <returns></returns>
        string GetTourNum(string companyId, string crmId, DateTime? ldate, EyouSoft.Model.EnumType.ComStructure.OptionItemTypeSeriesFormat format);

        /// <summary>
        /// 获取派团给计调的信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        EyouSoft.Model.HTourStructure.MTourToPlaner GetTourToPlaner(string TourId);


        /// <summary>
        /// 派团给计调
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-1：计划已派团给计调  0:失败 1：成功</returns>
        int SendTourToPlaner(EyouSoft.Model.HTourStructure.MTourToPlaner model);


        /// <summary>
        /// 团队确认
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-1：该计划已确认  0:失败 1：成功</returns>
        int TourSure(EyouSoft.Model.HTourStructure.MTourSure model);

        /// <summary>
        /// 获取计划列表(团队产品、特价产品)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HTourStructure.MTourInfo> GetTourInfoList(
           int pageSize,
           int pageIndex,
           ref int recordCount,
           EyouSoft.Model.HTourStructure.MTourSearch search);      
        /// <summary>
        /// 获取计划(团队产品、特价产品)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
         EyouSoft.Model.HTourStructure.MTourInfo GetTourInfoModel(string TourId);
        
    }
}
