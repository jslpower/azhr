<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="selectWuPin.ascx.cs" Inherits="EyouSoft.Web.UserControl.selectWuPin" %>

<input type="text" id="<%=ClientText %>" readonly="readonly" style="background-color:#dadada" class="inputtext formsize140" name="<%=ClientText %>" value="<%=this.Name %>"
    <%if(IsMust){ %><%=NoticeHTML %><%} %> />
<input type="hidden" id="<%=ClientValue %>" name="<%=ClientValue %>" value="<%=this.HideID %>" />
<a id="<%=btnID %>" class="xuanyong Offers" href="javascript:void(0);"></a>

<script type="text/javascript" language="javascript">
    $(function() {
        $("#<%=btnID %>").live("click", function() {
            var url = "/CommonPage/selectWuPin.aspx?";
            var hideObj =$("#<%=ClientValue %>");
            var showObj = $("#<%=ClientText %>").attr("id");
            url += $.param({ callBack: "<%=CallBack %>", ShowID: showObj, pIframeID: "<%=IframeID %>", hideID: $(hideObj).val() })
            parent.Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择物品",
                modal: true,
                width: "948",
                height: "406"
            });
        })

    })
    window["<%=ClientID %>"] = {
        _callBack: function(_data) {
            if (_data) {
                $("#<%=ClientText %>").val(_data.wupin.name);
                $("#<%=ClientValue %>").val(_data.wupin.id);
            }
        }
    };
</script>
