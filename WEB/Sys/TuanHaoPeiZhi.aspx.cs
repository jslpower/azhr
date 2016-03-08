using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.Sys
{
    /// <summary>
    /// 团号配置
    /// </summary>
    /// 修改记录：
    /// 1、2011-4-26 曹胡生 创建
    public partial class TuanHaoPeiZhi : BackPage
    {
        //团队类型
        protected string TourTypeValueList;
        //部门
        protected string DepartValueList;
        //出团日期
        protected string LDateFormatList;
        //序列号
        protected string SeriesFormatList="null";
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            PageInit();
        }

        protected void Save()
        {
            IList<EyouSoft.Model.ComStructure.MTourNoOptionCode> TourNoOptionCodeList = new List<EyouSoft.Model.ComStructure.MTourNoOptionCode>();
            //生成的团号规则
            string GenTourCodeRule = string.Empty;

            string[] TourCodeRule = Utils.GetFormValues("ddl_Rule");
            for (int i = 0; i < TourCodeRule.Length; i++)
            {
                if ((EyouSoft.Model.EnumType.ComStructure.OptionItemType)Utils.GetInt(TourCodeRule[i]) == EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称)
                {
                    GenTourCodeRule += ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称).ToString();
                    if (TourNoOptionCodeList.Count(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称) == 0)
                    {
                        string[] depart = Utils.GetFormValues("inputDepartCode");
                        for (int j = 0; j < depart.Length; j++)
                        {
                            TourNoOptionCodeList.Add(new EyouSoft.Model.ComStructure.MTourNoOptionCode()
                            {
                                Code = depart[j],
                                CompanyId = SiteUserInfo.CompanyId,
                                ItemId = Utils.GetFormValues("inputDepartId")[j],
                                ItemType = EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称,
                                IssueTime = System.DateTime.Now
                            });
                        }
                    }
                }
                if ((EyouSoft.Model.EnumType.ComStructure.OptionItemType)Utils.GetInt(TourCodeRule[i]) == EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期)
                {
                    GenTourCodeRule += ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期).ToString();
                    if (TourNoOptionCodeList.Count(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期) == 0)
                    {
                        TourNoOptionCodeList.Add(new EyouSoft.Model.ComStructure.MTourNoOptionCode()
                        {
                            Code = (Utils.GetInt(Utils.GetFormValue("rad_LeaveDateCode"))).ToString(),
                            CompanyId = SiteUserInfo.CompanyId,
                            ItemId = (Utils.GetInt(Utils.GetFormValue("rad_LeaveDateCode"))).ToString(),
                            ItemType = EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期,
                            IssueTime = System.DateTime.Now
                        });
                    }
                }
                if ((EyouSoft.Model.EnumType.ComStructure.OptionItemType)Utils.GetInt(TourCodeRule[i]) == EyouSoft.Model.EnumType.ComStructure.OptionItemType.分隔符)
                {
                    GenTourCodeRule += ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.分隔符).ToString();
                    if (TourNoOptionCodeList.Count(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.分隔符) == 0)
                    {
                        TourNoOptionCodeList.Add(new EyouSoft.Model.ComStructure.MTourNoOptionCode()
                        {
                            Code = "-",
                            CompanyId = SiteUserInfo.CompanyId,
                            ItemId = ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.分隔符).ToString(),
                            ItemType = EyouSoft.Model.EnumType.ComStructure.OptionItemType.分隔符,
                            IssueTime = System.DateTime.Now
                        });
                    }
                }
                if ((EyouSoft.Model.EnumType.ComStructure.OptionItemType)Utils.GetInt(TourCodeRule[i]) == EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称)
                {
                    GenTourCodeRule += ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称).ToString();
                    if (TourNoOptionCodeList.Count(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称) == 0)
                    {
                        TourNoOptionCodeList.Add(new EyouSoft.Model.ComStructure.MTourNoOptionCode()
                        {
                            Code = txtCompanyCode.Text,
                            CompanyId = SiteUserInfo.CompanyId,
                            ItemId = SiteUserInfo.CompanyId,
                            ItemType = EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称,
                            IssueTime = System.DateTime.Now
                        });
                    }
                }
                if ((EyouSoft.Model.EnumType.ComStructure.OptionItemType)Utils.GetInt(TourCodeRule[i]) == EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型)
                {
                    GenTourCodeRule += ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型).ToString();
                    if (TourNoOptionCodeList.Count(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型) == 0)
                    {
                        string[] tourtype = Utils.GetFormValues("inputTourTypeCode");
                        for (int j = 0; j < tourtype.Length; j++)
                        {
                            TourNoOptionCodeList.Add(new EyouSoft.Model.ComStructure.MTourNoOptionCode()
                            {
                                Code = tourtype[j],
                                CompanyId = SiteUserInfo.CompanyId,
                                ItemId = (Utils.GetInt(Utils.GetFormValues("inputTourTypeValue")[j])).ToString(),
                                ItemType = EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型,
                                IssueTime = System.DateTime.Now
                            });
                        }
                    }
                }
                if ((EyouSoft.Model.EnumType.ComStructure.OptionItemType)Utils.GetInt(TourCodeRule[i]) == EyouSoft.Model.EnumType.ComStructure.OptionItemType.客户简码)
                {
                    GenTourCodeRule += ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.客户简码).ToString();
                    if (TourNoOptionCodeList.Count(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.客户简码) == 0)
                    {
                        TourNoOptionCodeList.Add(new EyouSoft.Model.ComStructure.MTourNoOptionCode()
                        {
                            CompanyId = SiteUserInfo.CompanyId,
                            ItemId = ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.客户简码).ToString(),
                            ItemType = EyouSoft.Model.EnumType.ComStructure.OptionItemType.客户简码,
                            IssueTime = System.DateTime.Now
                        });
                    }
                }
            }
            if (!string.IsNullOrEmpty(GenTourCodeRule))
            {
                GenTourCodeRule += ((int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.序列号).ToString();
                if (TourNoOptionCodeList.Count(p => p.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.序列号) == 0)
                {
                    TourNoOptionCodeList.Add(new EyouSoft.Model.ComStructure.MTourNoOptionCode()
                    {
                        Code = (Utils.GetInt(Utils.GetFormValue("rad_SeriesCode"))).ToString(),
                        CompanyId = SiteUserInfo.CompanyId,
                        ItemId = (Utils.GetInt(Utils.GetFormValue("rad_SeriesCode"))).ToString(),
                        ItemType = EyouSoft.Model.EnumType.ComStructure.OptionItemType.序列号,
                        IssueTime = System.DateTime.Now
                    });
                }
            }
            Response.Clear();
            if (new EyouSoft.BLL.ComStructure.BComSetting().UpdateTourCodeSet(CurrentUserCompanyID, GenTourCodeRule) && new EyouSoft.BLL.ComStructure.BTourNoOptionCode().Update(TourNoOptionCodeList))
            {

                Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功"));

            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "修改失败"));
            }
            Response.End();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_系统设置_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_系统设置_栏目, false);
                return;
            }
        }

        /// <summary>
        /// 初始化团号规则
        /// </summary>
        protected void PageInit()
        {
            EyouSoft.Model.ComStructure.MComSetting model = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            IList<EyouSoft.Model.ComStructure.MTourNoOptionCode> TourCodeItemList = new EyouSoft.BLL.ComStructure.BTourNoOptionCode().GetModel(SiteUserInfo.CompanyId);
            IList<EyouSoft.Model.ComStructure.MComDepartment> ComDepartmentList = new EyouSoft.BLL.ComStructure.BComDepartment().GetList(SiteUserInfo.CompanyId);
            if (!string.IsNullOrEmpty(model.TourNoSetting))
            {
                string[] str = new string[model.TourNoSetting.Length - 1];
                for (int i = 0; i < model.TourNoSetting.Length - 1; i++)
                {
                    str[i] = model.TourNoSetting.Substring(i, 1);
                }
                repItemList.DataSource = str;
                repItemList.DataBind();
            }
            else
            {
                repItemList.DataSource = new string[] { "0" };
                repItemList.DataBind();
            }
            repDepartList.DataSource = ComDepartmentList;
            repDepartList.DataBind();
            repTourTypeList.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourType));
            repTourTypeList.DataBind();

            System.Text.StringBuilder Depart = new System.Text.StringBuilder("[");
            System.Text.StringBuilder TourType = new System.Text.StringBuilder("[");
            System.Text.StringBuilder LDateFormat = new System.Text.StringBuilder("[");
            foreach (var item in TourCodeItemList)
            {
                if (item.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称)
                {

                    txtCompanyCode.Text = item.Code;
                }
                if (item.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称)
                {
                    Depart.AppendFormat("{0}\"ID\":\"{1}\",\"Value\":\"{2}\"{3},", "{", item.ItemId, item.Code, "}");
                }
                if (item.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型)
                {
                    TourType.AppendFormat("{0}\"ID\":\"{1}\",\"Value\":\"{2}\"{3},", "{", item.ItemId, item.Code, "}");
                }
                if (item.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期)
                {
                    LDateFormat.AppendFormat("{0}\"ID\":\"{1}\",\"Value\":\"{2}\"{3},", "{", item.ItemId, item.Code, "}");
                }
                if (item.ItemType == EyouSoft.Model.EnumType.ComStructure.OptionItemType.序列号)
                {
                    SeriesFormatList = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                }
            }
            Depart.Replace(",", "", Depart.Length - 1, 1);
            TourType.Replace(",", "", TourType.Length - 1, 1);
            LDateFormat.Replace(",", "", LDateFormat.Length - 1, 1);
            Depart.Append("]");
            TourType.Append("]");
            LDateFormat.Append("]");
            DepartValueList = Depart.ToString();
            TourTypeValueList = TourType.ToString();
            LDateFormatList = LDateFormat.ToString();
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.general;
        }
    }
}
