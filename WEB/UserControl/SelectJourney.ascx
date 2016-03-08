<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectJourney.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.SelectJourney" %>
<a href="javascript:void(0);" class="xuanyong" id="linktoJourney"></a>
<input type="hidden" id="hd_Journey" value="" />
<input type="hidden" id="hd_SelectJourney_Id" name="hd_SelectJourney_Id" runat="server" />

<script type="text/javascript">
    $(function() {
        $("#linktoJourney").click(function() {
            var Id = "linktoJourney";
            var LngType = $("#hidLngType").val();
            var _data = { "sl": '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', LngType: LngType, Id: Id };
            Boxy.iframeDialog({ iframeUrl: "/CommonPage/Journey.aspx", title: "行程备注", modal: true, width: "700px", height: "400px", data: _data });
        });
    });
</script>

