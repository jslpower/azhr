//短信账户相关信息 汪奇志 2012-04-16
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SmsStructure
{
    #region 短信账户价格信息业务实体
    /// <summary>
    /// 短信账户价格信息业务实体
    /// </summary>
    public class MSmsPriceInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsPriceInfo() { }

        /// <summary>
        /// 短信通道
        /// </summary>
        public int ChannelIndex { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 通道价格
        /// </summary>
        public decimal Price { get; set; }
    }
    #endregion

    #region 短信通道信息业务实体
    /// <summary>
    /// 短信通道信息业务实体
    /// </summary>
    public class MSmsChannelInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsChannelInfo() { }

        /// <summary>
        /// 短信通道
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string Name { get; set; }
    }
    #endregion

    #region 短信账户信息业务实体
    /// <summary>
    /// 短信账户信息业务实体
    /// </summary>
    public class MSmsAccountInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsAccountInfo() { }

        /// <summary>
        /// 短信账户编号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal YuE { get; set; }
        /// <summary>
        /// 价格信息集合
        /// </summary>
        public IList<MSmsPriceInfo> Prices { get; set; }

    }
    #endregion
}
