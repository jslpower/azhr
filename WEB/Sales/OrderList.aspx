<%@ Page Title="散拼订单" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="OrderList.aspx.cs" Inherits="EyouSoft.Web.Sales.OrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div style="height: 10px;" class="tablehead">
        </div>
        <!--列表表格-->
        <div class="tablelist-box" style="height: 638px;">
            <table width="100%" cellspacing="0" border="0" id="liststyle">
                <tbody>
                    <tr>
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th valign="middle" align="center">
                            订单号(客户确认单)
                        </th>
                        <th valign="middle" align="center">
                            下单人
                        </th>
                        <th valign="middle" align="center">
                            客源单位
                        </th>
                        <th valign="middle" align="center">
                            下单时间
                        </th>
                        <th valign="middle" align="right">
                            销售价
                        </th>
                        <th valign="middle" align="center">
                            人数
                        </th>
                        <th valign="middle" align="center">
                            合计金额
                        </th>
                        <th valign="middle" align="center">
                            查看
                        </th>
                        <th valign="middle" align="center">
                            订单状态
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" id="checkbox" name="checkbox">
                                </td>
                                <td align="center">
                                    <a href="javascript:;">
                                        <%#Eval("OrderCode")%></a>
                                </td>
                                <td align="center">
                                    <span id="CommonSalesRoom1_lbljdName">
                                        <%#Eval("Operator")%></span>
                                </td>
                                <td align="left">
                                    <a href="javascript:;">
                                        <%#Eval("BuyCompanyName")%></a>
                                </td>
                                <td align="center">
                                    <%# Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%>
                                </td>
                                <td align="right" class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%>
                                </td>
                                <td align="center">
                                    <%# Eval("Adults")%>+<%#Eval("Childs")%>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), this.ProviderToMoney)%>
                                </td>
                                <td align="center">
                                    <a class="check-btn" onclick="OrderInfo.ToOrderInfoByType('<%#Eval("TourType")%>','<%#Eval("OrderId")%>');return false;"
                                        href="javascript:void(0);"></a>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderStatus").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" Text="" runat="server"></asp:Literal>
                    <tr>
                        <td align="right" colspan="6">
                            合计：
                        </td>
                        <td align="center" class="red">
                            <%=SumCount%>人
                        </td>
                        <td align="center" class="red">
                            ￥<%=SumMoney.ToString("f2") %>
                        </td>
                        <td align="center" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none; height: 10px;" class="tablehead">
        </div>
    </div>

    <script type="text/javascript">
        var OrderInfo = {
            ToOrderInfoByType: function(tourtype, orderid) {
                var sl = '<%=Request.QueryString["sl"] %>';
                if (tourtype != null) {
                    window.location.href = "/Sales/SanPinBaoMing.aspx?OrderId=" + orderid + "&sl=" + sl + "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
                }
                else {
                    tableToolbar._showMsg("数据有误");
                }
            }
        }

        $(function() {
            tableToolbar.init({})
        })
    </script>

</asp:Content>
