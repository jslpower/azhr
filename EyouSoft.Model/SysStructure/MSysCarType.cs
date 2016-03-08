using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    /// <summary>
    /// 系统车位座位表模板实体
    /// </summary>
    [Serializable]
    public class MBaseSysCarType
    {
        /// <summary>
        ///车型编号 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateId { get; set; }


        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsDefault { get; set; }


        /// <summary>
        /// 模板图片
        /// </summary>
        public string FilePath { get; set; }
    }


    /// <summary>
    /// 车型座次
    /// </summary>
    [Serializable]
    public class MSysCarTypeSeat
    {

        /// <summary>
        /// 座位编号
        /// </summary>
        public int SeatNumber { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        public int PointX { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public int PoinY { get; set; }
    }


    /// <summary>
    /// 模板车型座次表
    /// </summary>
    [Serializable]
    public class MSysCarType : MBaseSysCarType
    {
        /// <summary>
        /// 车型座位数
        /// </summary>
        public int SeatNum { get; set; }

        /// <summary>
        /// 车型座次
        /// </summary>
        public IList<MSysCarTypeSeat> list { get; set; }
    }


    /// <summary>
    /// 车型座位数
    /// </summary>
    [Serializable]
    public class MSysCarTypeSeatNum
    {

        /// <summary>
        /// 车型编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 车型座位数
        /// </summary>
        public int SeatNum { get; set; }
    }



}
