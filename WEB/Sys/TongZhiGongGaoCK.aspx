<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TongZhiGongGaoCK.aspx.cs"
    Inherits="EyouSoft.Web.Sys.JiChuSheZhi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="24%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    标题：
                </td>
                <td width="76%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布对象：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbObj" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    内容：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <p style="padding: 5px;">
                        <asp:Label ID="lbContent" runat="server"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    附件：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbFile" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否提醒：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="LbAwake" runat="server"></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否发送短信：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="LbSendmsg" runat="server"></asp:Label>
                    <asp:Label ID="LbMsgcontent" runat="server"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布人：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbSender" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布时间：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbTime" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
