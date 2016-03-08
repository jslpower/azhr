using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.Toolkit.DAL
{
    using System.Xml;

    /// <summary>
    /// 数据层访问基类
    /// 读取配置文件，生成数据库可用连接
    /// </summary>
    public class DALBase
    {
        private readonly Database _systemstore = DatabaseFactory.CreateDatabase("SystemStore");
        private readonly Database _smsstore = DatabaseFactory.CreateDatabase("SmsStore");


        /// <summary>
        /// 系统库
        /// </summary>
        protected Database SystemStore
        {
            get
            {
                return _systemstore;
            }
        }

        /// <summary>
        /// 短信库
        /// </summary>
        protected Database SmsStore
        {
            get { return _smsstore; }
        }

        /// <summary>
        /// 数据库CHAR(1)转换成布尔类型，1→true 其它→false
        /// </summary>
        /// <param name="s">CHAR(1)</param>
        /// <returns></returns>
        public bool GetBoolean(string s)
        {
            return s == "1" ? true : false;
        }

        /// <summary>
        /// 将bool转换成char(1) true:1 false:0
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string GetBooleanToStr(bool s)
        {
            return s ? "1" : "0";
        }

        /// <summary>
        /// 将整形Id数组转换为半角逗号分割的字符串
        /// </summary>
        /// <param name="ids">整形Id数组</param>
        /// <returns>半角逗号分割的字符串</returns>
        protected string GetIdsByArr(params int[] ids)
        {
            /*string strIds = string.Empty;
            if (ids == null || ids.Length < 1)
                return strIds;

            strIds = ids.Where(strId => strId > 0).Aggregate(strIds, (current, strId) => current + (strId + ","));

            return strIds.Trim(',');*/

            if (ids == null || ids.Length < 1) return string.Empty;

            StringBuilder s = new StringBuilder();

            foreach (var item in ids)
            {
                s.Append(",");
                s.Append(item);
            }

            return s.ToString().Substring(1);
        }

        /// <summary>
        /// 根据当前操作者编号和部门编号集合获取组织机构浏览条件
        /// </summary>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="deptIds">部门集合</param>
        /// <returns>组织机构浏览条件</returns>
        protected string GetOrgCondition(string operatorId, ICollection<int> deptIds)
        {
            var str = string.Empty;

            if (!string.IsNullOrEmpty(operatorId) && deptIds!=null)
            {
                str = string.Format(" AND (OperatorId = '{0}'", operatorId);

                if (deptIds.Count > 0 && !deptIds.Contains(-1))
                {
                    str = str + deptIds.Aggregate(" OR OperatorDeptId IN (", (current, deptId) => current + string.Format("{0},", deptId)).TrimEnd(',') + ")";
                }
                str = str + ")";
            }

            return str;
        }

        /// <summary>
        /// 根据当前操作者编号和部门编号集合获取组织机构浏览条件
        /// </summary>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="deptIds">部门集合</param>
        /// <param name="sqlOperatorId">操作者编号字段</param>
        /// <param name="sqlDeptId">操作者部门编号字段</param>
        /// <returns>组织机构浏览条件</returns>
        protected string GetOrgCondition(string operatorId, ICollection<int> deptIds,string sqlOperatorId,string sqlDeptId)
        {
            var str = string.Empty;

            if (!string.IsNullOrEmpty(operatorId) && deptIds != null)
            {
                str = string.Format(" AND ({0} = '{1}'",sqlOperatorId, operatorId);

                if (deptIds.Count > 0 && !deptIds.Contains(-1))
                {
                    str = str + deptIds.Aggregate(string.Format(" OR {0} IN (",sqlDeptId), (current, deptId) => current + string.Format("{0},", deptId)).TrimEnd(',') + ")";
                }
                str = str + ")";
            }

            return str;
        }

        /// <summary>
        /// 根据金额汇总字段作成XML
        /// </summary>
        /// <param name="fields">金额汇总字段集合</param>
        /// <returns>金额汇总XML</returns>
        protected string CreateXmlSumByField(params string[] fields)
        {
            if (fields==null||fields.Length<=0)
            {
                return string.Empty;
            }
            var xml = new XmlDocument();
            var sbxml = new StringBuilder();

            sbxml.Append("<root>");
            foreach (var i in fields)
            {
                sbxml.Append("<row SumName=\"" + i + "\"/>");
            }
            sbxml.Append("</root>");
            xml.LoadXml(sbxml.ToString());
            return xml.InnerXml;
        }

        /// <summary>
        /// 获取XML文档属性值
        /// </summary>
        /// <param name="xml"></param>
        ///  <param name="attribute">属性</param>
        /// <returns></returns>
        protected string GetValueByXml(string xml, string attribute)
        {
            if (string.IsNullOrEmpty(xml)) return "";
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                return Utils.GetXAttributeValue(xRow, attribute);
            }
            return "";
        }
    }
}
