using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 上车地点业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComCarPlace
    {
        private readonly EyouSoft.IDAL.ComStructure.IComCarPlace dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComCarPlace>();

        public BComCarPlace() { }

        #region 
        /// <summary>
        /// 添加上车地点
        /// </summary>
        /// <param name="item">上传地点实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(MComCarPlace item)
        {
            bool result = false;
            if (item == null)
            {
                result = dal.Add(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("新增上车地点,名称为:{0}", item.Place));
                }
            }

            return result;
        }
        /// <summary>
        /// 修改上车地点
        /// </summary>
        /// <param name="item">上传地点实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(MComCarPlace item)
        {
            bool result = false;
            if (item == null)
            {
                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改上车地点,编号为:{0}", item.Id));
                }
            }

            return result;
        }
        /// <summary>
        /// 获取加价金额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="place">上车地点</param>
        /// <returns>加价金额</returns>
        public decimal GetAmountByPlace(string companyId, string place)
        {
            decimal moeny = 0;
            if (!string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(place))
            {
                moeny = dal.GetAmountByPlace(companyId, place);
            }
            return moeny;
        }
        /// <summary>
        /// 获取所有上车地点信息
        /// </summary>
        /// <returns>上车地点集合</returns>
        public IList<MComCarPlace> GetList(string companyId)
        {
            IList<MComCarPlace> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(companyId);
            }

            return list;
        }
        /// <summary>
        /// 分页获取上车地点列表
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns>上车地点集合</returns>
        public IList<MComCarPlace> GetList(int pageCurrent, int pageSize, ref int pageCount, string companyId)
        {
            IList<MComCarPlace> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                pageCurrent = pageCurrent < 1 ? 1 : pageCurrent;
                list = dal.GetList(pageCurrent, pageSize, ref pageCount, companyId);
            }

            return list;
        }
        #endregion
    }
}
