using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Common.Page;
using System.Text;

namespace EyouSoft.Web.Sales
{
    /// <summary>
    /// 散拼列表
    /// 创建人：田想兵  创建日期:2011.9.13
    /// </summary>
    public partial class SanpinList : BackPage
    {

        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 100;
        #endregion
        /// <summary>
        /// 页面类型1.组团2.地接团队3.出境团队
        /// </summary>
        protected int type = 0;
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;
        /// <summary>
        /// 是否可以报名
        /// </summary>
        private bool IsBaoMing = true;

        /// <summary>
        /// 散拼打印单链接
        /// </summary>
        protected string PrintPageSp = string.Empty;
        /// <summary>
        /// 订单打印单
        /// </summary>
        protected string PrintPageOrder = string.Empty;
        /// <summary>
        /// 修改，派团计调，审核计划，复制
        /// </summary>
        protected string ListPower = string.Empty;
        /// <summary>
        /// 模版团编号
        /// </summary>
        protected string ParentId = Utils.GetQueryStringValue("ParentId");
        /// <summary>
        /// 表头行数
        /// </summary>
        protected int RowSpan = string.IsNullOrEmpty(Utils.GetQueryStringValue("ParentId")) ? 1 : 2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));

            #region 获得打印单链接
            EyouSoft.BLL.ComStructure.BComSetting comSettingBll = new EyouSoft.BLL.ComStructure.BComSetting();
            PrintPageSp = comSettingBll.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单);
            PrintPageOrder = comSettingBll.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.订单信息汇总表);
            comSettingBll = null;
            #endregion

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType != "")
            {
                AJAX(doType);
            }
            #endregion

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));

            

            #region 获取查询条件
            //线路区域
            int areaId = Utils.GetInt(Utils.GetQueryStringValue("ddlArea"));
            //团号
            string tourCode = Utils.GetQueryStringValue("txtTourCode");
            //线路名称
            string routeName =Utils.GetQueryStringValue("txtRouteName");
            //出团开始时间
            DateTime? txtBeginDateF = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtOutDateS"));
            DateTime? txtBeginDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtOutDateE"));
            ////回团结束时间
            DateTime? txtEndDateF = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtGetDateS"));
            DateTime? txtEndDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtGetDateE"));

            //销售员
            string sellerId = Utils.GetQueryStringValue(this.SellsSelect1.SellsIDClient);
            string sellerName = Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);

            this.SellsSelect1.SellsID = sellerId;
            this.SellsSelect1.SellsName = sellerName;
            //团队状态
            string tourState = Utils.GetQueryStringValue("selState");
            //操作状态
            string sltTourState = Utils.GetQueryStringValue("sltTourState");
            //EyouSoft.Model.HTourStructure.MTourSearch searchModel = new EyouSoft.Model.HTourStructure.MTourSearch();
            EyouSoft.Model.TourStructure.MTourSanPinSearch searchModel = new EyouSoft.Model.TourStructure.MTourSanPinSearch();
            searchModel.AreaId = areaId;
            searchModel.LLDate = txtBeginDateS;
            searchModel.SLDate = txtBeginDateF;
            searchModel.LRDate = txtEndDateS;
            searchModel.SRDate = txtEndDateF;
            searchModel.RouteName = routeName;
            searchModel.SellerName = sellerName;
            //searchModel.BeginRDate = txtBeginDateF;
            //searchModel.EndRDate = txtEndDateF;
            searchModel.TourCode = tourCode;
            searchModel.ParentId = ParentId;
            if ( sltTourState != "")
            {
                searchModel.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)Utils.GetInt(sltTourState);
            }
            if (!string.IsNullOrEmpty(tourState))
            {
                searchModel.TourSureStatus = (EyouSoft.Model.EnumType.TourStructure.TourSureStatus)Utils.GetInt(tourState);
            }

            #endregion
            BTour bll = new BTour();
            IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> list = bll.GetTourSanPinList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searchModel,false);
            //IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> list = bll.GetTourSanPinList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, (EyouSoft.Model.EnumType.TourStructure.ModuleType)(type - 1), searchModel, false);
            if (list != null && list.Count > 0)
            {
                rpt_List.DataSource = list;
                rpt_List.DataBind();
                //绑定分页
                BindPage();
                this.litMsg.Visible = false;
            }
            else
            {
                this.litMsg.Visible = true;
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }



        }
        /// <summary>
        /// 判断线路是否为短线（为短线做标识）
        /// </summary>
        /// <param name="obj">团队类型</param>
        /// <returns></returns>
        protected string GetTourType(object obj)
        {
            //if (obj != null && (EyouSoft.Model.EnumType.TourStructure.TourType)obj == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线)
            //{
            //    return "(短)";
            //}
            //else
            //{
                return "";
            //}
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;


            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }

        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            string ids = Utils.GetQueryStringValue("ids");
            //对应执行操作
            switch (doType)
            {
                case "delete":
                    msg = DeleteData(ids);
                    break;
                case "keman":
                    msg = KeManData(ids);
                    break;
                case "stop":
                    msg = TingShouData(ids);
                    break;
                case "zhengc":
                    msg = ZhengChangData(ids);
                    break;
                case "canel":
                    string remarks = Server.HtmlDecode(Utils.GetQueryStringValue("remarks"));
                    msg = CanelData(ids, remarks);
                    break;
                case "contact":
                    msg = GetContactInfo();
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string ids)
        {
            string[] id = ids.Split(',');
            //删除操作
            bool result = false;
            if (id.Length > 0)
            {
                BTour bll = new BTour();

                result = bll.DeleteTour(SiteUserInfo.CompanyId, id);

            }
            if (result)
            {
                return UtilsCommons.AjaxReturnJson("1", "删除成功!");
            }
            else
            {
                return UtilsCommons.AjaxReturnJson("0", "删除失败!");
            }
        }

        /// <summary>
        /// 取消散拼计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private string CanelData(string ids, string canelRemarks)
        {
            string[] id = ids.Split(',');

            if (id == null || id.Length == 0) return UtilsCommons.AjaxReturnJson("0", "操作失败：未选择要取消的计划");

            if (id.Length > 1) return UtilsCommons.AjaxReturnJson("0", "操作失败：一次只能取消一个计划");

            BTour bll = new BTour();
            bool bllRetCode = bll.CancelTour(id, canelRemarks, SiteUserInfo.CompanyId);

            if (bllRetCode) return UtilsCommons.AjaxReturnJson("1", "操作成功!");
            else return UtilsCommons.AjaxReturnJson("0", "操作失败!");
        }

        /// <summary>
        /// 获得报价标准
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        protected string GetPriceByTour(string tourID)
        {
            string str = string.Empty;
            IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list = new EyouSoft.BLL.TourStructure.BTour().GetTourSanPinPrice(tourID);
            if (list != null && list.Count > 0)
            {
                str = UtilsCommons.GetPriceStandardTable(list, this.ProviderToMoney);
            }
            return str;
        }

        /// <summary>
        /// 客满操作
        /// </summary>
        /// <param name="id">设置的ID</param>
        /// <returns></returns>
        private string KeManData(string ids)
        {
            string[] id = ids.Split(',');
            bool result = false;
            string msg = string.Empty;
            if (id.Length > 0)
            {
                BTour bll = new BTour();
                for (int i = 0; i < id.Length; i++)
                {
                    result = bll.SetHandStatus(id[i], EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.手动客满);
                    if (result == false)
                    {
                        msg += (i + 1).ToString() + ",";
                    }
                }
                if (msg.Length > 0)
                {
                    msg.Remove(msg.Length - 1, 1);
                    msg = "部分数据已设置客满,当前选中数据中的第" + msg + "行设置失败!";
                }
                else
                {
                    msg = "设置客满成功!";
                }
            }
            return UtilsCommons.AjaxReturnJson("1", msg);
        }

        /// <summary>
        /// 停收操作
        /// </summary>
        /// <param name="id">设置的ID</param>
        /// <returns></returns>
        private string TingShouData(string ids)
        {
            string[] id = ids.Split(',');
            bool result = false;
            string msg = string.Empty;
            if (id.Length > 0)
            {
                BTour bll = new BTour();
                for (int i = 0; i < id.Length; i++)
                {
                    result = bll.SetHandStatus(id[i], EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.手动停收);
                    if (result == false)
                    {
                        msg += (i + 1).ToString() + ",";
                    }
                }
                if (msg.Length > 0)
                {
                    msg.Remove(msg.Length - 1, 1);
                    msg = "部分数据已设置停收,当前选中数据中的第" + msg + "行设置失败!";
                }
                else
                {
                    msg = "设置停收成功!";
                }
            }
            return UtilsCommons.AjaxReturnJson("1", msg);
        }

        /// <summary>
        /// 正常操作
        /// </summary>
        /// <param name="id">设置的ID</param>
        /// <returns></returns>
        private string ZhengChangData(string ids)
        {
            string[] id = ids.Split(',');
            bool result = false;
            string msg = string.Empty;
            if (id.Length > 0)
            {
                BTour bll = new BTour();
                for (int i = 0; i < id.Length; i++)
                {
                    result = bll.SetHandStatus(id[i], EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.报名中);
                    if (result == false)
                    {
                        msg += (i + 1).ToString() + ",";
                    }
                }
                if (msg.Length > 0)
                {
                    msg.Remove(msg.Length - 1, 1);
                    msg = "部分数据已设置成功,当前选中数据中的第" + msg + "行设置失败!";
                }
                else
                {
                    msg = "设置成功!";
                }
            }
            return UtilsCommons.AjaxReturnJson("1", msg);
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_栏目, true);
                return;
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_新增))
            {
                this.phForAdd.Visible = false;
                this.phForCopy.Visible = false;
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_删除))
            {
                this.phForDelete.Visible = false;
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_修改))
            {
                this.phForUpdate.Visible = false;
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_取消))
            {
                this.phForCanel.Visible = false;
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_派团给计调))
            {
                this.phForOper.Visible = false;
            }
            //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_审核供应商计划))
            //{
            //    this.phForShemHe.Visible = false;
            //}

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_订单报名))
            {
                IsBaoMing = false;
            }

            ListPower = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_修改).ToString().ToLower() + ",";
            ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_派团给计调).ToString().ToLower() + ",";
            //ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_审核供应商计划).ToString().ToLower() + ",";
            ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.自有产品_散拼产品_新增).ToString().ToLower();

        }

        /// <summary>
        /// 根据变更信息显示
        /// </summary>
        /// <param name="isChange"></param>
        /// <param name="isSure"></param>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetChangeInfo(bool isChange, bool isSure, string tourId, string tourStatus)
        {
            string str = string.Empty;
            if (isChange)
            {
                str = "<span ><a  class='fontred' target='_blank' href='" + PrintPageSp + "?tourid=" + tourId + "'>(变)</a></span>";
            }
            if (isSure)
            {
                str = "<span ><a class='fontgreen' target='_blank' href='" + PrintPageSp + "?tourid=" + tourId + "'>(变)</a></span>";
            }
            if (tourStatus == "已取消")
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 通过收客状态返回html
        /// </summary>
        /// <param name="tourId"></param>
        /// <param name="state"></param>
        /// <param name="sourceId"></param>
        /// <param name="isCheck"></param>
        /// <returns></returns>
        protected string GetHtmlByShouKeState(object tourId, object state, object sourceId, object isCheck, object tourStatus, object tourType)
        {
            StringBuilder sb = new StringBuilder();
            bool c = isCheck == null ? false : (bool)isCheck;
            EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus skState = (EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus)state;
            EyouSoft.Model.EnumType.TourStructure.TourStatus tStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)((int)tourStatus);
            EyouSoft.Model.EnumType.TourStructure.TourType tType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            if (tStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.封团 && tStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消)
            {
                switch (skState)
                {
                    case EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.报名中:
                    //case EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.手动客满:
                    //case EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.手动停收:
                        //if (tType == EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线)
                        //{
                        //    if (sourceId != null && sourceId.ToString().Trim() != "" && c)
                        //    {
                        //        sb.Append("<a onclick='SanPinList.OpenBaoMing(this);return false;' href='/TeamCenter/ShortSanKeBaoMing.aspx?tourID=" + tourId.ToString() + "&sl=" + Utils.GetQueryStringValue("sl") + "'>" + skState.ToString() + "</a>");
                        //    }
                        //    if (sourceId == null || sourceId.ToString().Trim() == "")
                        //    {
                        //        sb.Append("<a onclick='SanPinList.OpenBaoMing(this);return false;' href='/TeamCenter/ShortSanKeBaoMing.aspx?tourID=" + tourId.ToString() + "&sl=" + Utils.GetQueryStringValue("sl") + "'>" + skState.ToString() + "</a>");
                        //    }
                        //}
                        //else
                        //{
                            //if (sourceId != null && sourceId.ToString().Trim() != "" && c)
                            //{
                            //    sb.Append("<a onclick='SanPinList.OpenBaoMing(this);return false;' href='/Sales/SanPinBaoMing.aspx?tourID=" + tourId.ToString() + "&sl=" + Utils.GetQueryStringValue("sl") + "'>" + skState.ToString() + "</a>");
                            //}
                            //if (sourceId == null || sourceId.ToString().Trim() == "")
                            //{
                            //    sb.Append("<a onclick='SanPinList.OpenBaoMing(this);return false;' href='/Sales/SanPinBaoMing.aspx?tourID=" + tourId.ToString() + "&sl=" + Utils.GetQueryStringValue("sl") + "'>" + skState.ToString() + "</a>");
                            //}
                        //}
                        if (!string.IsNullOrEmpty(this.ParentId))
                        {
                            sb.Append("<a onclick='SanPinList.OpenBaoMing(this);return false;' href='/Sales/SanPinBaoMing.aspx?tourID=" + tourId.ToString() + "&sl=" + Utils.GetQueryStringValue("sl") + "'>" + skState.ToString() + "</a>");
                        }
                        else
                        {
                            sb.Append("<a onclick='SanPinList.OpenBaoMing(this);return false;' href='/Sales/sanpinlist.aspx?parentid=" + tourId.ToString() + "&sl=" + Utils.GetQueryStringValue("sl") + "'>" + skState.ToString() + "</a>");
                        }
                        break;
                    default:
                        sb.Append("<b class='fontred' data-class='tingshou'>" + skState.ToString() + "</b>");
                        break;
                }
            }
            if (IsBaoMing)
            {
                return sb.ToString();
            }
            else
            {
                return "无权报名";
            }
        }

        /// <summary>
        /// 获得发布人或供应商联系人的电话，手机
        /// </summary>
        /// <returns></returns>
        protected string GetContactInfo()
        {
            string sourceId = Utils.GetQueryStringValue("sourceId");
            string operId = Utils.GetQueryStringValue("operId");
            string str = string.Empty;
            EyouSoft.Model.TourStructure.MPersonInfo infoModel = new EyouSoft.BLL.TourStructure.BTour().GetPersonInfo(sourceId, operId);
            if (infoModel != null)
            {
                str = "<span style='display:none'>联系电话：" + infoModel.Phone + "<br> 联系手机：" + infoModel.Mobile + "</span>";
            }
            else
            {
                str = string.Format(str, "", "");
            }
            return str;
        }

        /// <summary>
        /// 根据计划来源显示发布人信息
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="operatorInfo"></param>
        /// <param name="ShowPublisher"></param>
        /// <returns></returns>
        protected string GetOperatorInfo(object sourceId, object operatorInfo, object sourceCompanyName)
        {
            string str = "<a data-class='paopao' data-sourceId='{1}' data-operId='{2}'>{0}</a>";
            if (sourceId == null || sourceId.ToString().Trim() == "")
            {
                if (operatorInfo != null)
                {
                    EyouSoft.Model.TourStructure.MOperatorInfo info = (EyouSoft.Model.TourStructure.MOperatorInfo)operatorInfo;
                    str = string.Format(str, info.Name, "", info.OperatorId);
                }
            }
            else
            {
                if (sourceCompanyName != null)
                {
                    str = string.Format(str, sourceCompanyName, sourceId, "");
                }
            }
            return str;
        }

        #endregion
    }
}
