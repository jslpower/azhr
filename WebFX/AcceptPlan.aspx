<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcceptPlan.aspx.cs" Inherits="EyouSoft.WebFX.AcceptPlan" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分销商收客计划</title>
    <link href="/Css/fx_style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

</head>
<body style="background: 0 none;">
     <uc1:HeadDistributorControl runat="server" ID="HeadDistributorControl1" ProcductClass="default Producticon" />
    <!-- InstanceEndEditable -->
    <div class="list-main">
        <div class="linebox-menu" id="Div1">
            <asp:Repeater runat="server" ID="rptkeyword">
                <ItemTemplate>
                    <a data-id="<%#Eval("keyword") %>" title="<%#Eval("keyword") %>" href="AcceptPlan.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&keyword=<%#Eval("keyword") %>"><span>
                        <%#Eval("keyword").ToString().Length > 5 ? GetAreaName(Eval("keyword").ToString()) : Eval("keyword")%></span></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
            <div class="hr_10">
            </div>
        <div class="linebox-menu" id="AreaDiv">
            <asp:Repeater runat="server" ID="rpArea">
                <ItemTemplate>
                    <a data-id="<%#Eval("AreaId") %>" title="<%#Eval("AreaName") %>" href="AcceptPlan.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&keyword=<%=EyouSoft.Common.Utils.GetQueryStringValue("keyword") %>&AreaId=<%#Eval("AreaId") %>"><span>
                        <%#Eval("AreaName").ToString().Length > 5 ? GetAreaName(Eval("AreaName").ToString()) : Eval("AreaName")%></span></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="list-maincontent">
            <div class="hr_10">
            </div>
            <div class="listsearch">
                <form id="frm" method="get" action="AcceptPlan.aspx">
                <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                线路名称：<input name="txtRouteName" id="txtRouteName" type="text" class="searchInput"
                    style="width: 100px;" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>' />
                        <%if (!this.IsParent) %>
                        <%{ %>
                    出团日期：<input name="txtBeginLDate" id="txtBeginLDate" type="text" class="searchInput size68"
                        onfocus="WdatePicker()" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginLDate") %>' />
                    -
                    <input name="txtEndLDate" id="txtEndLDate" type="text" class="searchInput size68"
                        onfocus="WdatePicker()" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndLDate") %>' />
                        <%} %>
                <a href="javascript:void(0)" id="btnSearch">
                    <img src="/images/fx-images/searchbg.gif" alt="" />
                </a>
                </form>
            </div>
            <div class="listtablebox">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1">
                    <tr>
                        <th rowspan="<%=this.RowSpan %>" align="center">
                            编号
                        </th>
                        <%if (!this.IsParent) %>
                        <%{ %>
                        <th rowspan="<%=this.RowSpan %>" align="center">
                            团号
                        </th>
                        <%} %>
                        <th rowspan="<%=this.RowSpan %>" align="left">
                            线路名称
                        </th>
                        <%if (!this.IsParent) %>
                        <%{ %>
                        <th rowspan="<%=this.RowSpan %>" align="center">
                            出团时间
                        </th>
                        <%} %>
                        <th rowspan="<%=this.RowSpan %>" align="center">
                            天数
                        </th>
                        <th rowspan="<%=this.RowSpan %>" align="center">
                            发布人
                        </th>
                        <th rowspan="<%=this.RowSpan %>" align="center">
                            OP
                        </th>
                        <th rowspan="<%=this.RowSpan %>" align="center">
                            门市价
                        </th>
                        <%if (!this.IsParent) %>
                        <%{ %>
                        <th colspan="4" align="center">
                            人数
                        </th>
                        <%} %>
                        <th rowspan="<%=this.RowSpan %>" align="center">
                            报名
                        </th>
                    </tr>
                        <%if (!this.IsParent) %>
                        <%{ %>
                    <tr>
                        <th align="center" class="nojiacu">
                            预
                        </th>
                        <th align="center" class="nojiacu">
                            留
                        </th>
                        <th align="center" class="nojiacu">
                            实
                        </th>
                        <th align="center" class="nojiacu">
                            剩
                        </th>
                    </tr>
                        <%} %>
                    <asp:Repeater runat="server" ID="RtPlan">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                                <td align="center">
                                    <%#Container.ItemIndex+1+(pageIndex-1)*pageSize %>
                                    <input type="hidden" data-class="hidPlanInfo" data-tourtype="<%#Eval("TourType") %>" />
                                </td>
                        <%if (!this.IsParent) %>
                        <%{ %>
                                <td align="center">
                                    <%#Eval("TourCode")%><%#GetChangeInfo((bool)Eval("IsChange"), (bool)Eval("IsSure"), Eval("tourId").ToString(), Eval("TourStatus").ToString())%>
                                </td>
                        <%} %>
                                <td align="left">
                                    <a href="<%#PrintPageSp %>?tourId=<%#Eval("TourId") %>" class="lineInfo" target="_blank">
                                        <%#Eval("RouteName")%></a>
                                </td>
                        <%if (!this.IsParent) %>
                        <%{ %>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),this.ProviderToDate)%>
                                </td>
                        <%} %>
                                <td align="center">
                                    <%#Eval("TourDays")%>
                                </td>
                                <td align="center">
                                    <%#(EyouSoft.Model.EnumType.TourStructure.ShowPublisher)Eval("ShowPublisher") == EyouSoft.Model.EnumType.TourStructure.ShowPublisher.供应商 ? Eval("SourceCompanyName") : (Container.DataItem as EyouSoft.Model.TourStructure.MTourSanPinInfo).SaleInfo.Name%>
                                </td>
                                <td align="center">
                                    <%# GetPlaner((Container.DataItem as EyouSoft.Model.TourStructure.MTourSanPinInfo).TourPlaner) %>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PeerAdultPrice"),this.ProviderToMoney)%>
                                </td>
                        <%if (!this.IsParent) %>
                        <%{ %>
                                <td align="center">
                                    <%#Eval("PlanPeopleNumber")%>
                                </td>
                                <td align="center">
                                        <%#Eval("LeavePeopleNumber")%>
                                </td>
                                <td align="center">
                                    <a href='<%#GetPrintPage(Eval("TourId").ToString(),"2") %>' target="_blank">
                                        <%#Eval("Adults")%><sup class="fontred"> +<%#Eval("Childs")%></sup></a>
                                </td>
                                <td align="center">
                                    <%#Eval("PeopleNumberLast")%>
                                </td>
                        <%} %>
                                <td align="center">
                                    <a href="javascript:void(0);" data-tourid="<%#Eval("TourId") %>" class="fontb-reds"
                                        data-shoukestatus="<%#Eval("TourShouKeStatus") %>">报名</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="PhPage" runat="server">
                        <tr>
                            <td colspan="14" align="center" bgcolor="#f4f4f4">
                                <div class="pages">
                                    <cc1:ExporPageInfoSelect runat="server" ID="ExporPageInfoSelect1" />
                                </div>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:Literal ID="litMsg" Visible="false" runat="server" Text="<tr><td align='center' colspan='14'>暂无计划!</td></tr>"></asp:Literal>
                </table>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <!-- InstanceEndEditable -->
    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script type="text/javascript" src="/Js/bt.min.js"></script>

    <script type="text/javascript" src="/Js/jquery.boxy.js"></script>

    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

    <script type="text/javascript">
        var AcceptPlan = {
            Data: {
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                keyword: '<%=EyouSoft.Common.Utils.GetQueryStringValue("keyword") %>',
                AreaId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("AreaId") %>'
            },
            SetArea: function() {
                $(".linebox-menu").find("a").each(function() {
                    if ($(this).attr("data-id") == AcceptPlan.Data.AreaId||$(this).attr("data-id") == AcceptPlan.Data.keyword) {
                        $(this).attr("class", "linebox-menudefault");
                    }
                });
            },
            Submit: function() {
                $("#frm").submit();
            },
            Accept: function() {
                $(".fontb-reds").each(function(i) {
                    var isparent="<%=this.IsParent %>";
                    var data = $(this).attr("data-shoukestatus");
                    var Tourtype = $.trim($(this).closest("tr").find("input[data-class='hidPlanInfo']").attr("data-tourtype"));
                    if(isparent=="True"){
                        $(this).attr("href","AcceptPlan.aspx?sl=" + AcceptPlan.Data.sl + "&ParentId=" + $(this).attr("data-tourid"));
                    }
                    else{
                        if (data) {
                            var shoukestatus = "<%=EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.报名中%>";
                            if (data != shoukestatus) {
                                // $(this).removeAttr("class");
                                $(this).attr("class", "fontb-red");
                            }
                            $(this).bind("click", function() {
                                if (data != shoukestatus) {
                                    if (data == "<%=EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.自动客满%>" || data == "<%=EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.手动客满%>") {
                                        tableToolbar._showMsg("计划已客满不允许报名！");
                                    }
                                    else {
                                        tableToolbar._showMsg("计划已停收不允许报名！");
                                    }
                                }
                                else {
                                    var url = "AcceptPlanApply.aspx?sl=" + AcceptPlan.Data.sl + "&TourId=" + $(this).attr("data-tourid");
                                    if (Tourtype == "组团散拼短线") {
                                        url += "&IsShort=1";
                                    }
                                    window.location.href = url;
                                }

                                return false;
                            });
                        }
                    }
                });
            },
            PageInit: function() {
                //获取线路
                AcceptPlan.SetArea();

                //查询列表
                $("#btnSearch").click(function() {
                    AcceptPlan.Submit();
                });

                //获取状态控制报名shoukestatus
                AcceptPlan.Accept();

                //Enter搜索
                $("#frm").find(":text").keypress(function(e) {
                    if (e.keyCode == 13) {
                        AcceptPlan.Submit();
                        return false;
                    }
                });
            }
        };


        $(function() {
            AcceptPlan.PageInit();

        });

    </script>

</body>
</html>
