using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HGysStructure
{
    /// <summary>
    /// 供应商预存款相关业务逻辑类
    /// </summary>
    public class BYuCunKuan
    {
        readonly EyouSoft.IDAL.HGysStructure.IYuCunKuan dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HGysStructure.IYuCunKuan>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BYuCunKuan() { }
        #endregion

        #region private members
        /// <summary>
        /// 重置供应商预存款余额，返回1成功，其它失败
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        int ResetYuCunKuanYuE(string gysId)
        {
            if (string.IsNullOrEmpty(gysId)) return 0;

            return dal.ResetYuCunKuanYuE(gysId);
        }
        #endregion

        #region public members
        /// <summary>
        /// 新增预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.HGysStructure.MYuCunKuanInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.GysId)) return 0;
            info.YuCunId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.Insert(info);

            if (dalRetCode == 1)
            {
                ResetYuCunKuanYuE(info.GysId);

                SysStructure.BSysLogHandle.Insert("新增预存款信息，编号：" + info.YuCunId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 更新预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.HGysStructure.MYuCunKuanInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.GysId)
                ||string.IsNullOrEmpty(info.YuCunId)) return 0;

            int dalRetCode = dal.Update(info);

            if (dalRetCode == 1)
            {
                ResetYuCunKuanYuE(info.GysId);

                SysStructure.BSysLogHandle.Insert("更新预存款信息，编号：" + info.YuCunId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="yuCunId">预存款编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string gysId, string yuCunId)
        {
            if (string.IsNullOrEmpty(companyId) 
                || string.IsNullOrEmpty(gysId) 
                || string.IsNullOrEmpty(yuCunId)) return 0;

            int dalRetCode = dal.Delete(companyId, gysId, yuCunId);

            if (dalRetCode == 1)
            {
                ResetYuCunKuanYuE(gysId);

                SysStructure.BSysLogHandle.Insert("删除预存款信息，编号：" + yuCunId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取预存款信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MYuCunKuanInfo> GetYuCunKuans(string gysId)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetYuCunKuans(gysId);
        }
        #endregion
    }
}
