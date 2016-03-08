<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Give.ascx.cs" Inherits="EyouSoft.WebFX.UserControl.Give" %>
<table width="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin: 5px auto;"
    class="autoAdd" id="Tab_Give">
    <tbody>
        <tr>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "项目")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "金额")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "备注")%>
            </th>
            <th width="120" align="center">
                <%=(String)GetGlobalResourceObject("string", "操作")%>
            </th>
        </tr>
        <%if (!(this.SetQuoteGiveList != null && this.SetQuoteGiveList.Count > 0))
          {%>
        <tr class="tempRowwupin">
            <td align="center">
                <input type="hidden" name="hidWuPinId" value="" />
                <input type="text" class="formsize120 searchInput " readonly="readonly" style="background-color: #dadada"
                    name="txtWuPin" value="" />
                <a class="xuanyongWuPin xuanyong" href="javascript:;"><img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>'></a>
            </td>
            <td align="center">
                <input type="text" class="searchInput formsize120" name="txt_WuPinPrice" value=""
                    valid="isMoney" errmsg="<%=(String)GetGlobalResourceObject("string", "请输入正确的赠送金额")%>" />
            </td>
            <td align="center">
                <input type="text" class="searchInput formsize140" name="txt_WuPinRemark" value="" />
            </td>
            <td align="center">
                <a href="javascript:void(0)" class="addbtnwp">
                    <img src='<%=(String)GetGlobalResourceObject("string", "图片添加链接")%>' alt="" /></a>
                <a href="javascript:void(0)" class="delbtnwp">
                    <img src='<%=(String)GetGlobalResourceObject("string", "图片删除链接")%>' alt="" /></a>
            </td>
        </tr>
        <%} %>
        <asp:Repeater runat="server" ID="rpGive">
            <ItemTemplate>
                <tr class="tempRowwupin">
                    <td align="center">
                        <input type="hidden" name="hidWuPinId" value="<%#Eval("ItemId")%>" />
                        <input type="text" class="formsize120 searchInput" name="txtWuPin" readonly="readonly"
                            style="background-color: #dadada" value="<%#Eval("Item") %>" />
                        <a class="xuanyongWuPin xuanyong" href="javascript:;">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>'
                                alt="" /></a>
                    </td>
                    <td align="center">
                        <input type="text" class="searchInput formsize120" name="txt_WuPinPrice" value="<%#Convert.ToDecimal(Eval("Price")).ToString("f2") %>"
                            valid="isMoney" errmsg="<%=(String)GetGlobalResourceObject("string", "请输入正确的赠送金额")%>" />
                    </td>
                    <td align="center">
                        <input type="text" class="searchInput formsize140" name="txt_WuPinRemark" value="<%#Eval("Remark") %>" />
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="addbtnwp">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片添加链接")%>' alt="" /></a>
                        <a href="javascript:void(0)" class="delbtnwp">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片删除链接")%>' alt="" /></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<script type="text/javascript">
    var GivePage = {
        DelRowCallBack: function() {
            AddPrice.SumZongFeiPrice();
            AddPrice.SetSumPrice();
        }
    };
    $(function() {
        $("#Tab_Give").autoAdd({ tempRowClass: "tempRowwupin", addButtonClass: "addbtnwp", delButtonClass: "delbtnwp", delCallBack: GivePage.DelRowCallBack });
        $(".xuanyongWuPin").live("click", function() {
            $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
            var url = "/CommonPage/selectWuPin.aspx?aid=" + $(this).attr("id") + "&";
            var hideObj = $(this).parent().find("input[type='hidden']");
            var showObj = $(this).parent().find("input[type='text']");
            if (!hideObj.attr("id")) {
                hideObj.attr("id", "hideID_" + parseInt(Math.random() * 10000000));
            }
            if (!showObj.attr("id")) {
                showObj.attr("id", "ShowID_" + parseInt(Math.random() * 10000000));
            }
            url += $.param({ hideID: $(hideObj).val(), callBack: "CallBackWuPin" })
            Boxy.iframeDialog({
                iframeUrl: url,
                title: '<%=(String)GetGlobalResourceObject("string", "选择物品")%>',
                modal: true,
                width: "948",
                height: "406"
            });
        });
        $("#Tab_Give").delegate("input[name='txt_WuPinPrice']", "blur", function() {
            AddPrice.SumZongFeiPrice();
            AddPrice.SetSumPrice();
        })
    })


    function CallBackWuPin(obj) {
        if (obj) {
            $("#" + obj.aid).parent().find("input[type='hidden']").val(obj.wupin.id);
            $("#" + obj.aid).parent().find("input[type='text']").val(obj.wupin.name);
            $("#" + obj.aid).closest("tr").find("input[name='txt_WuPinPrice']").val(obj.wupin.price);
        }
        AddPrice.SumZongFeiPrice();
        AddPrice.SetSumPrice();
    }
   
    
    
</script>

