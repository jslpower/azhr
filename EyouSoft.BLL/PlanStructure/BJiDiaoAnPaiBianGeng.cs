//计调安排变更相关业务逻辑类 汪奇志 2013-04-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.PlanStructure;

namespace EyouSoft.BLL.PlanStructure
{
    /// <summary>
    /// 计调安排变更相关业务逻辑类
    /// </summary>
    public class BJiDiaoAnPaiBianGeng
    {
        readonly EyouSoft.IDAL.PlanStructure.IJiDiaoAnPaiBianGeng dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PlanStructure.IJiDiaoAnPaiBianGeng>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BJiDiaoAnPaiBianGeng() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 获取计调安排变更信息业务实体
        /// </summary>
        /// <param name="anPaiId">安排编号</param>
        /// <param name="bianGengLeiXing">变更类型 daoyou||xiaoshou||jidiao</param>
        /// <param name="jiaJianLeiXing">加减类型 jia||jian</param>
        /// <returns></returns>
        public MJiDiaoAnPaiBianGengInfo GetInfo(string anPaiId, string bianGengLeiXing, string jiaJianLeiXing)
        {
            if (string.IsNullOrEmpty(anPaiId) || string.IsNullOrEmpty(bianGengLeiXing)) return null;

            //var _bianGengLeiXing = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.导游报账;
            //if (bianGengLeiXing == "xiaoshou") _bianGengLeiXing = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.销售报账;
            //else if (bianGengLeiXing == "jidiao") _bianGengLeiXing = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.计调报账;

            return dal.GetInfo(anPaiId, EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.计调报账, jiaJianLeiXing);
        }

        /// <summary>
        /// 获取计调安排变更相关信息业务实体
        /// </summary>
        /// <param name="anPaiId">安排编号</param>
        /// <returns></returns>
        public EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengXgInfo GetXgInfo(string anPaiId)
        {
            if (string.IsNullOrEmpty(anPaiId)) return null;

            return dal.GetXgInfo(anPaiId);
        }
        #endregion
    }
}
