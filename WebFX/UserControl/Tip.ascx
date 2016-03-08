<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tip.ascx.cs" Inherits="EyouSoft.WebFX.UserControl.Tip" %>
<table width="100%" id="table_Tip" cellspacing="0" cellpadding="0" border="0" align="center"
    style="margin: 5px auto;" class="autoAdd">
    <tbody>
        <tr>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "小费名称")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "单价")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "天数")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "合计金额")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "备注")%>
            </th>
            <th width="120" align="center">
                <%=(String)GetGlobalResourceObject("string", "操作")%>
            </th>
        </tr>
        <%if (!(this.SetQuoteTipList != null && this.SetQuoteTipList.Count > 0))
          {%>
        <tr class="tempRowTip">
            <td align="center">
                <input type="text" class="searchInput formsize120" name="txt_Quote_Tip" value="" />
            </td>
            <td align="center">
                <input type="text" class="searchInput formsize120" name="txt_Quote_Price" valid="isMoney"
                    errmsg='<%=(String)GetGlobalResourceObject("string", "请输入正确的金额")%>' value="" />
            </td>
            <td align="center">
                <input type="text" class="searchInput formsize120" name="txt_Quote_Days" valid="isInt"
                    errmsg='<%=(String)GetGlobalResourceObject("string", "请输入正确的天数")%>' value="" />
            </td>
            <td align="center">
                <input type="text" class="searchInput formsize120" name="txt_Quote_SumPrice" valid="isMoney"
                    errmsg='<%=(String)GetGlobalResourceObject("string", "请输入正确的金额")%>' value="" />
            </td>
            <td align="center">
                <input type="text" class="searchInput formsize120" name="txt_Quote_Remark" value="" />
            </td>
            <td align="center">
                <a href="javascript:void(0)" class="addbtntip">
                    <img src='<%=(String)GetGlobalResourceObject("string", "图片添加链接")%>'></a><a
                        href="javascript:void(0)" class="delbtntip">
                        <img src='<%=(String)GetGlobalResourceObject("string", "图片删除链接")%>'></a>
            </td>
        </tr>
        <%} %>
        <asp:Repeater runat="server" ID="rpQuote">
            <ItemTemplate>
                <tr class="tempRowTip">
                    <td align="center">
                        <input type="text" class="searchInput formsize120" name="txt_Quote_Tip" value="<%#Eval("Tip") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="searchInput formsize120" name="txt_Quote_Price" valid="isMoney"
                            errmsg='<%=(String)GetGlobalResourceObject("string", "请输入正确的金额")%>' value="<%#Convert.ToDecimal(Eval("Price")).ToString("f2") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="searchInput formsize120" name="txt_Quote_Days" valid="isInt"
                            errmsg='<%=(String)GetGlobalResourceObject("string", "请输入正确的天数")%>' value="<%#Eval("Days") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="searchInput formsize120" data-name="sumtipprice" name="txt_Quote_SumPrice"
                            valid="isMoney" errmsg='<%=(String)GetGlobalResourceObject("string", "请输入正确的金额")%>'
                            value="<%#Convert.ToDecimal(Eval("SumPrice")).ToString("f2") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="searchInput formsize120" name="txt_Quote_Remark" value="<%#Eval("Remark") %>" />
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="addbtntip">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片添加链接")%>'></a><a
                                href="javascript:void(0)" class="delbtntip">
                                <img src='<%=(String)GetGlobalResourceObject("string", "图片删除链接")%>'></a>
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
        })
        $("#table_Tip").delegate("input[name='txt_Quote_Days']", "blur", function() {
            var days = 0;
            var price = 0.00;
            var sumprice = 0.00;
            price = tableToolbar.getFloat($.trim($(this).closest("tr").find("input[name='txt_Quote_Price']").val()));
            days = tableToolbar.getInt($.trim($(this).val()));
            $(this).closest("tr").find("input[name='txt_Quote_SumPrice']").val(days * price);
            setTimeout(AddPrice.SumTipPrice, 200);
        })
        $("#table_Tip").delegate("input[name='txt_Quote_SumPrice']", "blur", function() {
            AddPrice.SumTipPrice();
        })

    });
</script>

