<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="singlejingdian.aspx.cs" Inherits="EyouSoft.Web.PrintPage.singleingdian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <div id="i_div_to">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
            <tr>
                <td width="50%" height="30" align="left">
                    <span class="font14">敬呈：<asp:TextBox ID="txtunitname" runat="server" CssClass="input120"></asp:TextBox>/<asp:TextBox
                        ID="txtunitContactname" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td width="25%" align="left">
                    <span class="font14">电话：<asp:TextBox ID="txtunittel" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td width="25%" align="right">
                    <span class="font14">传真：<asp:TextBox ID="txtunitfax" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
            </tr>
            <tr>
                <td width="50%" height="30" align="left">
                    <span class="font14">发自：<asp:TextBox ID="txtsourcename" runat="server" CssClass="input120"></asp:TextBox>/<asp:TextBox
                        ID="txtname" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td width="25%" align="left">
                    <span class="font14">电话：<asp:TextBox ID="txttel" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
                <td width="25%" align="right">
                    <span class="font14">传真：<asp:TextBox ID="txtfax" runat="server" CssClass="input100"></asp:TextBox></span>
                </td>
            </tr>
        </table>
    </div>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2">
        <tr>
            <td height="40" align="left">
                <b class="font14">景点通知单</b>
            </td>
            <td align="left">
            </td>
            <td align="right">
                <b class="font16">团号：<asp:Label ID="lbTourID" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="40" align="left" class="font14">
                    您好！感谢贵景区长期对我社的友好合作，现将我社团队计划传真于您，请回传确认！
                </td>
            </tr>
        </tbody>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" class="list_2">
        <tbody>
            <tr>
                <th align="right" width="100px">
                    人数
                </th>
                <td colspan="3">
                    <asp:Label ID="lbPeopleCount" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th align="right" width="100px">
                    费用总额
                </th>
                <td>
                    <asp:Label ID="lbTotleCost" runat="server" Text=""></asp:Label>
                </td>
                <th width="100px" align="right">
                    支付方式
                </th>
                <td>
                    <asp:Label ID="lblPayType" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th align="right" width="100px">
                    景点备注
                </th>
                <td colspan="3">
                    <asp:Label ID="LbRemark" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <table class="list_2" align="center" width="696" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td height="120" align="center" style="width: 50%">
                <div id="div1">
                    旅行社签章</div>
            </td>
            <td colspan="3" align="center">
                景区签章
            </td>
        </tr>
    </table>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="30" align="right">
                    签发日期：<%=EyouSoft.Common.UtilsCommons.GetDateString(DateTime.Now,ProviderToDate)%>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
