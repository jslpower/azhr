using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SmsStructure
{
    #region 短信常用短语业务逻辑

    /// <summary>
    /// 短信常用短语业务逻辑
    /// </summary>
    /// 周文超 2011-09-13
    public class BSmsPhrase : BLLBase
    {
        private readonly IDAL.SmsStructure.ISmsPhrase _dal = Component.Factory.ComponentFactory.CreateDAL<IDAL.SmsStructure.ISmsPhrase>();

        /// <summary>
        /// 添加短信常用短语
        /// </summary>
        /// <param name="model">常用短语实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int AddSmsPhrase(Model.SmsStructure.MSmsPhrase model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId) || string.IsNullOrEmpty(model.Content)
                || (model.SmsPhraseType != null && model.SmsPhraseType.TypeId <= 0))
                return 0;

            return _dal.AddSmsPhrase(model);
        }

        /// <summary>
        /// 修改短信常用短语
        /// </summary>
        /// <param name="model">常用短语实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int UpdateSmsPhrase(Model.SmsStructure.MSmsPhrase model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId) || string.IsNullOrEmpty(model.Content)
                || (model.SmsPhraseType != null && model.SmsPhraseType.TypeId <= 0))
                return 0;

            return _dal.UpdateSmsPhrase(model);
        }

        /// <summary>
        /// 删除短信常用短语
        /// </summary>
        /// <param name="phraseIds">短信常用短语编号</param>
        /// <returns>返回1成功；其他失败</returns>
        public int DelSmsPhrase(params int[] phraseIds)
        {
            if (phraseIds == null || phraseIds.Length < 1)
                return 0;

            return _dal.DelSmsPhrase(phraseIds);
        }

        /// <summary>
        /// 获取常用短语信息
        /// </summary>
        /// <param name="phraseId">常用短语编号</param>
        /// <returns>返回常用短语信息</returns>
        public Model.SmsStructure.MSmsPhrase GetSmsPhrase(int phraseId)
        {
            if (phraseId <= 0)
                return null;

            return _dal.GetSmsPhrase(phraseId);
        }

        /// <summary>
        /// 获取短信常用短语列表
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">查询实体</param>
        /// <returns>返回常用短语信息集合</returns>
        public IList<Model.SmsStructure.MSmsPhrase> GetSmsPhraseList(int pageSize, int pageIndex, ref int recordCount
            , string companyId, Model.SmsStructure.MQuerySmsPhrase model)
        {
            if(string.IsNullOrEmpty(companyId))
                return null;

            return _dal.GetSmsPhraseList(pageSize, pageIndex, ref recordCount, companyId, model);
        }
    }

    #endregion

    #region 短信常用短语信息类别业务逻辑

    /// <summary>
    /// 短信常用短语信息类别业务逻辑
    /// </summary>
    /// 周文超 2011-09-13
    public class BSmsPhraseType : BLLBase
    {
        private readonly IDAL.SmsStructure.ISmsPhraseType _dal = Component.Factory.ComponentFactory.CreateDAL<IDAL.SmsStructure.ISmsPhraseType>();

        /// <summary>
        /// 添加常用短语信息类别
        /// </summary>
        /// <param name="model">常用短语信息类别实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int AddSmsPhraseType(Model.SmsStructure.MSmsPhraseType model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId))
                return 0;

            return _dal.AddSmsPhraseType(model);
        }

        /// <summary>
        /// 修改常用短语信息类别
        /// </summary>
        /// <param name="model">常用短语信息类别实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int UpdateSmsPhraseType(Model.SmsStructure.MSmsPhraseType model)
        {
            if (model == null || model.TypeId <= 0)
                return 0;

            return _dal.UpdateSmsPhraseType(model);
        }

        /// <summary>
        /// 删除常用短语信息类别
        /// </summary>
        /// <param name="typeIds">常用短语信息类别编号</param>
        /// <returns>返回1成功；其他失败</returns>
        public int DelSmsPhraseType(params int[] typeIds)
        {
            if (typeIds == null || typeIds.Length < 1)
                return 0;

            return _dal.DelSmsPhraseType(typeIds);
        }

        /// <summary>
        /// 获取常用短语信息类别
        /// </summary>
        /// <param name="typeId">常用短语信息类别编号</param>
        /// <returns>返回常用短语信息类别实体</returns>
        public Model.SmsStructure.MSmsPhraseType GetSmsPhraseType(int typeId)
        {
            if (typeId <= 0)
                return null;

            return _dal.GetSmsPhraseType(typeId);
        }


        /// <summary>
        /// 获取常用短语信息类别列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>返回常用短语信息类别集合</returns>
        public IList<Model.SmsStructure.MSmsPhraseType> GetSmsPhraseTypeList(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return null;

            return _dal.GetSmsPhraseTypeList(companyId);
        }
    }

    #endregion
}
