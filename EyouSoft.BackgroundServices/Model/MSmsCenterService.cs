using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.BackgroundServices
{

    #region Channel  短信发送通道

    /// <summary>
    /// 短信发送通道
    /// </summary>
    [Serializable]
    public enum Channel
    {
        /// <summary>
        /// 通用通道(6分/条)，到达率高
        /// </summary>
        通用通道 = 0,
        /// <summary>
        /// 广告通道(5分/条)
        /// </summary>
        广告通道 = 1
    }

    #endregion

    #region MobileType 短信接收号码类型

    /// <summary>
    /// 短信接收号码类型
    /// </summary>
    [Serializable]
    public enum MobileType
    {
        /// <summary>
        /// 移动,联通手机号码
        /// </summary>
        Mobiel,

        /// <summary>
        /// 小灵通号码
        /// </summary>
        Phs
    }

    #endregion

    #region 短信中心发送短信服务实体

    /// <summary>
    /// 短信中心发送短信服务实体
    /// </summary>
    /// 周文超 2011-09-14
    [Serializable]
    public class MSmsCenterService
    {
        /// <summary>
        /// 发送编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 短信账户编号
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// 发送通道
        /// </summary>
        public Channel Channel { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送费用
        /// </summary>
        public decimal SendAmount { get; set; }

        /// <summary>
        /// 单条价格
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 是否已发送（T为已发送，F为未发送）
        /// </summary>
        public bool IsSend { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 接收号码
        /// </summary>
        public IList<MSmsNumber> Number { get; set; }

    }

    #endregion

    #region 短信发送明细接收号码实体

    /// <summary>
    /// 短信发送明细接收号码实体
    /// </summary>
    /// 周文超 2011-09-13
    [Serializable]
    public class MSmsNumber
    {
        /// <summary>
        /// 号码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 号码类型
        /// </summary>
        public MobileType Type { get; set; }
    }

    #endregion

    #region 短信通道实体

    /// <summary>
    /// 短信通道实体
    /// </summary>
    public class MSmsChannel
    {
        /// <summary>
        /// 该通道短信通道索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 该通道短信通道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 该通道发送短信的用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 该通道发送短信的密码
        /// </summary>
        public string Pw { get; set; }
    }

    #endregion

    #region  短信通道实体集合

    /// <summary>
    /// 短信通道实体集合
    /// </summary>
    /// 周文超 2011-09-20
    public class MSmsChannelList
    {
        static List<MSmsChannel> _items = null;

        /// <summary>
        /// 默认构造方法
        /// </summary>
        public MSmsChannelList()
        {
            if (_items == null)
            {
                _items = new List<MSmsChannel>();

                var items = Toolkit.ConfigHelper.ConfigClass.GetConfigurationSecion("smsSettings") as List<EyouSoft.Toolkit.ConfigurationSectionHandler.SmsSettingInfo>;

                foreach (var item in items)
                {
                    _items.Add(new MSmsChannel()
                    {
                        ChannelName = item.Name,
                        Index = item.Index,
                        Pw = item.Password,
                        UserName = item.Username
                    });
                }
            }
        }

        /// <summary>
        /// 获得某个通道索引值下的通道实体,若不存在,则抛出异常
        /// </summary>
        /// <param name="index">通道索引值,从0开始</param>
        /// <returns></returns>
        public MSmsChannel this[int index]
        {
            get
            {
                var c = new MSmsChannel();
                if (_items == null || _items.Count < 1)
                {
                    c.Index = -1;
                    c.ChannelName = "未找到该通道";
                    c.Pw = string.Empty;
                    c.UserName = string.Empty;

                    return c;
                }

                c = _items.Find(item => item.Index == index) ?? new MSmsChannel
                {
                    Index = -1,
                    ChannelName = "未找到该通道",
                    Pw = string.Empty,
                    UserName = string.Empty
                };

                return c;
            }
        }

        /// <summary>
        /// 获得总的通道数量
        /// </summary>
        public int Count { get { return _items.Count; } }
    }

    #endregion
}
