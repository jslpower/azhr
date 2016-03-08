<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BaseBar.ascx.cs" Inherits="EyouSoft.Web.UserControl.BaseBar" %>
<div style="background: none #f6f6f6;" class="tablehead">
    <ul class="fixed">
        <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" memuid="1"
            hidefocus="true" href="/Sys/YuYanGuanLi.aspx?sl=51"><span>语言管理</span></a></li>
        <% if (PrivsPage[1] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="2" hidefocus="true"
            href="/Sys/GuoJiaGuanLi.aspx?sl=51&memuid=2"><span>国家管理</span></a></li>
        <%}
           if (PrivsPage[2] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="3" hidefocus="true"
            href="/Sys/ChengShiGuanLi.aspx?sl=51&memuid=3"><span>城市管理</span></a></li>
        <%} if (PrivsPage[3] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="4" hidefocus="true"
            href="/Sys/XianLuQuYu.aspx?sl=51&memuid=4"><span>线路区域</span></a></li>
        <%} if (PrivsPage[4] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="5" hidefocus="true"
            href="/Sys/FangXingGuanLi.aspx?sl=51&memuid=5"><span>房型管理</span></a></li>
        <%} if (PrivsPage[5] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="6" hidefocus="true"
            href="/Sys/KeHuDengJi.aspx?sl=51&memuid=6"><span>客户等级</span></a></li>
        <%} if (PrivsPage[6] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="7" hidefocus="true"
            href="/Sys/ZhiFuFangShi.aspx?sl=51&memuid=7"><span>支付方式</span></a></li>
        <%} if (PrivsPage[7] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="8" hidefocus="true"
            href="/Sys/XingChengLiangDian.aspx?sl=51&memuid=8"><span>行程亮点</span></a></li>
        <%} if (PrivsPage[8] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="9" hidefocus="true"
            href="/Sys/BaoJiaBeiZhu.aspx?sl=51&memuid=9"><span>报价备注</span></a></li>
        <%} if (PrivsPage[9] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="10" hidefocus="true"
            href="/Sys/XingChengBeiZhu.aspx?sl=51&memuid=10"><span>行程备注</span></a></li>
        <%} if (PrivsPage[10] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="11" hidefocus="true"
            href="/Sys/DaoYouXuZhi.aspx?sl=51&memuid=11"><span>导游须知</span></a></li>
        <%} if (PrivsPage[11] == "1")
           {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="12" hidefocus="true"
            href="/Sys/MemberTypeList.aspx?sl=51&memuid=12"><span>会员类型</span></a></li>
        <% } %>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="13" hidefocus="true"
            href="/Sys/BaoJiaBiaoZhun.aspx?sl=52&memuid=13"><span>报价标准</span></a></li>
    </ul>
</div>

<script type="text/javascript">
    //当前菜单序号
    var memuid = <%=Request.QueryString["memuid"]==null?"1":Request.QueryString["memuid"]%>;
    $(".tablehead").children(".fixed").children().each(function() {
        var Aobject = $(this).find("a");
        $(Aobject).attr("class", "ztorderform");
        if ($(Aobject).attr("memuid") == memuid) {
            $(Aobject).attr("class", "ztorderform de-ztorderform");
        }
    });
</script>

