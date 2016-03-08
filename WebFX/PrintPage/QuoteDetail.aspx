<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteDetail.aspx.cs" Inherits="EyouSoft.WebFX.PrintPage.QuoteDetail"
    ValidateRequest="false" MasterPageFile="~/MasterPage/Print.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td width="45%" height="30" align="left">
                    <asp:Literal runat="server" ID="ltJourneySpot"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="40" align="left" class="p_xinchen">
                    <%=(String)GetGlobalResourceObject("string", "详细行程")%>
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
                            <%=(String)GetGlobalResourceObject("string", "第")%><%#Eval("Days")%><%=(String)GetGlobalResourceObject("string", "天")%>
                        </td>
                        <td align="left" class="citybg">
                            <%#GetQuotePlanCity(Eval("QuotePlanCityList"), Eval("QuotePlanSpotList"))%>
                        </td>
                        <td width="170" align="left" class="zhusubg">
                            <%=(String)GetGlobalResourceObject("string", "酒店")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="hanggao">
                        <%#GetLngTypeSpotsGuideWord(Eval("QuotePlanSpotList"), Eval("Content").ToString())%>
                        </td>
                        <td valign="top" class="hanggao padd5">
                            <%#GetLngTypeHotel(Eval("HotelId1").ToString(),Eval("HotelName1").ToString())%><br>
                            <%#GetLngTypeHotel(Eval("HotelId2").ToString(),Eval("HotelName2").ToString())%><br>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td class="tjbg">
                            &nbsp;
                        </td>
                        <td class="hanggao tjbg">
                            <%=(String)GetGlobalResourceObject("string", "景点")%>：<b><%#GetQuotePlanSpot(Eval("QuotePlanSpotList"))%></b>
                        </td>
                        <td rowspan="4">
                            <img width="150" height="120" src="<%#Eval("FilePath") %>" complete="complete" style="visibility: <%#string.IsNullOrEmpty(Eval("FilePath").ToString())?"hidden":"visibility"%>"/>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="tjbg">
                            &nbsp;
                        </td>
                        <td class="hanggao tjbg">
                            <span style="display: <%#Convert.ToBoolean(Eval("IsBreakfast"))?"":"none"%>">
                            <%=(String)GetGlobalResourceObject("string", "早餐")%>：<b><%#GetLngTypeRestaurant(Eval("BreakfastRestaurantId").ToString())%></b>
                            </span>
                            <span style="display: <%#Convert.ToBoolean(Eval("IsLunch"))?"":"none"%>">
                            <%=(String)GetGlobalResourceObject("string", "中餐")%>：<b><%#GetLngTypeRestaurant(Eval("LunchRestaurantId").ToString())%></b>
                            </span>
                            <span style="display: <%#Convert.ToBoolean(Eval("IsSupper"))?"":"none"%>">
                            <%=(String)GetGlobalResourceObject("string", "晚餐")%>：<b><%#GetLngTypeRestaurant(Eval("SupperRestaurantId").ToString())%></b>
                            </span>
                        </td>
                    <%--</tr>
                    <tr>
                        <td class="tjbg">
                            &nbsp;
                        </td>
                        <td class="hanggao tjbg">
                            <%=(String)GetGlobalResourceObject("string", "购物")%>：<b><%#GetQuotePlanShop(Eval("QuotePlanShopList"))%></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="hanggao">
                            <%=(String)GetGlobalResourceObject("string", "参考菜单")%>：<span style="display: <%#Convert.ToBoolean(Eval("IsBreakfast"))?"":"none"%>">
                                      <%#GetLngTypeMenu(Eval("BreakfastMenuId").ToString(),Eval("BreakfastMenu").ToString())%><br>
                                      </span>
                                      <span style="display: <%#Convert.ToBoolean(Eval("IsLunch"))?"":"none"%>">
                                      <%#GetLngTypeMenu(Eval("LunchMenuId").ToString(),Eval("LunchMenu").ToString())%><br>
                                      </span>
                                      <span style="display: <%#Convert.ToBoolean(Eval("IsSupper"))?"":"none"%>">
                                      <%#GetLngTypeMenu(Eval("SupperMenuId").ToString(),Eval("SupperMenu").ToString())%>
                                      </span>
                        </td>
                    </tr>--%>
                </tbody>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <!--购物点-->
    <%--<table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="30" align="left" class="beizhu">
                    <%=(String)GetGlobalResourceObject("string", "购物点")%>：<br />
                    <asp:Literal runat="server" ID="ltShop"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <br />--%>
    <!--风味餐-->
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="30" align="left" class="beizhu">
                    <%=(String)GetGlobalResourceObject("string", "风味餐")%>：<br />
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
                    <%=(String)GetGlobalResourceObject("string", "赠送")%>：<br />
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
                    <%=(String)GetGlobalResourceObject("string", "小费名称")%>
                </td>
                <td height="30" align="left" class="beizhu">
                    <%=(String)GetGlobalResourceObject("string", "单价")%>
                </td>
                <td height="30" align="left" class="beizhu">
                    <%=(String)GetGlobalResourceObject("string", "天数")%>
                </td>
                <td height="30" align="left" class="beizhu">
                    <%=(String)GetGlobalResourceObject("string", "合计金额")%>
                </td>
                <td height="30" align="left" class="beizhu">
                    <%=(String)GetGlobalResourceObject("string", "备注")%>
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
                            <%=(String)GetGlobalResourceObject("string", "成人价")%>
                            <%#ToMoneyString(Eval("AdultPrice"))%>
                            <%=(String)GetGlobalResourceObject("string", "元人")%>
                            <img align="absmiddle" src="<%=Utils.ConvertToAbsolute("/images/child.gif") %>">
                            <%=(String)GetGlobalResourceObject("string", "儿童价")%>
                            <%#ToMoneyString(Eval("ChildPrice"))%>
                            <%=(String)GetGlobalResourceObject("string", "元人")%>&nbsp;<img style="vertical-align: middle" src="<%=Utils.ConvertToAbsolute("/images/lindui.gif") %>">
                            <%=(String)GetGlobalResourceObject("string", "领队")%>
                            <%#ToMoneyString(Eval("LeadPrice"))%>
                            <%=(String)GetGlobalResourceObject("string", "元人")%>&nbsp; <%=(String)GetGlobalResourceObject("string", "单房差")%>
                            <%#ToMoneyString(Eval("SingleRoomPrice"))%>
                            <%=(String)GetGlobalResourceObject("string", "元人")%> <%=(String)GetGlobalResourceObject("string", "其他费用")%>
                            <%#ToMoneyString(Eval("OtherPrice"))%>
                            <%=(String)GetGlobalResourceObject("string", "元人")%>&nbsp;
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
                    <%=(String)GetGlobalResourceObject("string", "备注")%>：<br />
                    <asp:Literal runat="server" ID="ltRemark"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
