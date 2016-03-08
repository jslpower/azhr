using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HTourStructure
{
    using System.Data;

    using EyouSoft.Model.EnumType.TourStructure;

    public class BQuote : EyouSoft.BLL.BLLBase
    {
        private readonly EyouSoft.IDAL.HTourStructure.IQuote dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HTourStructure.IQuote>();

        /// <summary>
        /// 验证是否存在相同的询价编号
        /// </summary>
        /// <param name="BuyId">询价编号</param>
        /// <param name="QuoteId">报价编号</param>
        /// <returns></returns>
        public bool isExist(string BuyId, string QuoteId)
        {
            if (string.IsNullOrEmpty(BuyId)) return false;
            //if (string.IsNullOrEmpty(QuoteId)) return false;
            return dal.isExist(BuyId, QuoteId);
        }

        /// <summary>
        /// 添加报价
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:添加成功 2：报价成功 0：操作失败</returns>
        public int AddQuote(EyouSoft.Model.HTourStructure.MQuote model)
        {
            //if (string.IsNullOrEmpty(model.BuyCompanyID)
            //    || string.IsNullOrEmpty(model.BuyCompanyName)
            //    || model.BuyTime == null
            //    //|| string.IsNullOrEmpty(model.BuyId)
            //    //|| model.AreaId <= 0
            //    ||string.IsNullOrEmpty(model.TourType.ToString())
            //    || string.IsNullOrEmpty(model.RouteName)
            //    || model.Days <= 0
            //    || model.CountryId <= 0
            //    || string.IsNullOrEmpty(model.SellerId)
            //    || string.IsNullOrEmpty(model.SellerName)//
            //    //|| model.SellerDeptId == 0//
            //    || model.MaxAdults <= 0
            //    //|| model.MinAdults <= 0
            //    || string.IsNullOrEmpty(model.Operator)//
            //    || string.IsNullOrEmpty(model.OperatorId)
            //    //|| model.OperatorDeptId == 0
            //    )//
            //    return 0;

            model.QuoteId = Guid.NewGuid().ToString();

            if (model.QuoteStatus == EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功)
            {
                if (model.QuoteTour == null) return 0;

                if (model.QuoteTour.LDate == null
                    || model.QuoteTour.RDate == null
                    || model.QuoteTour.Adults + model.QuoteTour.Childs + model.QuoteTour.Leads <= 0
                    || model.QuoteTour.AdultPrice <= 0)
                    return 0;

                model.QuoteTour.TourId = Guid.NewGuid().ToString();

                model.QuoteTour.TourCode = new BTour().GenerateTourNo(model.OperatorDeptId, model.CompanyId, model.QuoteTour.TourType, model.QuoteTour.LDate,model.BuyCompanyID);
            }

            int flg = dal.AddQuote(model);

            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                if (model.QuoteStatus == EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功)
                {
                    str.AppendFormat("添加报价,{0},报价编号:{1}", model.QuoteStatus, model.QuoteId);
                }
                else
                {
                    str.AppendFormat("添加报价,报价编号:{0}", model.QuoteId);
                }
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }

            return flg;
        }

        /// <summary>
        /// 修改报价
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:修改成功 2：报价成功 0：操作失败</returns>
        public int UpdateQuote(EyouSoft.Model.HTourStructure.MQuote model)
        {
            //if (string.IsNullOrEmpty(model.QuoteId)
            //    || string.IsNullOrEmpty(model.BuyCompanyID)
            //    || string.IsNullOrEmpty(model.BuyCompanyName)
            //    || model.BuyTime == null
            //    //|| string.IsNullOrEmpty(model.BuyId)
            //    //|| model.AreaId <= 0
            //    || string.IsNullOrEmpty(model.RouteName)
            //    || model.Days <= 0
            //    || model.CountryId <= 0
            //    || string.IsNullOrEmpty(model.SellerId)
            //    || string.IsNullOrEmpty(model.SellerName)
            //    //|| model.SellerDeptId == 0
            //    || model.MaxAdults <= 0
            //    //|| model.MinAdults <= 0
            //    || string.IsNullOrEmpty(model.Operator)
            //    || string.IsNullOrEmpty(model.OperatorId)
            //    //|| model.OperatorDeptId == 0
            //    )
            //    return 0;

            if (model.QuoteStatus == EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功)
            {
                if (model.QuoteTour == null) return 0;
                if (model.QuoteTour.LDate == null
                    || model.QuoteTour.RDate == null
                    || model.QuoteTour.Adults + model.QuoteTour.Childs + model.QuoteTour.Leads <= 0
                    || model.QuoteTour.AdultPrice <= 0)
                    return 0;

                model.QuoteTour.TourId = Guid.NewGuid().ToString();

                model.QuoteTour.TourCode = new BTour().GenerateTourNo(model.OperatorDeptId, model.CompanyId, model.QuoteTour.TourType, model.QuoteTour.LDate,model.BuyCompanyID);
            }

            int flg = dal.UpdateQuote(model);
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                if (model.QuoteStatus == EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功)
                {
                    str.AppendFormat("修改报价,{0},报价编号:{1}", model.QuoteStatus, model.QuoteId);
                }
                else
                {
                    str.AppendFormat("修改报价,报价编号:{0}", model.QuoteId);
                }
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }


        /// <summary>
        /// 删除报价
        /// </summary>
        /// <param name="QuoteIds">报价编号</param>
        /// <returns></returns>
        public bool DeleteQuote(string QuoteIds)
        {
            if (string.IsNullOrEmpty(QuoteIds)) return false;
            if (dal.DeleteQuote(QuoteIds))
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("删除报价,报价编号:{0}", QuoteIds);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;

        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MQuote GetQuoteModel(string QuoteId)
        {
            if (string.IsNullOrEmpty(QuoteId)) return null;
            return dal.GetQuoteModel(QuoteId);
        }


        /// <summary>
        /// 获取报价的分页列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MQuoteInfo> GetQuoteList(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            EyouSoft.Model.HTourStructure.MQuoteSearch search)
        {
            return dal.GetQuoteList(pageSize, pageIndex, ref recordCount, search);
        }


        /// <summary>
        /// 获取报价比较
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MQuoteCompare> GetQuoteCompareList(string ParentId)
        {
            if (string.IsNullOrEmpty(ParentId)) return null;
            return dal.GetQuoteCompareList(ParentId);
        }

        /// <summary>
        /// 获取报价比较的价格条目信息
        /// </summary>
        /// <param name="QuoteIds"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MQuoteCost> GetQuoteCostList(string[] QuoteIds)
        {
            if (QuoteIds == null && QuoteIds.Length == 0) return null;
            return dal.GetQuoteCostList(QuoteIds);
        }

        /// <summary>
        /// 根据报价编号、价格类型、项目类型获取报价差异信息
        /// </summary>
        /// <param name="quoteId">报价编号</param>
        /// <param name="costMode">价格类型</param>
        /// <param name="priceType">项目类型</param>
        /// <returns>报价差异信息</returns>
        public DataSet GetQuoteCompare(string quoteId,CostMode costMode,Pricetype priceType)
        {
            return dal.GetQuoteCompare(quoteId, costMode, priceType);
        }

        /// <summary>
        /// 根据报价编号得到相关其它报价
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MTourQuoteNo> GetTourQuoteNo(string QuoteId)
        {
            if (string.IsNullOrEmpty(QuoteId)) return null;
            return dal.GetTourQuoteNo(QuoteId);
        }
    }
}
