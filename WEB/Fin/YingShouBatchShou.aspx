<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingShouBatchShou.aspx.cs" Inherits="EyouSoft.Web.Fin.YingShouBatchShou" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批量收款</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        .alertbox-outbox02 table
        {
            border-collapse: collapse;
        }
        .alertbox-outbox02 table td.alertboxTableT
        {
            border-width: 1px;
            border-color: #85c1dd;
            border-style: solid;
        }
        .alertbox-outbox02 table td
        {
            border: 1px #b8c5ce solid;
            padding: 0 3px;
        }
        .xzjeInput
        {
            text-align: right;
            width: 60px;
            padding-right: 2px;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <div style="margin: 0 auto; width: 99%;">
            <div class="hr_5">
            </div>
            <table width="99%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" id="liststyle"
                style="margin: 0 auto;">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        团号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        订单号
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        客户单位
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        线路名称
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付方式
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        出团日期
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        应收金额
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        未收金额
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        到款金额
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-orderid="<%#Eval("OrderId") %>">
                            <td height="30" align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("OrderCode")%>
                            </td>
                            <td align="left">
                                <%#Eval("BuyCompanyName")%>
                            </td>
                            <td align="left">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <select name="sel_collectionRefundMode" class="inputselect">
                                    <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType.收入)%>
                                </select>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"),ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("NotReceivedMoeny"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <input type="text" class="inputtext formsize40 txt_GetMoney" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td height="30" colspan="8" align="right">
                        <strong>合计：</strong>
                    </td>
                    <td align="right">
                        <b id="b_amount" class="fontred">0</b>
                    </td>
                </tr>
            </table>
            <div class="hr_5">
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" id="a_QuantitySave" hidefocus="true" style="text-indent: 0px;">
                批量收款</a> <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>

    <script type="text/javascript">
        var QuantitySetMoney = {
            BindBtn: function() {
                var that = this;
                $(".alertbox-btn_a_active").removeClass("alertbox-btn_a_active");
                var a_QuantitySave = $("#a_QuantitySave");
                a_QuantitySave.unbind("clcik");
                a_QuantitySave.css({ "background-position": "", "text-decoration": "" });
                a_QuantitySave.html("批量收款");
                a_QuantitySave.click(function() {
                    that.Save(this);
                    return false;
                })
                /*绑定自动计算功能*/
                $(".txt_GetMoney")
                .unbind("blur")
                .blur(function() {
                    var sum = 0;
                    $(".txt_GetMoney").each(function() {
                        sum += parseFloat($(this).val()) || 0;
                    })
                    $("#b_amount").html(sum)
                })
                /*
                .keypress(function() {
                var sumobj = $("#b_amount");
                sumobj.attr("sum", (parseFloat(sumobj.html()) || 0) - (parseFloat($(this).val()) || 0))
                })
                .keyup(function() {
                var sumobj = $("#b_amount");
                sumobj.html((parseFloat(sumobj.attr("sum")) || 0) + (parseFloat($(this).val()) || 0))
                })
                */
            },
            Save: function(obj) {
                var url = '/Fin/YingShouBatchShou.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
                var that = this, dataArr = [], subDataArr = [];
                var data = {}
                var obj = $(obj);
                obj.unbind("click")
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
                obj.html("<s class=baochun></s>&nbsp;&nbsp;&nbsp;收款中...");
                var ischk = true;
                $("#liststyle tr[data-orderid]").each(function() {
                    var obj = $(this);
                    subDataArr.push(obj.attr("data-orderid")); /*订单编号*/
                    subDataArr.push(obj.find("select[name='sel_collectionRefundMode']").val())/*支付类型*/
                    if (obj.find(":text.txt_GetMoney").val().length <= 0 || !(parseFloat(obj.find(":text.txt_GetMoney").val()))) {
                        ischk = false;
                        return false;
                    }
                    subDataArr.push(obj.find(":text.txt_GetMoney").val())/*到款金额*/
                    dataArr.push(subDataArr.join('|'));
                    subDataArr = [];
                })
                if (!ischk) {
                    parent.tableToolbar._showMsg("到款金额异常!");
                    that.BindBtn();
                    return false;
                }
                data.list = dataArr.join(',');
                data.doType = "Save";
                data.ParentType = parseInt(Boxy.queryString("ParentType")) || 1;
                $.newAjax({
                    type: "post",
                    data: data,
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg('批量收款成功!', function() {
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
                        parent.tableToolbar._showMsg(parent.tableToolbar.errorMsg);
                        that.BindBtn();
                    }
                });

                return false;
            },
            PageInit: function() {
                tableToolbar.init({
                    tableContainerSelector: "#liststyle" //表格选择器
                })
                this.BindBtn();
            }
        }
        $(function() {
            QuantitySetMoney.PageInit();
        })
    </script>

</body>
</html>
