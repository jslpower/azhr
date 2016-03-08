using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CrmStructure
{
    /// <summary>
    /// 团队回访
    /// 创建者:钱琦
    /// 时间:2011-10-1
    /// </summary>
    public class BCrmVisit : BLLBase
    {
        #region dal对象
        EyouSoft.IDAL.CrmStructure.ICrmVisit visitDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CrmStructure.ICrmVisit>();
        EyouSoft.IDAL.TourStructure.ITour tourDal=EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.ITour>();
        #endregion

        #region 团队回访
        /// <summary>
        /// 添加团队回访Model
        /// </summary>
        /// <param name="model">团队回访Model</param>
        /// <returns>返回值 -1:公司编号未赋值 -2:团队编号未赋值 -3:订单编号未赋值 -4:团队回访必填信息不完善 小于0:错误 1:正确</returns>
        public int AddCrmVisitModel(Model.CrmStructure.MCrmVisit model)
        {
           
            if (string.IsNullOrEmpty(model.CompanyId))
            {
                return -1;
            }
           
            if (string.IsNullOrEmpty(model.TourId))
            {
                return -2;
            }
            if (model.Identity ==null)
            {
                return -4;
            }
            if (model.ReturnType ==null)
            {
                return -4;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return -4;
            }
            model.IssueTime = DateTime.Now;
            model.VisitId = Guid.NewGuid().ToString();
            for (int i = 0; i < model.VisitDetailList.Count;i++ )
            {
                model.VisitDetailList[i].VisitId = model.VisitId;
            }
            int result= visitDal.AddCrmVisitModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("添加质量管理团队回访数据：团队回访编号："+model.VisitId+"团队编号："+model.TourId);
            }
            return result;
        }


         /// <summary>
        /// 修改团队回访Model
        /// </summary>
        /// <param name="model">团队回访Model</param>
        /// <returns></returns>
        public int UpdateCrmVisitModel(Model.CrmStructure.MCrmVisit model)
        {
            
            if (string.IsNullOrEmpty(model.CompanyId))
            {
                return -1;
            }

            if (string.IsNullOrEmpty(model.TourId))
            {
                return -2;
            }
            if (model.Identity == null)
            {
                return -4;
            }
            if (model.ReturnType == null)
            {
                return -4;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return -4;
            }
            model.IssueTime = DateTime.Now;
            for (int i = 0; i < model.VisitDetailList.Count; i++)
            {
                model.VisitDetailList[i].VisitId = model.VisitId;
            }
            int result = visitDal.AddCrmVisitModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改质量管理团队回访数据：团队回访编号：" + model.VisitId + "团队编号：" + model.TourId);
            }
            return result;
        }


        /// <summary>
        /// 获得显示在团队回访页面上的列表数据
        /// </summary>
        /// <param name="model">团队回访列表查询Model</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MVisitListModel> GetVisitShowModel(Model.CrmStructure.MVisitListModel model, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            if (model == null)
                return null;
            if (string.IsNullOrEmpty(model.CompanyId))
                return null;
            return visitDal.GetVisitShowModel(model,startDate,endDate, pageIndex, pageSize, ref recordCount);
        }


        /// <summary>
        /// 获得团队回访每日汇总表
        /// </summary>
        /// <param name="CompanyId">系统公司编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MDayTotalModel> GetDayTotalModelList(string CompanyId, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, ref int recordCount)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return null;
            return visitDal.GetDayTotalModelList(CompanyId, startTime, endTime, pageIndex, pageSize, ref recordCount);
        }
        /// <summary>
        /// 获取回访明细
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="startTime">回访开始时间</param>
        /// <param name="endTime">回访结束时间</param>
        /// <returns>回访明细集合</returns>
        public IList<Model.CrmStructure.MCrmVisit> GetCrmVisit(int pageIndex, int pageSize, ref int recordCount, string companyId, string tourId, DateTime? startTime, DateTime? endTime)
        {
            IList<Model.CrmStructure.MCrmVisit> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = visitDal.GetCrmVisit(pageIndex, pageSize, ref recordCount,companyId,tourId, startTime,endTime);
            }
            return list;
        }

        /// <summary>
        /// 获得团队回访Model
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="visitId">回访编号</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrmVisit GetVisitModel(string tourId,string visitId)
        {
            return visitDal.GetVisitModel(tourId,visitId);
        }
        #endregion
    }
}
