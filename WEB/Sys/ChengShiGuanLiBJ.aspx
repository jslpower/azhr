<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChengShiGuanLiBJ.aspx.cs"
    Inherits="EyouSoft.Web.Sys.ChengShiGuanLiBJ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="30%" align="right" class="alertboxTableT">
                        国家：
                    </td>
                    <td align="left">
                        <select id="sltCountry" name="sltCountry" class="inputselect">
                        </select>
                    </td>
                </tr>
                <asp:PlaceHolder ID="plc_isAddCity" runat="server" Visible="false">
                    <tr>
                        <td align="right" class="alertboxTableT">
                            省份：
                        </td>
                        <td align="left">
                            <select id="sltProvince" name="sltProvince" class="inputselect">
                            </select>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plc_isAddDistry" runat="server" Visible="false">
                    <tr>
                        <td align="right" class="alertboxTableT">
                            城市：
                        </td>
                        <td align="left">
                            <select id="sltCity" name="sltCity" class="inputselect">
                            </select>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td width="30%" align="right" class="alertboxTableT">
                        中文名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_cnName" runat="server" valid="required" errmsg="中文名称不能为空！"></asp:TextBox><font
                            class="fontbsize12">* </font>
                    </td>
                </tr>
<%--                <tr>
                    <td align="right" class="alertboxTableT">
                        泰文名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_tnName" runat="server"></asp:TextBox>
                    </td>
                </tr>
--%>                <tr>
                    <td align="right" class="alertboxTableT">
                        英文名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_enName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        简拼：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtJP" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        全拼：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtQP" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:;" id="btnSave" onclick="return pageOpt.bindSave();">
                <s class="baochun"></s>保 存</a><a hidefocus="true" href="javascript:;" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var pageOpt = {
            Params: { mark: '<%=EyouSoft.Common.Utils.GetQueryStringValue("isPro") %>', sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("isAdd") %>' },
            BindBtn: function() {
                $("#btnSave").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
            },

            PageInit: function() {
                tableToolbar.IsHandleElse = true;
                pcToobar.init({
                    gID: "#sltCountry",
                    pID: "#sltProvince",
                    cID: "#sltCity",
                    comID: '<%=this.SiteUserInfo.CompanyId %>',
                    gSelect: '<%=Request.QueryString["sltCountry"] %>',
                    pSelect: '<%=Request.QueryString["sltProvince"] %>',
                    cSelect: '<%=Request.QueryString["sltCity"] %>'
                })
            }, //
            bindSave: function() {
                var form = $("#btnSave").closest("form").get(0);
                if (ValiDatorForm.validator("form", "parent")) {
                    $("#btnSave").html("提交中...").unbind("click");
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/Sys/ChengShiGuanLiBJ.aspx?" + $.param(pageOpt.Params),
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
                } //end if
            } //
        };
        $(function() {
            pageOpt.PageInit();
        });
    </script>

</body>
</html>
