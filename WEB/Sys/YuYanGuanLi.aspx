<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="YuYanGuanLi.aspx.cs" Inherits="EyouSoft.Web.Sys.YuYanGuanLi" %>
<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BaseBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
            <uc1:BaseBar ID="BaseBar1" runat="server" />
        <div style="background: none;" class="table-box">
            <table width="50%" style="margin: 30px auto;">
                <tbody>
                    <tr>
                        <th colspan="2">
                            语言管理
                        </th>
                    </tr>
                    <asp:Literal ID="lit_list" runat="server"></asp:Literal>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
