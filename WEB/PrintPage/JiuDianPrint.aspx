<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true" CodeBehind="JiuDianPrint.aspx.cs" Inherits="EyouSoft.Web.PrintPage.JiuDianPrint"   ValidateRequest="false" %>
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
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2">
        <tr>
            <td height="40" align="left">
                <b class="font14">酒店订房单</b>
            </td>
            <td align="left">
            </td>
            <td align="right">
                <b class="font16">团号：<asp:Label runat="server" ID="lbTourCode"></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="left" class="font14">
                您好！感谢贵公司长期对我社的友好合作，现将我社团队用房计划传真于您，请回传确认！
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
        <tr>
            <th width="100" align="right">
                入住时间
            </th>
            <td width="132">
                <asp:Label runat="server" ID="lbStartDate">
                </asp:Label>
            </td>
            <th width="100" align="right">
                离店时间
            </th>
            <td width="132">
                <asp:Label runat="server" ID="lbEndDate">
                </asp:Label>
            </td>
            <th width="100" align="right">
                天数
            </th>
            <td width="132">
                <asp:Label runat="server" ID="lbDays">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                房型
            </th>
            <td colspan="5">
                <asp:Repeater runat="server" ID="rpt_PlanHotelRoomList">
                    <ItemTemplate>
                        <%#Eval("RoomType") %>：<%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("UnitPrice"), ProviderToMoney)%>元/间
                        X
                        <%#Eval("Quantity")%>间 
                        =
                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("Total"), ProviderToMoney)%>元
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <th align="right">
                房间数量
            </th>
            <td>
                <asp:Label runat="server" ID="lbNum">
                </asp:Label>
            </td>
            <th align="right">
                是否含早
            </th>
            <td>
                <asp:Label runat="server" ID="lbIsMeal">
                </asp:Label>
            </td>
            <th align="right">
                支付方式<!--免费房数-->
            </th>
            <td>
                <asp:Literal runat="server" ID="ltrZhiFuFangShi"></asp:Literal>
                <!--<asp:Label runat="server" ID="lbFreeNumber">
                </asp:Label>-->
            </td>
        </tr>
        <!--<tr>
            <th align="right">
                早餐费用
            </th>
            <td colspan="5">
                <asp:Label runat="server" ID="lbMealCost">
                </asp:Label>
            </td>
        </tr>-->
        <tr>
            <th align="right">
                用房要求
            </th>
            <td colspan="5">
                <asp:Label runat="server" ID="lbCostRemarks">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                费用明细
            </th>
            <td colspan="5">
                <asp:Label runat="server" ID="lbCostDetail">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                费用总额
            </th>
            <td colspan="5">
                <asp:Label runat="server" ID="lbConfirmation">
                </asp:Label>
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
