using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 描述：系统车型模板接口类
    /// 修改记录：
    /// 1、2012-08-14 PM 王磊 创建
    /// </summary>
    public interface ISysCarType
    {
        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="SeatNum"></param>
        /// <returns>1:成功，0：失败 2:该座位数的车型在系统中已存在</returns>
        int AddSysCarType(int SeatNum);



        /// <summary>
        /// 添加车型模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:成功，0：失败 </returns>
        int AddSysCarType(EyouSoft.Model.SysStructure.MSysCarType model);


        /// <summary>
        /// 添加车型模板坐标
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <param name="list"></param>
        /// <returns>1:成功，0：失败</returns>
        int AddSysCarType(string TemplateId, IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list);


        /// <summary>
        /// 修改系统车位座位
        /// </summary>
        /// <returns></returns>
        bool UpdateSysCarType(string TemplateId, IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list);


        
        /// <summary>
        /// 设置系统默认车型
        /// </summary>
        /// <param name="Id">车型编号</param>
        /// <param name="TemplateId">模板编号</param>
        /// <returns></returns>
        bool UpdateSysCarType(int Id, string TemplateId);

        /// <summary>
        /// 删除座位数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteSysCarTypeSeatNum(int id);


        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        int DeleteSysCarTypeTempLate(string TemplateId);




        /// <summary>
        /// 获取车型模板
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.SysStructure.MSysCarType> GetCarTypeList();


        /// <summary>
        /// 根据座位的编号获取车型模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.SysStructure.MSysCarType> GetCarTypeList(int id);


        /// <summary>
        /// 根据模板编号获取车型模板
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        EyouSoft.Model.SysStructure.MSysCarType GetCarTypeById(string TemplateId);


        /// <summary>
        /// 获取车型座位数
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> GetCarTypeSeatNumList();

    }
}
