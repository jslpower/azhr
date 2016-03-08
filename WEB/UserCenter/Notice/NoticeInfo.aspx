<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeInfo.aspx.cs" Inherits="Web.UserCenter.Notice.NoticeInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="center" bgcolor="#CAE9F6" class="alertboxTableT">
                    <strong>标题：<asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td height="28" align="left" class="kuang2" style="text-indent: 20px; line-height: 22px;">
                    <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#CAE9F6" class="alertboxTableT">
                    发布人：<asp:Label ID="lblAddUser" runat="server" Text=""></asp:Label>&nbsp; 发布时间：<asp:Label
                        ID="lblAddDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();parent.location.href = parent.location.href;"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
</body>
</html>
