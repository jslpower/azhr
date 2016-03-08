using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HGysStructure
{
    /// <summary>
    /// 供应商价格相关业务逻辑类
    /// </summary>
    public class BJiaGe
    {
        readonly EyouSoft.IDAL.HGysStructure.IJiaGe dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HGysStructure.IJiaGe>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BJiaGe() { }
        #endregion

        #region public members
        /// <summary>
        /// 新增车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertCheXingJiaGe(EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CheXingId)) return 0;

            info.JiaGeId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertCheXingJiaGe(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增车型价格，价格编号：" + info.JiaGeId + "，车型编号：" + info.CheXingId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateCheXingJiaGe(EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo info)
        {
            if (info == null
               || string.IsNullOrEmpty(info.CheXingId)
               || string.IsNullOrEmpty(info.JiaGeId)) return 0;

            int dalRetCode = dal.UpdateCheXingJiaGe(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改车型价格，价格编号：" + info.JiaGeId + "，车型编号：" + info.CheXingId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="cheXingId">车型编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        public int DeleteCheXingJiaGe(string companyId, string gysId, string cheXingId, string jiaGeId)
        {
            if (string.IsNullOrEmpty(jiaGeId)) return 0;

            int dalRetCode = dal.DeleteCheXingJiaGe(companyId, gysId, cheXingId, jiaGeId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除车型价格，价格编号：" + jiaGeId + "，车型编号：" + cheXingId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取车型价格信息集合
        /// </summary>
        /// <param name="cheXingId">车型编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo> GetCheXingJiaGes(string cheXingId)
        {
            if (string.IsNullOrEmpty(cheXingId)) return null;

            return dal.GetCheXingJiaGes(cheXingId);
        }

        /// <summary>
        /// 新增景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertJingDianJiaGe(EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.JingDianId)) return 0;

            info.JiaGeId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertJingDianJiaGe(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增景点价格信息，价格编号：" + info.JiaGeId + "，景点编号：" + info.JingDianId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateJingDianJiaGe(EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo info)
        {
            if (info == null
               || string.IsNullOrEmpty(info.JingDianId)
               || string.IsNullOrEmpty(info.JiaGeId)) return 0;

            int dalRetCode = dal.UpdateJingDianJiaGe(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改景点价格信息，价格编号：" + info.JiaGeId + "，景点编号：" + info.JingDianId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="jingDianId">景点编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        public int DeleteJingDianJiaGe(string companyId, string gysId, string jingDianId, string jiaGeId)
        {
            if (string.IsNullOrEmpty(jiaGeId)) return 0;

            int dalRetCode = dal.DeleteJingDianJiaGe(companyId, gysId, jingDianId, jiaGeId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除景点价格信息，价格编号：" + jiaGeId + "，景点编号：" + jingDianId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取景点价格信息集合
        /// </summary>
        /// <param name="jingDianId">景点编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> GetJingDianJiaGes(string jingDianId)
        {
            return GetJingDianJiaGes(jingDianId, null);
        }

        /// <summary>
        /// 获取景点价格信息集合
        /// </summary>
        /// <param name="jingDianId">景点编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> GetJingDianJiaGes(string jingDianId, EyouSoft.Model.HGysStructure.MJiaGeChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(jingDianId)) return null;

            return dal.GetJingDianJiaGes(jingDianId, chaXun);
        }

        /// <summary>
        /// 新增餐馆菜单信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertCanGuanCaiDan(EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.GysId)) return 0;

            if (string.IsNullOrEmpty(info.CaiDanId)) info.CaiDanId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertCanGuanCaiDan(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增餐馆菜单，菜单编号：" + info.CaiDanId + "，供应商编号：" + info.GysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改餐馆菜单信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateCanGuanCaiDan(EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.GysId)
                || string.IsNullOrEmpty(info.CaiDanId)) return 0;

            if (GetCanGuanCaiDanInfo(info.CaiDanId, info.LngType) == null) return InsertCanGuanCaiDan(info);

            int dalRetCode = dal.UpdateCanGuanCaiDan(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改餐馆菜单，菜单编号：" + info.CaiDanId + "，供应商编号：" + info.GysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除餐馆菜单信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="caiDanId">菜单编号</param>
        /// <returns></returns>
        public int DeleteCanGuanCaiDan(string companyId, string gysId, string caiDanId)
        {
            if (string.IsNullOrEmpty(caiDanId)) return 0;

            int dalRetCode = dal.DeleteCanGuanCaiDan(companyId, gysId, caiDanId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除餐馆菜单，菜单编号：" + caiDanId + "，供应商编号：" + gysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取餐馆菜单信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="LngType">语种类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> GetCanGuanCaiDans(string gysId)
        {
            return GetCanGuanCaiDans(gysId, EyouSoft.Model.EnumType.SysStructure.LngType.中文);
        }

        /// <summary>
        /// 获取餐馆菜单信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="LngType">语种类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> GetCanGuanCaiDans(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType LngType)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetCanGuanCaiDans(gysId, LngType);
        }

        /// <summary>
        /// 获取餐馆可用菜单信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="LngType">语种类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> GetCanGuanCaiDan(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType LngType)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetCanGuanCaiDan(gysId, LngType);
        }

        /// <summary>
        /// 获取餐馆菜单信息
        /// </summary>
        /// <param name="caiDanId">菜单编号</param>
        /// <param name="lngType">语言类型</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo GetCanGuanCaiDanInfo(string caiDanId, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            if (string.IsNullOrEmpty(caiDanId)) return null;

            return dal.GetCanGuanCaiDanInfo(caiDanId, lngType);
        }

        /// <summary>
        /// 获取餐馆菜单信息
        /// </summary>
        /// <param name="caiDanId">菜单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo GetCanGuanCaiDanInfo(string caiDanId)
        {
            return dal.GetCanGuanCaiDanInfo(caiDanId, EyouSoft.Model.EnumType.SysStructure.LngType.中文);
        }

        /// <summary>
        /// 新增酒店价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertJiuDianJiaGe(EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.GysId)
                || string.IsNullOrEmpty(info.FangXingId)) return 0;

            info.JiaGeId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertJiuDianJiaGe(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增酒店价格信息，价格编号：" + info.JiaGeId + "，酒店编号：" + info.GysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改酒店价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateJiuDianJiaGe(EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.GysId)
                || string.IsNullOrEmpty(info.FangXingId)
                || string.IsNullOrEmpty(info.JiaGeId)) return 0;

            int dalRetCode = dal.UpdateJiuDianJiaGe(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改酒店价格信息，价格编号：" + info.JiaGeId + "，酒店编号：" + info.GysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除酒店价格信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        public int DeleteJiuDianJiaGe(string companyId, string gysId, string jiaGeId)
        {
            if (string.IsNullOrEmpty(jiaGeId)) return 0;
            int dalRetCode = dal.DeleteJiuDianJiaGe(companyId, gysId, jiaGeId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除酒店价格信息，价格编号：" + jiaGeId + "，酒店编号：" + gysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取酒店价格信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo> GetJiuDianJiaGes(string gysId)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetJiuDianJiaGes(gysId);
        }

        /// <summary>
        /// 获取酒店价格信息业务实体
        /// </summary>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo GetJiuDianJiaGeInfo(string jiaGeId)
        {
            if (string.IsNullOrEmpty(jiaGeId)) return null;

            return dal.GetJiuDianJiaGeInfo(jiaGeId);
        }
        #endregion
    }
}
