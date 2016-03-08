<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="JingDianPrint.aspx.cs" Inherits="EyouSoft.Web.PrintPage.JingDianPrint"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <div id="i_div_to">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
            <tr>
                <td width="10%" height="30" align="left">
                    <span class="font14">To：</span>
                </td>
                <td width="46%" height="30" align="left">
                    <span class="font14"><asp:TextBox ID="txtunitname" runat="server" CssClass="input180"></asp:TextBox>/<asp:TextBox
                        ID="txtunitContactname" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td width="10%" align="left">
                    <span class="font14">Tel：</span>
                </td>
                <td width="12%" align="left">
                    <span class="font14"><asp:TextBox ID="txtunittel" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td width="10%" align="right">
                    <span class="font14">Fax：</span>
                </td>
                <td width="12%" align="right">
                    <span class="font14"><asp:TextBox ID="txtunitfax" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
            </tr>
            <tr>
                <td height="30" align="left">
                    <span class="font14">From：</span>
                </td>
                <td height="30" align="left">
                    <span class="font14"><asp:TextBox ID="txtsourcename" runat="server" CssClass="input180"></asp:TextBox>/<asp:TextBox
                        ID="txtname" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td align="left">
                    <span class="font14">Tel：</span>
                </td>
                <td align="left">
                    <span class="font14"><asp:TextBox ID="txttel" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td align="right">
                    <span class="font14">Fax：</span>
                </td>
                <td  align="right">
                    <span class="font14"><asp:TextBox ID="txtfax" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
            </tr>
        </table>
    </div>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="">
        <tr>
            <td height="40" align="center">
                <b class="font24">Booking Order</b><%--<select>
                    <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanBGType))) %>
                </select>--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                <b class="font16">File No：<asp:Label ID="lbTourID" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="40" align="left" class="font14">
                    Hi!Please send the confirmation back ASAP.Thanks a lot!
                </td>
            </tr>
        </tbody>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" class="list_2">
        <tbody>
<asp:Repeater ID="rptlist" runat="server">
            <HeaderTemplate>
                <tr>
                    <th rowspan="<%=listCount+1 %>" align="right">
                        Supplier
                    </th>
                    <th align="center">
                        Name
                    </th>
                    <th align="center">
                        Date
                    </th>
                    <th align="center">
                        Adult
                    </th>
                    <th align="center">
                        Child
                    </th>
                    <th align="center">
                        Adult price
                    </th>
                    <th align="center">
                        Child price
                    </th>
                    <th align="center">
                        Guide
                    </th>
                    <th align="center">
                        Total
                    </th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%#Eval("Attractions")%>
                    </td>
                    <td align="center">
                        <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("VisitTime"), ProviderToDate)%>
                    </td>
                    <td align="center">
                        <%#Eval("AdultNumber")%>
                    </td>
                    <td align="center">
                        <%#Eval("ChildNumber")%>
                    </td>
                    <td align="center">
                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), ProviderToMoney)%>
                    </td>
                    <td align="center">
                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ChildPrice"), ProviderToMoney)%>
                    </td>
                    <td align="center">
                        <%#Eval("Seats")%>
                    </td>
                    <td align="center">
                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPrice"), ProviderToMoney)%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
            <tr>
                <th align="right">
                    Guide mobile
                </th>
                <td colspan="8">
                    <asp:Label ID="lblLinkNum" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th align="right">
                    TOTAL PRICE
                </th>
                <td colspan="8">
                    <asp:Label ID="lbTotleCost" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th align="right">
                    Note
                </th>
                <td colspan="8">
                    <asp:Label ID="LbRemark" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <table class="list_2" align="center" width="696" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td height="120" align="center" style="width: 50%">
                <div id="div1">
                    </div>
            </td>
            <td colspan="3" align="center">
                Sign by
            </td>
        </tr>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="30" align="right">
                    Send date：<%=EyouSoft.Common.UtilsCommons.GetDateString(DateTime.Now,ProviderToDate)%>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
