using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    /// <summary>
    /// 用户登录日志信息业务实体
    /// </summary>
    /// 汪奇志 2012-04-23
    public class MSysLogLoginInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSysLogLoginInfo() { }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

    }

    /// <summary>
    /// 用户登录日志查询信息业务实体
    /// </summary>
    /// 汪奇志 2012-04-23
    public class MSysLogLoginSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSysLogLoginSearchInfo() { }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录起始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 登录截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
    }
}
