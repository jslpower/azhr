<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountRecharge.aspx.cs"
    Inherits="Web.SmsCenter.AccountRecharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ޱ����ĵ�</title>
    <link type="text/css" rel="stylesheet" href="/css/style.css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" border="0" style="margin: 0 auto;">
            <tbody>
                <tr>
                    <td width="18%" height="34" align="right">
                        �ͻ����ƣ�
                    </td>
                    <td width="82%">
                        <asp:TextBox ID="txtCompanyName" runat="server" class="inputtext formsize180" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="34" align="right">
                        ��ֵ�ˣ�
                    </td>
                    <td>
                        <asp:TextBox ID="txtContact" runat="server" class="inputtext formsize180" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="34" align="right">
                        ��ֵʱ�䣺
                    </td>
                    <td>
                        <asp:TextBox ID="txtIssueTime" runat="server" class="inputtext formsize180" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="34" align="right">
                        ��ֵ��
                    </td>
                    <td>
                        <input id="txtMoney" name="txtMoney" maxlength="5" type="text" class="inputtext formsize60" />
                        <span class="fontred">*</span>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:" id="btnSave">��Ҫ��ֵ</a><a hidefocus="true" href="javascript:"
                onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>ȡ ��</a></div>
        <table width="94%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin: 0 auto;">
            <tbody>
                <tr>
                    <td align="left">
                        <b>��˾�˻�<br>
                            ������ŵ�Ƽ����޹�˾</b><br>
                        <span style="font-size: 18px; font-weight: bold; color: #009933; font-family: Arial, Helvetica, sans-serif">
                            8001 2929 3708 0910 01</span><br>
                        �й����к����и��¼���������֧��
                        <p>
                            �������ǽ���ʱΪ����ͨ���ţ�<br>
                            �������µ�ͷ���С�� 0571-56884627</p>
                    </td>
                    <td align="left">
                        <strong>�����˻�<br>
                            <strong>1�������٣� ũ�����к��ݿƼ���֧�п��� </strong>
                            <br>
                            <span style="font-size: 18px; font-weight: bold; color: #009933; font-family: Arial, Helvetica, sans-serif">
                                622848? 0322? 1100? 65115</span>
                            <br>
                            <strong>2�������٣��й��������к���</strong><br>
                            <span style="font-size: 18px; font-weight: bold; color: #009933; font-family: Arial, Helvetica, sans-serif">
                                6222? 8015? 4111? 1051? 601</span></strong>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#btnSave").click(function() {
                if ($("#txtMoney").val() == "") {
                    parent.tableToolbar._showMsg("�������ֵ��");
                    return false;
                }
                if (!/^[0-9]*.?([0-9])*$/.test($("#txtMoney").val())) {
                    parent.tableToolbar._showMsg("��������Ч��ֵ��");
                    return false;
                }
                $(this).unbind("click");
                $(this).text("�ύ��...");
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/SmsCenter/AccountRecharge.aspx?dotype=save&sl=<%=Request.QueryString["sl"] %>",
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg,function(){ parent.window.location.href="/SmsCenter/AccountInfo.aspx?sl=<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>";});
                           
                        }
                        parent.tableToolbar._showMsg(ret.msg);
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("������æ�����Ժ�����!");
                    }
                });
                $(this).text("��Ҫ��ֵ");
                $(this).bind("click");
            });
        });
    </script>

    </form>
</body>
</html>
