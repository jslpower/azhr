<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XingChengDan.aspx.cs" Inherits="EyouSoft.WebFX.PrintPage.XingChengDan" MasterPageFile="~/MasterPage/Print.Master" ValidateRequest="false" Title="简要行程单"%>
<%@ Import Namespace="EyouSoft.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td>
                    <asp:Literal runat="server" ID="ltJourneySpot"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
    <tr><td><span style="font-size: medium">各地接待社及酒店：</span></td></tr>
    <asp:Repeater runat="server" ID="rpt">
    <ItemTemplate>
    <tr>
    <td><%#Eval("DiJieName")%></td>
    <td><%#Eval("Contact")%></td>
    <td>T：<%#Eval("Phone")%></td>
    <td>F：<%#Eval("Fax")%></td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="40" align="left" class="p_xinchen">
                    详细行程
                </td>
            </tr>
        </tbody>
    </table>
    <asp:Repeater runat="server" ID="rpPlan">
        <ItemTemplate>
            <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 0;">
                <tbody>
                    <tr class="font14">
                        <td width="80" height="25" align="center" class="daybg">
                            <%--第<%#Eval("Days")%>天--%><%#GettourDate(Container.ItemIndex)%>
                        </td>
                        <td align="left" class="citybg">
                            <%#GetQuotePlanCity(Eval("TourPlanCityList"), Eval("TourPlanSpotList"))%>
                        </td>
                        <td width="170" align="left" class="zhusubg">
                            住宿
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="hanggao">
                            <%--景点：<b><%#GetQuotePlanSpot(Eval("TourPlanSpotList"))%></b>--%>
                            <%#GetLngTypeSpotsGuideWord(Eval("TourPlanSpotList"), Eval("Content").ToString())%>
                        </td>
                        <td valign="top" class="hanggao padd5">
                            <%#GetLngTypeHotel(Eval("HotelId1").ToString(),Eval("HotelName1").ToString())%><br>
                        </td>
                    </tr>
                    <tr>
                        <td class="tjbg">
                            &nbsp;
                        </td>
                        <td class="hanggao tjbg">
                            <span style="display: <%#Convert.ToBoolean(Eval("IsBreakfast"))?"":"none"%>">早餐：<b><%#GetLngTypeRestaurant(Eval("BreakfastRestaurantId").ToString())%></b>
                            </span><span style="display: <%#Convert.ToBoolean(Eval("IsLunch"))?"":"none"%>">午餐：<b><%#GetLngTypeRestaurant(Eval("LunchRestaurantId").ToString())%></b>
                            </span><span style="display: <%#Convert.ToBoolean(Eval("IsSupper"))?"":"none"%>">晚餐：<b><%#GetLngTypeRestaurant(Eval("SupperRestaurantId").ToString())%></b>
                            </span>
                        </td>
                        <%--<td rowspan="3">
                            <img width="150" height="120" src="<%#Eval("FilePath") %>" complete="complete" style="visibility: <%#string.IsNullOrEmpty(Eval("FilePath").ToString())?"hidden":"visibility"%>" />
                        </td>--%>
                    </tr>
                   
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="hanggao">
                            参考菜单：<span style="display: <%#Convert.ToBoolean(Eval("IsBreakfast"))?"":"none"%>">
                                <%#GetLngTypeMenu(Eval("BreakfastMenuId").ToString(),Eval("BreakfastMenu").ToString())%><br>
                            </span><span style="display: <%#Convert.ToBoolean(Eval("IsLunch"))?"":"none"%>">
                                <%#GetLngTypeMenu(Eval("LunchMenuId").ToString(),Eval("LunchMenu").ToString())%><br>
                            </span><span style="display: <%#Convert.ToBoolean(Eval("IsSupper"))?"":"none"%>">
                                <%#GetLngTypeMenu(Eval("SupperMenuId").ToString(),Eval("SupperMenu").ToString())%>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <!--风味餐-->
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="30" align="left" class="beizhu">
                    风味餐：<br />
                    <asp:Literal runat="server" ID="ltFoot"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <!--赠送-->
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="30" align="left" class="beizhu">
                    赠送：<br />
                    <asp:Literal runat="server" ID="ltGive"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <!--小费-->
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" style="background: none repeat scroll 0 0 #F8F8F8;">
        <tbody>
            <tr>
                <td height="30" align="left" class="beizhu">
                    小费名称
                </td>
                <td height="30" align="left" class="beizhu">
                    单价
                </td>
                <td height="30" align="left" class="beizhu">
                    天数
                </td>
                <td height="30" align="left" class="beizhu">
                    合计金额
                </td>
                <td height="30" align="left" class="beizhu">
                    备注
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rpTip">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="left" class="beizhu">
                            <%#Eval("Tip") %>
                        </td>
                        <td height="30" align="left" class="beizhu">
                            <%#ToMoneyString( Eval("Price"))%>
                        </td>
                        <td height="30" align="left" class="beizhu">
                            <%#Eval("Days")%>
                        </td>
                        <td height="30" align="left" class="beizhu">
                            <%#ToMoneyString( Eval("SumPrice")) %>
                        </td>
                        <td height="30" align="left" class="beizhu">
                            <%#Eval("Remark")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <br />
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <asp:Repeater runat="server" ID="rpPrice">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="left" class="beizhu">
                            <img width="16" height="15" align="absmiddle" src="<%=Utils.ConvertToAbsolute("/images/chengren.gif") %>">
                            成人价
                            <%#ToMoneyString(Eval("AdultPrice"))%>
                            元/人
                            <img align="absmiddle" src="<%=Utils.ConvertToAbsolute("/images/child.gif") %>">
                            儿童价
                            <%#ToMoneyString(Eval("ChildPrice"))%>
                            元/人&nbsp;<img style="vertical-align: middle" src="<%=Utils.ConvertToAbsolute("/images/lindui.gif") %>">
                            领队
                            <%#ToMoneyString(Eval("LeadPrice"))%>
                            元/人&nbsp; 单房差
                            <%#ToMoneyString(Eval("SingleRoomPrice"))%>
                            元 其它费用
                            <%#ToMoneyString(Eval("OtherPrice"))%>
                            元&nbsp;
                        </td>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <!--备注-->
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="30" align="left" class="beizhu">
                    备注：<br />
                    <asp:Literal runat="server" ID="ltRemark"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
