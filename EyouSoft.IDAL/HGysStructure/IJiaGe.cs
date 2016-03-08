using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HGysStructure
{
    /// <summary>
    /// 供应商价格相关数据访问类接口
    /// </summary>
    public interface IJiaGe
    {
        /// <summary>
        /// 新增车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertCheXingJiaGe(EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo info);
        /// <summary>
        /// 修改车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateCheXingJiaGe(EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo info);
        /// <summary>
        /// 删除车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="cheXingId">车型编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        int DeleteCheXingJiaGe(string companyId, string gysId, string cheXingId, string jiaGeId);
        /// <summary>
        /// 获取车型价格信息集合
        /// </summary>
        /// <param name="cheXingId">车型编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo> GetCheXingJiaGes(string cheXingId);

        /// <summary>
        /// 新增景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertJingDianJiaGe(EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo info);
        /// <summary>
        /// 修改景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateJingDianJiaGe(EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo info);
        /// <summary>
        /// 删除景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="jingDianId">景点编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        int DeleteJingDianJiaGe(string companyId,string gysId, string jingDianId, string jiaGeId);
        /// <summary>
        /// 获取景点价格信息集合
        /// </summary>
        /// <param name="jingDianId">景点编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> GetJingDianJiaGes(string jingDianId, EyouSoft.Model.HGysStructure.MJiaGeChaXunInfo chaXun);

        /// <summary>
        /// 新增餐馆菜单信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertCanGuanCaiDan(EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo info);
        /// <summary>
        /// 修改餐馆菜单信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateCanGuanCaiDan(EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo info);
        /// <summary>
        /// 删除餐馆菜单信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="caiDanId">菜单编号</param>
        /// <returns></returns>
        int DeleteCanGuanCaiDan(string companyId, string gysId, string caiDanId);
        /// <summary>
        /// 获取餐馆菜单信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="LngType">语种类型</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> GetCanGuanCaiDans(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType LngType);
        /// <summary>
        /// 获取餐馆可用菜单信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="LngType">语种类型</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> GetCanGuanCaiDan(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType LngType);
        /// <summary>
        /// 获取餐馆菜单信息
        /// </summary>
        /// <param name="caiDanId">菜单编号</param>
        /// <param name="lngType">语言类型</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo GetCanGuanCaiDanInfo(string caiDanId,EyouSoft.Model.EnumType.SysStructure.LngType lngType);

        /// <summary>
        /// 新增酒店价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertJiuDianJiaGe(EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo info);
        /// <summary>
        /// 修改酒店价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateJiuDianJiaGe(EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo info);
        /// <summary>
        /// 删除酒店价格信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        int DeleteJiuDianJiaGe(string companyId, string gysId, string jiaGeId);
        /// <summary>
        /// 获取酒店价格信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo> GetJiuDianJiaGes(string gysId);
        /// <summary>
        /// 获取酒店价格信息业务实体
        /// </summary>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo GetJiuDianJiaGeInfo(string jiaGeId);
    }
}
