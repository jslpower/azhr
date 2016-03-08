<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouKeXinXi.aspx.cs" Inherits="EyouSoft.Web.PrintPage.YouKeXinXi" MasterPageFile="~/MasterPage/Print.Master" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="left" width="110px">
                <b class="font14">游客信息汇总表</b>
            </td>
            <td align="left" width="380px">
                <b class="font24">
                    <asp:Label runat="server" ID="lbRouteName">
                    </asp:Label></b>
            </td>
            <td align="right" width="65px">
                <b class="font16">团号：</b>
            </td>
            <td align="right">
                <asp:Label runat="server" ID="lbTourCode"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" align="left">
                <span class="font14">团队发布人：<asp:Label runat="server" ID="lbName"></asp:Label></span>
            </td>
            <td align="left">
                <span class="font14">计调：<asp:Label runat="server" ID="lbTourPlaner"></asp:Label></span>
            </td>
            <td align="right">
                <span class="font14">导游：<asp:Label runat="server" ID="lbGuid"></asp:Label></span>
            </td>
        </tr>
    </table>
    <asp:Repeater runat="server" ID="rpt_CustomerList">
        <HeaderTemplate>
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <tr>
                    <th align="center">
                        序号
                    </th>
                    <th align="center">
                        客源单位
                    </th>
                    <th align="center">
                        姓名
                    </th>
                    <th align="center">
                        性别
                    </th>
                    <th align="center">
                        类型
                    </th>
                    <th align="center">
                        证件号
                    </th>
                    <th align="center">
                        联系手机
                    </th>
                    <th align="center">
                        备注
                    </th>
<%--                    <th align="center">
                        销售员
                    </th>
                    <th align="center">
                        订单号
                    </th>
--%>                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td align="center">
                    <%#Container.ItemIndex+1 %>
                </td>
                <td align="center">
                    <%#Eval("BuyCompanyName")%>
                </td>
                <td align="center">
                    <%#Eval("CnName")%>
                </td>
                <td align="center">
                    <%#Eval("Gender").ToString()%>
                </td>
                <td align="center">
                    <%#Eval("VisitorType").ToString()%>
                </td>
                <td align="center">
                    <%#Eval("CardNumber")%>
                </td>
                <td align="center">
                    <%#Eval("Contact")%>
                </td>
                <td align="center">
                    <%#Eval("Remark")%>
                </td>
                <%--<td align="center">
                    <%#Eval("SellerName")%>
                </td>
                <td align="center">
                    <%#Eval("OrderCode")%>
                </td>--%>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
