<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetSeat.ascx.cs" Inherits="EyouSoft.WebFX.SetSeat" %>
<a href="JavaScript:void(0);" id="zwfp_box" data-tourid="<%=this.TourId %>" class="car_fenpeibtn">
</a>
<label id="<%=this.LabMsgClientID %>">
    <%=this.LabMsgText %>
</label>
<input type="hidden" id="<%=this.setSeatHidClientID %>" name="<%=this.setSeatHidClientID %>"
    value='<%=this.setSeatHidValue %>' />
<input type="hidden" id="<%=this.PeoNumClientID %>" name="<%=this.PeoNumClientID %>"
    value="<%=this.PeoNum %>" />
<input type="hidden" id="<%=this.HidSeatedDataClientID %>" name="<%=this.HidSeatedDataClientID %>"
    value="<%=this.HidSeatedData %>" />

<script type="text/javascript">
    var win = top || window;
    $("#zwfp_box").click(function() {
        $("#<%=this.LabMsgClientID %>").html("");
        var peoNum = $("#<%=this.PeoNumClientID %>").val();
        if (peoNum <= 0) {
            tableToolbar._showMsg("请先输入成人数或者儿童数！");
            return false;
        }
        //window["seatData"] = {};
        var url = "/CommonPage/SetSeat.aspx?",
        tourId = $(this).attr("data-tourId");
        url += $.param({
            tourId: tourId,
            peoNum: peoNum,
            oldPeoNum: '<%=this.OldPeoNum %>',
            orderId: '<%=this.OrderId %>',
            callBackFun: '<%=this.CallBackFun %>',
            seatedData: $("#<%=HidSeatedDataClientID %>").val()
        });
        win.Boxy.iframeDialog({
            iframeUrl: url,
            title: "<%=SetTitle %>",
            modal: true,
            width: "930px",
            height: "460px"
        })
    })
</script>

