<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingFuBatchFu.aspx.cs" Inherits="EyouSoft.Web.Fin.YingFuBatchFu" %>

<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
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
            <span class="formtableT formtableT02">登记审批信息</span>
            <table id="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 50px">
                        计调项
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 100px">
                        供应商单位
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 80px">
                        付款日期
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 50px">
                        请款人
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 50px">
                        请款金额
                    </td>
                    <td align="center" style="width: 60px" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付方式
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 80px">
                        最晚付款日期
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 80px">
                        审批状态
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 80px">
                        审批日期
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-registerid="<%#Eval("RegisterId") %>">
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
                            <td align="right" date-register="<%#Eval("Register") %>">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PaymentAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <select class="inputselect sel_PaymentType" data-paymenttype="<%#Eval("PaymentType") %>">
                                    <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType.支出) %>
                                </select>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("Deadline"), ProviderToDate)%>
                            </td>
                            <td align="left">
                                <%#Eval("Remark")%>
                            </td>
                            <td align="center">
                                <%#Eval("Status")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("ApproverTime"), ProviderToDate)%>
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
                            <asp:Label ID="lbl_amount" runat="server" Text="0"></asp:Label></b>
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
                        <uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="选择 出纳" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付日期：
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txt_PDate" onclick="WdatePicker()" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hideDeptID" runat="server" />
            <asp:HiddenField ID="hideDeptName" runat="server" />
            </form>
            <div class="hr_10">
            </div>
        </div>
        <div class="alertbox-btn">
            <%if (IsInAccount)
              { %>
            <a href="javascript:void(0);" hidefocus="true" id="a_InAccount">财务入账</a>
            <%}
              else
              { %>
            <a href="javascript:void(0);" hidefocus="true" id="a_Save"><s class="baochun"></s>支
                付</a>
            <%} %>
            <a href="javascript:void(0);" id="a_print" hidefocus="true" style="text-indent: 4px;">
                打 印</a><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var PayMoney = {
            Form: [],
            SetForm: function(obj) {/*提交数据验证*/
                var that = this;
            	that.Form = [];
                $("#tab_list tr[data-registerid]").each(function() {
                    var trobj = $(this);
                    var datas = [];
                    datas.push(trobj.attr("data-registerid"))
                    datas.push(trobj.find("select").val())
                    that.Form.push(datas.join(','))
                })
            },
            Save: function(obj) {
                var that = this;
                var istrue = false;

                var uID, uName, deptID;
                uID = $.trim($("#<%=this.SellsSelect1.SellsIDClient %>").val());
                uName = $("#<%=this.SellsSelect1.SellsNameClient %>").val();
                deptID = $("#<%=this.hideDeptID.ClientID %>").val();
                $(".sel_PaymentType").each(function() {
                    if (this.value === "") {
                        parent.tableToolbar._showMsg("请选择支付方式!");
                        $(this).focus();
                        istrue = true;
                        return false;
                    }
                })

                if (uID == "") {
                    parent.tableToolbar._showMsg("请选择出纳!");
                    return false;
                }

                if (istrue) {
                    return false;
                }

                if ($("#chk_PType").attr("checked") == false) {
                    parent.tableToolbar._showMsg('支付状态未勾选!');
                    return false;
                }
                var url = "YingFuBatchFu.aspx?" + $.param({ sl: '<%=Request.QueryString["sl"] %>', doType: "Save" }); ;
                var obj = $(obj);
                obj.unbind("click")
                obj.addClass("class", "alertbox-btn_a_active")
                obj.text("提交中...");
                that.SetForm();
                $.newAjax({
                    type: "post",
                    data: { data: that.Form.join('|'), PDate: $("#<%=txt_PDate.ClientID %>").val(), uID: uID, uName: uName, deptID: deptID },
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(data) {
                        if (parseInt(data.result) === 1) {
                            parent.tableToolbar._showMsg('支付成功!', function() {
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

                return false;
            },
            BindBtn: function() {
                var that = this;
                $("#a_print").click(function() {
                    PrintPage("#a_print");
                    return false;
                })
                $("#a_Save").unbind("click").click(function() {
                    that.Save(this);
                    return false;
                });
                $("#a_InAccount").unbind("click").click(function() {/*财务入账*/
                    var tr = $(this).closest("tr");
                    parent.Boxy.iframeDialog({
                        iframeUrl: "/FinanceManage/Common/InAccount.aspx?" + $.param({
                            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                            KeyId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("registerIds") %>',
                            DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.计调付款%>',
                            tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourID")%>'
                        }),
                        title: "财务入账",
                        modal: true,
                        width: "900px",
                        height: "500px",
                        draggable: true
                    });
                });
            },
            PageInit: function() {
                var that = this;
                /*计算合计,统计登记ID*/
                var tablist = $("#tab_list");
                var i = 0, registeridArr = [], registerid = "";
                if ('<%=IsEnableKis %>' == 'False') {
                    $("#a_InAccount").remove();
                }
                $("select[data-paymenttype]").each(function() {
                    var obj = $(this);
                    obj.val(obj.attr("data-paymenttype"));
                })
                if ("<%=IsInAccount %>" == "True") {
                    $("#chk_PType").attr("checked", "checked");
                    $("#chk_PType").attr("disabled", "disabled");
                }
                that.BindBtn();
            }

        }
        $(function() {
            PayMoney.PageInit();
        })
    </script>

</body>
</html>
