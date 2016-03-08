<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QiTaShenHe.aspx.cs" Inherits="EyouSoft.Web.Fin.QiTaShenHe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>财务管理-杂费-</title>
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
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <span class="formtableT formtableT02">登记信息</span>
            <table id="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付项目
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付单位
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        付款日期
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        请款人
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付方式
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center">
                                <%#Eval("FeeItem")%>
                            </td>
                            <td align="left">
                                <%#Eval("Crm")%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("DealTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Dealer")%>
                            </td>
                            <td align="right" date-feeamount="<%#Eval("FeeAmount") %>">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FeeAmount"),ProviderToMoney)%>
                            </td>
                            <td align="center">
                                <%#Eval("PayTypeName")%>
                            </td>
                            <td align="left">
                                <%#Eval("Remark")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td height="28" colspan="4" align="right">
                        <strong>合计：</strong>
                    </td>
                    <td align="right">
                        <b id="b_amount" class="fontred"></b>
                    </td>
                    <td colspan="2" align="center">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
            <span class="formtableT formtableT02">审批信息</span>
            <form id="Form1" runat="server">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批状态：
                    </td>
                    <td width="35%" align="left" id="td_EVType">
                        <input type="checkbox" id="chk_EVType" style="border: none;" />
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批人：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label ID="lbl_audit" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批日期：
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_auditTime" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批意见：
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_auditRemark" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txt_auditRemark" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </form>
            <div class="hr_10">
            </div>
        </div>
        <div class="alertbox-btn">
            <asp:Panel ID="pan_InMoney" runat="server" Visible="false" Style="display: inline">
                <a href="javascript:void(0);" hidefocus="true" id="a_InMoney">财务入账</a>
            </asp:Panel>
            <asp:Panel ID="pan_ExamineVBtn" runat="server" Style="display: inline">
                <a href="javascript:void(0);" hidefocus="true" id="a_ExamineV"><s class="baochun"></s>
                    审 批</a>
            </asp:Panel>
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"

                hidefocus="true"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>

    <script type="text/javascript">
        var ExamineV = {
            parvateDataStr: {
                收入: ["收入项目", "收款日期", "收入金额", "收入方式"],
                支出: ["支出项目", "付款日期", "支付金额", "支付方式"]
            },
            Save: function(obj) {
                var that = this, url = '/Fin/QiTaShenHe.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', obj = $(obj);
                if ($("#chk_EVType:checked").length <= 0) {
                    parent.tableToolbar._showMsg('审核状态未勾选!');
                    return false;
                }
                obj.unbind();
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
                obj.html("<s class=baochun></s>  提交中...");
                $.newAjax({
                    type: "post",
                    data: $.param({
                        doType: "EV",
                        OtherFeeID: '<%=OtherFeeID %>',
                        auditRemark: $("#<%=txt_auditRemark.ClientID %>").val(),
                        parent: '<%=(int)ParentItemType %>'
                    }),
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg('审核成功!', function() {
                                window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                parent.location.href = parent.location.href;
                            });
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg);
                            that.BindBtn();
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                        that.BindBtn();
                    }
                });

                return false;
            },
            BindBtn: function() {
                var that = this;
                var obj = $("#a_ExamineV");
                obj.css("background-position", "0 0");
                obj.html("<s class=baochun></s> 审 批");
                obj.unbind("click");
                obj.click(function() {
                    that.Save(this);
                    return false;
                })

                obj = $("#a_InMoney");
                obj.css("color", "");
                obj.text("财务入账");
                obj.unbind("click").click(function() {
                    parent.Boxy.iframeDialog({
                        iframeUrl: "/FinanceManage/Common/InAccount.aspx?" + $.param({
                            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                            KeyId: Boxy.queryString("OtherFeeID"),
                            DefaultProofType: Boxy.queryString("DefaultProofType"),
                            tourId: Boxy.queryString("tourId")
                        }),
                        title: "财务入账",
                        modal: true,
                        width: "900px",
                        height: "500px",
                        draggable: true
                    });
                })
            },
            PageInit: function() {
                var that = this;
                var datastr = that.parvateDataStr['<%=ParentItemType %>'];
                var tr = $("#tab_list tr:eq(0)");
                tr.find("td:eq(0)").html(datastr[0])
                tr.find("td:eq(2)").html(datastr[1])
                tr.find("td:eq(4)").html(datastr[2])
                tr.find("td:eq(5)").html(datastr[3])
                if ($("#a_ExamineV").length <= 0) {
                    $("#tab_list tr:last").remove();
                    $("#td_EVType").html("已审核")
                }
                else {
                    /*计算合计,统计登记ID*/
                    var tablist = $("#tab_list");
                    var i = 0
                    tablist.find("td[date-feeamount]").each(function() {
                        i = tableToolbar.calculate(i, $(this).attr("date-feeamount"), "+")
                    })
                    $("#b_amount").html('<%=this.ProviderMoneyStr %>' + i);
                }
                if ('<%=IsEnableKis %>' == 'False') {
                    $("#a_InMoney").remove();
                }
                that.BindBtn();
            }
        }
        $(function() {
            ExamineV.PageInit();
        })
    </script>

</body>
</html>
