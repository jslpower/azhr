<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="LunChuanPrint.aspx.cs" Inherits="EyouSoft.Web.PrintPage.LunChuanPrint"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <div id="i_div_to">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
            <tr>
                <td width="50%" height="30" align="left">
                    <span class="font14">敬呈：<asp:TextBox ID="txtCompanyName" runat="server" CssClass="input120"></asp:TextBox>/<asp:TextBox
                        ID="txtCompanyContactName" runat="server" CssClass="input120"></asp:TextBox></span>
                </td>
                <td width="25%" align="left">
                    <span class="font14">电话：<asp:TextBox ID="txtContact" runat="server" CssClass="input120"></asp:TextBox></span>
                </td>
                <td width="25%" align="right">
                    <span class="font14">传真：<asp:TextBox ID="txtFax" runat="server" CssClass="input120"></asp:TextBox></span>
                </td>
            </tr>
            <tr>
                <td width="50%" height="30" align="left">
                    <span class="font14">发自：<asp:TextBox ID="txtSelfName" runat="server" CssClass="input120"></asp:TextBox>/<asp:TextBox
                        ID="txtSelfContactName" runat="server" CssClass="input120"></asp:TextBox></span>
                </td>
                <td width="25%" align="left">
                    <span class="font14">电话：<asp:TextBox ID="txtSelfContact" runat="server" CssClass="input120"></asp:TextBox></span>
                </td>
                <td width="25%" align="right">
                    <span class="font14">传真：<asp:TextBox ID="txtSelfFax" runat="server" CssClass="input120"></asp:TextBox></span>
                </td>
            </tr>
        </table>
    </div>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="">
        <tr>
            <td height="40" align="center">
                <b class="font24">订票委托单</b><select>
                    <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanBGType))) %>
                </select>
            </td>
        </tr>
        <tr>
            <td align="right">
                <b class="font16">团号：<asp:Label runat="server" ID="lbTourCode"></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="left" class="font14">
                您好！感谢贵公司长期对我社的友好合作，现将我社团队计划传真于您，请回传确认！
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
        <asp:Repeater ID="rptlist" runat="server">
            <HeaderTemplate>
                <tr>
                    <th width="80" rowspan="<%=listCount+1 %>" align="right">
                        航班
                    </th>
                    <th align="center">
                        出发时间
                    </th>
                    <th align="center">
                        出发地
                    </th>
                    <th align="center">
                        目的地
                    </th>
                    <th align="center">
                        轮船号
                    </th>
                    <th align="center">
                        舱位
                    </th>
                    <th align="center">
                        单价
                    </th>
                    <th align="center">
                        票数
                    </th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("DepartureTime"),ProviderToDate)%>
                        <%#Eval("Time")%>
                    </td>
                    <td align="center">
                        <%#Eval("Departure")%>
                    </td>
                    <td align="center">
                        <%#Eval("Destination")%>
                    </td>
                    <td align="center">
                        <%#Eval("Numbers")%>
                    </td>
                    <td align="center">
                        <%#((EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType)Eval("SeatType")).ToString()%>
                    </td>
                    <td align="center">
                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FarePrice"), ProviderToMoney)%>
                    </td>
                    <td align="center">
                        <%#Eval("PepolePayNum")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <th align="right" width="80px">
                费用明细
            </th>
            <td colspan="7" align="left">
                <asp:Label ID="lbCostDesc" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right" width="80px">
                费用总额
            </th>
            <td colspan="3" align="left">
                <asp:Label ID="lbTotleCost" runat="server" Text=""></asp:Label>
            </td>
                <th align="right" width="80px">
                支付方式
            </th>
            <td colspan="3" align="left">
                <asp:Label ID="lblPayType" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right" width="80px">
                备注
            </th>
            <td colspan="7" align="left">
                <asp:Label ID="lbRemark" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <table class="list_2" align="center" width="696" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td height="120" align="center" style="width: 50%">
                <div id="divImgCachet">
                    旅行社签章</div>
            </td>
            <td colspan="3" align="center">
                供应商签章
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" align="right">
                签发日期：<asp:Label runat="server" ID="lbDate"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
