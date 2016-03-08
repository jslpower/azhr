<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeadOrderControl.ascx.cs"
    Inherits="Web.UserControl.HeadOrderControl" %>
<%--<a class="back-info" href="javascript:window.history.go(-1)">返回</a>--%> <span class="scrollDiv">
    <ul name="tx_menu">
        <%=GetRemindCountByType()%>
    </ul>
</span>

<script type="text/javascript">
    //判断是否显示SPAN

    $(document).ready(function() {
    var li_length = $("ul[name=tx_menu]").find("li");
        if (li_length.length == 0) {
            $("span[class=scrollDiv]").hide();
        }
    })
    //滚动公告
    function AutoScroll(obj) {
        $(obj).find("ul:first").animate({
            marginTop: "-25px"
        }, 500, function() {
            $(this).css({ marginTop: "0px" }).find("li:first").appendTo(this);
        });
    }
    var s = true;
    var t = 0;
    function startli() {
        if (s) t = setInterval('AutoScroll(".scrollDiv")', 2000);
    }
    //suggest
    $(document).ready(function() {
        //滚动公告
        startli();
        $(".scrollDiv").hover(function() {
            clearInterval(t);
            var s = false;
        }, function() {
            s = true;
            startli();
        });
    })
</script>

