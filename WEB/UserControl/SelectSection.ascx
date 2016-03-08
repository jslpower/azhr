<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectSection.ascx.cs"
    Inherits="Web.UserControl.SelectSection" %>
<span id="span<%=this.SetPriv %>">
    <input id="<%=this.SelectNameClient %>" errmsg="请选择<%=this.SetTitle %>!" valid="required"
        class="inputtext formsize120" name="<%=this.SelectNameClient %>" type="text"
        value="<%=this.SectionName %>" />
    <input type="hidden" id="<%=this.SelectIDClient %>" name="<%=this.SelectIDClient %>"
        value='<%=this.SectionID %>' />
    <a href="javascript:void(0)" class="xuanyong" data-width="530" data-height="380"
        id="<%=this.SetPriv %>_a_btn">&nbsp;</a> </span>

<script type="text/javascript">
    $(function() {
        newToobar.init({
            box: "#span<%=this.SetPriv %>",
            className: "xuanyong",
            iframeUrl: "/CommonPage/SelectSection.aspx",
            title: "<%=this.SetTitle %>",
            callBackFun: '<%=CallBackFun %>',
            para: { pIframeId: "<%=this.ParentIframeID %>", sModel: "<%=this.SModel %>", setTitel: "<%=this.SetTitle %>" }
        });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.SelectNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
        }
        if ('<%=this.IsNotValid.ToString().ToLower() %>' == "false") {
            $("#<%=this.SelectNameClient %>").removeAttr("valid");
        }
        //文本框的值被改变了就清空隐藏域的值
        $("#<%=this.SelectNameClient %>").change(function() {
            $(this).parent().find("input[type='hidden']").val("");
        })
    })
</script>

