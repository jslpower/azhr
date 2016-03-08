<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectJourneySpot.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.SelectJourneySpot" %>
<a href="javascript:void(0);" class="xuanyong" id="linktoJourneySpot"></a>
<input type="hidden" id="hd_JourneySpot" value="" />
<input type="hidden" id="hd_SelectJourneySpot_Id" name="hd_SelectJourneySpot_Id" runat="server" />

<script type="text/javascript">
    $(function() {
        $("#linktoJourneySpot").click(function() {
            var areaId = $("#hidAreaId").val();
            if (areaId != "") {
                var Id = "linktoJourneySpot";
                var LngType = $("#hidLngType").val();
                var _data = { "sl": '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', AreaID: areaId, LngType: LngType, Id: Id };
                Boxy.iframeDialog({ iframeUrl: "/CommonPage/JourneySpot.aspx", title: "行程亮点", modal: true, width: "700px", height: "400px", data: _data });
            }
            else {
                tableToolbar._showMsg("请选择线路区域");
            }
        });
    });
</script>

