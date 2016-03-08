<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMSEdit.aspx.cs" Inherits="Web.SmsCenter.SMSEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信中心-常用短信</title>
    <link type="text/css" rel="stylesheet" href="/css/style.css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" border="0" style="margin: 0 auto;">
            <tbody>
                <tr>
                    <td height="34" align="right">
                        <span style="color: Red;">*</span><span class="addtableT">类型：</span>
                    </td>
                    <td width="82%">
                        <select name="selType" id="selType" class=" inputselect">
                        </select>
                        <span><span style="display: none;">
                            <input id="txtTypeName" name="txtTypeName" type="text" class=" inputtext formsize140" /></span>
                            <a id="TypeAdd" href="javascript:">新增</a> <a id="TypeDel" href="javascript:">删除</a></span>
                    </td>
                </tr>
                <tr>
                    <td height="34" align="right" class="style1">
                        <span style="color: Red;">*</span><span class="addtableT">发送内容：</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"
                            Height="80px"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:" id="btnSave"><s class="baochun"></s>保 存</a>
            <a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>取 消</a></div>
    </div>

    <script type="text/javascript">
        var SMSEdit = {
            TypeList: <%=TypeList %>,
            TypeSelValue: "<%=TypeSelValue %>",
            //提交表单
            Submit: function(SaveType) {
                var params = { dotype: SaveType, sl: '<%=Request.QueryString["sl"] %>', id: '<%=Request.QueryString["id"] %>' };
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/SmsCenter/SMSEdit.aspx?" + $.param(params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1" && SaveType == "btnSave") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = "/SmsCenter/SmsList.aspx?sl=" + params.sl; });
                        }
                        else if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg);
                            SMSEdit.TypeList = ret.obj;
                            SMSEdit.TypeSelValue = $("#selType").val();
                            SMSEdit.SelBind();
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("服务器忙，请稍后再试!");
                    }
                });
            },

            SelBind: function() {
                var TypeList = (SMSEdit.TypeList);
                $("#selType").empty();
                for (var i = 0; i < TypeList.length; i++) {
                    $("#selType").get(0).options.add(new Option(TypeList[i].TypeName, TypeList[i].TypeId));
                }
                if (SMSEdit.TypeSelValue != "") {
                    $("#selType").attr("value", SMSEdit.TypeSelValue)
                }
            }
        };
        $(function() {
            SMSEdit.SelBind();
            $("#TypeAdd").click(function() {
                if ($(this).text() == "保存") {
                    if ($("#txtTypeName").val() == "") {
                        parent.tableToolbar._showMsg("类型名称不能为空!");
                        return false;
                    }
                    SMSEdit.Submit("TypeAdd");
                    $(this).text("新增");
                    $(this).prev("span").attr("style", "display: none;");
                }
                else {
                    $(this).prev("span").removeAttr("style");
                    $(this).text("保存");
                }
            });
            $("#TypeDel").click(function() { SMSEdit.Submit("TypeDel"); });
            $("#btnSave").click(function() {
                if(!$("#selType").val()){
                    parent.tableToolbar._showMsg("短信类型不能为空!");
                    return false;
                }
                if ($("#<%=txtContent.ClientID %>").val() == "") {
                    parent.tableToolbar._showMsg("短信发送内容不能为空!");
                    return false;
                }
                SMSEdit.Submit("btnSave");
            });
        });
    </script>

    </form>
</body>
</html>
