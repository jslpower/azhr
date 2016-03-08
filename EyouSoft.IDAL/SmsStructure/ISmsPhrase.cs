using System.Collections.Generic;

namespace EyouSoft.IDAL.SmsStructure
{
    #region 短信常用短语数据接口

    /// <summary>
    /// 短信常用短语数据接口
    /// </summary>
    /// 周文超 2011-09-13
    public interface ISmsPhrase
    {
        /// <summary>
        /// 添加短信常用短语
        /// </summary>
        /// <param name="model">常用短语实体</param>
        /// <returns>返回1成功；其他失败</returns>
        int AddSmsPhrase(Model.SmsStructure.MSmsPhrase model);

        /// <summary>
        /// 修改短信常用短语
        /// </summary>
        /// <param name="model">常用短语实体</param>
        /// <returns>返回1成功；其他失败</returns>
        int UpdateSmsPhrase(Model.SmsStructure.MSmsPhrase model);

        /// <summary>
        /// 删除短信常用短语
        /// </summary>
        /// <param name="phraseIds">短信常用短语编号</param>
        /// <returns>返回1成功；其他失败</returns>
        int DelSmsPhrase(params int[] phraseIds);

        /// <summary>
        /// 获取常用短语信息
        /// </summary>
        /// <param name="phraseId">常用短语编号</param>
        /// <returns>返回常用短语信息</returns>
        Model.SmsStructure.MSmsPhrase GetSmsPhrase(int phraseId);

        /// <summary>
        /// 获取短信常用短语列表
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">查询实体</param>
        /// <returns>返回常用短语信息集合</returns>
        IList<Model.SmsStructure.MSmsPhrase> GetSmsPhraseList(int pageSize, int pageIndex, ref int recordCount
                                                              , string companyId,
                                                              Model.SmsStructure.MQuerySmsPhrase model);
    }

    #endregion

    #region 短信常用短语信息类别数据接口

    /// <summary>
    /// 短信常用短语信息类别数据接口
    /// </summary>
    /// 周文超 2011-09-13
    public interface ISmsPhraseType
    {
        /// <summary>
        /// 添加常用短语信息类别
        /// </summary>
        /// <param name="model">常用短语信息类别实体</param>
        /// <returns>返回1成功；其他失败</returns>
        int AddSmsPhraseType(Model.SmsStructure.MSmsPhraseType model);

        /// <summary>
        /// 修改常用短语信息类别
        /// </summary>
        /// <param name="model">常用短语信息类别实体</param>
        /// <returns>返回1成功；其他失败</returns>
        int UpdateSmsPhraseType(Model.SmsStructure.MSmsPhraseType model);

        /// <summary>
        /// 删除常用短语信息类别
        /// </summary>
        /// <param name="typeIds">常用短语信息类别编号</param>
        /// <returns>返回1成功；其他失败</returns>
        int DelSmsPhraseType(params int[] typeIds);

        /// <summary>
        /// 获取常用短语信息类别
        /// </summary>
        /// <param name="typeId">常用短语信息类别编号</param>
        /// <returns>返回常用短语信息类别实体</returns>
        Model.SmsStructure.MSmsPhraseType GetSmsPhraseType(int typeId);


        /// <summary>
        /// 获取常用短语信息类别列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>返回常用短语信息类别集合</returns>
        IList<Model.SmsStructure.MSmsPhraseType> GetSmsPhraseTypeList(string companyId);

    }

    #endregion
}
