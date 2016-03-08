<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BiJiao.aspx.cs" Inherits="EyouSoft.Web.Quote.BiJiao"
    MasterPageFile="~/MasterPage/Front.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div style="width: 98.5%; margin: 10px auto;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td height="40" align="center" class="zzjg_font15" colspan="3">
                            <asp:Literal runat="server" ID="ltCostTitle"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
                                <tbody>
                                    <tr>
                                        <th align="left">
                                            项目
                                        </th>
                                    </tr>
                                    <asp:Literal runat="server" ID="ltFristCost"></asp:Literal>
                                </tbody>
                            </table>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td valign="top">
                            <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
                                <tbody>
                                    <tr>
                                        <th align="left">
                                            项目
                                        </th>
                                    </tr>
                                    <asp:Literal runat="server" ID="ltSecondCost"></asp:Literal>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div style="width: 98.5%; margin: 10px auto;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td height="40" align="center" class="zzjg_font15" colspan="3">
                            <asp:Literal runat="server" ID="ltPriceTitle"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
                                <tbody>
                                    <tr>
                                        <th align="left">
                                            项目
                                        </th>
                                    </tr>
                                    <asp:Literal runat="server" ID="ltFristPrice"></asp:Literal>
                                </tbody>
                            </table>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td valign="top">
                            <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
                                <tbody>
                                    <tr>
                                        <th align="left">
                                            项目
                                        </th>
                                    </tr>
                                    <asp:Literal runat="server" ID="ltSecondPrice"></asp:Literal>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
