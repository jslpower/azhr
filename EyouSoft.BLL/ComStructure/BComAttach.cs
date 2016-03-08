using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 附件
    /// 创建者：郑付杰
    /// 创建时间：2011/10/11
    /// </summary>
    public class BComAttach
    {
        private readonly EyouSoft.IDAL.ComStructure.IComAttach dal = 
            EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComAttach>();

        public BComAttach() { }

        #region IComAttach 成员
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="item">附件实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComAttach item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
            }
            return result;
        }
        /// <summary>
        /// 增加附件下载次数
        /// </summary>
        /// <param name="itemId">关联编号</param>
        /// <param name="itemType">关联类型</param>
        public void AddDownloads(string itemId, EyouSoft.Model.EnumType.ComStructure.AttachItemType itemType)
        {
            if (!string.IsNullOrEmpty(itemId))
            {
                dal.AddDownloads(itemId, itemType);
            }
        }
        /// <summary>
        /// 删除附件并添加到待删除附件列表
        /// </summary>
        /// <param name="item">附件实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(MComAttach item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Delete(item);
            }
            return result;
        }
        /// <summary>
        /// 批量修改已删除附件状态
        /// </summary>
        /// <param name="ids">文件编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool UpdateFileQue(params string[] ids)
        {
            StringBuilder id = new StringBuilder();

            for (int i = 0; i < ids.Length; i++)
            {
                id.Append(ids[i]);
                if (i + 1 < ids.Length)
                {
                    id.Append(",");
                }
            }

            return dal.UpdateFileQue(id.ToString());
        }
        /// <summary>
        /// 获取单个附件
        /// </summary>
        /// <param name="itemId">关联编号</param>
        /// <param name="itemType">关联类型</param>
        /// <returns>附件实体</returns>
        public MComAttach GetModel(string itemId, EyouSoft.Model.EnumType.ComStructure.AttachItemType itemType)
        {
            MComAttach item = null;
            if (!string.IsNullOrEmpty(itemId))
            {
                item = dal.GetModel(itemId, itemType);
            }
            return item;
        }
        /// <summary>
        /// 获取待删除附件列表
        /// </summary>
        /// <param name="num">获取数量</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>待删除附件列表</returns>
        public IList<MComDeletedFileQue> GetList(int? num, DateTime? startTime, DateTime? endTime)
        {
            return dal.GetList(num, startTime, endTime);
        }

        #endregion
    }
}
