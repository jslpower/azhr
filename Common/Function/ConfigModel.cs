using System;
using System.Collections.Specialized;
using System.Configuration;
namespace Adpost.Common.Function
{
    /// <summary>
    /// Web.config 操作类
    ///  Copyright (C) 2006-2008 ChenZhiRen(Adpost) All Right Reserved.
    /// 定义为不可继承性
    /// typeof(System.Configuration.NameValueFileSectionHandler).Assembly.FullName.ToString()
    /// </summary>
    public sealed class ConfigModel
    {
        #region 通用获取指定节点值
        /// <summary>
        /// 取得配置文件中的字符串KEY
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名</param>
        /// <returns>返回KEY值</returns>
        public static string GetConfigString(string SectionName, string key)
        {
            string returnVal = "";
            if (SectionName != "")
            {
                try
                {
                    var cfgName = (NameValueCollection)ConfigurationSettings.GetConfig(SectionName);
                    //NameValueCollection cfgName = (NameValueCollection)ConfigurationManager.GetSection(SectionName);
                    if (cfgName[key] != null)
                    {
                        returnVal = cfgName[key];
                    }
                    cfgName = null;
                }catch
                {}
            }
            return returnVal;
        }
        /// <summary>
        /// 得到配置文件中的配置decimal信息
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名称</param>
        /// <returns>返回浮点数</returns>
        public static decimal GetConfigDecimal(string SectionName, string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(SectionName, key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToDecimal(cfgVal);
                try
                {
                result = decimal.Parse(cfgVal);
                }
                catch(FormatException)
                {
                  //格式化错误
                }
            }
            return result;
        }
        /// <summary>
        /// 得到配置文件中的配置int信息
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名</param>
        /// <returns>返回整数</returns>
        public static int GetConfigInt(string SectionName, string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(SectionName, key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToInt32(cfgVal);
                try
                {
                    result = Int32.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误.
                }
            }
            return result;
        }
        /// <summary>
        /// 得到配置文件中的配置int信息
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名</param>
        /// <returns>返回布尔值</returns>
        public static bool GetConfigBoolean(string SectionName, string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(SectionName, key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToInt32(cfgVal);
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误.
                }
            }
            return result;
        }
        #endregion        
        #region 获取AppSetting节点值
        /// <summary>
        /// 取得默认节点的配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingString(string key)
        {
            string returnVal = "";
            NameValueCollection appSettings = ConfigurationSettings.AppSettings;
            //NameValueCollection appSettings = ConfigurationManager.AppSettings;
            if (appSettings[key] != null)
            {
                returnVal = appSettings[key];
            }
            appSettings = null;
            return returnVal;
        }
        /// <summary>
        /// 得到配置文件中的默认节点配置int信息
        /// </summary>
        /// <param name="key">KEY名</param>
        /// <returns>返回整数</returns>
        public static int GetAppSettingInt(string key)
        {
            int result = 0;
            string cfgVal = GetAppSettingString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToInt32(cfgVal);
                try
                {
                    result = Int32.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误.
                }
            }
            return result;
        }
        /// <summary>
        /// 取得配置文件中 默认节点的 浮点数型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetAppSettingDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetAppSettingString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToDecimal(cfgVal);
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误
                }
            }
            return result;
        }
        /// <summary>
        /// 得到配置文件中的配置int信息
        /// </summary>
        /// <param name="key">KEY名</param>
        /// <returns>返回布尔值</returns>
        public static bool GetAppSettingBoolean(string key)
        {
            bool result = false;
            string cfgVal = GetAppSettingString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                //result = Convert.ToInt32(cfgVal);
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    //格式化错误.
                }
            }
            return result;
        }
        #endregion
        #region 获取 ConnectionStrings节点值
        /// <summary>
        /// 取得ConnectionStrings节点的配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            string returnVal = "";
            //ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;
            //if (null != connectionStrings[key])
            //{
            //    returnVal = connectionStrings[key].ConnectionString;
            //}
            //connectionStrings = null;
            return returnVal;
        }
        #endregion
        #region 节点管理
        #endregion
    }
}
