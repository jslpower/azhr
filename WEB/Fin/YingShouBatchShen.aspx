<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingShouBatchShen.aspx.cs" Inherits="EyouSoft.Web.Fin.YingShouBatchShen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>财务管理-应收管理-批量审核</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <style type="text/css">
    body,html{background:#e9f4f9;}
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: auto; text-align: center; font-size: 12px; color: #FF0000; font-weight: bold;
            padding-bottom: 5px;">
            <asp:Label ID="lbl_listTitle" runat="server" Text=""></asp:Label>
        </div>
        <table id="tab_list" width="99%" border="0" align="center" cellpadding="0" cellspacing="0"
            style="margin: 0 auto;">
            <tr>
                <td style="height:28px;" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">序号</span>
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    订单号
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    收款日期
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    收款人
                </td>
                <td bgcolor="#B7E0F3" style="text-align:right;">
                    收款金额&nbsp;
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    收款方式
                </td>
                <td align="left" bgcolor="#B7E0F3">
                    备注
                </td>
            </tr>
            <asp:Repeater ID="rpt_list" runat="server">
                <ItemTemplate>
                    <tr data-id="<%#Eval("Id") %>" data-orderid="<%#Eval("OrderId") %>">
                        <td height="28" align="center">
                            <%#Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#Eval("OrderCode") %>
                        </td>
                        <td align="center">
                            <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("CollectionRefundDate"),ProviderToDate) %>
                        </td>
                        <td align="center">
                            <%#Eval("CollectionRefundOperator") %>
                        </td>
                        <td style="text-align:right;">
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("CollectionRefundAmount"),ProviderToMoney)%>&nbsp;
                        </td>
                        <td align="center">
                            <%#Eval("CollectionRefundModeName")%>
                        </td>
                        <td align="center">
                            <%#Eval("Memo")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder runat="server" ID="phEmpty">
                <tr>
                    <td colspan="7" style="height:28px; text-align:center;">选中的订单暂无需要审批的收款登记信息。</td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phHeJiJinE">
            <tr>
                <td style="height:28px;">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td  style="text-align:right;">
                    <b>合计金额：<asp:Literal runat="server" ID="ltrHeJiJinE"></asp:Literal></b>&nbsp;
                </td>
                <td >
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            </asp:PlaceHolder>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="phShengHe">
                <a href="javascript:void(0);" id="a_Save" hidefocus="true" style="text-indent: 0px;"> 批量审核</a>
            </asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var QuantityExamineV = {
            Save: function(obj) {
                var url = '/Fin/YingShouBatchShen.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&doType=Save';
                var that = this, dataArr = [], subDataArr = [];
                var data = { list: "" };
                var obj = $(obj);
                obj.unbind("click")
                obj.css("background-position", "0 -57px")
                obj.css("text-decoration", "none")
                obj.html("<s class=baochun></s>&nbsp;&nbsp;&nbsp;审核中...");
                $("#tab_list tr[data-orderid][data-id]").each(function() {
                    var obj = $(this);
                    subDataArr.push(obj.attr("data-orderid")); /*订单编号*/
                    subDataArr.push(obj.attr("data-id")); /**/
                    dataArr.push(subDataArr.join('|'));
                    subDataArr = [];
                });
                data.list = dataArr.join(',');
                $.newAjax({
                    type: "post",
                    data: data,
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg('审批成功!', function() {
                                window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                parent.location.href = parent.location.href;
                            });
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg);
                            that.PageInit();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                        that.PageInit();
                    }
                });
                return false;
            },
            PageInit: function() {
                var _self = this;
                var obj = $("#a_Save");
                obj.text("批量审核");
                obj.unbind("click");
                obj.click(function() { _self.Save(this); return false; });
            }
        };
        $(function() {
            QuantityExamineV.PageInit();
        });
    </script>

</body>
</html>
