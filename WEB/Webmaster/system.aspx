<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="system.aspx.cs" Inherits="Web.Webmaster._system" MasterPageFile="~/Webmaster/mpage.Master" %>

<%@ MasterType VirtualPath="~/Webmaster/mpage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="Scripts" ID="ScriptsContent">
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle" ID="TitleContent">
    查看子系统信息-<asp:Literal runat="server" ID="ltrSysName1"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="MainContent">
    <table cellpadding="2" cellspacing="1" style="font-size: 12px; width: 100%;">
        <tr>
            <td>
                系统编号：<asp:Literal runat="server" ID="ltrSysId"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                公司编号：<asp:Literal runat="server" ID="ltrCompanyId"></asp:Literal>
            </td>
        </tr>        
        <tr>
            <td>
                系统名称：<asp:Literal runat="server" ID="ltrSysName2"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                创建时间：<asp:Literal runat="server" ID="ltrIssueTime"></asp:Literal>
            </td>
        </tr>
        <tr class="trspace">
            <td></td>
        </tr>
        <tr>
            <td>
                联系姓名：<asp:Literal runat="server" ID="ltrFullname"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                联系电话：<asp:Literal runat="server" ID="ltrTelephone"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                联系手机：<asp:Literal runat="server" ID="ltrMobile"></asp:Literal>
            </td>
        </tr>
        <tr class="trspace">
            <td></td>
        </tr>
        <tr>
            <td>
                用户编号：<asp:Literal runat="server" ID="ltrUserId"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                登录账号：<asp:Literal runat="server" ID="ltrUsername"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                登录密码：<asp:Literal runat="server" ID="ltrPassword"></asp:Literal>
            </td>
        </tr>
        <tr class="trspace">
            <td>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="phSms">
            <tr>
                <td>
                    短信账号：<asp:Literal runat="server" ID="ltrSmsAccount"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    短信AppKey：<asp:Literal runat="server" ID="ltrSmsAppKey"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    短信AppSecret：<asp:Literal runat="server" ID="ltrSmsAppSecret"></asp:Literal>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phSmsCreate">
            <tr>
                <td><asp:Button ID="btnCreateSms" runat="server" Text="暂未开通短信账户，点击开通短信账户" OnClick="btnCreateSms_Click" /></td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageRemark" ID="RemarkContent">

</asp:Content>
