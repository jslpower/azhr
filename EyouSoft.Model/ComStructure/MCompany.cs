using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 公司(系统)信息
    /// </summary>
    [Serializable]
    public class MCompany
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 系统编号
        /// </summary>
        public string SysId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 旅行社类别
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 许可证号
        /// </summary>
        public string License { get; set; }
        /// <summary>
        /// 公司负责人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 公司网站url
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 外部logo
        /// </summary>
        public string WLogo { get; set; }
        /// <summary>
        /// 系统内部logo
        /// </summary>
        public string NLogo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 公司银行帐号信息
        /// </summary>
        public IList<MComAccount> ComAccount { get; set; }
        /// <summary>
        /// 手机端logo
        /// </summary>
        public string MLogo { get; set; }
        /// <summary>
        /// 供应商端logo
        /// </summary>
        public string GYSLogo { get; set; }
        /// <summary>
        /// 分销商端logo
        /// </summary>
        public string FXSLogo { get; set; }
    }
}
