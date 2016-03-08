using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    public class MSysRoom
    {
        /// <summary>
        /// 房型编号
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 操作人部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

    }

    #region 房型搜索实体
    /// <summary>
    /// 房型搜索实体
    /// </summary>
    public class MSysRoomSearchModel
    { }
    #endregion

}
