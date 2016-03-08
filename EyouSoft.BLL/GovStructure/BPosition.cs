using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 职务管理BLL
    /// 2011-09-07 邵权江 创建
    /// </summary>
    public class BPosition
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IPosition dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IPosition>();

        #region  成员方法
        /// <summary>
        /// 判断职务信息是否存在
        /// </summary>
        /// <param name="PositionName">职务名称</param>
        /// <param name="Id">职务Id,新增Id=0</param>
        /// <returns></returns>
        public bool ExistsNum(string PositionName, int Id, string CompanyId)
        {
            if (!string.IsNullOrEmpty(PositionName) && Id >= 0 && !string.IsNullOrEmpty(CompanyId))
            {
                return dal.ExistsNum(PositionName, Id, CompanyId);
            }
            return false;
        }

        /// <summary>
        /// 增加一条职务信息
        /// </summary>
        /// <param name="model">职务model</param>
        /// <returns></returns>
        public bool AddGovPosition(EyouSoft.Model.GovStructure.MGovPosition model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.OperatorId))
            {
                int identity = dal.AddGovPosition(model);
                if (identity != 0)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条职务信息：编号为：" + identity);
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// 更新一条职务信息
        /// </summary>
        /// <param name="model">职务model</param>
        /// <returns></returns>
        public bool UpdateGovPosition(EyouSoft.Model.GovStructure.MGovPosition model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.OperatorId))
            {
                bool result = dal.UpdateGovPosition(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条职务信息：编号为：" + model.PositionId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得职务实体
        /// </summary>
        /// <param name="PositionId">职务ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovPosition GetGovPositionModel(int PositionId, string CompanyId)
        {
            EyouSoft.Model.GovStructure.MGovPosition model = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                model = new EyouSoft.Model.GovStructure.MGovPosition();
                return dal.GetGovPositionModel(PositionId, CompanyId);
            }
            return model;
        }

        /// <summary>
        /// 获得职务信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovPosition> GetGovPositionList(string CompanyId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovPosition> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovPosition>();
                return dal.GetGovPositionList(CompanyId, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据职务ID删除(需判断职务有无人员担任)
        /// </summary>
        /// <param name="PositionIds">职务ID</param>
        /// <returns>0或负值：失败，1成功，2职务正在使用</returns>
        public int DeleteGovPosition(params string[] PositionIds)
        {
            if (PositionIds.Length > 0)
            {
                int result = dal.DeleteGovPosition(PositionIds);
                if (result == 1)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < PositionIds.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", PositionIds[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除职务：编号为：" + sId.ToString());
                }
                return result;
            }
            return 0;
        }

        #endregion
    }
}
