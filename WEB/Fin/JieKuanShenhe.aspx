<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JieKuanShenhe.aspx.cs" Inherits="EyouSoft.Web.Fin.JieKuanShenhe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>借款管理</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="Form1" runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        借款人：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label ID="lbl_borrower" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        预借金额：
                    </td>
                    <td width="35%" align="left">
                        <b class="fontgreen">
                            <asp:Label ID="lbl_borrowAmount" runat="server" Text=""></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        实借金额：
                    </td>
                    <td width="35%" align="left">
                        <asp:TextBox ID="txt_realAmount" valid="required|IsDecimalTwo" errmsg="实借金额不能为空!|实借金额格式不正确!"
                            runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        预领签单数：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label ID="lbl_preSignNum" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        实领签单数：
                    </td>
                    <td width="35%" align="left">
                        <asp:TextBox ID="txt_relSignNum" valid="required|RegInteger" errmsg="实领签单数不能为空!|实领签单数格式不正确!"
                            runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批状态：
                    </td>
                    <td width="35%" align="left">
                        <input type="checkbox" id="chk_EVType" style="border: none;" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批人：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label ID="lbl_approver" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批时间：
                    </td>
                    <td width="35%" align="left">
                        <asp:TextBox ID="txt_approveDate" runat="server" CssClass="inputtext formsize80"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批意见：
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txt_approval" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)l;" id="a_ExamineA" hidefocus="true"><s class="baochun"></s>
                审 批</a><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>
    </form>

    <script type="text/javascript">
        var ExamineA = {
            Form: null,
            FormCheck: function(obj) {/*提交数据验证*/
                this.Form = $(obj).get(0);
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            Save: function(obj) {
                var that = this, url = "JieKuanShenHe.aspx?", obj = $(obj);
                if ($("#chk_EVType:checked").length <= 0) {
                    parent.tableToolbar._showMsg('审批状态未勾选!');
                    return false;
                }
                if (that.FormCheck($("form"))) {
                    obj.unbind();
                    obj.css("background-position", "0 -57px");
                    obj.css("text-decoration", "none");
                    obj.html("<s class=baochun></s>  提交中...");

                    $.newAjax({
                        type: "post",
                        data: $(that.Form).serialize() + "&" + $.param({ debitID: '<%=debitID %>' }),
                        cache: false,
                        url: url + $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' }),
                        dataType: "json",
                        success: function(data) {
                            if (parseInt(data.result) === 1) {
                                parent.tableToolbar._showMsg('审批成功!', function() {
                                    window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                    parent.location.href = parent.location.href;
                                });
                            }
                            else {
                                parent.tableToolbar._showMsg("审批失败!");
                                that.BindBtn();
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(parent.tableToobar.errorMsg);
                            that.BindBtn();
                        }
                    });
                }
                return false;
            },
            BindBtn: function() {
                var that = this;
                var a_ExamineV = $("#a_ExamineA")
                a_ExamineV.click(function() {
                    that.Save(this);
                    return false;
                })
                if ('<%=verificated %>' == "1") {
                    $("#a_ExamineA").remove();
                }
                a_ExamineV.css("background-position", "0 0");
                a_ExamineV.html("<s class=baochun></s> 审 批");
            },
            PageInit: function() {
                this.BindBtn();
            }
        }
        $(function() {
            ExamineA.PageInit();
        })
    </script>

</body>
</html>
