using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 导游须知、行程备注、报价备注、行程亮点、线路区域成员
    /// </summary>
    public interface ISysOptionConfig
    {
        #region 导游需知
        /// <summary>
        /// 添加导游须知
        /// </summary>
        /// <param name="info">导游须知</param>
        /// <returns>1:成功、0:失败</returns>
        int AddGuideKnow(MSysGuideKonw info);
        /// <summary>
        /// 修改导游须知
        /// </summary>
        /// <param name="info">导游须知</param>
        /// <returns>1:成功、0:失败</returns>
        int UpdateGuideKnow(MSysGuideKonw info);
        /// <summary>
        /// 删除导游须知
        /// </summary>
        /// <param name="info">导游须知</param>
        /// <returns>1:成功、0:失败</returns>
        int DeleteGuideKnow(int id);
        /// <summary>
        ///获取导游需知
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        MSysGuideKonw GetGuideKnow(int id);
        /// <summary>
        /// 获取导游须知列表
        /// </summary>
        /// <returns></returns>
        IList<MSysGuideKonw> GetGuideKnowList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysGuideKonwSearch searchInfo);
        #endregion

        #region 行程备注
        /// <summary>
        /// 是否存在报价备注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        bool isEsistsMSysJourneyMark(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType);
        /// <summary>
        /// 添加行程备注
        /// </summary>
        /// <param name="info">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        int AddMSysJourneyMark(MSysJourneyMark info);
        /// <summary>
        /// 修改行程备注
        /// </summary>
        /// <param name="info">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        int UpdateMSysJourneyMark(MSysJourneyMark info);
        /// <summary>
        /// 删除行程备注
        /// </summary>
        /// <param name="info">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        int DeleteMSysJourneyMark(int id);
        /// <summary>
        ///获取行程备注
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        MSysJourneyMark GetJourneyMark(int id);
        /// <summary>
        /// 获取行程备注列表
        /// </summary>
        /// <returns></returns>
        IList<MSysJourneyMark> GetJourneyMarkList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysJourneyMarkSearch searchInfo);

        #endregion

        #region 报价备注
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        bool isEsistsMBackPriceMark(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType);
        /// <summary>
        /// 添加报价备注
        /// </summary>
        /// <param name="info">报价备注</param>
        /// <returns>1:成功、0:失败</returns>
        int AddMBackPriceMark(MBackPriceMark info);
        /// <summary>
        /// 修改报价备注
        /// </summary>
        /// <param name="info">报价备注</param>
        /// <returns>1:成功、0:失败</returns>
        int UpdateMBackPriceMark(MBackPriceMark info);
        /// <summary>
        /// 删除报价备注
        /// </summary>
        /// <param name="info">报价备注</param>
        /// <returns>1:成功、0:失败</returns>
        int DeleteMBackPriceMark(int id);
        /// <summary>
        ///获取报价备注
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        MBackPriceMark GetBackPriceMark(int id);
        /// <summary>
        /// 获取报价备注列表
        /// </summary>
        /// <returns></returns>
        IList<MBackPriceMark> GetMBackPriceMarkList(string companyId, int pageSize, int pageIndex, ref int recordCount, MBackPriceMarkSearch searchInfo);

        /// <summary>
        /// 获取报价备注列表(用于打印单)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        IList<MBackPriceMark> GetMBackPriceMarkList(int[] ids, EyouSoft.Model.EnumType.SysStructure.LngType LngType);
        #endregion

        #region 行程亮点
        /// <summary>
        /// 是否存在行程亮点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        bool isEsistsJourneyLight(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType);
        /// <summary>
        /// 添加行程亮点
        /// </summary>
        /// <param name="info">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        int AddJourneyLight(MSysJourneyLight info);
        /// <summary>
        /// 修改行程亮点
        /// </summary>
        /// <param name="info">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        int UpdateJourneyLight(MSysJourneyLight info);
        /// <summary>
        /// 删除行程亮点
        /// </summary>
        /// <param name="info">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        int DeleteJourneyLight(int id);
        /// <summary>
        ///获取行程亮点
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        MSysJourneyLight GetJourneyLight(int id);

        /// <summary>
        /// 获取行程亮点(多个)
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        string GetJourneyLight(string Ids);
        /// <summary>
        /// 获取行程亮点列表
        /// </summary>
        /// <returns></returns>
        IList<MSysJourneyLight> GetJourneyLightList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysJourneyLightSearch searchInfo);

        /// <summary>
        /// 获取行程亮点列表(用于打印单)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        IList<MSysJourneyLight> GetJourneyLightList(int[] ids, EyouSoft.Model.EnumType.SysStructure.LngType lngType);
        #endregion

        #region 线路区域
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="info">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        int AddRoute(MSysRoute info);
        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="info">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        int UpdateRoute(MSysRoute info);
        /// <summary>
        /// 删除线路区域
        /// </summary>
        /// <param name="info">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        int DeleteRoute(int id);
        /// <summary>
        ///获取线路区域
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        MSysRoute GetRoute(int id);
        /// <summary>
        /// 获取线路区域列表
        /// </summary>
        /// <returns></returns>
        IList<MSysRoute> GetRouteList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysRouteSearch searchInfo);
        #endregion

    }
}
