using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HTourStructure
{
    using System.Data;

    using EyouSoft.Model.EnumType.TourStructure;

    public interface IQuote
    {
        /// <summary>
        /// 验证是否存在相同的询价编号
        /// </summary>
        /// <param name="BuyId">询价编号</param>
        /// <param name="QuoteId">报价编号</param>
        /// <returns></returns>
        bool isExist(string BuyId, string QuoteId);
        /// <summary>
        /// 添加报价
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:添加成功 2：报价成功 0：操作失败</returns>
        int AddQuote(EyouSoft.Model.HTourStructure.MQuote model);

        /// <summary>
        /// 修改报价
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:修改成功 2：报价成功 0：操作失败</returns>
        int UpdateQuote(EyouSoft.Model.HTourStructure.MQuote model);

        /// <summary>
        /// 删除报价
        /// </summary>
        /// <param name="QuoteIds">报价编号</param>
        /// <returns></returns>
        bool DeleteQuote(string QuoteIds);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        EyouSoft.Model.HTourStructure.MQuote GetQuoteModel(string QuoteId);


        /// <summary>
        /// 获取报价的分页列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="Search"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HTourStructure.MQuoteInfo> GetQuoteList(
           int pageSize,
           int pageIndex,
           ref int recordCount,
           EyouSoft.Model.HTourStructure.MQuoteSearch search);


        /// <summary>
        /// 获取报价比较
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HTourStructure.MQuoteCompare> GetQuoteCompareList(string ParentId); 

        /// <summary>
        /// 获取报价比较的价格条目信息
        /// </summary>
        /// <param name="QuoteIds"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HTourStructure.MQuoteCost> GetQuoteCostList(string[] QuoteIds);

        /// <summary>
        /// 根据报价编号、价格类型、项目类型获取报价差异信息
        /// </summary>
        /// <param name="quoteId">报价编号</param>
        /// <param name="costMode">价格类型</param>
        /// <param name="priceType">项目类型</param>
        /// <returns>报价差异信息</returns>
        DataSet GetQuoteCompare(string quoteId, CostMode costMode, Pricetype priceType);

        /// <summary>
        /// 根据报价编号得到相关其它报价
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HTourStructure.MTourQuoteNo> GetTourQuoteNo(string QuoteId);
    }
}
