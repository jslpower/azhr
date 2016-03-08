<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancialControl.aspx.cs"
    Inherits="EyouSoft.WebFX.FinancialControl" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>分销商财务管理</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <uc1:HeadDistributorControl ID="HeadDistributorControl1" runat="server" FinanceClass="default cawuglicon" />
    <div class="list-main">
        <div class="list-maincontent">
            <div class="hr_10">
            </div>
            <div class="listsearch">
                <form id="frm" action="FinancialControl.aspx">
                <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                订单号：
                <input type="text" class="searchInput" name="txtOrderCode" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOrderCode") %>' />
                线路名称：<input type="text" class="searchInput size170" name="txtRouteName" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>' />
                财务状况：
                <select id="ddlIsClean" name="ddlIsClean">
                    <%=GetFinancialStatus(EyouSoft.Common.Utils.GetQueryStringValue("ddlIsClean"))%>
                </select>
                <a href="javascript:void(0)" id="btnSearch">
                    <img src="/Images/fx-images/searchbg.gif" /></a>
                </form>
            </div>
            <div class="listtablebox">
                <table width="100%" cellspacing="0" cellpadding="0" border="0" id="liststyle">
                    <tbody>
                        <tr class="odd">
                            <th align="center">
                                编号
                            </th>
                            <th align="center">
                                订单号
                            </th>
                            <th align="left">
                                线路名称
                            </th>
                            <th align="center">
                                订单状态
                            </th>
                            <th align="center">
                                人数
                            </th>
                            <th align="center">
                                应付金额
                            </th>
                            <th align="center">
                                已付金额
                            </th>
                            <th align="center">
                                未付金额
                            </th>
                            <th align="center">
                                操作
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="RpFinancial">
                            <ItemTemplate>
                                <tr class='<%#Container.ItemIndex%2==0?"odd":"" %>'>
                                    <td align="center">
                                        <%#Container.ItemIndex+1+(pageIndex-1)*pageSize %>
                                    </td>
                                    <td align="center">
                                        <%#Eval("OrderCode")%>
                                    </td>
                                    <td align="left">
                                            <%#Eval("RouteName") %>
                                    </td>
                                    <td align="center">
                                        <span class="fontbsize12">
                                            <%#Eval("Status")%></span>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Adults") %><sup class="fontred">+<%#Eval("Childs") %></sup>
                                    </td>
                                    <td align="right">
                                        <strong class="fontbsize12">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TotalAmount"), this.ProviderToMoney)%></strong>
                                    </td>
                                    <td align="right">
                                        <strong class="font-green">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Received"),this.ProviderToMoney) %></strong>
                                    </td>
                                    <td align="right">
                                        <strong class="fontblue">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("TotalAmount")) - Convert.ToDecimal(Eval("Received")), this.ProviderToMoney)%></strong>
                                    </td>
                                    <td align="center">
                                        <a class="link1" href="javascript:void();" data-orderid="<%#Eval("OrderId") %>" data-money="<%#Eval("Receivable") %>_<%#Eval("Received") %>_<%#Convert.ToDecimal(Eval("Receivable"))-Convert.ToDecimal(Eval("Received")) %>">
                                            登记</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder ID="PhPage" runat="server">
                            <tr>
                                <td align="right" colspan="5">
                                    <strong>合计</strong>：
                                </td>
                                <td align="right">
                                    <strong class="fontbsize12">
                                        <asp:Literal ID="LtTotalSumPrice" runat="server"></asp:Literal></strong>
                                </td>
                                <td align="right">
                                    <strong class="font-green">
                                        <asp:Literal ID="LtTotalReceived" runat="server"></asp:Literal></strong>
                                </td>
                                <td align="right">
                                    <strong class="fontblue">
                                        <asp:Literal ID="LtTotalUnReceived" runat="server"></asp:Literal></strong>
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="odd">
                                <td bgcolor="#f4f4f4" align="center" colspan="14">
                                    <div class="pages">
                                        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:Literal ID="litMsg" Visible="false" runat="server" Text="<tr><td align='center' colspan='14'>暂无记录!</td></tr>"></asp:Literal>
                    </tbody>
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
        $(".link1").click(function() {
            var url = "FinancialRegistration.aspx?" + $.param({
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                OrderId: $(this).attr("data-orderId"),
                Datamoney: $(this).attr("data-money")
            });

            Boxy.iframeDialog({
                iframeUrl: url,
                title: "登记",
                modal: true,
                width: "880px",
                height: "323px"
            });
            return false;
        });


        $("#btnSearch").click(function() {
            $("#frm").submit();
        });

        //Enter搜索
        $("#frm").find(":text").keypress(function(e) {
            if (e.keyCode == 13) {
                $("#frm").submit();
                return false;
            }
        });
    });
</script>

