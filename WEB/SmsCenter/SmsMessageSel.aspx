<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsMessageSel.aspx.cs"
    Inherits="EyouSoft.Web.SmsCenter.SmsMessageSel" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="/css/style.css">

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <title></title>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" height="79" cellspacing="0" cellpadding="0" class="alertboxbk1"
            border="0" bgcolor="#FFFFFF" align="center" style="margin: 0 auto; border-collapse: collapse;"
            id="liststyle">
            <tbody>
                <tr class="odd">
                    <td height="25" bgcolor="#B7E0F3" align="center" width="5%">
                        <input type="checkbox" name="checkbox" onclick="SmsMessageSel.SelAll(this);">
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center" width="10%">
                        类型
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center" width="85%">
                        常用语
                    </td>
                </tr>
                <cc2:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                        <tr class="<%#Container.ItemIndex%2==0?"":"#odd" %>">
                            <td align="center" width="5%">
                                <input type="checkbox" id="<%#Eval("PhraseId")%>" value="<%#Eval("PhraseId")%>" name="checkbox1">
                            </td>
                            <td nowrap="nowrap" align="center" width="10%">
                                <%#((EyouSoft.Model.SmsStructure.MSmsPhraseTypeBase)Eval("SmsPhraseType")).TypeName%>
                            </td>
                            <td nowrap="nowrap" align="center" width="85%" name="content">
                                <span><%#Eval("Content")%></span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </cc2:CustomRepeater>
            </tbody>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:" id="btnSelect" onclick=" return SmsMessageSel.Select();">
                <s class="xuanzhe"></s>选 择</a><a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">

        var SmsMessageSel = {
            PWindow: parent.document,
            Select: function() {
                var smsArr = [];
                $("[name='checkbox1']:checked").each(function() {
                    smsArr.push($(this).parent().parent().find("td[name='content']").find("span").text());
                });
                var txtContent = SmsMessageSel.PWindow.getElementById("txtSendContent");
                if (txtContent.value == "") {
                    txtContent.value += smsArr.join(',');
                } else {
                    txtContent.value += "," + smsArr.join(',');
                }
                window.parent.SendSms.fontNum(txtContent);
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide()
                return false;
            },

            //全选
            SelAll: function(obj) {
                $("input:checkbox").attr("checked", $(obj).attr("checked"));
            }
        };
    
    </script>

    </form>
</body>
</html>
