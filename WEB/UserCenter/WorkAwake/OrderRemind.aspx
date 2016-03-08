<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="OrderRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.OrderRemind" %>

<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
            <div style="float: left; padding-top: 5px;">
                <%-- <ul class="fixed">
                    <li><s class="weichuli"></s><a hidefocus="true" href="/UserCenter/WorkAwake/OrderRemind.aspx?type=1">
                        <span>未处理订单</span></a></li>
                    <li class="line"></li>
                    <li><s class="yichuli"></s><a hidefocus="true" href="/UserCenter/WorkAwake/OrderRemind.aspx?type=2">
                        <span>已处理订单</span></a></li>
                </ul>--%>
            </div>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr>
                        <th align="center" class="th-line">
                            序号
                        </th>
                        <th align="center" class="th-line">
                            订单号
                        </th>
                        <th align="center" class="th-line">
                            客户单位
                        </th>
                        <th align="center" class="th-line">
                            人数
                        </th>
                        <th align="center" class="th-line">
                            合同金额
                        </th>
                        <th align="center" class="th-line">
                            查看
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" />
                                    <%# Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderCode")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Customer")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Adults")%>
                                    <sup class="fontred">+<%#Eval("Childs")%></sup>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPrice"), ProviderToMoney)%>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0)" class="check-btn" title="查看" onclick="OrderRemind.ToOrderInfoByType('<%#Eval("TourType") %>','<%#Eval("OrderId") %>')">
                                    </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="width: 100%; text-align: center; background-color: #ffffff">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div style="border: 0 none;" class="tablehead">
            <%--<ul class="fixed">
                <li><s class="weichuli"></s><a hidefocus="true" href="/UserCenter/WorkAwake/OrderRemind.aspx?type=1">
                    <span>未处理订单</span></a></li>
                <li class="line"></li>
                <li><s class="yichuli"></s><a hidefocus="true" href="/UserCenter/WorkAwake/OrderRemind.aspx?type=2">
                    <span>已处理订单</span></a></li>
            </ul>--%>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var OrderRemind = {
            ToOrderInfoByType: function(tourtype, orderid) {
                var sl = '<%=Request.QueryString["sl"] %>'
                if (tourtype != null) {
                    if (tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼 %>' ||
                   tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼 %>' ||
                   tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼 %>'
                   ) {
                        window.location.href = "/TeamCenter/SanKeBaoMing.aspx?OrderId=" + orderid + "&sl=" + sl + "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
                    }
                    else if (tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线 %>') {
                        window.location.href = "/TeamCenter/ShortSanKeBaoMing.aspx?OrderId=" + orderid + "&sl=" + sl + "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
                    }
                    else {
                        tableToolbar._showMsg("数据有误");
                    }
                }
            }
        }


        $(function() {
            tableToolbar.init({});

        })
    
    </script>

    </form>
</asp:Content>
