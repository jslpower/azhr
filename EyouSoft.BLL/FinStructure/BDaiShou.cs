//代收相关信息业务逻辑类 汪奇志 2013-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.FinStructure
{
    public class BDaiShou : BLLBase
    {
        readonly EyouSoft.IDAL.FinStructure.IDaiShou dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.FinStructure.IDaiShou>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BDaiShou() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 写入代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.FinStructure.MDaiShouInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OrderId) 
                || string.IsNullOrEmpty(info.AnPaiId)
                || string.IsNullOrEmpty(info.OperatorId))
                return 0;

            info.DaiShouId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.Insert(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("添加代收信息，代收编号：" + info.DaiShouId + "。");
            }

            return dalRetCode;
        }

        /// <summary>
        /// 更新代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.FinStructure.MDaiShouInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OrderId)
                || string.IsNullOrEmpty(info.AnPaiId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.DaiShouId))
                return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.Update(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改代收信息，代收编号：" + info.DaiShouId + "。");
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="daiShouId">代收编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int Delete(string daiShouId, string companyId)
        {
            if (string.IsNullOrEmpty(daiShouId) || string.IsNullOrEmpty(companyId)) return 0;

            int dalRetCode = dal.Delete(daiShouId, companyId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除代收信息，代收编号：" + daiShouId + "。");
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取代收信息业务实体
        /// </summary>
        /// <param name="daiShouId">代收编号</param>
        /// <returns></returns>
        public EyouSoft.Model.FinStructure.MDaiShouInfo GetInfo(string daiShouId)
        {
            if (string.IsNullOrEmpty(daiShouId)) return null;

            return dal.GetInfo(daiShouId);
        }

        /// <summary>
        /// 获取代收信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MDaiShouInfo> GetDaiShous(string tourId)
        {
            if (string.IsNullOrEmpty(tourId)) return null;

            return dal.GetDaiShous(tourId);
        }

        /// <summary>
        /// 获取代收订单信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MDaiShouOrderInfo> GetOrders(string tourId)
        {
            if (string.IsNullOrEmpty(tourId)) return null;

            return dal.GetOrders(tourId);
        }

        /// <summary>
        /// 获取代收计调安排信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MDaiShouJiDiaoAnPaiInfo> GetAnPais(string tourId)
        {
            if (string.IsNullOrEmpty(tourId)) return null;

            return dal.GetAnPais(tourId);
        }

        /// <summary>
        /// 获取代收信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MDaiShouInfo> GetDaiShous(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.FinStructure.MDaiShouChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            bool isOnlySelf = false;
            int[] depts = null;
            //depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_代收管理, out  isOnlySelf);

            return dal.GetDaiShous(companyId, LoginUserId, depts, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 代收审批，返回1成功，其它失败
        /// </summary>
        /// <param name="info">审批实体</param>
        /// <returns></returns>
        public int ShenPi(EyouSoft.Model.FinStructure.MDaiShouShenPiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.DaiShouId)
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId))
                return 0;

            info.Time = DateTime.Now;

            int dalRetCode = dal.ShenPi(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("审批代收信息，代收编号：" + info.DaiShouId + "，审批状态：" + info.Status + "。");
            }

            return dalRetCode;
        }
        #endregion
    }
}
