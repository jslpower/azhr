<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingFuBatchShen.aspx.cs" Inherits="EyouSoft.Web.Fin.YingFuBatchShen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>付款审核</title>
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
                        计调项
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        供应商单位
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        付款日期
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        请款人
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        请款金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付方式
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        最晚付款日期
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center">
                                <%#Eval("PlanTyp")%>
                            </td>
                            <td align="left">
                                <%#Eval("Supplier")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("PaymentDate"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Dealer")%>
                            </td>
                            <td align="right" date-register="<%#Eval("PaymentAmount") %>" data-registerid="<%#Eval("RegisterId") %>">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PaymentAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#Eval("PaymentName")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("Deadline"), ProviderToDate)%>
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
                        <b class="fontred">
                            <asp:Label ID="lbl_amount" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td colspan="3" align="center">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
            <span class="formtableT formtableT02">审批信息</span>
            <form runat="server" id="form1">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批状态：
                    </td>
                    <td width="35%" align="left">
                        <input type="checkbox" id="chk_EAType" style="border: none;" />
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批人：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label ID="lbl_EAPerson" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批日期：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_EADate" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批意见：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_EARemark" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hd_registerIds" runat="server" />
            </form>
            <div class="hr_10">
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_ExamineA"><s class="baochun"></s>
                审 批<a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>

    </div>

    <script type="text/javascript">
        var ExamineA = {
            Form: null,
            FormCheck: function(obj) {/*提交数据验证*/
                this.Form = $(obj).get(0);
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "alert");
            },
            Save: function(obj) {
                var that = this;
                if ($("#chk_EAType:checked").length <= 0) {
                    parent.tableToolbar._showMsg('审批状态未勾选!');
                    return false;
                }
                var url = "YingFuBatchShen.aspx?" + $.param({ sl: '<%=Request.QueryString["sl"] %>', doType: "Save" });
                if (that.FormCheck($("form"))) {
                    var obj = $(obj);
                    obj.unbind("click")
                    obj.addClass("class", "alertbox-btn_a_active")
                    obj.text("提交中...");
                    $.newAjax({
                        type: "post",
                        data: $(that.Form).find("input").serialize(),
                        cache: false,
                        url: url,
                        dataType: "json",
                        success: function(data) {
                            if (parseInt(data.result) === 1) {
                                parent.tableToolbar._showMsg('审批成功!', function() {
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
                            //ajax异常--你懂得
                        parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                            that.BindBtn();
                        }
                    });
                }
                return false;
            },
            BindBtn: function() {
                var that = this;
                $("#a_ExamineA").unbind("click").click(function() {
                    that.Save(this);
                    return false;
                })
            },
            PageInit: function() {
                var that = this;
                that.BindBtn();
                /*计算合计,统计登记ID*/
                var tablist = $("#tab_list");
                var registeridArr = [], registerid = "";
                tablist.find("td[date-register]").each(function() {
                    registerid = $(this).attr("data-registerid");
                    if (registerid.length > 0) {
                        registeridArr.push(registerid);
                    }
                })
                $("#<%=hd_registerIds.ClientID %>").val(registeridArr.join(','));
            }

        }
        $(function() {
            ExamineA.PageInit();
        })
    </script>

</body>
</html>
