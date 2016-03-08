using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HGysStructure
{
    #region 领队、司机信息业务实体
    /// <summary>
    /// 领队、司机信息业务实体
    /// </summary>
    public class MSiJiInfo
    {
        /// <summary>
        /// 领队、司机编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 领队、司机名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 领队、司机类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing LeiXing { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender Gender { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 评价类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.SiJiPingJiaLeiXing PingJiaLeiXing { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string PingJiaNeiRong { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 领队、司机查询信息业务实体
    /// <summary>
    /// 领队、司机查询信息业务实体
    /// </summary>
    public class MSiJiChaXunInfo
    {
        /// <summary>
        /// 领队、司机类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 领队、司机名称
        /// </summary>
        public string Name { get; set; }
    }
    #endregion
}
