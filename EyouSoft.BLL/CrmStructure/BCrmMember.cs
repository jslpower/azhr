using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CrmStructure
{
    /// <summary>
    /// 个人会员业务逻辑类
    /// </summary>
    public class BCrmMember : BLLBase
    {
        readonly EyouSoft.IDAL.CrmStructure.ICrmMember dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CrmStructure.ICrmMember>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BCrmMember() { }
        #endregion

        #region public members
        /// <summary>
        /// 写入个人会员信息，返回1成功
        /// </summary>
        /// <param name="info">个人会员信息业务实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.CrmStructure.MCrmPersonalInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId) || string.IsNullOrEmpty(info.OperatorId)) return 0;
            if (string.IsNullOrEmpty(info.Name)) return -1;

            info.CrmId = string.IsNullOrEmpty(info.CrmId) ? Guid.NewGuid().ToString() : info.CrmId;

            int dalRetCode = dal.Insert(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("添加个人会员信息，会员编号：" + info.CrmId + "，会员姓名：" + info.Name + "。");
                return 1;
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改个人会员信息，返回1成功
        /// </summary>
        /// <param name="info">个人会员信息业务实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.CrmStructure.MCrmPersonalInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId) || string.IsNullOrEmpty(info.OperatorId)) return 0;
            if (string.IsNullOrEmpty(info.Name)) return -1;
            if (string.IsNullOrEmpty(info.CrmId)) return -2;

            int dalRetCode = dal.Update(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改个人会员信息，会员编号：" + info.CrmId + "，会员姓名：" + info.Name + "。");
                return 1;
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取个人会员信息业务实体
        /// </summary>
        /// <param name="crmId">个人会员编号</param>
        /// <returns></returns>
        public EyouSoft.Model.CrmStructure.MCrmPersonalInfo GetInfo(string crmId)
        {
            if (string.IsNullOrEmpty(crmId)) return null;

            return dal.GetInfo(crmId);
        }

        /// <summary>
        /// 获取个人会员积分明细集合
        /// </summary>
        /// <param name="crmId">个人会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CrmStructure.MJiFenInfo> GetJiFens(string crmId, int pageSize, int pageIndex, ref int recordCount)
        {
            if (string.IsNullOrEmpty(crmId)) return null;

            return dal.GetJiFens(crmId, pageSize, pageIndex, ref recordCount);
        }

        /// <summary>
        /// 设置个人会员积分
        /// </summary>
        /// <param name="info">积分信息业务实体</param>
        /// <returns></returns>
        public bool SetJiFen(EyouSoft.Model.CrmStructure.MJiFenInfo info)
        {
            if (info == null) return false;

            info.IssueTime = DateTime.Now;

            if (dal.SetJiFen(info))
            {
                SysStructure.BSysLogHandle.Insert(string.Format("设置个人会员积分，会员编号：{0}，积分{1}{2}。", info.CrmId, info.ZengJianLeiBie, info.JiFen));

                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断是否存在相同的个人会员身份证号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">个人会员编号</param>
        /// <param name="idCardCode">身份证号</param>
        /// <returns></returns>
        public bool IsExistsIdCardCode(string companyId, string crmId, string idCardCode)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(idCardCode)) return true;

            return dal.IsExistsIdCardCode(companyId, crmId, idCardCode);
        }

        /// <summary>
        /// 判断是否存在相同的个人会员
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">个人会员编号</param>
        /// <returns></returns>
        public bool IsExists(string companyId, string crmId)
        {
            return dal.IsExists(companyId, crmId);
        }
        #endregion
    }
}
