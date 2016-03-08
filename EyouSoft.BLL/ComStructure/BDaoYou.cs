using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.ComStructure
{
    using EyouSoft.Model.ComStructure;

    /// <summary>
    /// 导游档案业务逻辑类
    /// </summary>
    public class BDaoYou: BLLBase
    {
        readonly EyouSoft.IDAL.ComStructure.IDaoYou dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IDaoYou>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BDaoYou() { }
        #endregion

        #region public members
        /// <summary>
        /// 导游档案修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.ComStructure.MDaoYouInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.DaoYouId)) return 0;

            int dalRetCode = dal.CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改导游信息，编号：" + info.DaoYouId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取导游档案实体
        /// </summary>
        /// <param name="daoYouId">导游编号</param>
        /// <returns></returns>
        public EyouSoft.Model.ComStructure.MDaoYouInfo GetInfo(string daoYouId)
        {
            if (string.IsNullOrEmpty(daoYouId)) return null;

            return dal.GetInfo(daoYouId);
        }

        /// <summary>
        /// 获取导游导游信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MDaoYouInfo> GetDaoYous(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.ComStructure.MDaoYouChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetDaoYous(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 删除导游信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="daoYouId">导游编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string daoYouId)
        {
            if (string.IsNullOrEmpty(companyId) 
                || string.IsNullOrEmpty(daoYouId)) return 0;

            int dalRetCode = dal.Delete(companyId, daoYouId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除导游信息，编号：" + daoYouId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取导游带团明细信息集合
        /// </summary>
        /// <param name="daoYouId">导游编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXInfo> GetDaoYouDaiTuanXXs(string daoYouId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(daoYouId)) return null;

            return dal.GetDaoYouDaiTuanXXs(daoYouId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取导游上团统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MDaoYouShangTuanInfo> GetDaoYouShangTuanTongJi(string companyId, EyouSoft.Model.ComStructure.MDaoYouShangTuanChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            var items = dal.GetDaoYouShangTuanTongJi(companyId, chaXun);

            if (items != null && items.Count > 0)
            {
                if (chaXun != null)
                {
                    switch (chaXun.PaiXu)
                    {
                        case 1:
                            items = items.OrderBy(p => p.TuanDuiShu).ToList(); break;
                        case 2:
                            items = items.OrderByDescending(p => p.TuanTianShu).ToList(); break;
                        case 3:
                            items = items.OrderBy(p => p.TuanTianShu).ToList(); break;
                        default:
                            items = items.OrderByDescending(p => p.TuanDuiShu).ToList(); break;
                    }
                }
                else
                {
                    items = items.OrderByDescending(p => p.TuanDuiShu).ToList();
                }
            }

            return items;
        }

        /// <summary>
        /// 获得导游排班信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="year">查询年份</param>
        /// <param name="month">查询月份</param>
        /// <param name="NextTimeStart">查询下团时间开始</param>
        /// <param name="NextTimeEnd">查询下团时间结束</param>
        /// <param name="Location">下团地点</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<MGuidePlanWork> GetGuidePlanWork(string CompanyId, string GuideName, int year, int month, DateTime? NextTimeStart, DateTime? NextTimeEnd, string Location, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<MGuidePlanWork> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<MGuidePlanWork>();
                list = dal.GetGuidePlanWork(CompanyId, GuideName, year, month, NextTimeStart, NextTimeEnd, Location, pageIndex, pageSize, ref recordCount);
            }
            return list;
        }

        /// <summary>
        /// 获取排班统计列表
        /// </summary>
        /// <param name="CompanyId">系统公司编号</param>
        /// <param name="tourcode">团号</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="NextTimeStart">出团时间—开始</param>
        /// <param name="NextTimeEnd">出团时间—截至</param>
        /// <param name="tourmark">团态标识</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<MGuidePlanWork> GetPaiBanTongJi(string CompanyId, string tourcode, int year, int month, DateTime? NextTimeStart, DateTime? NextTimeEnd, string tourmark, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<MGuidePlanWork> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = dal.GetPaiBanTongJi(CompanyId, tourcode, year, month, NextTimeStart, NextTimeEnd, tourmark, pageIndex, pageSize, ref recordCount);
            }
            return list;
        }

        /// <summary>
        /// 获得导游当日排班详细信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideId">导游编号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public IList<MGuidePlanWork> GetGuidePlanWorkInfo(string CompanyId, string GuideId, DateTime date)
        {
            IList<MGuidePlanWork> list = null;
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(GuideId) && date != null)
            {
                list = new List<MGuidePlanWork>();
                list = dal.GetGuidePlanWorkInfo(CompanyId, GuideId, date);
            }
            return list;
        }

        #endregion
    }
}
