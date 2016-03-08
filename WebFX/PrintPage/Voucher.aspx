<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voucher.aspx.cs" Inherits="EyouSoft.WebFX.PrintPage.Voucher" MasterPageFile="~/MasterPage/Print.Master" ValidateRequest="false" Title="voucher"%>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" class="list_2">
        <tbody>
            <tr>
                <th>Client:</th>
                <td><asp:Literal runat="server" ID="litCustomer"></asp:Literal></td>
                <th>Issued On:</th>
                <td><asp:Literal runat="server" ID="litIssueTime"></asp:Literal></td>
            </tr>
            <tr>
                <th>Contact:</th>
                <td><asp:Literal runat="server" ID="litContact"></asp:Literal></td>
                <th>Order:</th>
                <td><asp:Literal runat="server" ID="litOrderCode"></asp:Literal></td>
            </tr>
            <tr>
                <th>Tour:</th>
                <td><asp:Literal runat="server" ID="litRouteName"></asp:Literal></td>
                <th>Adult:</th>
                <td><asp:Literal runat="server" ID="litAdult"></asp:Literal></td>
            </tr>
            <tr>
                <th>Date:</th>
                <td><asp:Literal runat="server" ID="litLeaveDate"></asp:Literal></td>
                <th>Child:</th>
                <td><asp:Literal runat="server" ID="litChild"></asp:Literal></td>
            </tr>
            <tr>
                <th>Pickup:</th>
                <td><asp:Literal runat="server" ID="litArrive"></asp:Literal></td>
                <th>Infant:</th>
                <td><asp:Literal runat="server" ID="litInfant"></asp:Literal></td>
            </tr>
            <tr>
                <th>Agent:</th>
                <td colspan="3"><asp:Literal runat="server" ID="litAgent"></asp:Literal></td>
            </tr>
        </tbody>
    </table>
</asp:Content>
