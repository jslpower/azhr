using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HTourStructure
{
    using System.Web;

    using EyouSoft.BLL.TourStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Toolkit;
    using System.Data;

    public class BTour : EyouSoft.BLL.BLLBase
    {
        private readonly EyouSoft.IDAL.HTourStructure.ITour dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HTourStructure.ITour>();

        /// <summary>
        /// 验证是否存在相同的团号(特价产品)
        /// </summary>
        /// <param name="TourCode">团号</param>
        /// <param name="QuoteId">计划编号</param>
        /// <returns></returns>
        public bool isExist(string TourCode, string TourId)
        {
            if (string.IsNullOrEmpty(TourCode))
            {
                return false;
            }
            if (string.IsNullOrEmpty(TourId)) return false;
            return dal.isExist(TourCode, TourId);

        }

        /// <summary>
        /// 添加散拼计划模版团
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parentid">模版团编号</param>
        /// <returns>1:添加成功 0：添加失败</returns>
        public int AddSanPin(EyouSoft.Model.HTourStructure.MTour model, ref string parentid)
        {
            if (model.AreaId == 0
                || string.IsNullOrEmpty(model.RouteName)
                //|| string.IsNullOrEmpty(model.SellerId)
                //|| string.IsNullOrEmpty(model.SellerName)
                //|| model.SellerDeptId == 0
                || model.TourDays <= 0
                || string.IsNullOrEmpty(model.OperatorId)
                || string.IsNullOrEmpty(model.Operator)
                || model.OperatorDeptId == 0) return 0;

            model.TourId = Guid.NewGuid().ToString();
            parentid = model.TourId;
            if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品)
            {

                model.TourCode = GenerateTourNo(model.OperatorDeptId, model.CompanyId, model.TourType, model.LDate, model.BuyCompanyID);
            }

            int flg = dal.AddSanPin(model);

            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("添加了散拼,散拼编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 添加散拼计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:添加成功 0：添加失败</returns>
        public int AddSanPin(EyouSoft.Model.HTourStructure.MTour model)
        {
            if (model.AreaId == 0
                || string.IsNullOrEmpty(model.RouteName)
                //|| string.IsNullOrEmpty(model.SellerId)
                //|| string.IsNullOrEmpty(model.SellerName)
                //|| model.SellerDeptId == 0
                || model.TourDays <= 0
                || string.IsNullOrEmpty(model.OperatorId)
                || string.IsNullOrEmpty(model.Operator)
                || model.OperatorDeptId == 0) return 0;

            model.TourId = Guid.NewGuid().ToString();
            if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品)
            {

                model.TourCode = GenerateTourNo(model.OperatorDeptId, model.CompanyId, model.TourType, model.LDate, model.BuyCompanyID);
            }

            int flg = dal.AddSanPin(model);

            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("添加了散拼,散拼编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 修改团队计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:修改成功 0：修改失败</returns>
        public int UpdateSanPin(EyouSoft.Model.HTourStructure.MTour model)
        {
            int flg = 0;
            if (model.AreaId == 0
                 || string.IsNullOrEmpty(model.RouteName)
                //|| string.IsNullOrEmpty(model.SellerId)
                //|| string.IsNullOrEmpty(model.SellerName)
                //|| model.SellerDeptId == 0
                 || model.TourDays <= 0
                 || string.IsNullOrEmpty(model.OperatorId)
                 || string.IsNullOrEmpty(model.Operator)
                 || model.OperatorDeptId == 0) return 0;

                flg = dal.UpdateSanPin(model);
            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("修改了散拼,散拼编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 获取散拼实体
        /// </summary>
        /// <param name="TourIds">计划编号</param>
        /// <param name="isparent">是否模版团</param>
        /// <param name="ldate">出团日期</param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetSanPinModel(string TourIds)
        {
            if (string.IsNullOrEmpty(TourIds)) return null;
            return dal.GetSanPinModel(TourIds, null, null);

        }

        /// <summary>
        /// 获取散拼实体
        /// </summary>
        /// <param name="TourIds">计划编号</param>
        /// <param name="isparent">是否模版团</param>
        /// <param name="ldate">出团日期</param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetSanPinModel(string TourIds, bool? isparent, DateTime? ldate)
        {
            if (string.IsNullOrEmpty(TourIds)) return null;
            return dal.GetSanPinModel(TourIds,isparent, ldate);

        }

        /// <summary>
        /// 添加团队计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:添加成功 0：添加失败</returns>
        public int AddTour(EyouSoft.Model.HTourStructure.MTour model)
        {
            if (string.IsNullOrEmpty(model.CompanyId)
                //|| model.AreaId == 0
                || string.IsNullOrEmpty(model.RouteName)
                //|| string.IsNullOrEmpty(model.SellerId)
                //|| string.IsNullOrEmpty(model.SellerName)
                //|| model.SellerDeptId == 0
                || model.TourDays <= 0
                || string.IsNullOrEmpty(model.OperatorId)
                || string.IsNullOrEmpty(model.Operator)
                || model.OperatorDeptId == 0) return 0;

            model.TourId = Guid.NewGuid().ToString();
            if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.团队产品 || model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.自由行)
            {
                if (string.IsNullOrEmpty(model.BuyCompanyID)
                    || string.IsNullOrEmpty(model.BuyCompanyName)
                    || model.Adults + model.Childs + model.Leaders <= 0) return 0;

                model.TourCode = GenerateTourNo(model.OperatorDeptId, model.CompanyId, model.TourType, model.LDate, model.BuyCompanyID);
            }


            int flg = dal.AddTour(model);

            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("添加了计划,计划编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 修改团队计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:修改成功 0：修改失败</returns>
        public int UpdateTour(EyouSoft.Model.HTourStructure.MTour model)
        {
            if (string.IsNullOrEmpty(model.CompanyId)
                 //|| model.AreaId == 0
                 || string.IsNullOrEmpty(model.RouteName)
                //|| string.IsNullOrEmpty(model.SellerId)
                //|| string.IsNullOrEmpty(model.SellerName)
                //|| model.SellerDeptId == 0
                 || model.TourDays <= 0
                 || string.IsNullOrEmpty(model.OperatorId)
                 || string.IsNullOrEmpty(model.Operator)
                 || model.OperatorDeptId == 0) return 0;

            if (model.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.团队产品 || model.TourType== TourType.自由行)
            {
                if (string.IsNullOrEmpty(model.BuyCompanyID)
                    || string.IsNullOrEmpty(model.BuyCompanyName)
                    || model.Adults + model.Childs + model.Leaders <= 0) return 0;
            }

            int flg = dal.UpdateTour(model);
            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("修改了计划,计划编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }


        /// <summary>
        /// 修改团队状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:修改成功 0：修改失败</returns>
        public int UpdateTourStatus(EyouSoft.Model.HTourStructure.MTourStatusChange model)
        {
            if (string.IsNullOrEmpty(model.TourId)
                || string.IsNullOrEmpty(model.CompanyId)
                || string.IsNullOrEmpty(model.OperatorId)
                || string.IsNullOrEmpty(model.Operator)
                || model.OperatorDeptId == 0) return 0;

            int flg = dal.UpdateTourStatus(model);

            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("将计划状态修改为{0},计划编号:{1}", model.TourStatus, model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());

                //接受任务时保留接受前的行程信息
                if (model.TourStatus == TourStatus.计调配置 && (model.IsJieShou.HasValue ? (!model.IsJieShou.Value) : true))
                {
                    var bModel = new EyouSoft.Model.TourStructure.MBianGeng
                    {
                        BianId = model.TourId,
                        BianType = EyouSoft.Model.EnumType.TourStructure.BianType.接受任务,
                        OperatorId = this.LoginUserId,
                        Url =
                            new EyouSoft.Toolkit.request(
                            Utils.AbsoluteWebRoot.AbsoluteUri + "printpage/xingchengdan.aspx?tourid=" + model.TourId,
                            1024,
                            768,
                            1024,
                            768,
                            this.LoginUserInfo.CompanyId,
                            HttpContext.Current.Request.Cookies).SavePageAsImg()
                    };
                    new BBianGeng().InsertBianGeng(bModel);
                }
            }
            return flg;
        }

        /// <summary>
        /// 修改团态标识、销售标识
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int UpdateTourMark(EyouSoft.Model.ComStructure.MGuidePlanWork m)
        {
            return this.dal.UpdateTourMark(m);
        }

        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SuccessDelTourIds">成功删除的计划编号列表</param>
        /// <param name="TourIds">计划编号列表</param>
        /// <returns></returns>
        public bool DeleteTour(string CompanyId, string[] TourIds)
        {
            List<string> SuccessDelTourIds = new List<string>();
            dal.DeleteTour(CompanyId, ref SuccessDelTourIds, TourIds);
            if (SuccessDelTourIds.Count > 0)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("删除了计划,计划编号:{0}", String.Join(",", SuccessDelTourIds.ToArray()));
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetTourModel(string TourIds)
        {
            if (string.IsNullOrEmpty(TourIds)) return null;
            return dal.GetTourModel(TourIds);

        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetPaituanModel(string TourIds)
        {
            if (string.IsNullOrEmpty(TourIds)) return null;
            return dal.GetPaituanModel(TourIds);

        }

        /// <summary>
        /// 获取线路信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetRouteInfoByTourId(string TourId)
        {
            if (string.IsNullOrEmpty(TourId)) return null;
            return dal.GetRouteInfoByTourId(TourId);
        }

        /// <summary>
        /// 获取派团给计调的信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTourToPlaner GetTourToPlaner(string TourId)
        {

            if (string.IsNullOrEmpty(TourId)) return null;
            return dal.GetTourToPlaner(TourId);
        }


        /// <summary>
        /// 派团给计调
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-1：计划已派团给计调  0:失败 1：成功</returns>
        public int SendTourToPlaner(EyouSoft.Model.HTourStructure.MTourToPlaner model)
        {
            if (string.IsNullOrEmpty(model.TourId)) return 0;
            if (model.TourPlanerList == null && model.TourPlanerList.Count == 0) return 0;
            if (model.TourPlanItemList == null && model.TourPlanItemList.Count == 0) return 0;

            int flg = dal.SendTourToPlaner(model);
            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("派团给计调,计划编号:{0}", model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }


        /// <summary>
        /// 团队确认
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-1：该计划已确认  0:失败 1：成功</returns>
        public int TourSure(EyouSoft.Model.HTourStructure.MTourSure model)
        {
            if (string.IsNullOrEmpty(model.TourId)) return 0;
            int flg = dal.TourSure(model);
            if (flg == 1)
            {
                //添加操作日志
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("计划确认:{0},计划编号:{1}", model.TourSureStatus, model.TourId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());

                //团队确认时保留确认前的行程信息
                var bModel = new EyouSoft.Model.TourStructure.MBianGeng
                    {
                        BianId = model.TourId,
                        BianType = EyouSoft.Model.EnumType.TourStructure.BianType.团队确认,
                        OperatorId = this.LoginUserId,
                        Url =
                            new EyouSoft.Toolkit.request(
                            Utils.AbsoluteWebRoot.AbsoluteUri + "printpage/xingchengdan.aspx?tourid=" + model.TourId,
                            1024,
                            768,
                            1024,
                            768,
                            this.LoginUserInfo.CompanyId,
                            HttpContext.Current.Request.Cookies).SavePageAsImg()
                    };
                new BBianGeng().InsertBianGeng(bModel);
            }

            return flg;
        }

        /// <summary>
        /// 获取计划列表(团队产品、特价产品)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MTourInfo> GetTourInfoList(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            EyouSoft.Model.HTourStructure.MTourSearch search)
        {
            return dal.GetTourInfoList(pageSize, pageIndex, ref recordCount, search);
        }
        public EyouSoft.Model.HTourStructure.MTourInfo GetTourInfoModel(string TourId)
        {
            if (string.IsNullOrEmpty(TourId)) return null;
            return dal.GetTourInfoModel(TourId);
        }

        /// <summary>
        /// 生成团号
        /// </summary>
        /// <param name="DeptId"></param>
        /// <param name="CompanyId"></param>
        /// <param name="TourType"></param>
        /// <param name="LDate"></param>
        /// <returns></returns>
        public string GenerateTourNo(int DeptId, string CompanyId, EyouSoft.Model.EnumType.TourStructure.TourType TourType, DateTime LDate, string crmId)
        {
            string TourNoSetting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CompanyId).TourNoSetting;
            IList<EyouSoft.Model.ComStructure.MTourNoOptionCode> list = new EyouSoft.BLL.ComStructure.BTourNoOptionCode().GetModel(CompanyId);
            string TourCode = "";

            if (string.IsNullOrEmpty(TourNoSetting))
            {
                TourNoSetting = "35";
                TourCode += LDate.ToString("yyMMdd");
                TourCode += dal.GetTourNum(CompanyId).ToString();
            }

            if (list != null && list.Count > 0)
            {
                TourCode = string.Empty;
                for (int i = 0; i < TourNoSetting.Length; i++)
                {
                    switch ((EyouSoft.Model.EnumType.ComStructure.OptionItemType)Convert.ToInt32(TourNoSetting.Substring(i, 1)))
                    {
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称:
                            {
                                TourCode += list.SingleOrDefault(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称).Code;
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称:
                            {
                                var item = list.SingleOrDefault(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称 && p.ItemId == DeptId.ToString());
                                if (item != null)
                                {
                                    TourCode += item.Code;
                                }
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型:
                            {
                                TourCode += list.SingleOrDefault(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型 && p.ItemId == ((int)TourType).ToString()).Code;
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.分隔符:
                            {
                                TourCode += "-";
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.序列号:
                            {
                                var pCrmId = string.Empty;
                                DateTime? pLDate = null;
                                if (TourNoSetting.Contains(((int)OptionItemType.客户简码).ToString()))
                                {
                                    pCrmId = crmId;
                                }
                                if (TourNoSetting.Contains(((int)OptionItemType.出团日期).ToString()))
                                {
                                    pLDate = LDate;
                                }
                                var seriesFormat = OptionItemTypeSeriesFormat.流水号;
                                var mTourNoOptionCode = list.SingleOrDefault(p => p.ItemType == OptionItemType.序列号);
                                if (mTourNoOptionCode != null)
                                {
                                    seriesFormat = (OptionItemTypeSeriesFormat)Convert.ToInt32(mTourNoOptionCode.Code);
                                }
                                TourCode += this.dal.GetTourNum(CompanyId, pCrmId, pLDate, seriesFormat).ToString();
                                break;
                            }
                        case EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期:
                            {
                                EyouSoft.Model.EnumType.ComStructure.OptionItemTypeLDateFormat OptionItemTypeLDateFormat = (EyouSoft.Model.EnumType.ComStructure.OptionItemTypeLDateFormat)Convert.ToInt32(list.SingleOrDefault(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期).Code);
                                if (OptionItemTypeLDateFormat == EyouSoft.Model.EnumType.ComStructure.OptionItemTypeLDateFormat.格式1)
                                {
                                    TourCode += LDate.ToString("yyMMdd");
                                }
                                else
                                {
                                    TourCode += LDate.ToString("yy-MM-dd");
                                }
                                break;
                            }
                        case OptionItemType.客户简码:
                            var m = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(crmId);
                            if (m != null && !string.IsNullOrEmpty(m.BrevityCode))
                            {
                                TourCode += m.BrevityCode;
                            }
                            break;
                    }
                }
            }

            if (string.IsNullOrEmpty(TourCode))
            {
                TourCode += LDate.ToString("yyMMdd");
                TourCode += dal.GetTourNum(CompanyId).ToString();
            }

            return TourCode;
        }




    }
}
