//导游报账、销售报账、计调报账变更操作窗口 汪奇志 2013-04-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.Web.CommonPage
{
    using System.Text;

    using EyouSoft.Model.ComStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;

    /// <summary>
    /// 导游报账、销售报账、计调报账变更操作窗口
    /// </summary>
    public partial class JiDiaoAnPaiBianGengEdit : BackPage
    {
        #region attributes
        /// <summary>
        /// 计调安排编号
        /// </summary>
        string AnPaiId=string.Empty;
        /// <summary>
        /// 变更类型
        /// </summary>
        string BianGengLeiXing = string.Empty;
        /// <summary>
        /// 加减类型
        /// </summary>
        string JiaJianLeiXing = string.Empty;
        /// <summary>
        /// 加减类型
        /// </summary>
        protected string JiaJianLeiXing1 = "增加";
        /// <summary>
        /// 导游报账权限
        /// </summary>
        //bool Privs_DaoYouBaoZhang = false;
        /// <summary>
        /// 计调报账权限
        /// </summary>
        //bool Privs_XiaoShouBaoZhang = false;
        /// <summary>
        /// 销售报账权限
        /// </summary>
        //bool Privs_JiDiaoBaoZhang = false;
        /// <summary>
        /// 跳过导游报账
        /// </summary>
        //bool IsTiaoGuoDaoYouBaoZhang = false;
        /// <summary>
        /// 跳过销售报账
        /// </summary>
        //bool IsTiaoGuoXiaoShouBaoZhang = false;
        /// <summary>
        /// 是否允许操作导游收支
        /// </summary>
        //bool IsCaoZuoDaoYouShouZhi = false;
        EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengXgInfo EditXgInfo = null;
        EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengInfo EditInfo = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AnPaiId = Utils.GetQueryStringValue("anpaiid");
            BianGengLeiXing = Utils.GetQueryStringValue("biangengleixing");
            JiaJianLeiXing = Utils.GetQueryStringValue("jiajianleixing");
            this.SingleFileUpload1.CompanyID = this.SiteUserInfo.CompanyId;

            if (string.IsNullOrEmpty(AnPaiId) 
                || string.IsNullOrEmpty(BianGengLeiXing) 
                || string.IsNullOrEmpty(JiaJianLeiXing)) RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常"));

            if (Utils.GetQueryStringValue("dotype") == "submit") BaoCun();

            if (JiaJianLeiXing == "jian") JiaJianLeiXing1 = "减少";

            InitEditInfo();
            InitPrivs();
        }

        #region private members
        /// <summary>
        /// 获取旧附件信息
        /// </summary>
        /// <returns></returns>
        private MComAttach OldGetAttach()
        {
            //之前上传的附件
            string stroldupload = Utils.GetFormValue("hideFileInfo");
            MComAttach attModel = new MComAttach() { ItemType = AttachItemType.费用变更, ItemId = AnPaiId, };
            if (!string.IsNullOrEmpty(stroldupload))
            {
                string[] oldupload = stroldupload.Split(',');
                if (oldupload != null && oldupload.Length > 0)
                {
                    if (!string.IsNullOrEmpty(oldupload[0]))
                    {
                        string[] uploaditem = oldupload[0].Split('|');
                        attModel.Name = uploaditem[0];
                        attModel.FilePath = uploaditem[1];
                    }
                }
            }
            return attModel;
        }

        /// <summary>
        /// 获取新附件信息
        /// </summary>
        /// <returns></returns>
        private MComAttach NewGetAttach()
        {
            MComAttach attModel = new MComAttach() { ItemType = AttachItemType.费用变更, ItemId = AnPaiId, };
            //新上传附件
            string[] upload = Utils.GetFormValues(this.SingleFileUpload1.ClientHideID);
            for (int i = 0; i < upload.Length; i++)
            {
                string[] newupload = upload[i].Split('|');
                if (newupload != null && newupload.Length > 1)
                {
                    attModel.FilePath = newupload[1];
                    attModel.Name = newupload[0];
                }
            }
            return attModel;
        }

        /// <summary>
        /// initprivs
        /// </summary>
        void InitPrivs()
        {
            //if (BianGengLeiXing=="daoyou") Privs_DaoYouBaoZhang = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_导游报账操作);
            //if (BianGengLeiXing == "xiaoshou") Privs_XiaoShouBaoZhang = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售报账_销售报账操作);
            //if (BianGengLeiXing == "jidiao") Privs_JiDiaoBaoZhang = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调报账_计调报账操作);

            //var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            //if (setting != null)
            //{
            //    IsTiaoGuoDaoYouBaoZhang = setting.SkipGuide;
            //    IsTiaoGuoXiaoShouBaoZhang = setting.SkipGuide;
            //}
            switch ((Menu2)Utils.GetInt(this.SL))
            {
                case Menu2.计调中心_计调报账:
                    if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调报账_计调报账操作) && EditXgInfo.TourStatus >= TourStatus.导游带团 && EditXgInfo.TourStatus<=TourStatus.导游报销)
                    {
                        ltrOperatorHtml.Text = "<a id=\"i_a_submit\" href=\"javascript:void(0);\" style=\"text-indent:0px;\">保存</a>"; 
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 初始化编辑信息
        /// </summary>
        void InitEditInfo()
        {
            EditInfo = new EyouSoft.BLL.PlanStructure.BJiDiaoAnPaiBianGeng().GetInfo(AnPaiId, BianGengLeiXing, JiaJianLeiXing);
            EditXgInfo = new EyouSoft.BLL.PlanStructure.BJiDiaoAnPaiBianGeng().GetXgInfo(AnPaiId);

            if (EditXgInfo == null) return;

            if (EditInfo != null)
            {
                //if (EditXgInfo.AnPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 || EditXgInfo.AnPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮)
                //{
                //    txtRenShu.Value = EditInfo.DRenShu.ToString("F2");
                //}
                //else
                //{
                    txtRenShu.Value = EditInfo.RenShu.ToString();
                //}
                txtJinE.Value = EditInfo.JinE.ToString("F2");
                txtBeiZhu.Value = EditInfo.BeiZhu;
            }

            //txtFeiYongMingXi.Value = EditXgInfo.FeiYongMingXi;
            ltrAnPaiLeiXing.Text = EditXgInfo.AnPaiLeiXing.ToString();
            txtAnPaiLeiXing.Value = ((int)EditXgInfo.AnPaiLeiXing).ToString();

            if (EditXgInfo.AnPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游)
            {
                ltrGysTitle.Text = "导游姓名";
                ltrGysName.Text = EditXgInfo.GysName + "&nbsp;&nbsp; 电话：" + EditXgInfo.GysLxrTelephone;
            }
            else
            {
                ltrGysName.Text = EditXgInfo.GysName + "&nbsp;&nbsp;联系人：" + EditXgInfo.GysLxrName + "&nbsp;&nbsp; 电话：" + EditXgInfo.GysLxrTelephone;
            }

            //InitDaoYouBaoZhangCaoZuo();
            //InitXiaoShouBaoZhangCaoZuo();
            //InitJiDiaoBaoZhangCaoZuo();

            #region 附件处理
            //附件
            var strFile = new StringBuilder();
            var m = new BLL.ComStructure.BComAttach().GetModel(AnPaiId, AttachItemType.费用变更);
            if (null != m)
            {
                strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={2}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"iPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", m.FilePath, m.Name, HttpUtility.UrlEncode(m.Name));
            }
            this.lbFiles.Text = strFile.ToString();//附件
            #endregion
        }

        ///// <summary>
        ///// 初始化导游报账操作
        ///// </summary>
        //void InitDaoYouBaoZhangCaoZuo()
        //{
        //    if (BianGengLeiXing != "daoyou") return;
        //    if (!Privs_DaoYouBaoZhang)
        //    {
        //        ltrOperatorHtml.Text = "你没有导游报账权限，不可导游报账。";
        //        return;
        //    }

        //    //已安排导游 非安排导游不可报账
        //    if (EditXgInfo.TourDaoYous != null && EditXgInfo.TourDaoYous.Count > 0)
        //    {
        //        if (!EditXgInfo.TourDaoYous.Contains(SiteUserInfo.UserId))
        //        {
        //            ltrOperatorHtml.Text = "非本团安排导游，不可导游报账。";
        //            return;
        //        }
        //    }
        //    else//未安排导游，团队销售员、计调员均可操作导游报账
        //    {
        //        if (EditXgInfo.TourXiaShouYuanId != SiteUserInfo.UserId && !EditXgInfo.TourJiDiaos.Contains(SiteUserInfo.UserId))
        //        {
        //            ltrOperatorHtml.Text = "非本团销售员或计调员，不可导游报账。";
        //            return;
        //        }
        //    }

        //    TourStatus[] status = { TourStatus.导游带团, TourStatus.导游报帐 };
        //    if (!status.Contains(EditXgInfo.TourStatus))
        //    {
        //        ltrOperatorHtml.Text = "当前团队状态为" + EditXgInfo.TourStatus + "，不可导游报账。";
        //        return;
        //    }
        //    IsCaoZuoDaoYouShouZhi = true;
        //    if (!IsCaoZuoDaoYouShouZhi && EditXgInfo.ZhiFuFangShi == EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付)
        //    {
        //        ltrOperatorHtml.Text = "当前安排项为导游现付项，不可变更。";
        //        return;
        //    }

        //    ltrOperatorHtml.Text = "<a id=\"i_a_submit\" href=\"javascript:void(0);\" style=\"text-indent:0px;\">保存</a>";
        //}

        ///// <summary>
        ///// 初始化销售报账操作
        ///// </summary>
        //void InitXiaoShouBaoZhangCaoZuo()
        //{
        //    if (BianGengLeiXing != "xiaoshou") return;
        //    if (!Privs_XiaoShouBaoZhang)
        //    {
        //        ltrOperatorHtml.Text = "你没有销售报账权限，不可销售报账。";
        //        return;
        //    }

        //    if (EditXgInfo.TourXiaShouYuanId != SiteUserInfo.UserId)
        //    {
        //        ltrOperatorHtml.Text = "非本团销售员，不可销售报账。";
        //        return;
        //    }
        //    //合同金额未确认 不可报账
        //    if (!new EyouSoft.BLL.TourStructure.BTour().GetConfirmMoneyStatus(EditXgInfo.TourId))
        //    {
        //        ltrOperatorHtml.Text = "本团下有未确认的合同，不可销售报账";
        //        return;
        //    }

        //    TourStatus[] status = { TourStatus.销售待审 };
        //    if (IsTiaoGuoDaoYouBaoZhang) status = new TourStatus[] { TourStatus.导游带团, TourStatus.导游报帐, TourStatus.销售待审 };

        //    团队状态判断
        //    if (!status.Contains(EditXgInfo.TourStatus))
        //    {
        //        ltrOperatorHtml.Text = "当前团队状态为" + EditXgInfo.TourStatus + "，不可销售报账。";
        //        return;
        //    }
        //    if (IsTiaoGuoDaoYouBaoZhang) IsCaoZuoDaoYouShouZhi = true;
        //    if (!IsCaoZuoDaoYouShouZhi && EditXgInfo.ZhiFuFangShi == EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付)
        //    {
        //        ltrOperatorHtml.Text = "当前安排项为导游现付项，不可变更。";
        //        return;
        //    }

        //    ltrOperatorHtml.Text = "<a id=\"i_a_submit\" href=\"javascript:void(0);\" style=\"text-indent:0px;\">保存</a>";
        //}

        ///// <summary>
        ///// 初始化计调报账操作
        ///// </summary>
        //void InitJiDiaoBaoZhangCaoZuo()
        //{
        //    if (BianGengLeiXing != "jidiao") return;
        //    if (!Privs_JiDiaoBaoZhang)
        //    {
        //        ltrOperatorHtml.Text = "你没有计调报账权限，不可计调报账。";
        //        return;
        //    }

        //    //非本团计调不可报账
        //    if (!EditXgInfo.TourJiDiaos.Contains(SiteUserInfo.UserId))
        //    {
        //        ltrOperatorHtml.Text = "非本团指定计调，不可计调报账。";
        //        return;
        //    }

        //    //合同金额未确认 不可报账
        //    if (!new EyouSoft.BLL.TourStructure.BTour().GetConfirmMoneyStatus(EditXgInfo.TourId))
        //    {
        //        ltrOperatorHtml.Text = "本团下有未确认的合同，不可计调报账";
        //        return;
        //    }

        //    TourStatus[] status = { TourStatus.计调待审 };

        //    if (IsTiaoGuoDaoYouBaoZhang)
        //    {
        //        if (IsTiaoGuoXiaoShouBaoZhang)
        //        {
        //            status = new TourStatus[] { TourStatus.导游带团, TourStatus.导游报帐, TourStatus.销售待审, TourStatus.计调待审 };
        //        }
        //        else
        //        {
        //            //status = new TourStatus[] { TourStatus.计调待审 };
        //        }
        //    }
        //    else
        //    {
        //        if (IsTiaoGuoXiaoShouBaoZhang)
        //        {
        //            status = new TourStatus[] { TourStatus.销售待审, TourStatus.计调待审 };
        //        }
        //        else
        //        {
        //            //status = new TourStatus[] { TourStatus.计调待审 };
        //        }
        //    }

        //    //团队状态判断
        //    if (!status.Contains(EditXgInfo.TourStatus))
        //    {
        //        ltrOperatorHtml.Text = "当前团队状态为" + EditXgInfo.TourStatus + "，不可计调报账。";
        //        return;
        //    }
        //    if (IsTiaoGuoDaoYouBaoZhang) IsCaoZuoDaoYouShouZhi = true;
        //    if (!IsCaoZuoDaoYouShouZhi && EditXgInfo.ZhiFuFangShi == EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付)
        //    {
        //        ltrOperatorHtml.Text = "当前安排项为导游现付项，不可变更。";
        //        return;
        //    }

        //    ltrOperatorHtml.Text = "<a id=\"i_a_submit\" href=\"javascript:void(0);\" style=\"text-indent:0px;\">保存</a>";
        //}

        /// <summary>
        /// 保存变更信息
        /// </summary>
        void BaoCun()
        {
            var anPaiLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.PlanStructure.PlanProject>(Utils.GetFormValue(txtAnPaiLeiXing.UniqueID), EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它);
            var info = new EyouSoft.Model.HPlanStructure.MPlanCostChange();


            info.ChangeCost =Utils.GetDecimal( Utils.GetFormValue(txtJinE.UniqueID));
            //info.ChangeType = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.导游报账;

            //if (BianGengLeiXing == "xiaoshou") info.ChangeType = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.销售报账;
            if (BianGengLeiXing == "jidiao") info.ChangeType = EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass.计调报账;

            info.IssueTime = DateTime.Now;
            //if (anPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 || anPaiLeiXing == EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮)
            //{
            //    info.DNum = Utils.GetDecimal(Utils.GetFormValue(txtRenShu.UniqueID));
            //    info.PeopleNumber = Convert.ToInt32(info.DNum);
            //}
            //else
            //{
                info.PeopleNumber = Utils.GetInt(Utils.GetFormValue(txtRenShu.UniqueID));
                info.DNum = info.PeopleNumber;
            //}
            info.PlanId = AnPaiId;
            info.Remark = Utils.GetFormValue(txtBeiZhu.UniqueID);
            info.Type = JiaJianLeiXing == "jia";
            //info.FeiYongMingXi = Utils.GetFormValue(txtFeiYongMingXi.UniqueID);

            bool bllRetCode = new EyouSoft.BLL.HPlanStructure.BPlan().AddOrUpdPlanCostChange(info);

            if (bllRetCode)
            {
                var n = this.NewGetAttach();
                var o = this.OldGetAttach();
                var b = new BLL.ComStructure.BComAttach();
                b.Delete(o);
                if (!string.IsNullOrEmpty(n.FilePath))
                {
                    b.Add(n);
                }
                else if (!string.IsNullOrEmpty(o.FilePath))
                {
                    b.Add(o);
                }
            }

            if (bllRetCode) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
