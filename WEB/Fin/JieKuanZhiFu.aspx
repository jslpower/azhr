<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JieKuanZhiFu.aspx.cs" Inherits="EyouSoft.Web.Fin.JieKuanZhiFu" %>

<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
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
                        <asp:TextBox ID="txt_realAmount" Enabled="false" valid="IsDecimalTwo" errmsg="实借金额格式不正确!"
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
                        <asp:TextBox ID="txt_relSignNum" Enabled="false" valid="isPIntegers" errmsg="实领签单数格式不正确!"
                            runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批状态：
                    </td>
                    <td width="35%" align="left">
                        <input type="checkbox" disabled="disabled" checked="checked" style="border: none;" />
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
                    <td align="left">
                        <asp:TextBox ID="txt_approval" Enabled="false" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        支付状态：
                    </td>
                    <td align="left">
                        <input type="checkbox" id="chk_payType" style="border: none;" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        出纳：
                    </td>
                    <td align="left">
                        <uc1:SellsSelect ID="txt_lender" runat="server" SetTitle="出纳" />
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        支付时间：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_lendDate" runat="server" ReadOnly="true" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" id="a_InAccount" hidefocus="true">财务入账</a> <a href="javascript:void(0)l;"
                id="a_Save" hidefocus="true"><s class="baochun"></s>支 付</a><a href="javascript:void(0);"
                    onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>
    </form>

    <script type="text/javascript">
        var ExamineA = {
            Save: function(obj) {
                var that = this, url = "JieKuanZhiFu.aspx?", obj = $(obj);
                if ($("#chk_payType:checked").length <= 0) {
                    parent.tableToolbar._showMsg('支付状态未勾选!');
                    return false;
                }
                obj.unbind();
                obj.css("background-position", "0 -57px");
                obj.css("text-decoration", "none");
                obj.html("<s class=baochun></s>  提交中...");
                $.newAjax({
                    type: "post",
                    data: {
                        debitID: '<%=debitID %>',
                        txt_lenderID: $("#<%=txt_lender.SellsIDClient%>").val(),
                        txt_lenderName: $("#<%=txt_lender.SellsNameClient %>").val()
                    },
                    cache: false,
                    url: url + $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' }),
                    dataType: "json",
                    success: function(data) {
                        if (parseInt(data.result) === 1) {
                            parent.tableToolbar._showMsg('添加成功!', function() {
                                window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                parent.location.href = parent.location.href;
                            });
                        }
                        else {
                            parent.tableToolbar._showMsg(data.msg);
                            that.BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(parent.tableToolbar.errorMsg);
                        that.BindBtn();
                    }
                });

                return false;
            },
            BindBtn: function() {
                var that = this;
                var obj = $("#a_Save")
                obj.unbind("click");
                obj.click(function() {
                    that.Save(this);
                    return false;
                })
                obj.css("background-position", "0 0");
                obj.html("<s class=baochun></s> 支 付");
                obj = $("#a_InAccount");
                obj.unbind("click");
                obj.click(function() {
                    parent.Boxy.iframeDialog({
                        iframeUrl: "/FinanceManage/Common/InAccount.aspx?" + $.param({
                            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                            KeyId: '<%=debitID %>',
                            DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.导游借款 %>',
                            tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>'
                        }),
                        title: "财务入账",
                        modal: true,
                        width: "900px",
                        height: "500px",
                        draggable: true
                    });
                    return false;
                })
                obj.css("background-position", "0 0");
                obj.html("财务入账");
            },
            PageInit: function() {
                if ('<%=IsEnableKis %>' == 'False') {
                    $("#a_InAccount").remove();
                }
                var intintstatus = '<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("intstatus")) %>';
                if (intintstatus == '<%=(int)EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付 %>') {
                    var chk_payType = $("#chk_payType")
                    chk_payType.attr("checked", "checked");
                    chk_payType.attr("disabled", "disabled");
                    $("#a_Save").remove();
                }
                else {
                    $("#a_InAccount").remove();
                    if ('<%=verificated %>' == "1") {
                        $("#a_Save").remove();
                    }
                }
                this.BindBtn();
            }
        }
        $(function() {
            ExamineA.PageInit();
        })
    </script>

</body>
</html>
