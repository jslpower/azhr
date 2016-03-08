using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HGysStructure
{
    /// <summary>
    /// 供应商相关业务逻辑类
    /// </summary>
    public class BGys : BLLBase
    {
        readonly EyouSoft.IDAL.HGysStructure.IGys dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HGysStructure.IGys>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BGys() { }
        #endregion

        #region public members
        /// <summary>
        /// 写入供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.HGysStructure.MGysInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId))
                return 0;

            if (string.IsNullOrEmpty(info.GysId)) info.GysId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            if (info.CheXings != null && info.CheXings.Count > 0)
            {
                foreach (var item in info.CheXings)
                {
                    if (string.IsNullOrEmpty(item.CheXingId)) item.CheXingId = Guid.NewGuid().ToString();
                }
            }

            if (info.GouWu != null && info.GouWu.ChanPins != null && info.GouWu.ChanPins.Count > 0)
            {
                foreach (var item in info.GouWu.ChanPins)
                {
                    if (string.IsNullOrEmpty(item.ChanPinId)) item.ChanPinId = Guid.NewGuid().ToString();
                }
            }

            if (info.GouWu != null && info.GouWu.HeTongs != null && info.GouWu.HeTongs.Count > 0)
            {
                foreach (var item in info.GouWu.HeTongs)
                {
                    if (string.IsNullOrEmpty(item.HeTongId)) item.HeTongId = Guid.NewGuid().ToString();
                }
            }

            if (info.JingDians != null && info.JingDians.Count > 0)
            {
                foreach (var item in info.JingDians)
                {
                    if (string.IsNullOrEmpty(item.JingDianId)) item.JingDianId = Guid.NewGuid().ToString();
                }
            }

            if (info.HeTongs != null && info.HeTongs.Count > 0)
            {
                foreach (var item in info.HeTongs)
                {
                    if (string.IsNullOrEmpty(item.HeTongId)) item.HeTongId = Guid.NewGuid().ToString();
                }
            }

            if (info.Lxrs != null && info.Lxrs.Count > 0)
            {
                foreach (var item in info.Lxrs)
                {
                    if (string.IsNullOrEmpty(item.LxrId)) item.LxrId = Guid.NewGuid().ToString();
                }
            }

            int dalRetCode = dal.Insert(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("添加供应商，编号：" + info.GysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MGysInfo GetInfo(string gysId)
        {
            return GetInfo(gysId, EyouSoft.Model.EnumType.SysStructure.LngType.中文);
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="lng">语言类型</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MGysInfo GetInfo(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType lng)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetInfo(gysId, lng);
        }

        /// <summary>
        /// 更新供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.HGysStructure.MGysInfo info)
        {
            if (info == null
               || string.IsNullOrEmpty(info.GysId)
               || string.IsNullOrEmpty(info.CompanyId)
               || string.IsNullOrEmpty(info.OperatorId))
                return 0;

            //if (!dal.IsExistsLng(info.GysId, info.LngType)) return Insert(info);

            info.IssueTime = DateTime.Now;

            if (info.CheXings != null && info.CheXings.Count > 0)
            {
                foreach (var item in info.CheXings)
                {
                    if (string.IsNullOrEmpty(item.CheXingId)) item.CheXingId = Guid.NewGuid().ToString();
                }
            }

            if (info.GouWu != null && info.GouWu.ChanPins != null && info.GouWu.ChanPins.Count > 0)
            {
                foreach (var item in info.GouWu.ChanPins)
                {
                    if (string.IsNullOrEmpty(item.ChanPinId)) item.ChanPinId = Guid.NewGuid().ToString();
                }
            }

            if (info.GouWu != null && info.GouWu.HeTongs != null && info.GouWu.HeTongs.Count > 0)
            {
                foreach (var item in info.GouWu.HeTongs)
                {
                    if (string.IsNullOrEmpty(item.HeTongId)) item.HeTongId = Guid.NewGuid().ToString();
                }
            }

            if (info.JingDians != null && info.JingDians.Count > 0)
            {
                foreach (var item in info.JingDians)
                {
                    if (string.IsNullOrEmpty(item.JingDianId)) item.JingDianId = Guid.NewGuid().ToString();
                }
            }

            if (info.HeTongs != null && info.HeTongs.Count > 0)
            {
                foreach (var item in info.HeTongs)
                {
                    if (string.IsNullOrEmpty(item.HeTongId)) item.HeTongId = Guid.NewGuid().ToString();
                }
            }

            if (info.Lxrs != null && info.Lxrs.Count > 0)
            {
                foreach (var item in info.Lxrs)
                {
                    if (string.IsNullOrEmpty(item.LxrId)) item.LxrId = Guid.NewGuid().ToString();
                }
            }

            int dalRetCode = dal.Update(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改供应商，编号：" + info.GysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string gysId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(gysId)) return 0;

            int dalRetCode = dal.Delete(companyId, gysId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除供应商，编号：" + gysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取供应商信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MLBInfo> GetGyss(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)
                || chaXun == null) return null;

            var items = dal.GetGyss(companyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.SysStructure.BGeography();
                var jiagebll = new EyouSoft.BLL.HGysStructure.BJiaGe();

                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);

                    if (item.LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.餐馆)
                    {
                        item.CanGuanCaiDans = jiagebll.GetCanGuanCaiDans(item.GysId);
                    }
                }
                citybll = null;
                jiagebll = null;
            }

            return items;
        }

        /// <summary>
        /// 获取选用的供应商信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> GetXuanYongs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            var items = dal.GetXuanYongs(companyId, pageSize, pageIndex, ref recordCount, chaXun);

            return items;
        }

        /// <summary>
        /// 获取选用的景点信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MXuanYongJingDianInfo> GetXuanYongJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            var items = dal.GetXuanYongJingDians(companyId, pageSize, pageIndex, ref recordCount, chaXun);

            return items;
        }

        /// <summary>
        /// 获取供应商交易明细信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="heJi">合计信息[0:数量:int][1:结算金额:decimal][2:已支付金额:decimal]</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MJiaoYiMingXiInfo> GetJiaoYiMingXis(string companyId, string gysId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MJiaoYiMingXiChaXunInfo chaXun, out object[] heJi)
        {
            heJi = new object[] { 0, 0M, 0M };
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(gysId)) return null;

            var items = dal.GetJiaoYiMingXis(companyId, gysId, pageSize, pageIndex, ref recordCount, chaXun, out heJi);

            return items;
        }

        /// <summary>
        /// 获取供应商类型
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing? GetGysLeiXing(string gysId)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetGysLeiXing(gysId);
        }

        /// <summary>
        /// 获取购物店信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaIds">县区编号集合</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> GetXuanYongGouWuDians(string companyId, int[] areaIds)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetXuanYongGouWuDians(companyId, areaIds);
        }

        /// <summary>
        /// 获取景点信息业务实体
        /// </summary>
        /// <param name="jingDingId">景点编号</param>
        /// <param name="lng">语言</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MJingDianJingDianInfo GetJingDianInfo(string jingDingId, EyouSoft.Model.EnumType.SysStructure.LngType lng)
        {
            if (string.IsNullOrEmpty(jingDingId)) return null;

            var info = dal.GetJingDianInfo(jingDingId, lng);

            if (lng != EyouSoft.Model.EnumType.SysStructure.LngType.中文 && info == null)
            {
                info = GetJingDianInfo(jingDingId);
            }

            return info;
        }

        /// <summary>
        /// 获取景点信息业务实体
        /// </summary>
        /// <param name="jingDingId">景点编号</param>
        /// <param name="lng">语言</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MJingDianJingDianInfo GetJingDianInfo(string jingDingId)
        {
            return GetJingDianInfo(jingDingId, EyouSoft.Model.EnumType.SysStructure.LngType.中文);
        }

        /// <summary>
        /// 获取景点信息业务实体
        /// </summary>
        /// <param name="jingDingId">景点编号</param>
        /// <param name="lng">语言</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MJingDianJingDianInfo GetJingDianInfo(string jingDingId, string cityid)
        {
            return GetJingDianInfo(jingDingId, EyouSoft.Model.EnumType.SysStructure.LngType.中文, cityid);
        }

        /// <summary>
        /// 获取景点信息业务实体
        /// </summary>
        /// <param name="jingDianId"></param>
        /// <param name="lng"></param>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MJingDianJingDianInfo GetJingDianInfo(string jingDianId, EyouSoft.Model.EnumType.SysStructure.LngType lng, string cityid)
        {
            if (string.IsNullOrEmpty(jingDianId)) return null;

            var info = dal.GetJingDianInfo(jingDianId, lng, cityid);

            if (lng != EyouSoft.Model.EnumType.SysStructure.LngType.中文 && info == null)
            {
                info = GetJingDianInfo(jingDianId,cityid);
            }

            return info;
        }
        #endregion
    }
}
