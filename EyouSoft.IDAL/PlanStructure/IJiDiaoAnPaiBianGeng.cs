//计调安排变更相关数据访问类接口 汪奇志 2013-04-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.PlanStructure;

namespace EyouSoft.IDAL.PlanStructure
{
    /// <summary>
    /// 计调安排变更相关数据访问类接口
    /// </summary>
    public interface IJiDiaoAnPaiBianGeng
    {
        /// <summary>
        /// 获取计调安排变更信息业务实体
        /// </summary>
        /// <param name="anPaiId">安排编号</param>
        /// <param name="bianGengLeiXing">变更类型</param>
        /// <param name="jiaJianLeiXing">加减类型</param>
        /// <returns></returns>
        MJiDiaoAnPaiBianGengInfo GetInfo(string anPaiId, EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass bianGengLeiXing, string jiaJianLeiXing);
        /// <summary>
        /// 获取计调安排变更相关信息业务实体
        /// </summary>
        /// <param name="anPaiId">安排编号</param>
        /// <returns></returns>
        MJiDiaoAnPaiBianGengXgInfo GetXgInfo(string anPaiId);
    }
}
