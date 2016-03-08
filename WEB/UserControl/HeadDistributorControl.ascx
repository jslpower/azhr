<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeadDistributorControl.ascx.cs"
    Inherits="Web.UserControl.HeadDistributorControl" %>
<%@ Register Src="~/UserControl/DistributorNotice.ascx" TagName="Notice" TagPrefix="uc2" %>
<div class="top">
    <div class="menubg">
        <div style="width: 371px; height: 64px;">
            <asp:Literal runat="server" ID="ltrLogo"></asp:Literal>
        </div>
        <!-- InstanceBeginEditable name="导航" -->
        <ul class="menu fixed">
            <li><a href="/logout.aspx" class="indexicon"><span><s></s>首页</span></a></li>
            <li><a href='XunJia.aspx?sl=<%=Request.QueryString["sl"] %>' class="<%=ProcductClass %>">
                <span><s></s>询价中心</span></a></li>
            <li><a href='XianLu.aspx?sl=<%=Request.QueryString["sl"] %>' class="<%=OrderClass %>">
                <span><s></s>特推线路</span></a></li>
            <li><a href='JieSuan.aspx?sl=<%=Request.QueryString["sl"] %>' class="<%=FinanceClass %>">
                <span><s></s>结算中心</span></a></li>
            <li><a href='YongHu.aspx?sl=<%=Request.QueryString["sl"] %>' class="<%=SystemClass %>">
                <span><s></s>系统设置</span></a></li>
        </ul>
        <!-- InstanceEndEditable -->
    </div>
</div>
<!-- 公告开始 -->
<uc2:Notice ID="Notice1" runat="server" />
<!-- 公告结束 -->

<%--<script type="text/javascript">
    $(function() {
        GoAjax();
        setInterval(function() {
            GoAjax();
        }, 30000);
        $(".closebtn").click(function() {
            $(".right_botbox").hide();
        });
        function GoAjax() {
            $.newAjax({
                url: "/GroupEnd/Distribution/MyOrder.aspx?getDate=getDate",
                type: "post",
                cache: false,
                dataType: "json",
                success: function(back) {
                    if (back.msg != "") {
                        $("#ul_MSG").html(back.msg);
                        $(".right_botbox").show();
                    }
                    else {
                        $(".right_botbox").hide();
                    }
                }
            });
        }
    });

</script>--%>

