<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrepaidAppliaction.aspx.cs" Inherits="EyouSoft.Web.Plan.PrepaidAppliaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/ValiDatorForm.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    团号：
                </td>
                <td width="35%" align="left">
                    <asp:Literal ID="litTourCode" runat="server"></asp:Literal>
                </td>
                <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    业务员：
                </td>
                <td width="35%">
                    <asp:Literal ID="litSellers" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    预付单位：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:Literal ID="litPrepaidCompany" runat="server"></asp:Literal>
                </td>
                <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="fontred">*</span>预付金额：
                </td>
                <td height="28" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtPrepaidPrices" runat="server" CssClass="inputtext formsize80"
                        valid="required|isMoney" errmsg="*请输入预付金额!|预付金额格式不正确!"></asp:TextBox>
                    元
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>最晚付款日期：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtPayDate" runat="server" CssClass="inputtext formsize80" valid="required"
                        errmsg="*请选择付款日期!" onfocus="WdatePicker({minDate:'%y-%M-#{%d}'})"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    用途说明：
                </td>
                <td colspan="3" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtUseInterpret" runat="server" TextMode="MultiLine" CssClass="inputtext formsize450"
                        Style="height: 60px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    申请人：
                </td>
                <td width="35%" align="left">
                    <asp:Literal ID="litAppPeople" runat="server"></asp:Literal>
                </td>
                <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    申请时间：
                </td>
                <td width="35%">
                    <asp:Literal ID="litAppDate" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">已申请列表</span>
            <table id="tabPrepaidList" width="100%" border="0" cellspacing="0" cellpadding="0"
                style="height: auto; zoom: 1; overflow: hidden;">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        编号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        预付单位
                    </td>
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        预付金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        最晚付款日期
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        用途说明
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        申请人
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        申请时间
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        状态
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <asp:Repeater ID="repPrepaidList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center">
                                <%# Container.ItemIndex+1 %>
                            </td> 
                            <td height="28" align="center">
                                <%# Eval("Supplier")%>
                            </td>   
                            <td height="28" align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PaymentAmount"), ProviderToMoney)%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("Deadline"),ProviderToDate)%>
                            </td>
                            <td align="left">
                                <span class="alertboxTableT">
                                    <%# Eval("Remark")%></span>
                            </td>
                            <td height="28" align="center">
                                <%# Eval("Operator")%>
                            </td>   
                            <td height="28" align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"), ProviderToDate)%>
                            </td>   
                            <td align="center">
                                <%# Eval("Status").ToString() %>
                            </td>
                            <td align="center">
                                <a href="javascript:" data-class="update">
                                    <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("RegisterId") %>" />
                                    <%#GetFinStatus((EyouSoft.Model.EnumType.FinStructure.FinStatus)(int)Eval("Status"))== true ?"修改":"查看" %></a>
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%#GetFinStatus((EyouSoft.Model.EnumType.FinStructure.FinStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.FinStructure.FinStatus), Eval("Status").ToString())) %>'>
                                    <a href="javascript:" data-class="delete">
                                        <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("RegisterId") %>" />
                                        删除</a> </asp:PlaceHolder>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="panView" runat="server"><a href="javascript:" hidefocus="true"
                style="text-indent: 0px;" data-class="SaleConfirm">销售确认</a> <a href="javascript:"
                    hidefocus="true" style="text-indent: 0px;" data-class="PlanConfirm">计调确认</a>
            </asp:PlaceHolder>
            <a href="javascript:" hidefocus="true" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var PrepaidPage = {
            sl: '<%=SL %>',
            type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("Type") %>',
            tourID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
            planID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("PlanId") %>',
            souceName: '<%=EyouSoft.Common.Utils.GetQueryStringValue("souceName") %>',
            _ID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("ID") %>',
            _iframeId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>',
            _Save: function(obj, ID) {
                $.newAjax({
                    type: "POST",
                    url: "/Plan/PrepaidAppliaction.aspx?sl=" + PrepaidPage.sl + "&action=" + obj + "&PlanId=" + PrepaidPage.planID + "&tourId=" + PrepaidPage.tourID + "&ID=" + ID,
                    cache: false,
                    dataType: 'json',
                    data: $(".alertbox-btn").find("[data-class='SaleConfirm']").closest("form").serialize(),
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = '/Plan/PrepaidAppliaction.aspx?sl=' + PrepaidPage.sl + "&PlanId=" + PrepaidPage.planID + "&tourId=" + PrepaidPage.tourID + "&souceName=" + PrepaidPage.souceName + "&iframeId=" + PrepaidPage._iframeId;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = window.location.href;
                            });
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _Delete: function(_ObjID) {
                $.newAjax({
                    type: "get",
                    url: '/Plan/PrepaidAppliaction.aspx?sl=' + PrepaidPage.sl + '&action=delete&ID=' + _ObjID,
                    cache: false,
                    dataType: 'json',
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = '/Plan/PrepaidAppliaction.aspx?sl=' + PrepaidPage.sl + "&PlanId=" + PrepaidPage.planID + "&tourId=" + PrepaidPage.tourID + "&souceName=" + PrepaidPage.souceName + "&iframeId=" + PrepaidPage._iframeId;
                            });
                            return false;
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                            return false;
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _BindBtn: function() {
                $("#tabPrepaidList").find("[data-class='update']").unbind("click");
                $("#tabPrepaidList").find("[data-class='update']").click(function() {
                    var id = $(this).find("img").attr("data-id");
                    if (id) {
                        window.location.href = '/Plan/PrepaidAppliaction.aspx?type=' + PrepaidPage.type + '&sl=' + PrepaidPage.sl + '&tourID=' + PrepaidPage.tourID + '&action=update&ID=' + id + "&PlanId=" + PrepaidPage.planID + "&souceName=" + escape(PrepaidPage.souceName) + "&iframeId=" + PrepaidPage._iframeId;
                    }
                });

                $("#tabPrepaidList").find("[data-class='delete']").unbind("click");
                $("#tabPrepaidList").find("[data-class='delete']").click(function() {
                    var newThis = $(this);
                    tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var id = newThis.find("img").attr("data-ID");
                        if (id) {
                            PrepaidPage._Delete(id);
                        }
                    });
                    return false;
                });

                //销售确认
                $(".alertbox-btn").find("[data-class='SaleConfirm']").unbind("click");
                $(".alertbox-btn").find("[data-class='SaleConfirm']").click(function() {
                    if (!ValiDatorForm.validator($("#<%=form1.ClientID %>").get(0), "alert")) {
                        return;
                    }
                    else {
                        var obj = "saveSale";
                        $(this).unbind("click");
                        $(this).text("保存中...");
                        $(this).css("background-position", "0 -55px");
                        PrepaidPage._Save(obj, PrepaidPage._ID);
                    }
                });

                //计调确认
                $(".alertbox-btn").find("[data-class='PlanConfirm']").unbind("click");
                $(".alertbox-btn").find("[data-class='PlanConfirm']").click(function() {
                    if (!ValiDatorForm.validator($("#<%=form1.ClientID %>").get(0), "alert")) {
                        return;
                    }
                    else {
                        var obj = "savePlan";
                        $(this).unbind("click");
                        $(this).text("保存中...");
                        $(this).css("background-position", "0 -55px");
                        PrepaidPage._Save(obj, PrepaidPage._ID); 
                    }
                });
            },
            _PageInit: function() {
                PrepaidPage._BindBtn();
                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("ID") %>' != "") {
                    $(".alertbox-btn").find("[data-class='SaleConfirm']").text("保存");
                    $(".alertbox-btn").find("[data-class='PlanConfirm']").hide();
                }
            }
        }

        $(document).ready(function() {
            FV_onBlur.initValid($("#<%=form1.ClientID %>").get(0));
            PrepaidPage._PageInit();
        });
    </script>

</body>
</html>
