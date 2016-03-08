<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TuanXiaoFei.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.TuanXiaoFei" %>
<table width="100%" id="table_Tip" cellspacing="0" cellpadding="0" border="0" align="center"
    style="margin: 5px auto;" class="autoAdd">
    <tbody>
        <tr>
            <th align="center">
                小费名称
            </th>
            <th align="center">
                单价
            </th>
            <th align="center">
                天数
            </th>
            <th align="center">
                合计金额
            </th>
            <th align="center">
                备注
            </th>
            <th width="120" align="center">
                操作
            </th>
        </tr>
        <%if (!(this.SetXiaoFei != null && this.SetXiaoFei.Count > 0))
          {%>
        <tr class="tempRowTip">
            <td align="center">
                <input type="text" class="inputtext formsize120" name="txt_Quote_Tip" value="" />
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize120" name="txt_Quote_Price" valid="isMoney"
                    errmsg="请输入正确的单价" value="" />
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize120" name="txt_Quote_Days" valid="isInt"
                    errmsg="请输入正确的天数" value="" />
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize120" name="txt_Quote_SumPrice" valid="isMoney"
                    errmsg="请输入正确的合计金额" value="" />
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize120" name="txt_Quote_Remark" value="" />
            </td>
            <td align="center">
                <a href="javascript:void(0)" class="addbtntip">
                    <img width="48" height="20" src="../images/addimg.gif"></a><a href="javascript:void(0)"
                        class="delbtntip">
                        <img width="48" height="20" src="../images/delimg.gif"></a>
            </td>
        </tr>
        <%} %>
        <asp:Repeater runat="server" ID="rpQuote">
            <ItemTemplate>
                <tr class="tempRowTip">
                    <td align="center">
                        <input type="text" class="inputtext formsize120" name="txt_Quote_Tip" value="<%#Eval("Tip") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize120" name="txt_Quote_Price" valid="isMoney"
                            errmsg="请输入正确的单价" value="<%#Convert.ToDecimal(Eval("Price")).ToString("f2") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize120" name="txt_Quote_Days" valid="isInt"
                            errmsg="请输入正确的天数" value="<%#Eval("Days") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize120" data-name="sumtipprice" name="txt_Quote_SumPrice"
                            valid="isMoney" errmsg="请输入正确的合计金额" value="<%#Convert.ToDecimal(Eval("SumPrice")).ToString("f2") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize120" name="txt_Quote_Remark" value="<%#Eval("Remark") %>" />
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="addbtntip">
                            <img width="48" height="20" src="../images/addimg.gif"></a><a href="javascript:void(0)"
                                class="delbtntip">
                                <img width="48" height="20" src="../images/delimg.gif"></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<script type="text/javascript">
    var TipPage = {
        DelRowCallBack: function() {
            AddPrice.SumTipPrice();
            setTimeout(AddPrice.SetTotalPrice, 200);
            AddPrice.SumHeJiPrice();
        }
    };
    $(function() {
        $("#table_Tip").autoAdd({ tempRowClass: "tempRowTip", addButtonClass: "addbtntip", delButtonClass: "delbtntip", delCallBack: TipPage.DelRowCallBack });

        $("#table_Tip").delegate("input[name='txt_Quote_Price']", "blur", function() {
            var days = 0;
            var price = 0.00;
            var sumprice = 0.00;
            days = tableToolbar.getInt($.trim($(this).closest("tr").find("input[name='txt_Quote_Days']").val()));
            price = tableToolbar.getFloat($.trim($(this).val()));
            $(this).closest("tr").find("input[name='txt_Quote_SumPrice']").val(days * price);
            setTimeout(AddPrice.SumTipPrice, 200);
            setTimeout(AddPrice.SetTotalPrice, 200);
            AddPrice.SumHeJiPrice();
        })
        $("#table_Tip").delegate("input[name='txt_Quote_Days']", "blur", function() {
            var days = 0;
            var price = 0.00;
            var sumprice = 0.00;
            price = tableToolbar.getFloat($.trim($(this).closest("tr").find("input[name='txt_Quote_Price']").val()));
            days = tableToolbar.getInt($.trim($(this).val()));
            $(this).closest("tr").find("input[name='txt_Quote_SumPrice']").val(days * price);
            setTimeout(AddPrice.SumTipPrice, 200);
            setTimeout(AddPrice.SetTotalPrice, 200);
            AddPrice.SumHeJiPrice();
        })
        $("#table_Tip").delegate("input[name='txt_Quote_SumPrice']", "blur", function() {
            AddPrice.SumTipPrice();
            setTimeout(AddPrice.SetTotalPrice, 200);
            AddPrice.SumHeJiPrice();
        })

    });
</script>

