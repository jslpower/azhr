using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HGysStructure
{
    /// <summary>
    /// 物品管理相关数据访问类接口
    /// </summary>
    public interface IWuPin
    {
        /// <summary>
        /// 新增物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Insert(EyouSoft.Model.HGysStructure.MWuPinInfo info);
        /// <summary>
        /// 修改物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Update(EyouSoft.Model.HGysStructure.MWuPinInfo info);
        /// <summary>
        /// 删除物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="wuPinId">物品编号</param>
        /// <returns></returns>
        int Delete(string companyId, string wuPinId);
        /// <summary>
        /// 获取物品信息实体
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MWuPinInfo GetInfo(string wuPinId);
        /// <summary>
        /// 获取物品信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MWuPinInfo> GetWuPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MWuPinChaXunInfo chaXun);
        /// <summary>
        /// 获取物品数量信息
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="lingYongId">不含的领用编号</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MWuPinShuLiangInfo GetShuLiangInfo(string wuPinId, string lingYongId);
        /// <summary>
        /// 新增领用信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertLingYong(EyouSoft.Model.HGysStructure.MWuPinLingYongInfo info);
        /// <summary>
        /// 获取领用信息集合
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="leiXing">领用类型</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MWuPinLingYongLBInfo> GetLingYongs(string wuPinId, EyouSoft.Model.EnumType.GysStructure.WuPinLingYongLeiXing leiXing);
        /// <summary>
        /// 物品归还，返回1成功，其它失败
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="lingYongId">领用编号</param>
        /// <param name="guiHuanOperatorId">归还操作人编号</param>
        /// <param name="guiHuanTime">归还时间</param>
        /// <returns></returns>
        int GuiHuan(string wuPinId, string lingYongId, string guiHuanOperatorId, DateTime guiHuanTime);
    }
}
