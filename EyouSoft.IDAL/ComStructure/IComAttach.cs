using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 附件接口
    /// 创建者：郑付杰
    /// 创建时间：2011/9/8
    /// </summary>
    public interface IComAttach
    {
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="item">附件实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Add(MComAttach item);
        /// <summary>
        /// 增加附件下载次数
        /// </summary>
        /// <param name="itemId">关联编号</param>
        /// <param name="itemType">关联类型</param>
        void AddDownloads(string itemId, EyouSoft.Model.EnumType.ComStructure.AttachItemType itemType);
        /// <summary>
        /// 删除附件并添加到待删除附件列表
        /// </summary>
        /// <param name="item">附件实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Delete(MComAttach item);
        /// <summary>
        /// 批量修改已删除附件状态
        /// </summary>
        /// <param name="ids">文件编号</param>
        /// <returns>true：成功 false：失败</returns>
        bool UpdateFileQue(string ids);
        /// <summary>
        /// 获取单个附件
        /// </summary>
        /// <param name="itemId">关联编号</param>
        /// <param name="itemType">关联类型</param>
        /// <returns>附件实体</returns>
        MComAttach GetModel(string itemId, EyouSoft.Model.EnumType.ComStructure.AttachItemType itemType);
        /// <summary>
        /// 获取待删除附件列表
        /// </summary>
        /// <param name="num">获取数量</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>待删除附件列表</returns>
        IList<MComDeletedFileQue> GetList(int? num, DateTime? startTime, DateTime? endTime);
    }
}
