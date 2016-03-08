<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendEmail.ascx.cs" Inherits="EyouSoft.WebFX.UserControl.SendEmail" %>
<table height="35" cellspacing="0" cellpadding="0" width="696" align="center" border="0"
    id="tbEmail" style="visibility: <%=string.IsNullOrEmpty(Request.QueryString["LngType"])?"hidden":"visibility"%>">
    <tr align="center">
        <td align="right">
            <%=(String)GetGlobalResourceObject("string", "邮件接收方地址")%>
            ：
        </td>
        <td>
            <input type="text" id="txtToEmail" style="border-bottom-width: 1px; border-bottom-style: solid;
                border-bottom-color: #000000; border-top-width: 0px; border-right-width: 0px;
                border-left-width: 0px; border-top-style: none; border-right-style: none; border-left-style: none;
                width: 170px" value="" />
        </td>
        <td style="width: 350px" align="left">
            <input type="button" value="发送邮件" id="btnSendEmail" onclick="SendEmail.Send()" style="border: 0px;
                font-size: 12px; width: 63px; height: 20px; cursor: pointer" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    $(document).ready(function() {
        $("#txtToEmail").bind("keypress", function(e) {
            if (e.keyCode == 13) {
                $("#btnSendEmail").click();
                return false;
            }
        });
    });
    var SendEmail = {
        ResponeEmail: function(subject, content) {
            var toEmail = $.trim($("#txtToEmail").val());
            var parms = { "CompanyId": "<%=CurrCompanyId %>", "CompanyName": "<%=CurrCompanyName %>", "ToEmail": toEmail, "Content": content, "Subject": subject }
            $.ajax({
                url: "/Ashx/AjaxSendEmail.ashx",
                type: "post",
                data: $.param(parms),
                dataType: "text",
                success: function(msg) {
                    alert('<%=(String)GetGlobalResourceObject("string", "' + msg + '")%>');
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                    alert(errorThrown);
                }
            });
        },
        Send: function() {
            var toEmail = $.trim($("#txtToEmail").val());
            var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
            if (!reg.test(toEmail)) {
                alert('<%=(String)GetGlobalResourceObject("string", "请输入正确的邮箱地址")%>');
                return;
            }
            if (confirm('<%=(String)GetGlobalResourceObject("string", "是否确定发送")%>')) {
                var subject = '<%=(String)GetGlobalResourceObject("string", "报价单")%>', content = $("#divAllHtml").html();
                var printType = "<%=PrintType %>"; //打印类型[1:行程单;2:报价单;3:计调安排确认单;4:出团通知书;5:出团确认单;6:费用结算单]
                switch (printType) {
                    case "1":
                        subject = '<%=(String)GetGlobalResourceObject("string", "行程单")%>';
                        break;
                    case "2":
                        subject = '<%=(String)GetGlobalResourceObject("string", "报价单")%>';
                        break;
                    case "3":
                        subject = '<%=(String)GetGlobalResourceObject("string", "计调安排确认单")%>';
                        break;
                    case "4":
                        subject = '<%=(String)GetGlobalResourceObject("string", "出团通知书")%>';
                        break;
                    case "5":
                        subject = '<%=(String)GetGlobalResourceObject("string", "出团确认单")%>';
                        break;
                    case "6":
                        subject = '<%=(String)GetGlobalResourceObject("string", "费用结算单")%>';
                        break;
                    default:
                        subject = '<%=(String)GetGlobalResourceObject("string", "报价单")%>';
                        break;
                }

                $(content).find("input[type=button]").remove();
                $(content).find("input[type=submit]").remove();
                $(content).find("input[type=checkbox]").hide();
                $(content).find("input[type=text]").each(function() {
                    $(this).removeClass();
                    $(this).addClass("bottow_side");
                });
                $(content).find("textarea").each(function() {
                    $(this).css("overflow", "hidden");
                });

                if ($(content).find("#divIMG").length > 0) {
                    $(content).find("#divIMG").hide();
                }

                content = "<div>" + content + "</div>";
                SendEmail.ResponeEmail(subject, content);
            }
        }
    };
</script>

