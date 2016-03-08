using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CrmStructure
{
    using Exception = System.Exception;

    /// <summary>
    /// 投诉管理
    /// 创建者:钱琦
    /// 时间:2011-10-1
    /// </summary>
    public class BCrmComplaint : BLLBase
    {

        #region dal对象
        EyouSoft.IDAL.CrmStructure.ICrmComplaint complaintDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CrmStructure.ICrmComplaint>();
        EyouSoft.IDAL.TourStructure.ITour tourDal=EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.ITour>();
        #endregion

        #region 投诉管理
        /// <summary>
        /// 添加投诉管理Model
        /// </summary>
        /// <param name="model">投诉管理Model</param>
        /// <param name="tourCode">团号</param>
        /// <returns>返回值  -2:公司编号未赋值 -3:团号未赋值 -5:投诉必填信息不完善 0 错误 1:正确</returns>
        public int AddCrmComplaintModel(Model.CrmStructure.MCrmComplaint model,string tourCode)
        {
            if (string.IsNullOrEmpty(tourCode))
            {
                return -5;
            }
            if (string.IsNullOrEmpty(model.ComplaintsName))
            {
                return -5;
            }
            if (string.IsNullOrEmpty(tourCode))
            {
                return -3;
            }
           
            if (string.IsNullOrEmpty(model.CompanyId))
            {
                return -2;
            }
            model.IssueTime = DateTime.Now;
            model.ComplaintsId = Guid.NewGuid().ToString();
            int result= complaintDal.AddCrmComplaintModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("添加质量管理投诉管理：投诉编号编号为：" + model.ComplaintsId + "团号：" + tourCode);
            }
            return result;
        }

        /// <summary>
        /// 获得投诉管理Model
        /// </summary>
        /// <param name="ComplaintsId">投诉管理编号</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrmComplaint GetCrmComplaintModel(string ComplaintsId)
        {
            if (string.IsNullOrEmpty(ComplaintsId))
                return null;
            return complaintDal.GetCrmComplaintModel(ComplaintsId);
        }

         /// <summary>
        /// 获得投诉管理显示列表页面上的数据
        /// </summary>
        /// <param name="model">投诉管理列表页面搜索Model</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MComplaintsListModel> GetVisitShowModel(Model.CrmStructure.MComplaintsSearchModel model, int pageIndex, int pageSize, ref int recordCount)
        {
            if (model == null)
                return null;
            if (string.IsNullOrEmpty(model.CompanyId))
                return null;
            return complaintDal.GetVisitShowModel(model, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <param name="complaintsId">投诉编号</param>
        /// <param name="handleName">处理人</param>
        /// <param name="handleTime">处理时间</param>
        /// <param name="handleOpinion">处理意见</param>
        /// <param name="isHandle">是否处理</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetComplaintDeal(string complaintsId, string handleName, DateTime handleTime, string handleOpinion, bool isHandle)
        {
            if (string.IsNullOrEmpty(complaintsId))
            {
                throw new Exception("投诉编号不能为空！");
            }
            var ok = this.complaintDal.SetComplaintDeal(complaintsId, handleName, handleTime, handleOpinion, isHandle);
            if (ok)
            {
                SysStructure.BSysLogHandle.Insert("处理了投诉编号编号为：" + complaintsId + "的投诉");                
            }
            return ok;
        }
       
        #endregion
    }
}
