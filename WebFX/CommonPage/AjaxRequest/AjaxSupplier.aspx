<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSupplier.aspx.cs" Inherits="EyouSoft.WebFX.CommonPage.AjaxRequest.AjaxSupplier" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!--paopao start-->

<script type="text/javascript">
    $(function() {
        var type = '<%=Request.QueryString["type"] %>';
        if (type == "<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店 %>") {
            jQuery.bt.options.closeWhenOthersOpen = true;
            $(".i_bt").bt({
                trigger: ["click"],
                contentSelector: "$(this).parent().find('span').html()",
                positions: ['right', 'left'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 5,
                spikeGirth: 15,
                width: 500,
                overlap: 0,
                centerPointY: 4,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '200%' }
            });
        }
        $("#tblList input").click(function() {
            var self = $(this);
            if (type == "2") { //餐馆（功能要求：要选择菜单）
                self.attr("id", "btn_" + parseInt(Math.random() * 100000));
                var url = "/CommonPage/selectCaiDan.aspx?";
                var hidcaidanid = self.closest("td").find("input[type='hidden'][name='hidcaidanid']").val();
                var hideObj = self.val(); //餐厅编号
                var showObj = self.attr("data-show"); //餐厅名称
                url += $.param({ hideID: hideObj, callBack: "CallBackCanTing", CaidanId: hidcaidanid, parentiframeid: '<%=Request.QueryString["iframeId"] %>', LgType: '<%=Request.QueryString["LgType"] %>' })
                top.Boxy.iframeDialog({
                    iframeUrl: url,
                    title: '<%=(String)GetGlobalResourceObject("string", "选择菜单")%>',
                    modal: true,
                    width: "560",
                    height: "460"
                });
            }
            //酒店报价paopao
            if (type == "1") {
                var hideDelayTimer = null;
                var hideDelayTimer2 = null;
                $("div.bt-content").live("mouseover", function(e) {
                    if (hideDelayTimer) clearTimeout(hideDelayTimer);
                    if (hideDelayTimer2) clearTimeout(hideDelayTimer2);
                });
                $("div.bt-content").live("mouseout", function(e) {
                    if (hideDelayTimer) clearTimeout(hideDelayTimer);
                    hideDelayTimer2 = setTimeout(function() {
                        $("div.bt-wrapper").remove();
                    }, 100);
                });
                $(".i_bt").mouseover(function() {
                    if (hideDelayTimer2) clearTimeout(hideDelayTimer2);
                    if (hideDelayTimer) clearTimeout(hideDelayTimer);
                }).mouseout(function() {
                    hideDelayTimer = setTimeout(function() {
                        $("div.bt-wrapper").remove();
                    }, 100);
                });
            }
        })



    })

    function CallBackCanTing(obj) {
        $("#tblList").find("input[type='radio']:checked").each(function() {
            $(this).closest("td").find("input[type='hidden'][name='hidcaidanid']").val(obj.caidanid);
            $(this).closest("td").find("input[type='hidden'][name='hidcaidanname']").val(obj.name);
            $(this).attr("data-pricejs", obj.pricejs);
            $(this).attr("data-priceth", obj.pricesell);
        })
    }
    function SetJiuDianBaoJia(o) {
        $("#tblList").find("input[type='radio']:checked").each(function() {
            $(this).attr("data-WPrice", $(o).val());
        })
        $(o).attr("checked", "checked");
            useSupplierPage.SetValue();
            useSupplierPage.SelectValue();
            return false;
    }
</script>

<!--paopao end-->
<table width="100%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
    id="tblList" style="margin: 0 auto">
    <tr>
        <asp:repeater runat="server" id="RepList">  
         <ItemTemplate>
            <td align="left" height="30px" width="25%">
             <input type="hidden" value="" name="hidcaidanid" /><input type="hidden" value="" name="hidcaidanname" />
        <label> 
            <input name="1" class="i_bt" type="radio" value="<%#Eval("GysId") %>" data-WPrice='0.00' data-pricejs='' data-priceth='' data-show="<%#Eval("GysName")%>" data-contactname="<%#GetContactInfo(Eval("Lxrs"),"name")%>" data-fax="<%#GetContactInfo(Eval("Lxrs"),"fax")%>" data-tel="<%#GetContactInfo(Eval("Lxrs"),"tel")%>" data-mobile='<%#GetContactInfo(Eval("Lxrs"),"mobile")%>' <%#Eval("GysName")==Request.QueryString["name"]?"checked=checked":"" %> /> 
                <%if (EyouSoft.Common.Utils.GetQueryStringValue("type").Equals("1"))%>
                <%{%>
                <span style="display: none;"><%#GetHotelPrice(Eval("GysId")) %></span><a href="javascript:void(0)" class="i_fd_lxr"><%#Eval("GysName")%></a>
                <%}%>
                <%else%>
                <%{%>
                <%#Eval("GysName")%>
                <%}%>
        </label><%#EyouSoft.Common.UtilsCommons.GetCompanyRecommend(Eval("IsTuiJian"), Eval("IsQianDan"))%>
         <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,recordCount,4) %>
        </ItemTemplate>  
        </asp:repeater>
        <tr>
            <td height="23" align="right" class="alertboxTableT" colspan="5">
                <div style="position: relative; height: 32px;">
                    <div class="pages" id="div_AjaxPage">
                        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
</table>
