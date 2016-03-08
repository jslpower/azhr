<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMoblieList.aspx.cs"
    Inherits="EyouSoft.Web.SmsCenter.SendMoblieList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信中心-发送历史</title>
    <link type="text/css" rel="stylesheet" href="/css/style.css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="100%" cellspacing="0" cellpadding="0" border="0" style="margin: 0 auto;table-layout:fixed;word-bressak:break-all">
            <tbody>
                <tr>
                    <td align="right" style="width:20%">
                        <span class="addtableT">发送时间：</span>
                    </td>
                    <td align="left"  style="width:80%;">
                        <asp:Label ID="lblSendTime" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="addtableT">号码：</span>
                    </td>
                    <td align="left">
                        <asp:Literal ID="litSendMobile" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="addtableT">发送内容：</span>
                    </td>
                    <td align="left">
                        <asp:Literal ID="litSendContent" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="addtableT">费用：</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblPrice" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="addtableT">状态：</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="alertbox-btn">
        <a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
            <s class="chongzhi"></s>关 闭</a></div>
    </form>
</body>
</html>
