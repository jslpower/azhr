using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HGysStructure
{
    /// <summary>
    /// 司机、领队相关业务逻辑类
    /// </summary>
    public class BSiJi
    {
        readonly EyouSoft.IDAL.HGysStructure.ISiJi dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HGysStructure.ISiJi>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BSiJi() { }
        #endregion

        #region public members
        /// <summary>
        /// 新增领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.HGysStructure.MSiJiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId)) return 0;

            info.GysId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.Insert(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增" + info.LeiXing + "信息，编号：" + info.GysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 更新领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.HGysStructure.MSiJiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                ||string.IsNullOrEmpty(info.GysId)) return 0;

            int dalRetCode = dal.Update(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改" + info.LeiXing + "信息，编号：" + info.GysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">领队、司机编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string gysId)
        {
            if (string.IsNullOrEmpty(companyId) 
                || string.IsNullOrEmpty(gysId)) return 0;

            var info = GetInfo(gysId);

            if (info == null) return -1;

            int dalRetCode = dal.Delete(companyId, gysId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除" + info.LeiXing + "信息，编号：" + gysId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取领队、司机信息业务实体
        /// </summary>
        /// <param name="gysId">领队、司机编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MSiJiInfo GetInfo(string gysId)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetInfo(gysId);
        }

        /// <summary>
        /// 获取领队、司机信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MSiJiInfo> GetSiJis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MSiJiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetSiJis(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }
        #endregion
    }
}
