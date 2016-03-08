<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectCity.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.SelectCity" %>
<input type="text" id="<%=ClientText %>"  class="inputtext formsize140" name="<%=ClientText %>" value="<%=this.Name %>"
    <%if(IsMust){ %><%=NoticeHTML %><%} %> />
<input type="hidden" id="<%=ClientValue %>" name="<%=ClientValue %>" value="<%=this.CityID %>" />
<a id="<%=btnID %>" class="xuanyong" href="javascript:void(0);"></a>

<script type="text/javascript" language="javascript">
    $(function() {
        $("#<%=btnID %>").live("click", function() {
            var url = "/CommonPage/selectCity.aspx?";
            var hideObj = $("#<%=ClientValue %>");
            var showObj = $("#<%=ClientText %>").attr("id");
            url += $.param({ callBack: "<%=CallBack %>", ShowID: showObj, pIframeID: "<%=IframeID %>" })
            parent.Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择城市",
                modal: true,
                width: "450",
                height: "260"
            });
        })

        $("#<%=this.ClientText %>").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID, {
            width: 130,
            selectFirst: '<%=SelectFrist.ToString().ToLower() %>',
            blur: '<%=SelectFrist.ToString().ToLower() %>'
        }).result(function(e, data) {
            $("#<%=this.ClientValue %>").val(data[1]);
            $("#<%=this.ClientText %>").attr("data-old", data[0]);
        });

        $("#<%=this.ClientText %>").keyup(function() {
            var v = $.trim($(this).val());
            if (v == "") {
                $(this).next("input[type='hidden']").val("");
            }
            if (v != $.trim($(this).attr("data-old"))) {
                $(this).next("input[type='hidden']").val("");
            }
        });

    })
</script>

