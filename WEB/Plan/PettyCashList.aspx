<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="PettyCashList.aspx.cs" Inherits="EyouSoft.Web.Plan.PettyCashList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="PlanConfigMenu" TagPrefix="uc1" Src="/UserControl/PlanConfigMenu.ascx" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="hr_10">
        </div>
        <div class="jd-mainbox fixed">
            <uc1:PlanConfigMenu id="PlanConfigMenu1" runat="server" />
            <div class="jdcz-main">
                <div class="addContent-box">
                    <span class="formtableT">团队信息</span>
                    <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                        <tr>
                            <td width="15%" class="addtableT">
                                团号：
                            </td>
                            <td width="40%" class="kuang2">
                                <asp:Literal ID="litTourCode" runat="server"></asp:Literal>
                            </td>
                            <td width="15%" class="addtableT">
                                线路区域：
                            </td>
                            <td width="30%" class="kuang2">
                                <asp:Literal ID="litAreaName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                线路名称：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litRouteName" runat="server"></asp:Literal>
                            </td>
                            <td class="addtableT">
                                天数：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litDays" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                人数：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litPeoples" runat="server"></asp:Literal>
                            </td>
                            <td class="addtableT">
                                用房数：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litHouses" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                团队国籍/地区：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litCountry" runat="server"></asp:Literal>
                            </td>
                            <td class="addtableT">
                                抵达时间：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litDDDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                抵达城市：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litDDCity" runat="server"></asp:Literal>
                            </td>
                            <td class="addtableT">
                                航班/时间：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litDDHBDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                离境时间：
                            </td>
                            <td colspan="3" class="kuang2">
                                <asp:Literal ID="litLJDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                离开城市：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litLKCity" runat="server"></asp:Literal>
                            </td>
                            <td class="addtableT">
                                航班/时间：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litLKHBDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                业务员：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litSellers" runat="server"></asp:Literal>
                            </td>
                            <td class="addtableT">
                                OP：
                            </td>
                            <td class="kuang2">
                                <asp:Literal ID="litOperaters" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tablelist-box " style="width: 98.5%">
                    <span class="formtableT">导游支出</span>
                    <asp:PlaceHolder ID="tabAyencyView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repayencyList.Items.Count+1 %>" class="th-line w30">
                                    <b>地<br />
                                        接</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>地接社名称</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>人数</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>费用明细</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repayencyList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanHotelRoomList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabGuidView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repGuidList.Items.Count+1 %>" class="th-line w30">
                                    <b>导<br />
                                        游</b>
                                </th>
                                <td width="10%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>导游姓名</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>导游电话</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>上团时间</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>下团时间</strong>
                                </td>
                                <td align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>上团地点</strong>
                                </td>
                                <td align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>下团地点</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repGuidList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("ContactPhone")%>
                                        </td>
                                        <td align="center">
                                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartDate"), ProviderToDate)%>
                                        </td>
                                        <td align="center">
                                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("EndDate"), ProviderToDate)%>
                                        </td>
                                        <td align="center">
                                            <span title="<%# Eval("PlanGuide.OnLocation") %>">
                                                <%# EyouSoft.Common.Utils.GetText2(Eval("PlanGuide.OnLocation").ToString(), 10, true)%></span>
                                        </td>
                                        <td align="center">
                                            <span title="<%# Eval("PlanGuide.NextLocation") %>">
                                                <%# EyouSoft.Common.Utils.GetText2(Eval("PlanGuide.NextLocation").ToString(), 10, true)%></span>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabHotelView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.rephotellist.Items.Count+1 %>" class="th-line w30">
                                    <b>酒<br />
                                        店</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>酒店名称</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>天数</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>免费房数</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>费用明细</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="rephotellist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="jd-table01">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("FreeNumber")%>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanHotelRoomList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabCarsView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repcarslist.Items.Count+1 %>" class="th-line w30">
                                    <b>车<br />
                                        队</b>
                                </th>
                                <td width="20%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>车队名称</strong>
                                </td>
                                <td align="center" bgcolor="#D4ECF7" class="th-line">
                                    费用明细
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repcarslist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="jd-table01">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanCarList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabAirView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repairlist.Items.Count+1 %>" class="th-line w30">
                                    <b>飞<br />
                                        机</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>出票点</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>出票数</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>费用明细</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repairlist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanLargeFrequencyList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabtrainView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.reptrainlist.Items.Count+1 %>" class="th-line w30">
                                    <b>火<br />
                                        车</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>出票点</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>付费数量</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>免费数量</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>费用明细</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="reptrainlist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("PepolePayNum")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("FreeNumber")%>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanLargeFrequencyList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabbusView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repbuslist.Items.Count+1 %>" class="th-line w30">
                                    <b>汽<br />
                                        车</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>出票点</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>张数</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>费用明细</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repbuslist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanLargeFrequencyList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabchinashipView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repchinashiplist.Items.Count+1 %>" class="th-line w30">
                                    <b>轮<br />
                                        船</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>游船公司</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>出票数</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>费用明细</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repchinashiplist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanLargeFrequencyList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabAttrView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repattrlist.Items.Count+1 %>" class="th-line w30">
                                    <b>景<br />
                                        点</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>景点名称</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>人数</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>费用明细</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repattrlist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="jd-table01">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <b class="fontblue">
                                                <%# Eval("AdultNumber")%></b><sup class="fontred">+<%# Eval("ChildNumber")%></sup>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanAttractionsList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabDinView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repDinlist.Items.Count+1 %>" class="th-line w30">
                                    <b>用<br />
                                        餐</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>餐馆名称</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>用餐时间</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>人数</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>费用明细</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repDinlist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartDate"), ProviderToDate)%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <%# GetAPMX(Eval("PlanDiningList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐.ToString())%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabshopView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repshoplist.Items.Count+1 %>" class="th-line w30">
                                    <b>购<br />
                                        物</b>
                                </th>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>购物店名称</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>进店日期</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repshoplist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# UtilsCommons.GetDateString(Eval("StartDate"), "yyyy-MM-dd")%>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabpickView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.reppicklist.Items.Count+1 %>" class="th-line w30">
                                    <b>领<br />
                                        料</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>领料内容</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>领料人</strong>
                                </td>
                                <td align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>数量</strong>
                                </td>
                                <td align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>单价</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="reppicklist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("ContactName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="center">
                                            <%# Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(Eval("ContactFax").ToString())) %>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="tabotherView" runat="server" Visible="false">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th rowspan="<%=this.repotherlist.Items.Count+1 %>" class="th-line w30">
                                    <b>其<br />
                                        它</b>
                                </th>
                                <td width="20%" align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>供应商名称</strong>
                                </td>
                                <td width="10%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>人数</strong>
                                </td>
                                <td align="left" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支出项目</strong>
                                </td>
                                <td width="8%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>状态</strong>
                                </td>
                                <td width="9%" align="center" bgcolor="#D4ECF7" class="th-line">
                                    <strong>支付方式</strong>
                                </td>
                                <td width="10%" align="right" bgcolor="#D4ECF7" class="th-line">
                                    <strong>结算费用</span></strong>
                                </td>
                            </tr>
                            <asp:Repeater ID="repotherlist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center" class="jd-table01">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("ServiceStandard") %>">
                                                <%# EyouSoft.Common.Utils.GetText2(Eval("ServiceStandard").ToString(), 30, true)%></span>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="hr_5">
                        </div>
                    </asp:PlaceHolder>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="right">
                                <strong>共需要签单数：</strong>
                            </td>
                            <td width="10%" align="left">
                                <strong>
                                    <asp:Literal ID="litSignNums" runat="server"></asp:Literal></strong>
                            </td>
                            <td width="17%" align="right">
                                <strong>合计金额：</strong>
                            </td>
                            <td width="10%" align="right">
                                <b class="fontred">
                                    <asp:Literal ID="littotalPrices" runat="server"></asp:Literal></b>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="hr_5">
                </div>
                <asp:PlaceHolder ID="tabGuidPaymentView" runat="server">
                    <div class="tablelist-box " style="width: 98.5%">
                        <span class="formtableT">导游收款</span>
                        <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                            <tr>
                                <th width="35%">
                                    订单号
                                </th>
                                <th width="35%" align="left">
                                    客户单位
                                </th>
                                <th width="15%">
                                    人数
                                </th>
                                <th width="15%" align="right">
                                    现收金额
                                </th>
                            </tr>
                            <asp:Repeater ID="repGuidPayment" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <%# Eval("OrderCode")%>
                                        </td>
                                        <td align="left">
                                            <%# Eval("BuyCompanyName")%>
                                        </td>
                                        <td align="center">
                                            <%#Convert.ToInt32(Eval("Adults")) + Convert.ToInt32(Eval("Childs")) + Convert.ToInt32(Eval("Others"))%>
                                        </td>
                                        <td align="right">
                                            <b class="fontred">
                                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("GuideIncome"),ProviderToMoney)%></b>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:PlaceHolder ID="tabPlanItemView" runat="server">
                                <tr>
                                    <td colspan="3" align="right">
                                        <strong>合计金额：</strong>
                                    </td>
                                    <td align="right">
                                        <b class="fontred">
                                            <asp:Literal ID="litsumPrices" runat="server"></asp:Literal></b>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                        </table>
                        <div class="hr_5">
                        </div>
                    </div>
                </asp:PlaceHolder>
                <div class="tablelist-box " style="width: 98.5%">
                    <span class="formtableT">导游借款</span>
                    <table width="100%" id="tabPreAppView" cellpadding="0" cellspacing="0" class="add-baojia">
                        <tr>
                            <th width="15%">
                                借款人
                            </th>
                            <th width="8%">
                                借款时间
                            </th>
                            <th width="8%">
                                预借金额
                            </th>
                            <th width="8%">
                                实借金额
                            </th>
                            <th width="10%">
                                预领签单数
                            </th>
                            <th width="10%">
                                实领签单数
                            </th>
                            <th width="28%">
                                用途说明
                            </th>
                            <th width="13%" data-class="actionView">
                                操作
                            </th>
                        </tr>
                        <asp:Repeater ID="repGuidPayList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <uc1:sellsSelect ID="SelectGuid1" CompanyID="<%=this.SiteUserInfo.CompanyId %>" readonly="true"
                                            runat="server" SellsName='<%# Eval("Borrower") %>' SellsID='<%# Eval("BorrowerId") %>'
                                            SetTitle="导游" />
                                    </td>
                                    <td align="center">
                                        <input name="txtBorrowTime" onfocus="WdatePicker({minDate: '%y-%M-#{%d' });" type="text"
                                            value="<%# Eval("BorrowTime") %>" style="width: 60px;" class="inputtext" />
                                    </td>
                                    <td align="center">
                                        <input name="txtBorrowAmount" type="text" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("BorrowAmount").ToString())) %>"
                                            style="width: 60px;" class="inputtext" />
                                    </td>
                                    <td align="center">
                                        <input name="txtRealAmount" type="text" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("RealAmount").ToString())) %>"
                                            style="width: 60px; background-color: #dadada;" class="inputtext" readonly="readonly" />
                                    </td>
                                    <td align="center">
                                        <input name="txtPreSignNum" type="text" class="inputtext formsize50" value="<%# Eval("PreSignNum") %>" />
                                    </td>
                                    <td align="center">
                                        <input name="txtRelSignNum" style="background-color: #dadada;" readonly="readonly"
                                            type="text" class="inputtext formsize50" value="<%# Eval("RelSignNum") %>" />
                                    </td>
                                    <td align="center">
                                        <textarea name="txtUseFor" id="txtUseFor" style="height: 28px;" class="inputtext formsize250"><%# Eval("UseFor")%></textarea>
                                    </td>
                                    <td align="center" data-class="actionView">
                                        <%# GetFinStatus((EyouSoft.Model.EnumType.FinStructure.FinStatus)(int)Eval("Status"),this.tourStatus,Eval("id").ToString())  %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td align="center">
                                <uc1:sellsSelect ID="SelectGuid1" CompanyID="<%=this.SiteUserInfo.CompanyId %>" readonly="true"
                                    runat="server" SetTitle="导游" />
                            </td>
                            <td align="center">
                                <input name="txtBorrowTime" onfocus="WdatePicker({minDate: '%y-%M-#{%d' });" type="text"
                                    style="width: 60px;" class="inputtext" />
                            </td>
                            <td align="center">
                                <input name="txtBorrowAmount" type="text" style="width: 60px;" class="inputtext" />
                            </td>
                            <td align="center">
                                <input name="txtRealAmount" type="text" readonly="readonly" value="0" style="width: 60px;
                                    background-color: #dadada;" class="inputtext" readonly="true" />
                            </td>
                            <td align="center">
                                <input name="txtPreSignNum" type="text" class="inputtext formsize50" />
                            </td>
                            <td align="center">
                                <input name="txtRelSignNum" type="text" readonly="readonly" class="inputtext formsize50"
                                    value="0" style="background-color: #dadada;" readonly="true" class="inputtext" />
                            </td>
                            <td align="center">
                                <textarea name="txtUseFor" id="txtUseFor" style="height: 28px;" class="inputtext formsize250"></textarea>
                            </td>
                            <td align="center" data-class="actionView">
                                <%= GetFinStatus(null,this.tourStatus,"")  %>
                            </td>
                        </tr>
                    </table>
                    <div class="hr_5">
                    </div>
                </div>
                <div class="hr_10">
                </div>
            </div>
        </div>
        <div class="hr_10">
        </div>
    </div>

    <script type="text/javascript">
        var PrePage = {
            type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("Type") %>',
            sl: '<%=SL %>',
            tourID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("TourId") %>',
            _DeletePre: function(objId) {
                $.newAjax({
                    type: "get",
                    url: '/Plan/PettyCashList.aspx?sl=' + PrePage.sl + '&action=delete&ID=' + objId,
                    cache: false,
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            tableToolbar._showMsg(data.msg, function() {
                                window.location.href = '/Plan/PettyCashList.aspx?type=' + PrePage.type + '&sl=' + PrePage.sl + '&TourId=' + PrePage.tourID;
                            });
                            return false;
                        }
                        else {
                            tableToolbar._showMsg(data.msg);
                            PrePage._BindBtn();
                            return false;
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        PrePage._BindBtn();
                        return false;
                    }
                });
            },
            _SavePre: function(datas, objId) {
                $.newAjax({
                    type: "get",
                    url: '/Plan/PettyCashList.aspx?sl=' + PrePage.sl + '&TourId=' + PrePage.tourID + '&action=update&ID=' + objId,
                    cache: false,
                    dataType: "json",
                    data: datas,
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = '/Plan/PettyCashList.aspx?type=' + PrePage.type + '&sl=' + PrePage.sl + '&TourId=' + PrePage.tourID;
                            });
                            return false;
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                            PrePage._BindBtn();
                            return false;
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        PrePage._BindBtn();
                        return false;
                    }
                });
            },
            _BindBtn: function() {
                //删除
                $("#tabPreAppView").find("[data-class='deletePreApp']").unbind("click");
                $("#tabPreAppView").find("[data-class='deletePreApp']").click(function() {
                    $(this).unbind("click");
                    var preID = $(this).find("img").attr("data-Id");
                    if (preID) {
                        PrePage._DeletePre(preID);
                    }
                    return false;
                });

                //修改操作
                $("#tabPreAppView").find("[data-class='savePreApp']").unbind("click");
                $("#tabPreAppView").find("[data-class='savePreApp']").click(function() {

                    var _s = $(this).closest("tr");
                    var data = { guid: "", guidName: "", txtBorrowTime: "", txtBorrowAmount: "", txtPreSignNum: "", txtUseFor: "", txtTourNo: "" };
                    data.guid = _s.find("td:eq(0)").find("input[type='hidden']").val();
                    data.guidName = $.trim(_s.find("td:eq(0)").find("input[type='text']").val());
                    data.txtBorrowTime = $.trim(_s.find("input[name='txtBorrowTime']").val());
                    data.txtBorrowAmount = $.trim(_s.find("input[name='txtBorrowAmount']").val());
                    data.txtPreSignNum = $.trim(_s.find("input[name='txtPreSignNum']").val());
                    data.txtUseFor = $.trim(_s.find("textarea[name='txtUseFor']").val());
                    data.txtTourNo = $(".firsttable").find("td:eq(1)").html();
                    if (data.guid == "" || data.guidName == "" || data.guid == null || data.guidName == null) {
                        tableToolbar._showMsg("请选择导游!");
                        _s.find("td:eq(0)").find("span input[type='text']").focus();
                        return false;
                    }

                    if (data.txtBorrowTime == "") {
                        tableToolbar._showMsg("请选择借款日期!");
                        return false;
                    }

                    if (data.txtBorrowAmount == "") {
                        tableToolbar._showMsg("请填写借款金额!");
                        _s.find("input[name='txtBorrowAmount']").focus();
                        return false;
                    }

                    if (data.txtPreSignNum == "") {
                        tableToolbar._showMsg("请填写预领签单数!");
                        _s.find("input[name='txtPreSignNum']").focus();
                        return false;
                    }

                    var ID = $(this).find("img").attr("data-id");
                    $(this).unbind("click");
                    PrePage._SavePre(data, ID);
                });
            },
            _PageInit: function() {
                PrePage._BindBtn();
                var result = '<%=ret %>';
                if (result.toUpperCase() == "TRUE") {
                    $("#tabPreAppView").find("[data-class='actionView']").hide();
                }
            }
        }
        $(document).ready(function() {
            PrePage._PageInit();
        });
    </script>

</asp:Content>
