using System;
using System.Collections.Generic;
using System.Linq;

namespace EyouSoft.SmsWeb.Model
{
    #region 短信发送通道信息业务实体
    /// <summary>
    /// 短信发送通道信息业务实体
    /// </summary>
    [Serializable]
    public class MSmsChannelInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsChannelInfo() { }

        /// <summary>
        /// 发送通道
        /// </summary>
        public Channel Cnannel { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单价(单位:元)
        /// </summary>
        public decimal Price { get; set; }
    }
    #endregion
}
