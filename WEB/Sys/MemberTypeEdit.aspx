<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberTypeEdit.aspx.cs"
    Inherits="Web.Sys.MemberTypeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="9%" height="28" align="right" class="alertboxTableT">
                        类型名称：
                    </td>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtTypeName" runat="server" CssClass=" inputtext formsize180" valid="request" errmsg="类型名称不能为空！"></asp:TextBox>
                         
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn"  style="text-align: center;">
            <a hidefocus="true" href="javascript:" id="btnSave" onclick="return MemberTypeEdit.Save();">
                <s class="baochun"></s>保 存</a> <a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>
     <script type="text/javascript">
         var MemberTypeEdit = {
             Params:{sl:"<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",memuid:"<%=EyouSoft.Common.Utils.GetQueryStringValue("memuid") %>",id:"<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>"
            },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...");
                $("#btnSave").unbind("click");
                $("#btnSave").css("background-position", "0-57px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click");
                $("#btnSave").css("background-position", "0-28px");
                $("#btnSave").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    MemberTypeEdit.Save();
                    return false;
                });
            },
            CheckDate:function(){
                var msg = "";
                var typename = document.getElementById("<%=txtTypeName.ClientID%>").value;
                if (typename == "") {
                    msg += "会员类型名称不能为空!<br/>";
                }
                if(msg!=""){
                    parent.tableToolbar._showMsg(msg);
                    return false;
                }
                return true;
            },
            Save: function() {
                MemberTypeEdit.CheckDate();
                MemberTypeEdit.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sys/MemberTypeEdit.aspx?dotype=save&"+$.param(MemberTypeEdit.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg,function(){parent.window.location.href="/Sys/MemberTypeList.aspx?&"+$.param(MemberTypeEdit.Params);});
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                        MemberTypeEdit.BindBtn();
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后再试!");
                        MemberTypeEdit.BindBtn();
                    }
                });
            }
         };
     </script>
</body>
</html>
