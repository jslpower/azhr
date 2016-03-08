<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QiTaZhiFu.aspx.cs" Inherits="EyouSoft.Web.Fin.QiTaZhiFu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>财务管理-杂费支出-</title>
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
            <span class="formtableT formtableT02">登记审批信息</span>
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
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付方式
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批状态
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批日期
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-id="<%#Eval("Id") %>">
                            <td height="28" align="center">
                                <%#Eval("FeeItem")%>
                            </td>
                            <td align="left">
                                <%#Eval("Crm")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("DealTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Dealer")%>
                            </td>
                            <td align="center" date-register="<%#Eval("FeeAmount") %>">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FeeAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center" data-paymenttypeint="<%#Eval("PayType") %>" data-paymenttypestr="<%#Eval("PayTypeName") %>">
                                <select class="sel_PaymentType">
                                    <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType.支出) %>
                                </select>
                            </td>
                            <td align="left">
                                <%#Eval("Remark")%>
                            </td>
                            <td align="center">
                                <%#Eval("Status")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("AuditTime"), ProviderToDate)%>
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
                    <td colspan="5" align="center">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
            <span class="formtableT formtableT02">支付信息</span>
            <form runat="server" id="form1">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付状态：
                    </td>
                    <td width="35%" align="left">
                        <input type="checkbox" id="chk_PType" style="border: none;" />
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        出纳：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label ID="lbl_PPerson" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付日期：
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txt_PMDate" runat="server" onfocus="WdatePicker();" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </form>
            <div class="hr_10">
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" id="a_Pay" hidefocus="true"><s class="baochun"></s>支 付</a>
            <a href="javascript:void(0);" id="a_print" hidefocus="true" style="text-indent: 4px;">
                打 印</a> <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var PayMoney = {
            Form: [],
            SetForm: function() {/*提交数据验证*/
                var that = this;
                that.Form = [];
                $("#tab_list tr[data-id]").each(function() {
                    var trobj = $(this);
                    var datas = [];
                    datas.push(trobj.attr("data-id"))
                    datas.push(trobj.find("select").val())
                    datas.push($.trim(trobj.find(":selected").html()))
                    that.Form.push(datas.join(','))
                })
            },
            Save: function(obj) {
                var that = this;
                if ($("#chk_EAType:checked").length < 0) {
                    alert("支付状态未勾选!")
                    return false;
                }
                var url = '/Fin/QiTaZhiFu.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
                var obj = $(obj);
                obj.unbind("click")
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
                obj.html("<s class=baochun></s>  支付中...");
                that.SetForm();
                $.newAjax({
                    type: "post",
                    data: { data: that.Form.join('|'), doType: "PM", PMDate: $("#txt_PMDate").val() },
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg('支付成功!');
                            setTimeout(function() {
                                window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                parent.location.href = parent.location.href;
                            }, 1000)
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
                $("#a_print").click(function() {
                    PrintPage("#a_print");
                    return false;
                })
                var a_Pay = $("#a_Pay");
                a_Pay.unbind("click");
                a_Pay.click(function() {
                    parent.tableToolbar.ShowConfirmMsg("是否完成支付?", function() {
                        PayMoney.Save("#a_Pay");
                    })
                    return false;
                })
                a_Pay.css("background-position", "0 0")
                a_Pay.css("text-decoration", "none")
                a_Pay.html("<s class=baochun></s>支 付")
            },
            PageInit: function() {
                var that = this;
                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("isShow") %>' == "false") {
                    $("#a_Pay").remove();
                    $("input,select").attr("disabled", "disabled");
                    $("#chk_PType").attr("checked", "checked")
                }
                that.BindBtn();
                /*计算合计,统计登记ID*/
                var tablist = $("#tab_list");
                var i = 0, registeridArr = [], registerid = "";
                tablist.find("td[date-register]").each(function() {
                    //i = i + parseFloat($(this).attr("date-register"));
                    i = i + tableToolbar.getFloat($(this).attr("date-register"));
                })
                i = tableToolbar.calculate(i, 0, "+"); /*保留2位小数*/
                tablist.find("#b_amount").html(i)
                tablist.find("[data-paymenttypeint]").each(function() {
                    var obj = $(this)
                    obj.find("select").val(obj.attr("data-paymenttypeint"));
                })
            }

        }
        $(function() {
            PayMoney.PageInit();
        })
    </script>

</body>
</html>
