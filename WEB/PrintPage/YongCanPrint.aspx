<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="YongCanPrint.aspx.cs" Inherits="EyouSoft.Web.PrintPage.YongCanPrint"   ValidateRequest="false" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.PlanStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <div id="i_div_to">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
            <tr>
                <td width="10%" height="30" align="left">
                    <span class="font14">To：</span>
                </td>
                <td width="46%" height="30" align="left">
                    <span class="font14"><asp:TextBox ID="txtCompanyName" runat="server" CssClass="input180"></asp:TextBox>/<asp:TextBox
                        ID="txtCompanyContactName" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td width="10%" align="left">
                    <span class="font14">Tel：</span>
                </td>
                <td width="12%" align="left">
                    <span class="font14"><asp:TextBox ID="txtContact" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td width="10%" align="right">
                    <span class="font14">Fax：</span>
                </td>
                <td width="12%" align="right">
                    <span class="font14"><asp:TextBox ID="txtFax" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
            </tr>
            <tr>
                <td height="30" align="left">
                    <span class="font14">From：</span>
                </td>
                <td height="30" align="left">
                    <span class="font14"><asp:TextBox ID="txtSelfName" runat="server" CssClass="input180"></asp:TextBox>/<asp:TextBox
                        ID="txtSelfContactName" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td align="left">
                    <span class="font14">Tel：</span>
                </td>
                <td align="left">
                    <span class="font14"><asp:TextBox ID="txtSelfContact" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td align="right">
                    <span class="font14">Fax：</span>
                </td>
                <td align="right">
                    <span class="font14"><asp:TextBox ID="txtSelfFax" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
            </tr>
        </table>
    </div>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="">
        <tr>
            <td height="40" align="center">
                <b class="font24">Meal Booking</b><%--<select>
                    <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanBGType))) %>
                </select>--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                <b class="font16">File No：<asp:Label runat="server" ID="lbTourCode"></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="left" class="font14">
                Hi!Please send the confirmation back ASAP.Thanks a lot!
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
        <tr>
            <th align="right" style="width:7%">
                Date
            </th>
            <td style="width:20%">
                <asp:Label runat="server" ID="lbStartDate"></asp:Label>
            </td>
            <th align="right" style="width:20%">
                Guide
            </th>
            <td style="width:20%">
                <asp:Label runat="server" ID="lblDaoYou"></asp:Label>
            </td>
            <th align="right" style="width:20%">
                Guide Mobile
            </th>
            <td style="width:13%">
                <asp:Label runat="server" ID="lblMobile"></asp:Label>
            </td>
        </tr>
        <asp:Repeater ID="rptlist" runat="server">
            <HeaderTemplate>
                <tr>
                    <th rowspan="<%=listCount+1 %>" align="right">
                        Meal
                    </th>
                    <th align="center" colspan="5">
                        Meal/Detail/Adult+Child/Table/<%if(PriceType==PlanDiningPriceType.人)%>
                        <%{%>Adult Price<%}%><%else %><%{%>Single Price<%}%>/<%if(PriceType==PlanDiningPriceType.人)%>
                        <%{%>Child Price/Free/Discount<%}%>/Amount
                    </th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="left" colspan="6">
                        <%#Eval("MenuName")+GetMenu(Eval("MenuId"))%>/<%#Eval("DiningType")%>/<%#Eval("AdultNumber") + "+" + Eval("ChildNumber")%>/<%#Eval("TableNumber")%>/<%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultUnitPrice"), ProviderToMoney)%>/<%if(PriceType==PlanDiningPriceType.人)%><%{%><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ChildPrice"), ProviderToMoney)%>/<%#Eval("FreeNumber")%>/<%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FreePrice"), ProviderToMoney)%>/<%}%><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPrice"), ProviderToMoney)%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <th align="right">
                Total
            </th>
            <td>
                <asp:Label runat="server" ID="lbConfirmation"></asp:Label>
            </td>
            <th align="right">
                Payment
            </th>
            <td>
                <asp:Label runat="server" ID="lbPaymentType"></asp:Label>
            </td>
            <th align="right">
                Free
            </th>
            <td>
                <asp:Label runat="server" ID="lblMian">No</asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                Note
            </th>
            <td colspan="5">
                <asp:Label runat="server" ID="lbCostRemarks"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="list_2" align="center" width="696" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td height="120" align="center" style="width: 50%">
                <div id="divImgCachet">
                    </div>
            </td>
            <td colspan="3" align="center">
                Sign by
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" align="right">
                Sent date：<asp:Label runat="server" ID="lbDate"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
