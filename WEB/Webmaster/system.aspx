<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="system.aspx.cs" Inherits="Web.Webmaster._system" MasterPageFile="~/Webmaster/mpage.Master" %>

<%@ MasterType VirtualPath="~/Webmaster/mpage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="Scripts" ID="ScriptsContent">
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle" ID="TitleContent">
    �鿴��ϵͳ��Ϣ-<asp:Literal runat="server" ID="ltrSysName1"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="MainContent">
    <table cellpadding="2" cellspacing="1" style="font-size: 12px; width: 100%;">
        <tr>
            <td>
                ϵͳ��ţ�<asp:Literal runat="server" ID="ltrSysId"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                ��˾��ţ�<asp:Literal runat="server" ID="ltrCompanyId"></asp:Literal>
            </td>
        </tr>        
        <tr>
            <td>
                ϵͳ���ƣ�<asp:Literal runat="server" ID="ltrSysName2"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                ����ʱ�䣺<asp:Literal runat="server" ID="ltrIssueTime"></asp:Literal>
            </td>
        </tr>
        <tr class="trspace">
            <td></td>
        </tr>
        <tr>
            <td>
                ��ϵ������<asp:Literal runat="server" ID="ltrFullname"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                ��ϵ�绰��<asp:Literal runat="server" ID="ltrTelephone"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                ��ϵ�ֻ���<asp:Literal runat="server" ID="ltrMobile"></asp:Literal>
            </td>
        </tr>
        <tr class="trspace">
            <td></td>
        </tr>
        <tr>
            <td>
                �û���ţ�<asp:Literal runat="server" ID="ltrUserId"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                ��¼�˺ţ�<asp:Literal runat="server" ID="ltrUsername"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                ��¼���룺<asp:Literal runat="server" ID="ltrPassword"></asp:Literal>
            </td>
        </tr>
        <tr class="trspace">
            <td>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="phSms">
            <tr>
                <td>
                    �����˺ţ�<asp:Literal runat="server" ID="ltrSmsAccount"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    ����AppKey��<asp:Literal runat="server" ID="ltrSmsAppKey"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    ����AppSecret��<asp:Literal runat="server" ID="ltrSmsAppSecret"></asp:Literal>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phSmsCreate">
            <tr>
                <td><asp:Button ID="btnCreateSms" runat="server" Text="��δ��ͨ�����˻��������ͨ�����˻�" OnClick="btnCreateSms_Click" /></td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageRemark" ID="RemarkContent">

</asp:Content>
