<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" 
AutoEventWireup="true" CodeBehind="YingShou.aspx.cs" Inherits="EyouSoft.Web.PrintPage.YingShou" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
<table width="860" border="0" align="center" cellpadding="0" cellspacing="0" class="list">
  <tr>
    <th align="center">Date</th>
    <th align="center">FILE NO.</th>
    <th align="left">INVOICE No.</th>
    <th align="left">Detail</th>
    <th align="left">$</th>
    <th align="left">$</th>
    <th align="left">$</th>
    <th align="center">&nbsp;</th>
    <th align="center">$</th>
  </tr>
  <asp:Repeater runat="server" ID="rpt">
  <ItemTemplate>
  <tr>
    <td align="center"><asp:Label runat="server" ID="lblIssueTime"></asp:Label></td>
    <td align="center"><asp:Label runat="server" ID="lblOrderCode"></asp:Label></td>
    <td align="left"><asp:TextBox runat="server" ID="txtInvoice"></asp:TextBox></td>
    <td align="left"><asp:Label runat="server" ID="lblBuyCompanyName"></asp:Label></td>
    <td align="left"><asp:Label runat="server" ID="lblConfirmMomey"></asp:Label></td>
    <td align="left">PAID <%#Eval("CollectionRefundDate","{0:dd-MM-yy}") %></td>
    <td align="left">&nbsp;</td>
    <td align="left">CFM: 50649808</td>
    <td align="left"><%#Eval("CollectionRefundAmount","{0:F2}")%></td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
</table>
</asp:Content>
