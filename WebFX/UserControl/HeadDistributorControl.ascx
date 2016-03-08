<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeadDistributorControl.ascx.cs" Inherits="EyouSoft.WebFX.UserControl.HeadDistributorControl" %>
<%@ Register Src="~/UserControl/DistributorNotice.ascx" TagName="Notice" TagPrefix="uc2" %>
<div class="top">
    <div class="menubg">
        <div class="fx_logo"><asp:Literal runat="server" ID="ltrLogo"></asp:Literal></div>
        <ul class="menu fixed">
           <%-- <li><a href="/logout.aspx" class="indexicon"><span><s></s><%=(String)GetGlobalResourceObject("string", "首页")%></span></a></li>--%>
            <li><a href='AcceptPlan.aspx?sl=0&LgType=<%=Request.QueryString["LgType"] %>' class="<%=ProcductClass %>">
                <span><s></s><%=(String)GetGlobalResourceObject("string", "收客计划")%></span></a></li>
            <li><a href='MyOrder.aspx?sl=0&LgType=<%=Request.QueryString["LgType"] %>' class="<%=OrderClass %>">
                <span><s></s><%=(String)GetGlobalResourceObject("string", "我的订单")%></span></a></li>
            <li style="visibility: <%=this.IsPubLogin?"hidden":"visibility"%>"><a href='FinancialControl.aspx?sl=0&LgType=<%=Request.QueryString["LgType"] %>' class="<%=FinanceClass %>">
                <span><s></s><%=(String)GetGlobalResourceObject("string", "财务管理")%></span></a></li>
            <li style="visibility: <%=this.IsPubLogin?"hidden":"visibility"%>"><a href='YongHu.aspx?sl=0&LgType=<%=Request.QueryString["LgType"] %>' class="<%=SystemClass %>">
                <span><s></s><%=(String)GetGlobalResourceObject("string", "系统设置")%></span></a></li>
            <li><a href="/logout.aspx" class="indexicon"><span><s></s>退出系统</span></a></li>
        </ul>
    </div>
</div>
<!-- 公告开始 -->
<uc2:Notice ID="Notice1" runat="server" />
<!-- 公告结束 -->


