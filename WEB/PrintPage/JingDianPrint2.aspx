<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="JingDianPrint2.aspx.cs" Inherits="EyouSoft.Web.PrintPage.JingDianPrint2"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
            <link href="/Css/print1.css" rel="stylesheet" type="text/css" />
<table width="696" border="0" cellpadding="0" cellspacing="0" class="Basic_Table" style="margin-top:5px;">
  <tr>
    <td height="40" align="left" class="BTitle">订景点(含表演秀/游船)委托单</td>
    <td align="right" valign="middle" class="zhtai">委托状态：<select name="select" class="selectbk" id="select">
        <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanBGType)))%>
    </select></td>
  </tr>
</table>
<div id="i_div_to">
        <div class="Basic_Table Tablebk_top bg01">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_00f font16">
    <td width="34%" height="40">TO：<asp:Literal ID="txtunitname" runat="server"></asp:Literal>/<asp:Literal ID="txtunitContactname" runat="server"></asp:Literal></td>
    <td width="22%">TEL：<asp:Literal ID="txtunittel" runat="server"></asp:Literal></td>
    <td width="22%">FAX：<asp:Literal ID="txtunitfax" runat="server"></asp:Literal></td>
    <td width="22%">手机：<asp:Literal runat="server" ID="txtCompanyMob"></asp:Literal></td>
  </tr>
</table>
</div>

<div class="Basic_Table Tablebk_bot bg02">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_00f font16">
    <td width="34%" height="40">FROM：<asp:Literal ID="txtsourcename" runat="server"></asp:Literal>/<asp:Literal ID="txtname" runat="server"></asp:Literal></td>
    <td width="22%">TEL：<asp:Literal ID="txttel" runat="server"></asp:Literal></td>
    <td width="22%">FAX：<asp:Literal ID="txtfax" runat="server"></asp:Literal> </td>
    <td width="22%">手机：<asp:Literal runat="server" ID="txtSelfMob"></asp:Literal></td>
  </tr>
</table>
</div>

    </div>
    <div class="Basic_Table Tablebk_top bg03" style="margin-top:5px;">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right" class="borderR">公司团号</th>
    <td width="150" class="borderR"><asp:Label ID="lbTourID" runat="server" Text=""></asp:Label></td>
    <th width="90" align="right" class="borderR">国籍</th>
    <td class="borderR"><asp:Label runat="server" ID="lblGuoJi"></asp:Label></td>
    <th width="50" align="right" class="borderR">人数</th>
    <td><asp:Literal runat="server" ID="txtPeople"></asp:Literal></td>
  </tr>
  </table>
</div>

<div class="Basic_Table Tablebk_mid" style="margin-top:-8px;">
<table width="698" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right">预订日期</th>
    <td width="150" class="borderR"><asp:Literal runat="server" ID="ltlIssueTime"></asp:Literal></td>
    <th width="90" align="right">席位等级</th>
    <td class="borderR">
<asp:Repeater ID="rptlist" runat="server">
            <ItemTemplate>
                        <%#Eval("Seats")%>
            </ItemTemplate>
        </asp:Repeater>
     </td>
    </tr>
  <tr class="font_000 font16">
    <th align="right">单价</th>
    <td class="borderR"><asp:Literal runat="server" ID="ltlPrice"></asp:Literal></td>
    <th align="right">付款方式</th>
    <td class="borderR"><asp:Label runat="server" ID="lbPaymentType"></asp:Label></td>
  </tr>
  <tr class="font_000 font16">
    <th align="right">导游姓名</th>
    <td class="borderR"><asp:Label runat="server" ID="lblDaoYou"></asp:Label></td>
    <th align="right">手机号码</th>
    <td class="borderR"><asp:Label runat="server" ID="lblMobile"></asp:Label></td>
  </tr>
  </table>
</div>

<div class="Basic_Table Tablebk_bot bg04">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right" class="borderR">备注</th>
    <td><asp:Label ID="LbRemark" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
</div>

<table width="696" border="0" cellpadding="0" cellspacing="0" class="Basic_Table">
  <tr>
    <td height="40" align="right" class="font16 font_00f"><%=DateTime.Now.Year %>&nbsp;年&nbsp;<%=DateTime.Now.Month %>&nbsp;月&nbsp;<%=DateTime.Now.Day %>&nbsp;日</td>
  </tr>
</table>

</asp:Content>
