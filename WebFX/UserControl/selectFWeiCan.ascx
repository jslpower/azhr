<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="selectFWeiCan.ascx.cs"
    Inherits="EyouSoft.WebFX.UserControl.selectFWeiCan" %>
<table width="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin: 5px auto;"
    class="autoAdd" id="TabFengWeiCan">
    <tbody>
        <tr>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "选择")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "价格")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "备注")%>
            </th>
            <th width="120" align="center">
                <%=(String)GetGlobalResourceObject("string", "操作")%>
            </th>
        </tr>
        <asp:PlaceHolder runat="server" ID="ph_showorhide">
            <tr class="tempRowfwei">
                <td align="center">
                    <input type="hidden" name="hidfootid" value="" />
                    <input type="hidden" value="" name="hid_fpricejs" class="pricefjs" />
                    <input type="hidden" name="hid_fcaidanid" class="canid" value="" />
                    <input type="hidden" name="hid_fcanguanid" value="" class="menuid" />
                    <input type="text" class="formsize120 searchInput" name="txtfcaidanname" readonly="readonly"
                        style="background-color: #dadada" value="" />
                    <a href="javascript:;" class="fengweican xuanyong">
                        <img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>'
                            alt="" /></a>
                </td>
                <td align="center">
                    <input type="text" class="formsize50 searchInput" name="txtfprice" data-name='txtfweiprice'
                        value="">
                </td>
                <td align="center">
                    <input type="text" class="formsize140 searchInput" name="txtfremark" value="">
                </td>
                <td align="center">
                    <a href="javascript:void(0)" class="addbtnfwei">
                        <img src='<%=(String)GetGlobalResourceObject("string", "图片添加链接")%>'></a>
                    <a href="javascript:void(0)" class="delbtnfwei">
                        <img src='<%=(String)GetGlobalResourceObject("string", "图片删除链接")%>'></a>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:Repeater runat="server" ID="rptFengWeiList">
            <ItemTemplate>
                <tr class="tempRowfwei">
                    <td align="center">
                        <input type="hidden" name="hidfootid" value="<%#Eval("FootId") %>" />
                        <input type="hidden" value="<%#Convert.ToDecimal(Eval("SettlementPrice")).ToString("f2") %>"
                            name="hid_fpricejs" class='<%#Convert.ToString(Eval("FootId")).Trim()==""?"pricefjs":"" %>' />
                        <input type="hidden" name="hid_fcaidanid" class="canid" value="<%#Eval("MenuId") %>" />
                        <input type="hidden" name="hid_fcanguanid" value="<%#Eval("RestaurantId") %>" class="menuid" />
                        <input type="text" class="formsize120 searchInput" name="txtfcaidanname" readonly="readonly"
                            style="background-color: #dadada" value="<%#Eval("Menu") %>" />
                        <a href="javascript:;" class="<%#Convert.ToString(Eval("FootId")).Trim()==""?"xuanyong fengweican":"xuanyong" %>">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>'
                                alt="" />
                        </a>
                    </td>
                    <td align="center">
                        <input type="text" class="formsize50 searchInput" name="txtfprice" <%#Convert.ToString(Eval("FootId")).Trim()==""?"data-name='txtfweiprice'":"readonly='readonly' style='background-color: #dadada'" %>
                            value="<%#Convert.ToDecimal(Eval("Price")).ToString("f2") %>">
                    </td>
                    <td align="center">
                        <input type="text" class="formsize140 searchInput" name="txtfremark" value="<%#Eval("Remark") %>">
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="addbtnfwei">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片添加链接")%>'></a>
                        <%#getIsDelBtn(Eval("FootId"))%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<script type="text/javascript">
    $(function() {
        $("#TabFengWeiCan .fengweican").live("click", function() {
            $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
            var url = "/CommonPage/UserSupplier.aspx?aid=" + $(this).attr("id") + "&";
            var hideObj = $(this).parent().find("input[class='canid']"); //餐厅编号
            var hidemenu = $(this).parent().find("input[class='menuid']"); //菜单编号
            var showObj = $(this).prev("input[type='text']"); //菜单名称
            if (!hideObj.attr("id")) {
                hideObj.attr("id", "hideID_" + parseInt(Math.random() * 10000000));
            }
            if (!showObj.attr("id")) {
                showObj.attr("id", "ShowID_" + parseInt(Math.random() * 10000000));
            }
            if (!hidemenu.attr("id")) {
                hidemenu.attr("id", "hidemenu_" + parseInt(Math.random() * 10000000));
            }
            url += $.param({ suppliertype: 2, hideID: $(hideObj).val(), callBack: "CallBackFengWeiCan", ShowID: $(showObj).val(), hidcaidanid: $(hidemenu).val(),LgType:'<%=Request.QueryString["LgType"] %>' })
            Boxy.iframeDialog({
                iframeUrl: url,
                title: '<%=(String)GetGlobalResourceObject("string", "选择餐馆")%>',
                modal: true,
                width: "948",
                height: "406"
            });
        })
    })

    function CallBackFengWeiCan(obj) {
        $("#" + obj.aid).parent().find("input[type='hidden'][class='canid']").val(obj.id);
        $("#" + obj.aid).parent().find("input[type='hidden'][class='menuid']").val(obj.caidanid);
        $("#" + obj.aid).parent().find("input[type='hidden'][name='hid_fpricejs']").val(obj.pricejs);
        $("#" + obj.aid).prev("input[type='text']").val(obj.caidanname);
        $("#" + obj.aid).closest("td").next("td").find("input[name='txtfprice']").val(obj.pricesell);
        AddPrice.SumMenuPrice();
    }
</script>

