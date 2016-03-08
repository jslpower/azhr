<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LineSelect.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.LineSelect" %>
<span id="span<%=this.SetPriv %>">
    <input id="<%=this.LineNameClient %>" errmsg="请选择线路名称!" valid="required" class="inputtext formsize120"
        data-old="<%=this.LineName %>" name="<%=this.LineNameClient %>" type="text" value="<%=this.LineName %>" />
    <input type="hidden" id="<%=this.LineIDClient %>" name="<%=this.LineIDClient %>"
        value='<%=this.LineID %>' />
    <%if (this.HideSelect == false)
      { %>
    <a href="javascript:void(0)" class="xuanyong" data-width="730" data-height="300"
        id="<%=this.SetPriv %>_a_btn">&nbsp;</a>
    <%} %>
</span>

<script type="text/javascript">
    $(function() {
        newToobar.init({
            box: "#span<%=this.SetPriv %>",
            className: "xuanyong",
            iframeUrl: "/CommonPage/LineSelect.aspx",
            title: "<%=this.SetTitle %>",
            callBackFun: '<%=CallBackFun %>',
            para: { pIframeId: "<%=this.ParentIframeID %>", sModel: "<%=this.SModel %>", sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' }
        });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.LineNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
        }
        //文本框的值被改变了就清空隐藏域的值
        $("#<%=this.LineNameClient %>").change(function() {

            $(this).parent().find("input[type='hidden']").val("");
        })
    })
</script>

