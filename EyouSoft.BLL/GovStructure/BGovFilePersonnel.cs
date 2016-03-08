using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 离职申请BLL 邵权江 2011-9-06
    /// </summary>
    public class BGovFilePersonnel
    {
        EyouSoft.IDAL.GovStructure.IGovFilePersonnel dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IGovFilePersonnel>();

        /// <summary>
        /// 添加离职信息/人事变动信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGovFilePersonnel(EyouSoft.Model.GovStructure.MGovFilePersonnel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.FileId) && !string.IsNullOrEmpty(model.OperatorID))
            {
                model.Id = Guid.NewGuid().ToString();
                if (model.GovPersonnelApproveList != null && model.GovPersonnelApproveList.Count > 0)
                {
                    for (int i = 0; i < model.GovPersonnelApproveList.Count; i++)
                    {
                        model.GovPersonnelApproveList[i].Id = model.Id;
                    }
                }
                bool result = dal.AddGovFilePersonnel(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条离职申请信息：编号为：" + model.Id);
                }
                return result;
            }
            return false;
        }
        /// <summary>
        /// 修改离职申请信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGovFilePersonnel(EyouSoft.Model.GovStructure.MGovFilePersonnel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Id) && !string.IsNullOrEmpty(model.FileId) && !string.IsNullOrEmpty(model.OperatorID))
            {
                if (model.GovPersonnelApproveList != null && model.GovPersonnelApproveList.Count > 0)
                {
                    for (int i = 0; i < model.GovPersonnelApproveList.Count; i++)
                    {
                        model.GovPersonnelApproveList[i].Id = model.Id;
                    }
                }
                bool result = dal.UpdateGovFilePersonnel(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条离职申请信息：编号为：" + model.Id);
                }
                return result;
            }
            return false;
        }
        /// <summary>
        /// 根据 离职ID 获取离职申请实体信息
        /// </summary>
        /// <param name="ID">离职ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovFilePersonnel GetGovFilePersonnelModel(string ID)
        {
            EyouSoft.Model.GovStructure.MGovFilePersonnel model = null;
            if (!string.IsNullOrEmpty(ID))
            {
                model = new EyouSoft.Model.GovStructure.MGovFilePersonnel();
                model = dal.GetGovFilePersonnelModel(ID);
            }
            return model;
        }
        /// <summary>
        /// 根据 档案ID 获取离职申请实体信息
        /// </summary>
        /// <param name="ID">档案ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovFilePersonnel GetGovFilePersonnelModelByFileId(string ID)
        {
            EyouSoft.Model.GovStructure.MGovFilePersonnel model = null;
            if (!string.IsNullOrEmpty(ID))
            {
                model = new EyouSoft.Model.GovStructure.MGovFilePersonnel();
                model = dal.GetGovFilePersonnelModelByFileId(ID);
            }
            return model;
        }
        /// <summary>
        /// 修改离职审批信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGovFilePersonnel(EyouSoft.Model.GovStructure.MGovPersonnelApprove model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Id) && !string.IsNullOrEmpty(model.ApproveID))
            {
                bool result = dal.AddGovFilePersonnel(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条离职审批信息：编号为：" + model.Id);
                }
                return result;
            }
            return false;
        }
        /// <summary>
        /// 更新离职状态
        /// </summary>
        /// <param name="DepartureTime">离职时间</param>
        /// <param name="Id">员工人事编号</param>
        /// <returns></returns>
        public bool UpdateIsLeft(DateTime DepartureTime, string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                bool result = dal.UpdateIsLeft(DepartureTime, Id);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新离职状态：员工人事编号为：" + Id);
                }
                return result;
            }
            return false;
        }
        /// <summary>
        /// 删除离职申请信息
        /// </summary>
        /// <param name="Ids">编号</param>
        /// <returns></returns>
        public bool DeleteGovFilePersonnel(params string[] Ids)
        {
            if (Ids.Length > 0)
            {
                bool result = dal.DeleteGovFilePersonnel(Ids);
                if (result)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", Ids[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除离职申请信息：编号为：" + sId.ToString());
                }
                return result;
            }
            return false;
        }
        /// <summary>
        /// 获取离职信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Name">员工姓名</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovFilePersonnel> GetGovFilePersonnelList(string CompanyId, string Name, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovFilePersonnel> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<EyouSoft.Model.GovStructure.MGovFilePersonnel>();
                return dal.GetGovFilePersonnelList(CompanyId, Name, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }       
    }
}
