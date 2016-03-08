<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../../MasterPage/Print.Master"
    CodeBehind="dingdanxinxi.aspx.cs" Inherits="EyouSoft.WebFX.PrintPage.xz.fxs.dingdanxinxi" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="PrintC1" ID="Content1">
    <style type="text/css">
        .forlabel
        {
            cursor: pointer;
        }
    </style>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="left">
                <b class="font24">
                    <asp:Label runat="server" ID="lbRouteName">
                    </asp:Label></b>
            </td>
            <td align="right">
                <b class="font16">团号：<asp:Label runat="server" ID="lbTourCode"></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="20">
                <a href="<%=url %>"><b class="font14">查看全部名单</b></a>
            </td>
        </tr>
    </table>
    <asp:Repeater runat="server" ID="rpt_OrderList" OnItemDataBound="rpt_OrderList_ItemDataBound">
        <ItemTemplate>
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="30">
                        <b class="font16">
                            <input type="checkbox" name="checkbox" data-class="checkbox" id="chk_1<%#Container.ItemIndex+1 %>" />
                            <label for="chk_1<%#Container.ItemIndex+1 %>" class="forlabel">
                                订单号：<strong><%#Eval("OrderCode") %></strong></label></b>
                    </td>
                </tr>
            </table>
            <div data-class="tab_chk_1<%#Container.ItemIndex+1 %>">
                <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2"
                    data-class="tab_chk_1<%#Container.ItemIndex+1 %>">
                    <tr>
                        <th width="108" align="right">
                            客源单位
                        </th>
                        <td colspan="3">
                            <%#Eval("BuyCompanyName")%>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            联系人
                        </th>
                        <td width="193">
                            <%#Eval("DContactName")%>
                        </td>
                        <th width="100" align="right">
                            联系电话
                        </th>
                        <td width="282">
                            <%#Eval("ContactTel")%>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            订单销售员
                        </th>
                        <td>
                            <%#Eval("SellerName")%>
                        </td>
                        <th align="right">
                            下单人
                        </th>
                        <td>
                            <%#Eval("Operator")%>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            价格组成
                        </th>
                        <td colspan="3">
                            成人单价：<strong><%#EyouSoft.Common.UtilsCommons.GetMoneyString(
                                             EyouSoft.Common.Utils.GetDecimal(Eval("AdultPrice").ToString()), ProviderToMoney)%></strong>
                            X成人数：<strong><%#Eval("Adults")%></strong> + 儿童单价：<strong><%#EyouSoft.Common.UtilsCommons.GetMoneyString(
                                             EyouSoft.Common.Utils.GetDecimal(Eval("ChildPrice").ToString()), ProviderToMoney)%></strong>
                            X 儿童数：<strong><%#Eval("Childs")%></strong>+ 其它费用：<%#EyouSoft.Common.UtilsCommons.GetMoneyString(
                                             EyouSoft.Common.Utils.GetDecimal(Eval("OtherPrice").ToString()), ProviderToMoney)%>元
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            增加费用
                        </th>
                        <td>
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SaleAddCost"), ProviderToMoney)%>
                        </td>
                        <th align="right">
                            备注
                        </th>
                        <td>
                            <%#Eval("SaleAddCostRemark")%>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            减少费用
                        </th>
                        <td>
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SaleReduceCost"), ProviderToMoney)%>
                        </td>
                        <th align="right">
                            备注
                        </th>
                        <td>
                            <%#Eval("SaleReduceCostRemark")%>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            合计金额
                        </th>
                        <td colspan="3">
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPrice"), ProviderToMoney)%>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            订单备注
                        </th>
                        <td colspan="3">
                            <%#Eval("OrderRemark")%>
                        </td>
                    </tr>
                </table>
            </div>
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="30">
                        <b class="font16">
                            <input type="checkbox" name="checkbox" data-class="checkbox" id="chk_2<%#Container.ItemIndex+1 %>" />
                            <label for="chk_2<%#Container.ItemIndex+1 %>" class="forlabel">
                                退款信息</label></b>
                    </td>
                </tr>
            </table>
            <div data-class="tab_chk_2<%#Container.ItemIndex+1 %>">
                <asp:Repeater runat="server" ID="rpt_BackList">
                    <HeaderTemplate>
                        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                            <tr>
                                <th width="80" align="center">
                                    退款人
                                </th>
                                <th width="80" align="center">
                                    退款金额
                                </th>
                                <th width="80" align="center">
                                    退款方式
                                </th>
                                <th align="center">
                                    备注
                                </th>
                                <th width="80" align="center">
                                    审核
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("CollectionRefundOperator")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("CollectionRefundAmount"), ProviderToMoney)%>
                            </td>
                            <td align="center">
                                <%#Eval("CollectionRefundModeName")%>
                            </td>
                            <td>
                                <%#Eval("Memo")%>
                            </td>
                            <td align="center">
                                <%#(bool)Eval("IsCheck")?"已审核":"未审核"%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <tr>
                    <th width="100" align="right">
                        变更增加
                    </th>
                    <td width="132">
                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("SumPriceAddCost"), ProviderToMoney)%>
                    </td>
                    <th width="100" align="right">
                        备注
                    </th>
                    <td>
                        <%#Eval("SumPriceAddCostRemark")%>
                    </td>
                </tr>
                <tr>
                    <th align="right">
                        变更减少
                    </th>
                    <td>
                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("SumPriceReduceCost"), ProviderToMoney)%>
                    </td>
                    <th align="right">
                        备注
                    </th>
                    <td>
                        <%#Eval("SumPriceReduceCostRemark")%>
                    </td>
                </tr>
                <tr>
                    <th align="right">
                        确认金额
                    </th>
                    <td colspan="3">
                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("ConfirmMoney"), ProviderToMoney)%>
                    </td>
                </tr>
            </table>
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="30">
                        <b class="font16">
                            <input type="checkbox" name="checkbox" data-class="checkbox" id="chk_3<%#Container.ItemIndex+1 %>" />
                            <label for="chk_3<%#Container.ItemIndex+1 %>" class="forlabel">
                                游客信息</label></b>
                    </td>
                </tr>
            </table>
            <div data-class="tab_chk_3<%#Container.ItemIndex+1 %>">
                <asp:Repeater runat="server" ID="rpt_CustomerList">
                    <HeaderTemplate>
                        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                            <tr>
                                <th align="center">
                                    姓名
                                </th>
                                <th align="center">
                                    性别
                                </th>
                                <th align="center">
                                    类型
                                </th>
                                <th align="center">
                                    证件号
                                </th>
                                <th align="center">
                                    联系手机
                                </th>
                                <th align="center">
                                    备注
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("CnName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Gender").ToString()%>
                            </td>
                            <td align="center">
                                <%#Eval("VisitorType").ToString()%>
                            </td>
                            <td align="center">
                                <%#Eval("CardNumber")%>
                            </td>
                            <td align="center">
                                <%#Eval("Contact")%>
                            </td>
                            <td align="center">
                                <%#Eval("Remark")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <script type="text/javascript">
        function PrevFun() {
            $("input[type='checkbox'][data-class='checkbox']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                var checked = self.attr("checked");
                self.after("<label data-id='" + id + "' data-type='checkbox' data-checked='" + checked + "'></label>");
                if (!self.attr("checked")) {
                    self.closest("table").hide();
                }
                self.remove();
            })

            setTimeout(function() {
                $("label[data-type='checkbox']").each(function() {
                    var id = $(this).attr("data-id");
                    var checded = $(this).attr("data-checked");
                    if (checded == 'false') {
                        $(this).after("<input type='checkbox' name='checkbox' data-class='checkbox' id='" + id + "' />")
                    }
                    else {
                        $(this).after("<input type='checkbox' name='checkbox' data-class='checkbox' id='" + id + "' checked='checked' />")
                    }
                    $(this).remove();
                })
                $("input[type='checkbox'][data-class='checkbox']").click(function() { showorhide(this) });
                $("input[type='checkbox'][data-class='checkbox']").closest("table").show();
            }, 9000);
        }
        function showorhide(obj) {
            var self = $(obj);
            var id = self.attr("id");
            if (!self.attr("checked")) {
                self.closest("table").next("div[data-class='tab_" + id + "']").find("table").hide();
            }
            else {
                self.closest("table").next("div[data-class='tab_" + id + "']").find("table").show();
            }
        }
        $(function() {
            $("input[type='checkbox'][data-class='checkbox']").attr("checked", "checked");
            $("input[type='checkbox'][data-class='checkbox']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                if ($.trim(self.closest("table").next("div[data-class='tab_" + id + "']").text()) == "") {
                    self.closest("table").hide();
                }
            })
            $("input[type='checkbox'][data-class='checkbox']").click(function() {
                showorhide(this);
            })
        })
    </script>

</asp:Content>
