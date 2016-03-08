<%@ Page Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="DaoYouPrint.aspx.cs" Inherits="EyouSoft.Web.PrintPage.DaoYouPrint"
    Title="导游任务单" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="center">
                <b class="font24">导游任务单</b>
                <select>
                    <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanBGType))) %>
                </select>
            </td>
        </tr>
        <tr>
            <td align="right">
                <b class="font16">
                    线路名称：<asp:Label ID="lbRouteName" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox_t" data-check="check" />
                    团队信息</b>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="696" align="center" border="0" class="list_2">
        <tr>
            <th width="80" align="right">
                团号：
            </th>
            <td align="left" width="268">
                <asp:Label runat="server" ID="lbTourCode"></asp:Label>
            </td>
            <th width="80" align="right">
                天数：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbdayCount"></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                抵达时间：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbstarttime"></asp:Label>
            </td>
            <th align="right">
                抵达地点：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbstartstand"></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                离开时间：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbendtime"></asp:Label>
            </td>
            <th align="right">
                离开地点：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbendstand"></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                人数：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbpeoplecount"></asp:Label>
            </td>
            <th align="right">
                带团导游：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbguidename"></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                本团销售：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbsellerinfo"></asp:Label>
            </td>
            <th align="right">
                本团计调：
            </th>
            <td align="left">
                <asp:Label runat="server" ID="lbplander"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_rpt_OrderinfoList">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox_t" data-check="check" />
                        订单信息</b>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
            <asp:Repeater runat="server" ID="rpt_OrderinfoList">
                <HeaderTemplate>
                    <tr>
                        <th width="130" align="center">
                            订单号
                        </th>
                        <th width="130" align="center">
                            客户单位
                        </th>
                        <th align="center">
                            联系人
                        </th>
                        <th style="text-align: center">
                            联系电话
                        </th>
                        <th width="100" align="center">
                            人数
                        </th>
                        <th width="80" align="center">
                            导游现收
                        </th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%#Eval("OrderCode")%>
                        </td>
                        <td align="center">
                            <%#Eval("BuyCompanyName")%>
                        </td>
                        <td align="center">
                            <%#Eval("ContactName")%>
                        </td>
                        <td style="text-align: center;">
                            <%# Eval("ContactTel")%>
                        </td>
                        <td align="center">
                            <%#Eval("Adults")%>+<%#Eval("Childs")%>+<%#Eval("Others")%>
                        </td>
                        <td align="center">
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("GuideIncome"),ProviderToMoney)%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:PlaceHolder>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TReceiveJourney"
        runat="server" class="borderline_2">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-check="check" />
                    导游安排接待行程</b>
            </td>
        </tr>
        <tr>
            <td class="td_text">
                <asp:Label runat="server" ID="lbReceiveJourney"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TService"
        runat="server" class="borderline_2">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" data-check="check" />
                    导游安排服务标准</b>
            </td>
        </tr>
        <tr>
            <td class="td_text">
                <asp:Label runat="server" ID="lbServiceStandard"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TGuideNote"
        runat="server" class="borderline_2">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" id="checkbox3" data-check="check" />
                    导游须知</b>
            </td>
        </tr>
        <tr>
            <td class="td_text">
                <asp:Label runat="server" ID="lbGuidNote"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="cktourDetail" id="cktourDetail" data-check="check" />
                    团队支付详单</b>
            </td>
        </tr>
    </table>
    <div>
        <asp:PlaceHolder runat="server" ID="ph_dijie">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <tr>
                    <th width="5%" rowspan="<%=dijie+1 %>" align="center">
                        地接
                    </th>
                    <th width="20%" align="center">
                        地接社名称
                    </th>
                    <th width="30%" align="center">
                        地址
                    </th>
                    <th width="10%" align="center">
                        联系人
                    </th>
                    <th width="35%" align="center">
                        电话/传真
                    </th>
                    <%-- <th width="12%" align="center">
                        人数
                    </th>
                    <th width="37%" align="left">
                        费用明细
                    </th>
                    <th width="10%" align="center">
                        支付方式
                    </th>
                    <th width="12%" align="center">
                        结算费用
                    </th>--%>
                </tr>
                <asp:Repeater runat="server" ID="rpt_dijie">
                    <ItemTemplate>
                        <tr>
                            <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接 )%>
                            <%--<td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetAPMX(Eval("PlanHotelRoomList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接.ToString())%>
                            </td>
                            <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_hotel">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <tr>
                    <th width="5%" rowspan="<%=hotel+1 %>" align="center">
                        酒店
                    </th>
                    <th width="20%" align="center">
                        酒店名称
                    </th>
                    <th width="30%" align="center">
                        地址
                    </th>
                    <%--  <th width="12%" align="center">
                        房间数
                    </th>--%>
                    <th width="10%" align="center">
                        联系人
                    </th>
                    <th width="35%" align="center">
                        电话/传真
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt_hotellistk">
                    <ItemTemplate>
                        <tr>
                            <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店 )%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_chedui">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <tr>
                    <th width="5%" rowspan="<%=chedui+1 %>" align="center">
                        车队
                    </th>
                    <th width="20%" align="center">
                        车队名称
                    </th>
                    <%--     <th width="12%" align="center">
                        数量
                    </th>
                    <th width="13%" align="center">
                        用车时间
                    </th>
                    <th width="24%" align="left">
                        费用明细
                    </th>--%>
                    <th width="30%" align="center">
                        车牌/车型
                    </th>
                    <th width="10%" align="center">
                        司机
                    </th>
                    <th width="35%" align="center">
                        电话
                    </th>
                    <%--    <th width="10%" align="center">
                        支付方式
                    </th>
                    <th width="12%" align="center">
                        结算费用
                    </th>--%>
                </tr>
                <asp:Repeater runat="server" ID="rpt_chedui">
                    <ItemTemplate>
                        <tr>
                            <td align="left">
                                <%#Eval("SourceName")%>
                            </td>
                            <%--<td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartDate"),ProviderToDate)%><%# Eval("StartTime") != null && Eval("StartTime").ToString().Trim()!="" ?  "(" + Eval("StartTime") + ")" :""%>
                                <br />
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("EndDate"),ProviderToDate)%><%# Eval("EndTime") != null && Eval("EndTime").ToString().Trim() != "" ? "(" + Eval("EndTime") + ")" : ""%>
                            </td>
                            <td align="left">
                                <%# EyouSoft.Common.UtilsCommons.GetAPMX(Eval("PlanCarList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车.ToString())%>
                            </td>--%>
                            <%# reCarInfo(Eval("PlanCarList"))%>
                            <%--<td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_plane">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_plane">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=plane+1 %>" align="center">
                                飞机
                            </th>
                            <th width="20%" align="center">
                                出票点
                            </th>
                            <th width="30%" align="center">
                                地址
                            </th>
                            <th width="10%" align="center">
                                联系人
                            </th>
                            <th width="35%" align="center">
                                电话/传真
                            </th>
                            <%-- <th width="12%" align="center">
                                出票数
                            </th>
                            <th width="13%" align="center">
                                出发时间
                            </th>
                            <th width="24%" align="left">
                                费用明细
                            </th>
                            <th width="10%" align="center">
                                支付方式
                            </th>
                            <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机 )%>
                            <%--<td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="left">
                                <%#GetDepartureTime(Eval("PlanLargeFrequencyList"))%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetAPMX(Eval("PlanLargeFrequencyList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机.ToString())%>
                            </td>
                            <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_train">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_train">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=train+1%>" align="center">
                                火车
                            </th>
                            <th width="20%" align="center">
                                出票点
                            </th>
                            <th width="30%" align="center">
                                地址
                            </th>
                            <th width="10%" align="center">
                                联系人
                            </th>
                            <th width="35%" align="center">
                                电话/传真
                            </th>
                            <%-- <th width="12%" align="center">
                                张数
                            </th>
                            <th width="13%" align="center">
                                出发时间
                            </th>
                            <th width="24%" align="left">
                                费用明细
                            </th>
                            <th width="10%" align="center">
                                支付方式
                            </th>
                            <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车 )%>
                            <%--   <td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="left">
                                <%#GetDepartureTime(Eval("PlanLargeFrequencyList"))%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetAPMX(Eval("PlanLargeFrequencyList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车.ToString())%>
                            </td>
                            <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_bus">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_bus">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=bus+1 %>" align="center">
                                汽车
                            </th>
                            <th width="20%" align="center">
                                出票点
                            </th>
                            <th width="30%" align="center">
                                地址
                            </th>
                            <th width="10%" align="center">
                                联系人
                            </th>
                            <th width="35%" align="center">
                                电话/传真
                            </th>
                            <%--<th width="12%" align="center">
                                张数
                            </th>
                            <th width="13%" align="center">
                                出发时间
                            </th>
                            <th width="24%" align="left">
                                费用明细
                            </th>
                            <th width="10%" align="center">
                                支付方式
                            </th>
                            <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车 )%>
                            <%--<td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="left">
                                <%#GetDepartureTime(Eval("PlanLargeFrequencyList"))%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetAPMX(Eval("PlanLargeFrequencyList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车.ToString())%>
                            </td>
                            <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_jingdian">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_jingdian">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=jingdian+1 %>" align="center">
                                景点
                            </th>
                            <th width="20%" align="center">
                                景点名称
                            </th>
                            <%--<th width="12%" align="center">
                                人数
                            </th>
                            <th width="37%" align="left">
                                费用明细
                            </th>--%>
                            <th width="30%" align="center">
                                地址
                            </th>
                            <th width="10%" align="center">
                                联系人
                            </th>
                            <th width="35%" align="center">
                                电话/传真
                            </th>
                            <%--      <th width="10%" align="center">
                                支付方式
                            </th>
                            <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%--   <td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetAPMX(Eval("PlanAttractionsList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点.ToString())%>
                            </td>--%>
                            <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点 )%>
                            <%--       <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_guoneichuan">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_guoneichuan">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=guoneichuan+1 %>" align="center">
                                游船
                            </th>
                            <th width="20%" align="center">
                                游船公司
                            </th>
                            <th width="30%" align="center">
                                地址
                            </th>
                            <th width="10%" align="center">
                                联系人
                            </th>
                            <th width="35%" align="center">
                                电话/传真
                            </th>
                            <%--    <th width="12%" align="center">
                                人数
                            </th>
                            <th width="13%" align="center">
                                登船日期
                            </th>
                            <th width="24%" align="left">
                                费用明细
                            </th>
                            <th width="10%" align="center">
                                支付方式
                            </th>
                            <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船 )%>
                            <%--<td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartDate"),ProviderToDate)%>
                            </td>
                            <td align="left">
                                <%# EyouSoft.Common.UtilsCommons.GetAPMX(Eval("PlanLargeFrequencyList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船.ToString())%>
                            </td>
                            <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_yongcan">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_yongcan">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=yongcan+1 %>" align="center">
                                用餐
                            </th>
                            <th width="20%" align="center">
                                餐馆名称
                            </th>
                            <th width="30%" align="center">
                                地址
                            </th>
                            <th width="10%" align="center">
                                联系人
                            </th>
                            <th width="35%" align="center">
                                电话/传真
                            </th>
                            <%--       <th width="12%" align="center">
                                人数
                            </th>
                            <th width="13%" align="center">
                                用餐时间
                            </th>
                            <th width="24%" align="left">
                                费用明细
                            </th>
                            <th width="10%" align="center">
                                支付方式
                            </th>
                            <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                                  <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船 )%>
                            <%--<td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartDate"),ProviderToDate)%>
                            </td>
                            <td align="left">
                                <%#EyouSoft.Common.UtilsCommons.GetAPMX(Eval("PlanDiningList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐.ToString())%>
                            </td>
                            <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_gouwu">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_gouwu">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=gouwu+1 %>" align="center">
                                购物
                            </th>
                            <th width="20%" align="center">
                                购物店(短信编号)
                            </th>
                            <th width="20%" align="center">
                                产品(短信编号)
                            </th>
                             <th width="10%" align="center">
                                流水
                            </th>
                            <%-- <th width="47%" align="left">
                                返利标准
                            </th>--%>
                            <th width="15%" align="center">
                                地址
                            </th>
                            <th width="10%" align="center">
                                联系人
                            </th>
                            <th width="20%" align="center">
                                电话/传真
                            </th>
                            <%--<th width="19%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                        <td align="left"><%#Eval("SourceName") %>(<%#Eval(("IdentityId"))%>)</td>
                            <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物 )%>
                            <%--<td align="center">
                                <%#Eval("Num")%>
                            </td>--%>
                            <%--<td align="left">
                                <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("ServiceStandard").ToString())%>
                            </td>--%>
                            <%--<td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_lingliao">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_lingliao">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=lingliao+1 %>" align="center">
                                领料
                            </th>
                            <th width="25%" align="center">
                                领料内容
                            </th>
                            <th width="30%" align="center">
                                数量
                            </th>
                            <th width="15%" align="center">
                                领料人
                            </th>
                            <th width="25%" align="center">
                                单价
                            </th>
                            <%--    <th width="10%" align="center">
                                支付方式
                            </th>
                            <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="left">
                                <%#Eval("SourceName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="center">
                                <%#Eval("ContactName")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("contactfax"), ProviderToMoney)%>
                            </td>
                            <%--     <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_qita">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_qita">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=qita +1%>" align="center">
                                其他
                            </th>
                            <th width="15%" align="center">
                                供应商名称
                            </th>
                            <th width="30%" align="center">
                                地址
                            </th>
                            <th width="10%" align="center">
                                联系人
                            </th>
                            <th width="40%" align="center">
                                电话/传真
                            </th>
                            <%--     <th width="10%" align="center">
                                支付方式
                            </th>
                            <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                                     <%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它 )%>
                            <%--    <td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="center">
                                <span title="<%# Eval("ServiceStandard") %>">
                                    <%# EyouSoft.Common.Utils.GetText2( Eval("ServiceStandard").ToString(),30,true) %></span>
                            </td>--%>
                            <%-- <td align="center">
                                <%#Eval("PaymentType").ToString() == "0" ? "" : Eval("PaymentType")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_guid">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
                <asp:Repeater runat="server" ID="rpt_guid">
                    <HeaderTemplate>
                        <tr>
                            <th width="5%" rowspan="<%=guid+1 %>" align="center">
                                导游
                            </th>
                            <th width="35%" align="center">
                                导游(短信编号)/电话
                            </th>
                            <th width="15%" align="center">
                                上团时间
                            </th>
                            <th width="15%" align="center">
                                上团地点
                            </th>
                            <th width="15%" align="center">
                                下团时间
                            </th>
                            <th width="15%" align="center">
                                下团地点
                            </th>
                            <%-- <th width="12%" align="center">
                                结算费用
                            </th>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("SourceName")%>(<%#GetGYSinfo(Eval("SourceId"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游)%>)/<%#Eval("ContactPhone")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartDate"),ProviderToDate)%>
                            </td>
                            <td><%#Eval("PlanGuide.OnLocation")%></td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("EndDate"),ProviderToDate)%>
                            </td>
                            <td><%#Eval("PlanGuide.NextLocation")%></td>
                            <%-- <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:PlaceHolder>
    </div>

    <script type="text/javascript">
        $(function() {
            $("input[data-check='check']").attr("checked", "checked");
            $("input[name='checkbox']").each(function() {
                $(this).click(function() {
                    if (!this.checked) {
                        $(this).closest("tr").next("tr").hide();
                    }
                    else {
                        $(this).closest("tr").next("tr").show();
                    }
                })
            })
            $("input[name='checkbox_t']").each(function() {
                $(this).click(function() {
                    if (!this.checked) {
                        $(this).closest("table").next("table").hide();
                    }
                    else {
                        $(this).closest("table").next("table").show();
                    }
                })
            })
            $("#cktourDetail").click(function() {
                if (!this.checked) {
                    $(this).closest("table").next("div").hide();
                }
                else {
                    $(this).closest("table").next("div").show();
                }

            })
        })
        function PrevFun() {
            $("input[data-check='check']").each(function() {
                if (!this.checked) {
                    $(this).closest("table").hide();
                }
            })
            window.setTimeout(function() {
                $("input[data-check='check']").each(function() {
                    if (!this.checked) {
                        $(this).closest("table").show();
                    }
                })
            }, 1000);
        }
    </script>

    <script type="text/javascript">
        function PrevFun() {
            $("input[type='checkbox'][name='checkbox']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                var checked = self.attr("checked");
                self.after("<label data-id='" + id + "' data-type='checkbox' data-checked='" + checked + "'></label>");
                if (!self.attr("checked")) {
                    self.closest("tr").hide();
                }
                self.remove();
            })
            $("input[type='checkbox'][name='checkbox_t']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                var checked = self.attr("checked");
                self.after("<label data-id='" + id + "' data-type='checkbox_t' data-checked='" + checked + "'></label>");
                if (!self.attr("checked")) {
                    self.closest("table").hide();
                }
                self.remove();
            })
            $("input[type='checkbox'][name='cktourDetail']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                var checked = self.attr("checked");
                self.after("<label data-id='" + id + "' data-type='cktourDetail' data-checked='" + checked + "'></label>");
                if (!self.attr("checked")) {
                    self.closest("table").hide();
                }
                self.remove();
            })
            setTimeout(function() {
                $("label[data-type='checkbox']").each(function() {
                    var id = $(this).attr("data-id");
                    var checded = $(this).attr("data-checked");
                    if (checded == 'false') {
                        $(this).after("<input type='checkbox' name='checkbox' data-class='checkbox' id='" + id + "' />")
                    }
                    else {
                        $(this).after("<input type='checkbox' name='checkbox' data-class='checkbox' id='" + id + "' checked='checked' />")
                    }
                    $(this).remove();
                })
                $("input[type='checkbox'][name='checkbox']").click(function() { showorhide(this, "1") });
                $("input[type='checkbox'][name='checkbox']").closest("tr").show();
                $("label[data-type='checkbox_t']").each(function() {
                    var id = $(this).attr("data-id");
                    var checded = $(this).attr("data-checked");
                    if (checded == 'false') {
                        $(this).after("<input type='checkbox' name='checkbox_t' data-class='checkbox' id='" + id + "' />");
                    }
                    else {
                        $(this).after("<input type='checkbox' name='checkbox_t' data-class='checkbox' id='" + id + "' checked='checked' />");
                    }
                    $(this).remove();
                })
                $("input[type='checkbox'][name='checkbox_t']").click(function() { showorhide(this, "2") });
                $("input[type='checkbox'][name='checkbox_t']").closest("table").show();
                $("label[data-type='cktourDetail']").each(function() {
                    var id = $(this).attr("data-id");
                    var checded = $(this).attr("data-checked");
                    if (checded == 'false') {
                        $(this).after("<input type='checkbox' name='cktourDetail' data-class='checkbox' id='" + id + "' />");
                    }
                    else {
                        $(this).after("<input type='checkbox' name='cktourDetail' data-class='checkbox' id='" + id + "' checked='checked' />");
                    }
                    $(this).remove();
                })
                $("input[type='checkbox'][name='cktourDetail']").click(function() { showorhide(this, "3") });
                $("input[type='checkbox'][name='cktourDetail']").closest("table").show();
            }, 1000);
        }

        function showorhide(obj, name) {
            var self = $(obj);
            var id = self.attr("id");
            if (!self.attr("checked")) {
                if (name == "1") {
                    self.closest("tr").next("tr").hide();
                }
                else if (name == "2") {
                    self.closest("table").next("table").hide();
                }
                else {
                    self.closest("table").next("div").find("table").hide();
                }
            }
            else {
                if (name == "1") {
                    self.closest("tr").next("tr").show();
                }
                else if (name == "2") {
                    self.closest("table").next("table").show();
                }
                else {
                    self.closest("table").next("div").show();
                }
            }
        }
        $(function() {
            $("input[type='checkbox'][data-check='check']").attr("checked", "checked")
            //$("input[type='checkbox'][name='checkbox']").click(function() { showorhide(this,"1") });
            //$("input[type='checkbox'][name='checkbox_t']").click(function() { showorhide(this,"2") });
        })
    </script>

</asp:Content>
