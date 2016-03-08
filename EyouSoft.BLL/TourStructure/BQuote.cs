using System.Collections.Generic;

namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 描述：团队报价业务层
    /// 修改记录：
    /// 1、2011-09-05 PM 曹胡生 创建
    /// </summary>
    public class BQuote : EyouSoft.BLL.BLLBase
    {
        private readonly EyouSoft.IDAL.TourStructure.IQuote dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.IQuote>();

        #region 成员方法
        /// <summary>
        /// 获得团队报价列表
        /// </summary>
        /// <param name="CompanyId">报价所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="info">搜索实体</param> 
        /// <param name="ModuleType">模块类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourQuoteInfo> GetTourQuoteList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourQuoteSearch info)
        {
            //是否仅查看自己的数据(true:仅查看计划销售员为自己的报价)
            bool isOnlySelf = false;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] DepartIds = null;
            //switch (ModuleType)
            //{
                //case EyouSoft.Model.EnumType.TourStructure.ModuleType.组团:
                //    {
                //        DepartIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.组团团队_团队报价, out  isOnlySelf);
                //        break;
                //    }
                //case EyouSoft.Model.EnumType.TourStructure.ModuleType.出境:
                //    {
                //        DepartIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.出境团队_团队报价, out  isOnlySelf);
                //        break;
                //    }
                //case EyouSoft.Model.EnumType.TourStructure.ModuleType.地接:
                //    {
                //        DepartIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.地接团队_团队报价, out  isOnlySelf);
                //        break;
                //    }
            //}
            EyouSoft.Model.ComStructure.MComSetting MComSet = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId);
            return dal.GetTourQuoteList(CompanyId, pageSize, pageIndex, ref recordCount, info, DepartIds, isOnlySelf, this.LoginUserId, MComSet.ShowBeforeMonth, MComSet.ShowAfterMonth);
        }

         /// <summary>
        /// 获得报价签证文件
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MComAttach> GetVisaFileList(string QuoteId, int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetVisaFileList(QuoteId, pageSize, pageIndex, ref recordCount);
        }

        /// <summary>
        /// 获得团队报价信息
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourQuoteInfo GetQuoteInfo(string QuoteId)
        {
            return dal.GetQuoteInfo(QuoteId);
        }

        /// <summary>
        /// 新增团队报价
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddTourQuote(EyouSoft.Model.TourStructure.MTourQuoteInfo info)
        {
            //如果是新增，则ParentId为0
            if (info != null)
            {
                info.QuoteId = System.Guid.NewGuid().ToString();
                info.ParentId = "0";
            }
            if (dal.AddTourQuote(info))
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("新增报价,报价编号:{0}", info.QuoteId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存为新报价
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddNewTourQuote(EyouSoft.Model.TourStructure.MTourQuoteInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.ParentId)) return false;
            info.QuoteId = System.Guid.NewGuid().ToString();
            if (dal.AddTourQuote(info))
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("保存为新报价,报价编号:{0},父级编号:{1}", info.QuoteId, info.ParentId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改团队报价
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateTourQuote(EyouSoft.Model.TourStructure.MTourQuoteInfo info)
        {
            if (string.IsNullOrEmpty(info.QuoteId)) return false;
            if (dal.UpdateTourQuote(info) == 3)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("修改报价,报价编号:{0}", info.QuoteId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改团队报价(2报价成功 3 修改成功 4操作失败 5销售员超限 6客户超限 7 销售员客户均超限)
        /// 2012-8-17 王磊 返回的结果 去掉【1超限，垫付申请中】
        /// </summary>
        /// <param name="info">报价实体</param>
        /// <returns></returns>
        public int SuccessTourQuote(EyouSoft.Model.TourStructure.MTourQuoteInfo info)
        {
            int result = 3;
            if (info.MTourQuoteTourInfo != null && info.MTourQuoteTourInfo.LDate != null)
            {
                info.MTourQuoteTourInfo.TourId = System.Guid.NewGuid().ToString();
                info.MTourQuoteTourInfo.TourCode = "";
                //info.OrderCode = info.MTourQuoteTourInfo.TourCode + "01";
            }
            info.OrderId = System.Guid.NewGuid().ToString();
            info.OrderCode = "";
            if (string.IsNullOrEmpty(info.QuoteId))
            {
                //先新增，再报价成功
                info.QuoteId = System.Guid.NewGuid().ToString();
                info.ParentId = "0";
                if (dal.AddTourQuote(info))
                {
                    result = dal.UpdateTourQuote(info);
                }
            }
            else
            {
                result = dal.UpdateTourQuote(info);
            }
            if (result == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("报价未成功(超限垫付申请中)，,报价编号:{0}", info.QuoteId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            if (result == 2)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("报价成功,报价编号:{0}", info.QuoteId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return result;
        }

        ///<summary>
        /// 取消团队报价
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <param name="Reason">取消原因</param> 
        /// <returns></returns>
        public bool CalcelTourQuote(string QuoteId, string Reason)
        {
            if (string.IsNullOrEmpty(QuoteId) || string.IsNullOrEmpty(Reason))
            {
                return false;
            }
            if (dal.CalcelTourQuote(QuoteId, Reason))
            {
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("报价取消,报价编号:{0}", QuoteId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除报价
        /// </summary>
        /// <param name="QuoteIds">报价编号，多个用逗号分隔</param>
        /// <returns></returns>
        public bool DeleteQuote(string QuoteIds)
        {
            if (dal.DeleteQuote(QuoteIds))
            {
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("报价删除,报价编号:{0}", QuoteIds);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        ///// <summary>
        ///// 获取销售员超限详细(NULL表示未超限)
        ///// </summary>
        ///// <param name="SaleId">销售员编号</param>
        ///// <param name="Money">当前金额</param>
        ///// <param name="CompanyId">公司编号</param>
        ///// <returns></returns>
        //public EyouSoft.Model.FinStructure.MSalesmanWarning GetSaleOverrunDetail(string SaleId, decimal Money, string CompanyId)
        //{
        //    //bool ArrearsRangeControl = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId).ArrearsRangeControl;
        //    //if (ArrearsRangeControl)
        //    //{
        //    //    return dal.GetSaleOverrunDetail(SaleId, Money);
        //    //}
        //    return null;
        //}

        ///// <summary>
        ///// 获取客户超限详细(NULL表示未超限)
        ///// </summary>
        ///// <param name="CrmId">客户编号</param>
        ///// <param name="Money">当前金额</param> 
        ///// <param name="CompanyId">公司编号</param> 
        ///// <returns></returns>
        //public EyouSoft.Model.FinStructure.MCustomerWarning GetCustomerOverrunDetail(string CrmId, decimal Money, string CompanyId)
        //{
        //    bool ArrearsRangeControl = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId).ArrearsRangeControl;
        //    if (ArrearsRangeControl)
        //    {
        //        return dal.GetCustomerOverrunDetail(CrmId, Money);
        //    }
        //    return null;
        //}

        /// <summary>
        ///计调报价 
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <param name="CostCalculation">成本核算</param>
        /// <returns></returns>
        public bool PlanerQuote(string QuoteId, string CostCalculation)
        {
            if (dal.PlanerQuote(QuoteId, CostCalculation))
            {
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("计调已报价,报价编号:{0}", QuoteId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        #endregion
    }
}
