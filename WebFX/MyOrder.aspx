<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrder.aspx.cs" Inherits="EyouSoft.WebFX.MyOrder" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/DistributorNotice.ascx" TagName="Notice" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type">
    <title>分销商我的订单</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <uc1:HeadDistributorControl runat="server" ID="HeadDistributorControl1" OrderClass="default orderformicon" />
    <!-- InstanceEndEditable -->
    <div class="list-main">
        <div class="list-maincontent">
            <div class="hr_10">
            </div>
            <div class="listsearch">
                <form id="frm" action="MyOrder.aspx" method="get">
                <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                客源单位：
                <input type="text" class="searchInput" name="DCompanyName" value='<%=Request.QueryString["DCompanyName"] %>' />
                订单状态：
                <select id="ddlOrderStatus" name="ddlOrderStatus">
                    <%=EyouSoft.Common.UtilsCommons.GetGroupEndOrderStatus(EyouSoft.Common.Utils.GetQueryStringValue("ddlOrderStatus"))%>
                </select>
                出团地点：
                <select id="ddlArea" name="ddlArea">
                    <%=GetArea(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea"))%>
                </select>
                <a href="javascript:void(0)" id="btnSearch">
                    <img src="/images/fx-images/searchbg.gif"></a>
                </form>
            </div>
            <div class="listtablebox">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="liststyle">
                    <tr>
                        <th rowspan="2" align="center">
                            编号
                        </th>
                        <th rowspan="2" align="center">
                            订单号
                        </th>
                        <th rowspan="2" align="left">
                            线路名称
                        </th>
                        <th rowspan="2" align="center">
                            出团时间
                        </th>
                        <th rowspan="2" align="center">
                            下单时间
                        </th>
                        <th colspan="2" align="center">
                            价格
                        </th>
                        <th rowspan="2" align="center">
                            人数
                        </th>
                        <th rowspan="2" align="center">
                            线路区域
                        </th>
                        <th rowspan="2" align="center">
                            订单状态
                        </th>
                        <th rowspan="2" align="center">
                            操作
                        </th>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#DCEFF3">
                            成人
                        </td>
                        <td align="center" bgcolor="#DCEFF3">
                            儿童
                        </td>
                    </tr>
                    <asp:Repeater ID="RtOrder" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"odd":"" %>">
                                <td align="center">
                                    <%#Container.ItemIndex+1+(pageIndex-1)*pageSize %>
                                </td>
                                <td align="center">
                                    <a href="<%#PrintPageDd %>?tourId=<%#Eval("TourId") %>&type=<%#(int)(EyouSoft.Model.EnumType.TourStructure.OrderStatus)Eval("OrderStatus") %>"
                                        target="_blank" class="lineInfo" data="<%#Eval("DCompanyName") %>_<%#Eval("DContactName") %>_<%#Eval("DContactTel") %>">
                                        <%#Eval("OrderCode")%></a>
                                </td>
                                <td align="left">
                                    <a href="<%#PrintPageSp %>?tourId=<%#Eval("TourId") %>" target="_blank" class="example"
                                        data="<%#GetRoutePaoPao(Eval("PlanerList") as System.Collections.Generic.List<EyouSoft.Model.TourStructure.Planer> ,Eval("SellerName").ToString(),Eval("SellerContactTel").ToString(),Eval("SellerContactMobile").ToString()) %>">
                                        <%#Eval("RouteName")%></a>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString( Eval("LDate"),this.ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"),this.ProviderToDate) %>
                                </td>
                                <td align="right" class="fontblue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"),this.ProviderToMoney)%>
                                </td>
                                <td align="right" class="font-orange">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ChildPrice"),this.ProviderToMoney)%>
                                </td>
                                <td align="center">
                                    <%#Eval("Adults")%>
                                    <sup class="fontred">+<%#Eval("Childs")%></sup>
                                </td>
                                <td align="center" class="font-green">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td align="center">
                                    <%--<span data='font' data-value="<%#(int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus), Eval("GroupOrderStatus").ToString())%>">
                                        <%#Eval("GroupOrderStatus")%></span>--%>
                                    <%#EyouSoft.Common.UtilsCommons.GetOrderStateForHtml((int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.OrderStatus), Eval("OrderStatus").ToString()))%>
                                </td>
                                <td align="center">
                                    <a class="link1" href="javascript:void(0)" data-tourid='<%#Eval("TourId") %>' data-orderid="<%#Eval("OrderId")%>" data-orderstatus="<%#Eval("OrderStatus") %>" data-page="<%#GetPrintPage(Eval("TourId").ToString(),Eval("OrderId").ToString()) %>" >
                                   
                                        查看</a> <a target="_blank" href="<%#GetPrintPage(Eval("TourId").ToString(),Eval("OrderId").ToString()) %>">
                                            名单</a>
                                            <a target="_blank" href="/PrintPage/Voucher.aspx?OrderId=<%#Eval("OrderId") %>">Voucher</a>
                                             <a class="linkCancle" data-orderstatus="<%#Eval("OrderStatus") %>" data-orderid="<%#Eval("OrderId") %>"
                                                href="javascript:void(0)">取消</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="PhPage" runat="server">
                        <tr>
                            <td colspan="15" align="center" bgcolor="#f4f4f4">
                                <div class="pages">
                                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                </div>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:Literal ID="litMsg" Visible="false" runat="server" Text="<tr><td align='center' colspan='15'>暂无订单!</td></tr>"></asp:Literal>
                </table>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <!-- InstanceEndEditable -->
