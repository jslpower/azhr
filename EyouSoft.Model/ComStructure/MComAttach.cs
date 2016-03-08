using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 附件信息实体
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_ComAttach")]
    public class MComAttach
    {
        /// <summary>
        /// 关联类型
        /// </summary>
        [Column(Name = "ItemType",DbType="tinyint")]
        public EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType { get; set; }
        /// <summary>
        /// 关联编号
        /// </summary>
        [Column(Name = "ItemId", DbType = "char(36)")]
        public string ItemId { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        [Column(Name = "Name", DbType = "nvarchar(255)")]
        public string Name { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        [Column(Name = "FilePath", DbType = "nvarchar(255)")]
        public string FilePath { get; set; }
        /// <summary>
        /// 附件大小(kb)
        /// </summary>
        [Column(Name = "Size", DbType = "int")]
        public int Size { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        [Column(Name = "Downloads", DbType = "int")]
        public int Downloads { get; set; }
    }

    /// <summary>
    /// 待删除文件实体
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_SysDeletedFileQue")]
    public class MComDeletedFileQue
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Column(Name = "ID", DbType = "int",IsPrimaryKey=true)]
        public int Id { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [Column(Name = "FilePath", DbType = "nvarchar(250)")]
        public string FilePath { get; set; }
        /// <summary>
        /// 状态(1:已处理 0:未处理)
        /// </summary>
        [Column(Name = "FileState", DbType = "tinyint")]
        public string FileState { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [Column(Name = "IssueTime", DbType = "datetime")]
        public DateTime IssueTime { get; set; }
    }
}
