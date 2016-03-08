using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Cache.Tag
{
    /// <summary>
    /// 缓存标签
    /// </summary>
    public static class TagName
    {
        /// <summary>
        /// 系统域名 XZ/SYS/DOMAIN/{0}
        /// </summary>
        public const string SysDomain = "XZ/SYS/DOMAIN/{0}";
        /// <summary>
        /// 系统域名集合
        /// </summary>
        public const string SysDomains = "XZ/SYS/DOMAINS";
        /// <summary>
        /// 系统城市 XZ/SYS/CITY
        /// </summary>
        //public const string SysCity = "XZ/SYS/CITY";
        /// <summary>
        /// 登录用户 XZ/COM/{0}/USER/{1}
        /// </summary>
        public const string ComUser = "XZ/COM/{0}/USER/{1}";
        /// <summary>
        /// 公司部门 XZ/COM/{0}/DEPT
        /// </summary>
        public const string ComDept = "XZ/COM/{0}/DEPT";
        /// <summary>
        /// 公司配置 XZ/COM/{0}/SETTING
        /// </summary>
        public const string ComSetting = "XZ/COM/{0}/SETTING";
        /// <summary>
        /// 公司菜单 XZ/SYS/{0}/MENU
        /// </summary>
        public const string ComMenu = "XZ/SYS/{0}/MENU";
        /// <summary>
        /// 系统默认国家XZ/SYS/COUNTRY
        /// </summary>
        //public const string ComDefCountry = "XZ/SYS/COUNTRY";
        /// <summary>
        /// 公司常用城市XZ/COM/{0}/CITY
        /// </summary>
        //public const string ComCity = "XZ/COM/{0}/CITY";
        /// <summary>
        /// 系统二级栏目及权限XZ/SYS/MENU2S
        /// </summary>
        public const string SysMenu2s = "XZ/SYS/MENU2S";
        /// <summary>
        /// 金蝶科目 XZ/FIN/KINGDEESUBJECT
        /// </summary>
        public const string KingDeeSubject = "XZ/FIN/KINGDEESUBJECT";
        /// <summary>
        /// 金蝶核算项目 XZ/FIN/KINGDEECHKITEM
        /// </summary>
        public const string KingDeeChkItem = "XZ/FIN/KINGDEECHKITEM";
        /// <summary>
        /// 公司报价标准
        /// </summary>
        public const string BaoJiaBiaoZhun = "XZ/COM/{0}/BAOJIABIAOZHUN";
        /// <summary>
        /// 公司客户等级
        /// </summary>
        public const string KeHuDengJi = "XZ/COM/{0}/KEHUDENGJI";
        /// <summary>
        /// 系统维护
        /// </summary>
        public const string SysWeiHu = "XZ/SYS/WEIHU";
        /// <summary>
        /// 公司国家 XZ/COM/{0}/COUNTRY
        /// </summary>
        public const string ComCountry = "XZ/COM/{0}/COUNTRY";
    }
}
