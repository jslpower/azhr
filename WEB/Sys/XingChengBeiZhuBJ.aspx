<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XingChengBeiZhuBJ.aspx.cs" ValidateRequest="false"
    Inherits="EyouSoft.Web.Sys.XingChengBeiZhuBJ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="30%" align="right" class="alertboxTableT">
                        语种：
                    </td>
                    <td align="left">
                        <select id="lngType" name="lngType">
                            <asp:Literal ID="lit_option" runat="server"></asp:Literal>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        行程备注：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_backMark" runat="server" CssClass=" inputtext formsize350" valid="required"
                            errmsg="行程备注不能为空！"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:;" id="btnSave" onclick="return pageOpt.pageSave();">
                <s class="baochun"></s>保 存</a><a hidefocus="true" href="javascript:;" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var pageOpt = {
            Params: { mark: '<%=EyouSoft.Common.Utils.GetQueryStringValue("dotype") %>', sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', lngType: '<%=EyouSoft.Common.Utils.GetQueryStringValue("lngType") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', masterID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("master") %>' },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position", "0-57px");
            },
            BindBtn: function() {
                $("#btnSave").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
            },
            pageSave: function() {
            	//同步编辑器数据到文本框
                KEditer.sync();
                var form = $("#btnSave").closest("form").get(0);
                if (ValiDatorForm.validator("form", "parent")) {
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/Sys/XingChengBeiZhuBJ.aspx?" + $.param(pageOpt.Params),
                        data: $("#btnSave").closest("form").serialize(),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = parent.window.location.href; });
                            } else {
                                parent.tableToolbar._showMsg(ret.msg);
                                pageOpt.BindBtn();
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                            pageOpt.BindBtn();
                        }
                    });

                } else {
                    return false;
                }
            }
        };
        $(function() {
        	//创建编辑器
        	KEditer.init('<%=txt_backMark.ClientID %>', {resizeMode: 0, items: keSimple, height: "200px", width: "500px"});
            $("#lngType").change(function() {
                $("#<%=txt_backMark.ClientID%>").val("");
            })
        })
    </script>

</body>
</html>
