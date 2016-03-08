<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" 
AutoEventWireup="true" CodeBehind="YingFu.aspx.cs" Inherits="EyouSoft.Web.PrintPage.YingFu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
<table width="860" border="0" align="center" cellpadding="0" cellspacing="0" class="list">
  <tr>
    <th align="center">Date</th>
    <th align="center">FILE NO.</th>
    <th align="center">INVOICE No.</th>
    <th align="left">Detail</th>
    <th align="left">Detail</th>
    <th align="left">ISSUE DATE</th>
    <th align="left">PAY DETAIL</th>
    <th align="left">PAID DATE</th>
    <th align="left">$</th>
  </tr>
  <asp:Repeater runat="server" ID="rpt">
  <ItemTemplate>
  <tr>
    <td align="center"><asp:Label runat="server" ID="lblIssueTime"></asp:Label></td>
    <td align="center"><asp:Label runat="server" ID="lblTourCode"></asp:Label></td>
    <td align="left"><asp:TextBox runat="server" ID="txtInvoice"></asp:TextBox></td>
    <td align="left"><asp:Label runat="server" ID="lblPlanType"></asp:Label></td>
    <td align="left"><asp:Label runat="server" ID="lblSourceName"></asp:Label></td>
    <td align="left"><%#Eval("PaymentDate","{0:yyy-MM-dd}") %></td>
    <td align="left"><%#Eval("PaymentName")%></td>
    <td align="left"><%#Eval("PayTime","{0:dd-MM-yy}")%></td>
    <td align="left"><%#Eval("PaymentAmount","{0:F2}")%></td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
</table>
</asp:Content>
