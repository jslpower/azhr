using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HGysStructure
{
    /// <summary>
    /// 物品管理相关业务逻辑类
    /// </summary>
    public class BWuPin
    {
        readonly EyouSoft.IDAL.HGysStructure.IWuPin dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HGysStructure.IWuPin>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BWuPin() { }
        #endregion

        #region public members
        /// <summary>
        /// 新增物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.HGysStructure.MWuPinInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId)) return 0;
            info.WuPinId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.Insert(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增物品信息，编号：" + info.WuPinId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.HGysStructure.MWuPinInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                ||string.IsNullOrEmpty(info.WuPinId)) return 0;

            var shuLiangInfo = GetShuLiangInfo(info.WuPinId, null);
            if (info.ShuLiangRK < shuLiangInfo.LingYong + shuLiangInfo.FaFang + shuLiangInfo.JieYue1) return -1;

            int dalRetCode = dal.Update(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改物品信息，编号：" + info.WuPinId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="wuPinId">物品编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string wuPinId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(wuPinId)) return 0;
            var shuLiangInfo = GetShuLiangInfo(wuPinId, null);

            if (shuLiangInfo.LingYong + shuLiangInfo.FaFang + shuLiangInfo.JieYue1 + shuLiangInfo.JieYue2 > 0) return -1;

            int dalRetCode = dal.Delete(companyId, wuPinId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除物品信息，编号：" + wuPinId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取物品信息实体
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MWuPinInfo GetInfo(string wuPinId)
        {
            if (string.IsNullOrEmpty(wuPinId)) return null;

            var info = dal.GetInfo(wuPinId);

            if (info != null) info.ShuLiang = GetShuLiangInfo(wuPinId, null);

            return info;
        }

        /// <summary>
        /// 获取物品信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MWuPinInfo> GetWuPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MWuPinChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            var items = dal.GetWuPins(companyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.ShuLiang = GetShuLiangInfo(item.WuPinId, null);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取物品数量信息
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="lingYongId">不含的领用编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MWuPinShuLiangInfo GetShuLiangInfo(string wuPinId, string lingYongId)
        {
            if (string.IsNullOrEmpty(wuPinId)) return new EyouSoft.Model.HGysStructure.MWuPinShuLiangInfo();

            var info = dal.GetShuLiangInfo(wuPinId, lingYongId);

            if (info == null) return new EyouSoft.Model.HGysStructure.MWuPinShuLiangInfo();

            return info;
        }

        /// <summary>
        /// 新增领用信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertLingYong(EyouSoft.Model.HGysStructure.MWuPinLingYongInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.WuPinId)
                ||string.IsNullOrEmpty(info.LingYongRenId)) return 0;

            var shuLiangInfo = GetShuLiangInfo(info.WuPinId, null);

            if (shuLiangInfo.KuCun < info.ShuLiang) return -1;

            info.LingYongId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertLingYong(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增" + info.LingYongLeiXing + "，编号：" + info.LingYongId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取领用信息集合
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="leiXing">领用类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MWuPinLingYongLBInfo> GetLingYongs(string wuPinId, EyouSoft.Model.EnumType.GysStructure.WuPinLingYongLeiXing leiXing)
        {
            if (string.IsNullOrEmpty(wuPinId)) return null;

            var items = dal.GetLingYongs(wuPinId, leiXing);

            return items;
        }

        /// <summary>
        /// 物品归还，返回1成功，其它失败
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="lingYongId">领用编号</param>
        /// <param name="guiHuanOperatorId">归还操作人编号</param>
        /// <returns></returns>
        public int GuiHuan(string wuPinId, string lingYongId, string guiHuanOperatorId)
        {
            if (string.IsNullOrEmpty(wuPinId) 
                || string.IsNullOrEmpty(lingYongId) 
                || string.IsNullOrEmpty(guiHuanOperatorId)) return 0;

            int dalRetCode = dal.GuiHuan(wuPinId, lingYongId, guiHuanOperatorId, DateTime.Now);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("物品归还，归还编号：" + lingYongId + "，物品编号：" + wuPinId);
            }

            return dalRetCode;
        }
        #endregion
    }
}
