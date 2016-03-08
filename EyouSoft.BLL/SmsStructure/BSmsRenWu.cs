//短信中心-短信任务相关业务逻辑类
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SmsStructure
{
    /// <summary>
    /// 短信中心-短信任务相关业务逻辑类
    /// </summary>
    public class BSmsRenWu
    {
        readonly EyouSoft.IDAL.SmsStructure.ISmsRenWu dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SmsStructure.ISmsRenWu>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BSmsRenWu() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 写入短信上行信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertShangXing(EyouSoft.Model.SmsStructure.MSmsShangXingInfo info)
        {
            if (info == null) return 0;

            info.ShangXingId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertShangXing(info);

            return dalRetCode;
        }

        /// <summary>
        /// 写入短信任务，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.SmsStructure.MSmsRenWuInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.ShangXingId))
                return 0;

            info.RenWuId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.Insert(info);

            return dalRetCode;
        }

        /// <summary>
        /// 接收任务，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int JieShouRenWu(EyouSoft.Model.SmsStructure.MSmsRenWuJieShouInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.RenWuId) 
                || string.IsNullOrEmpty(info.JieShouRenId))
                return 0;

            info.JieShouTime = DateTime.Now;

            int dalRetCode = dal.JieShouRenWu(info);

            return dalRetCode;
        }

        /// <summary>
        /// 获取短信任务信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">总索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SmsStructure.MSmsRenWuInfo> GetRenWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SmsStructure.MSmsRenWuChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetRenWus(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 行程变化，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int XCBH(EyouSoft.Model.SmsStructure.MSmsXCBHInfo info)
        {
            if (info == null) return 0;
            return dal.XCBH(info);
        }

        /// <summary>
        /// 进店报账，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int JDBZ(EyouSoft.Model.SmsStructure.MSmsJDBZInfo info)
        {
            if (info == null) return 0;
            return dal.JDBZ(info);
        }
        #endregion
    }
}
