using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 支付方式接口
    /// 创建者：郑付杰
    /// 创建时间：2011/9/8
    /// </summary>
    public interface IComPayment
    {
        /// <summary>
        /// 添加公司支付方式
        /// </summary>
        /// <param name="item">支付方式实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Add(MComPayment item);
        /// <summary>
        /// 修改公司支付方式
        /// </summary>
        /// <param name="item">支付方式实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Update(MComPayment item);
        /// <summary>
        /// 删除公司非系统默认支付方式
        /// </summary>
        /// <param name="paymentId">支付方式编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        bool Delete(int paymentId,string companyId);
        /// <summary>
        /// 获取单个支付方式
        /// </summary>
        /// <param name="paymentId">支付方式编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>支付方式实体</returns>
        MComPayment GetModel(int paymentId, string companyId);
        /// <summary>
        /// 获取公司支付方式
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sourceType">款项来源</param>
        /// <param name="itemType">支付方式类型</param>
        /// <returns>支付方式集合</returns>
        IList<MComPayment> GetList(string companyId, SourceType? sourceType, ItemType itemType);
        /// <summary>
        /// 获取公司所有支付方式
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>支付方式集合</returns>
        IList<MComPayment> GetList(string companyId);
    }
}
