using System;
using System.Collections.Specialized;
using System.Configuration;
namespace Adpost.Common.Function
{
    /// <summary>
    /// Web.config ������
    ///  Copyright (C) 2006-2008 ChenZhiRen(Adpost) All Right Reserved.
    /// ����Ϊ���ɼ̳���
    /// typeof(System.Configuration.NameValueFileSectionHandler).Assembly.FullName.ToString()
    /// </summary>
    public sealed class ConfigModel
    {
        #region ͨ�û�ȡָ���ڵ�ֵ
        /// <summary>
        /// ȡ�������ļ��е��ַ���KEY
        /// </summary>
        /// <param name="SectionName">�ڵ�����</param>
        /// <param name="key">KEY��</param>
        /// <returns>����KEYֵ</returns>
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
        /// �õ������ļ��е�����decimal��Ϣ
        /// </summary>
        /// <param name="SectionName">�ڵ�����</param>
        /// <param name="key">KEY����</param>
        /// <returns>���ظ�����</returns>
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
                  //��ʽ������
                }
            }
            return result;
        }
        /// <summary>
        /// �õ������ļ��е�����int��Ϣ
        /// </summary>
        /// <param name="SectionName">�ڵ�����</param>
        /// <param name="key">KEY��</param>
        /// <returns>��������</returns>
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
                    //��ʽ������.
                }
            }
            return result;
        }
        /// <summary>
        /// �õ������ļ��е�����int��Ϣ
        /// </summary>
        /// <param name="SectionName">�ڵ�����</param>
        /// <param name="key">KEY��</param>
        /// <returns>���ز���ֵ</returns>
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
                    //��ʽ������.
                }
            }
            return result;
        }
        #endregion        
        #region ��ȡAppSetting�ڵ�ֵ
        /// <summary>
        /// ȡ��Ĭ�Ͻڵ������
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
        /// �õ������ļ��е�Ĭ�Ͻڵ�����int��Ϣ
        /// </summary>
        /// <param name="key">KEY��</param>
        /// <returns>��������</returns>
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
                    //��ʽ������.
                }
            }
            return result;
        }
        /// <summary>
        /// ȡ�������ļ��� Ĭ�Ͻڵ�� ��������
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
                    //��ʽ������
                }
            }
            return result;
        }
        /// <summary>
        /// �õ������ļ��е�����int��Ϣ
        /// </summary>
        /// <param name="key">KEY��</param>
        /// <returns>���ز���ֵ</returns>
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
                    //��ʽ������.
                }
            }
            return result;
        }
        #endregion
        #region ��ȡ ConnectionStrings�ڵ�ֵ
        /// <summary>
        /// ȡ��ConnectionStrings�ڵ������
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
        #region �ڵ����
        #endregion
    }
}
