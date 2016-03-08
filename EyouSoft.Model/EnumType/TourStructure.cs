//计划与订单相关枚举 2011-09-01 PM 曹胡生 创建
namespace EyouSoft.Model.EnumType.TourStructure
{
    #region 散拼计划收客状态
    /// <summary>
    /// 散拼计划收客状态
    /// </summary>
    public enum TourShouKeStatus
    {
        /// <summary>
        /// 报名中 = 0
        /// </summary>
        报名中 = 0,
        /// <summary>
        /// 自动客满 = 1
        /// </summary>
        自动客满 = 1,
        /// <summary>
        /// 自动停收 = 2
        /// </summary>
        自动停收 = 2,
        /// <summary>
        /// 手动客满=3
        /// </summary>
        手动客满 = 3,
        /// <summary>
        /// 手动停收=4
        /// </summary>
        手动停收 = 4
    }
    #endregion

    #region 计划状态
    /// <summary>
    /// 计划状态
    /// </summary>
    public enum TourStatus
    {
        /// <summary>
        /// 销售未派计划 = 0,
        /// </summary>
        销售未派计划 = 0,
        /// <summary>
        /// 计调未接收 = 1
        /// </summary>
        计调未接收 = 1,
        /// <summary>
        /// 计调配置 = 2
        /// </summary>
        计调配置 = 2,
        /// <summary>
        /// 导游带团 = 3
        /// </summary>
        导游带团 = 3,
        /// <summary>
        /// 导游报帐=4
        /// </summary>
        导游报帐 = 4,
        /// <summary>
        /// 导游报销=5
        /// </summary>
        导游报销 = 5,
        /// <summary>
        /// 财务待审=6
        /// </summary>
        财务待审 = 6,
        /// <summary>
        /// 单团核算 = 7
        /// </summary>
        单团核算 = 7,
        /// <summary>
        /// 封团 = 8
        /// </summary>
        封团 = 8,
        /// <summary>
        /// 已取消=9
        /// </summary>
        已取消 = 9,
    }
    #endregion

    #region 计划类型
    /// <summary>
    /// 计划类型
    /// </summary>
    public enum TourType
    {
        /// <summary>
        /// 组团团队
        /// </summary>
        团队产品 = 0,
        /// <summary>
        /// 自由行
        /// </summary>
        自由行 = 1,
        /// <summary>
        /// 散拼产品=2
        /// </summary>
        散拼产品=2,
        /// <summary>
        /// 单项业务=3
        /// </summary>
        单项业务=3,
        /// <summary>
        /// 散拼模版团
        /// </summary>
        散拼模版团 = 4,
        /// <summary>
        /// 线路产品=9
        /// </summary>
        线路产品=9
    }
    #endregion

    #region 计划对外报价类型
    /// <summary>
    /// 计划对外报价类型
    /// </summary>
    public enum TourQuoteType
    {
        /// <summary>
        /// 整团
        /// </summary>
        整团 = 0,
        /// <summary>
        /// 分项
        /// </summary>
        分项 = 1
    }
    #endregion

