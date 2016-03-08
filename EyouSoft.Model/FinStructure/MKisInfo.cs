//金蝶相关信息业务实体 汪奇志 2012-05-08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.FinStructure
{
    /// <summary>
    /// 金蝶科目信息业务实体(webmaster 金蝶默认科目配置)
    /// </summary>
    public class MKisAccountGroupInfo
    {
        /// <summary>
        /// 科目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 科目代码
        /// </summary>
        public string Code { get; set; }
    }
}
