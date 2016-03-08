<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="singlehuoche.aspx.cs" Inherits="EyouSoft.Web.PrintPage.singlehuoche" %>

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
                <b class="font14">火车票订单</b>
            </td>
            <td align="left">
            </td>
            <td align="right">
                <b class="font16">团号：<asp:Label ID="lbTourID" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="left" class="font14">
                您好！感谢贵公司长期对我社的友好合作，现将我社团队火车票计划传真于您，请给予落实！
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
        <tr>
            <th align="right" width="80px">
                数量
            </th>
            <td align="left" width="140px">
                <asp:Label ID="lblNum" runat="server" Text=""></asp:Label>
            </td>
            <th align="right">
                支付方式
            </th>
            <td>
                <asp:Literal runat="server" ID="ltrZhiFuFangShi"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th align="right" width="80px">
                费用总额
            </th>
            <td align="left" colspan="3">
                <asp:Label ID="lbTotleCost" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right" width="80px">
                备注
            </th>
            <td align="left" colspan="3">
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
                签发日期：<%=EyouSoft.Common.UtilsCommons.GetDateString(DateTime.Now,ProviderToDate)%>
            </td>
        </tr>
    </table>
</asp:Content>