    #region 订单来源类型
    /// <summary>
    /// 订单来源类型
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 分销商下单=0
        /// </summary>
        分销商下单 = 0,
        /// <summary>
        /// 代客预定
        /// </summary>
        代客预定 = 1,
        /// <summary>
        /// 团队计划
        /// </summary>
        团队计划 = 2,
        /// <summary>
        /// 单项服务
        /// </summary>
        单项服务 = 3,
        /// <summary>
        /// 无计划散客=4
        /// </summary>
        无计划散客 = 4
    }
    #endregion

    #region 订单的收款\退款状态
    /// <summary>
    /// 订单的收款\退款状态
    /// </summary>
    public enum CollectionRefundState
    {
        /// <summary>
        /// 收款 = 0
        /// </summary>
        收款 = 0,
        /// <summary>
        /// 退款 = 1
        /// </summary>
        退款 = 1
    }
    #endregion

    #region 订单状态枚举
    /// <summary>
    /// 订单状态枚举
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        未处理 = 0,
        /// <summary>
        /// 已留位
        /// </summary>
        已留位 = 1,
        /// <summary>
        /// 留位过期
        /// </summary>
        留位过期 = 2,
        /// <summary>
        /// 不受理
        /// </summary>
        不受理 = 3,
        /// <summary>
        /// 已成交
        /// </summary>
        已成交 = 4,
        /// <summary>
        /// 已取消=5
        /// </summary>
        已取消 = 5,
        /// <summary>
        /// 垫付申请审核
        /// </summary>
        垫付申请审核 = 6,
        /// <summary>
        /// 垫付申请审核成功
        /// </summary>
        垫付申请审核成功 = 7,
        /// <summary>
        /// 垫付申请审核失败
        /// </summary>
        垫付申请审核失败 = 8,
        /// <summary>
        /// 资金超限
        /// </summary>
        资金超限 = 9


    }
    #endregion

    #region  辅助订单状态的枚举（用于供应商、分销商的订单状态）
    /// <summary>
    /// 辅助订单状态的枚举（用于供应商、分销商的订单状态）
    /// </summary>
    public enum GroupOrderStatus
    {
        预留未确认 = 0,
        预留过期 = 1,
        报名未确认 = 2,
        已留位 = 3,
        已确认 = 4,
        预留不受理 = 5,
        报名不受理 = 6,
        已取消 = 7
    }
    #endregion

    #region 订单游客输入方式
    /// <summary>
    /// 订单游客显示类型
    /// </summary>
    public enum CustomerDisplayType
    {
        /// <summary>
        /// 附件方式 = 0
        /// </summary>
        附件方式 = 0,
        /// <summary>
        /// 输入方式 = 1
        /// </summary>
        输入方式 = 1,
    }

    #endregion

    #region 游客类型

    /// <summary>
    /// 游客类型
    /// </summary>
    public enum VisitorType
    {
        /// <summary>
        /// 成人 = 0
        /// </summary>
        成人 = 0,
        /// <summary>
        /// 儿童 = 1
        /// </summary>
        儿童 = 1,
        /// <summary>
        /// 领队=2
        /// </summary>
        领队 = 2
    }
    #endregion

    #region 游客状态
    /// <summary>
    /// 游客状态
    /// </summary>
    public enum TravellerStatus
    {
        /// <summary>
        /// 在团 = 0
        /// </summary>
        在团 = 0,
        /// <summary>
        /// 退团 = 1
        /// </summary>
        退团 = 1
    }
    #endregion

    #region 游客证件类型枚举
    /// <summary>
    /// 游客证件类型枚举
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 未知
        /// </summary>
        未知 = 0,
        /// <summary>
        /// 身份证
        /// </summary>
        身份证 = 1,
        /// <summary>
        /// 军官证
        /// </summary>
        军官证 = 2,
        /// <summary>
        /// 台胞证
        /// </summary>
        台胞证 = 3,
        /// <summary>
        /// 港澳通行证
        /// </summary>
        港澳通行证 = 4,
        /// <summary>
        /// 户口本
        /// </summary>
        户口本 = 5,
        /// <summary>
        /// 大陆居民
        /// </summary>
        大陆居民 = 6,
        /// <summary>
        /// 往来港澳通行证
        /// </summary>
        往来港澳通行证 = 7,
        /// <summary>
        /// 往来台湾通行证
        /// </summary>
        往来台湾通行证 = 8,
        /// <summary>
        /// 因私护照
        /// </summary>
        因私护照 = 9
    }
    #endregion

    #region 游客签证状态
    /// <summary>
    /// 游客签证状态
    /// </summary>
    public enum VisaStatus
    {
        /// <summary>
        /// 未知=0
        /// </summary>
        未知 = 0,
        /// <summary>
        /// 材料收集中
        /// </summary>
        材料收集中 = 1,
        /// <summary>
        /// 材料已交待审
        /// </summary>
        材料已交待审 = 2,
        /// <summary>
        /// 签证成功
        /// </summary>
        签证成功 = 3,
        /// <summary>
        /// 拒签
        /// </summary>
        拒签 = 4
    }
    #endregion

    #region 团队计划报价枚举
    /// <summary>
    /// 团队计划报价状态
    /// </summary>
    public enum QuoteState
    {
        /// <summary>
        /// 未处理 = 0
        /// </summary>
        未处理 = 0,
        /// <summary>
        /// 报价成功 = 1
        /// </summary>
        报价成功 = 1,
        /// <summary>
        /// 取消报价 = 2
        /// </summary>
        取消报价 = 2,
        ///// <summary>
        ///// 垫付申请审核=3
        ///// </summary>
        //垫付申请审核 = 3,
        ///// <summary>
        ///// 审核失败=4
        ///// </summary>
        //审核失败 = 4,
        ///// <summary>
        ///// 审核成功=5
        ///// </summary>
        //审核成功 = 5
    }
    #endregion

    //#region 模块类型
    ///// <summary>
    ///// 模块类型
    ///// </summary>
    //public enum ModuleType
    //{
    //    /// <summary>
    //    /// 组团=0
    //    /// </summary>
    //    组团 = 0,
    //    /// <summary>
    //    /// 地接=1
    //    /// </summary>
    //    地接 = 1,
    //    /// <summary>
    //    /// 出境=2
    //    /// </summary>
    //    出境 = 2,
    //    /// <summary>
    //    /// 同业分销=3
    //    /// </summary>
    //    同业分销 = 3
    //}
    //#endregion

    //#region 判断超限时，人员类型
    ///// <summary>
    ///// 判断超限时，人员类型
    ///// </summary>
    //public enum PType
    //{
    //    /// <summary>
    //    /// 销售员=0
    //    /// </summary>
    //    销售员 = 1,
    //    /// <summary>
    //    /// 客户单位=1
    //    /// </summary>
    //    客户单位 = 2
    //}
    //#endregion

    //#region 订单修改、变更操作时的状态
    ///// <summary>
    ///// 订单修改、变更操作时的状态
    ///// </summary>
    //public enum ChangeType
    //{
    //    /// <summary>
    //    /// 修改 = 0
    //    /// </summary>
    //    修改 = 0,
    //    /// <summary>
    //    /// 变更 = 1
    //    /// </summary>
    //    变更 = 1
    //}
    //#endregion

    #region 同业分销查询订单类型（用于查询）
    /// <summary>
    /// 同业分销查询订单类型
    /// </summary>
    public enum OrderTypeBySearch
    {
        /// <summary>
        /// 全部订单
        /// </summary>
        全部订单 = 0,
        /// <summary>
        /// 我销售的订单
        /// </summary>
        我销售的订单 = 1,
        /// <summary>
        /// 我操作的订单
        /// </summary>
        我操作的订单 = 2

    }
    #endregion

    #region 供应商计划在分销商显示的发布人
    /// <summary>
    /// 供应商计划在分销商显示的发布人
    /// </summary>
    public enum ShowPublisher
    {
        /// <summary>
        /// 审核人=0
        /// </summary>
        审核人 = 0,
        /// <summary>
        /// 供应商=1
        /// </summary>
        供应商 = 1
    }
    #endregion

    #region 报账列表
    /// <summary>
    /// 报账列表
    /// </summary>
    public enum BZList
    {
        /// <summary>
        /// 导游报账=0
        /// </summary>
        导游报账 = 0,
        ///// <summary>
        ///// 销售报账=1
        ///// </summary>
        //销售报账 = 1,
        /// <summary>
        /// 计调报账=2
        /// </summary>
        计调报账 = 2,
        ///// <summary>
        ///// 计调终审=3
        ///// </summary>
        //计调终审 = 3,
        /// <summary>
        /// 报销=4
        /// </summary>
        报销 = 4,
        /// <summary>
        /// 单团核算=5
        /// </summary>
        单团核算 = 5,
        /// <summary>
        /// 报账
        /// </summary>
        报账 = 6
    }
    #endregion


    #region 车型变更的类型
    /// <summary>
    /// 车型变更的类型
    /// </summary>
    public enum CarChangeType
    {
        /// <summary>
        /// 车型变更
        /// </summary>
        上车地点变更 = 0,

        /// <summary>
        /// 座次变更
        /// </summary>
        车型座次变更 = 1
    }
    #endregion






    //--浙江海峡-------------------------------------------------
    /// <summary>
    /// 团型
    /// </summary>
    public enum TourMode
    {
        /// <summary>
        /// 购物团(团队)
        /// </summary>
        整团 = 0,

        /// <summary>
        /// 无购物团(团队)
        /// </summary>
        纯车 = 1,

        ///// <summary>
        ///// 十人已下团(散拼)
        ///// </summary>
        //十人以下团 = 2

    }

    /// <summary>
    /// 报价单文件类别
    /// </summary>
    public enum QuoteFileModel
    {
        /// <summary>
        /// 外语报价单
        /// </summary>
        外语报价单 = 0,
        /// <summary>
        /// 附件
        /// </summary>
        附件 = 1
    }

    /// <summary>
    /// 价格项目类型
    /// </summary>
    public enum Pricetype
    {
        /// <summary>
        /// 大交通
        /// </summary>
        飞机 = 0,
        /// <summary>
        /// 用车
        /// </summary>
        用车 = 1,
        /// <summary>
        /// 房1
        /// </summary>
        房1 = 2,
        /// <summary>
        /// 房2
        /// </summary>
        房2 = 3,
        /// <summary>
        /// 餐
        /// </summary>
        餐 = 4,
        /// <summary>
        /// 导服
        /// </summary>
        导服 = 5,
        /// <summary>
        /// 景点
        /// </summary>
        景点 = 6,
        /// <summary>
        /// 保险
        /// </summary>
        保险 = 7,
        /// <summary>
        /// 小交通
        /// </summary>
        小交通 = 8,
        /// <summary>
        /// 综费
        /// </summary>
        综费 = 9,
        /// <summary>
        /// 十六免一
        /// </summary>
        十六免一 = 10,
        /// <summary>
        /// 其他
        /// </summary>
        其他 = 11,
        火车=12,
        汽车=13,
        轮船=14
    }

    /// <summary>
    /// 价格单位
    /// </summary>
    public enum PriceUnit
    {
        /// <summary>
        /// 元_人
        /// </summary>
        元_人 = 0,
        /// <summary>
        /// 元_团
        /// </summary>
        元_团 = 1
    }

    /// <summary>
    /// 价格类型(成本价格、销售价格)
    /// </summary>
    public enum CostMode
    {
        /// <summary>
        /// 成本价格
        /// </summary>
        成本价格 = 0,
        /// <summary>
        /// 销售价格
        /// </summary>
        销售价格 = 1
    }

    /// <summary>
    /// 团队确认状态
    /// </summary>
    public enum TourSureStatus
    {
        /// <summary>
        /// 未确认
        /// </summary>
        未确认 = 0,
        /// <summary>
        /// 已确认
        /// </summary>
        已确认 = 1
    }

    /// <summary>
    /// 业务变更状态（0：销售未确认 1：销售暂不处理 2：销售已确认/计调未确认 3：计调已确认）
    /// </summary>
    public enum ChangeStatus
    {
        /// <summary>
        /// 销售未确认=0
        /// </summary>
        销售未确认 = 0,
        /// <summary>
        /// 销售暂不处理=1
        /// </summary>
        销售暂不处理 = 1,
        /// <summary>
        /// 销售已确认/计调未确认=2
        /// </summary>
        计调未确认 = 2,
        /// <summary>
        /// 计调已确认=3
        /// </summary>
        计调已确认 = 3
    }

    /// <summary>
    /// 业务变更类型(0 导游变更1 销售变更)
    /// </summary>
    public enum ChangeType
    {
        /// <summary>
        /// 导游变更=0
        /// </summary>
        导游变更 = 0,
        /// <summary>
        /// 销售变更=1
        /// </summary>
        销售变更 = 1
    }

    /// <summary>
    /// 确认人类型[0:销售员 1:计调员]
    /// </summary>
    public enum ConfirmerType
    {
        /// <summary>
        /// 销售员=0
        /// </summary>
        销售员 = 0,
        /// <summary>
        /// 计调员=1
        /// </summary>
        计调员 = 1
    }

    /// <summary>
    /// 编号类型
    /// </summary>
    public enum JourneyType
    {
        /// <summary>
        /// 行程亮点
        /// </summary>
        行程亮点 = 0,
        /// <summary>
        /// 行程备注
        /// </summary>
        行程备注 = 1,
        /// <summary>
        /// 报价备注
        /// </summary>
        报价备注 = 2
    }

    /// <summary>
    /// 变更类型
    /// </summary>
    public enum BianType
    {
        /// <summary>
        /// 团队确认时形成原始行程单
        /// </summary>
        团队确认 = 0,
        /// <summary>
        /// 接受任务形成原始行程单
        /// </summary>
        接受任务,
        /// <summary>
        /// 导游确认形成原始行程单
        /// </summary>
        导游确认,
    }
}
