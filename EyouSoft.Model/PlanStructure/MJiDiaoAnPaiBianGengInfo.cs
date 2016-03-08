//计调安排变更相关信息业务实体 汪奇志 2013-04-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.PlanStructure
{
    #region 计调安排变更信息业务实体
    /// <summary>
    /// 计调安排变更信息业务实体
    /// </summary>
    public class MJiDiaoAnPaiBianGengInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiDiaoAnPaiBianGengInfo() { }

        /// <summary>
        /// 计调安排编号
        /// </summary>
        public string AnPaiId { get; set; }
        /// <summary>
        /// 变更类型
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass BianGengLeiXing { get; set; }
        /// <summary>
        /// 加减类型 jia||jian
        /// </summary>
        public string JiaJianLeiXing { get; set; }
        /// <summary>
        /// 变更人数
        /// </summary>
        public int RenShu { get; set; }
        /// <summary>
        /// 变更费用
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 变更备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 变更人数（decimal）
        /// </summary>
        public decimal DRenShu { get; set; }
    }
    #endregion

    #region 计调安排变更相关信息业务实体
    /// <summary>
    /// 计调安排变更相关信息业务实体
    /// </summary>
    public class MJiDiaoAnPaiBianGengXgInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiDiaoAnPaiBianGengXgInfo() { }

        /// <summary>
        /// 计调安排编号
        /// </summary>
        public string AnPaiId { get; set; }
        /// <summary>
        /// 计调安排费用明细
        /// </summary>
        public string FeiYongMingXi { get; set; }
        /// <summary>
        /// 计划销售员编号
        /// </summary>
        public string TourXiaShouYuanId { get; set; }
        /// <summary>
        /// 计划导游集合
        /// </summary>
        public IList<string> TourDaoYous { get; set; }
        /// <summary>
        /// 计划计调员集合
        /// </summary>
        public IList<string> TourJiDiaos { get; set; }
        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 计调安排支付方式
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.Payment ZhiFuFangShi { get; set; }
        /// <summary>
        /// 计调安排类型
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject AnPaiLeiXing { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 供应商联系人电话
        /// </summary>
        public string GysLxrTelephone { get; set; }
        /// <summary>
        /// 供应商联系人姓名
        /// </summary>
        public string GysLxrName { get; set; }
    }
    #endregion
}
