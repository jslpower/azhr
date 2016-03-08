using System;

namespace EyouSoft.Model.SmsStructure
{
    #region 短信常用短语类型

    /// <summary>
    /// 短信常用短语类型基类
    /// </summary>
    public class MSmsPhraseTypeBase
    {
        /// <summary>
        /// 类型编号
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }

    /// <summary>
    /// 短信常用短语类型
    /// </summary>
    /// 周文超 2011-09-13
    public class MSmsPhraseType : MSmsPhraseTypeBase
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }

    #endregion

    #region 短信常用短语

    /// <summary>
    /// 短信常用短语
    /// </summary>
    /// 周文超 2011-09-13
    public class MSmsPhrase
    {
        /// <summary>
        /// 短语编号
        /// </summary>
        public int PhraseId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 常用短语类型
        /// </summary>
        public MSmsPhraseTypeBase SmsPhraseType { get; set; }

        /// <summary>
        /// 短语内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }

    #endregion

    #region 短信常用短语查询实体

    /// <summary>
    /// 短信常用短语查询实体
    /// </summary>
    /// 周文超 2011-09-13
    public class MQuerySmsPhrase
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 常用短语类型
        /// </summary>
        public MSmsPhraseTypeBase SmsPhraseType { get; set; }
    }

    #endregion
}
