using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.GysStructure
{
    #region 供应商类型
    /// <summary>
    /// 供应商类型
    /// </summary>
    public enum GysLeiXing
    {
        /// <summary>
        /// 地接社
        /// </summary>
        地接社 = 0,
        /// <summary>
        /// 酒店
        /// </summary>
        酒店 = 1,
        /// <summary>
        /// 餐馆
        /// </summary>
        餐馆 = 2,
        /// <summary>
        /// 车队
        /// </summary>
        车队 = 3,
        /// <summary>
        /// 区间交通
        /// </summary>
        区间交通 = 4,
        /// <summary>
        /// 景点
        /// </summary>
        景点 = 5,
        /// <summary>
        /// 购物
        /// </summary>
        购物 = 6,
        /// <summary>
        /// 其他
        /// </summary>
        其他 = 7,
        /// <summary>
        /// 物品
        /// </summary>
        物品 = 8,
        /// <summary>
        /// 领队
        /// </summary>
        领队 = 9,
        /// <summary>
        /// 司机
        /// </summary>
        司机 = 10
    }
    #endregion

    #region 酒店星级
    /// <summary>
    /// 酒店星级
    /// </summary>
    public enum JiuDianXingJi
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// MOTEL或两星级 = 1
        /// </summary>
        MOTEL或两星级 = 1,
        /// <summary>
        /// 三星级 = 2
        /// </summary>
        三星级 = 2,
        /// <summary>
        /// 四星级 = 3
        /// </summary>
        四星级 = 3,
        /// <summary>
        /// 四星半 = 4
        /// </summary>
        四星半 = 4,
        /// <summary>
        /// 五星级 = 5
        /// </summary>
        五星级 = 5,
        /// <summary>
        /// 超五星级或六星级=6
        /// </summary>
        超五星级或六星级=6,
    }
    #endregion

    #region 景点星级
    /// <summary>
    /// 景点星级
    /// </summary>
    public enum JingDianXingJi
    {
        /// <summary>
        /// None
        /// </summary>
        None=0,
        /// <summary>
        /// A
        /// </summary>
        A,
        /// <summary>
        /// AA
        /// </summary>
        AA,
        /// <summary>
        /// AAA
        /// </summary>
        AAA,
        /// <summary>
        /// AAAA
        /// </summary>
        AAAA,
        /// <summary>
        /// AAAAA
        /// </summary>
        AAAAA
    }
    #endregion

    #region 结算方式
    /// <summary>
    /// 结算方式
    /// </summary>
    public enum JieSuanFangShi
    {
        /// <summary>
        /// 现付
        /// </summary>
        现付 = 0,
        /// <summary>
        /// 挂账
        /// </summary>
        挂账
    }
    #endregion

    #region 酒店报价团型
    /// <summary>
    /// 酒店报价团型
    /// </summary>
    public enum JiuDianBaoJiaTuanXing
    {
        /// <summary>
        /// 团
        /// </summary>
        团,
        /// <summary>
        /// 散
        /// </summary>
        散
    }
    #endregion

    #region 供应商附件类型
    /// <summary>
    /// 供应商附件类型
    /// </summary>
    public enum GysFuJianLeiXing
    {
        /// <summary>
        /// None
        /// </summary>
        None=0,
        /// <summary>
        /// 酒店图片
        /// </summary>
        酒店图片,
        /// <summary>
        /// 景点图片
        /// </summary>
        景点图片
    }
    #endregion

    #region 领队、司机评价类型
    /// <summary>
    /// 领队、司机评价类型
    /// </summary>
    public enum SiJiPingJiaLeiXing
    {
        /// <summary>
        /// None
        /// </summary>
        None=0,
        /// <summary>
        /// 好
        /// </summary>
        好,
        /// <summary>
        /// 中
        /// </summary>
        中,
        /// <summary>
        /// 差
        /// </summary>
        差,
        /// <summary>
        /// 黑名单
        /// </summary>
        黑名单
    }
    #endregion

    #region  物品领用、发放、借阅类型
    /// <summary>
    /// 物品领用、发放、借阅类型
    /// </summary>
    public enum WuPinLingYongLeiXing
    {
        /// <summary>
        /// 领用
        /// </summary>
        领用 = 0,
        /// <summary>
        /// 发放
        /// </summary>
        发放,
        /// <summary>
        /// 借阅
        /// </summary>
        借阅
    }
    #endregion

    #region 物品借阅状态
    /// <summary>
    /// 物品借阅状态
    /// </summary>
    public enum WuPinJieYueStatus
    {
        /// <summary>
        /// 借阅中
        /// </summary>
        借阅中 = 0,
        /// <summary>
        /// 已归还
        /// </summary>
        已归还
    }
    #endregion

    /// <summary>
    /// 酒店报价房型
    /// </summary>
    public enum JiuDianBaoJiaRoomType
    {
        单人=1,
        双人=2,
        三人=3
    }

    /// <summary>
    /// 酒店报价价格类型
    /// </summary>
    public enum JiuDianBaoJiaPriceType
    {
        合同价,
        零售价
    }
}
