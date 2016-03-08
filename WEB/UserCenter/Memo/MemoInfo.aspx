<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemoInfo.aspx.cs" Inherits="EyouSoft.Web.UserCenter.Memo.MemoInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../Css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="18%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    时间：
                </td>
                <td colspan="3" align="left">
                    <asp:Literal ID="litTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    事件紧急程度：
                </td>
                <td width="32%" align="left" bgcolor="#e0e9ef">
                    <asp:Literal ID="litMemoUrgent" runat="server"></asp:Literal>
                </td>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    完成状态：
                </td>
                <td width="35%" align="left" bgcolor="#e0e9ef">
                    <asp:Literal ID="litMemoState" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    事件标题：
                </td>
                <td colspan="3" align="left">
                    <asp:Literal ID="littitle" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    事件详细：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef" style="padding: 10px;">
                    <asp:Literal ID="litMemoText" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="#" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>
</body>
</html>
