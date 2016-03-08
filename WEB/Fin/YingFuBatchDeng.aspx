<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingFuBatchDeng.aspx.cs" Inherits="EyouSoft.Web.Fin.YingFuBatchDeng" %>

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
            <table id="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" style="width: 65px;" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        计调项
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        单位名称
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        结算金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        付款日期
                    </td>
                    <td align="center" style="width: 150px;" bgcolor="#B7E0F3" class="alertboxTableT">
                        请款人
                    </td>
                    <td align="right" style="width: 75px;" bgcolor="#B7E0F3" class="alertboxTableT">
                        已登记金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        登记金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付方式
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        最晚付款日期
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-tourid="<%#Eval("TourId") %>" data-planid="<%#Eval("PlanId") %>">
                            <td height="28" align="center">
                                <%#Eval("PlanTyp")%>
                            </td>
                            <td align="left">
                                <%#Eval("Supplier")%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Payable"),ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <input type="text" class="txt_paymentDate" style="width: 70px;" onfocus="WdatePicker();"
                                    value="<%= EyouSoft.Common.UtilsCommons.GetDateString(DateTime.Now,ProviderToDate) %>" />
                            </td>
                            <td align="center" data-sellsname="<%=SiteUserInfo.Name %>" data-userid="<%=SiteUserInfo.UserId %>">
                                <uc1:SellsSelect ID="txt_Sells" runat="server" SetTitle="收款人" />
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Register"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <input type="text" class="inputtext formsize50 txt_paymentAmount" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroString((EyouSoft.Common.Utils.GetDecimal(Eval("Payable").ToString())-EyouSoft.Common.Utils.GetDecimal(Eval("Register").ToString())).ToString()) %>" />
                            </td>
                            <td align="center">
                                <select class="sel_PaymentType">
                                    <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType.支出)%>
                                </select>
                            </td>
                            <td align="center">
                                <input type="text" class="txt_Deadline" style="width: 70px;" onfocus="WdatePicker();"
                                    value="" />
                            </td>
                            <td align="left">
                                <label>
                                    <textarea cols="45" rows="5" class="inputtext txt_Remark" style="width: 100px; height: 28px;"></textarea>
                                </label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="litMsg" runat="server" Text="<tr><td colspan='10' align='center'>没有可登记项!</td></tr>"></asp:Literal>
                
            </table>
        </div>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="phdSave" runat="server"><a href="javascript:void(0);" hidefocus="true"
                id="a_QuantitySave" style="text-indent: 0px;">批量登记</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var QuantityRegister = {
            BindBtn: function() {
                var that = this;
                $(".alertbox-btn_a_active").removeClass("alertbox-btn_a_active");
                var a_QuantitySave = $("#a_QuantitySave");
                if (a_QuantitySave.length > 0) {
                    a_QuantitySave.unbind("click");
                    a_QuantitySave.css({ "background-position": "0 0px", "text-decoration": "" });
                    a_QuantitySave.text("批量登记");
                    a_QuantitySave.click(function() {
                        that.Save(this);
                        return false;
                    })
                }
            },
            Save: function(obj) {
                var that = this, dataArr = [], subDataArr = [];
                var obj = $(obj);

                $("#tab_list tr[data-tourid]").each(function() {
                    var trobj = $(this);
                    subDataArr.push(trobj.find("td:eq(3)").find("[type='text']").val()); /*付款时间*/
                    subDataArr.push(trobj.find("td:eq(4)").find("[type='text']").val()); /*请款人*/
                    subDataArr.push(trobj.find("td:eq(6)").find("[type='text']").val()); /*登记金额*/
                    subDataArr.push(trobj.find("td:eq(7)").find("select").val()); /*付款类型*/
                    subDataArr.push(trobj.find("td:eq(8)").find("[type='text']").val()); /*最晚付款日期*/
                    subDataArr.push(trobj.find("td:eq(9)").find("textarea").val()); /*备注*/
                    subDataArr.push(trobj.attr("data-tourid"));  /*团队编号*/
                    subDataArr.push(trobj.attr("data-planid"));  /*计调编号*/
                    subDataArr.push(trobj.find("td:eq(4)").find("[type='hidden']").val()); /*请款人ID*/
                    dataArr.push(subDataArr.join('|'));
                    subDataArr = [];
                })
                if (dataArr.length == 0) {
                    parent.tableToolbar._showMsg("没有可登记的项!");
                    return false;
                }
                obj.unbind("click")
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
                obj.html("<s class=baochun></s>&nbsp;&nbsp;&nbsp;提交中...");
                $.newAjax({
                    type: "post",
                    data: { data: dataArr.join(',') },
                    cache: false,
                    url: "YingFuBatchDeng.aspx?" + $.param({ doType: "Save", sl: '<%=Request.QueryString["sl"] %>' }),
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg('操作成功!', function() {
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
            },
            PageInit: function() {
                this.BindBtn();
                $("td[data-sellsname][data-userid]").each(function() {
                    var obj = $(this);
                    obj.find("[type='text']").val(obj.attr("data-sellsname"));
                    obj.find("[type='hidden']").val(obj.attr("data-userid"));

                })
            }
        }
        $(function() {
            QuantityRegister.PageInit();
        })
    </script>

</body>
</html>
