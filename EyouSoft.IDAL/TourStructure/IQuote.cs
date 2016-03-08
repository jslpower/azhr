using System.Collections.Generic;

namespace EyouSoft.IDAL.TourStructure
{
    /// <summary>
    /// 描述：团队报价接口类
    /// 修改记录：
    /// 1、2011-09-05 PM 曹胡生 创建
    /// </summary>
    public interface IQuote
    {
        #region 团队报价相关方法
        /// <summary>
        /// 获得团队报价列表
        /// </summary>
        /// <param name="CompanyId">报价所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="info">搜索实体</param> 
        /// <param name="DeptId">部门集合</param> 
        /// <param name="isOnlySelf">是否仅查看自己的数据</param> 
        /// <param name="LoginUserId">当前登录的用户编号</param> 
        /// <param name="ShowBeforeMonth">显示当前时间前几个月的数据</param> 
        /// <param name="ShowAfterMonth">显示当前时间前后个月的数据</param> 
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MTourQuoteInfo> GetTourQuoteList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourQuoteSearch info, int[] DeptId, bool isOnlySelf, string LoginUserId, int ShowBeforeMonth, int ShowAfterMonth);

         /// <summary>
        /// 获得报价签证文件
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.ComStructure.MComAttach> GetVisaFileList(string QuoteId, int pageSize, int pageIndex, ref int recordCount);

        /// <summary>
        /// 获得团队报价信息
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <returns></returns>
        EyouSoft.Model.TourStructure.MTourQuoteInfo GetQuoteInfo(string QuoteId);

        /// <summary>
        /// 新增团队报价
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool AddTourQuote(EyouSoft.Model.TourStructure.MTourQuoteInfo info);

        /// <summary>
        /// 修改团队报价,报价成功
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int UpdateTourQuote(EyouSoft.Model.TourStructure.MTourQuoteInfo info);

        /// <summary>
        /// 取消团队报价
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <param name="Reason">取消原因</param> 
        /// <returns></returns>
        bool CalcelTourQuote(string QuoteId, string Reason);

        /// <summary>
        /// 删除报价
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <returns></returns>
        bool DeleteQuote(string QuoteId);

        /// <summary>
        /// 获取销售员超限详细
        /// </summary>
        /// <param name="SaleId">销售员编号</param>
        /// <param name="Money">当前金额</param>
        /// <returns></returns>
        //EyouSoft.Model.FinStructure.MSalesmanWarning GetSaleOverrunDetail(string SaleId, decimal Money);

        /// <summary>
        /// 获取客户超限详细
        /// </summary>
        /// <param name="CrmId">客户编号</param>
        /// <param name="Money">当前金额</param>
        /// <returns></returns>
        //EyouSoft.Model.FinStructure.MCustomerWarning GetCustomerOverrunDetail(string CrmId, decimal Money);

        /// <summary>
        ///计调报价 
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <param name="CostCalculation">成本核算</param>
        /// <returns></returns>
        bool PlanerQuote(string QuoteId, string CostCalculation);

        #endregion
    }
}
