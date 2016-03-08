using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 物品管理BLL
    /// 2011-09-07 邵权江 创建
    /// </summary>
    public class BGood
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IGood dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IGood>();

        #region  成员方法
        /// <summary>
        /// 判断物品是否存在
        /// </summary>
        /// <param name="Name">物品名称</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GoodId">物品Id,新增Id=""</param>
        /// <returns></returns>
        public bool ExistsNum(string Name, string GoodId, string CompanyId)
        {
            if (!string.IsNullOrEmpty(Name) && GoodId != null && !string.IsNullOrEmpty(CompanyId))
            {
                return dal.ExistsNum(Name, GoodId, CompanyId);
            }
            return false;
        }

        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="model">物品model</param>
        public bool AddGovGood(Model.GovStructure.MGovGood model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Name) && model.Number > 0 && !string.IsNullOrEmpty(model.OperatorId) && model.Time != null)
            {
                model.GoodId = Guid.NewGuid().ToString();
                bool result = dal.AddGovGood(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条物品信息：编号为：" + model.GoodId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 更新物品
        /// </summary>
        /// <param name="model">物品model</param>
        /// <returns></returns>
        public bool UpdateGovGood(Model.GovStructure.MGovGood model)
        {
            if (model != null && !string.IsNullOrEmpty(model.GoodId) && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Name) && model.Number > 0 && !string.IsNullOrEmpty(model.OperatorId) && model.Time != null)
            {
                bool result = dal.UpdateGovGood(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条物品信息：编号为：" + model.GoodId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得物品实体
        /// </summary>
        /// <param name="GoodId">物品编号ID</param>
        /// <param name="CompanyId">公司编号ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovGood GetGovGoodModel(string GoodId, string CompanyId)
        {
            EyouSoft.Model.GovStructure.MGovGood model=null;
            if (!string.IsNullOrEmpty(GoodId) && !string.IsNullOrEmpty(CompanyId))
            {
                model = new EyouSoft.Model.GovStructure.MGovGood();
                model = dal.GetGovGoodModel(GoodId, CompanyId);
            }
            return model;
        }

        /// <summary>
        /// 根据条件获得物品信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Name">物品名称</param>
        /// <param name="TimeBegin">开始时间</param>
        /// <param name="TimeEnd">结束时间</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovGoodList> GetGovGoodList(string CompanyId, string Name, DateTime? TimeBegin, DateTime? TimeEnd, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovGoodList> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovGoodList>();
                list = dal.GetGovGoodList(CompanyId, Name, TimeBegin, TimeEnd, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据物品编号删除
        /// </summary>
        /// <param name="GoodIds">物品编号ID</param>
        /// <returns>0或负值：失败，1成功，2已被借阅未归还，不能删除</returns>
        /// <returns></returns>
        public int DeleteGovGood(params string[] GoodIds)
        {
            if (GoodIds.Length > 0)
            {
                int result = dal.DeleteGovGood(GoodIds);
                if (result == 1)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < GoodIds.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", GoodIds[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除公司合同：编号为：" + sId.ToString());
                }
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 根据物品编号获取库存
        /// </summary>
        /// <param name="GoodIds">物品编号ID</param>
        /// <returns></returns>
        public int GetGovGoodNum(string GoodId)
        {
            if (!string.IsNullOrEmpty(GoodId))
            {
                return dal.DeleteGovGood(GoodId);
            }
            return 0;
        }

        /// <summary>
        /// 增加物品领用/发放/借阅
        /// </summary>
        /// <param name="model">物品领用/发放/借阅model</param>
        /// <returns>正值1:成功； 负值或0:失败；2:超过库存</returns>
        public int AddGovGoodUse(Model.GovStructure.MGovGoodUse model)
        {
            if (model != null && !string.IsNullOrEmpty(model.GoodId) && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.UserId) && model.DeptId > 0 && model.Number > 0 && !string.IsNullOrEmpty(model.OperatorId) && model.Time != null)
            {
                model.UseId = Guid.NewGuid().ToString();
                int result = dal.AddGovGoodUse(model);
                if (result == 1)
                {
                    SysStructure.BSysLogHandle.Insert("增加物品领用/发放/借阅：编号为：" + model.UseId);
                }
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 批量增加物品领用/发放/借阅
        /// </summary>
        /// <param name="list">物品领用/发放/借阅model列表</param>
        /// <returns>0：成功； 正值：超过库存的物品数量； -1：失败</returns>
        public int AddGovGoodUseList(IList<EyouSoft.Model.GovStructure.MGovGoodUse> list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null && !string.IsNullOrEmpty(list[i].CompanyId) && !string.IsNullOrEmpty(list[i].GoodId) && !string.IsNullOrEmpty(list[i].UserId) && list[i].DeptId > 0 && list[i].Number > 0 && !string.IsNullOrEmpty(list[i].OperatorId) && list[i].Time != null)
                    {
                        list[i].UseId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        return -1;
                    }
                }
                int result = dal.AddGovGoodUseList(list);
                if (result==0)
                {
                    foreach (Model.GovStructure.MGovGoodUse model in list)
                    {
                        SysStructure.BSysLogHandle.Insert("增加物品领用/发放/借阅：编号为：" + model.UseId);
                    }
                }
                return result;
            }
            return -1;
        }

        /// <summary>
        /// 物品领用/发放/借阅信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GoodId">物品编号</param>
        /// <param name="GoodUseType">使用类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovGoodUseList> GetGovUseGoodList(string CompanyId, string GoodId, int GoodUseType, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovGoodUseList> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovGoodUseList>();
                return dal.GetGovUseGoodList(CompanyId, GoodId, GoodUseType, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据借阅编号归还
        /// </summary>
        /// <param name="IDs">借阅编号ID</param>
        /// <returns>0或负值：失败，1成功</returns>
        public int ReturnGovGoodBorrow(params string[] IDs)
        {
            if (IDs.Length > 0)
            {
                int result = dal.ReturnGovGoodBorrow(IDs);
                if (result == 1)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < IDs.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", IDs[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("归还借阅物品：编号为：" + sId.ToString());
                }
                return result;
            }
            return 0;
        }

        #endregion  成员方法
    }
}
