<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanChangeContent.aspx.cs"
    Inherits="Web.UserCenter.WorkAwake.PlanChangeContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>组团团队-组团报价-变更明细</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/tankkuang.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="height: 30px; line-height: 30px; width: 98%; margin: auto; font-size: 14px;">
            <strong>第一次变更</strong></div>
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    变更标题：
                </td>
                <td colspan="3" align="left">
                    变更标题变更标题变更标题变更标题变更标题变更标题变更标题变更标题变更标题
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    变更人：
                </td>
                <td width="35%" align="left" bgcolor="#e0e9ef">
                    胡晓明
                </td>
                <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    变更时间：
                </td>
                <td width="35%" height="28" bgcolor="#e0e9ef">
                    2012-12-12
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    变更内容：
                </td>
                <td colspan="3" style="padding: 6px 4px;">
                    变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容变更内容
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="#" hidefocus="true">确认变更</a><a href="javascript:void(0)" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s class="chongzhi"></s>关闭</a>
        </div>
    </div>
    </form>
</body>
</html>
