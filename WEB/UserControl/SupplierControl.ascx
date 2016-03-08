<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierControl.ascx.cs" Inherits="EyouSoft.Web.UserControl.SupplierControl" %>
<input type="text" id="<%=ClientText %>" readonly="readonly" style="background-color:#dadada" class="inputtext formsize140" name="<%=ClientText %>" value="<%=this.Name %>"
    <%if(IsMust){ %><%=NoticeHTML %><%} %> />
<input type="hidden" id="<%=ClientValue %>" name="<%=ClientValue %>" value="<%=this.HideID %>" />
<input type="hidden" id="<%=ClientType %>" name="<%=ClientType %>" value="<%=this.SupplierType %>" />
<a id="<%=btnID %>" class="xuanyong Offers" href="javascript:void(0);"></a>

<script type="text/javascript" language="javascript">
    $(function() {
        $("#<%=btnID %>").live("click", function() {
            var url = "/CommonPage/UserSupplier.aspx?";
            var hideObj =$("#<%=ClientValue %>");
            var showObj = $("#<%=ClientText %>").attr("id");
            var type = "<%=SupplierType %>";
            url += $.param({ suppliertype: type, callBack: "<%=CallBack %>", ShowID: showObj, pIframeID: "<%=IframeID %>", hideID: $(hideObj).val() })
            parent.Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择供应商",
                modal: true,
                width: "948",
                height: "406"
            });
        })

    })
    window["<%=ClientID %>"] = {
        _callBack: function(_data) {
            if (_data) {
                $("#<%=ClientText %>").val(_data.name);
                $("#<%=ClientValue %>").val(_data.id);
            }
        }
    };
</script>
