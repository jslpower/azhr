using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.BLL.SysStructure
{
    /// <summary>
    /// 导游须知、行程备注、报价备注、行程亮点、线路区域操作
    /// </summary>
    public class BSysOptionConfig
    {
        private readonly EyouSoft.IDAL.SysStructure.ISysOptionConfig dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SysStructure.ISysOptionConfig>();

        #region 导游需知
        /// <summary>
        /// 添加导游须知
        /// </summary>
        /// <param name="info">导游须知</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddGuideKnow(MSysGuideKonw info)
        {
            if (string.IsNullOrEmpty(info.DepartId.ToString())) return 0;
            return dal.AddGuideKnow(info);
        }
        /// <summary>
        /// 修改导游须知
        /// </summary>
        /// <param name="info">导游须知</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateGuideKnow(MSysGuideKonw info)
        {
            return dal.UpdateGuideKnow(info);
        }
        /// <summary>
        /// 删除导游须知
        /// </summary>
        /// <param name="id">导游须知</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteGuideKnow(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return 0;
            return dal.DeleteGuideKnow(id);
        }
        /// <summary>
        /// 获取导游须知列表
        /// </summary>
        /// <returns></returns>
        public IList<MSysGuideKonw> GetGuideKnowList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysGuideKonwSearch searchInfo)
        {
            return dal.GetGuideKnowList(companyId, pageSize, pageIndex, ref recordCount, searchInfo);
        }
        #endregion

        #region 行程备注
        /// <summary>
        /// 判断是否存在行程备注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        public bool isEsistsMSysJourneyMark(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            return dal.isEsistsMSysJourneyMark(id, lngType);
        }
        /// <summary>
        /// 添加行程备注
        /// </summary>
        /// <param name="info">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddMSysJourneyMark(MSysJourneyMark info)
        {
            //info.MasterId = Guid.NewGuid().ToString();
            return dal.AddMSysJourneyMark(info);

        }
        /// <summary>
        /// 修改行程备注
        /// </summary>
        /// <param name="info">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateMSysJourneyMark(MSysJourneyMark info)
        {
            //if (string.IsNullOrEmpty(info.Id.ToString()) || string.IsNullOrEmpty(info.MasterId)) return 0;
            return dal.UpdateMSysJourneyMark(info);

        }
        /// <summary>
        /// 删除行程备注
        /// </summary>
        /// <param name="id">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteMSysJourneyMark(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return 0;
            return dal.DeleteMSysJourneyMark(id);
        }
        /// <summary>
        /// 获取行程备注列表
        /// </summary>
        /// <returns></returns>
        public IList<MSysJourneyMark> GetMSysJourneyMarkList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysJourneyMarkSearch searchInfo)
        {
            return dal.GetJourneyMarkList(companyId, pageSize, pageIndex, ref recordCount, searchInfo);
        }

        #endregion

        #region 报价备注

        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="lngType">语言类型</param>
        /// <returns></returns>
        public bool isEsistsMBackPriceMark(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            return dal.isEsistsMBackPriceMark(id, lngType);
        }
        /// <summary>
        /// 添加报价备注
        /// </summary>
        /// <param name="info">报价备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddMBackPriceMark(MBackPriceMark info)
        {
            return dal.AddMBackPriceMark(info);
        }
        /// <summary>
        /// 修改报价备注
        /// </summary>
        /// <param name="info">报价备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateMBackPriceMark(MBackPriceMark info)
        {
            if (string.IsNullOrEmpty(info.Id.ToString())) return 0;
            return dal.UpdateMBackPriceMark(info);
        }
        /// <summary>
        /// 删除报价备注
        /// </summary>
        /// <param name="id">报价备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteMBackPriceMark(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return 0;
            return dal.DeleteMBackPriceMark(id);
        }
        /// <summary>
        /// 获取报价备注列表
        /// </summary>
        /// <returns></returns>
        public IList<MBackPriceMark> GetMBackPriceMarkList(string companyId, int pageSize, int pageIndex, ref int recordCount, MBackPriceMarkSearch searchInfo)
        {
            return dal.GetMBackPriceMarkList(companyId, pageSize, pageIndex, ref recordCount, searchInfo);
        }


        /// <summary>
        /// 获取报价备注列表(用于打印单)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<MBackPriceMark> GetMBackPriceMarkList(int[] ids, EyouSoft.Model.EnumType.SysStructure.LngType LngType)
        {
            if (ids == null || ids.Length <= 0) return null;
            return dal.GetMBackPriceMarkList(ids, LngType);
        }
        #endregion

        #region 行程亮点
        /// <summary>
        /// 是否存在行程亮点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        public bool isEsistsJourneyLight(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            return dal.isEsistsJourneyLight(id, lngType);
        }
        /// <summary>
        /// 添加行程亮点
        /// </summary>
        /// <param name="info">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddJourneyLight(MSysJourneyLight info)
        {
            return dal.AddJourneyLight(info);
        }
        /// <summary>
        /// 修改行程亮点
        /// </summary>
        /// <param name="info">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateJourneyLight(MSysJourneyLight info)
        {
            if (string.IsNullOrEmpty(info.Id.ToString())) return 0;
            return dal.UpdateJourneyLight(info);
        }
        /// <summary>
        /// 删除行程亮点
        /// </summary>
        /// <param name="id">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteJourneyLight(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return 0;
            return dal.DeleteJourneyLight(id);
        }
        /// <summary>
        /// 获取行程亮点列表
        /// </summary>
        /// <returns></returns>
        public IList<MSysJourneyLight> GetJourneyLightList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysJourneyLightSearch searchInfo)
        {
            return dal.GetJourneyLightList(companyId, pageSize, pageIndex, ref recordCount, searchInfo);
        }

        /// <summary>
        /// 获取行程亮点列表(用于打印单)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        public IList<MSysJourneyLight> GetJourneyLightList(int[] ids, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            if (ids == null || ids.Length <= 0 || lngType.ToString() == "") return null;
            return dal.GetJourneyLightList(ids, lngType);
        }
        #endregion

        #region 线路区域
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="info">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddRoute(MSysRoute info)
        {
            return dal.AddRoute(info);
        }
        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="info">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateRoute(MSysRoute info)
        {
            if (string.IsNullOrEmpty(info.AreaId.ToString())) return 0;
            return dal.UpdateRoute(info);
        }
        /// <summary>
        /// 删除线路区域
        /// </summary>
        /// <param name="id">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteRoute(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return 0;
            return dal.DeleteRoute(id);
        }
        /// <summary>
        /// 获取线路区域列表
        /// </summary>
        /// <returns></returns>
        public IList<MSysRoute> GetRouteList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysRouteSearch searchInfo)
        {
            return dal.GetRouteList(companyId, pageSize, pageIndex, ref recordCount, searchInfo);
        }
        #endregion



        #region 获取实体

        /// <summary>
        /// 获取导游须知
        /// </summary>
        /// <param name="id">获取导游须知编号</param>
        /// <returns></returns>
        public MSysGuideKonw GetGuideKnow(int id)
        {
            if (id == 0) return null;
            return dal.GetGuideKnow(id);
        }

        /// <summary>
        /// 获取行程备注
        /// </summary>
        /// <param name="id">行程备注编号</param>
        /// <returns></returns>
        public MSysJourneyMark GetJourneyMark(int id)
        {
            if (id == 0) return null;
            return dal.GetJourneyMark(id);
        }
        /// <summary>
        /// 获取报价备注
        /// </summary>
        /// <param name="id">报价备注编号</param>
        /// <returns></returns>
        public MBackPriceMark GetBackPriceMark(int id)
        {
            if (id == 0) return null;
            return dal.GetBackPriceMark(id);
        }
        /// <summary>
        /// 获取行程亮点
        /// </summary>
        /// <param name="id">行程亮点编号</param>
        /// <returns></returns>
        public MSysJourneyLight GetJourneyLight(int id)
        {
            if (id == 0) return null;
            return dal.GetJourneyLight(id);
        }
        /// <summary>
        /// 获取行程亮点(多个)
        /// </summary>
        /// <param name="Ids">行程亮点编号(用,分隔)</param>
        /// <returns></returns>
        public string GetJourneyLight(string Ids)
        {
            if (String.IsNullOrEmpty(Ids)) return null;
            return dal.GetJourneyLight(Ids);
        }
        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <param name="id">线路区域编号</param>
        /// <returns></returns>
        public MSysRoute GetRoute(int id)
        {
            if (id == 0) return null;
            return dal.GetRoute(id);
        }

        #endregion


    }
}
