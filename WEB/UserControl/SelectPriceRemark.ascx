<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectPriceRemark.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.SelectPriceRemark" %>
<a href="javascript:void(0);" class="xuanyong" id="linktoPriceRemark"></a>
<input type="hidden" id="hfReturnValue" value="" />
<input type="hidden" id="hd_SelectPriceRemark_Id" name="hd_SelectPriceRemark_Id" runat="server" />

<script type="text/javascript">
    $(function() {
        $("#linktoPriceRemark").click(function() {
            var Id = "linktoPriceRemark";
            var LngType = $("#hidLngType").val();
            var _data = { "sl": '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', LngType: LngType, Id: Id };
            Boxy.iframeDialog({ iframeUrl: "/CommonPage/PriceRemark.aspx", title: "报价备注", modal: true, width: "700px", height: "400px", data: _data });
        });
    });
</script>

