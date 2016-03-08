<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectPriceRemark.ascx.cs" Inherits="EyouSoft.WebFX.UserControl.SelectPriceRemark" %>
<a href="javascript:void(0);" class="xuanyong" id="linktoPriceRemark"><img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>' alt="" /></a>
<input type="hidden" id="hfReturnValue" value="" />
<input type="hidden" id="hd_SelectPriceRemark_Id" name="hd_SelectPriceRemark_Id" runat="server" />

<script type="text/javascript">
    $(function() {
        $("#linktoPriceRemark").click(function() {
            var Id = "linktoPriceRemark";
            var LngType = $("#hidLngType").val();
            var _data = { "sl": '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', LgType: LngType, Id: Id };
            Boxy.iframeDialog({ iframeUrl: "/CommonPage/PriceRemark.aspx", title: '<%=(String)GetGlobalResourceObject("string", "报价备注")%>', modal: true, width: "700px", height: "400px", data: _data });
        });
    });
</script>
