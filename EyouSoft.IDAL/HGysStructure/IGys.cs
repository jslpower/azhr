using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HGysStructure
{
    /// <summary>
    /// 供应商相关数据访问类接口
    /// </summary>
    public interface IGys
    {
        /*/// <summary>
        /// 是否存在指定语种的供应商信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="lng">语种</param>
        /// <returns></returns>
        bool IsExistsLng(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType lng);*/
        /// <summary>
        /// 写入供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Insert(EyouSoft.Model.HGysStructure.MGysInfo info);
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="lng">语言类型</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MGysInfo GetInfo(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType lng);
        /// <summary>
        /// 更新供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Update(EyouSoft.Model.HGysStructure.MGysInfo info);
        /// <summary>
        /// 删除供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        int Delete(string companyId, string gysId);
        /// <summary>
        /// 获取供应商信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MLBInfo> GetGyss(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MLBChaXunInfo chaXun);
        /// <summary>
        /// 获取选用的供应商信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> GetXuanYongs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo chaXun);
        /// <summary>
        /// 获取选用的景点信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MXuanYongJingDianInfo> GetXuanYongJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo chaXun);
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
        IList<EyouSoft.Model.HGysStructure.MJiaoYiMingXiInfo> GetJiaoYiMingXis(string companyId, string gysId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MJiaoYiMingXiChaXunInfo chaXun, out object[] heJi);
        /// <summary>
        /// 获取供应商类型
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        EyouSoft.Model.EnumType.GysStructure.GysLeiXing? GetGysLeiXing(string gysId);

        /// <summary>
        /// 获取购物店信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaIds">县区编号集合</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> GetXuanYongGouWuDians(string companyId, int[] areaIds);

        /// <summary>
        /// 获取景点信息业务实体
        /// </summary>
        /// <param name="jingDingId">景点编号</param>
        /// <param name="lng">语言</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MJingDianJingDianInfo GetJingDianInfo(string jingDingId, EyouSoft.Model.EnumType.SysStructure.LngType lng);

        /// <summary>
        /// 获取景点信息业务实体
        /// </summary>
        /// <param name="jingDingId">景点编号</param>
        /// <param name="lng">语言</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MJingDianJingDianInfo GetJingDianInfo(string jingDingId, EyouSoft.Model.EnumType.SysStructure.LngType lng, string cityid);
    }
}
