<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiRunFenPeiOrderSel.aspx.cs" Inherits="EyouSoft.Web.Fin.LiRunFenPeiOrderSel" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单号选用</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                style="margin: 0 auto" id="liststyle">
                <tr>
                    <td width="5%" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        选择
                    </td>
                    <td width="15%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        订单号
                    </td>
                    <td align="left" bgcolor="#b7e0f3" class="alertboxTableT">
                        客户单位
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        人数
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        合同金额
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        结算金额
                    </td>
                    <td width="10%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        销售员
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center" style="padding: 3px 3px">
                                <input data-ordercode="<%#Eval("OrderCode") %>" name="Order" data-maoprofit="<%#Eval("MaoProfit") %>"
                                    data-orderid="<%#Eval("OrderId") %>" data-seller="<%#Eval("SellerName") %>" type="radio" />
                            </td>
                            <td>
                                <%#Eval("OrderCode")%>
                            </td>
                            <td>
                                <%#Eval("BuyCompanyName")%>
                            </td>
                            <td>
                                <%#Eval("PersonNum")%>
                            </td>
                            <td>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"),ProviderToMoney)%>
                            </td>
                            <td>
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmSettlementMoney"),ProviderToMoney)%>
                            </td>
                            <td>
                                <%#Eval("SellerName")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_msg" runat="server">
                    <tr align="center">
                        <td colspan="7">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="alertbox-btn">
                <a href="javascript:void(0);" hidefocus="true" id="a_Save"><s class="xuanzhe"></s>选
                    择</a><a href="#" hidefocus="true"><s class="chongzhi"></s>重 置</a>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var OrderList = {
            _parentWindow: null,
            _thisCallBackData: {
                Ids: null,
                OrderCodes: null,
                Staffs: null,
                MaoProfit: null
            },
            PageInit: function() {
                var that = this;
                var _queryString = Boxy.getUrlParams();
                this._parentWindow = _queryString["pIframeID"] ? window.parent.Boxy.getIframeWindow(_queryString["pIframeID"]) : parent;
                $("#a_Save").click(function() {
                    var obj = $(":radio:checked");
                    if (obj.length > 0) {
                        that._thisCallBackData.Ids = obj.attr("data-orderid");
                        that._thisCallBackData.Staffs = obj.attr("data-seller");
                        that._thisCallBackData.OrderCodes = obj.attr("data-ordercode");
                        that._thisCallBackData.MaoProfit = obj.attr("data-maoprofit");
                        var callBackFun = _queryString["callBack"];
                        var callBackFunArr = callBackFun ? callBackFun.split('.') : null;
                        var parents = that._parentWindow;
                        if (callBackFunArr) {
                            for (var item in callBackFunArr) {
                                if (callBackFunArr.hasOwnProperty(item)) {/*筛选掉原型链属性*/
                                    parents = parents[callBackFunArr[item]];
                                }
                            }
                            parents(that._thisCallBackData);
                        }
                    }
                    parent.Boxy.getIframeDialog(_queryString["iframeId"]).hide();
                    return false;
                })
            }
        }
        $(function() {
            OrderList.PageInit();
        })
    </script>

</body>
</html>
