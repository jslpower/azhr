<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="PlanList.aspx.cs" Inherits="EyouSoft.Web.Plan.PlanList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" name="txtTourCode" class="inputtext formsize120" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>" />
                    客源单位：<uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" selectfrist="false" />
                    抵达时间：
                    <input type="text" class="inputtext" style="width: 63px; padding-left: 2px;" id="txtStartTime"
                        name="txtStartTime" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtStartTime") %>" />
                    -
                    <input type="text" class="inputtext" style="width: 63px; padding-left: 2px;" id="txtEndTime"
                        name="txtEndTime" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndTime") %>" />
                    销售员：
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>"
                        SelectFrist="false" />
                    OP：
                    <uc1:SellsSelect ID="SellsSelect2" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>"
                        SelectFrist="false" SetTitle="OP" />
                    <br />
                    团队状态：
                    <select name="sltTourStatus" class="inputselect">
                        <asp:Literal ID="litTourStatus" runat="server"></asp:Literal>
                    </select>
                    操作状态：
                    <select name="sltOperaterStatus" class="inputselect">
                        <asp:Literal ID="litOperaterStatus" runat="server"></asp:Literal>
                    </select>
                    <input type="submit" id="search" class="search-btn" value="" />
                </p>
            </span>
        </div>
        <input type="hidden" name="sl" id="sl" value="<%=SL %>" />
        </form>
        <div class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th rowspan="2" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        团号
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        团队名称
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        抵达时间
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        天数
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        客户单位
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        业务员
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        OP
                    </th>
                    <th colspan="3" align="center" class="th-line">
                        人数
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        计调情况
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        团队状态
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line nojiacu h20">
                        成人
                    </th>
                    <th align="center" class="th-line nojiacu h20">
                        儿童
                    </th>
                    <th align="center" class="th-line nojiacu h20">
                        领队
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%# Eval("TourId") %>" />
                            </td>
                            <td align="center">
                                <a href='<%# GetUrl(Eval("TourType"),Eval("TourId")) %>' target="_blank" style="text-decoration: none"
                                    data-class="TravelagencyShow">
                                    <%# Eval("TourCode")%>
                                </a>
                                <%#GetTourPlanIschange(Convert.ToBoolean(Eval("IsChange")), Convert.ToBoolean(Eval("IsSure")),Eval("TourId").ToString())%>
                                <div style="display: none">
                                    <b>
                                        <%# Eval("TourCode")%></b><br />
                                    发布人：<%# Eval("Operator")%><br />
                                    发布时间：<%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("UpdateTime"), "yyyy/MM/dd") %>
                                </div>
                            </td>
                            <td align="left">
                                <a href='<%# teamPrintUrl %>?tourid=<%# Eval("TourId") %>' target="_blank">
                                    <%# Eval("RouteName") %>
                                </a>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Convert.ToDateTime(Eval("LDate")), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%# Eval("TourDays")%>
                            </td>
                            <td align="center">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%# GetTourType((EyouSoft.Model.EnumType.TourStructure.TourType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType),Eval("TourType").ToString())) %>'>
                                    <a href="javascript:void(0);" data-class="customershow">
                                        <%#GetCustomerInfo(Eval("CompanyInfo"), "single")%>
                                    </a>
                                    <div style="display: none">
                                        <%# GetCustomerInfo(Eval("CompanyInfo"), "info")%>
                                    </div>
                                </asp:PlaceHolder>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%# GetOperaList(Eval("TourPlaner"))%>
                            </td>
                            <td align="center">
                                <%# Eval("Adults")%>
                            </td>
                            <td align="center">
                                <%# Eval("Childs")%>
                            </td>
                            <td align="center">
                                <%# Eval("Leaders")%>
                            </td>
                            <td align="center" data-class="GetJiDiaoIcon" data-tourid="<%# Eval("TourId") %>">
                                <%# EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.HTourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%>
                            </td>
                            <td align="center" <%# (EyouSoft.Model.EnumType.TourStructure.TourStatus)Eval("TourStatus") == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团 ? "class='fontgray'" : ""%>>
                                <%# Eval("TourSureStatus").ToString()%>
                            </td>
                            <td align="center">
                                <%# GetOperate((EyouSoft.Model.EnumType.TourStructure.TourStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus), Eval("TourStatus").ToString()), Eval("TourId").ToString(), Eval("TourPlaner"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server" Visible="false">
                    <tr align="center">
                        <td colspan="14">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">
        var TeamOperaterPage = {
            //计调任务接受
            _OperaterReceive: function(comID, tourId, state) {
                $.newAjax({
                    type: "POST",
                    url: '/Ashx/ReceiveJob.ashx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&type=receive&com=' + comID + "&Operator=" + encodeURIComponent("<%=this.SiteUserInfo.Name %>") + "&OperatorID=<%=this.SiteUserInfo.UserId %>" + "&OperatDepID=<%=this.SiteUserInfo.DeptId  %>&tourId=" + tourId + "&state=" + state,
                    async: false,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result) {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = window.location.href;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            //click事件
            _BindBtn: function() {
                $("#liststyle").find("[data-class='receiveOp']").unbind("click").click(function() {
                	$(this).unbind("click");
                    var ac = $(this).attr("data-teamPlaner");
                    if (ac.toUpperCase() == "FALSE") {
                        tableToolbar._showMsg("不是该计划的OP,没有接受任务的权限!");
                    } else {
                        var companyID = "<%=this.SiteUserInfo.CompanyId %>";
                        var tourId = $(this).attr("data-tourid");
                        var state = $(this).attr("data-State");
                        TeamOperaterPage._OperaterReceive(companyID, tourId, state);
                    }
                    return false;
                });

                //发布人泡泡提示
                $("#liststyle").find("[data-class='TravelagencyShow']").bt({
                    contentSelector: function() {
                        return $(this).siblings("div").html();
                    },
                    positions: ['left', 'right', 'bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 200,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });

                //客户单位泡泡
                $("#liststyle").find("[data-class='customershow']").bt({
                    contentSelector: function() {
                        return $(this).siblings("div").html();
                    },
                    positions: ['left', 'right', 'bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 200,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });

                //供应商泡泡
                BtFun.InitBindBt("GetJiDiaoIcon");
            },
            _DataInit: function() {
                TeamOperaterPage._BindBtn();
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        };


        $(document).ready(function() {
            //初始化
            TeamOperaterPage._DataInit();
            tableToolbar.init({
                tableContainerSelector: "#liststyle"
            });
        });
        
    </script>

</asp:Content>
