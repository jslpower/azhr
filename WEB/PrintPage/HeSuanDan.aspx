<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeSuanDan.aspx.cs" Inherits="EyouSoft.Web.PrintPage.HeSuanDan" MasterPageFile="~/MasterPage/Print.Master" ValidateRequest="false" Title="核算单"%>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="center">
                <b class="font24"><asp:Literal runat="server" ID="ltrDanJuTitle">核算单</asp:Literal></b>
            </td>
        </tr>
        <tr>
            <td align="left">
                <b class="font16">
                    <asp:Label runat="server" ID="lbRouteName">
                    </asp:Label></b>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="696" align="center" border="0" class="list_2">
        <tr>
            <th width="108" align="right">
                团号：
            </th>
            <td width="247" align="left">
                <asp:Label runat="server" ID="lbTourCode"></asp:Label>
            </td>
            <th width="100" align="right">
                出团时间：
            </th>
            <td width="260" align="left">
                <asp:Label runat="server" ID="lbLDate">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                天数：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbTourDays">
                </asp:Label>
            </td>
            <th align="right">
                人数：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbPersonNum">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                销售员：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbSeller">
                </asp:Label>
            </td>
            <th align="right">
                OP：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbTourPlaner">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                导游：
            </th>
            <td colspan="3" align="left">
                <asp:Label runat="server" ID="lbGuid">
                </asp:Label>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-class="checkbox" id="tuankuan" />
                    <label for="tuankuan">
                        团款收入</label></b>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_tuankuan">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2"
            data-class="tab_tuankuan">
            <tr>
                <th width="125" align="center">
                    订单号
                </th>
                <th align="center">
                    客户单位
                </th>
                <th width="50" align="center">
                    销售员
                </th>
                <th width="65" align="right">
                    合同金额
                </th>
                <th width="65" align="right">
                    结算金额
                </th>
                <th width="65" align="right">
                    导游收款
                </th>
                <th width="65" align="right">
                    财务收款
                </th>
                <th width="65" align="right">
                    订单利润
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_tuankuan">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%#Eval("OrderCode")%>
                        </td>
                        <td align="left">
                            <%#Eval("BuyCompanyName")%>
                        </td>
                        <td align="center">
                            <%#Eval("SellerName")%>
                        </td>
                        <td align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)(Eval("ConfirmMoney") ?? 0), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)(Eval("ConfirmSettlementMoney") ?? 0), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)(Eval("GuideRealIncome") ?? 0), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)(Eval("ConfirmMoney") ?? 0) - (decimal)((Eval("GuideRealIncome") ?? 0)), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)(Eval("Profit") ?? 0), ProviderToMoney)%></b>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="3" align="right">
                    合计：
                </td>
                <td align="right">
                    <b>
                        <asp:Label ID="lbConfirmMoneyCount" runat="server"></asp:Label></b>
                </td>
                <td align="right">
                    <b>
                        <asp:Label runat="server" ID="lbSettlementMoneyCount"></asp:Label></b>
                </td>
                <td align="right">
                    <b>
                        <asp:Label runat="server" ID="lbGuideIncomeCount"></asp:Label></b>
                </td>
                <td align="right">
                    <b>
                        <asp:Label runat="server" ID="lbCheckMoneyCount"></asp:Label></b>
                </td>
                <td align="right">
                    <b>
                        <asp:Label runat="server" ID="lbProfitCount"></asp:Label></b>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phGouWuShouRu" Visible="false">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-class="checkbox" id="qita" />
                    <label for="qita">
                        其他收入</label></b>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_qita">
        <asp:Repeater runat="server" ID="rpt_qita">
            <HeaderTemplate>
                <table cellspacing="0" cellpadding="0" width="696" align="center" border="0" class="list_2"
                    data-class="tab_qita">
                    <tr>
                        <th width="16%">
                            收入类型
                        </th>
                        <th width="25%" align="left">
                            付款单位
                        </th>
                        <th width="9%" align="right">
                            金额
                        </th>
                        <th width="50%">
                            备注
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%#Eval("FeeItem")%>
                    </td>
                    <td align="left">
                        <%#Eval("Crm")%>
                    </td>
                    <td align="right">
                        <b>
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)(Eval("FeeAmount") ?? 0), ProviderToMoney)%></b>
                    </td>
                    <td align="left">
                        <%# EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("Remark").ToString())%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:PlaceHolder>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-class="checkbox" id="qitazhichu" />
                    <label for="qitazhichu">
                        其他支出</label></b>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_qitazhichu">
        <asp:Repeater runat="server" ID="rpt_qitazhichu">
            <HeaderTemplate>
                <table cellspacing="0" cellpadding="0" width="696" align="center" border="0" class="list_2"
                    data-class="tab_qitazhichu">
                    <tr>
                        <th width="16%">
                            支出类型
                        </th>
                        <th width="25%" align="left">
                            收款单位
                        </th>
                        <th width="9%" align="right">
                            金额
                        </th>
                        <th width="50%">
                            备注
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%#Eval("FeeItem")%>
                    </td>
                    <td align="left">
                        <%#Eval("Crm")%>
                    </td>
                    <td align="right">
                        <b>
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)(Eval("FeeAmount") ?? 0), ProviderToMoney)%></b>
                    </td>
                    <td align="left">
                        <%# EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("Remark").ToString())%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:PlaceHolder>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-class="checkbox" id="lirun" />
                    <label for="lirun">
                        隐形支出</label></b>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_lirun">
        <asp:Repeater runat="server" ID="rpt_lirun">
            <HeaderTemplate>
                <table cellspacing="0" cellpadding="0" width="696" align="center" border="0" class="list_2"
                    data-class="tab_lirun">
                    <tr>
                        <th height="31">
                            订单号/团号
                        </th>
                        <th align="center">
                            人员
                        </th>
                        <th align="right">
                            分配金额
                        </th>
                        <th align="right">
                            毛利
                        </th>
                        <th align="right">
                            净利
                        </th>
                        <th align="left">
                            备注
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td width="16%" align="center">
                        <%#Eval("TourCode")%>/<%#Eval("OrderCode")%>
                    </td>
                    <td width="8%" align="center">
                        <%#Eval("Staff")%>
                    </td>
                    <td width="12%" align="right">
                        <b>
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("Amount"), ProviderToMoney)%></b>
                    </td>
                    <td width="9%" align="right">
                        <b>
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("Gross"), ProviderToMoney)%></b>
                    </td>
                    <td width="8%" align="right">
                        <b>
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(((decimal)Eval("Gross")) - ((decimal)Eval("Amount")), ProviderToMoney)%></b>
                    </td>
                    <td align="left">
                        <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("Remark").ToString())%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:PlaceHolder>
    </asp:PlaceHolder>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-class="checkbox" id="zhichu" />
                    <label for="zhichu">
                        团队支出</label></b>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_zhichu">
        <table cellspacing="0" cellpadding="0" width="696" align="center" border="0" class="list_2"
            data-class="tab_zhichu">
            <tr>
                <th>
                    类别
                </th>
                <th align="left">
                    供应商
                </th>
                <th align="left">
                    接待行程
                </th>
                <th align="center">
                    支付方式
                </th>
                <th align="center">
                    数量
                </th>
                <th align="right">
                    结算金额
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_zhichu">
                <ItemTemplate>
                    <tr>
                        <td width="16%" align="center">
                            <%#Eval("Type")%>
                        </td>
                        <td width="25%" align="left">
                            <%#Eval("SourceName")%>
                        </td>
                        <td width="30%" align="left">
                            <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("ServiceStandard").ToString())%>
                        </td>
                        <td width="11%" align="center">
                            <%#Eval("PaymentType").ToString()%>
                        </td>
                        <td width="7%" align="center">
                            <%#Eval("Num")%>
                        </td>
                        <td width="11%" align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("Confirmation"), ProviderToMoney)%></b>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="5" align="right">
                    合计：
                </td>



                <td align="right">
                    <b>
                        <asp:Label runat="server" ID="lbSettlementMoney"></asp:Label></b>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-class="checkbox" id="baozhang" />
                    <label for="baozhang">
                        报账汇总</label></b>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_baozhang">
        <table cellspacing="0" cellpadding="0" width="696" align="center" border="0" class="list_2"
            data-class="tab_baozhang">
            <tr>
                <th width="14%" height="20" align="right">
                    导游收入
                </th>
                <th width="13%" align="right">
                    导游借款
                </th>
                <th width="14%" align="right">
                    导游支出
                </th>
                <th width="14%" align="right">
                    补领/归还
                </th>
                <th width="15%" align="center">
                    实领签单数
                </th>
                <th width="15%" align="center">
                    已使用签单数
                </th>
                <th width="15%" align="center">
                    归还签单数
                </th>
            </tr>
            <tr>
                <td width="14%" align="right">
                    <b>
                        <asp:Label ID="lb_guidesIncome" runat="server"></asp:Label></b>
                </td>
                <td width="13%" align="right">
                    <b>
                        <asp:Label ID="lb_guidesBorrower" runat="server"></asp:Label></b>
                </td>
                <td width="14%" align="right">
                    <b>
                        <asp:Label ID="lb_guidesSpending" runat="server"></asp:Label></b>
                </td>
                <td width="14%" align="right">
                    <b>
                        <asp:Label ID="lb_replacementOrReturn" runat="server"></asp:Label></b>
                </td>
                <td width="15%" align="center">
                    <asp:Label ID="lb_RCSN" runat="server"></asp:Label>
                </td>
                <td width="15%" align="center">
                    <asp:Label ID="lb_HUSN" runat="server"></asp:Label>
                </td>
                <td width="15%" align="center">
                    <asp:Label ID="lb_RSN" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-class="checkbox" id="huizhong" />
                    <label for="huizhong">
                        团队汇总信息</label></b>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_huizong">
        <table cellspacing="0" cellpadding="0" width="696" align="center" border="0" class="list_2"
            data-class="tab_huizhong">
            <tr>
                <th width="25%" align="center">
                    团队收入
                </th>
                <th width="25%" align="center">
                    团队支出
                </th>
                <th width="25%" align="center">
                    团队利润
                </th>
                <th width="25%" align="center">
                    利润率
                </th>
            </tr>
            <tr>
                <td width="25%" align="center">
                    <b>
                        <asp:Label ID="lb_tourMoneyIn" runat="server"></asp:Label></b>
                </td>
                <td width="25%" align="center">
                    <b>
                        <asp:Label ID="lb_tourMoneyOut" runat="server"></asp:Label></b>
                </td>
                <td width="25%" align="center">
                    <b>
                        <asp:Label ID="lb_tourMoney" runat="server"></asp:Label></b>
                </td>
                <td width="25%" align="center">
                    <b>
                        <asp:Label ID="lb_tourMoneyRate" runat="server"></asp:Label></b>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>

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
            }, 1000);
        }

        function showorhide(obj) {
            var self = $(obj);
            var id = self.attr("id");
            if (!self.attr("checked")) {
                self.closest("table").next("table[data-class='tab_" + id + "']").hide();
            }
            else {
                self.closest("table").next("table[data-class='tab_" + id + "']").show();
            }
        }
        $(function() {
            $("input[type='checkbox'][data-class='checkbox']").attr("checked", "checked")
            $("input[type='checkbox'][data-class='checkbox']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                if ($.trim(self.closest("table").next("table[data-class='tab_" + id + "']").text()) == "") {
                    self.closest("table").hide();
                }
            })
            $("input[type='checkbox'][data-class='checkbox']").click(function() { showorhide(this) });
        })
    </script>

</asp:Content>
