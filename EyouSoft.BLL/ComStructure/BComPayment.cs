using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 公司支付方式业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComPayment
    {
        private readonly EyouSoft.IDAL.ComStructure.IComPayment dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComPayment>();
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BComPayment() { }

        #region
        /// <summary>
        /// 添加公司支付方式
        /// </summary>
        /// <param name="item">支付方式实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComPayment item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加支付方式,名称为:{0}", item.Name));
                }
            }
            return result;
        }

        /// 修改公司支付方式
        /// </summary>
        /// <param name="item">支付方式实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComPayment item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改支付方式,编号为:{0}", item.PaymentId));
                }
            }
            return result;
        }
        /// <summary>
        /// 删除公司非系统默认支付方式
        /// </summary>
        /// <param name="paymentId">支付方式编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(int paymentId, string companyId)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(companyId))
            {
                result = dal.Delete(paymentId, companyId);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("删除支付方式,编号为:{0}", paymentId));
                }
            }
            return result;
        }
        /// <summary>
        /// 获取单个支付方式
        /// </summary>
        /// <param name="paymentId">支付方式编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>支付方式实体</returns>
        public MComPayment GetModel(int paymentId, string companyId)
        {
            MComPayment item = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(paymentId, companyId);
            }
            return item;
        }
        /// <summary>
        /// 获取公司所有支付方式
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>支付方式集合</returns>
        public IList<MComPayment> GetList(string companyId)
        {
            IList<MComPayment> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(companyId);
            }
            return list;
        }

        /// <summary>
        /// 获取公司支付方式
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sourceType">款项来源</param>
        /// <param name="itemType">支付方式类型</param>
        /// <returns>支付方式集合</returns>
        public IList<MComPayment> GetList(string companyId, SourceType? sourceType, ItemType itemType)
        {
            IList<MComPayment> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(companyId, sourceType, itemType);
            }
            return list;
        }
        #endregion
    }
}