</body>
</html>
<!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

<script src="/Js/bt.min.js" type="text/javascript"></script>

<script src="/Js/jquery.boxy.js" type="text/javascript"></script>

<!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

<script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

<script src="/Js/table-toolbar.js" type="text/javascript"></script>

<script type="text/javascript">

    $(function() {

        //Enter搜索
        $("#frm").find(":text").keypress(function(e) {
            if (e.keyCode == 13) {
                $("#frm").submit();
                return false;
            }
        });
        $("#liststyle").find("a[data-class='showCarModel']").click(function() {
            var self = $(this);
            var url = "/CommonPage/SetSeat.aspx?tourid=" + self.attr("data-tourid") + "&oldPeoNum=" + self.attr("data-peonum") + "&orderid=" + self.attr("data-orderid") + "&PeoNum=" + self.attr("data-peonum") + "&isShow=Show";
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "查看座位",
                modal: true,
                width: "930px",
                height: "460px"
            })
        })

        //控制状态的字体颜色
        $("span[data='font']").each(function() {
            var value = $(this).attr("data-value");
            if (value == "<%=(int)EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus.报名未确认 %>" || value == "<%=(int)EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus.预留未确认 %>") {
                $(this).attr("class", "fontbsize12");
            }
        });

        $("#btnSearch").click(function() {
            $("#frm").submit();
        });


//        $(".link1").each(function() {
//            var OrderStatus = $(this).attr("data-orderstatus");
//            var newfref = $(this).attr("data-page");
//            if (OrderStatus != "<%=EyouSoft.Model.EnumType.TourStructure.OrderStatus.未处理 %>") {
//                $(this).attr(
//                { "href": newfref,
//                    "class": "chakan",
//                    "target": "_blank"
//                });
//            }
//        });



        $(".link1").click(function() {

            // alert("begin")
            var url = "OrderSee.aspx?" + $.param({
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                OrderId: $(this).attr("data-orderId"),
                TourId: $(this).attr("data-tourid")
            });
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "订单详情",
                modal: true,
                width: "750px",
                height: "400px"
            });
            return false;
        });

        //订单的取消操作（已成交的订单不允许取消）
        $(".linkCancle").each(function() {
            var status = $(this).attr("data-orderstatus");
            if (status != "<%=EyouSoft.Model.EnumType.TourStructure.OrderStatus.未处理 %>") {
                $(this).css("display", "none");
            }
        });

        $(".linkCancle").click(function() {
            var data = {
                s1: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                Type: "Cancle",
                OrderId: $(this).attr("data-orderId")
            };

            tableToolbar.ShowConfirmMsg("是否确认取消该订单？", function() {
                $.newAjax({
                    url: "MyOrder.aspx?sl=" + data.s1,
                    type: "post",
                    data: $.param(data),
                    dataType: "json",
                    success: function(back) {
                        if (back.result == "1") {
                            tableToolbar._showMsg(back.msg, function() {
                                window.location.reload();
                            });
                        }
                        else {
                            tableToolbar._showMsg(back.msg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("服务器忙！");
                    }

                });

            });

        });


        //Route PaoPao
        $('.example').each(function() {
            if ($(this).attr("data")) {
                $(this).bt({
                    contentSelector: function() {
                        return $(this).attr("data");
                    },
                    positions: ['left', 'right', 'bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 330,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });
            }
        });

        //OrderCode  PaoPao
        $('.lineInfo').each(function() {
            if ($(this).attr("data")) {
                $(this).bt({
                    contentSelector: function() {
                        var arr = $(this).attr("data").split("_");
                        if (arr != ",,") {
                            return "客源单位：" + arr[0] + "<br/> 联系人：" + arr[1] + "<br />联系电话：" + arr[2] + "";
                        } else {
                            return "";
                        }
                    },
                    positions: ['left', 'right', 'bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 170,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });
            }
        });
    });

</script>

