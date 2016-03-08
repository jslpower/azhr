<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancialRegistration.aspx.cs"
    Inherits="EyouSoft.WebFX.FinancialRegistration" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type" />
    <title>分销商财务管理-登记</title>
    <link type="text/css" rel="stylesheet" href="/Css/style.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: auto; text-align: center; font-size: 12px; color: #FF0000; font-weight: bold;
            padding-bottom: 5px;">
            应付金额：<asp:Label runat="server" ID="LtTotalReceived"></asp:Label>
            已付金额：<asp:Label runat="server" ID="LtTotalSumPrice"></asp:Label>
            未付金额：<asp:Label runat="server" ID="LtTotalUnReceived"></asp:Label>
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin: 0 auto">
            <tbody>
                <tr>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        <span class="alertboxTableT">序号</span>
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        <span class="fontred">*</span>付款日期
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        <span class="fontred">*</span>收款人
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        <span class="fontred">*</span>付款金额
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        <span class="fontred">*</span>付款方式
                    </td>
                    <td bgcolor="#B7E0F3" align="center">
                        审核状态
                    </td>
                    <td bgcolor="#B7E0F3" align="center">
                        备注
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        操作
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="RpIncome">
                    <ItemTemplate>
                        <tr data-id="<%#Eval("Id") %>">
                            <td height="28" align="center">
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td align="center">
                                <input type="text" class="formsize80" onfocus="WdatePicker();" id="txt_collectionRefundDate"
                                    value="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("CollectionRefundDate"),ProviderToDate) %>" />
                            </td>
                            <td align="center">
                             <%#Eval("CollectionRefundOperator") %>
                            </td>
                            <td align="center">
                                <input type="text" size="10" id="txt_collectionRefundAmount" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("CollectionRefundAmount")) %>" />
                            </td>
                            <td align="center">
                                <label>
                                    <select id="ddl_CollectionRefundMode">
                                        <%#EyouSoft.Common.UtilsCommons.GetStrPaymentList(SiteUserInfo.CompanyId, Eval("CollectionRefundMode").ToString(), EyouSoft.Model.EnumType.ComStructure.ItemType.收入)%>
                                    </select>
                                </label>
                            </td>
                            <td align="center">
                               <label>
                                  <%#GetStatusForShenhe(Eval("IsCheck"))%>
                               </label>
                            </td>
                            <td align="center">
                                <textarea rows="" cols="" style="height: 25px; width: 110px;" id="txt_Memo" id="textarea"><%#Eval("Memo")%></textarea>
                            </td>
                            <td align="center">
                                <a data_ischeck="<%#Eval("IsCheck") %>" href="javascript:void(0)" id="_BtnUpdate">修改</a>
                                <a href="javascript:void(0)" id="_BtnDelete">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr data-id="">
                    <td height="28" align="center">
                        -
                    </td>
                    <td align="center">
                        <input type="text" class="formsize80" onfocus="WdatePicker();" id="txt_collectionRefundDate"
                            value="<%=EyouSoft.Common.UtilsCommons.GetDateString( DateTime.Now,ProviderToDate) %>" />
                    </td>
                    <td align="center">
                         <%=SiteUserInfo.Username %>
                    </td>
                    <td align="center">
                        <input type="text" size="10" id="txt_collectionRefundAmount" />
                    </td>
                    <td align="center">
                        <label>
                            <select style="width: 80px;" id="ddl_CollectionRefundMode" name="select">
                                <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(SiteUserInfo.CompanyId,EyouSoft.Model.EnumType.ComStructure.ItemType.收入)%>
                            </select>
                        </label>
                    </td>
                      <td align="center">
                        <label>
                         待审
                        </label>
                    </td>
                    <td align="center">
                        <textarea rows="" cols="" style="height: 25px; width: 110px;" id="txt_Memo" id="textarea2"></textarea>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" id="_btnSave">保存</a>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0)" id="_btnClose" onclick="window.parent.location.reload();">
                <s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var FinancialRegistration = {
            Data: {
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                OrderId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("OrderId") %>'
            },
            Submit: function(obj, type) {/*提交数据*/
                FinancialRegistration.UnBind();
                var objTr = obj.closest("tr");
                var data = {
                    Type: type,
                    OrderId: FinancialRegistration.Data.OrderId,
                    Id: objTr.attr("data-id"),
                    txt_collectionRefundDate: objTr.find("#txt_collectionRefundDate").val(),
                    ddl_CollectionRefundMode: objTr.find("#ddl_CollectionRefundMode").val(),
                    txt_collectionRefundAmount: objTr.find("#txt_collectionRefundAmount").val(),
                    shenhe: objTr.find("#shenhe").val(),
                    txt_Memo: objTr.find("#txt_Memo").val(),
                    SellsSelect1_txtSellName:objTr.find("#SellsSelect1_txtSellName").val() ,
                    SellsSelect1_hideSellID: objTr.find("#SellsSelect1_hideSellID").val()
                };

                $.newAjax({
                    url: "FinancialRegistration.aspx?sl=" + FinancialRegistration.Data.sl,
                    type: "post",
                    data: $.param(data),
                    dataType: "json",
                    success: function(back) {
                        if (back.result == "1") {
                            parent.tableToolbar._showMsg(back.msg, function() {
                                window.location.href = "FinancialRegistration.aspx?" + $.param(FinancialRegistration.Data);
                            });

                        }
                        else {
                            parent.tableToolbar._showMsg(back.msg);
                        }
                        FinancialRegistration.Bind();
                    },
                    error: function() {
                        alert("服务器忙！");
                    }

                });

            },
            Bind: function() {
                $("a[data_ischeck='False']").bind('click');
            },
            UnBind: function() {
                $("a[data_ischeck='False']").unbind('click');
            }
        };

        $(function() {
            $("a[data_ischeck='True']").closest("td").html("已审核");

            var that = FinancialRegistration;
            $("#_btnSave").css("color", "").unbind("click").click(function() {
                that.Submit($(this), "Add")
                return false;
            });


            $("#_BtnUpdate").css("color", "").unbind("click").click(function() {
                that.Submit($(this), "Update")
                return false;
            });


            $("#_BtnDelete").css("color", "").unbind("click").click(function() {
                that.Submit($(this), "Delete")
                return false;
            });
        });
    </script>

</body>
</html>
