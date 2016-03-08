using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace EyouSoft.BLL.SysStructure
{
    /// <summary>
    /// 系统车座位模板
    /// </summary>
    public class BSysCarType
    {
        private readonly EyouSoft.IDAL.SysStructure.ISysCarType dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SysStructure.ISysCarType>();

        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="SeatNum"></param>
        /// <returns>1:成功，0：失败 2:该座位数的车型在系统中已存在</returns>
        public int AddSysCarType(int SeatNum)
        {
            return dal.AddSysCarType(SeatNum);
        }


        /// <summary>
        /// 添加车型模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:成功，0：失败 </returns>
        public bool AddSysCarType(EyouSoft.Model.SysStructure.MSysCarType model)
        {
            model.TemplateId = Guid.NewGuid().ToString();
            return dal.AddSysCarType(model) == 1 ? true : false;
        }


        /// <summary>
        /// 添加车型模板坐标
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <param name="list"></param>
        /// <returns>1:成功，0：失败</returns>
        public bool AddSysCarType(string TemplateId, IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list)
        {
            return dal.AddSysCarType(TemplateId, list) == 1 ? true : false;
        }


        /// <summary>
        /// 修改系统车位座位
        /// </summary>
        /// <returns></returns>
        public bool UpdateSysCarType(string TemplateId, IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list)
        {
            return dal.UpdateSysCarType(TemplateId, list);
        }



        /// <summary>
        /// 设置系统默认车型
        /// </summary>
        /// <param name="Id">车型编号</param>
        /// <param name="TemplateId">模板编号</param>
        /// <returns></returns>
        public bool UpdateSysCarType(int Id, string TemplateId)
        {
            return dal.UpdateSysCarType(Id, TemplateId);
        }


        /// <summary>
        /// 删除车型(即座位数)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1:成功，0：失败 2:该车型下的模板已有公司使用</returns>
        public int DeleteSysCarTypeSeatNum(int id)
        {
            return dal.DeleteSysCarTypeSeatNum(id);
        }


        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns>1:成功，0：失败 2:该模板已有公司使用</returns>
        public int DeleteSysCarTypeTempLate(string TemplateId)
        {
            return dal.DeleteSysCarTypeTempLate(TemplateId);
        }

        /// <summary>
        /// 获取车型模板
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysCarType> GetCarTypeList()
        {
            return dal.GetCarTypeList();
        }


        /// <summary>
        /// 根据座位的编号获取车型模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysCarType> GetCarTypeList(int id)
        {
            return dal.GetCarTypeList(id);
        }


        /// <summary>
        /// 根据模板编号获取车型模板
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public EyouSoft.Model.SysStructure.MSysCarType GetCarTypeById(string TemplateId)
        {
            return dal.GetCarTypeById(TemplateId);
        }


        /// <summary>
        /// 获取车型座位数
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> GetCarTypeSeatNumList()
        {
            return dal.GetCarTypeSeatNumList();
        }




    }
}
