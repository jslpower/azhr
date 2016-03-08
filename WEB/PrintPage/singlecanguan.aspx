<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="singlecanguan.aspx.cs" Inherits="EyouSoft.Web.PrintPage.singlecanguan" %>

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
                <b class="font14">订餐通知单</b>
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
                您好！感谢贵公司长期对我社的友好合作，现将我社团队用餐计划传真于您，请回传确认！
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
        <tr>
            <th width="100" align="right">
                人数
            </th>
            <td>
                <asp:Label runat="server" ID="lbMealNumber"></asp:Label>
            </td>
            <th width="100" align="right">
                付款方式
            </th>
            <td>
                <asp:Label runat="server" ID="lbPaymentType"></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                费用总额
            </th>
            <td colspan="3">
                <asp:Label runat="server" ID="lbConfirmation"></asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                备注
            </th>
            <td colspan="3">
                <asp:Label runat="server" ID="lbCostRemarks"></asp:Label>
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
