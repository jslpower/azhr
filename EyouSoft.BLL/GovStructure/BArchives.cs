using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 人事档案BLL 邵权江 2011-9-20
    /// </summary>
    public class BArchives
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IArchives dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IArchives>();
        /// <summary>
        /// 附件类型
        /// </summary>
        EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.ComStructure.AttachItemType), "人事档案");

        /// <summary>
        /// 判断身份证号是否存在
        /// </summary>
        /// <param name="IDNumber">身份证号</param>
        /// <param name="Id">档案Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsIDNumber(string IDNumber, string Id, string CompanyId)
        {
            if (!string.IsNullOrEmpty(IDNumber) && Id != null && !string.IsNullOrEmpty(CompanyId))
            {
                return dal.ExistsFileNumber(IDNumber, Id, CompanyId);
            }
            return false;
        }

        /// <summary>
        /// 判断档案编号是否存在
        /// </summary>
        /// <param name="FileNumber">档案编号</param>
        /// <param name="Id">档案Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsFileNumber(string FileNumber, string Id, string CompanyId)
        {
            if (!string.IsNullOrEmpty(FileNumber) && Id != null && !string.IsNullOrEmpty(CompanyId))
            {
                return dal.ExistsFileNumber(FileNumber, Id, CompanyId);
            }
            return false;
        }

        /// <summary>
        /// 添加档案信息
        /// </summary>
        /// <param name="model">档案实体</param>
        /// <param name="IsUser">是否用户</param>
        /// <param name="IsGuide">是否导游</param>
        /// <returns>-1：存在相同的身份证号。  -5：同步导游必须同步用户。  -6：导游信息不完整。  0：添加失败。  1： 添加成功。</returns>
        public int AddArchives(EyouSoft.Model.GovStructure.MGovFile model, bool IsUser, bool IsGuide)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.FileNumber) && !string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.IDNumber))
            {
                if (!string.IsNullOrEmpty(model.UserName) 
                    && new EyouSoft.BLL.ComStructure.BComUser().IsExistsUserName(model.UserName, model.CompanyId, model.UserId))
                    return -7;

                if (IsGuide && !IsUser)
                {
                    return -5;
                }
                model.ID = Guid.NewGuid().ToString();
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.ID;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                if (model.GovFilePositionList != null && model.GovFilePositionList.Count > 0)
                {
                    for (int i = 0; i < model.GovFilePositionList.Count; i++)
                    {
                        model.GovFilePositionList[i].FileId = model.ID;
                    }
                }
                if (model.GovFileEducationList != null && model.GovFileEducationList.Count > 0)
                {
                    for (int i = 0; i < model.GovFileEducationList.Count; i++)
                    {
                        model.GovFileEducationList[i].FileId = model.ID;
                    }
                }
                if (model.GovFileCurriculumList != null && model.GovFileCurriculumList.Count > 0)
                {
                    for (int i = 0; i < model.GovFileCurriculumList.Count; i++)
                    {
                        model.GovFileCurriculumList[i].FileId = model.ID;
                    }
                }
                if (model.GovFilehomeList != null && model.GovFilehomeList.Count > 0)
                {
                    for (int i = 0; i < model.GovFilehomeList.Count; i++)
                    {
                        model.GovFilehomeList[i].FileId = model.ID;
                    }
                }
                if (model.GovFileContractList != null && model.GovFileContractList.Count > 0)
                {
                    for (int i = 0; i < model.GovFileContractList.Count; i++)
                    {
                        model.GovFileContractList[i].FileId = model.ID;
                    }
                }
                if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
                {
                    model.MD5Password = new EyouSoft.Toolkit.DataProtection.HashCrypto().MD5Encrypt(model.Password);
                    if (IsUser)
                        model.UserId = Guid.NewGuid().ToString();
                    if (IsGuide)
                        model.GuideId = Guid.NewGuid().ToString();
                }
                int result = dal.AddArchives(model);
                if (result == 1)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条档案信息：编号为：" + model.ID);
                }
                return result;
            }
            return -6;
        }
        /// <summary>
        /// 修改档案信息
        /// </summary>
        /// <param name="model">档案实体</param>
        /// <param name="IsUser">是否用户</param>
        /// <param name="IsGuide">是否导游</param>
        /// <returns>-1：存在相同的身份证号。  -2:已经存在相同的用户名称。  -3:已经存在相同的身份证号的导游。  -4：用户名或密码不能为空。  -5：同步导游必须同步用户。  -6：导游信息不完整。  0：添加失败。  1： 添加成功。</returns>
        public int UpdateArchives(EyouSoft.Model.GovStructure.MGovFile model, bool IsUser, bool IsGuide)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.ID) && !string.IsNullOrEmpty(model.FileNumber) && !string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.IDNumber))
            {
                if (!string.IsNullOrEmpty(model.UserName)
                    && new EyouSoft.BLL.ComStructure.BComUser().IsExistsUserName(model.UserName, model.CompanyId, model.UserId))
                    return -7;

                if (IsGuide && !IsUser)
                {
                    return -5;
                }

                if (IsUser)
                {
                    if (string.IsNullOrEmpty(model.UserId))
                    {
                        if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
                        {
                            model.MD5Password = new EyouSoft.Toolkit.DataProtection.HashCrypto().MD5Encrypt(model.Password);
                            model.UserId = Guid.NewGuid().ToString();
                            if (string.IsNullOrEmpty(model.GuideId) && IsGuide)
                                model.GuideId = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            return -4;
                        }
                    }
                    else
                    {
                        model.UserName = null;
                        model.Password = null;
                        if (string.IsNullOrEmpty(model.GuideId) && IsGuide)
                            model.GuideId = Guid.NewGuid().ToString();
                    }
                }

                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.ID;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                else
                {
                    model.ComAttachList = null;
                }
                if (model.GovFilePositionList != null && model.GovFilePositionList.Count > 0)
                {
                    for (int i = 0; i < model.GovFilePositionList.Count; i++)
                    {
                        model.GovFilePositionList[i].FileId = model.ID;
                    }
                }
                if (model.GovFileEducationList != null && model.GovFileEducationList.Count > 0)
                {
                    for (int i = 0; i < model.GovFileEducationList.Count; i++)
                    {
                        model.GovFileEducationList[i].FileId = model.ID;
                    }
                }
                if (model.GovFileCurriculumList != null && model.GovFileCurriculumList.Count > 0)
                {
                    for (int i = 0; i < model.GovFileCurriculumList.Count; i++)
                    {
                        model.GovFileCurriculumList[i].FileId = model.ID;
                    }
                }
                if (model.GovFilehomeList != null && model.GovFilehomeList.Count > 0)
                {
                    for (int i = 0; i < model.GovFilehomeList.Count; i++)
                    {
                        model.GovFilehomeList[i].FileId = model.ID;
                    }
                }
                if (model.GovFileContractList != null && model.GovFileContractList.Count > 0)
                {
                    for (int i = 0; i < model.GovFileContractList.Count; i++)
                    {
                        model.GovFileContractList[i].FileId = model.ID;
                    }
                }
                int result = dal.UpdateArchives(model, ItemType, IsUser, IsGuide);
                if (result == 1)
                {
                    SysStructure.BSysLogHandle.Insert("修改一条档案信息：编号为：" + model.ID);
                }
                return result;
            }
            return -6;
        }
        /// <summary>
        /// 根据档案id获取档案实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovFile GetArchivesModel(string ID)
        {
            EyouSoft.Model.GovStructure.MGovFile model = null;
            if (!string.IsNullOrEmpty(ID))
            {
                model = new EyouSoft.Model.GovStructure.MGovFile();
                model = dal.GetArchivesModel(ID, ItemType);
            }
            return model;
        }

        /// <summary>
        /// 根据用户id获取档案实体
        /// </summary>
        /// <param name="UserId">用户UserId</param>
        /// <returns>true:成功，false:失败</returns>
        public EyouSoft.Model.GovStructure.MGovFile GetArchivesModelByUserId(string UserId)
        {
            EyouSoft.Model.GovStructure.MGovFile model = null;
            if (!string.IsNullOrEmpty(UserId))
            {
                model = new EyouSoft.Model.GovStructure.MGovFile();
                model = dal.GetArchivesModel(UserId, ItemType);
            }
            return model;
        }

        /// <summary>
        /// 获取档案列表
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovFile> GetSearchArchivesList(EyouSoft.Model.GovStructure.MSearchGovFile model,string CompanyId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovFile> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<EyouSoft.Model.GovStructure.MGovFile>();
                list= dal.GetSearchArchivesList(model, CompanyId, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }
        /*/// <summary>
        /// 删档案信息
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>0：成功     1：已经分配用户，不能删除     -1：失败</returns>
        public int DeleteArchives(params string[] Ids)
        {
            if (Ids.Length > 0)
            {
                int result = dal.DeleteArchives(ItemType, Ids);
                if (result == 0)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", Ids[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除档案信息：编号为：" + sId.ToString());
                }
                return result;
            }
            return -1;
        }*/

        /// <summary>
        /// 删除档案信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="dangAnId">档案编号</param>
        /// <returns></returns>
        public int DeleteArchives(string companyId, string dangAnId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(dangAnId)) return 0;

            int dalRetCode = dal.DeleteArchives(companyId, dangAnId);
            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除人事档案信息，档案编号：" + dangAnId);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取内部通讯录信息
        /// </summary>
        /// <param name="UserName">姓名</param>
        /// <param name="DepartIds">部门编号集合（如：1,2,3）</param>
        /// <param name="Department">部门</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovFile> GetSearchArchivesList(string UserName, string DepartIds, string Department, string CompanyId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovFile> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<EyouSoft.Model.GovStructure.MGovFile>();
                list = dal.GetSearchArchivesList(UserName, DepartIds, Department, CompanyId, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }
    }
}
